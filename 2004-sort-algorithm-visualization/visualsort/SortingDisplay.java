package visualsort;

import java.awt.*;
import javax.swing.*;
import java.util.Vector;

/**
 * Author:   Sergei Golitsinski.
 * Created:  Nov 25, 2004.
 * Modified: Dec 21, 2004.
 * Comment:  to achieve custom painting, 
 * 			 implement "protected void paintComponent(Graphics g)"
 */
public abstract class SortingDisplay extends JPanel
{
	private int elements;         //elements in arrays to sort
	private int paintDelay;       //speed of sorting demo
	private MainPanel parent;     //main GUI element/enclosing frame
	private Vector algorithms;    //holds names of sorting algorithms classes
	private Vector threads;       //holds threads (thread == running sorting demo)
	private ArrayFactory factory; //generates one random array for all algorithms 
	
	public SortingDisplay(MainPanel parent, int width, int height, int elements)
	{
		super();

		this.elements = elements;				
		threads       = new Vector();
		algorithms    = new Vector();
		factory       = getArrayFactory(); 
		paintDelay    = 0;		
		this.parent   = parent;
		
		setBackground(Color.WHITE);		
		setPreferredSize(new Dimension(width, height));
	}
	
	public void done(String name, int basicOps) { parent.done(name, basicOps); }
	
	public boolean isEmpty() { return algorithms.isEmpty(); }
	
	public void setDelay(int delay) { paintDelay = delay; }
	
	public void addAlgorithm(String className)
	{
		try {
			SortingAlgorithm a = 
				(SortingAlgorithm)Class.forName(className).newInstance();			
			int[] array = factory.getArray();
			a.init(this, array);
			algorithms.addElement(a);
			repaint();
		}
		catch (InstantiationException e) { e.printStackTrace(); }
		catch (IllegalAccessException e) { e.printStackTrace(); }
		catch (ClassNotFoundException e) { e.printStackTrace(); }
	}
	
	public void removeAlgorithm(String name)
	{
		for (int i = 0; i < algorithms.size(); i++)
		{
			SortingAlgorithm a = (SortingAlgorithm)algorithms.elementAt(i);
			String algName = a.getClass().getName();
			if (algName.compareTo(name) == 0)		
			{	
				algorithms.removeElementAt(i);
				break; //removes 1 occurance of this algorithm
			}
		}
		repaint();
	}
	
	public void start(int delay)
	{
		paintDelay = delay;
		for (int i = 0; i < algorithms.size(); i++)
		{
			Thread t = new Thread((SortingAlgorithm)algorithms.elementAt(i));
			threads.addElement(t);
			t.start();
		}
	}
	
	public void reset()
	{		
		//thread.stop() is depricated, but I can't see any reasonable way around it
		for (int i = 0; i < threads.size(); i++)
			( (Thread)threads.elementAt(i) ).stop(); 
		
		threads = new Vector();
		algorithms = new Vector();
		repaint();
	}
	
	public void animate()
	{
		try { Thread.sleep(paintDelay); }
		catch (InterruptedException e) { }
		repaint();
	}	
	
	protected int getElements() { return elements; }
	
	protected Vector getAlgorithms() { return algorithms; }
	
	protected abstract ArrayFactory getArrayFactory();
}