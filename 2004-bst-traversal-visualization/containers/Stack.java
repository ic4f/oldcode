package containers;

/**
 * Base stack interface. 
 * Author:   Sergei Golitsinski.
 * Created:  May 24, 2004.
 * Modified: May 24, 2004.
 */
public interface Stack extends Container
{
	public void push (Object obj);
	public Object pop();
	public Object peek();
}
