package trees;

/**
 * Post-order binary tree iterator.
 * Author:   Sergei Golitsinski.
 * Created:  May 24, 2004.
 * Modified: May 24, 2004.
 */
public class PostOrderIterator extends BinaryTreeIterator
{
	public PostOrderIterator(BinaryTreeNode n)
	{
		super();
		slideLeft(n);
	}
	
	private void slideLeft(BinaryTreeNode node)
	{
		while (node != null)
		{
			getStack().push(node);
			node = node.getLeft();
		}
		if (!getStack().isEmpty())
		{
			node = (BinaryTreeNode)getStack().peek();
			if (node.getRight() != null)
				slideLeft(node.getRight());
		}
	}
	
	public Object next()
	{
		BinaryTreeNode node = (BinaryTreeNode)getStack().pop();	
			
		if (hasNext())
		{
			BinaryTreeNode parentRightChild = 
				( (BinaryTreeNode)getStack().peek() ).getRight();					
			if ((parentRightChild != null) && (parentRightChild != node))
				slideLeft(parentRightChild);
		}				
		return node;
	}
}