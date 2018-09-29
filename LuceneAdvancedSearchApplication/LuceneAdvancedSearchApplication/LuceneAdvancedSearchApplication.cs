using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis; // for Analyser
using Lucene.Net.Documents; // for Document and Field
using Lucene.Net.Index; //for Index Writer
using Lucene.Net.Store; //for Directory
using Lucene.Net.Search; // for IndexSearcher
using Lucene.Net.QueryParsers;  // for QueryParser
using Lucene.Net.Analysis.Snowball; //hahahahaha
using System.IO;

namespace LuceneAdvancedSearchApplication
{
    class LuceneAdvancedSearchApplication
    {
        Lucene.Net.Store.Directory luceneIndexDirectory;
        Lucene.Net.Analysis.Analyzer analyzer;
        Lucene.Net.Index.IndexWriter writer;
        IndexSearcher searcher;
        QueryParser parser;
        Lucene.Net.Search.Similarity newSimilarity;

        const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;
        const string TEXT_FN = "Text";


        public LuceneAdvancedSearchApplication()
        {
            luceneIndexDirectory = null;
            writer = null;
            analyzer = new Lucene.Net.Analysis.SimpleAnalyzer();      // Stop analyzer takes 
            parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, TEXT_FN, analyzer);
            newSimilarity = new NewSimilarity();

        }
        /// <summary>
        /// Creates the index at a given path
        /// </summary>
        /// <param name="indexPath">The pathname to create the index</param>
        public void CreateIndex(string indexPath)
        {

            luceneIndexDirectory = Lucene.Net.Store.FSDirectory.Open(indexPath);
            IndexWriter.MaxFieldLength mfl = new IndexWriter.MaxFieldLength(IndexWriter.DEFAULT_MAX_FIELD_LENGTH);
            // writer = new Lucene.Net.Index.IndexWriter(luceneIndexDirectory, analyzer, true, mfl);
            writer = new Lucene.Net.Index.IndexWriter(luceneIndexDirectory, analyzer, true, mfl);
            //writer.SetSimilarity(newSimilarity);
        }

        /// <summary>
        /// Flushes the buffer and closes the index
        /// </summary>
        public void CleanUpIndexer()
        {
            writer.Optimize();
            writer.Flush(true, true, true);
            writer.Dispose();
        }


        /// <summary>
        /// Creates the searcher object
        /// </summary>
        public void CreateSearcher()
        {
            searcher = new IndexSearcher(luceneIndexDirectory);
            //searcher.Similarity = newSimilarity;
        }

        /// <summary>
        /// Searches the index for the querytext
        /// </summary>
        /// <param name="querytext">The text to search the index</param>
        public void SearchText(string querytext)
        {

            System.Console.WriteLine("Searching for " + querytext);
            querytext = querytext.ToLower();
            Query query = parser.Parse(querytext);

            TopDocs results = searcher.Search(query, 100);
            System.Console.WriteLine("Number of results is " + results.TotalHits);
            //int rank = 0;
            //foreach (ScoreDoc scoreDoc in results.ScoreDocs)
            //{
            //    rank++;
            //    Lucene.Net.Documents.Document doc = searcher.Doc(scoreDoc.Doc);
            //    Lucene.Net.Search.Explanation exp = searcher.Explain(query, scoreDoc.Doc);
            //    string myFieldValue = doc.Get("words").ToString();
            //    Console.WriteLine("Rank " + rank + " text " + myFieldValue + "\n" + exp.ToString());

            //}


        }

        /// <summary>
        /// Closes the index after searching
        /// </summary>
        public void CleanUpSearcher()
        {
            searcher.Dispose();
        }
        //public void IndexText(string text)
        //{

        //    Lucene.Net.Documents.Field field = new Field(TEXT_FN, text, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES);
        //    Lucene.Net.Documents.Document doc = new Document();
        //    doc.Add(field);
        //    writer.AddDocument(doc);
        //}

        /// <summary>
        /// Indexes a given string into the index
        /// </summary>
        /// <param name="text">The text to index</param>
        public void IndexText(string path)
        {
            System.IO.DirectoryInfo root = new System.IO.DirectoryInfo(path);   // Create DirectoryInfo object
            System.IO.FileInfo[] files = null;  // Create FileInfo array

            // Get all files in the directory
            try
            {
                files = root.GetFiles("*.*");
            }

            catch (UnauthorizedAccessException e)
            {
                System.Console.WriteLine(e.Message);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                //Console.WriteLine(files.Length);
                //Process every file
                foreach (System.IO.FileInfo fi in files)
                {
                    string name = fi.FullName;    // Get file name
                    Console.WriteLine("Adding doc " + name + " to Index");

                    StreamReader reader = new StreamReader(name);   // Create a reader
                    string text = reader.ReadToEnd();   // Read the whole text

                    int indexI = text.IndexOf(".I ") + 3;   // Get ID starting index
                    int indexT = text.IndexOf(".T\r\n");    // Get title starting index
                    int indexA = text.IndexOf(".A\r\n");    // Get author starting index
                    int indexB = text.IndexOf(".B\r\n");    // Get bibliography starting index
                    int indexW = text.IndexOf(".W\r\n");    // Get words starting index

                    //Console.WriteLine(text.Substring(indexI, indexT-2-indexI));
                    //Console.WriteLine(text.Substring(indexT + 4, ((indexA - 2 - (indexT + 4)) > 0) ? (indexA - 2 - (indexA + 4)) : 0));
                    //Console.WriteLine(text.Substring(indexA + 4, ((indexB - 2 - (indexA + 4)) > 0) ? (indexB - 2 - (indexA + 4)) : 0));
                    //Console.WriteLine(text.Substring(indexB + 4, ((indexW - 2 - (indexB + 4)) > 0) ? (indexW - 2 - (indexB + 4)) : 0));
                    //Console.WriteLine(text.Substring(indexW + 4, text.Length - 6 - indexW));

                    string id = text.Substring(indexI, indexT - 2 - indexI);    // Get ID string
                    string title = text.Substring(indexT + 4, ((indexA - 2 - (indexT + 4)) > 0) ? (indexA - 2 - (indexT + 4)) : 0);     // Get title string
                    string author = text.Substring(indexA + 4, ((indexB - 2 - (indexA + 4)) > 0) ? (indexB - 2 - (indexA + 4)) : 0);    // Get author string
                    string biblio = text.Substring(indexB + 4, ((indexW - 2 - (indexB + 4)) > 0) ? (indexW - 2 - (indexB + 4)) : 0);    // Get bibliography string
                    string words = text.Substring(indexW + 4, ((text.Length - 2 - (indexW + 4)) > 0) ? (text.Length - 2 - (indexW + 4)) : 0);   // Get words string

                    Lucene.Net.Documents.Document doc = new Document();     // Create document
                    // Add 5 fields to the document
                    //doc.Add(new Lucene.Net.Documents.Field("id", id, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
                    //doc.Add(new Lucene.Net.Documents.Field("title", title, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
                    //doc.Add(new Lucene.Net.Documents.Field("author", author, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
                    //doc.Add(new Lucene.Net.Documents.Field("bibliography", biblio, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
                    //doc.Add(new Lucene.Net.Documents.Field("words", words, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
                    doc.Add(new Lucene.Net.Documents.Field(TEXT_FN, text, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

                    writer.AddDocument(doc);    // Add document
                }
            }
        } 


        public Dictionary<string, string> ReadCranNeeds(string path)    // Load cran_information_need into a dictionary
        {
            Dictionary <string, string> dic = new Dictionary<string, string>(); // Create a dictionary
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string text = reader.ReadToEnd();   // Read the whole text file
                    string[] sub = text.Split(new string[] { ".I " }, StringSplitOptions.RemoveEmptyEntries);   // Split by ".I "

                    foreach (string need in sub)       // Loop through each query
                    {
                        int indexD = text.IndexOf(".D\r\n");   // Get Description starting index
                        //Console.WriteLine(indexD);
                        //Console.WriteLine(need.Substring(0, indexD - 5));
                        //Console.WriteLine(need.Substring(indexD + 1));
                        dic.Add(need.Substring(0, indexD - 5), need.Substring(indexD + 1).TrimEnd('\r', '\n'));     // Add ID and Description into dictionary as pairs
                    }

                    //Console.WriteLine(dic["001"]);
                    //foreach (KeyValuePair<string, string> kvp in dic)
                    //{
                    //    //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                    //    Console.WriteLine(kvp.Key);
                    //    Console.WriteLine(kvp.Value);
                    //}
                    //foreach (string i in sub)
                    //{
                    //    Console.WriteLine(i);
                    //}

                    //int indexI = text.IndexOf(".I ") + 3;   // Get ID starting index
                    //int indexT = text.IndexOf(".T\r\n");    // Get title starting index
                    //int indexA = text.IndexOf(".A\r\n");    // Get author starting index
                    //int indexB = text.IndexOf(".B\r\n");    // Get bibliography starting index
                    //int indexW = text.IndexOf(".W\r\n");    // Get words starting index

                    ////Console.WriteLine(text.Substring(indexI, indexT-2-indexI));
                    ////Console.WriteLine(text.Substring(indexT + 4, ((indexA - 2 - (indexT + 4)) > 0) ? (indexA - 2 - (indexA + 4)) : 0));
                    ////Console.WriteLine(text.Substring(indexA + 4, ((indexB - 2 - (indexA + 4)) > 0) ? (indexB - 2 - (indexA + 4)) : 0));
                    ////Console.WriteLine(text.Substring(indexB + 4, ((indexW - 2 - (indexB + 4)) > 0) ? (indexW - 2 - (indexB + 4)) : 0));
                    ////Console.WriteLine(text.Substring(indexW + 4, text.Length - 6 - indexW));

                    //string id = text.Substring(indexI, indexT - 2 - indexI);    // Get ID string
                    //string title = text.Substring(indexT + 4, ((indexA - 2 - (indexT + 4)) > 0) ? (indexA - 2 - (indexT + 4)) : 0);     // Get title string
                    //string author = text.Substring(indexA + 4, ((indexB - 2 - (indexA + 4)) > 0) ? (indexB - 2 - (indexA + 4)) : 0);    // Get author string
                    //string biblio = text.Substring(indexB + 4, ((indexW - 2 - (indexB + 4)) > 0) ? (indexW - 2 - (indexB + 4)) : 0);    // Get bibliography string
                    //string words = text.Substring(indexW + 4, ((text.Length - 2 - (indexW + 4)) > 0) ? (text.Length - 2 - (indexW + 4)) : 0);   // Get words string

                    //Console.WriteLine(line);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("couldn't read");
                Console.WriteLine(e.Message);
            }
            return dic;
        }


        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello Lucene.Net");

            LuceneAdvancedSearchApplication myLuceneApp = new LuceneAdvancedSearchApplication();
            // source collection
            //List<string> l = new List<string>();
            //l.Add("The magical world of oz");
            //l.Add("The mad, mad, mad, mad world");
            //l.Add("Possum magic");
            //l.Add("Mad isn't bad");
            //l.Add("Mad's greatest hits");

            // Index code
            string indexPath = @"C:\LuceneFolder";
            string sourcePath = @"D:\Desktop\ifn647-project\LuceneAdvancedSearchApplication\crandocs";
            string needsPath = @"D:\Desktop\ifn647-project\LuceneAdvancedSearchApplication\cran_information_needs.txt";

            TextAnalyser.TextAnalyser textAnalyser = new TextAnalyser.TextAnalyser();
            DateTime start = System.DateTime.Now;   // Indexing time starts
            myLuceneApp.CreateIndex(indexPath);     // Create index at the given path
            System.Console.WriteLine("Adding Documents to Index");
            myLuceneApp.IndexText(sourcePath);      // Add file collection to the index one by one
            myLuceneApp.CleanUpIndexer();           // Flush the buffer and close the index
            Console.WriteLine("All documents added.");
            DateTime end = System.DateTime.Now;   // Indexing time ends
            Console.WriteLine("The time for creating index was " + (end - start));  // Calculate and show the indexing time

            Dictionary <string, string> cranNeeds = myLuceneApp.ReadCranNeeds(needsPath);   // Put the cran_information_need into a dictionary
            //Console.WriteLine(cranNeeds["001"]);

            //// Searching Code
            start = System.DateTime.Now;   //Searching time starts
            myLuceneApp.CreateSearcher();
            //foreach(KeyValuePair<string, string> entry in cranNeeds)
            //{
            //    myLuceneApp.SearchText(entry.Value);

            //}
            myLuceneApp.SearchText(cranNeeds["001"]);
            myLuceneApp.CleanUpSearcher();
            end = System.DateTime.Now;   // Searching time starts
            Console.WriteLine("The time for creating index was " + (end - start));  // Calculate and show the searching time
            Console.ReadLine();
        }
    }
}
