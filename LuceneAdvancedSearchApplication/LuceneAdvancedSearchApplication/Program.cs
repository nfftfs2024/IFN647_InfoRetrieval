using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Syn.WordNet;

namespace LuceneAdvancedSearchApplication
{
    static class Program
    {

        public static LuceneSearcheEngine myLuceneApp;      // Set publicly callable LuceneSearchEngine object
        public static PorterStemmer myStemmer;              // Set publicly callable PorterStemmer object
        public static WordNetEngine wordNet;                // Set WordNet object
        //public static Dictionary<string, string[]> thesaurus;   // Set thesaurus dictionary

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BuildIndexGUIForm());

            var directory = System.IO.Directory.GetCurrentDirectory() + "\\wordnet";        // Set WordNet directory
            wordNet = new WordNetEngine();  // Initiate WordNet object

            // Load WordNet data and index
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.adj")), PartOfSpeech.Adjective);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.adv")), PartOfSpeech.Adverb);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.noun")), PartOfSpeech.Noun);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.verb")), PartOfSpeech.Verb);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.adj")), PartOfSpeech.Adjective);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.adv")), PartOfSpeech.Adverb);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.noun")), PartOfSpeech.Noun);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.verb")), PartOfSpeech.Verb);
            Console.WriteLine("Loading WordNet database...");
            wordNet.Load();
            Console.WriteLine("Load completed.");

            Application.Run(new SearchGUIForm());
        }


        public static void BuildIndex_Click(string sourcePath, string indexPath)
        {
            myLuceneApp = new LuceneSearcheEngine();    // Initiate LuceneSearchEngine object
            myStemmer = new PorterStemmer();            // Initiate PorterStemmer object
            //thesaurus = myLuceneApp.CreateThesaurus();  // Get thesaurus dictionary

            DateTime start = System.DateTime.Now;   // Indexing time starts
            myLuceneApp.CreateIndex(indexPath);     // Create index at the given path
            System.Console.WriteLine("Adding Documents to Index");
            myLuceneApp.IndexText(sourcePath);      // Add file collection to the index one by one
            System.Console.WriteLine("All documents added.");
            myLuceneApp.CleanUpIndexer();       // Clean up indexer
            DateTime end = System.DateTime.Now;   // Indexing time ends
            MessageBox.Show("The time for indexing text was " + (end - start).TotalMilliseconds + " milliseconds", "Reporting Indexing Time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        public static List<Dictionary<string, string>> Search_Click(string querytext, bool asIsCheckBox, bool QECheckbox, bool advCheckBox, string cond, out string finalQueryTxt)
        {
            List<List<string>> tempList = new List<List<string>>();     // Create a list of lists for receiving output from SearchText method
            List<Dictionary<string, string>> resultListDict = new List<Dictionary<string, string>>();   // Create a list of dictionaries for outputting to GUI


            myLuceneApp.CreateSearcher();           // Create searcher

            if (QECheckbox)
            {
                if(advCheckBox)
                {
                    int indexT = querytext.IndexOf("Title:");  // Get again the index just in case it has been changed
                    int indexA = querytext.IndexOf("Author:");  // Get again the index just in case it has been changed
                    if (indexT != -1 && indexA != -1)
                    {
                        string title_query = querytext.Substring(indexT + 6, ((indexA - 1 - (indexT + 6)) > 0) ? (indexA - 1 - (indexT + 6)) : 0);     // Get title string
                        string author_query = querytext.Substring(indexA + 7, ((querytext.Length - (indexA + 7)) > 0) ? (querytext.Length - (indexA + 7)) : 0);    // Get author string
                        string querytitle = myLuceneApp.PreProcess(myStemmer, title_query);       // Pre-process query texts if checked
                        string queryauthor = myLuceneApp.PreProcess(myStemmer, author_query);       // Pre-process query texts if checked
                        querytext = "Title:" + querytitle + " Author:" + queryauthor;
                    }//When both the title and author are queried
                    if (indexT == -1)
                    {
                        string author_query = querytext.Substring(indexA + 7, ((querytext.Length - (indexA + 7)) > 0) ? (querytext.Length - (indexA + 7)) : 0);    // Get author string
                        string queryauthor = myLuceneApp.PreProcess(myStemmer, author_query);       // Pre-process query texts if checked
                        querytext = "Author:" + queryauthor;
                    }//When only the Author is queried

                    if (indexA == -1)
                    {
                        string title_query = querytext.Substring(indexT + 6, ((querytext.Length - (indexT + 6)) > 0) ? (querytext.Length - (indexT + 6)) : 0);     // Get title string
                        string querytitle = myLuceneApp.PreProcess(myStemmer, title_query);       // Pre-process query texts if checked
                        querytext = "Title:" + querytitle;
                    }//When only the title is queried               
                }
                else
                {
                    querytext = querytext.Replace("Title:", "").Replace("Author:", "");
                    querytext = myLuceneApp.PreProcess(myStemmer, querytext);       // Pre-process query texts if checked
                }
                
            }

            tempList = myLuceneApp.SearchText(querytext, asIsCheckBox, QECheckbox, advCheckBox, cond, out finalQueryTxt);     // Get search result list of lists
            myLuceneApp.CleanUpSearcher();        // Clean searcher

            int rank = 0;
            foreach (List<string> result in tempList)   // Go through each resulting document
            {
                rank++;

                //Retrieving values indexed as TEXT
                string text = result[0];    // Get whole text from input list
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
                

                //Handling issues in the abstract to display it
                abst = abst.Replace("\n", " ");  // Replace abstract LF
                Regex rx = new Regex("^.*?[.?!]", RegexOptions.Compiled | RegexOptions.IgnoreCase);     // Set the RE to match first sentence of abstract
                MatchCollection abst_first = rx.Matches(abst);   // Get RE match for first sentence of abstract
                string abst_fir = "";   // Create first sentence variable
                if (abst_first.Count != 0)  // Check if there is no RE match (usually abstract is empty)
                    abst_fir = abst_first[0].Value;
               
                                
                // Add everything into the created list of dictionaries
                resultListDict.Add(new Dictionary<string, string> {{"rank", rank.ToString()}, {"id", id}, {"title", title}, {"author", author},
                    { "biblio", biblio}, {"abstract", abst}, {"abstract_first", abst_fir}, {"score", result[1]}});     
            }
            return resultListDict;
        }

        public static void Create_BaseLine_Results(Dictionary<string, string> cNeeds)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\BaseLineResults.txt";
            List<string> resultListDict = new List<string>();
            List<float> valueListBase = new List<float>();
            List<string> docsIdsListBase = new List<string>();
            //// Searching Code
            myLuceneApp.CreateSearcher();           // Create searcher
            bool control = true;
            foreach (string key in cNeeds.Keys)
            {
                string querytext = myLuceneApp.PreProcess(myStemmer, cNeeds[key]);
                Tuple<List<float>, List<string>,int> result = myLuceneApp.SearchText_baseline(querytext);
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

        public static void SaveClick(List<Dictionary<string,string>> resultListDict, StreamWriter writer, int queryCount)
        {
            

            for (int i = 0; i < resultListDict.Count; i++)
            {
               

                writer.WriteLine(queryCount.ToString()+ "\tQ1"+ "\t" + resultListDict[i]["id"] + "\t" + (i + 1)+  "\t"+resultListDict[i]["score"]+"    \tBaselineSystem");
                

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
                    //Verification of structure in Cran_needs document
                    string text = reader.ReadToEnd();   // Read the whole text file

                    Regex rxi = new Regex(".I ", RegexOptions.Compiled);     // Set the RE to match first sentence of abstract
                    Regex rxd = new Regex(".D\n", RegexOptions.Compiled);
                    MatchCollection abst_i = rxi.Matches(text);
                    MatchCollection abst_d = rxd.Matches(text);

                    if (abst_i.Count > 0 && abst_d.Count > 0 && abst_i.Count == abst_d.Count)
                    {
                        string[] sub = text.Split(new string[] { ".I " }, StringSplitOptions.RemoveEmptyEntries);   // Split by ".I "
                        foreach (string need in sub)       // Loop through each query
                        {
                            int indexD = text.IndexOf(".D\n");   // Get Description starting index
                            dic.Add(need.Substring(0, indexD - 4), need.Substring(indexD).Replace("\n", " ").TrimEnd('\n'));     // Add ID and Description into dictionary as pairs
                        }
                        reader.Close();
                    }
                    if (abst_i.Count == 0 || abst_d.Count == 0)
                    {
                        MessageBox.Show("The file used does not contain the correct indicators .I and .D to identify the queries\nPlease Select a different file", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (abst_i.Count != abst_d.Count)
                    {
                        MessageBox.Show("The file used does not have the same number of ID queries than texts to be searched\nPlease Select a different file", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        public static Tuple<string, string> Query_Simple_Preprocessing_advanced_Search(string querytext)
        {
            string title_query = "";
            string author_query = "";
            int indexT = querytext.IndexOf("Title:");  // Get again the index just in case it has been changed
            int indexA = querytext.IndexOf("Author:");  // Get again the index just in case it has been changed
            if (indexT != -1 && indexA != -1)
            {
                title_query = querytext.Substring(indexT + 6, ((indexA - 1 - (indexT + 6)) > 0) ? (indexA - 1 - (indexT + 6)) : 0);     // Get title string
                author_query = querytext.Substring(indexA + 7, ((querytext.Length - (indexA + 7)) > 0) ? (querytext.Length - (indexA + 7)) : 0);    // Get author string
            }//When both the title and author are queried
            else
            {
                if (indexT == -1)
                {
                    author_query = querytext.Substring(indexA + 7, ((querytext.Length - (indexA + 7)) > 0) ? (querytext.Length - (indexA + 7)) : 0);    // Get author string
                }//When only the Author is queried
                else
                {
                    title_query = querytext.Substring(indexT + 6, ((querytext.Length - (indexT + 6)) > 0) ? (querytext.Length - (indexT + 6)) : 0);     // Get title string
                }
            }
            var finalAdvQuery = Tuple.Create(title_query, author_query);
            return finalAdvQuery;
        }

    }
}
