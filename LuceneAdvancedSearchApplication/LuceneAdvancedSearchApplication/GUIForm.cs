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

   
 

        private void OpenButton_Click(object sender, EventArgs e)
        {
            myFolderBrowserDialog.ShowDialog();
            BrowseLabel.Text = myFolderBrowserDialog.SelectedPath;
            sourcePath= myFolderBrowserDialog.SelectedPath;
            
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            mySaveFileDialog.ShowDialog();
            SaveLabel.Text = mySaveFileDialog.FileName;
            indexPath = mySaveFileDialog.FileName;
        }
        private void NeedsButton_Click(object sender, EventArgs e)
        {
            myNeedsDialog.ShowDialog();
         
            needsPath = myNeedsDialog.FileName;

        }
 

        private void myOpenFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void Confirm_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
