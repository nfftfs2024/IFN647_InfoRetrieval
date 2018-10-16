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

        public string advtext, cond_operator;

        List<Dictionary<string, string>> resultListDict;    // Create global result list in type of list of dictionaries
        int limit;              // Create document starting index variable
        string finalQueryTxt;    // Create final query text variable

        string savePath;        // Create save path variable
        int pageNub;            // Create page number variable
        int queryCount;         // Create query count variable
        int totalpage;          // Create total page variable



        public SearchGUIForm()
        {
            InitializeComponent();

            // Create the column headers for the list view
            resultListView.Columns.Add("Rank", 50);
            resultListView.Columns.Add("DocID", 55);
            resultListView.Columns.Add("Title", 450);
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
            comboBox1.Enabled = true;
        }

        private void SaveResult_Click(object sender, EventArgs e)   // Select directory path after click on Set Index Directory
        {
            
            SaveFileDialog saveDialog = new SaveFileDialog();
           
            SaveDialog.Filter = "Text File | *.txt";
            SaveDialog.ShowDialog();
            savePath = SaveDialog.FileName;
            StreamWriter writer = new StreamWriter(savePath,append:true);
            Program.SaveClick(resultListDict, writer,queryCount);
            
            ConvertBtn.Enabled=true;


        }

        private void SearchBtn1_Click(object sender, EventArgs e)   // Whe clicking on search button for Cran Needs
        {
            limit = 0;      // Set starting result index

            DateTime start = System.DateTime.Now;   // Searching time starts
            resultListDict = Program.Search_Click(cranNeeds[comboBox1.SelectedItem.ToString()], false, QECheckbox.Checked, advancedCheck.Checked, cond_operator, out finalQueryTxt);       // Search Cran needs texts
            DateTime end = System.DateTime.Now;   // Searching time starts
            MessageBox.Show("The time for searching text was " + (end - start).TotalMilliseconds +" milliseconds", "Reporting Searching Time", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ViewData(limit, resultListDict);    // View data on listview
            Program.Create_BaseLine_Results(cranNeeds);

            NextBtn.Enabled = true;                 // Enable next button
            PreviousBtn.Enabled = false;            // Disable previous button
            SaveResult.Enabled = true;              // Enable save result button
            resultLab.Text = "Result numbers:";     // Display result number label
            //FinalQTxtbox.Text = cranNeeds[comboBox1.SelectedItem.ToString()];     //Print Query 
            FinalQTxtbox.Text = finalQueryTxt;
            resultNumLab.Text = resultListDict.Count.ToString();    // Display result number



            pageNub = 1;
            totalpage = Convert.ToInt32(Math.Ceiling((double)resultListDict.Count / 10));
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
                List<Dictionary<string, string>> tempListDict = new List<Dictionary<string, string>>();            // Create temporary list of dictionaries
                DateTime start = System.DateTime.Now;   // Searching time starts
                tempListDict = Program.Search_Click(TextEnter.Text, asIsCheckBox.Checked, QECheckbox.Checked, advancedCheck.Checked, cond_operator, out finalQueryTxt);     // Search user input texts
                DateTime end = System.DateTime.Now;   // Searching time starts
                
                if (tempListDict.Count != 0)
                {
                    MessageBox.Show("The time for searching text was " + (end - start).TotalMilliseconds + " milliseconds", "Reporting Searching Time", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    resultListDict = tempListDict;  // Assign temporary list to global variable as current 10 results
                    queryCount++;       // Count number of query search 
                    ViewData(limit, resultListDict);    // View data on listview

                    NextBtn.Enabled = true;                 // Enable next button
                    PreviousBtn.Enabled = false;            // Disable previous button
                    SaveResult.Enabled = true;              // Enable save result button
                    FinalQTxtbox.Text = finalQueryTxt;      // Display final query text
                    resultLab.Text = "Result numbers:";     // Display result number label
                    resultNumLab.Text = resultListDict.Count.ToString();    // Display result number
                    pageNub = 1;
                    totalpage = Convert.ToInt32(Math.Ceiling((double)resultListDict.Count / 10));
                    Pagelabel.Text = String.Format("Page {0} of {1}", pageNub, totalpage);
                }
                else
                {
                    MessageBox.Show("No results were found, please try something else!", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);  // Display error
                }

            }
          
        }

        private void NextBtn_Click(object sender, EventArgs e)  // When clicking on Next 10 button
        {
            pageNub++;
            totalpage = Convert.ToInt32(Math.Ceiling((double)resultListDict.Count / 10));
            Pagelabel.Text = String.Format("Page {0} of {1}", pageNub, totalpage);

            limit += 10;        // Get new rank starting counter
            ViewData(limit, resultListDict);    // View data on listview

            PreviousBtn.Enabled = true; // Enable previous button
            if (limit + 20 - resultListDict.Count >= 10)  // If no next 10 results
            {
                NextBtn.Enabled = false;    // Disable next button
            }
        }

        private void PreviousBtn_Click(object sender, EventArgs e)  // When clicking on Previous 10 button
        {
            pageNub--;
            totalpage = Convert.ToInt32(Math.Ceiling((double)resultListDict.Count / 10));
            Pagelabel.Text = String.Format("Page {0} of {1}", pageNub, totalpage);

            limit -= 10;        // Get new rank starting counter
            ViewData(limit, resultListDict);    // View data on listview

            NextBtn.Enabled = true;     // Enable next button
            if (limit - 10 < 0)         // If no previous results
            {
                PreviousBtn.Enabled = false;    // Disable previous button
            }
        }


        private void resultListDictView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (resultListView.Items.Count != 0)
            {
                if (resultListView.SelectedItems.Count > 0)
                {
                    int rank = Int32.Parse(resultListView.SelectedItems[0].Text);
                    //var popform = new Form();
                    //popform.ShowDialog();
                    MessageBox.Show(resultListDict[rank - 1]["abstract"], "Entire Abstract");
                }
            }

        }

        public void ViewData(int limit, List<Dictionary<string, string>> resultListDict)    // Create global method for viewing the data
        {
            resultListView.Items.Clear();
            resultListView.Controls.Clear();        // Clear current listview
            int end = 0;
            if (resultListDict.Count - limit < 10)      // Check if it's the last results less than 10
            {
                end = limit + (resultListDict.Count % 10);  // Get the modulus
            }
            else
            {
                end = limit + 10;
            }
            for (int i = limit; i < end; i++)     // Loop through current 10 results
            {
                // Add result details into the listview
                ListViewItem resultView = new ListViewItem(new[] { resultListDict[i]["rank"], resultListDict[i]["id"], resultListDict[i]["title"],
                                                            resultListDict[i]["author"], resultListDict[i]["biblio"], resultListDict[i]["abstract_first"] });
                resultListView.Items.Add(resultView);
            }
        }

        private void ConvertBtn_Click(object sender, EventArgs e)
        {
            myNeedsDialog.ShowDialog();
            
            Converter.Dos2Unix(myNeedsDialog.FileName);
            Converter.RemoveBOM(myNeedsDialog.FileName);
        }

        private void advancedSearching_Click(object sender, EventArgs e)
        {
            AdvancedSearchGUI as1 = new AdvancedSearchGUI(this);
            as1.Show();
        }

        internal void populate()
        {
            TextEnter.Text = advtext;
            SearchBtn2.Enabled = true;
            advancedCheck.Checked = true;
            advancedCheck.Visible = true;
            advancedCheck.Enabled = false;
            SearchBtn2.PerformClick();
            SearchBtn2.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            NeedsTxtbox.Text = cranNeeds[comboBox1.SelectedItem.ToString()];
        }

        private void asIsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (asIsCheckBox.Checked)
                QECheckbox.Enabled = false;
            else if (!asIsCheckBox.Checked)
                QECheckbox.Enabled = true;

        }

        private void QECheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (QECheckbox.Checked)
                asIsCheckBox.Enabled = false;
            else if (!QECheckbox.Checked)
                asIsCheckBox.Enabled = true;
        }
    }
}
