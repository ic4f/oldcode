package visualsort;

/**
 * 
 * Author:   Sergei Golitsinski.
 * Created:  Nov 27, 2004.
 * Modified: Nov 27, 2004.
 */
public abstract class SortingAlgorithm implements Runnable
{
	private int[] array; 
	private SortingDisplay display;
	private int cursorOne;
	private int cursorTwo;
	private int[] cursors;
	private int basicOps;
	
	public SortingAlgorithm() 
	{
		array = null;
		display = null;
		cursorOne = -1;
		cursorTwo = -1;
		cursors = null;
		basicOps = 0;	
	}
	
	public abstract void sort(int[] array);

	public abstract String name();
		
	public void init(SortingDisplay p, int[] a)
	{
		display = p;
		array = a;
		cursors = new int[a.length];
	}
	
	public void run()
	{
		basicOps = 0;
		sort(array); //passes the array for direct manipulation by subclasses
		display.repaint(); //neccessary to paint the last sorting iteration
		display.done(name(), basicOps);
	}
	
	public int[] array() { return array; }
	
	public int cursorOne() { return cursorOne; } 
	
	public int cursorTwo() { return cursorTwo; }

	public int[] cursors() { return cursors; }
		
	public void incrementBasicOps() { basicOps++; }
	
	public int getBasicOps() { return basicOps; }

	protected void animate(int a) 
	{ 
		cursorOne = a;
		display.animate();
	}
	
	protected void animate(int a, int b) 
	{ 
		cursorOne = a;
		cursorTwo = b;
		display.animate();
	}
	
	protected void animate(int[] a)
	{
		for (int i = 0; i < a.length; i++)
			cursors[i] = a[i];			
		
		display.animate();
	}
		
	protected void animate() { display.animate(); }
}