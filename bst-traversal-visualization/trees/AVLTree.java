package trees;

/**
 * AVL tree. 
 * Author:   Sergei Golitsinski.
 * Created:  May 25, 2004.
 * Modified: May 25, 2004.
 */
public class AVLTree extends BinarySearchTree
{
	public AVLTree() { super(); }
	
	public synchronized void insert(Comparable key)
	{
		setRoot(insertKey(getRoot(), key));
		// add balancing
	}

	public synchronized Object remove(Comparable key)
	{
		return null; // to do
	}
	
	protected BinaryTreeNode insertKey(AVLTreeNode node, Comparable key)
	{
		if (node == null)
			return new AVLTreeNode(key);
		else
			if (key.compareTo(node.getKey()) < 0)
				node.setLeft(insertKey(node.getLeft(), key));
			else
				node.setRight(insertKey(node.getRight(), key));
			return node;					
	}

}
