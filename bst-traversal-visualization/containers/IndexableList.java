package containers;

/**
 * Interface for an indexable list. 
 * Author:   Sergei Golitsinski.
 * Created:  May 24, 2004.
 * Modified: May 24, 2004.
 */
public interface IndexableList extends List
{
	public void insertAt(Object obj, int index);
	public void removeAt(int index);
	public Object elementAt(int index);
}