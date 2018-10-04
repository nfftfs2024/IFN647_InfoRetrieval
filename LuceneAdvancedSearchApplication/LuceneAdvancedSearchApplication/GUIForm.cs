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

    public partial class GUIForm : Form
    {
        public static String sourcePath { get; set; }
        public static String indexPath { get; set; }
        public static String needsPath { get; set; }
        public static String searchWords { get; set; }
        public static String savePath { get; set; }

        public static List<List<string>> resultList { get; set; }
        public static Int32 limit { get; set; }
        public static Boolean first { get; set; }

        Dictionary<string, string> cranNeeds;

        LuceneSearcheEngine myLuceneApp;    // Create a search engine object

        public GUIForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void BuildIndBtn_Click(object sender, EventArgs e)
        {
            myLuceneApp = new LuceneSearcheEngine();    // Initiate search engine object
            Program.BuildIndex_Click(sourcePath, indexPath, myLuceneApp);   // Build index
            SearchBtn2.Enabled = true;      // Enable search button 2
            
        }

        private void TextEnter_TextChanged(object sender, EventArgs e)
        {
            
            searchWords = TextEnter.Text;
        }

        private void SetSourceDirBtn_Click(object sender, EventArgs e)  // Select directory path after click on Set Source Directory
        {
            SourceDirBrowserDialog.ShowDialog();
            SourceLabel.Text = SourceDirBrowserDialog.SelectedPath;
            sourcePath= SourceDirBrowserDialog.SelectedPath;
            
        }

        private void SetIndexDirBtn_Click(object sender, EventArgs e)   // Select directory path after click on Set Index Directory
        {
            IndexDirBrowserDialog.ShowDialog();
            IndexLabel.Text = IndexDirBrowserDialog.SelectedPath;
            indexPath = IndexDirBrowserDialog.SelectedPath;
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
            StreamWriter writer = new StreamWriter(SaveDialog.OpenFile());
            //Program.SaveClick(resultList, writer);
            

        }

        private void SearchBtn1_Click(object sender, EventArgs e)   // Whe clicking on search button for Cran Needs
        {
            ExpandAbsBtn.Text = "Show Abstracts";       // Retore expand abstract button
            first = true;
            DateTime start = System.DateTime.Now;   // Searching time starts
            resultList = Program.Search_Click(cranNeeds[comboBox1.SelectedItem.ToString()], myLuceneApp);       // Search Cran needs texts
            DateTime end = System.DateTime.Now;   // Searching time starts
            MessageBox.Show("The time for searching text was " + (end - start), "Reporting Searching Time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string outp = Program.ViewData(0, resultList, first);                    // Collate search result into displaying formats
            Program.Create_BaseLine_Results(cranNeeds, myLuceneApp);

            TopLabel.Text = "Top 1-10 results";     // Display top description
            SearchOutput.Text = outp;               // Display top 10 results
            NextBtn.Enabled = true;                 // Enable next button
            ExpandAbsBtn.Enabled = true;            // Enable expand abstract button 
            SaveResult.Enabled = true;              // Enable save result button
            NeedQuery.Text = cranNeeds[comboBox1.SelectedItem.ToString()];     //Print Query 



        }

        private void SearchBtn2_Click(object sender, EventArgs e)       // When clicking on search button for user free-typing
        {
            ExpandAbsBtn.Text = "Show Abstracts";       // Retore expand abstract button
            first = true;
            if (TextEnter.Text == "")       // Check if the textbox is empty
            {
                MessageBox.Show("Enter something!!", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                List<List<string>> tempList = new List<List<string>>();            // Create temporary list
                DateTime start = System.DateTime.Now;   // Searching time starts
                tempList = Program.Search_Click(TextEnter.Text, myLuceneApp);     // Search user input texts
                DateTime end = System.DateTime.Now;   // Searching time starts


                if (tempList.Count != 0)
                {
                    MessageBox.Show("The time for searching text was " + (end - start), "Reporting Searching Time", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    resultList = tempList;  // Assign temporary list to global variable as current 10 results
                    string outp = Program.ViewData(0, resultList, first);                // Collate search result into displaying formats

                    TopLabel.Text = "Top 1-10 results";     // Display top description
                    SearchOutput.Text = outp;               // Display top 10 results
                    NextBtn.Enabled = true;                 // Enable next button
                    ExpandAbsBtn.Enabled = true;            // Enable expand abstract button 
                    SaveResult.Enabled = true;              // Enable save result button
                }
                else
                {
                    MessageBox.Show("No results were found, please try something else!", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);  // Display error
                }

            }
        }

        private void NextBtn_Click(object sender, EventArgs e)  // When clicking on Next 10 button
        {
           
            limit += 10;        // Get new rank starting counter
            string outp = Program.ViewData(limit, resultList, first);                    // Collate search result into displaying formats

            TopLabel.Text = String.Format("Top {0}-{1} results", limit + 1, limit + 10);    // Display top description
            SearchOutput.Text = outp;               // Display top 10 results
            PreviousBtn.Enabled = true; // Enable previous button
            if (limit + 20 > resultList.Count)  // If no next 10 results
            {
                NextBtn.Enabled = false;    // Disable next button
            }
            
                
        }

        private void PreviousBtn_Click(object sender, EventArgs e)  // When clicking on Previous 10 button
        {
            

            limit -= 10;        // Get new rank starting counter
            string outp = Program.ViewData(limit, resultList, first);                    // Collate search result into displaying formats

            TopLabel.Text = String.Format("Top {0}-{1} results", limit + 1, limit + 10);    // Display top description
            SearchOutput.Text = outp;   // Display previous 10 results
            NextBtn.Enabled = true;     // Enable next button
            if (limit - 10 < 0)         // If no previous results
            {
                PreviousBtn.Enabled = false;    // Disable previous button
            }
           
        }

        private void ExpandAbsBtn_Click(object sender, EventArgs e)
        {
            
            if (ExpandAbsBtn.Text == "Show Abstracts")  // For changning expand button text
            {
                first = false;
                string outp = Program.ViewData(limit, resultList, first);                    // Collate search result into displaying formats
                
                SearchOutput.Text = outp;   // Display texts
                ExpandAbsBtn.Text = "Hide Abstracts";   // Change the expand button text
            }
            else
            {
                first = true;
                string outp = Program.ViewData(limit, resultList, first);                    // Collate search result into displaying formats
               
                SearchOutput.Text = outp;   // Display texts
                ExpandAbsBtn.Text = "Show Abstracts";   // Change the expand button text
            }
        }
    }
}
