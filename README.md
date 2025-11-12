<div align="center">
  <img src="https://github.com/MetehanSarica/Notoid/raw/main/Notoid/Resources/NotoidLogo.png" alt="Notoid Logo" width="150" height="150">
  <h1>Notoid</h1>
  <p><b>AI-powered Study Assistant for Students and Researchers</b></p>
  <p><i>Summarize PDFs, Word, and PowerPoint notes into clean, structured study PDFs.</i></p>

  </div>

---

## 🚀 About The Project

**Notoid** is a desktop application designed to streamline your study and research workflow. It leverages the power of the Google Gemini AI to intelligently summarize large documents, including PDFs, Word files (.docx), and PowerPoint presentations (.pptx). Upload your notes, articles, or lecture slides, and Notoid will generate a concise, structured, and easy-to-read PDF summary, helping you focus on the most important information.

Built with productivity in mind, Notoid offers a clean and simple Windows Forms interface for a distraction-free experience.

## ✨ Features

* 🤖 **AI-Powered Summarization**: Utilizes the **Google Gemini API** for high-quality, academic-grade summaries.
* 🧾 **Multi-Format Support**: Natively handles **PDF**, **DOCX**, and **PPTX** files.
* 📘 **Professional PDF Output**: Generates clean, structured summary documents using the **QuestPDF** library.
* 📂 **Batch Processing**: Supports summarizing multiple files at once.
* ⏱️ **Real-Time Progress**: Track the summarization progress in real-time.
* 🪄 **Simple & Clean UI**: A straightforward **Windows Forms** interface built for ease of use.

## 🛠️ Built With

This project is built on the .NET Framework and utilizes several powerful libraries:

| Category | Technology |
| :--- | :--- |
| Language | **C# (.NET Framework 4.8)** |
| Framework | **Windows Forms** |
| AI Model | **Google Gemini API** |
| PDF Generation | **QuestPDF** |
| **Document Parsing** | PdfPig (for PDF), OpenXML SDK (for DOCX & PPTX) |
| Configuration | **System.Configuration** |

## 🏁 Getting Started

Follow these instructions to get a copy of the project up and running on your local machine for development and testing.

### Prerequisites

* **.NET Framework 4.8** (Developer Pack)
* **Visual Studio 2019** or later
* **Google Gemini API Key**: You must have a valid API key from [Google AI Studio](https://aistudio.google.com/app/apikey).

### Installation

1.  **Clone the repository:**
    ```sh
    git clone [https://github.com/MetehanSarica/Notoid.git](https://github.com/MetehanSarica/Notoid.git)
    ```
2.  **Open the solution:**
    Open `Notoid.sln` in Visual Studio.
3.  **Restore NuGet Packages:**
    Right-click the solution in Solution Explorer and select "Restore NuGet Packages".

### Configuration

Before you can run the project, you must add your Google Gemini API key.

1.  Open the `App.config` file in the `Notoid` project.
2.  Find the `<appSettings>` section.
3.  Update the `GeminiApiKey` value with your own API key:

    ```xml
    <?xml version="1.0" encoding="utf-8"?>
    <configuration>
      <appSettings>
        <add key="GeminiApiKey" value="YOUR_API_KEY_HERE"/>
      </appSettings>
    </configuration>
    ```

4.  Save the file and build the solution (F6 or Ctrl+Shift+B).

## 📖 Usage

1.  Run the application from Visual Studio (by pressing F5) or by executing the compiled `.exe` file.
2.  Click the "Add Files" button to select one or more PDF, DOCX, or PPTX files.
3.  The files will appear in the list.
4.  Click the "Start Summarization" button.
5.  The application will process each file and generate a summary. A progress bar will show the current status.
6.  Once complete, the summarized `.pdf` files will be saved in a "Summaries" folder located in the same directory as the application executable.

## 🤝 Contributing

Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".

1.  Fork the Project
2.  Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3.  Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4.  Push to the Branch (`git push origin feature/AmazingFeature`)
5.  Open a Pull Request

## 📄 License

Distributed under the MIT License. See `LICENSE.txt` for more information.

## 📧 Contact

Metehan Sarica - [@MetehanSarica](https://github.com/MetehanSarica)

Project Link: [https://github.com/MetehanSarica/Notoid](https://github.com/MetehanSarica/Notoid)
