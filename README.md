from pathlib import Path

readme_content = """# Notoid

**Notoid** is an AI-powered study assistant that summarizes your documents into concise, well-structured PDF study notes.  
It supports multiple file formats including **PDF**, **Word (.docx)**, and **PowerPoint (.pptx)**.

---

## 🧩 Features

- Summarize academic or professional documents using **Google Gemini AI**
- Support for multiple file formats: PDF, DOCX, PPTX  
- Simple and clean **Windows Forms UI**
- Generate high-quality summary PDFs with **QuestPDF**
- Custom app icon and modern design
- Multi-file processing support
- Automatic progress tracking during summarization

---

## 🧰 Technologies Used

- **C# (.NET Framework 4.8 / WinForms)**
- **Google Gemini API**
- **QuestPDF**
- **PdfPig**
- **OpenXML SDK (Word + PowerPoint)**
- **System.Configuration** for secure key management

---

## ⚙️ Configuration

Before running the project, create an `App.config` file (or modify the existing one) and add your **Gemini API Key**:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="GeminiApiKey" value="YOUR_API_KEY_HERE"/>
  </appSettings>
</configuration>
