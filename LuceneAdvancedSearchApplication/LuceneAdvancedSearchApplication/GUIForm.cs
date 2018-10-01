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

namespace LuceneAdvancedSearchApplication
{

    public partial class GUIForm : Form
    {
        public static String sourcePath { get; set; }
        public static String indexPath { get; set; }
        public static String needsPath { get; set; }
        public static String searchWords { get; set; }

        public static List<string> resultList { get; set; }
        public static Int32 limit { get; set; }

        LuceneAdvancedSearchApplication myLuceneApp = new LuceneAdvancedSearchApplication();

        public GUIForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        

        
        private void BuildIndBtn_Click(object sender, EventArgs e)
        {
            sourcePath = @"D:\Desktop\ifn647-project\LuceneAdvancedSearchApplication\crandocs";
            indexPath = @"C:\LuceneFolder";

            if (sourcePath is null)
                MessageBox.Show("You didn't completely select the source directory path", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (indexPath is null)
                MessageBox.Show("You didn't completely select the index directory path", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                // To keep form as Main interface
                //LuceneAdvancedSearchApplication myLuceneApp = new LuceneAdvancedSearchApplication();
                DateTime start = System.DateTime.Now;   // Indexing time starts
                myLuceneApp.CreateIndex(indexPath);     // Create index at the given path
                System.Console.WriteLine("Adding Documents to Index");
                myLuceneApp.IndexText(sourcePath);      // Add file collection to the index one by one
                Console.WriteLine("All documents added.");
                DateTime end = System.DateTime.Now;   // Indexing time ends
                MessageBox.Show("The time for indexing text was " + (end - start), "Reporting Indexing Time", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("The time for creating index was " + (end - start));  // Calculate and show the indexing time
                myLuceneApp.CleanUpIndexer();
                SearchBtn2.Enabled = true;      // Enable search button 2
            }


        }

        private void TextEnter_TextChanged(object sender, EventArgs e)
        {
            //TextShowChange.Text = TextEnter.Text;
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
            SearchBtn1.Enabled = true;      // Enable search button 1
        }

        private void SearchBtn1_Click(object sender, EventArgs e)   // Whe clicking on search button for Cran Needs
        {
            ExpandAbsBtn.Text = "Show Abstracts";       // Retore expand abstract button
            //needsPath = @"D:\Desktop\ifn647-project\LuceneAdvancedSearchApplication\cran_information_needs.txt";

            // To keep form as Main interface
            Dictionary<string, string> cranNeeds = myLuceneApp.ReadCranNeeds(needsPath);   // Put the cran_information_need into a dictionary
                                                                                              
                //// Searching Code
                DateTime start = System.DateTime.Now;   // Searching time starts
                myLuceneApp.CreateSearcher();           // Create searcher
                //foreach(KeyValuePair<string, string> entry in cranNeeds)
                //{
                //    myLuceneApp.SearchText(entry.Value);

                //}
                resultList =  myLuceneApp.SearchText(cranNeeds["001"]);     // Get search result list
                myLuceneApp.CleanUpSearcher();        // Clean searcher
                DateTime end = System.DateTime.Now;   // Searching time starts
                MessageBox.Show("The time for searching text was " + (end - start), "Reporting Searching Time", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("The time for searching text was " + (end - start));  // Calculate and show the searching time
                                                                                        

                string outp = "";       // Initial null string
                limit = 0;              // Set top rank starting counter

                for(int i = 0; i < limit + 10; i++)     // Concatenate the top 10 result strings
                {
                    Regex rx = new Regex("Abstract:.*?[.?!]", RegexOptions.Compiled | RegexOptions.IgnoreCase);     // Set the RE to match first sentence of abstract
                    MatchCollection matches = rx.Matches(resultList[i]);   // Get RE match

                    outp += "Rank: " + (i + 1).ToString() + "\r\n" + resultList[i].Substring(0, resultList[i].LastIndexOf("Abstract: ")) + matches[0].Value + "\r\n\r\n";   // Combine displaying texts

                //outp += "Rank: " + (i + 1).ToString() + "\r\n" + resultList[i] + "\r\n\r\n";
                }

                TopLabel.Text = "Top 1-10 results";     // Display top description
                SearchOutput.Text = outp;               // Display top 10 results
                NextBtn.Enabled = true;                 // Enable next button
                ExpandAbsBtn.Enabled = true;            // Enable expand abstract button      
        }


        private void SearchBtn2_Click(object sender, EventArgs e)       // When clicking on search button for user free-typing
        {
            ExpandAbsBtn.Text = "Show Abstracts";       // Retore expand abstract button
            if (TextEnter.Text == "")       // Check if the textbox is empty
            {
                MessageBox.Show("Enter something!!", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // To keep form as Main interface
                Dictionary<string, string> cranNeeds = myLuceneApp.ReadCranNeeds(needsPath);   // Put the cran_information_need into a dictionary

                //// Searching Code
                DateTime start = System.DateTime.Now;   // Searching time starts
                myLuceneApp.CreateSearcher();           // Create searcher
                //foreach(KeyValuePair<string, string> entry in cranNeeds)
                //{
                //    myLuceneApp.SearchText(entry.Value);

                //}
                List<string> tempList = new List<string>();            // Create temporary list
                tempList = myLuceneApp.SearchText(TextEnter.Text);     // Get search result list
                myLuceneApp.CleanUpSearcher();                         // Clean searcher
                DateTime end = System.DateTime.Now;   // Searching time starts
                Console.WriteLine("The time for searching text was " + (end - start));  // Calculate and show the searching time

                if (tempList.Count != 0)    // If there are found results
                {
                    MessageBox.Show("The time for searching text was " + (end - start), "Reporting Searching Time", MessageBoxButtons.OK, MessageBoxIcon.Information);  // Reporting searching time
                    string outp = "";       // Initial null string
                    limit = 0;              // Set top rank starting counter

                    resultList = tempList;  // Assign temporary list to global variable as current 10 results

                    for (int i = 0; i < limit + 10; i++)     // Concatenate the top 10 result strings
                    {

                        Regex rx = new Regex("Abstract:.*?[.?!]", RegexOptions.Compiled | RegexOptions.IgnoreCase);     // Set the RE to match first sentence
                        MatchCollection matches = rx.Matches(resultList[i]);   // Get RE match
                        //string match = "";
                        //if (matches.Count !=0)
                        //    match = matches[0].Value; 
                        outp += "Rank: " + (i + 1).ToString() + "\r\n" + resultList[i].Substring(0, resultList[i].LastIndexOf("Abstract: ")) + matches[0].Value + "\r\n\r\n";   // Combine displaying texts
                    }

                    TopLabel.Text = "Top 1-10 results";     // Display top description
                    SearchOutput.Text = outp;               // Display top 10 results
                    NextBtn.Enabled = true;                 // Enable next button
                    ExpandAbsBtn.Enabled = true;            // Enable expand abstract button
                }
                else
                {
                    MessageBox.Show("No results were found, please try something else!", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);  // Display error
                }
            }
        }

            private void NextBtn_Click(object sender, EventArgs e)  // When clicking on Next 10 button
        {
            ExpandAbsBtn.Text = "Show Abstracts";   // Restore expand button
            string outp = "";       // Initial null string
            
            //if (limit + 20 <= resultList.Count)     // Check if starting rank number exists
            //{

            limit += 10;        // Get new rank starting counter
            for (int i = limit; i < limit + 10; i++)    // Concatenate the next 10 result strings
            {
                Regex rx = new Regex("Abstract:.*?[.?!]", RegexOptions.Compiled | RegexOptions.IgnoreCase);     // Set the RE to match first sentence
                MatchCollection matches = rx.Matches(resultList[i]);   // Get RE match

                outp += "Rank: " + (i + 1).ToString() + "\r\n" + resultList[i].Substring(0, resultList[i].LastIndexOf("Abstract: ")) + matches[0].Value + "\r\n\r\n";   // Combine displaying texts
                //outp += "Rank: " + (i + 1).ToString() + "\r\n" + resultList[i] + "\r\n\r\n";
            }
            TopLabel.Text = String.Format("Top {0}-{1} results", limit + 1, limit + 10);    // Display top description
            SearchOutput.Text = outp;   // Display next 10 results
            PreviousBtn.Enabled = true; // Enable previous button
            if (limit + 20 > resultList.Count)  // If no next 10 results
            {
                NextBtn.Enabled = false;    // Disable next button
            }
            
            //}
            //else
            //{
                //NextBtn.Enabled = false;
                //MessageBox.Show("These are already the last 10 results!!", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);  // Display error
            //}
        }

        private void PreviousBtn_Click(object sender, EventArgs e)  // When clicking on Previous 10 button
        {
            ExpandAbsBtn.Text = "Show Abstracts";   // Restore expand button
            string outp = "";       // Initial null string

            //if (limit - 10 >= 0)    // Check if starting rank number exists
            //{

            limit -= 10;        // Get new rank starting counter
            for (int i = limit; i < limit + 10; i++)    // Concatenate previous 10 result strings
            {
                Regex rx = new Regex("Abstract:.*?[.?!]", RegexOptions.Compiled | RegexOptions.IgnoreCase);     // Set the RE to match first sentence
                MatchCollection matches = rx.Matches(resultList[i]);   // Get RE match

                outp += "Rank: " + (i + 1).ToString() + "\r\n" + resultList[i].Substring(0, resultList[i].LastIndexOf("Abstract: ")) + matches[0].Value + "\r\n\r\n";   // Combine displaying texts
                //outp += "Rank: " + (i + 1).ToString() + "\r\n" + resultList[i] + "\r\n\r\n";
            }
            TopLabel.Text = String.Format("Top {0}-{1} results", limit + 1, limit + 10);    // Display top description
            SearchOutput.Text = outp;   // Display previous 10 results
            NextBtn.Enabled = true;     // Enable next button
            if (limit - 10 < 0)         // If no previous results
            {
                PreviousBtn.Enabled = false;    // Disable previous button
            }

            // old ways for message box
            //}
            //else
            //{
                //PreviousBtn.Enabled = false;
                //MessageBox.Show("You are already at the top 10 results!!", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);  // Display error
            //}
        }

        private void ExpandAbsBtn_Click(object sender, EventArgs e)
        {
            // Backup expanding form
            //GUIForm form2 = new GUIForm();
            //form2.Show();
            string outp = "";   // Initial null string
            if (ExpandAbsBtn.Text == "Show Abstracts")  // For changning expand button text
            {
                for (int i = limit; i < limit + 10; i++)     // Concatenate the current 10 result strings
                {
                    outp += "Rank: " + (i + 1).ToString() + "\r\n" + resultList[i] + "\r\n\r\n";    // Combine original texts
                }
                SearchOutput.Text = outp;   // Display texts
                ExpandAbsBtn.Text = "Hide Abstracts";   // Change the expand button text
            }
            else
            {
                for (int i = limit; i < limit + 10; i++)     // Concatenate the top 10 result strings
                {

                    Regex rx = new Regex("Abstract:.*?[.?!]", RegexOptions.Compiled | RegexOptions.IgnoreCase);     // Set the RE to match first sentence
                    MatchCollection matches = rx.Matches(resultList[i]);   // Get RE match
                    outp += "Rank: " + (i + 1).ToString() + "\r\n" + resultList[i].Substring(0, resultList[i].LastIndexOf("Abstract: ")) + matches[0].Value + "\r\n\r\n";   // Combine displaying texts
                }
                SearchOutput.Text = outp;   // Display texts
                ExpandAbsBtn.Text = "Show Abstracts";   // Change the expand button text
            }
        }
    }
}
