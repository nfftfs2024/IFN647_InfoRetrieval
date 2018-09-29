namespace LuceneAdvancedSearchApplication
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
            this.TextEnter = new System.Windows.Forms.TextBox();
            this.TextButton = new System.Windows.Forms.Button();
            this.SourceDirBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.myNeedsDialog = new System.Windows.Forms.OpenFileDialog();
            this.SetSourceDireBtn = new System.Windows.Forms.Button();
            this.SetIndexDirBut = new System.Windows.Forms.Button();
            this.SourceLabel = new System.Windows.Forms.Label();
            this.IndexLabel = new System.Windows.Forms.Label();
            this.Confirm_button = new System.Windows.Forms.Button();
            this.NeedsButton = new System.Windows.Forms.Button();
            this.IndexDirBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.NeedsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextEnter
            // 
            this.TextEnter.Location = new System.Drawing.Point(49, 44);
            this.TextEnter.Name = "TextEnter";
            this.TextEnter.Size = new System.Drawing.Size(276, 20);
            this.TextEnter.TabIndex = 1;
            this.TextEnter.TextChanged += new System.EventHandler(this.TextEnter_TextChanged);
            // 
            // TextButton
            // 
            this.TextButton.Location = new System.Drawing.Point(353, 44);
            this.TextButton.Name = "TextButton";
            this.TextButton.Size = new System.Drawing.Size(72, 22);
            this.TextButton.TabIndex = 5;
            this.TextButton.Text = "Enter";
            this.TextButton.UseVisualStyleBackColor = true;
            this.TextButton.Click += new System.EventHandler(this.TextButton_Click);
            // 
            // SetSourceDireBtn
            // 
            this.SetSourceDireBtn.Location = new System.Drawing.Point(49, 92);
            this.SetSourceDireBtn.Name = "SetSourceDireBtn";
            this.SetSourceDireBtn.Size = new System.Drawing.Size(123, 24);
            this.SetSourceDireBtn.TabIndex = 8;
            this.SetSourceDireBtn.Text = "Set Source Directory";
            this.SetSourceDireBtn.UseVisualStyleBackColor = true;
            this.SetSourceDireBtn.Click += new System.EventHandler(this.SetSourceDirBtn_Click);
            // 
            // SetIndexDirBut
            // 
            this.SetIndexDirBut.Location = new System.Drawing.Point(49, 122);
            this.SetIndexDirBut.Name = "SetIndexDirBut";
            this.SetIndexDirBut.Size = new System.Drawing.Size(123, 23);
            this.SetIndexDirBut.TabIndex = 9;
            this.SetIndexDirBut.Text = "Set Index Directory";
            this.SetIndexDirBut.UseVisualStyleBackColor = true;
            this.SetIndexDirBut.Click += new System.EventHandler(this.SetIndexDirBtn_Click);
            // 
            // SourceLabel
            // 
            this.SourceLabel.AutoSize = true;
            this.SourceLabel.Location = new System.Drawing.Point(178, 98);
            this.SourceLabel.Name = "SourceLabel";
            this.SourceLabel.Size = new System.Drawing.Size(86, 13);
            this.SourceLabel.TabIndex = 10;
            this.SourceLabel.Text = "Source Directory";
            // 
            // IndexLabel
            // 
            this.IndexLabel.AutoSize = true;
            this.IndexLabel.Location = new System.Drawing.Point(179, 127);
            this.IndexLabel.Name = "IndexLabel";
            this.IndexLabel.Size = new System.Drawing.Size(78, 13);
            this.IndexLabel.TabIndex = 11;
            this.IndexLabel.Text = "Index Directory";
            // 
            // Confirm_button
            // 
            this.Confirm_button.Location = new System.Drawing.Point(49, 180);
            this.Confirm_button.Name = "Confirm_button";
            this.Confirm_button.Size = new System.Drawing.Size(75, 23);
            this.Confirm_button.TabIndex = 13;
            this.Confirm_button.Text = "OK";
            this.Confirm_button.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.Confirm_button.UseVisualStyleBackColor = true;
            this.Confirm_button.Click += new System.EventHandler(this.Confirm_button_Click);
            // 
            // NeedsButton
            // 
            this.NeedsButton.Location = new System.Drawing.Point(51, 151);
            this.NeedsButton.Name = "NeedsButton";
            this.NeedsButton.Size = new System.Drawing.Size(121, 23);
            this.NeedsButton.TabIndex = 14;
            this.NeedsButton.Text = "Select Cran needs";
            this.NeedsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.NeedsButton.UseVisualStyleBackColor = true;
            this.NeedsButton.Click += new System.EventHandler(this.NeedsButton_Click);
            // 
            // NeedsLabel
            // 
            this.NeedsLabel.AutoSize = true;
            this.NeedsLabel.Location = new System.Drawing.Point(179, 155);
            this.NeedsLabel.Name = "NeedsLabel";
            this.NeedsLabel.Size = new System.Drawing.Size(57, 13);
            this.NeedsLabel.TabIndex = 15;
            this.NeedsLabel.Text = "Needs File";
            // 
            // GUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 286);
            this.Controls.Add(this.NeedsLabel);
            this.Controls.Add(this.NeedsButton);
            this.Controls.Add(this.Confirm_button);
            this.Controls.Add(this.IndexLabel);
            this.Controls.Add(this.SourceLabel);
            this.Controls.Add(this.SetIndexDirBut);
            this.Controls.Add(this.SetSourceDireBtn);
            this.Controls.Add(this.TextButton);
            this.Controls.Add(this.TextEnter);
            this.Name = "GUIForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TextEnter;
        private System.Windows.Forms.Button TextButton;
        private System.Windows.Forms.FolderBrowserDialog SourceDirBrowserDialog;
        private System.Windows.Forms.OpenFileDialog myNeedsDialog;
        private System.Windows.Forms.Button SetSourceDireBtn;
        private System.Windows.Forms.Button SetIndexDirBut;
        private System.Windows.Forms.Label SourceLabel;
        private System.Windows.Forms.Label IndexLabel;

        private System.Windows.Forms.Label[] LabelArray;
        private System.Windows.Forms.Button Confirm_button;
        private System.Windows.Forms.Button NeedsButton;
        private System.Windows.Forms.FolderBrowserDialog IndexDirBrowserDialog;
        private System.Windows.Forms.Label NeedsLabel;
    }
}

