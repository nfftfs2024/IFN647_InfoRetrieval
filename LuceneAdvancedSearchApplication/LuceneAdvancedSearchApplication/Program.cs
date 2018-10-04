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

        public static string ViewData(int limit, List<List<string>> resultSet, bool first)
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
