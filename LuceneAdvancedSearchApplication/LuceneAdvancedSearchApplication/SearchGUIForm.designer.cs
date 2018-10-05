namespace LuceneAdvancedSearchApplication
{
    partial class SearchGUIForm
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
            this.myNeedsDialog = new System.Windows.Forms.OpenFileDialog();
            this.SearchBtn1 = new System.Windows.Forms.Button();
            this.NeedsButton = new System.Windows.Forms.Button();
            this.NeedsLabel = new System.Windows.Forms.Label();
            this.PreviousBtn = new System.Windows.Forms.Button();
            this.NextBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SearchBtn2 = new System.Windows.Forms.Button();
            this.SaveResult = new System.Windows.Forms.Button();
            this.SaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.Pagelabel = new System.Windows.Forms.Label();
            this.resultListView = new System.Windows.Forms.ListView();
            this.FinalQTxtbox = new System.Windows.Forms.TextBox();
            this.FinalQLab = new System.Windows.Forms.Label();
            this.resultLab = new System.Windows.Forms.Label();
            this.resultNumLab = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextEnter
            // 
            this.TextEnter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextEnter.Location = new System.Drawing.Point(192, 86);
            this.TextEnter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TextEnter.Name = "TextEnter";
            this.TextEnter.Size = new System.Drawing.Size(704, 30);
            this.TextEnter.TabIndex = 1;
            this.TextEnter.TextChanged += new System.EventHandler(this.TextEnter_TextChanged);
            // 
            // SearchBtn1
            // 
            this.SearchBtn1.Enabled = false;
            this.SearchBtn1.Location = new System.Drawing.Point(940, 189);
            this.SearchBtn1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SearchBtn1.Name = "SearchBtn1";
            this.SearchBtn1.Size = new System.Drawing.Size(126, 34);
            this.SearchBtn1.TabIndex = 13;
            this.SearchBtn1.Text = "Search";
            this.SearchBtn1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.SearchBtn1.UseVisualStyleBackColor = true;
            this.SearchBtn1.Click += new System.EventHandler(this.SearchBtn1_Click);
            // 
            // NeedsButton
            // 
            this.NeedsButton.Location = new System.Drawing.Point(64, 189);
            this.NeedsButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NeedsButton.Name = "NeedsButton";
            this.NeedsButton.Size = new System.Drawing.Size(182, 35);
            this.NeedsButton.TabIndex = 14;
            this.NeedsButton.Text = "Select Cran needs";
            this.NeedsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.NeedsButton.UseVisualStyleBackColor = true;
            this.NeedsButton.Click += new System.EventHandler(this.NeedsButton_Click);
            // 
            // NeedsLabel
            // 
            this.NeedsLabel.AutoSize = true;
            this.NeedsLabel.Location = new System.Drawing.Point(64, 229);
            this.NeedsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.NeedsLabel.Name = "NeedsLabel";
            this.NeedsLabel.Size = new System.Drawing.Size(84, 20);
            this.NeedsLabel.TabIndex = 15;
            this.NeedsLabel.Text = "Needs File";
            // 
            // PreviousBtn
            // 
            this.PreviousBtn.Enabled = false;
            this.PreviousBtn.Location = new System.Drawing.Point(842, 765);
            this.PreviousBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PreviousBtn.Name = "PreviousBtn";
            this.PreviousBtn.Size = new System.Drawing.Size(112, 35);
            this.PreviousBtn.TabIndex = 18;
            this.PreviousBtn.Text = "Previous 10";
            this.PreviousBtn.UseVisualStyleBackColor = true;
            this.PreviousBtn.Click += new System.EventHandler(this.PreviousBtn_Click);
            // 
            // NextBtn
            // 
            this.NextBtn.Enabled = false;
            this.NextBtn.Location = new System.Drawing.Point(1077, 765);
            this.NextBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NextBtn.Name = "NextBtn";
            this.NextBtn.Size = new System.Drawing.Size(112, 35);
            this.NextBtn.TabIndex = 19;
            this.NextBtn.Text = "Next 10";
            this.NextBtn.UseVisualStyleBackColor = true;
            this.NextBtn.Click += new System.EventHandler(this.NextBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(58, 91);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 25);
            this.label1.TabIndex = 20;
            this.label1.Text = "Search for:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(87, 137);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 25);
            this.label2.TabIndex = 21;
            this.label2.Text = "OR";
            // 
            // SearchBtn2
            // 
            this.SearchBtn2.Location = new System.Drawing.Point(940, 86);
            this.SearchBtn2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SearchBtn2.Name = "SearchBtn2";
            this.SearchBtn2.Size = new System.Drawing.Size(126, 34);
            this.SearchBtn2.TabIndex = 22;
            this.SearchBtn2.Text = "Search";
            this.SearchBtn2.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.SearchBtn2.UseVisualStyleBackColor = true;
            this.SearchBtn2.Click += new System.EventHandler(this.SearchBtn2_Click);
            // 
            // SaveResult
            // 
            this.SaveResult.Enabled = false;
            this.SaveResult.Location = new System.Drawing.Point(1686, 271);
            this.SaveResult.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SaveResult.Name = "SaveResult";
            this.SaveResult.Size = new System.Drawing.Size(264, 62);
            this.SaveResult.TabIndex = 24;
            this.SaveResult.Text = "Save";
            this.SaveResult.UseVisualStyleBackColor = true;
            this.SaveResult.Click += new System.EventHandler(this.SaveResult_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "001",
            "002",
            "023",
            "157",
            "219"});
            this.comboBox1.Location = new System.Drawing.Point(480, 191);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 28);
            this.comboBox1.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(291, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 25);
            this.label3.TabIndex = 26;
            this.label3.Text = "SelectCranNeed:";
            // 
            // Pagelabel
            // 
            this.Pagelabel.AutoSize = true;
            this.Pagelabel.Location = new System.Drawing.Point(964, 772);
            this.Pagelabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Pagelabel.MaximumSize = new System.Drawing.Size(195, 31);
            this.Pagelabel.Name = "Pagelabel";
            this.Pagelabel.Size = new System.Drawing.Size(0, 20);
            this.Pagelabel.TabIndex = 29;
            // 
            // resultListView
            // 
            this.resultListView.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.resultListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultListView.FullRowSelect = true;
            this.resultListView.GridLines = true;
            this.resultListView.Location = new System.Drawing.Point(39, 360);
            this.resultListView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.resultListView.Name = "resultListView";
            this.resultListView.Size = new System.Drawing.Size(1909, 393);
            this.resultListView.TabIndex = 10;
            this.resultListView.UseCompatibleStateImageBehavior = false;
            this.resultListView.View = System.Windows.Forms.View.Details;
            this.resultListView.SelectedIndexChanged += new System.EventHandler(this.resultListDictView_SelectedIndexChanged);
            // 
            // FinalQTxtbox
            // 
            this.FinalQTxtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FinalQTxtbox.Location = new System.Drawing.Point(718, 255);
            this.FinalQTxtbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FinalQTxtbox.Multiline = true;
            this.FinalQTxtbox.Name = "FinalQTxtbox";
            this.FinalQTxtbox.Size = new System.Drawing.Size(540, 95);
            this.FinalQTxtbox.TabIndex = 30;
            // 
            // FinalQLab
            // 
            this.FinalQLab.AutoSize = true;
            this.FinalQLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FinalQLab.Location = new System.Drawing.Point(572, 286);
            this.FinalQLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.FinalQLab.Name = "FinalQLab";
            this.FinalQLab.Size = new System.Drawing.Size(139, 25);
            this.FinalQLab.TabIndex = 31;
            this.FinalQLab.Text = "Final query - ";
            // 
            // resultLab
            // 
            this.resultLab.AutoSize = true;
            this.resultLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultLab.Location = new System.Drawing.Point(36, 328);
            this.resultLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.resultLab.Name = "resultLab";
            this.resultLab.Size = new System.Drawing.Size(0, 25);
            this.resultLab.TabIndex = 32;
            // 
            // resultNumLab
            // 
            this.resultNumLab.AutoSize = true;
            this.resultNumLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultNumLab.Location = new System.Drawing.Point(212, 328);
            this.resultNumLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.resultNumLab.Name = "resultNumLab";
            this.resultNumLab.Size = new System.Drawing.Size(0, 25);
            this.resultNumLab.TabIndex = 33;
            // 
            // SearchGUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 845);
            this.Controls.Add(this.resultNumLab);
            this.Controls.Add(this.resultLab);
            this.Controls.Add(this.FinalQLab);
            this.Controls.Add(this.FinalQTxtbox);
            this.Controls.Add(this.resultListView);
            this.Controls.Add(this.Pagelabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.SaveResult);
            this.Controls.Add(this.SearchBtn2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NextBtn);
            this.Controls.Add(this.PreviousBtn);
            this.Controls.Add(this.NeedsLabel);
            this.Controls.Add(this.NeedsButton);
            this.Controls.Add(this.SearchBtn1);
            this.Controls.Add(this.TextEnter);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SearchGUIForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.SearchGUIForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TextEnter;
        private System.Windows.Forms.OpenFileDialog myNeedsDialog;

        private System.Windows.Forms.Label[] LabelArray;
        private System.Windows.Forms.Button SearchBtn1;
        private System.Windows.Forms.Button NeedsButton;
        private System.Windows.Forms.Label NeedsLabel;
        private System.Windows.Forms.Button PreviousBtn;
        private System.Windows.Forms.Button NextBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SearchBtn2;
        private System.Windows.Forms.Button SaveResult;
        private System.Windows.Forms.SaveFileDialog SaveDialog;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Label Pagelabel;
        private System.Windows.Forms.ListView resultListView;
        private System.Windows.Forms.TextBox FinalQTxtbox;
        private System.Windows.Forms.Label FinalQLab;
        private System.Windows.Forms.Label resultLab;
        private System.Windows.Forms.Label resultNumLab;
    }
}

