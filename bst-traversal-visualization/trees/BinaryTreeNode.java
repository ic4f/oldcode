package trees;
/**
 * Node for binary trees.
 * Author:   Sergei Golitsinski.
 * Created:  May 24, 2004.
 * Modified: May 24, 2004.
 */
public class BinaryTreeNode
{
	private BinaryTreeNode leftChild;
	private BinaryTreeNode rightChild;
	private Comparable key;

	public BinaryTreeNode(Comparable key)
	{
		leftChild = null;
		rightChild = null;
		this.key = key;
	}
			
	public BinaryTreeNode(
		BinaryTreeNode left, BinaryTreeNode right, Comparable key)
	{
		leftChild = left;
		rightChild = right;
		this.key = key;
	}
	
	public void setLeft(BinaryTreeNode node)
	{
		leftChild = node;
	}
		
	public void setRight(BinaryTreeNode node)
	{
		rightChild = node;
	}
	
	public BinaryTreeNode getLeft() { return leftChild; }
		
	public BinaryTreeNode getRight() { return rightChild; }
		
	public Comparable getKey() { return key; }
	
	public boolean isLeaf()
	{ 
		return (leftChild == null && rightChild == null);
	}
}
