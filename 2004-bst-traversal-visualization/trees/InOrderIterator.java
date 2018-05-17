package trees;
/**
 * In-order binary tree iterator.
 * Author:   Sergei Golitsinski.
 * Created:  May 24, 2004.
 * Modified: May 24, 2004.
 */
public class InOrderIterator extends BinaryTreeIterator
{
	public InOrderIterator(BinaryTreeNode n)
	{
		super();
		slideLeft(n);
	}
	
	public Object next()
	{
		BinaryTreeNode node = null;	
		
		if (hasNext())
		{
			node = (BinaryTreeNode)getStack().pop(); 
			slideLeft(node.getRight());
		}
		
		return node;
	}

	private void slideLeft(BinaryTreeNode node)
	{
		while (node != null)
		{
			getStack().push(node);
			node = node.getLeft();
		}
	}
}
