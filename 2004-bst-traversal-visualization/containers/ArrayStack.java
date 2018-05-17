package containers;

import java.util.NoSuchElementException;

/**
 * Array implementation of a stack.
 * Author:   Sergei Golitsinski.
 * Created:  May 25, 2004.
 * Modified: May 25, 2004.
 */
public class ArrayStack implements Stack
{
	private int capacity;
	private int top;
	private Object[] elements;
	
	public ArrayStack() { init(100); }
	
	public ArrayStack(int capacity) { init(capacity); }
	
	public int size() { return top; }

	public Object peek()
	{
		if (isEmpty())
			throw new NoSuchElementException("stack is empty");
		else
			return elements[top - 1];
	}
			
	public boolean isEmpty() { return top == 0; }
	
	public void clear() { init(capacity);	}
	
	public Object pop()
	{
		Object element = null;
		if (isEmpty())
			throw new NoSuchElementException("stack is empty");
		else
		{
			element = elements[top - 1];
			elements[top - 1] = null;
			top--;
		}
		return element;
	}
	
	public void push (Object item)
	{
		try {
			elements[top] = item;
			top++;
		}
		catch (ArrayIndexOutOfBoundsException e) {
			throw new ArrayIndexOutOfBoundsException("stack overflow");
		}
	}
		
	/*---------HELPER METHODS--------------*/
	private void init(int capacity)
	{
		this.capacity = capacity;
		top           = 0;
		elements      = new Object[capacity];
	}
}
