namespace LuceneAdvancedSearchApplication
{
    partial class ExpandForm
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
            this.Abstract = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Abstract
            // 
            this.Abstract.Location = new System.Drawing.Point(13, 13);
            this.Abstract.Name = "Abstract";
            this.Abstract.Size = new System.Drawing.Size(469, 353);
            this.Abstract.TabIndex = 0;
            this.Abstract.Text = "label1";
            // 
            // ExpandForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 375);
            this.Controls.Add(this.Abstract);
            this.Name = "ExpandForm";
            this.Text = "ExpandForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Abstract;
    }
}