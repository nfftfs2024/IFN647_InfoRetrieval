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
//using Lucene.Net.Analysis.Snowball;
using System.IO;
//using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace LuceneAdvancedSearchApplication
{
    class LuceneSearcheEngine
    {
        Lucene.Net.Store.Directory luceneIndexDirectory;    // Create directory object
        Lucene.Net.Analysis.Analyzer analyzer;              // Create analyzer object
        Lucene.Net.Index.IndexWriter writer;                // Create index writer object
        IndexSearcher searcher;                             // Create searcher object
        QueryParser parser;                                 // Create parser object

        //Lucene.Net.Search.Similarity newSimilarity;   // for similarity measure

        const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;  // Lucene version 
        const string TEXT_FN = "Text";                                              // Lucene field "Text"


        public LuceneSearcheEngine()
        {
            luceneIndexDirectory = null;
            writer = null;
            analyzer = new Lucene.Net.Analysis.SimpleAnalyzer();      // Using simple analyzer for baseline system 
            parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, TEXT_FN, analyzer);
            //newSimilarity = new NewSimilarity();
        }

        /// <summary>
        /// Creates the index at a given path
        /// </summary>
        /// <param name="indexPath">The pathname to create the index</param>
        public void CreateIndex(string indexPath)
        {

            luceneIndexDirectory = Lucene.Net.Store.FSDirectory.Open(indexPath);
            IndexWriter.MaxFieldLength mfl = new IndexWriter.MaxFieldLength(IndexWriter.DEFAULT_MAX_FIELD_LENGTH);
            writer = new Lucene.Net.Index.IndexWriter(luceneIndexDirectory, analyzer, true, mfl);

            //writer.SetSimilarity(newSimilarity);  // for similarity measure
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
        public List<List<string>> SearchText(string querytext)
        {
            List<List<string>> resultList = new List<List<string>>() ;
            System.Console.WriteLine("Searching for " + querytext);
            querytext = querytext.ToLower();
            Query query = parser.Parse(querytext);

            TopDocs results = searcher.Search(query, 100);
            System.Console.WriteLine("Number of results is " + results.TotalHits);

            if (results.TotalHits != 0)     // Check if there are found results
            {
                for (int i = 0; i < 40; i++)    // Loop through the top 10 ranked documents
                {
                    int rank = i + 1;   // Set ranking number
                    ScoreDoc scoreDoc = results.ScoreDocs[i];   // Get the ranked document
                    Lucene.Net.Documents.Document doc = searcher.Doc(scoreDoc.Doc);     // Get document contents
                    string myFieldValue = doc.Get(TEXT_FN).ToString();  // Get document contents by fields                                               
                   
                    string[] parts = myFieldValue.Split(new string[] { ".W\r\n" }, StringSplitOptions.RemoveEmptyEntries);   // Cut half the texts from the starting of .W
                    string firsthalf = parts[0].Replace(".I ", "DocID: ").Replace(".T\r\n", "Title: ").Replace(".A\r\n", "Author: ").Replace(".B\r\n", "Bibliographic information: ");  // First half
                                                                                                                                                                                       
                    
                    string secondhalf = parts[1].Replace("\r\n", " ");  // Replace abstract CRLF
                    resultList.Add(new List<string> { firsthalf + "Abstract: " + secondhalf + "\n\n", scoreDoc.Score.ToString() });     // Combine texts
                                        
                }
            }
            
            return resultList;

        }

        /// <summary>
        /// Closes the index after searching
        /// </summary>
        public void CleanUpSearcher()
        {
            searcher.Dispose();
        }
       

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

                  
                    string id = text.Substring(indexI, indexT - 2 - indexI);    // Get ID string
                    string title = text.Substring(indexT + 4, ((indexA - 2 - (indexT + 4)) > 0) ? (indexA - 2 - (indexT + 4)) : 0);     // Get title string
                    string author = text.Substring(indexA + 4, ((indexB - 2 - (indexA + 4)) > 0) ? (indexB - 2 - (indexA + 4)) : 0);    // Get author string
                    string biblio = text.Substring(indexB + 4, ((indexW - 2 - (indexB + 4)) > 0) ? (indexW - 2 - (indexB + 4)) : 0);    // Get bibliography string
                    string words = text.Substring(indexW + 4, ((text.Length - 2 - (indexW + 4)) > 0) ? (text.Length - 2 - (indexW + 4)) : 0);   // Get words string

                    Lucene.Net.Documents.Document doc = new Document();     // Create document
                   
                    doc.Add(new Lucene.Net.Documents.Field(TEXT_FN, text, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.NO));

                    writer.AddDocument(doc);    // Add document
                }
            }
        }


        
      
    }
}
