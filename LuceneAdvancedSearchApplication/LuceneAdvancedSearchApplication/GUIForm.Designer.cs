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
            this.BuildIndBtn = new System.Windows.Forms.Button();
            this.SourceDirBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.myNeedsDialog = new System.Windows.Forms.OpenFileDialog();
            this.SetSourceDireBtn = new System.Windows.Forms.Button();
            this.SetIndexDirBut = new System.Windows.Forms.Button();
            this.SourceLabel = new System.Windows.Forms.Label();
            this.IndexLabel = new System.Windows.Forms.Label();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.NeedsButton = new System.Windows.Forms.Button();
            this.IndexDirBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.NeedsLabel = new System.Windows.Forms.Label();
            this.SearchOutput = new System.Windows.Forms.TextBox();
            this.TopLabel = new System.Windows.Forms.Label();
            this.PreviousBtn = new System.Windows.Forms.Button();
            this.NextBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextEnter
            // 
            this.TextEnter.Location = new System.Drawing.Point(110, 88);
            this.TextEnter.Name = "TextEnter";
            this.TextEnter.Size = new System.Drawing.Size(276, 20);
            this.TextEnter.TabIndex = 1;
            this.TextEnter.TextChanged += new System.EventHandler(this.TextEnter_TextChanged);
            // 
            // BuildIndBtn
            // 
            this.BuildIndBtn.Location = new System.Drawing.Point(353, 44);
            this.BuildIndBtn.Name = "BuildIndBtn";
            this.BuildIndBtn.Size = new System.Drawing.Size(72, 22);
            this.BuildIndBtn.TabIndex = 5;
            this.BuildIndBtn.Text = "Build Index";
            this.BuildIndBtn.UseVisualStyleBackColor = true;
            this.BuildIndBtn.Click += new System.EventHandler(this.BuildIndBtn_Click);
            // 
            // myNeedsDialog
            // 
            
            // 
            // SetSourceDireBtn
            // 
            this.SetSourceDireBtn.Location = new System.Drawing.Point(49, 12);
            this.SetSourceDireBtn.Name = "SetSourceDireBtn";
            this.SetSourceDireBtn.Size = new System.Drawing.Size(123, 24);
            this.SetSourceDireBtn.TabIndex = 8;
            this.SetSourceDireBtn.Text = "Set Source Directory";
            this.SetSourceDireBtn.UseVisualStyleBackColor = true;
            this.SetSourceDireBtn.Click += new System.EventHandler(this.SetSourceDirBtn_Click);
            // 
            // SetIndexDirBut
            // 
            this.SetIndexDirBut.Location = new System.Drawing.Point(49, 42);
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
            this.SourceLabel.Location = new System.Drawing.Point(178, 18);
            this.SourceLabel.Name = "SourceLabel";
            this.SourceLabel.Size = new System.Drawing.Size(86, 13);
            this.SourceLabel.TabIndex = 10;
            this.SourceLabel.Text = "Source Directory";
            // 
            // IndexLabel
            // 
            this.IndexLabel.AutoSize = true;
            this.IndexLabel.Location = new System.Drawing.Point(179, 47);
            this.IndexLabel.Name = "IndexLabel";
            this.IndexLabel.Size = new System.Drawing.Size(78, 13);
            this.IndexLabel.TabIndex = 11;
            this.IndexLabel.Text = "Index Directory";
            // 
            // SearchBtn
            // 
            this.SearchBtn.Location = new System.Drawing.Point(392, 86);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(84, 22);
            this.SearchBtn.TabIndex = 13;
            this.SearchBtn.Text = "Search";
            this.SearchBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // NeedsButton
            // 
            this.NeedsButton.Location = new System.Drawing.Point(52, 135);
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
            this.NeedsLabel.Location = new System.Drawing.Point(180, 140);
            this.NeedsLabel.Name = "NeedsLabel";
            this.NeedsLabel.Size = new System.Drawing.Size(57, 13);
            this.NeedsLabel.TabIndex = 15;
            this.NeedsLabel.Text = "Needs File";
            // 
            // SearchOutput
            // 
            this.SearchOutput.Location = new System.Drawing.Point(49, 205);
            this.SearchOutput.Multiline = true;
            this.SearchOutput.Name = "SearchOutput";
            this.SearchOutput.ReadOnly = true;
            this.SearchOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SearchOutput.Size = new System.Drawing.Size(535, 343);
            this.SearchOutput.TabIndex = 16;
            // 
            // TopLabel
            // 
            this.TopLabel.AutoSize = true;
            this.TopLabel.Location = new System.Drawing.Point(49, 171);
            this.TopLabel.Name = "TopLabel";
            this.TopLabel.Size = new System.Drawing.Size(0, 13);
            this.TopLabel.TabIndex = 17;
            // 
            // PreviousBtn
            // 
            this.PreviousBtn.Location = new System.Drawing.Point(422, 179);
            this.PreviousBtn.Name = "PreviousBtn";
            this.PreviousBtn.Size = new System.Drawing.Size(75, 23);
            this.PreviousBtn.TabIndex = 18;
            this.PreviousBtn.Text = "Previous 10";
            this.PreviousBtn.UseVisualStyleBackColor = true;
            this.PreviousBtn.Click += new System.EventHandler(this.PreviousBtn_Click);
            // 
            // NextBtn
            // 
            this.NextBtn.Location = new System.Drawing.Point(503, 179);
            this.NextBtn.Name = "NextBtn";
            this.NextBtn.Size = new System.Drawing.Size(75, 23);
            this.NextBtn.TabIndex = 19;
            this.NextBtn.Text = "Next 10";
            this.NextBtn.UseVisualStyleBackColor = true;
            this.NextBtn.Click += new System.EventHandler(this.NextBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Search for:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "OR";
            // 
            // GUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 560);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NextBtn);
            this.Controls.Add(this.PreviousBtn);
            this.Controls.Add(this.TopLabel);
            this.Controls.Add(this.SearchOutput);
            this.Controls.Add(this.NeedsLabel);
            this.Controls.Add(this.NeedsButton);
            this.Controls.Add(this.SearchBtn);
            this.Controls.Add(this.IndexLabel);
            this.Controls.Add(this.SourceLabel);
            this.Controls.Add(this.SetIndexDirBut);
            this.Controls.Add(this.SetSourceDireBtn);
            this.Controls.Add(this.BuildIndBtn);
            this.Controls.Add(this.TextEnter);
            this.Name = "GUIForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TextEnter;
        private System.Windows.Forms.Button BuildIndBtn;
        private System.Windows.Forms.FolderBrowserDialog SourceDirBrowserDialog;
        private System.Windows.Forms.OpenFileDialog myNeedsDialog;
        private System.Windows.Forms.Button SetSourceDireBtn;
        private System.Windows.Forms.Button SetIndexDirBut;
        private System.Windows.Forms.Label SourceLabel;
        private System.Windows.Forms.Label IndexLabel;

        private System.Windows.Forms.Label[] LabelArray;
        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.Button NeedsButton;
        private System.Windows.Forms.FolderBrowserDialog IndexDirBrowserDialog;
        private System.Windows.Forms.Label NeedsLabel;
        private System.Windows.Forms.TextBox SearchOutput;
        private System.Windows.Forms.Label TopLabel;
        private System.Windows.Forms.Button PreviousBtn;
        private System.Windows.Forms.Button NextBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

