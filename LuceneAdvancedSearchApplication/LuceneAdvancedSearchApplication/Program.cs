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
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUIForm());
        }


        public static void BuildIndex_Click(string sourcePath, string indexPath, LuceneSearcheEngine myLuceneApp)
        {
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

        public static List<List<string>> Search_Click(string querytext, LuceneSearcheEngine myLuceneApp)
        {
            List<List<string>> resultList = new List<List<string>>();
            //// Searching Code
            myLuceneApp.CreateSearcher();           // Create searcher
            resultList = myLuceneApp.SearchText(querytext);     // Get search result list
            myLuceneApp.CleanUpSearcher();        // Clean searcher
            return resultList;
        }

        public static string ViewData(int limit, List<List<string>> resultSet, bool first)
        {
            string outp = "";       // Initial null string

            for (int i = limit; i < limit + 10; i++)     // Concatenate the top 10 result strings
            {
                //Console.WriteLine(String.Format("text: {0}\n\nscore: {1}", resultSet[i][0], resultSet[i][1]));
                if (first)  // For only first sentence of abstract
                {
                    Regex rx = new Regex("Abstract:.*?[.?!]", RegexOptions.Compiled | RegexOptions.IgnoreCase);     // Set the RE to match first sentence of abstract
                    MatchCollection matches = rx.Matches(resultSet[i][0]);   // Get RE match
                    outp += "Rank: " + (i + 1).ToString() + "\r\n" + resultSet[i][0].Substring(0, resultSet[i][0].LastIndexOf("Abstract: ")) + matches[0].Value + "\r\n\r\n";   // Combine displaying texts
                }
                else
                {
                    outp += "Rank: " + (i + 1).ToString() + "\r\n" + resultSet[i][0] + "\r\n\r\n";    // Combine original texts
                }
            }
            return outp;
        }

        public static void SaveClick(List<string> resultList, StreamWriter writer)
        {
            

            for (int i = 0; i < 40; i++)
            {
               

                writer.WriteLine("Rank" + (i + 1));
                writer.WriteLine(resultList[i]);

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
                        int indexD = text.IndexOf(".D\r\n");   // Get Description starting index                       
                        dic.Add(need.Substring(0, indexD - 5), need.Substring(indexD + 1).TrimEnd('\r', '\n'));     // Add ID and Description into dictionary as pairs
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
