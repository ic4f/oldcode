package containers;
/**
 * Node for linear linked structures.
 * Author:   Sergei Golitsinski.
 * Created:  May 24, 2004.
 * Modified: May 24, 2004.
 */
public class Node
{
	private Object item;
	private Node link;
	
	public Node(Object item, Node link)
	{
		this.item = item;
		this.link = link;
	}
	
	public Object getItem() { return item; }
	
	public Node getLink() { return link; }
	
	public void setLink(Node newLink) { link = newLink; }
}