package containers;

import java.util.NoSuchElementException;

/**
 * Linked implementation of a stack. 
 * Author:   Sergei Golitsinski.
 * Created:  May 24, 2004.
 * Modified: May 24, 2004.
 */
public class LinkedStack implements Stack
{
	private Node topNode;
	private int numberOfElements;

	public LinkedStack() { init(); }

	public void push(Object item)
	{
		Node newNode = new Node(item, topNode);
		topNode = newNode;
		numberOfElements++;
	}	
	
	public Object pop()
	{
		Object element = null;
		if (isEmpty())
			throw new NoSuchElementException("stack is empty");
		else
		{
			element = topNode.getItem();
			topNode = topNode.getLink();			
			numberOfElements--;
		}
		return element;
	}	

	public Object peek()
	{
		if (isEmpty())
			throw new NoSuchElementException("stack is empty");
		else
			return topNode.getItem();
	}
	
	public int size() { return numberOfElements; }
		
	public void clear() { init(); }
				
	public boolean isEmpty() { return topNode == null; }
	
	
	/*---------HELPER METHODS--------------*/
	private void init()
	{
		topNode = null;
		numberOfElements = 0;
	}	
}