package trees;

import containers.*;

/**
 * Level-order binary tree operator (queue-based). 
 * Author:   Sergei Golitsinski.
 * Created:  May 24, 2004.
 * Modified: May 24, 2004.
 */
public class LevelOrderIterator extends BinaryTreeIterator
{
	private LinkedQueue queue;
	
	public LevelOrderIterator(BinaryTreeNode n)
	{
		super();
		queue = new LinkedQueue();
		queue.add(n);
	}
	
	public boolean hasNext() { return !queue.isEmpty(); }

	public void remove()	{	} // to do
	
	public Object next()
	{
		BinaryTreeNode node = null;
		
		if (hasNext())
		{
			node = (BinaryTreeNode)queue.remove();
			
			if (node.getLeft() != null) 
				queue.add(node.getLeft());	
			if (node.getRight() != null)
				queue.add(node.getRight());
		}
		return node;
	}
}