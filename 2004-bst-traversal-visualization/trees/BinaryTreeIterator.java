package trees;

import containers.*;

/**
 * Abstract class for binary tree iterators.
 * Author:   Sergei Golitsinski.
 * Created:  May 24, 2004.
 * Modified: May 24, 2004.
 */
public abstract class BinaryTreeIterator //implements Iterator
{
	private LinkedStack stack;
	
	public BinaryTreeIterator()
	{
		stack = new LinkedStack();
	}
	
	public boolean hasNext()
	{
		return !stack.isEmpty();
	}

	//public void remove()	{	} // to do
	
	protected LinkedStack getStack() { return stack; }
		
	public abstract Object next();	
}
