package trees;

/**
 * Basic binary search tree.
 * Author:   Sergei Golitsinski.
 * Created:  May 24, 2004.
 * Modified: May 24, 2004.
 */

public class BinarySearchTree extends ABinaryTree
{
	public BinarySearchTree() { super(); }

	public synchronized void insert(Comparable key)
	{
		setRoot(insertKey(getRoot(), key));
	}	

	public double avgSuccessfulSearch()
	{
		double count = size();
		return (internalPath() + count) / count; // (I + n)/n
	}
	
	public double avgUnsuccessfulSearch()
	{
		double count = size();
		return (internalPath() + 2 * count) / (count + 1); // (E + 2n); E = I + 2n
	}

	public boolean contains(Object key)
	{			 
		return containsHelper(getRoot(), key);	
	}
	
	public Object find(Object key) 
	{
		BinaryTreeNode node = getRoot();
		int compare;
		
		while (node != null)
			if ( (compare = node.getKey().compareTo(key)) > 0)
				node = node.getLeft();
			else if (compare < 0)
				node = node.getRight();
			else
				return node.getKey();
		
		//if (node != null)
			//return node.getKey();
		//else
			return null;
	}
	
	/* ----- HELPERS --------*/
	
	protected BinaryTreeNode insertKey(BinaryTreeNode node, Comparable key)
	{		
		if (node == null)
			return makeNode(key);
		else
			if (key.compareTo(node.getKey()) < 0)
				node.setLeft(insertKey(node.getLeft(), key));
			else
				node.setRight(insertKey(node.getRight(), key));
			return node;
	}
	
	protected BinaryTreeNode makeNode(Comparable key)
	{
		return new BinaryTreeNode(key); 
	}
	
	private boolean containsHelper(BinaryTreeNode node, Object key)
	{	
		int result;
		if (node != null)
			if ( (result = node.getKey().compareTo(key) ) > 0)
				return containsHelper(node.getLeft(), key);
			else if (result < 0)
				return containsHelper(node.getRight(), key);		
			else
				return true;

		return false;
	}

	private int internalPath()
	{
		int length = 0;
		for (BinaryTreeIterator i = makeLevelOrderIterator(); i.hasNext();)
		{
			BinaryTreeNode node = (BinaryTreeNode)i.next(); 
			length = length + getLevel(node);
		}
		return length;
	}		
	
	public int getLevel(BinaryTreeNode node) //changre to private
	{
		int level = 0;
		BinaryTreeNode treeNode = getRoot();
		
		while (treeNode != null)
		{
			if (node == treeNode)
				return level;
			else
				if (treeNode.getKey().compareTo(node.getKey()) > 0) 
					treeNode = treeNode.getLeft();
				else 
					treeNode = treeNode.getRight();

			level++;
		}
		return level;
	}	
}