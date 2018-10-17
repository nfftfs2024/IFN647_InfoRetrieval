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
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;


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
        List<String> exFile= new List<string>();

        FileInfo fileStopWords = new FileInfo(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\stopwords.txt"); //Defining path to save the defined stopwords 
        string[] stopWords = { "a", "an", "and", "are", "as", "at", "be", "but", "by", "for", "if", "in", "into", "is",
            "it", "no", "not", "of", "on", "or", "such", "that", "the", "their", "then", "there", "these", "they", "this", "to", "was", "will", "with", "what", "how", "can", "must", "when"};
        //Lucene.Net.Search.Similarity newSimilarity;   // for similarity measure

        const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;  // Lucene version 
        const string TEXT_FN = "Text";                                              // Lucene field "Text"
        const string TEXT_FN_TITLE = "Title";
        const string TEXT_FN_AUTHOR = "Author";
        const float a = 6, b = 3;

        IDictionary<string, float> boosts = new Dictionary<string, float>{{ TEXT_FN_TITLE, a},{TEXT_FN_AUTHOR, b}};

        public LuceneSearcheEngine()
        {
            luceneIndexDirectory = null;
            writer = null;
            //analyzer = new Lucene.Net.Analysis.SimpleAnalyzer();     // Using simple analyzer for baseline system 
            analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(VERSION, fileStopWords); //Using Standard Analyzer to apply steming and removing of stop words.
            parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, TEXT_FN, analyzer);
            multiParser = new MultiFieldQueryParser(VERSION, new[] {TEXT_FN_TITLE,TEXT_FN_AUTHOR}, analyzer, boosts);
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
                files = root.GetFiles("*.txt");
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
                    string name = fi.Name;    // Get file name
                    Console.WriteLine("Adding doc " + name + " to Index");
                    StreamReader reader = new StreamReader(fi.FullName);   // Create a reader
                    string text = reader.ReadToEnd();   // Read the whole text

                    Regex rxi = new Regex(".I ", RegexOptions.Compiled);     // Set the RE to match first sentence of abstract
                    Regex rxa = new Regex(".A\n", RegexOptions.Compiled);
                    Regex rxb = new Regex(".B\n", RegexOptions.Compiled);
                    Regex rxt = new Regex(".T\n", RegexOptions.Compiled);
                    Regex rxw = new Regex(".W\n", RegexOptions.Compiled);
                    MatchCollection abst_i = rxi.Matches(text);
                    MatchCollection abst_a = rxa.Matches(text);
                    MatchCollection abst_b = rxb.Matches(text);
                    MatchCollection abst_t = rxt.Matches(text);
                    MatchCollection abst_w = rxw.Matches(text);

                    if (abst_i.Count > 0 && abst_a.Count > 0 && abst_b.Count > 0 && abst_t.Count > 0 && abst_w.Count > 0)
                    {
                        int indexI = text.IndexOf(".I ");     // Get ID starting index
                        if (abst_i.Count > 1)                 // When having more than 1 .I
                            text = text.Substring(0, indexI + 3) + text.Substring(indexI + 3).Replace(".I ", " ");  // Remove the others except the first one
                        int indexT = text.IndexOf(".T\n");    // Get title starting index
                        if (abst_t.Count > 1)                 // When having more than 1 .T
                            text = text.Substring(0, indexT + 3) + text.Substring(indexT + 3).Replace(".T\n", "");  // Remove the others except the first one
                        int indexA = text.IndexOf(".A\n");    // Get author starting index
                        if (abst_a.Count > 1)                 // When having more than 1 .A
                            text = text.Substring(0, indexA + 3) + text.Substring(indexA + 3).Replace(".A\n", "");  // Remove the others except the first one
                        int indexB = text.IndexOf(".B\n");    // Get bibliography starting index
                        if (abst_b.Count > 1)                 // When having more than 1 .B
                            text = text.Substring(0, indexB + 3) + text.Substring(indexB + 3).Replace(".B\n", "");  // Remove the others except the first one
                        int indexW = text.IndexOf(".W\n");    // Get abstract starting index
                        if (abst_w.Count > 1)                 // When having more than 1 .W
                            text = text.Substring(0, indexW + 3) + text.Substring(indexW + 3).Replace(".W\n", "");  // Remove the others except the first one

                           

                        indexA = text.IndexOf(".A\n");  // Get again the index just in case it has been changed
                        indexB = text.IndexOf(".B\n");  // Get again the index just in case it has been changed
                        string title = text.Substring(indexT + 3, ((indexA - 1 - (indexT + 3)) > 0) ? (indexA - 1 - (indexT + 3)) : 0);     // Get title string
                        string author = text.Substring(indexA + 3, ((indexB - 1 - (indexA + 3)) > 0) ? (indexB - 1 - (indexA + 3)) : 0);    // Get author string

                        //This section is focused on removing the title from the abstract
                        int startTitle = text.IndexOf(".T\n") + 2;    // Get title starting index
                        int startAbstract = text.IndexOf(".A\n") - 1;    // Get index before author starting  
                        int startWords = text.IndexOf(".W\n");    // Get Words Starting index
                        int lengthOfTitle = startAbstract - startTitle; //Calculate length of title 
                        text = text.Remove(startWords + 2, lengthOfTitle); //Remove title from Words section.

                        // Indexing by using the fields
                        Lucene.Net.Documents.Document doc = new Document();     // Create document
                        Lucene.Net.Documents.Field titleField = new Lucene.Net.Documents.Field(TEXT_FN_TITLE, title, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);//Indexing field title
                        Lucene.Net.Documents.Field authorField = new Lucene.Net.Documents.Field(TEXT_FN_AUTHOR, author, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);//Indexing field author
                        doc.Add(new Lucene.Net.Documents.Field(TEXT_FN, text, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));//indexing field text
                        authorField.Boost = 2;
                        titleField.Boost = 5;
                        doc.Add(titleField);
                        doc.Add(authorField);
                        writer.AddDocument(doc);    // Add document
                        reader.Close();
                    }
                    else
                    {
                        Console.WriteLine(name);
                        exFile.Add(name);

                    }
                }
                var message = string.Join(Environment.NewLine, exFile);
                MessageBox.Show("The following files are excluded from the index because of the incorrect format:\n"+message);
                
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
            searcher2.Similarity = newSimilarity;
        }


        /// <summary>
        /// Searches the index for the querytext
        /// </summary>
        /// <param name="querytext">The text to search the index</param>
        public List<List<string>> SearchText(string querytext, bool asIsCheckBox, bool qeCheckBox, bool advCheckBox, string cond, out string finalQueryTxt)
        {
            List<List<string>> resultListDict = new List<List<string>>();      // Initiate a result list    
            if (asIsCheckBox == true && advCheckBox == true && qeCheckBox == false)
            {
                Console.WriteLine("Opcion 1");
                if (cond == "AND")
                {
                    multiParser.DefaultOperator = QueryParser.AND_OPERATOR;
                }
                else
                {
                    multiParser.DefaultOperator = QueryParser.OR_OPERATOR;
                }

                int indexT = querytext.IndexOf("Title:");  // Get again the index just in case it has been changed
                int indexA = querytext.IndexOf("Author:");  // Get again the index just in case it has been changed
                if(indexT!= -1 && indexA != -1)
                {
                    string title_query = querytext.Substring(indexT + 6, ((indexA - 1 - (indexT + 6)) > 0) ? (indexA - 1 - (indexT + 6)) : 0);     // Get title string
                    string author_query = querytext.Substring(indexA + 7, ((querytext.Length - (indexA + 7)) > 0) ? (querytext.Length - (indexA + 7)) : 0);    // Get author string
                    querytext = "Title:\"" + title_query + "\"" + " Author:\"" + author_query + "\"";
                }//When both the title and author are queried
                if (indexT == -1)
                {
                    string author_query = querytext.Substring(indexA + 7, ((querytext.Length - (indexA + 7)) > 0) ? (querytext.Length - (indexA + 7)) : 0);    // Get author string
                    querytext = "Author:\"" + author_query + "\"";
                }//When only the Author is queried

                if (indexA == -1)
                {
                    string title_query = querytext.Substring(indexT + 6, ((querytext.Length - (indexT + 6)) > 0) ? (querytext.Length - (indexT + 6)) : 0);     // Get title string
                    querytext = "Title:\"" + title_query + "\"";
                }//When only the title is queried

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
            }//Searching when AS-IS and advanced searching are activated
            else
            {
                if (asIsCheckBox == true && advCheckBox == false && qeCheckBox == false)
                {
                    Console.WriteLine("Opcion 2");
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
                }//Searching when only AS-IS is activated
                else
                {
                    //Searching when only Advanced searching is activated
                    if (asIsCheckBox == false && advCheckBox == true && qeCheckBox == false)
                    {
                        Console.WriteLine("Opcion 3");
                        //Option 1 the query complete and the parser adding the boosting 
                        /*
                        if (cond == "AND")
                        {
                            multiParser.DefaultOperator = QueryParser.AND_OPERATOR;
                        }
                        else
                        {
                            multiParser.DefaultOperator = QueryParser.OR_OPERATOR;
                        }
                        querytext = querytext.Replace("Title:","").Replace("Author:","");
                        Query query = multiParser.Parse(querytext);
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
                        */

                        //Option 2 sending i some way author and title separate.
                        if (cond == "AND")
                        {
                            Occur[] flags = { Occur.MUST, Occur.MUST };
                            Tuple<string, string> result_preprocessing = Program.Query_Simple_Preprocessing_advanced_Search(querytext);
                            string querytext_title = result_preprocessing.Item1;
                            if (querytext_title.Length == 0) querytext_title = "-Title";
                            string querytext_author = result_preprocessing.Item2;
                            if (querytext_author.Length == 0) querytext_author = "-Author";
                            String[] queries = new String[] { querytext_title, querytext_author };
                            String[] fields = { TEXT_FN_TITLE, TEXT_FN_AUTHOR };
                            Query query = MultiFieldQueryParser.Parse(VERSION, queries, fields, flags, analyzer);// Parse the query text by parser and create the query object
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
                            Occur[] flags = { Occur.SHOULD, Occur.SHOULD };
                            Tuple<string, string> result_preprocessing = Program.Query_Simple_Preprocessing_advanced_Search(querytext);
                            string querytext_title = result_preprocessing.Item1;
                            if (querytext_title.Length == 0) querytext_title = "-Title";
                            string querytext_author = result_preprocessing.Item2;
                            if (querytext_author.Length == 0) querytext_author = "-Author";
                            String[] queries = new String[] { querytext_title, querytext_author };
                            String[] fields = { TEXT_FN_TITLE, TEXT_FN_AUTHOR };
                            Query query = MultiFieldQueryParser.Parse(VERSION, queries, fields, flags, analyzer);// Parse the query text by parser and create the query object
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
                    }//Searching when only Advanced searching is activated
                    else
                    {
                        if (asIsCheckBox == false && advCheckBox == true && qeCheckBox == true)
                        {
                            Console.WriteLine("Opcion 4");
                            //Option 1 the query complete and the parser adding the boosting 
                            /*
                            if (cond == "AND")
                            {
                                multiParser.DefaultOperator = QueryParser.AND_OPERATOR;
                            }
                            else
                            {
                                multiParser.DefaultOperator = QueryParser.OR_OPERATOR;
                            }
                            querytext = querytext.Replace("Title:","").Replace("Author:","");
                            Query query = multiParser.Parse(querytext);
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
                            */

                            //Option 2 sending i some way author and title separate.
                            if (cond == "AND")
                            {
                                Occur[] flags = { Occur.MUST, Occur.MUST };
                                Tuple<string, string> result_preprocessing = Program.Query_Simple_Preprocessing_advanced_Search(querytext);
                                string querytext_title = result_preprocessing.Item1;
                                if (querytext_title.Length == 0) querytext_title = "-Title";;
                                string querytext_author = result_preprocessing.Item2;
                                if (querytext_author.Length == 0) querytext_author = "-Author";
                                String[] queries = new String[] { querytext_title, querytext_author };
                                String[] fields = { TEXT_FN_TITLE, TEXT_FN_AUTHOR };
                                Query query = MultiFieldQueryParser.Parse(VERSION, queries, fields, flags, analyzer);// Parse the query text by parser and create the query object
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
                                Occur[] flags = { Occur.SHOULD, Occur.SHOULD };
                                Tuple<string, string> result_preprocessing = Program.Query_Simple_Preprocessing_advanced_Search(querytext);
                                string querytext_title = result_preprocessing.Item1;
                                if (querytext_title.Length == 0) querytext_title = "-Title";
                                string querytext_author = result_preprocessing.Item2;
                                if (querytext_author.Length == 0) querytext_author = "-Author";
                                String[] queries = new String[] { querytext_title, querytext_author };
                                String[] fields = { TEXT_FN_TITLE, TEXT_FN_AUTHOR };
                                Query query = MultiFieldQueryParser.Parse(VERSION, queries, fields, flags, analyzer);// Parse the query text by parser and create the query object
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
                        else
                        {
                            Console.WriteLine("Opcion 5");
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
