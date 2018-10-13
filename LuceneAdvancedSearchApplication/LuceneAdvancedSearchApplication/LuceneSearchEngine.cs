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

namespace LuceneAdvancedSearchApplication
{
    class LuceneSearcheEngine
    {
        Lucene.Net.Store.Directory luceneIndexDirectory;    // Create directory object
        Lucene.Net.Analysis.Analyzer analyzer;              // Create analyzer object
        Lucene.Net.Index.IndexWriter writer;                // Create index writer object
        IndexSearcher searcher;                             // Create searcher object
        IndexSearcher searcher2;                            // Create a searcher for the Baseline.
        QueryParser parser;                                 // Create parser object
        MultiFieldQueryParser multiParser;
        NewSimilarity newSimilarity;

        FileInfo fileStopWords = new FileInfo(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\stopwords.txt"); //Defining path to save the defined stopwords 
        string[] stopWords = { "a", "an", "and", "are", "as", "at", "be", "but", "by", "for", "if", "in", "into", "is", "it", "no", "not", "of", "on", "or", "such", "that", "the", "their", "then", "there", "these", "they", "this", "to", "was", "will", "with" };
        //Lucene.Net.Search.Similarity newSimilarity;   // for similarity measure

        const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;  // Lucene version 
        const string TEXT_FN = "Text";                                              // Lucene field "Text"
        const string TEXT_FN_TITLE = "Title";
        const string TEXT_FN_AUTHOR = "Author";
        Dictionary<string, float> boosts = new Dictionary<string, float>{{ TEXT_FN_TITLE, 100},{TEXT_FN_AUTHOR, 50}};

        public LuceneSearcheEngine()
        {
            luceneIndexDirectory = null;
            writer = null;
            //analyzer = new Lucene.Net.Analysis.SimpleAnalyzer();     // Using simple analyzer for baseline system 
            analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(VERSION, fileStopWords); //Using Standard Analyzer to apply steming and removing of stop words.
            parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, TEXT_FN, analyzer);
            multiParser = new MultiFieldQueryParser(VERSION, new[] {TEXT_FN_TITLE,TEXT_FN_AUTHOR}, analyzer);
            newSimilarity = new NewSimilarity();
            
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
            writer.SetSimilarity(new NewSimilarity());


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
                    int indexT = text.IndexOf(".T");    // Get title starting index
                    int indexA = text.IndexOf(".A");    // Get author starting index
                    int indexB = text.IndexOf(".B");    // Get bibliography starting index
                    string title = text.Substring(indexT + 3, ((indexA - 1 - (indexT + 3)) > 0) ? (indexA - 1 - (indexT + 3)) : 0);     // Get title string
                    string author = text.Substring(indexA + 3, ((indexB - 1 - (indexA + 3)) > 0) ? (indexB - 1 - (indexA + 3)) : 0);    // Get author string


                    //This section is focused on removing the title from the abstract
                    int startTitle = text.IndexOf(".T\n") + 2;    // Get title starting index
                    int startAbstract = text.IndexOf(".A\n") -1 ;    // Get index before author starting  
                    int startWords = text.IndexOf(".W\n");    // Get Words Starting index
                    int lengthOfTitle = startAbstract - startTitle; //Calculate length of title 
                    text = text.Remove(startWords + 2, lengthOfTitle); //Remove title from Words section.
                    

                    // Indexing by using the fields
                    Lucene.Net.Documents.Document doc = new Document();     // Create document
                    doc.Add(new Lucene.Net.Documents.Field(TEXT_FN, text, Field.Store.YES, Field.Index.ANALYZED_NO_NORMS, Field.TermVector.NO));
                    doc.Add(new Lucene.Net.Documents.Field(TEXT_FN_TITLE, title, Field.Store.YES, Field.Index.ANALYZED_NO_NORMS, Field.TermVector.NO));
                    doc.Add(new Lucene.Net.Documents.Field(TEXT_FN_AUTHOR, author, Field.Store.YES, Field.Index.ANALYZED_NO_NORMS, Field.TermVector.NO));                 
                    writer.AddDocument(doc);    // Add document
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
            searcher.Similarity = newSimilarity;
        }


        /// <summary>
        /// Searches the index for the querytext
        /// </summary>
        /// <param name="querytext">The text to search the index</param>
        public List<List<string>> SearchText(string querytext, bool asIsCheckBox, bool advCheckBox, out string finalQueryTxt)
        {
            List<List<string>> resultListDict = new List<List<string>>();      // Initiate a result list    
            if (asIsCheckBox == true)
            {
                Query query = parser.Parse("\"" + querytext + "\"");      // Parse the query text by parser and create the query object
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
                if(advCheckBox)
                {
                    Query query = multiParser.Parse(querytext);      // Parse the query text by parser and create the query object
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

        public List<string> FindSyns(string term)       // Have to get WordNet data to here!
        {
            List<string> thesaurus = new List<string>();
            thesaurus.Add(term);        // Create synonym list and add the original word

            List<Syn.WordNet.SynSet> synSetList = Program.wordNet.GetSynSets(term);     // Get synonyms by using WordNet method

            for(int i = 0; i < synSetList.Count; i++)   // Loop through each entry
            {
                // Only take those that are Nouns and Verbs
                if ((synSetList[i].PartOfSpeech is Syn.WordNet.PartOfSpeech.Noun) || (synSetList[i].PartOfSpeech is Syn.WordNet.PartOfSpeech.Verb))
                {
                    foreach (string s in synSetList[i].Words)   // Loop through each synonym
                    {
                        if(!thesaurus.Contains(s) && !s.Contains("_"))      // If it's not yet contained in the synonym list and contains no "_"
                        {
                            thesaurus.Add(s);   // Add to synonym list
                        }
                    }
                }
            }
            return thesaurus;
        }

        /// <summary>
        /// Expands the query with terms in the thesaurus
        /// </summary>
        /// <param name="thesaurus">A thesaurus of stems and associated terms</param>
        /// <param name="query">a query to stem</param>
        /// <returns>the query expanded with words that share the stem</returns>
        public string GetWeightedExpandedQuery(string queryTerm)
        {
            List <string> thesaurus = new List<string>();
            string expandedQuery = "";

            thesaurus = FindSyns(queryTerm);    // Find synonyms

            if (thesaurus.Count > 1)    // If there's any synonyms found
            {
                foreach (string w in thesaurus)     // Loop through synonyms including original word
                {
                    if (w == queryTerm)
                        expandedQuery += w + "^5 ";     // Boost original queried word
                    else
                        expandedQuery += w + " ";
                }
                return expandedQuery.TrimEnd();     // Return concatenated word and synonyms
            }
            else
                return queryTerm;   // Return original word
        }

        public string PreProcess (PorterStemmer myStemmer, string text)
        {
            char[] splits = new char[] { ' ', '\t', '\'', '"', '-', '(', ')', ',', '’', '\n', ':', ';', '?', '.', '!' };    // Set token delimiters
            string[] tokens =  text.ToLower().Split(splits, StringSplitOptions.RemoveEmptyEntries);     // Tokenisation

            string ProcessedText = "";

            foreach (string t in tokens)        // Looping through each token
            {
                if ((!stopWords.Contains(t)) && (t.Length > 2))     // Remove stopwords
                {
                    //string tempt = myStemmer.stemTerm(t);
                    string tempt = GetWeightedExpandedQuery(t);     // Call query expansion on tokens
                    ProcessedText += tempt + " ";       // Add spaces between words
                }
            }
            return ProcessedText.TrimEnd();     // Trim tailing spaces
        }

    }
}
