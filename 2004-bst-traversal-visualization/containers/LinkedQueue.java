package containers;

import java.util.NoSuchElementException;

/**
 * Linked implementation of a queue.
 * Author:   Sergei Golitsinski.
 * Created:  May 24, 2004.
 * Modified: May 24, 2004.
 */
public class LinkedQueue implements Queue
{
	private int count;
	private int maxSize;
	private Node front;
	private Node rear;
	
	public LinkedQueue(int max) { init(max); }

	public LinkedQueue() { init(Integer.MAX_VALUE); }
		
	public void add(Object newItem)
	{
		Node node = new Node(newItem, null);
		
		if (isEmpty())
		{
			front = rear = node;
			front.setLink(node); 
			count++;
		}
		else if (isFull())
			System.out.println("Queue is full");
		else
		{
			rear.setLink(node);
			node.setLink(front);
			rear = node;
			count++;
		}	
	}
	
	public Object remove()
	{
		if (isEmpty())
		{
			System.out.println("Queue is empty");
			return null;
		}
		else
		{
			Object item = front.getItem();
			front = front.getLink();
			
			if (front == null)
				rear = null;
			else
				rear.setLink(front);
			
			count--;
			return item;			
		}
	}
	
	public Object peek()
	{
		if (isEmpty())
			throw new NoSuchElementException("queue is empty");
		else
			return front.getItem();
	}
	
	public boolean isEmpty() { return count == 0; }

	public boolean isFull() { return count == maxSize; }	
	
	public int size() { return count; }	
	
	public void clear() { init(maxSize); }		
	
	public void showStructure()
	{
		if (isEmpty())
			System.out.println("queue is empty");
		else
		{
			Node node = front;
			int i = 1;
			do 
			{
				System.out.println("item # " + i + ": " + node.getItem());
				node = node.getLink();
				i++;
			}	while (node != front);
		}
	}

	/*---------HELPER METHODS--------------*/
	private void init(int max)
	{
		count   = 0;
		maxSize = max;
		front   = null;
		rear    = null;
	}	

}