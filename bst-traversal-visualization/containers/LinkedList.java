package containers;

/**
 * Base interface for an ADT.
 * Author:   Sergei Golitsinski.
 * Created:  May 24, 2004.
 * Modified: May 24, 2004.
 */
public class LinkedList extends LinkedSchemeList implements List
{
	public LinkedList() { super(); }
	
	public void addFront(Object obj)	{ cons(obj); }	
	
	public void addRear(Object obj)
	{	
		if (isEmpty())
			addFront(obj);
		else
		{
			Node newNode = new Node(obj, null);
			Node tempNode1 = getFirstNode();
			Node tempNode2 = tempNode1.getLink();
		
			while (tempNode2 != null)
			{
				tempNode1 = tempNode2;
				tempNode2 = tempNode2.getLink();
			}
			tempNode1.setLink(newNode);
		}
	}
	
	public void removeFront() { removeHead(); }
	
	public void removeRear()
	{
		Node tempNode1 = getFirstNode();
		Node tempNode2 = tempNode1.getLink();
		
		while (tempNode2.getLink() != null)
		{
			tempNode1 = tempNode2;
			tempNode2 = tempNode2.getLink();
		}
		tempNode1.setLink(null);
	}
	
	public Object getFront() { return car();}
	
	public Object getRear()
	{
		Node tempNode = getFirstNode();
		
		while (tempNode.getLink() != null)
			tempNode = tempNode.getLink();
		
		return tempNode.getItem();	
	}
	
	public boolean contains(Object obj)
	{
		Node tempNode = getFirstNode();
		while (tempNode != null)
		{
			if (tempNode.getItem().equals(obj))
				return true;							
			tempNode = tempNode.getLink();
		}
		return false;
	}
}