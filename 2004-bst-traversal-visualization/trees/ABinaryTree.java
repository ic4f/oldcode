package trees;

/**
 * Abstract base class for binary trees.
 * Author:   Sergei Golitsinski.
 * Created:  May 24, 2004.
 * Modified: May 24, 2004.
 */
public abstract class ABinaryTree implements BinaryTree
{
	private BinaryTreeNode root;
	
	public ABinaryTree()
	{
		root = null;
	}
	
	public final BinaryTreeIterator makePreOrderIterator()
	{ 
		return new PreOrderIterator(root);
	}
	
	public final BinaryTreeIterator makeInOrderIterator()
	{ 
		return new InOrderIterator(root);
	}
		
	public final BinaryTreeIterator makePostOrderIterator()
	{ 
		return new PostOrderIterator(root);
	}
		
	public final BinaryTreeIterator makeLevelOrderIterator()
	{ 
		return new LevelOrderIterator(root);
	}	

	public int levels() { return countLevels(root, 0); }
	
	public int levels(BinaryTreeNode node)
	{ 
		return countLevels(node, 0); 
	}

	public int size() { return countNodes(root, 0); }
		
	public void clear() { root = null; }
	
	public boolean isEmpty() { return (root == null); }			
	
	public BinaryTreeNode getRoot() { return root; }

	protected void setRoot(BinaryTreeNode node) { root = node; }	

	public abstract void insert(Comparable key);
		
	
	/* ----- HELPERS --------*/
	
	private int countNodes(BinaryTreeNode node, int nodesSoFar)
	{
		if (node != null)
		{
			int countLeft  = countNodes(node.getLeft(), nodesSoFar);
			int countRight = countNodes(node.getRight(), nodesSoFar);
			nodesSoFar = nodesSoFar + countLeft + countRight + 1;
		}		
		return nodesSoFar;
	}
	
	private int countLevels(BinaryTreeNode node, int levelsSoFar)
	{
		if (node != null)
		{
			int countLeft  = countLevels(node.getLeft(), levelsSoFar + 1);
			int countRight = countLevels(node.getRight(), levelsSoFar + 1);
			levelsSoFar = Math.max(levelsSoFar, Math.max(countLeft, countRight));
		}		
		return levelsSoFar;
	}	
}
