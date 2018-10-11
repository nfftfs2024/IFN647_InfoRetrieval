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
            InitializeComponent();
        }

        private void advGotoSerchbut_Click(object sender, EventArgs e)
        {
            string title = titleBox.Text;
            string author = authorBox.Text;
            string textToSearch = "Title= " + title + "\tAuthor= " + author;
            originalForm.advtext = textToSearch;
            originalForm.populate();
            this.Close();

        }
    }
}
