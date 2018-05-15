package visualtree;

/**
 * 
 * Author:   Sergei Golitsinski.
 * Created:  June 6, 2004.
 * Modified: June 6, 2004.
 */
public class VisualNode implements Comparable
{
	public static int TO_VISIT = 0;
	public static int CURRENT  = 1;
	public static int VISITED  = 2;
	
	private int state;
	private Comparable value;
	private int level;
	private int x; 
	private int y;

	public VisualNode(Comparable c)
	{
		state = 0;
		value = c;
		level = 0;
		x = 0;
		y = 0;
	}
	
	public int compareTo(Object o)
	{
		return value.compareTo( ((VisualNode)o).getValue() );
	}
		
	public Object getValue() { return value; }	
	
	public int getState() { return state; }
	public int getLevel() { return level; }
	public int getX()     { return x; }
	public int getY()     { return y; }
	
	public void setLevel(int l)     { level = l; }
	public void setX(int i)         { x = i; }
	public void setY(int i)         { y = i; }
	public void setState(int state) { this.state = state; }
}
