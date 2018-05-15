package containers;

/**
 * Base queue interface. 
 * Author:   Sergei Golitsinski.
 * Created:  May 24, 2004.
 * Modified: May 24, 2004.
 */
public interface Queue extends Container
{
	public void add (Object obj);
	public Object remove();
	public Object peek();
}
