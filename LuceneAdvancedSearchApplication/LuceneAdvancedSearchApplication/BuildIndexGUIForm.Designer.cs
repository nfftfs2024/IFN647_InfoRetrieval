namespace LuceneAdvancedSearchApplication
{
    partial class BuildIndexGUIForm
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
            this.BuildIndBtn = new System.Windows.Forms.Button();
            this.IndexLabel = new System.Windows.Forms.Label();
            this.SourceLabel = new System.Windows.Forms.Label();
            this.SetSourceDireBtn = new System.Windows.Forms.Button();
            this.SourceTxtBox = new System.Windows.Forms.TextBox();
            this.IndTxtBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SourceDirBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.IndexDirBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BuildIndBtn
            // 
            this.BuildIndBtn.Location = new System.Drawing.Point(278, 299);
            this.BuildIndBtn.Name = "BuildIndBtn";
            this.BuildIndBtn.Size = new System.Drawing.Size(145, 38);
            this.BuildIndBtn.TabIndex = 6;
            this.BuildIndBtn.Text = "Build Index";
            this.BuildIndBtn.UseVisualStyleBackColor = true;
            this.BuildIndBtn.Click += new System.EventHandler(this.BuildIndBtn_Click);
            // 
            // IndexLabel
            // 
            this.IndexLabel.AutoSize = true;
            this.IndexLabel.Location = new System.Drawing.Point(73, 209);
            this.IndexLabel.MaximumSize = new System.Drawing.Size(400, 20);
            this.IndexLabel.Name = "IndexLabel";
            this.IndexLabel.Size = new System.Drawing.Size(129, 13);
            this.IndexLabel.TabIndex = 15;
            this.IndexLabel.Text = "Select the Index Directory";
            // 
            // SourceLabel
            // 
            this.SourceLabel.AutoSize = true;
            this.SourceLabel.Location = new System.Drawing.Point(73, 132);
            this.SourceLabel.MaximumSize = new System.Drawing.Size(400, 20);
            this.SourceLabel.Name = "SourceLabel";
            this.SourceLabel.Size = new System.Drawing.Size(137, 13);
            this.SourceLabel.TabIndex = 14;
            this.SourceLabel.Text = "Select the Source Directory";
            // 
            // SetSourceDireBtn
            // 
            this.SetSourceDireBtn.Location = new System.Drawing.Point(522, 159);
            this.SetSourceDireBtn.Name = "SetSourceDireBtn";
            this.SetSourceDireBtn.Size = new System.Drawing.Size(117, 24);
            this.SetSourceDireBtn.TabIndex = 12;
            this.SetSourceDireBtn.Text = "Select";
            this.SetSourceDireBtn.UseVisualStyleBackColor = true;
            this.SetSourceDireBtn.Click += new System.EventHandler(this.SetSourceDireBtn_Click);
            // 
            // SourceTxtBox
            // 
            this.SourceTxtBox.Location = new System.Drawing.Point(76, 161);
            this.SourceTxtBox.Name = "SourceTxtBox";
            this.SourceTxtBox.Size = new System.Drawing.Size(427, 20);
            this.SourceTxtBox.TabIndex = 17;
            // 
            // IndTxtBox
            // 
            this.IndTxtBox.Location = new System.Drawing.Point(76, 239);
            this.IndTxtBox.Name = "IndTxtBox";
            this.IndTxtBox.Size = new System.Drawing.Size(427, 20);
            this.IndTxtBox.TabIndex = 18;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(522, 235);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 24);
            this.button1.TabIndex = 19;
            this.button1.Text = "Select";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SetIndexDirBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Bright", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(149, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(415, 84);
            this.label1.TabIndex = 20;
            this.label1.Text = "Kingsland University \r\nof Technology";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // BuildIndexGUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 392);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.IndTxtBox);
            this.Controls.Add(this.SourceTxtBox);
            this.Controls.Add(this.IndexLabel);
            this.Controls.Add(this.SourceLabel);
            this.Controls.Add(this.SetSourceDireBtn);
            this.Controls.Add(this.BuildIndBtn);
            this.Name = "BuildIndexGUIForm";
            this.Text = "BuildIndexGUI";
            this.Load += new System.EventHandler(this.BuildIndexGUIForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BuildIndBtn;
        private System.Windows.Forms.Label IndexLabel;
        private System.Windows.Forms.Label SourceLabel;
        private System.Windows.Forms.Button SetSourceDireBtn;
        private System.Windows.Forms.TextBox SourceTxtBox;
        private System.Windows.Forms.TextBox IndTxtBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog SourceDirBrowserDialog;
        private System.Windows.Forms.FolderBrowserDialog IndexDirBrowserDialog;
        private System.Windows.Forms.Label label1;
    }
}