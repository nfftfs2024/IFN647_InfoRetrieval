using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Search;
using FieldInvertState = Lucene.Net.Index.FieldInvertState;

namespace LuceneAdvancedSearchApplication
{
    public class NewSimilarity : DefaultSimilarity
    {
        public override float Tf(float freq)
        {
            //
            //1~4==>1*2 4~9 2*2, 2*2 9 16 3* increase the importance of the unique terms
            //1~4 as important as 4~8
            //4~8   2* increase the inportance of unique terms...

            if (freq > 0 && freq < 4)
            {
                freq *= 4;
            }
            /*
             * make it decrease the overall inportance of tf
             * 
             */
            return (float)Math.Sqrt(freq) / 2;
        }

        public float idf(long docFreq, long numDocs)
        {
            //1400==> but docFreq term 1 and 2 also end up with the same as 14
            //more than repeated in 14 becomes 2 100*14
            //if it just exist in one docs... then 
            // log not that many different between 1~13 and
            //treat 1~4 is more imfortant than 6~13
            if (docFreq > 0 && docFreq < 5)
            {
                docFreq *= 10;
            }
            return (float)(Math.Log(numDocs / (double)(docFreq + 1)) + 1.0);
        }

    }
}
