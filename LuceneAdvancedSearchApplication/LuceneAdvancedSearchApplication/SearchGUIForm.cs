using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace LuceneAdvancedSearchApplication
{

    public partial class SearchGUIForm : Form
    {
        public static String sourcePath { get; set; }
        public static String indexPath { get; set; }
        public static String needsPath { get; set; }
        public static String searchWords { get; set; }
        public static String savePath { get; set; }

        public static List<Dictionary<string, string>> resultList { get; set; }
        public static Int32 limit { get; set; }
        public static Boolean first { get; set; }
        public static int pageNub { get; set; }
        public static int queryCount { get; set; }

        Dictionary<string, string> cranNeeds;

        LuceneSearcheEngine myLuceneApp;    // Create a search engine object

        public SearchGUIForm()
        {
            InitializeComponent();

            // Create the column headers for the list view
            resultListView.Columns.Add("DocID", 50);
            resultListView.Columns.Add("Title", 350);
            resultListView.Columns.Add("Author", 150);
            resultListView.Columns.Add("Bibliography", 150);
            resultListView.Columns.Add("TEXT", 400);
            queryCount = 0;
        }

        private void SearchGUIForm_Load(object sender, EventArgs e)
        {

        }

        private void TextEnter_TextChanged(object sender, EventArgs e)
        {
            searchWords = TextEnter.Text;
        }


        private void NeedsButton_Click(object sender, EventArgs e)      // Select file path after click on Select Cran Needs
        {
            myNeedsDialog.ShowDialog();
            NeedsLabel.Text = myNeedsDialog.FileName;
            needsPath = myNeedsDialog.FileName;
            cranNeeds = Program.ReadCranNeeds(needsPath);   // Put the cran_information_need into a dictionary
            SearchBtn1.Enabled = true;      // Enable search button 1
        }

        private void SaveResult_Click(object sender, EventArgs e)   // Select directory path after click on Set Index Directory
        {
            SaveDialog.ShowDialog();
            savePath = SaveDialog.FileName;
            StreamWriter writer = new StreamWriter(savePath,append:true);
            Program.SaveClick(resultList, writer,queryCount);


        }

        private void SearchBtn1_Click(object sender, EventArgs e)   // Whe clicking on search button for Cran Needs
        {
            ExpandAbsBtn.Text = "Show Abstracts";       // Retore expand abstract button
            first = true;   // Set only displaying first line
            limit = 0;      // Set starting result index

            DateTime start = System.DateTime.Now;   // Searching time starts
            resultList = Program.Search_Click(cranNeeds[comboBox1.SelectedItem.ToString()]);       // Search Cran needs texts
            DateTime end = System.DateTime.Now;   // Searching time starts
            MessageBox.Show("The time for searching text was " + (end - start), "Reporting Searching Time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Program.ViewData(limit, resultList);                    // Collate search result into displaying formats

            Program.Create_BaseLine_Results(cranNeeds, myLuceneApp);

            TopLabel.Text = "Top 1-10 results";     // Display top description
            //SearchOutput.Text = outp;               // Display top 10 results
            NextBtn.Enabled = true;                 // Enable next button
            PreviousBtn.Enabled = false;            // Disable previous button
            ExpandAbsBtn.Enabled = true;            // Enable expand abstract button 
            SaveResult.Enabled = true;              // Enable save result button

            NeedQuery.Text = cranNeeds[comboBox1.SelectedItem.ToString()];     //Print Query 
            pageNub = 1;
            Pagelabel.Text = "Page " + pageNub;
        }

        private void SearchBtn2_Click(object sender, EventArgs e)       // When clicking on search button for user free-typing
        {
            resultListView.Items.Clear();
            resultListView.Controls.Clear();

            ExpandAbsBtn.Text = "Show Abstracts";       // Retore expand abstract button
            first = true;   // Set only displaying first line
            limit = 0;      // Set starting result index

            if (TextEnter.Text == "")       // Check if the textbox is empty
            {
                MessageBox.Show("Enter something!!", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                List<Dictionary<string, string>> tempList = new List<Dictionary<string, string>>();            // Create temporary list
                DateTime start = System.DateTime.Now;   // Searching time starts
                tempList = Program.Search_Click(TextEnter.Text);     // Search user input texts
                DateTime end = System.DateTime.Now;   // Searching time starts


                if (tempList.Count != 0)
                {
                    MessageBox.Show("The time for searching text was " + (end - start), "Reporting Searching Time", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    resultList = tempList;  // Assign temporary list to global variable as current 10 results
                    queryCount++;
                    //string outp = Program.ViewData(limit, resultList, first);                // Collate search result into displaying formats

                    //for (int i = 0; i < 10; i++)
                    //{
                    //    ListViewItem resultView = new ListViewItem(new[] { resultList[i]["id"], resultList[i]["title"], resultList[i]["author"], resultList[i]["biblio"], resultList[i]["abstract"] });
                    //    resultListView.Items.Add(resultView);
                    //}

                    for (int i = limit; i < limit + 10; i++)     // Concatenate the top 10 result strings
                    {
                        ListViewItem resultView = new ListViewItem(new[] { resultList[i]["id"], resultList[i]["title"], resultList[i]["author"], resultList[i]["biblio"], resultList[i]["abstract"] });
                        resultListView.Items.Add(resultView);
                    }

                    TopLabel.Text = "Top 1-10 results";     // Display top description
                    //SearchOutput.Text = outp;               // Display top 10 results
                    NextBtn.Enabled = true;                 // Enable next button
                    PreviousBtn.Enabled = false;            // Disable previous button
                    ExpandAbsBtn.Enabled = true;            // Enable expand abstract button 
                    SaveResult.Enabled = true;              // Enable save result button
                }
                else
                {
                    MessageBox.Show("No results were found, please try something else!", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);  // Display error
                }

            }
            pageNub = 1;
            Pagelabel.Text = "Page " + pageNub;

        }

        private void NextBtn_Click(object sender, EventArgs e)  // When clicking on Next 10 button
        {
            resultListView.Items.Clear();
            resultListView.Controls.Clear();
            limit += 10;        // Get new rank starting counter
                                //Program.ViewData(limit, resultList);                    // Collate search result into displaying formats

            for (int i = limit; i < limit + 10; i++)     // Concatenate the top 10 result strings
            {
                ListViewItem resultView = new ListViewItem(new[] { resultList[i]["id"], resultList[i]["title"], resultList[i]["author"], resultList[i]["biblio"], resultList[i]["abstract"] });
                resultListView.Items.Add(resultView);
            }

            TopLabel.Text = String.Format("Top {0}-{1} results", limit + 1, limit + 10);    // Display top description
            //SearchOutput.Text = outp;               // Display top 10 results
            PreviousBtn.Enabled = true; // Enable previous button
            if (limit + 20 > resultList.Count)  // If no next 10 results
            {
                NextBtn.Enabled = false;    // Disable next button
            }
            pageNub++;
            Pagelabel.Text = "Page " + pageNub;

        }

        private void PreviousBtn_Click(object sender, EventArgs e)  // When clicking on Previous 10 button
        {

            resultListView.Items.Clear();
            resultListView.Controls.Clear();
            limit -= 10;        // Get new rank starting counter
                                //Program.ViewData(limit, resultList);                    // Collate search result into displaying formats

            for (int i = limit; i < limit + 10; i++)     // Concatenate the top 10 result strings
            {
                ListViewItem resultView = new ListViewItem(new[] { resultList[i]["id"], resultList[i]["title"], resultList[i]["author"], resultList[i]["biblio"], resultList[i]["abstract"] });
                resultListView.Items.Add(resultView);
            }

            TopLabel.Text = String.Format("Top {0}-{1} results", limit + 1, limit + 10);    // Display top description
            //SearchOutput.Text = outp;   // Display previous 10 results
            NextBtn.Enabled = true;     // Enable next button
            if (limit - 10 < 0)         // If no previous results
            {
                PreviousBtn.Enabled = false;    // Disable previous button
            }

            pageNub--;
            Pagelabel.Text = "Page " + pageNub;
        }

        private void ExpandAbsBtn_Click(object sender, EventArgs e)
        {
            resultListView.Items.Clear();
            resultListView.Controls.Clear();
            if (ExpandAbsBtn.Text == "Show Abstracts")  // For changning expand button text
            {
                first = false;
                //Program.ViewData(limit, resultList);                    // Collate search result into displaying formats
                for (int i = limit; i < limit + 10; i++)     // Concatenate the top 10 result strings
                {
                    ListViewItem resultView = new ListViewItem(new[] { resultList[i]["id"], resultList[i]["title"], resultList[i]["author"], resultList[i]["biblio"], resultList[i]["abstract"] });
                    resultListView.Items.Add(resultView);
                }
                //SearchOutput.Text = outp;   // Display texts
                ExpandAbsBtn.Text = "Hide Abstracts";   // Change the expand button text
            }
            else
            {
                first = true;
                //Program.ViewData(limit, resultList);                    // Collate search result into displaying formats
                for (int i = limit; i < limit + 10; i++)     // Concatenate the top 10 result strings
                {
                    ListViewItem resultView = new ListViewItem(new[] { resultList[i]["id"], resultList[i]["title"], resultList[i]["author"], resultList[i]["biblio"], resultList[i]["abstract"] });
                    resultListView.Items.Add(resultView);
                }
                //SearchOutput.Text = outp;   // Display texts
                ExpandAbsBtn.Text = "Show Abstracts";   // Change the expand button text
            }
        }

        private void ResultListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
