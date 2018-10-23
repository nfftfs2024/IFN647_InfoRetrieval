using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.tagger.maxent;
using java.util;
using java.io;

namespace LuceneAdvancedSearchApplication


{


    class POSTagger
    {
        static string jarRoot = @"..\..\..\..\data\paket-files\nlp.stanford.edu\stanford-postagger-full-2016-10-31";
        static string modelsDirectory = jarRoot + @"\models";

        // Loading POS Tagger
        MaxentTagger tagger = new MaxentTagger(modelsDirectory + @"\wsj-0-18-bidirectional-nodistsim.tagger");

        // Text for tagging
        
        public void FindPOS(string querytext)
        {
            MaxentTagger tagger = new MaxentTagger();
            StringReader stringReader = new StringReader(querytext);
            Array inputSentances = MaxentTagger.tokenizeText(stringReader).toArray();
            foreach(java.util.ArrayList sentance in inputSentances)
            {
                var taggedSentence = tagger.tagSentence(sentance);
            }

        }
    }
}
