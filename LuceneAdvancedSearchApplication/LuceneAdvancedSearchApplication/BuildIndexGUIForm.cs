using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuceneAdvancedSearchApplication
{
    public partial class BuildIndexGUIForm : Form
    {
        string indexPath = @"C:\Users\n9802614\Desktop\New folder";
        string sourcePath = @"H:\647\crandocs";

        public BuildIndexGUIForm()
        {
            InitializeComponent();
        }

        private void BuildIndexGUIForm_Load(object sender, EventArgs e)
        {

        }

        private void BuildIndBtn_Click(object sender, EventArgs e)
        {
            Program.BuildIndex_Click(sourcePath, indexPath);   // Build index
            this.Close();
        }

        private void SetSourceDireBtn_Click(object sender, EventArgs e)
        {
            SourceDirBrowserDialog.ShowDialog();
            SourceTxtBox.Text = SourceDirBrowserDialog.SelectedPath;
            sourcePath = SourceDirBrowserDialog.SelectedPath;
        }

        private void SetIndexDirBtn_Click(object sender, EventArgs e)
        {
            IndexDirBrowserDialog.ShowDialog();
            IndTxtBox.Text = IndexDirBrowserDialog.SelectedPath;
            indexPath = IndexDirBrowserDialog.SelectedPath;
        }
    }
}
