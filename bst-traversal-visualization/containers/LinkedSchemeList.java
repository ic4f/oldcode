package containers;

/**
 * Implementation of a linked scheme-like list.
 * Author:   Sergei Golitsinski.
 * Created:  March 7, 2004.
 * Modified: May 25, 2004.
 */
public class LinkedSchemeList implements SchemeList
{
	private Node firstNode;

	public LinkedSchemeList() { firstNode = null; }

	public int size()
	{
		int size = 0;
		if (!isEmpty())
		{
			Node temp = firstNode;
			while (temp != null)
			{
				temp = temp.getLink();
				size++;				
			}
		}	
		return size;		
	}	

	public void clear() { firstNode = null; }
	
	public boolean isEmpty() {	return ( firstNode == null); }
	
	public Object car()
	{
		if (isEmpty())
			return null; 
		else
			return firstNode.getItem();
	}

	public void cons(Object item)
	{
		Node newNode = new Node(item, firstNode);
		firstNode = newNode;
	}	

	public SchemeList cdr()
	{
		LinkedSchemeList temp = new LinkedSchemeList();
		temp.setFirstNode(firstNode.getLink());
		return temp;
	}
	
	public void print()
	{
		System.out.print("( ");	
		if (!isEmpty())
		{
			Node temp = firstNode;
			while (temp != null)
			{
				System.out.print(temp.getItem() + " ");
				temp = temp.getLink();
			}
		}		
		System.out.print(")");			
	}
	
	public String toString()
	{
		String list = "(";
		if (!isEmpty())
		{
			Node temp = firstNode;
			while (temp != null)
			{
				list = list + temp.getItem() + " ";
				temp = temp.getLink();
			}
		}
		list = list + ")";
		return list;
	}
	
	public void append (SchemeList list)
	{
		LinkedSchemeList listToAppend = (LinkedSchemeList)list;

		if (!listToAppend.isEmpty())
		{		
			Node newFirstNode = listToAppend.getFirstNode(); 
			Node tempNode1 = newFirstNode;  // last node of list to append
			Node tempNode2 = tempNode1.getLink();

			while (tempNode2 != null)
			{
				tempNode1 = tempNode2;
				tempNode2 = tempNode2.getLink();
			}
			tempNode1.setLink(firstNode);
			setFirstNode(newFirstNode);
		}
	}
			

	/*---------HELPERS-----------*/
	protected void setFirstNode(Node node) { firstNode = node;	}	

	protected Node getFirstNode() { return firstNode;	}		
	
	protected void removeHead() { firstNode = firstNode.getLink(); }
	

}