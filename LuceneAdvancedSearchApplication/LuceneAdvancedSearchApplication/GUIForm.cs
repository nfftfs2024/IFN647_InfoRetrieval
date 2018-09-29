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
    
    public partial class GUIForm : Form
    {
        public static String sourcePath { get; set; }
        public static String indexPath { get; set; }
        public static String needsPath { get; set; }
        public static String searchWords { get; set; }
        Poem p;
        public GUIForm()
        {
            InitializeComponent();
            p = new Poem();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        

        
        private void TextButton_Click(object sender, EventArgs e)
        {
            

        }

        private void TextEnter_TextChanged(object sender, EventArgs e)
        {
            //TextShowChange.Text = TextEnter.Text;
            searchWords = TextEnter.Text;
        }


        private void SetSourceDirBtn_Click(object sender, EventArgs e)
        {
            SourceDirBrowserDialog.ShowDialog();
            SourceLabel.Text = SourceDirBrowserDialog.SelectedPath;
            sourcePath= SourceDirBrowserDialog.SelectedPath;
            
        }

        private void SetIndexDirBtn_Click(object sender, EventArgs e)
        {
            IndexDirBrowserDialog.ShowDialog();
            IndexLabel.Text = IndexDirBrowserDialog.SelectedPath;
            indexPath = IndexDirBrowserDialog.SelectedPath;
        }
        private void NeedsButton_Click(object sender, EventArgs e)
        {
            myNeedsDialog.ShowDialog();
            NeedsLabel.Text = myNeedsDialog.FileName;
            needsPath = myNeedsDialog.FileName;
        }

        private void Confirm_button_Click(object sender, EventArgs e)
        {
            if ((indexPath is null) || (sourcePath is null) || (needsPath is null))
            {
                MessageBox.Show("You didn't completely select the directory paths or files", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.Close();
            }
        }
    }
}
