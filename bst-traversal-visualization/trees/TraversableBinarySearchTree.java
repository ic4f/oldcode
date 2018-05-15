package trees;
/**
 * BST with internal traversing functionality.
 * Author:   Sergei Golitsinski.
 * Created:  May 24, 2004.
 * Modified: May 24, 2004.
 */
public class TraversableBinarySearchTree extends BinarySearchTree
{
	public TraversableBinarySearchTree() { super(); }
			
	public void traverse(int order)
	{
		if (order == 1)
			traversePreOrder(getRoot());
		else if (order == 2)
			traverseInOrder(getRoot());		
		else if (order == 3)
			traversePostOrder(getRoot());		
	}
	
	private void traversePreOrder(BinaryTreeNode node)
	{
		if (node != null)
		{
			visit(node);
			traversePreOrder(node.getLeft());
			traversePreOrder(node.getRight());
		}		
	}	
	
	private void traverseInOrder(BinaryTreeNode node)
	{
		if (node != null)
		{
			traverseInOrder(node.getLeft());
			visit(node);			
			traverseInOrder(node.getRight());
		}		
	}	
		
	private void traversePostOrder(BinaryTreeNode node)
	{
		if (node != null)
		{
			traversePostOrder(node.getLeft());
			traversePostOrder(node.getRight());
			visit(node);			
		}		
	}		

	protected void visit(BinaryTreeNode node)
	{
		System.out.println("node value: " + node.getKey().toString());	
	}
}
