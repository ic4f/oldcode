package containers;

/**
 * Interface for an implementation of a scheme-like list.
 * Author:   Sergei Golitsinski.
 * Created:  March 7, 2004.
 * Modified: May 24, 2004.
 */
public interface SchemeList extends Container 
{
	public Object car     (); 
	public SchemeList cdr (); 
	public void cons      (Object obj); 
	public void append    (SchemeList list);
}