namespace LuceneAdvancedSearchApplication
{
    partial class AdvancedSearchGUI
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
            this.titleBox = new System.Windows.Forms.TextBox();
            this.authorBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.advGotoSerchbut = new System.Windows.Forms.Button();
            this.comboBoxCondition = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // titleBox
            // 
            this.titleBox.Location = new System.Drawing.Point(136, 23);
            this.titleBox.Name = "titleBox";
            this.titleBox.Size = new System.Drawing.Size(410, 26);
            this.titleBox.TabIndex = 0;
            // 
            // authorBox
            // 
            this.authorBox.Location = new System.Drawing.Point(136, 64);
            this.authorBox.Name = "authorBox";
            this.authorBox.Size = new System.Drawing.Size(410, 26);
            this.authorBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(68, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "TITLE:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(42, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "AUTHOR:";
            // 
            // advGotoSerchbut
            // 
            this.advGotoSerchbut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.advGotoSerchbut.Location = new System.Drawing.Point(337, 110);
            this.advGotoSerchbut.Name = "advGotoSerchbut";
            this.advGotoSerchbut.Size = new System.Drawing.Size(159, 62);
            this.advGotoSerchbut.TabIndex = 6;
            this.advGotoSerchbut.Text = "GO TO SEARCH";
            this.advGotoSerchbut.UseVisualStyleBackColor = true;
            this.advGotoSerchbut.Click += new System.EventHandler(this.advGotoSerchbut_Click);
            // 
            // comboBoxCondition
            // 
            this.comboBoxCondition.FormattingEnabled = true;
            this.comboBoxCondition.Items.AddRange(new object[] {
            "AND",
            "OR"});
            this.comboBoxCondition.Location = new System.Drawing.Point(136, 105);
            this.comboBoxCondition.Name = "comboBoxCondition";
            this.comboBoxCondition.Size = new System.Drawing.Size(121, 28);
            this.comboBoxCondition.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "CONDITION:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // AdvancedSearchGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 186);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxCondition);
            this.Controls.Add(this.advGotoSerchbut);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.authorBox);
            this.Controls.Add(this.titleBox);
            this.Name = "AdvancedSearchGUI";
            this.Text = "AdvancedSearchGUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox titleBox;
        private System.Windows.Forms.TextBox authorBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button advGotoSerchbut;
        private System.Windows.Forms.ComboBox comboBoxCondition;
        private System.Windows.Forms.Label label3;
    }
}