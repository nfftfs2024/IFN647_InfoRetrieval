using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalyser
{
    class TextAnalyser
    {
        PorterStemmerAlgorithm.PorterStemmer myStemmer; // for Activity 4,5
        System.Collections.Generic.Dictionary<string, int> tokenCount; // for Activity 5
        public string[] stopWords = {"a", "an", "and", "are", "as", "at", "be", "but", "by","for", "if", "in", "into", "is", "it","no", "not", "of", "on", "or", "such","that", "the", "their", "then", "there", "these","they", "this", "to", "was", "will", "with"}; // for challange activity

        public TextAnalyser()
        {
            myStemmer = new PorterStemmerAlgorithm.PorterStemmer();
            tokenCount = new Dictionary<string,int>();
        }

        //Activity 3
        /// <summary>
        /// Convert the  given text into tokens and then splits it into tokens according to whitespace and punctuation. 
        /// </summary>
        /// <param name="text">Some text</param>
        /// <returns>Lower case tokens</returns>
        public string[] TokeniseString(string text)
        {
            char[] splitters = new char[] { ' ', '\t', '\'', '"', '-', '(', ')', ',', '’', '\n', ':', ';', '?', '.', '!' };
            return text.ToLower().Split(splitters, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Prints out tokens for a given text string
        /// </summary>
        /// <param name="str">a string of text</param>
        public void OutputTokens(string str)
        {
            System.Console.WriteLine("Orginal: \"" + str + "\"");
            string[] tokens = TokeniseString(str);
            Console.WriteLine("Tokens: ");
            foreach (string t in tokens)
            {
                System.Console.WriteLine(t);
            }
        }

        //Activity 4
        /// <summary>
        /// Stems an array of tokens
        /// </summary>
        /// <param name="tokens">An array of lowercase tokens</param>
        /// <returns>An array of stems</returns>
        public string[] StemTokens(string[] tokens){
            int numTokens = tokens.Count();
            string[] stems = new string[numTokens];
            for(int i =0; i< numTokens; i++)
            {
                stems[i] = myStemmer.stemTerm(tokens[i]);
            }
            return stems;
        }

        /// <summary>
        /// Outputs stems to the sceen
        /// </summary>
        /// <param name="str">A string of text to be stemmed</param>
        public void OutputStems(string str)
        {
            System.Console.WriteLine("Orginal: \"" + str + "\"");
            string[] tokens = TokeniseString(str);
            string[] stems = StemTokens(tokens);
            Console.WriteLine("Stems: ");
            foreach (string s in stems)
            {
                System.Console.WriteLine(s);
            }
        }

        // Activity 5
        /// <summary>
        /// Counts the occurrences of a given set of tokens
        /// </summary>
        /// <param name="tokens">An array of tokens</param>
        public void CountOccurrences(string[] tokens)
        {
            foreach (string s in tokens)
            {
                int count=0;
                tokenCount.TryGetValue(s, out count);
                count = count + 1;
                tokenCount[s] = count;
            }
        }

        /// <summary>
        /// Displays the count of tokens to the screen
        /// </summary>
        public void DisplayCount()
        {
            foreach (var value in tokenCount)
            {
                string token = value.Key;
                int occurrences = value.Value;

                System.Console.WriteLine("Token is " + token + " number of occurrences are " + occurrences);
            }
        }

        /// <summary>
        /// Outputs occurrence count for a given string
        /// </summary>
        /// <param name="str">A string of text</param>
        public void OutputTokenCount(string str)
        {
            ResetCount();
            string[] tokens = TokeniseString(str);
            string[] stems = StemTokens(tokens);
            Console.Out.WriteLine("Orginal: " + str);
            CountOccurrences(stems);
            DisplayCount();
        }



        /// <summary>
        /// Resets the tokencount to 0
        /// </summary>
        public void ResetCount()
        {
            tokenCount.Clear();
        }



        // Activity 6
        /// <summary>
        /// Saves the token count to a file
        /// </summary>
        /// <param name="filename">The filename to save the token count to</param>
        public void SaveCountToFile(string filename)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter(filename);
            foreach (var value in tokenCount)
            {
                string token = value.Key;
                int occurrence = value.Value;
                writer.WriteLine("Token is " + token + " number of occurrences are " + occurrence);
            }
            writer.Flush();
            writer.Close();
        }


        /// <summary>
        /// Counts the occurrence of stems in a given text file and prints out the results to the screen
        /// </summary>
        /// <param name="infilename">The filename to be read in</param>
        /// <param name="outfilename">The filename to save the statistics</param>
        public void ProcessText(string infilename, string outfilename)
        {
            ResetCount();
            System.IO.StreamReader reader = new System.IO.StreamReader(infilename);
            string line = "";
            while ((line = reader.ReadLine()) != null){
                //string text = linel
                string[] tokens = TokeniseString(line);
                string[] stems = StemTokens(tokens);
                CountOccurrences(stems);
            }
            reader.Close();
            DisplayCount();
            SaveCountToFile(outfilename);

        }


        // Challenge Activity
        /// <summary>
        /// Removes stopwords from an array of tokens
        /// </summary>
        /// <param name="tokens">An array of tokens</param>
        /// <returns>The array of tokens without any stopwords</returns>
        public string[] StopWordFilter(string[] tokens)
        {

            int numTokens = tokens.Count();
            List<string> filteredTokens = new List<string>();
            for (int i = 0; i < numTokens; i++)
            {
                string token = tokens[i];
                if (!stopWords.Contains(token) && (token.Length > 2)) filteredTokens.Add(token);
            }
            return filteredTokens.ToArray<string>();
        }

 
        
        /// <summary>
        /// Outputs the token count without any stopwords
        /// </summary>
        /// <param name="strs"></param>
        public void OutputTokenCountWithoutStopwords(string str)
        {
            ResetCount();

            string[] tokens = TokeniseString(str);
            string[] tokensNoStop = StopWordFilter(tokens);
            string[] stems = StemTokens(tokensNoStop);
            CountOccurrences(stems);

            DisplayCount();
        }

        
        /// <summary>
        /// Counts the occurrences of stems in a given text file ignoring stop words and prints out the results to the screen
        /// </summary>
        /// <param name="infilename">The filename to be read in</param>
        /// <param name="outfilename">The filename to save the statistics</param>
        public void ProcessTextNoStopWords(string infilename, string outfilename)
        {
            ResetCount();
            System.IO.StreamReader reader = new System.IO.StreamReader(infilename);
            string line = "";
            while ((line = reader.ReadLine()) != null)
            {
                //string text = linel
                string[] tokens = TokeniseString(line);
                string[] tokensnostop = this.StopWordFilter(tokens);
                string[] stems = StemTokens(tokensnostop);
                CountOccurrences(stems);
            }
            reader.Close();
            DisplayCount();
            SaveCountToFile(outfilename);

        }

        static void Main(string[] args)
        {


            TextAnalyser textAnalyser = new TextAnalyser();
            
            System.Console.WriteLine("Activity 3");
            string text1 = "Tokenising, even in english, is a difficult problem. It's even harder in other languages - such as Chinese!";
            //textAnalyser.OutputTokens(text1);
 
            string stem1 = "civilise";
            string stem2 = "advertise";
            string stem3 = "professionalize";

            //System.Console.WriteLine();
            //System.Console.WriteLine("Activity 4");
            textAnalyser.OutputStems(stem1);
            textAnalyser.OutputStems(stem2);
            textAnalyser.OutputStems(stem3);



            //System.Console.WriteLine();
            //System.Console.WriteLine("Activity 5");
            //string text2 = "The word \"the\" is the most common word in the English language"; 
            //textAnalyser.OutputTokenCount(text2);


            //System.Console.WriteLine();
            //System.Console.WriteLine("Activity 6");
            //string infilename = @"C:\temp\AlicePara.txt";
            //string outfilename = @"C:\temp\AliceParaOccurrence.txt";
            //textAnalyser.ProcessText(infilename, outfilename);

            //System.Console.WriteLine();
            //System.Console.WriteLine("Challenge Activity");
            //string outfilename2 = @"C:\temp\AliceParaOccurrenceNoStop.txt";
            //textAnalyser.ProcessTextNoStopWords(infilename, outfilename2);

            System.Console.ReadLine();

        }



    }
}
