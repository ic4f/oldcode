using System;
using System.Collections;

namespace Giggle.Data
{
    /// <summary>
    /// Takes as an argument an integer array of search results (page IDs), 
    /// calculates root and base sets, initializes a PageGraph and calls its calculateAuthorityHubs method. 
    /// Provides access to sorted PageRecord arrays of authorities and hubs.
    /// </summary>
    public class AHPageLoader
    {
        private Index index;
        private AHPageGraph pageGraph;

        public AHPageLoader(Index index, ResultDocument[] resultDocs, int rootSize, int maxParents, int maxChildren)
        {
            this.index = index;
            pageGraph = new AHPageGraph(rootSize);
            loadBaseSet(resultDocs, rootSize, maxParents, maxChildren);
            loadLinks();
        }

        public AHDocument[] Authorities { get { return pageGraph.GetAuthorities(); } }

        public AHDocument[] Hubs { get { return pageGraph.GetHubs(); } }

        private void loadBaseSet(ResultDocument[] resultDocs, int rootSize, int maxParents, int maxChildren)
        {
            //make rootset
            short[] rootSet = new short[Math.Min(resultDocs.Length, rootSize)];
            for (int i = 0; i < rootSet.Length; i++)
                rootSet[i] = resultDocs[i].DocId;

            //make baseset
            short[] parents;
            short[] children;
            for (int i = 0; i < rootSet.Length; i++)
            {
                pageGraph.AddPage(rootSet[i]);

                parents = index.GetDocCitations(rootSet[i]);
                if (parents != null)
                {
                    maxParents = Math.Min(maxParents, parents.Length);
                    for (int j = 0; j < maxParents; j++)
                        pageGraph.AddPage(parents[j]);
                }
                children = index.GetDocLinks(rootSet[i]);
                if (children != null)
                {
                    maxChildren = Math.Min(maxChildren, children.Length);
                    for (int j = 0; j < maxChildren; j++)
                        pageGraph.AddPage(children[j]);
                }
            }
        }

        private void loadLinks()
        {
            Hashtable docs = pageGraph.BaseSet;

            short docId;
            short[] links;
            IDictionaryEnumerator en = docs.GetEnumerator();
            while (en.MoveNext())
            {
                docId = Convert.ToInt16(en.Key);
                links = index.GetDocLinks(docId);
                if (links != null)
                    foreach (short linkId in links)
                        if (docs.Contains(linkId))
                            pageGraph.AddLink(docId, linkId);

                links = index.GetDocCitations(docId);
                if (links != null)
                    foreach (short linkId in links)
                        if (docs.Contains(linkId))
                            pageGraph.AddLink(linkId, docId);
            }
        }
    }
}
