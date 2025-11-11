/*
 * GEREKLİ KÜTÜPHANELER (NuGet):
 * - DocumentFormat.OpenXml
 * - PdfPig
 * - QuestPDF
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.Json;
using System.Drawing;
using DocumentFormat.OpenXml.Packaging;
using Word = DocumentFormat.OpenXml.Wordprocessing;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using DocumentFormat.OpenXml.Presentation;
using Notoid; // Resources için eklendi
using System.Configuration;


namespace Notoid
{
    public partial class MainForm : Form
    {
        private readonly string ApiKey = ConfigurationManager.AppSettings["GeminiApiKey"] ?? "";
        private const string ApiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash-preview-09-2025:generateContent?key=";
        private static readonly HttpClient httpClient = new HttpClient();

        private List<string> filePaths = new List<string>();

        public MainForm()
        {
            InitializeComponent();

            // ✅ İkonu Resources’tan yükle
            using (var ms = new MemoryStream(Resources.NotoidLogoIcon))
            {
                this.Icon = new Icon(ms, new System.Drawing.Size(32, 32));
            }

            this.Text = "Notoid v1.3.4";
            QuestPDF.Settings.License = LicenseType.Community;
            UpdateSummarizeButtonState();
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select Files to Summarize";
            fileDialog.Filter = "All Supported Files (*.pdf;*.docx;*.pptx)|*.pdf;*.docx;*.pptx|PDF Files (*.pdf)|*.pdf|Word Documents (*.docx)|*.docx|PowerPoint Files (*.pptx)|*.pptx";
            fileDialog.Multiselect = true;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filePath in fileDialog.FileNames)
                {
                    if (!filePaths.Contains(filePath))
                    {
                        filePaths.Add(filePath);
                        lstFiles.Items.Add(Path.GetFileName(filePath));
                    }
                }
                UpdateSummarizeButtonState();
            }
        }

        private async void btnSummarize_Click(object sender, EventArgs e)
        {
            if (ApiKey == "YOUR_API_KEY")
            {
                MessageBox.Show("Please enter your API key first.", "Missing API Key",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (filePaths.Count == 0)
            {
                MessageBox.Show("Please select at least one file to summarize.", "No Files Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog savePdfDialog = new SaveFileDialog();
            savePdfDialog.Title = "Save Summary PDF";
            savePdfDialog.Filter = "PDF File (*.pdf)|*.pdf";
            savePdfDialog.FileName = "MyStudySummary.pdf";

            if (savePdfDialog.ShowDialog() == DialogResult.OK)
            {
                string savePath = savePdfDialog.FileName;
                LockUI(true);
                lblStatus.Text = "Starting summarization process...";
                progressBar.Value = 0;
                progressBar.Maximum = filePaths.Count;

                StringBuilder allSummaries = new StringBuilder();

                try
                {
                    for (int i = 0; i < filePaths.Count; i++)
                    {
                        string filePath = filePaths[i];
                        string fileName = Path.GetFileName(filePath);
                        lblStatus.Text = $"Reading '{fileName}'...";

                        string content = await ReadFileContentAsync(filePath);

                        if (content.Length > 20000)
                            content = content.Substring(0, 20000);

                        if (string.IsNullOrWhiteSpace(content))
                        {
                            allSummaries.AppendLine($"--- SUMMARY OF {fileName.ToUpper()} ---");
                            allSummaries.AppendLine("[Skipped: File is empty or unreadable.]\n\n");
                            progressBar.Value = i + 1;
                            continue;
                        }

                        lblStatus.Text = $"Summarizing '{fileName}' with AI...";
                        string summary = await SummarizeWithAIAsync(content);

                        allSummaries.AppendLine($"--- SUMMARY OF {fileName.ToUpper()} ---");
                        allSummaries.AppendLine(summary);
                        allSummaries.AppendLine("\n\n");

                        progressBar.Value = i + 1;
                    }

                    lblStatus.Text = "Creating PDF file...";
                    await CreatePdfAsync(allSummaries.ToString(), savePath);

                    lblStatus.Text = "Process completed!";
                    MessageBox.Show($"Your summary file has been created successfully:\n{savePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "An error occurred.";
                    MessageBox.Show($"An error occurred during the process: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    LockUI(false);
                    lblStatus.Text = "Status: Waiting...";
                    progressBar.Value = 0;
                }
            }
        }

        private void LockUI(bool isLocked)
        {
            btnAddFiles.Enabled = !isLocked;
            btnSummarize.Enabled = !isLocked;
            lstFiles.Enabled = !isLocked;
        }

        private void UpdateSummarizeButtonState()
        {
            btnSummarize.Enabled = filePaths.Count > 0;
        }

        private async Task<string> ReadFileContentAsync(string filePath)
        {
            return await Task.Run(() =>
            {
                string fileExtension = Path.GetExtension(filePath).ToLower();
                StringBuilder content = new StringBuilder();

                try
                {
                    if (fileExtension == ".docx")
                    {
                        using (WordprocessingDocument doc = WordprocessingDocument.Open(filePath, false))
                        {
                            if (doc.MainDocumentPart?.Document.Body != null)
                            {
                                foreach (var para in doc.MainDocumentPart.Document.Body.Elements<Word.Paragraph>())
                                    content.AppendLine(para.InnerText);
                            }
                        }
                    }
                    else if (fileExtension == ".pptx")
                    {
                        using (PresentationDocument ppt = PresentationDocument.Open(filePath, false))
                        {
                            var slideParts = ppt.PresentationPart?.SlideParts;
                            if (slideParts == null)
                                return $"[Error: No slides found in {Path.GetFileName(filePath)}]";

                            foreach (var slide in slideParts)
                            {
                                foreach (var text in slide.Slide.Descendants<DocumentFormat.OpenXml.Drawing.Text>())
                                    content.AppendLine(text.Text);
                            }
                        }
                    }
                    else if (fileExtension == ".pdf")
                    {
                        using (PdfDocument document = PdfDocument.Open(filePath))
                        {
                            foreach (Page page in document.GetPages())
                                content.AppendLine(page.Text);
                        }
                    }
                    else
                    {
                        content.Append($"[Unsupported file type: {Path.GetFileName(filePath)}]");
                    }
                }
                catch (Exception ex)
                {
                    return $"Error reading file {Path.GetFileName(filePath)}: {ex.Message}";
                }

                return content.ToString();
            });
        }

        private async Task<string> SummarizeWithAIAsync(string textToSummarize)
        {
            try
            {
                string fullApiUrl = $"{ApiUrl}{ApiKey}";
                string prompt = $"You are an expert academic assistant. Summarize the following text for a university student. Focus on the main concepts, key definitions, and critical conclusions. Present the summary clearly.\n\nText:\n\"{textToSummarize}\"";

                var payload = new
                {
                    contents = new[]
                    {
                        new { parts = new[] { new { text = prompt } } }
                    },
                };

                string jsonPayload = JsonSerializer.Serialize(payload);
                HttpContent requestBody = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(fullApiUrl, requestBody);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return $"Error from AI API: {response.StatusCode}\nDetails: {jsonResponse}";

                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    JsonElement root = doc.RootElement;

                    if (root.TryGetProperty("candidates", out JsonElement candidates) && candidates.GetArrayLength() > 0)
                    {
                        string summary = candidates[0].GetProperty("content")
                                           .GetProperty("parts")[0]
                                           .GetProperty("text")
                                           .GetString() ?? "[AI returned an empty summary.]";
                        return summary;
                    }
                    else
                    {
                        return "Error: Could not parse the AI response.";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"An error occurred while contacting the AI: {ex.Message}";
            }
        }

        private Task CreatePdfAsync(string allSummaries, string savePath)
        {
            return Task.Run(() =>
            {
                QuestPDF.Fluent.Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(2, Unit.Centimetre);
                        page.DefaultTextStyle(style =>
                            style.FontSize(10)
                                 .FontFamily("Calibri"));

                        page.Header()
                            .Text("Notoid - Ders Özetleri")
                            .FontSize(14)
                            .FontColor(Colors.Grey.Darken2)
                            .SemiBold();

                        page.Content()
                            .PaddingVertical(1, Unit.Centimetre)
                            .Column(col =>
                            {
                                col.Item().Text(allSummaries);
                            });

                        page.Footer()
                            .AlignCenter()
                            .Text(text =>
                            {
                                text.Span("Sayfa ");
                                text.CurrentPageNumber();
                                text.Span(" / ");
                                text.TotalPages();
                            });
                    });
                })
                .GeneratePdf(savePath);
            });
        }
    }
}
