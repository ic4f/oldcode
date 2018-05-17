using System;
using System.Collections;
using System.IO;
using d = Giggle.Data;

namespace Giggle.Indexing
{
    /// <summary>
    /// calculates PageRank, can return an array or write it to a file
    /// </summary>
    public class PageRankCalculator
    {
        private short[][] links;                //pos=docId, value = short[] docIds of links (forwardlinks)
        private Hashtable[] citations;  //key=docId, value = Hashtable of docIds of citations (backlinks)
        private float c;                            //weight c, used in PageRank formula
        private float[] ranks;
        private const float THRESHOLD = 0.001f; //reasonable value: takes 10 iterations to converge 25.000 page ranks

        public PageRankCalculator(short[][] links, short[][] citationsArray, float c) //arrays come from the Index object
        {
            this.links = links;
            this.c = c;
            ranks = new float[links.Length];
            loadCitations(citationsArray);
        }

        public float[] CalculateRanks()
        {
            initRanks();
            runPRAlgorithm();
            normalize();
            return ranks;
        }

        public void WriteRanksToFile(string directoryPath)
        {
            string path = directoryPath + "//" + d.Helper.SOURCE_PAGERANK_FILE;
            if (File.Exists(path))
                File.Delete(path);

            string pathText = directoryPath + "//" + d.Helper.DATATXT_DIRECTORY_NAME + "//" + d.Helper.SOURCE_PAGERANK_FILE + ".txt";
            if (File.Exists(pathText))
                File.Delete(pathText);

            FileStream fs = new FileStream(path, FileMode.CreateNew);
            BinaryWriter w = new BinaryWriter(fs);
            StreamWriter wText = new StreamWriter(pathText, false, System.Text.Encoding.ASCII);

            for (short i = 0; i < ranks.Length; i++)
            {
                w.Write(ranks[i]);
                wText.WriteLine(ranks[i]);
            }
            w.Close();
            wText.Close();
            fs.Close();
        }

        //transform short[][] into Hashtable[]
        private void loadCitations(short[][] citationsArray)
        {
            short[] temp;
            citations = new Hashtable[citationsArray.Length];
            for (int i = 0; i < citationsArray.Length; i++)
            {
                Hashtable docCitations = new Hashtable();
                citations[i] = docCitations;
                temp = citationsArray[i];
                if (temp != null)
                    for (int j = 0; j < temp.Length; j++)
                        docCitations.Add(temp[j], true);
            }
        }

        private void initRanks()
        {
            float initRank = 1f / ranks.Length;
            for (int i = 0; i < ranks.Length; i++)
                ranks[i] = initRank;
        }

        private void runPRAlgorithm()
        {
            Console.WriteLine("start PageRank calculation");
            int size = ranks.Length;
            float rankAccum; //accumulates rank for a page
            float p = (1 - c) / size; //the minimum probability
            float[] newRanks;   //required for calculating ranks maintaining ranks of previous iteration
            float[] tempRanks = new float[size]; //holds the difference of auth scores between iterations
            Hashtable parents;

            int counter = 0;
            do
            {
                Console.WriteLine("PageRank calculation counter=" + counter++);
                newRanks = new float[size];
                tempRanks = ranks;

                for (short i = 0; i < size; i++) //loop through all pages
                {
                    rankAccum = 0; //reset rank accumulator
                    parents = citations[i]; //parent pages / backlinks
                                            /*  
                                             * p = (1-c)/|number of pages| 
                                             * If page j has a link to page i, then rank(i) =+ rank(j) * (1/|children of j| * c + p)
                                             * Else - rank(i) =+ rank(j) * p
                                             */
                    for (short j = 0; j < size; j++) //loop through all pages (i.e. parents)						
                    {
                        if (parents != null && parents.Contains(j)) //optimize!!!!!!
                            rankAccum += (1f / links[j].Length * c + p) * ranks[j];
                        else
                            rankAccum += p * ranks[j];
                    }
                    newRanks[i] = rankAccum;
                }
                ranks = newRanks; //assign updated ranks		
            }
            while (notConverged(tempRanks));
        }

        private bool notConverged(float[] ranksTemp)
        {
            for (int i = 0; i < ranks.Length; i++)
                if ((Math.Abs(ranks[i] - ranksTemp[i]) > THRESHOLD))
                    return true;
            return false;
        }

        private void normalize()
        {
            float max = 0;
            for (int i = 0; i < ranks.Length; i++)
                if (ranks[i] > max)
                    max = ranks[i];

            for (int i = 0; i < ranks.Length; i++)
                ranks[i] = ranks[i] / max;
        }
    }
}
