using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LuceneAdvancedSearchApplication
{
    public partial class BuildIndexGUIForm : Form
    {
        //string indexPath = @"C:\Users\n9802614\Desktop\New folder";
        //string sourcePath = @"H:\647\crandocs";

        //string indexPath = @"D:\IR\ifn647-project\LuceneAdvancedSearchApplication\index";
        //string sourcePath = @"D:\IR\ifn647-project\LuceneAdvancedSearchApplication\crandocs";

        string indexPath;
        string sourcePath;
        public BuildIndexGUIForm()
        {
            InitializeComponent();
        }

        private void BuildIndexGUIForm_Load(object sender, EventArgs e)
        {

        }

        private void BuildIndBtn_Click(object sender, EventArgs e)
        {
            if (sourcePath is null)
                MessageBox.Show("You didn't completely select the source directory path", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (indexPath is null)
                MessageBox.Show("You didn't completely select the index directory path", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                Program.BuildIndex_Click(sourcePath, indexPath);   // Build index
                this.Close();
            }
        }

        private void SetSourceDireBtn_Click(object sender, EventArgs e)
        {
            bool chk = true;
            while (chk)
            {
                SourceDirBrowserDialog.ShowDialog();
                sourcePath = SourceDirBrowserDialog.SelectedPath;
                chk = CheckSourceDirectory(sourcePath);
            }
            SourceTxtBox.Text = SourceDirBrowserDialog.SelectedPath;
        }

        private void SetIndexDirBtn_Click(object sender, EventArgs e)
        {
            IndexDirBrowserDialog.ShowDialog();
            IndTxtBox.Text = IndexDirBrowserDialog.SelectedPath;
            indexPath = IndexDirBrowserDialog.SelectedPath;
        }

        public bool CheckSourceDirectory(string sourcePath)
        {
            bool chk;
            DirectoryInfo dir = new DirectoryInfo(sourcePath);
            FileInfo[] TXTFiles = dir.GetFiles("*.txt");
            if (TXTFiles.Length == 0)
            {
                MessageBox.Show("This directory does not have any text files for creating index", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                chk = true;
            }
            else
                chk = false;
            return chk;
        }
    }
}
