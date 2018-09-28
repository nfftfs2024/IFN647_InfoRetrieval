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
            analyzer = new Lucene.Net.Analysis.Snowball.SnowballAnalyzer(VERSION, "English");      // Stop analyzer takes 
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
            int rank = 0;
            foreach (ScoreDoc scoreDoc in results.ScoreDocs)
            {
                rank++;
                Lucene.Net.Documents.Document doc = searcher.Doc(scoreDoc.Doc);
                Lucene.Net.Search.Explanation exp = searcher.Explain(query, scoreDoc.Doc);
                string myFieldValue = doc.Get(TEXT_FN).ToString();
                Console.WriteLine("Rank " + rank + " text " + myFieldValue + "\n" + exp.ToString());

            }


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

                    //Console.WriteLine(text);
                    Lucene.Net.Documents.Field field = new Field(TEXT_FN, text, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES);
                    Lucene.Net.Documents.Document doc = new Document();
                    doc.Add(field);
                    writer.AddDocument(doc);
                }

            }

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

            DateTime start = System.DateTime.Now;   // Indexing time starts
            myLuceneApp.CreateIndex(indexPath);     // Create index at the given path
            System.Console.WriteLine("Adding Documents to Index");
            myLuceneApp.IndexText(sourcePath);      // Add file collection to the index one by one
            myLuceneApp.CleanUpIndexer();           // Flush the buffer and close the index
            Console.WriteLine("All documents added.");
            DateTime end = System.DateTime.Now;   // Indexing time ends
            Console.WriteLine("The time for creating index was " + (end - start));  // Calculate and show the indexing time


            //// Searching Code
            //myLuceneApp.CreateSearcher();

            //myLuceneApp.SearchText("mad");

            //myLuceneApp.CleanUpSearcher();


            Console.ReadLine();
        }
    }
}
