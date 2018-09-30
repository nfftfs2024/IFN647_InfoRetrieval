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

        public static List<string> resultList { get; set; }
        public static Int32 limit { get; set; }

        public GUIForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        

        
        private void BuildIndBtn_Click(object sender, EventArgs e)
        {

            if ((indexPath is null) || (sourcePath is null)
                {

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
        }


        private void Confirm_button_Click(object sender, EventArgs e)   // Whe clicking on "" button
        {
            //sourcePath = @"D:\Desktop\ifn647-project\LuceneAdvancedSearchApplication\crandocs";

            //indexPath = @"C:\LuceneFolder";

            //needsPath = @"D:\Desktop\ifn647-project\LuceneAdvancedSearchApplication\cran_information_needs.txt";

            if ((indexPath is null) || (sourcePath is null) || (needsPath is null))     // Check if the paths are set
            {
                if (sourcePath is null)
                    MessageBox.Show("You didn't completely select the source directory path", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (indexPath is null)
                    MessageBox.Show("You didn't completely select the index directory path", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("You didn't completely select the directory paths or files", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            else
            {
                //this.Close();

                // To keep form as Main interface
                LuceneAdvancedSearchApplication myLuceneApp = new LuceneAdvancedSearchApplication();
                DateTime start = System.DateTime.Now;   // Indexing time starts
                myLuceneApp.CreateIndex(indexPath);     // Create index at the given path
                System.Console.WriteLine("Adding Documents to Index");
                myLuceneApp.IndexText(sourcePath);      // Add file collection to the index one by one
                myLuceneApp.CleanUpIndexer();           // Flush the buffer and close the index
                Console.WriteLine("All documents added.");
                DateTime end = System.DateTime.Now;   // Indexing time ends
                Console.WriteLine("The time for creating index was " + (end - start));  // Calculate and show the indexing time

                Dictionary<string, string> cranNeeds = myLuceneApp.ReadCranNeeds(needsPath);   // Put the cran_information_need into a dictionary
                                                                                              

                //// Searching Code
                start = System.DateTime.Now;   //Searching time starts
                myLuceneApp.CreateSearcher();
                //foreach(KeyValuePair<string, string> entry in cranNeeds)
                //{
                //    myLuceneApp.SearchText(entry.Value);

                //}
                resultList =  myLuceneApp.SearchText(cranNeeds["001"]);     // Get search result list
                myLuceneApp.CleanUpSearcher();
                end = System.DateTime.Now;   // Searching time starts
                Console.WriteLine("The time for creating index was " + (end - start));  // Calculate and show the searching time
                                                                                        


                string outp = "";       // Initial null string
                limit = 0;              // Set top rank starting counter

                for(int i = 0; i < limit + 10; i++)     // Concatenate the top 10 result strings
                {
                    outp += "Rank: " + (i + 1).ToString() + "\r\n" + resultList[i] + "\r\n\r\n";
                }

                TopLabel.Text = "Top 1-10 results";     // Display top description
                SearchOutput.Text = outp;               // Display top 10 results
                
            }
        }

        private void NextBtn_Click(object sender, EventArgs e)  // When clicking on Next 10 button
        {
            string outp = "";       // Initial null string
            
            if (limit + 20 <= resultList.Count)     // Check if starting rank number exists
            {
                limit += 10;        // Get new rank starting counter
                for (int i = limit; i < limit + 10; i++)    // Concatenate the next 10 result strings
                {
                    outp += "Rank: " + (i + 1).ToString() + "\r\n" + resultList[i] + "\r\n\r\n";
                }
                TopLabel.Text = String.Format("Top {0}-{1} results", limit + 1, limit + 10);    // Display top description
                SearchOutput.Text = outp;   // Display next 10 results
            }
            else
            {
                MessageBox.Show("These are already the last 10 results!!", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);  // Display error
            }
        }

        private void PreviousBtn_Click(object sender, EventArgs e)  // When clicking on Previous 10 button
        {
            string outp = "";       // Initial null string

            if (limit - 10 >= 0)    // Check if starting rank number exists
            {
                limit -= 10;        // Get new rank starting counter
                for (int i = limit; i < limit + 10; i++)    // Concatenate previous 10 result strings
                {
                    outp += "Rank: " + (i + 1).ToString() + "\r\n" + resultList[i] + "\r\n\r\n";
                }
                TopLabel.Text = String.Format("Top {0}-{1} results", limit + 1, limit + 10);    // Display top description
                SearchOutput.Text = outp;   // Display previous 10 results
            }
            else
            {
                MessageBox.Show("You are already at the top 10 results!!", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);  // Display error
            }
        }
    }
}
