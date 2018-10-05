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
        string needsPath;       // Create need path variable
        Dictionary<string, string> cranNeeds;   // Create Crans need dictionary

        string searchWords;

        List<Dictionary<string, string>> resultList;    // Create global result list in type of list of dictionaries
        int limit;              // Create document starting index variable

        string savePath;        // Create save path variable
        int pageNub;            // Create page number variable
        int queryCount;         // Create query count variable
        int totalpage;          // Create total page variable

        

        public SearchGUIForm()
        {
            InitializeComponent();

            // Create the column headers for the list view
            resultListView.Columns.Add("Rank", 40);
            resultListView.Columns.Add("DocID", 45);
            resultListView.Columns.Add("Title", 380);
            resultListView.Columns.Add("Author", 120);
            resultListView.Columns.Add("Bibliography", 120);
            resultListView.Columns.Add("Abstract", 500);
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
            limit = 0;      // Set starting result index

            DateTime start = System.DateTime.Now;   // Searching time starts
            resultList = Program.Search_Click(cranNeeds[comboBox1.SelectedItem.ToString()]);       // Search Cran needs texts
            DateTime end = System.DateTime.Now;   // Searching time starts
            MessageBox.Show("The time for searching text was " + (end - start), "Reporting Searching Time", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ViewData(limit, resultList);    // View data on listview
            Program.Create_BaseLine_Results(cranNeeds);

            NextBtn.Enabled = true;                 // Enable next button
            PreviousBtn.Enabled = false;            // Disable previous button
            SaveResult.Enabled = true;              // Enable save result button

            //NeedQuery.Text = cranNeeds[comboBox1.SelectedItem.ToString()];     //Print Query 

            pageNub = 1;
            totalpage = resultList.Count / 10;
            Pagelabel.Text = String.Format("Page {0} of {1}", pageNub, totalpage);
        }

        private void SearchBtn2_Click(object sender, EventArgs e)       // When clicking on search button for user free-typing
        {

            limit = 0;      // Set starting result index

            if (TextEnter.Text == "")       // Check if the textbox is empty
            {
                MessageBox.Show("Enter something!!", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                List<Dictionary<string, string>> tempList = new List<Dictionary<string, string>>();            // Create temporary list of dictionaries
                DateTime start = System.DateTime.Now;   // Searching time starts
                tempList = Program.Search_Click(TextEnter.Text);     // Search user input texts
                DateTime end = System.DateTime.Now;   // Searching time starts
                
                if (tempList.Count != 0)
                {
                    MessageBox.Show("The time for searching text was " + (end - start), "Reporting Searching Time", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    resultList = tempList;  // Assign temporary list to global variable as current 10 results
                    queryCount++;       // Count number of query search 
                    ViewData(limit, resultList);    // View data on listview

                    NextBtn.Enabled = true;                 // Enable next button
                    PreviousBtn.Enabled = false;            // Disable previous button
                    SaveResult.Enabled = true;              // Enable save result button
                }
                else
                {
                    MessageBox.Show("No results were found, please try something else!", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);  // Display error
                }

            }
            pageNub = 1;
            totalpage = resultList.Count / 10;
            Pagelabel.Text = String.Format("Page {0} of {1}", pageNub, totalpage);
        }

        private void NextBtn_Click(object sender, EventArgs e)  // When clicking on Next 10 button
        {
            limit += 10;        // Get new rank starting counter
            ViewData(limit, resultList);    // View data on listview

            PreviousBtn.Enabled = true; // Enable previous button
            if (limit + 20 > resultList.Count)  // If no next 10 results
            {
                NextBtn.Enabled = false;    // Disable next button
            }

            pageNub++;
            totalpage = resultList.Count / 10;
            Pagelabel.Text = String.Format("Page {0} of {1}", pageNub, totalpage);
        }

        private void PreviousBtn_Click(object sender, EventArgs e)  // When clicking on Previous 10 button
        {
            limit -= 10;        // Get new rank starting counter
            ViewData(limit, resultList);    // View data on listview

            NextBtn.Enabled = true;     // Enable next button
            if (limit - 10 < 0)         // If no previous results
            {
                PreviousBtn.Enabled = false;    // Disable previous button
            }

            pageNub--;
            totalpage = resultList.Count / 10;
            Pagelabel.Text = String.Format("Page {0} of {1}", pageNub, totalpage);
        }


        private void ResultListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void ViewData(int limit, List<Dictionary<string, string>> resultList)    // Create global method for viewing the data
        {
            resultListView.Items.Clear();
            resultListView.Controls.Clear();        // Clear current listview

            for (int i = limit; i < limit + 10; i++)     // Loop through current 10 results
            {
                // Add result details into the listview
                ListViewItem resultView = new ListViewItem(new[] { resultList[i]["rank"], resultList[i]["id"], resultList[i]["title"], resultList[i]["author"], resultList[i]["biblio"], resultList[i]["abstract"] });
                resultListView.Items.Add(resultView);
            }
        }
    }
}
