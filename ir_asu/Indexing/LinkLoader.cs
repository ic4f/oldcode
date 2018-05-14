using System;
using System.IO;

namespace Giggle.Indexing
{
    public class LinkLoader
    {
        private short[][] docLinks; //array of doc links arrays
        private short[][] docCitations; //array of doc citations arrays
        private short[] docLinkCounts;
        private short[] docCitationCounts;

        public LinkLoader(string docLinksFile, string docCitationsFile, short numberOfDocs)
        {
            loadLinks(docLinksFile, numberOfDocs);
            loadCitations(docCitationsFile, numberOfDocs);
        }

        public short[][] DocLinks { get { return docLinks; } }

        public short[][] DocCitations { get { return docCitations; } }

        public short[] DocLinkCounts { get { return docLinkCounts; } }

        public short[] DocCitationCounts { get { return docCitationCounts; } }

        private void loadLinks(string sourcePath, short numberOfDocs)
        {
            docLinkCounts = new short[numberOfDocs];
            countInstances(docLinkCounts, sourcePath);

            docLinks = new short[numberOfDocs][];

            FileStream fs = File.OpenRead(sourcePath);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);

            short[] cursors = new short[numberOfDocs];
            short docId;
            short linkId;
            int space;
            string line;
            while ((line = r.ReadLine()) != null)
            {
                space = line.IndexOf(" ");
                docId = Convert.ToInt16(line.Substring(0, space));
                linkId = Convert.ToInt16(line.Substring(space + 1));

                if (docLinks[docId] == null)
                    docLinks[docId] = new short[docLinkCounts[docId]];

                docLinks[docId][cursors[docId]] = linkId;
                cursors[docId] = (short)(cursors[docId] + 1);
            }
            r.Close();
            fs.Close();
        }

        private void loadCitations(string sourcePath, short numberOfDocs)
        {
            docCitationCounts = new short[numberOfDocs];
            countInstances(docCitationCounts, sourcePath);

            docCitations = new short[numberOfDocs][];

            FileStream fs = File.OpenRead(sourcePath);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);

            short[] cursors = new short[numberOfDocs];
            short docId;
            short citationId;
            int space;
            string line;
            while ((line = r.ReadLine()) != null)
            {
                space = line.IndexOf(" ");
                docId = Convert.ToInt16(line.Substring(0, space));
                citationId = Convert.ToInt16(line.Substring(space + 1));

                if (docCitations[docId] == null)
                    docCitations[docId] = new short[docCitationCounts[docId]];

                docCitations[docId][cursors[docId]] = citationId;
                cursors[docId] = (short)(cursors[docId] + 1);
            }
            r.Close();
            fs.Close();
        }

        private void countInstances(short[] instances, string sourcePath)
        {
            FileStream fs = File.OpenRead(sourcePath);
            StreamReader r = new StreamReader(fs, System.Text.Encoding.ASCII);

            short docId;
            int space;
            string line;
            while ((line = r.ReadLine()) != null)
            {
                space = line.IndexOf(" ");
                docId = Convert.ToInt16(line.Substring(0, space));
                instances[docId] = (short)(instances[docId] + 1);
            }
            r.Close();
            fs.Close();
        }
    }
}
