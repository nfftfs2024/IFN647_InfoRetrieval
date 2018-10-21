using System;
using System.Linq;
using Microsoft.Azure.CognitiveServices.Language.SpellCheck;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LuceneAdvancedSearchApplication
{
    class SpellCheck
    {
        public static string Query_SpellCheck(string querytext)
        {
            List<String> correction = new List<string>();

            var client = new SpellCheckClient(new ApiKeyServiceClientCredentials("c1a10976d73e469382f0860c0ab2dac4"));

            char[] splits = new char[] { ' ', '\t', '\'', '"', '-', '(', ')', ',', '’', '\n', ':', ';', '?', '.', '!' };    // Set token delimiters
            string[] tokens = querytext.ToLower().Split(splits, StringSplitOptions.RemoveEmptyEntries);     // Tokenisation

            var result = client.SpellCheckerWithHttpMessagesAsync(text: querytext, mode: "proof", acceptLanguage: "en-US").Result;
            //Console.WriteLine("Correction for Query:" + querytext);

            // SpellCheck Results
            if (result?.Body.FlaggedTokens?.Count > 0)
            {
                foreach (var x in result.Body.FlaggedTokens)
                {
                    int index = Array.IndexOf(tokens, x.Token);

                    var suggestions = x.Suggestions;
                    if (suggestions?.Count > 0)
                    {
                        var firstSuggestion = suggestions.FirstOrDefault();
                        //Console.WriteLine(firstSuggestion.Suggestion);
                        tokens[index] = firstSuggestion.Suggestion;
                        correction.Add("\""+x.Token+"\" has been corrected to \"" + tokens[index]+"\"");
                    }
                }
            }

            string afterQuery = "";
            foreach (string t in tokens)
            {
                afterQuery += t + " ";
            }
            //Console.WriteLine(afterQuery.TrimEnd());
            if (correction.Count > 0)
            {
                var message = string.Join(Environment.NewLine, correction);
                MessageBox.Show("Words have been corrected by the Spell Checker\n\n" + message, "Word Correction", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return afterQuery.TrimEnd().Replace("title","Title:").Replace("author", "Author:");
        }
    }
}