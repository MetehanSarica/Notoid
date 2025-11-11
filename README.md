<p align="center">
  <img src="Resources/NotoidLogo.png" width="120" alt="Notoid Logo">
</p>

<h1 align="center">Notoid</h1>
<p align="center">
  <b>AI-powered Study Assistant for Students and Researchers</b><br>
  <i>Summarize PDFs, Word, and PowerPoint notes into clean, structured study PDFs.</i>
</p>

---

## 🧩 Features

- 🤖 AI-powered summarization using **Google Gemini API**
- 🧾 Supports multiple file formats: **PDF**, **DOCX**, **PPTX**
- 🪄 Simple and clean **Windows Forms UI**
- 📘 Generates professional summary PDFs with **QuestPDF**
- 🧠 Academic-grade AI summaries
- 📂 Multi-file support
- ⏱️ Real-time progress tracking during summarization
- 🧰 Built for productivity and focus

---

## 🧰 Technologies Used

| Category | Technology |
|-----------|-------------|
| Language | C# (.NET Framework 4.8) |
| Framework | Windows Forms |
| AI Model | Google Gemini API |
| PDF Engine | QuestPDF |
| Document Parsing | PdfPig & OpenXML SDK |
| Configuration | System.Configuration |

---

## ⚙️ Configuration

Before running the project, configure your API key inside `App.config`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="GeminiApiKey" value="YOUR_API_KEY_HERE"/>
  </appSettings>
</configuration>
