package trees;

/**
 * 
 * Author:   Sergei Golitsinski.
 * Created:  May 25, 2004.
 * Modified: May 25, 2004.
 */
public class AVLTreeNode extends BinaryTreeNode
{
	//private int balanceFactor; 
	
	public AVLTreeNode(Comparable key) 
	{
		super(key);
		//balanceFactor = 0;
	}
	
	public AVLTreeNode(BinaryTreeNode left, BinaryTreeNode right, Comparable key)
	{
		super(left, right, key);
		//balanceFactor = 0;
	}
	
	//public setBalanceFactor(int bf) {} //TODO
}
