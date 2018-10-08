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
using System.IO;
using System.Text.RegularExpressions;

namespace LuceneAdvancedSearchApplication
{
    class LuceneSearcheEngine
    {
        Lucene.Net.Store.Directory luceneIndexDirectory;    // Create directory object
        Lucene.Net.Analysis.Analyzer analyzer;              // Create analyzer object
        Lucene.Net.Analysis.Analyzer analyzerAsIs;             // Create analyzer to as-is searching
        Lucene.Net.Index.IndexWriter writer;                // Create index writer object
        IndexSearcher searcher;                             // Create searcher object
        IndexSearcher searcher2;                            // Create a searcher for the Baseline.
        QueryParser parser;                                 // Create parser object
        QueryParser parserAsIs;

        //Lucene.Net.Search.Similarity newSimilarity;   // for similarity measure

        const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;  // Lucene version 
        const string TEXT_FN = "Text";                                              // Lucene field "Text"


        public LuceneSearcheEngine()
        {
            luceneIndexDirectory = null;
            writer = null;
            analyzer = new Lucene.Net.Analysis.SimpleAnalyzer();      // Using simple analyzer for baseline system 
            analyzerAsIs = new Lucene.Net.Analysis.KeywordAnalyzer();      // Using keyword analyzer for query as-is
            parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, TEXT_FN, analyzer);
            parserAsIs = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, TEXT_FN, analyzerAsIs);
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

                    Lucene.Net.Documents.Document doc = new Document();     // Create document
                    doc.Add(new Lucene.Net.Documents.Field(TEXT_FN, text, Field.Store.YES, Field.Index.ANALYZED_NO_NORMS, Field.TermVector.NO));
                    writer.AddDocument(doc);    // Add document

                    // For later advanced features (Task 7)
                    int indexI = text.IndexOf(".I ") + 3;   // Get ID starting index
                    int indexT = text.IndexOf(".T\n");    // Get title starting index
                    int indexA = text.IndexOf(".A\n");    // Get author starting index
                    int indexB = text.IndexOf(".B\n");    // Get bibliography starting index
                    int indexW = text.IndexOf(".W\n");    // Get words starting index
                    string id = text.Substring(indexI, indexT - 1 - indexI);    // Get ID string
                    string title = text.Substring(indexT + 3, ((indexA - 1 - (indexT + 3)) > 0) ? (indexA - 1 - (indexT + 3)) : 0);     // Get title string
                    string author = text.Substring(indexA + 3, ((indexB - 1 - (indexA + 3)) > 0) ? (indexB - 1 - (indexA + 3)) : 0);    // Get author string
                    string biblio = text.Substring(indexB + 3, ((indexW - 1 - (indexB + 3)) > 0) ? (indexW - 1 - (indexB + 3)) : 0);    // Get bibliography string
                    string words = text.Substring(indexW + 3, ((text.Length - 1 - (indexW + 3)) > 0) ? (text.Length - 1 - (indexW + 3)) : 0);   // Get words string

                    reader.Close();
                }
            }
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
            searcher2 = new IndexSearcher(luceneIndexDirectory);
            //searcher.Similarity = newSimilarity;
        }


        /// <summary>
        /// Searches the index for the querytext
        /// </summary>
        /// <param name="querytext">The text to search the index</param>
        public List<List<string>> SearchText(string querytext, bool asIsCheckBox, out string finalQueryTxt)
        {
            List<List<string>> resultListDict = new List<List<string>>();      // Initiate a result list
            System.Console.WriteLine("Searching for " + querytext);
            querytext = querytext.ToLower();
            Console.WriteLine("The value of the boolean is {0}", asIsCheckBox);
            if (asIsCheckBox == true)
            {
                Console.WriteLine("Is accesing");
                Query query = parserAsIs.Parse(querytext);      // Parse the query text by parser and create the query object
                finalQueryTxt = query.ToString();           // Assign processed query text to final query text variable
                TopDocs results = searcher.Search(query, 10000);                  // Search the query
                System.Console.WriteLine("Number of results is " + results.TotalHits);
                if (results.TotalHits != 0)     // Check if there are found results
                {
                    for (int i = 0; i < results.TotalHits; i++)    // Loop through the top 10 ranked documents
                    {
                        ScoreDoc scoreDoc = results.ScoreDocs[i];   // Get the ranked document
                        Lucene.Net.Documents.Document doc = searcher.Doc(scoreDoc.Doc);     // Get document contents
                        string text = doc.Get(TEXT_FN).ToString();  // Get document contents by fields    
                        string score = scoreDoc.Score.ToString();   // Get document score
                        resultListDict.Add(new List<string> { text, score });     // Add contents and score into the created list of lists
                    }
                }
            }
            else
            {
                Console.WriteLine("NOT working at all");
                Query query = parser.Parse(querytext);      // Parse the query text by parser and create the query object
                finalQueryTxt = query.ToString();           // Assign processed query text to final query text variable
                TopDocs results = searcher.Search(query, 10000);                  // Search the query
                System.Console.WriteLine("Number of results is " + results.TotalHits);
                if (results.TotalHits != 0)     // Check if there are found results
                {
                    for (int i = 0; i < results.TotalHits; i++)    // Loop through the top 10 ranked documents
                    {
                        ScoreDoc scoreDoc = results.ScoreDocs[i];   // Get the ranked document
                        Lucene.Net.Documents.Document doc = searcher.Doc(scoreDoc.Doc);     // Get document contents
                        string text = doc.Get(TEXT_FN).ToString();  // Get document contents by fields    
                        string score = scoreDoc.Score.ToString();   // Get document score
                        resultListDict.Add(new List<string> { text, score });     // Add contents and score into the created list of lists
                    }
                }
            }
            
            return resultListDict;
        }

        public Tuple<List<float>, List<string>,int> SearchText_baseline(string querytext)
        {
            List<float> valueListBase = new List<float>();
            List<string> docsIdsListBase = new List<string>();
            querytext = querytext.ToLower();
            Query query = parser.Parse(querytext);
            TopDocs results = searcher2.Search(query, 2000);

            if (results.TotalHits != 0)     // Check if there are found results
            {
                for (int i = 0; i < results.TotalHits; i++)    // Loop through the top 10 ranked documents
                {
                    ScoreDoc scoreDoc = results.ScoreDocs[i];   // Get the ranked document
                    Lucene.Net.Documents.Document doc = searcher2.Doc(scoreDoc.Doc);     // Get document contents
                    string text = doc.Get(TEXT_FN).ToString();  // Get document contents by fields    
                    string score = scoreDoc.Score.ToString();   // Get document score
                    int idxI = text.IndexOf(".I ") + 3;   // Get ID starting index
                    int idxT = text.IndexOf(".T\n");    // Get title starting index
                    string id = text.Substring(idxI, idxT - 1 - idxI);    // Get ID string
                    docsIdsListBase.Add(id.Trim());
                    valueListBase.Add(scoreDoc.Score);
                }
            }
            var result = Tuple.Create(valueListBase, docsIdsListBase, results.TotalHits);
            return result;

        }

        /// <summary>
        /// Closes the index after searching
        /// </summary>
        public void CleanUpSearcher()
        {
            searcher.Dispose();
        }

        /// <summary>
        /// Creates a Thesuaris of stems
        /// </summary>
        /// <returns>A a Thesuaris of stems in the form: <stem,list of words> </returns>
        public Dictionary<string, string[]> CreateThesaurus()       // Have to get WordNet data to here!
        {
            Dictionary<string, string[]> thesaurus = new Dictionary<string, string[]>();

            thesaurus.Add("walk", new[] { "walk", "walked", "walking" });
            thesaurus.Add("run", new[] { "run", "running" });
            thesaurus.Add("love", new[] { "love", "lovely", "loving" });
            return thesaurus;
        }

        /// <summary>
        /// Expands the query with terms in the thesaurus
        /// </summary>
        /// <param name="thesaurus">A thesaurus of stems and associated terms</param>
        /// <param name="query">a query to stem</param>
        /// <returns>the query expanded with words that share the stem</returns>
        public string GetWeightedExpandedQuery(Dictionary<string, string[]> thesaurus, string queryTerm)
        {
            string expandedQuery = "";
            if (thesaurus.ContainsKey(queryTerm))
            {
                string[] word = thesaurus[queryTerm];
                foreach (string w in word)
                {
                    if (w == queryTerm)
                        expandedQuery += "^5";                    
                    expandedQuery += " " + w;
                }
            }
            return expandedQuery;
        }
    }
}
