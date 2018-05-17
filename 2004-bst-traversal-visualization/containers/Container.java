package containers;

import java.io.Serializable;

/**
 * Base interface for an ADT.
 * Author:   Sergei Golitsinski.
 * Created:  May 24, 2004.
 * Modified: May 24, 2004.
 */
public interface Container extends Serializable
{
	public int size();
	public void clear();
	public boolean isEmpty();
}
