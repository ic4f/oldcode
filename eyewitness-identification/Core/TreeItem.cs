using System;
using System.Collections;
using System.Text;

namespace Core
{
    public abstract class TreeItem
    {
        public TreeItem(int id, TreeItem parent, int depth, string[] data)
        {
            this.id = id;
            this.parent = parent;
            this.depth = depth;
            children = new ArrayList();

            if (data != null) //the root node will have no data
                LoadData(data);
        }

        protected abstract void LoadData(string[] data);

        protected abstract int IndentPixels { get; }

        public int Id { get { return id; } }

        public int Depth { get { return depth; } }

        public TreeItem Parent { get { return parent; } }

        internal void AddChild(TreeItem child)
        {
            children.Add(child);
        }

        protected ArrayList Children { get { return children; } }

        protected Tree ParentTree { get { return parentTree; } }

        protected string GetIndent()
        {
            return (IndentPixels * Depth).ToString();
        }

        internal void SetParent(TreeItem parent) { this.parent = parent; }

        internal void SetTree(Tree tree) { this.parentTree = tree; }

        private int id;
        private TreeItem parent;
        private ArrayList children;
        private int depth;
        private Tree parentTree;
    }
}
