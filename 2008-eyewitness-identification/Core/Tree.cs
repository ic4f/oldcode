using System;
using System.Collections;
using System.Data;
using System.Text;

namespace Core
{
    /// <summary>
    /// The treeData parameter is a 2-d string array with each row representing a tree item.
    /// Position 0 is the item id, position 1 is the id of the parent item. 
    /// The rest is data (such as text, url, highlight or not, etc.), which varies by tree type.
    /// </summary>
    public abstract class Tree
    {
        public static int ROOT_ID = -1; //cannot be 0!

        public abstract TreeItem MakeTreeItem(int id, TreeItem parent, int level, string[] itemData);

        public Tree(string[,] treeData, int currentId)
        {
            init(treeData, currentId);
        }

        public TreeItem MakeRoot()
        {
            return MakeTreeItem(ROOT_ID, null, -1, null);
        }

        public bool ItemIsParent(int id)
        {
            return parents.ContainsKey(id);
        }

        public TreeItem GetItem(int id) { return (TreeItem)items[id]; }

        protected int CurrentId { get { return currentId; } }

        protected TreeItem Root { get { return root; } }

        protected Hashtable Items { get { return items; } }

        private void init(string[,] treeData, int currentId)
        {
            this.currentId = currentId;
            items = new Hashtable();
            ids = new Hashtable();
            parents = new Hashtable();
            root = MakeRoot();
            items.Add(ROOT_ID, root);
            loadTree(treeData);
            loadParentNodes();
        }

        private void loadTree(string[,] treeData)
        {
            bool hasMoreItems = true;
            while (hasMoreItems)
                hasMoreItems = loadTreeHelper(treeData);
        }

        private bool loadTreeHelper(string[,] treeData)
        {
            bool hasMoreItems = false;
            int itemDataRowLength = treeData.GetUpperBound(1) - 1;
            for (int i = 0; i < treeData.GetUpperBound(0) + 1; i++)
            {
                int id = Convert.ToInt32(treeData[i, 0]);
                if (!ids.ContainsKey(id))
                {
                    int parentId = Convert.ToInt32(treeData[i, 1]);

                    string[] itemData = new string[itemDataRowLength];
                    for (int j = 0; j < itemDataRowLength; j++) //the first 2 positions are id and parentId				
                        itemData[j] = treeData[i, j + 2];

                    if (items[parentId] != null)
                    {
                        addItem(id, parentId, itemData);
                        ids.Add(id, parentId);
                    }
                    else
                        hasMoreItems = true;
                }
            }
            return hasMoreItems;
        }

        private void loadParentNodes()
        {
            int parentId = currentId;
            while (parentId != 0) //includes root node (ROOT_ID)
            {
                parents.Add(parentId, true);
                parentId = Convert.ToInt32(ids[parentId]);
            }
        }

        private void addItem(int id, int parentId, string[] itemData)
        {
            Console.WriteLine("adding parentId=" + parentId);
            TreeItem parent = (TreeItem)items[parentId];
            if (parent == null)
                throw new Exception("parent with id " + parentId + " does not exist");

            TreeItem item = MakeTreeItem(id, parent, parent.Depth + 1, itemData);
            item.SetTree(this);
            items.Add(item.Id, item);
            item.SetParent(parent);
            parent.AddChild(item);
        }

        private int currentId;
        private Hashtable items;
        private Hashtable ids;
        private TreeItem root;
        private Hashtable parents;
    }
}
