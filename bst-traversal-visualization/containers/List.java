package containers;

/**
 * Interface for a list.
 * Author:   Sergei Golitsinski.
 * Created:  March 7, 2004.
 * Modified: May 24, 2004.
 */
public interface List extends Container
{
	public void addFront(Object obj);
	public void addRear(Object obj);
	public void removeFront();
	public void removeRear();
	public Object getFront();
	public Object getRear();
	public boolean contains(Object obj);
}