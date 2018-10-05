using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LuceneAdvancedSearchApplication
{
    static class Program
    {

        public static LuceneSearcheEngine myLuceneApp;
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BuildIndexGUIForm());
            Application.Run(new SearchGUIForm());
        }


        public static void BuildIndex_Click(string sourcePath, string indexPath)
        {
            myLuceneApp = new LuceneSearcheEngine();    // Initiate LuceneSearchEngine object

            if (sourcePath is null)
                MessageBox.Show("You didn't completely select the source directory path", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (indexPath is null)
                MessageBox.Show("You didn't completely select the index directory path", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                DateTime start = System.DateTime.Now;   // Indexing time starts
                myLuceneApp.CreateIndex(indexPath);     // Create index at the given path
                System.Console.WriteLine("Adding Documents to Index");
                myLuceneApp.IndexText(sourcePath);      // Add file collection to the index one by one
                System.Console.WriteLine("All documents added.");
                myLuceneApp.CleanUpIndexer();       // Clean up indexer
                DateTime end = System.DateTime.Now;   // Indexing time ends
                MessageBox.Show("The time for indexing text was " + (end - start), "Reporting Indexing Time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static List<Dictionary<string, string>> Search_Click(string querytext)
        {
            List<List<string>> tempList = new List<List<string>>();
            List<Dictionary<string, string>> resultList = new List<Dictionary<string, string>>();
            //// Searching Code
            myLuceneApp.CreateSearcher();           // Create searcher
            //resultList = myLuceneApp.SearchText(querytext);     // Get search result list
            tempList = myLuceneApp.SearchText_bk(querytext);     // Get search result list
            myLuceneApp.CleanUpSearcher();        // Clean searcher

            foreach (List<string> result in tempList)
            {
                string text = result[0];
                int indexI = text.IndexOf(".I ") + 3;   // Get ID starting index
                int indexT = text.IndexOf(".T\n");    // Get title starting index
                int indexA = text.IndexOf(".A\n");    // Get author starting index
                int indexB = text.IndexOf(".B\n");    // Get bibliography starting index
                int indexW = text.IndexOf(".W\n");    // Get words starting index

                string id = text.Substring(indexI, indexT - 1 - indexI);    // Get ID string
                string title = text.Substring(indexT + 3, ((indexA - 1 - (indexT + 3)) > 0) ? (indexA - 1 - (indexT + 3)) : 0);     // Get title string
                string author = text.Substring(indexA + 3, ((indexB - 1 - (indexA + 3)) > 0) ? (indexB - 1 - (indexA + 3)) : 0);    // Get author string
                string biblio = text.Substring(indexB + 3, ((indexW - 1 - (indexB + 3)) > 0) ? (indexW - 1 - (indexB + 3)) : 0);    // Get bibliography string
                string abst = text.Substring(indexW + 3, ((text.Length - 1 - (indexW + 3)) > 0) ? (text.Length - 1 - (indexW + 3)) : 0);   // Get abstract string

                //string[] parts = resultSet[i][0].Split(new string[] { ".W\n" }, StringSplitOptions.RemoveEmptyEntries);   // Cut half the texts from the starting of .W
                //string firsthalf = parts[0].Replace(".I ", "DocID: ").Replace(".T\n", "\r\nTitle: ").Replace(".A\n", "\r\nAuthor: ").Replace(".B\n", "\r\nBibliographic information: ");  // First half
                abst = abst.Replace("\n", " ");  // Replace abstract LF

                Regex rx = new Regex("^.*?[.?!]", RegexOptions.Compiled | RegexOptions.IgnoreCase);     // Set the RE to match first sentence of abstract
                MatchCollection abst_first = rx.Matches(abst);   // Get RE match

                resultList.Add(new Dictionary<string, string> { { "id", id }, { "title", title }, { "author", author }, { "biblio", biblio }, { "abstract", abst }, { "abstract_first", abst_first[0].Value}, { "score", result[0] } });     // Add contents and score into the created list of lists
            }
            return resultList;
        }

        public static void Create_BaseLine_Results(Dictionary<string, string> cNeeds, LuceneSearcheEngine myLuceneApp)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\BaseLineResults.txt";
            List<string> resultList = new List<string>();
            List<float> valueListBase = new List<float>();
            List<string> docsIdsListBase = new List<string>();
            //// Searching Code
            myLuceneApp.CreateSearcher();           // Create searcher
            bool control = true;
            foreach (string key in cNeeds.Keys)
            {
                Tuple<List<float>, List<string>,int> result = myLuceneApp.SearchText_baseline(cNeeds[key]);
                myLuceneApp.CleanUpSearcher();        // Clean searcher
                valueListBase = result.Item1;    // Get scores ranked documents ranked
                docsIdsListBase = result.Item2;     //Get IDs ranked documents
                int totalHits = result.Item3 ;
                if (!File.Exists(path) && control)
                {
                    File.Create(path).Dispose();
                    using (TextWriter tw = new StreamWriter(path, append: true))
                    {
                        for (int i = 0; i < totalHits; i++)
                        {
                            tw.WriteLine(key + "\tQ0\t" + docsIdsListBase[i].ToString() + "\t{0}\t" + valueListBase[i].ToString() + "\tBaselineSystem", i + 1);
                            control = false;
                        }
                    }
                }
                else if (File.Exists(path) && control)
                {
                    File.Delete(path);
                    File.Create(path).Dispose();
                    using (TextWriter tw = new StreamWriter(path, append: true))
                    {
                        for (int i = 0; i < totalHits; i++)
                        {
                            tw.WriteLine(key + "\tQ0\t" + docsIdsListBase[i].ToString() + "\t{0}\t" + valueListBase[i].ToString() + "\tBaselineSystem", i + 1);
                            control = false;
                        }
                    }
                }
                else if (File.Exists(path) && !control)
                {
                    using (TextWriter tw = new StreamWriter(path, append: true))
                    {
                        for (int i = 0; i < totalHits; i++)
                        {
                            tw.WriteLine(key + "\tQ0\t" + docsIdsListBase[i].ToString() + "\t{0}\t" + valueListBase[i].ToString() + "\tBaselineSystem", i + 1);
                            control = false;
                        }
                    }
                }
            }
        }

        public static void ViewData(int limit, List<Dictionary<string, string>> resultList)
        {
            //xx.ResultListView.Items.Clear();
            //xx.ResultListView.Controls.Clear();
            //for (int i = limit; i < limit + 10; i++)     // Concatenate the top 10 result strings
            //{ 
            //        ListViewItem resultView = new ListViewItem(new[] { resultList[i]["id"], resultList[i]["title"], resultList[i]["author"], resultList[i]["biblio"], resultList[i]["abstract"] });
            //        xx.ResultListView.Items.Add(resultView);
                    //string[] parts = resultSet[i][0].Split(new string[] { ".W\n" }, StringSplitOptions.RemoveEmptyEntries);   // Cut half the texts from the starting of .W
                    //string firsthalf = parts[0].Replace(".I ", "DocID: ").Replace(".T\n", "\r\nTitle: ").Replace(".A\n", "\r\nAuthor: ").Replace(".B\n", "\r\nBibliographic information: ");  // First half
                    //string secondhalf = parts[1].Replace("\n", " ");  // Replace abstract LF

                    //if (first)  // For only first sentence of abstract
                    //{
                    //    Regex rx = new Regex("^.*?[.?!]", RegexOptions.Compiled | RegexOptions.IgnoreCase);     // Set the RE to match first sentence of abstract
                    //    MatchCollection matches = rx.Matches(secondhalf);   // Get RE match
                    //    outp += "Rank: " + (i + 1).ToString() + "\r\n" + firsthalf + "\r\nAbstract: " + matches[0].Value + "\r\n\r\n";   // Combine displaying texts
                    //}
                    //else
                    //{
                    //    outp += "Rank: " + (i + 1).ToString() + "\r\n" + firsthalf + "\r\nAbstract: " + secondhalf + "\r\n\r\n";    // Combine original texts
                    //}
        }
        

        public static string ViewData_bk(int limit, List<List<string>> resultSet, bool first)
        {
            string outp = "";       // Initial null string

            for (int i = limit; i < limit + 10; i++)     // Concatenate the top 10 result strings
            {
                string[] parts = resultSet[i][0].Split(new string[] { ".W\n" }, StringSplitOptions.RemoveEmptyEntries);   // Cut half the texts from the starting of .W
                string firsthalf = parts[0].Replace(".I ", "DocID: ").Replace(".T\n", "\r\nTitle: ").Replace(".A\n", "\r\nAuthor: ").Replace(".B\n", "\r\nBibliographic information: ");  // First half
                string secondhalf = parts[1].Replace("\n", " ");  // Replace abstract LF

                if (first)  // For only first sentence of abstract
                {
                    Regex rx = new Regex("^.*?[.?!]", RegexOptions.Compiled | RegexOptions.IgnoreCase);     // Set the RE to match first sentence of abstract
                    MatchCollection matches = rx.Matches(secondhalf);   // Get RE match
                    outp += "Rank: " + (i + 1).ToString() + "\r\n" + firsthalf + "\r\nAbstract: " + matches[0].Value + "\r\n\r\n";   // Combine displaying texts
                }
                else
                {
                    outp += "Rank: " + (i + 1).ToString() + "\r\n" + firsthalf + "\r\nAbstract: " + secondhalf + "\r\n\r\n";    // Combine original texts
                }
            }
            return outp;
        }
        public static void SaveClick(List<string> resultList, StreamWriter writer)
        {
            

            for (int i = 0; i < 40; i++)
            {
               

                writer.WriteLine("Rank" + (i + 1));
                writer.WriteLine(resultList[i ]);

            }

            writer.Dispose();

            writer.Close();
        }

        public static Dictionary<string, string> ReadCranNeeds(string path)    // Load cran_information_need into a dictionary
        {
            Dictionary<string, string> dic = new Dictionary<string, string>(); // Create a dictionary
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string text = reader.ReadToEnd();   // Read the whole text file
                    string[] sub = text.Split(new string[] { ".I " }, StringSplitOptions.RemoveEmptyEntries);   // Split by ".I "

                    foreach (string need in sub)       // Loop through each query
                    {
                        int indexD = text.IndexOf(".D\n");   // Get Description starting index                       
                        dic.Add(need.Substring(0, indexD - 4), need.Substring(indexD).TrimEnd('\n'));     // Add ID and Description into dictionary as pairs
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("couldn't read");
                Console.WriteLine(e.Message);
            }
            return dic;
        }
    }
}
