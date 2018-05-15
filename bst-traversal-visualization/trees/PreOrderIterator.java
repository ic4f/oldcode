package trees;
/**
 * Pre-order binary tree iterator.
 * Author:   Sergei Golitsinski.
 * Created:  May 24, 2004.
 * Modified: May 24, 2004.
 */
public class PreOrderIterator extends BinaryTreeIterator
{
	public PreOrderIterator(BinaryTreeNode n)
	{
		super();
		getStack().push(n);
	}
	
	public Object next()
	{
		BinaryTreeNode node = null;
				
		if (hasNext())
		{
			node = (BinaryTreeNode)getStack().pop();

			if (node.getRight() != null)
				getStack().push(node.getRight());
			if (node.getLeft() != null) 
				getStack().push(node.getLeft());			
		}

		return node;
	}
}
