using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace LuceneAdvancedSearchApplication
{
    public partial class AdvancedSearchGUI : Form
    {
        SearchGUIForm originalForm;
        public AdvancedSearchGUI(SearchGUIForm incomingForm)
        {
            originalForm = incomingForm;
            originalForm.SearchBtn2.Enabled = false;
            InitializeComponent();
        }

        private void advGotoSerchbut_Click(object sender, EventArgs e)
        {
            string gateway = comboBoxCondition.Text;
            string title = titleBox.Text;
            string author = authorBox.Text;
            string textToSearch = "";


            if (String.IsNullOrEmpty(title) && String.IsNullOrEmpty(author))
            {
                MessageBox.Show("Please Enter a value in some field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (author.Length > 0 && title.Length > 0)
                {
                    textToSearch = "Title:" + title + " " + "Author:" + author;                   
                }
                else
                {
                    if (author.Length > 0)
                    {
                        textToSearch = "Author:" + author ;
                    }
                    if (title.Length > 0)
                    {
                        textToSearch = "Title:" + title;
                    }
                }
                
            }
            originalForm.advtext = textToSearch;
            originalForm.cond_operator = gateway;
            originalForm.populate();
            //this.Close();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void AdvancedSearchGUI_Load(object sender, EventArgs e)
        {
        }

        private void AdvancedSearchGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            originalForm.SearchBtn2.Enabled = true;
            originalForm.advancedCheck.Checked = false;
        }
    }
}
