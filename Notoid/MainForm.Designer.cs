namespace Notoid
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnAddFiles = new Button();
            btnSummarize = new Button();
            lstFiles = new ListBox();
            progressBar = new ProgressBar();
            lblStatus = new Label();
            SuspendLayout();
            // 
            // btnAddFiles
            // 
            btnAddFiles.Location = new Point(37, 31);
            btnAddFiles.Name = "btnAddFiles";
            btnAddFiles.Size = new Size(130, 43);
            btnAddFiles.TabIndex = 0;
            btnAddFiles.Text = "Add Files... ";
            btnAddFiles.UseVisualStyleBackColor = true;
            btnAddFiles.Click += btnAddFiles_Click;
            // 
            // btnSummarize
            // 
            btnSummarize.Location = new Point(37, 307);
            btnSummarize.Name = "btnSummarize";
            btnSummarize.Size = new Size(644, 36);
            btnSummarize.TabIndex = 1;
            btnSummarize.Text = "Summarize and Save as PDF";
            btnSummarize.UseVisualStyleBackColor = true;
            btnSummarize.Click += btnSummarize_Click;
            // 
            // lstFiles
            // 
            lstFiles.FormattingEnabled = true;
            lstFiles.ItemHeight = 15;
            lstFiles.Location = new Point(37, 102);
            lstFiles.Name = "lstFiles";
            lstFiles.Size = new Size(644, 199);
            lstFiles.TabIndex = 2;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(37, 379);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(644, 23);
            progressBar.TabIndex = 3;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(37, 356);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(45, 15);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "Status: ";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(722, 450);
            Controls.Add(lblStatus);
            Controls.Add(progressBar);
            Controls.Add(lstFiles);
            Controls.Add(btnSummarize);
            Controls.Add(btnAddFiles);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MainForm";
            Text = "Notoid v1.0";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAddFiles;
        private Button btnSummarize;
        private ListBox lstFiles;
        private ProgressBar progressBar;
        private Label lblStatus;
    }
}
