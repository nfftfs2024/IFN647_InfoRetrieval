﻿namespace LuceneAdvancedSearchApplication
{
    partial class GUIForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.NextButton = new System.Windows.Forms.Button();
            this.TextEnter = new System.Windows.Forms.TextBox();
            this.NextLabel = new System.Windows.Forms.Label();
            this.TextButton = new System.Windows.Forms.Button();
            this.mySaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.myFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            //this.myNeedsDialog= new System.Windows.Forms.FileDialog();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.BrowseLabel = new System.Windows.Forms.Label();
            this.SaveLabel = new System.Windows.Forms.Label();
            this.TextShowChange = new System.Windows.Forms.Label();
            this.myOpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Confirm_button = new System.Windows.Forms.Button();
            this.Needs_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(335, 26);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(87, 20);
            this.NextButton.TabIndex = 0;
            this.NextButton.Text = "Get Next Line";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // TextEnter
            // 
            this.TextEnter.Location = new System.Drawing.Point(47, 74);
            this.TextEnter.Name = "TextEnter";
            this.TextEnter.Size = new System.Drawing.Size(276, 20);
            this.TextEnter.TabIndex = 1;
            this.TextEnter.TextChanged += new System.EventHandler(this.TextEnter_TextChanged);
            // 
            // NextLabel
            // 
            this.NextLabel.AutoSize = true;
            this.NextLabel.Location = new System.Drawing.Point(44, 30);
            this.NextLabel.Name = "NextLabel";
            this.NextLabel.Size = new System.Drawing.Size(39, 13);
            this.NextLabel.TabIndex = 4;
            this.NextLabel.Text = "Label1";
            // 
            // TextButton
            // 
            this.TextButton.Location = new System.Drawing.Point(350, 72);
            this.TextButton.Name = "TextButton";
            this.TextButton.Size = new System.Drawing.Size(72, 22);
            this.TextButton.TabIndex = 5;
            this.TextButton.Text = "Enter";
            this.TextButton.UseVisualStyleBackColor = true;
            this.TextButton.Click += new System.EventHandler(this.TextButton_Click);
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(47, 125);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(77, 24);
            this.BrowseButton.TabIndex = 8;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(47, 155);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 9;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // BrowseLabel
            // 
            this.BrowseLabel.AutoSize = true;
            this.BrowseLabel.Location = new System.Drawing.Point(148, 131);
            this.BrowseLabel.Name = "BrowseLabel";
            this.BrowseLabel.Size = new System.Drawing.Size(87, 13);
            this.BrowseLabel.TabIndex = 10;
            this.BrowseLabel.Text = "Browse Directory";
            // 
            // SaveLabel
            // 
            this.SaveLabel.AutoSize = true;
            this.SaveLabel.Location = new System.Drawing.Point(148, 160);
            this.SaveLabel.Name = "SaveLabel";
            this.SaveLabel.Size = new System.Drawing.Size(77, 13);
            this.SaveLabel.TabIndex = 11;
            this.SaveLabel.Text = "Save Directory";
            // 
            // TextShowChange
            // 
            this.TextShowChange.AutoSize = true;
            this.TextShowChange.Location = new System.Drawing.Point(44, 109);
            this.TextShowChange.Name = "TextShowChange";
            this.TextShowChange.Size = new System.Drawing.Size(108, 13);
            this.TextShowChange.TabIndex = 12;
            this.TextShowChange.Text = "Show text on change";
            // 
            // myOpenFileDialog1
            // 
            this.myOpenFileDialog1.FileName = "myOpenFileDialog1";
            this.myOpenFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.myOpenFileDialog1_FileOk);
            // 
            // Confirm_button
            // 
            this.Confirm_button.Location = new System.Drawing.Point(47, 213);
            this.Confirm_button.Name = "Confirm_button";
            this.Confirm_button.Size = new System.Drawing.Size(75, 23);
            this.Confirm_button.TabIndex = 13;
            this.Confirm_button.Text = "OK";
            this.Confirm_button.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.Confirm_button.UseVisualStyleBackColor = true;
            // 
            // Needs_button
            // 
            this.Needs_button.Location = new System.Drawing.Point(49, 184);
            this.Needs_button.Name = "Needs_button";
            this.Needs_button.Size = new System.Drawing.Size(75, 23);
            this.Needs_button.TabIndex = 14;
            this.Needs_button.Text = "Needs";
            this.Needs_button.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.Needs_button.UseVisualStyleBackColor = true;
            // 
            // GUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 286);
            this.Controls.Add(this.Needs_button);
            this.Controls.Add(this.Confirm_button);
            this.Controls.Add(this.TextShowChange);
            this.Controls.Add(this.SaveLabel);
            this.Controls.Add(this.BrowseLabel);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.TextButton);
            this.Controls.Add(this.NextLabel);
            this.Controls.Add(this.TextEnter);
            this.Controls.Add(this.NextButton);
            this.Name = "GUIForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.TextBox TextEnter;
        private System.Windows.Forms.Label NextLabel;
        private System.Windows.Forms.Button TextButton;
        private System.Windows.Forms.SaveFileDialog mySaveFileDialog;
        private System.Windows.Forms.FolderBrowserDialog myFolderBrowserDialog;
        private System.Windows.Forms.FileDialog myNeedsDialog;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label BrowseLabel;
        private System.Windows.Forms.Label SaveLabel;

        private System.Windows.Forms.Label[] LabelArray;
        private System.Windows.Forms.Label TextShowChange;
        private System.Windows.Forms.OpenFileDialog myOpenFileDialog1;
        private System.Windows.Forms.Button Confirm_button;
        private System.Windows.Forms.Button Needs_button;
    }
}

