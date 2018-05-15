package visualsort;

import java.awt.*;
import java.util.Vector;
/**
 * 
 * Author:   Sergei Golitsinski.
 * Created:  Dec 21, 2004.
 * Modified: Dec 21, 2004.
 */
public class LinesSortingDisplay extends SortingDisplay
{
	private int lengthOfLine; //length of a line in pixels
		
	public LinesSortingDisplay(MainPanel parent, int width, int height, int elements)
	{	
		super(parent, width, height, elements);
		lengthOfLine  = elements * 2;
	}
	
	protected ArrayFactory getArrayFactory()
	{
		int elements = getElements();
		int lengthOfLine = elements * 2; //called before same inst.var. is init.
		return new ArrayFactory(ArrayFactory.RANDOM, elements, lengthOfLine);
	}
	
	protected void paintComponent(Graphics g)
		{
			super.paintComponent(g);
			int interval = 10;
			int left = interval; //leftMargin
			int top = interval; //topMargin
			int elements = getElements();
			int unitHeight = elements * 2;
			int maxInRow = getWidth() / (lengthOfLine + interval);
			int maxInColumn = getHeight() / unitHeight;
		
			Vector algorithms = getAlgorithms();
						
			for (int i = 0; i < algorithms.size(); i++)
			{
				if (i == maxInRow) //move to next row
				{
					top = top * 5 + unitHeight;
					left = interval;
				}			
				SortingAlgorithm alg = (SortingAlgorithm)algorithms.elementAt(i);
				drawArray(alg, g, left, top, elements);
				drawCursors(alg, g, left, top);
				left += lengthOfLine + interval;
			}
		}
		
		private void drawArray
			(SortingAlgorithm alg, Graphics g, int left, int top, int elements)
		{
			int[] array = alg.array();		
			g.setColor(Color.BLACK);	
			g.drawString
				(alg.name() + " (" + elements + ")", left, top + 15 + elements * 2);
			g.drawString("" + alg.getBasicOps(), left, top + 30 + elements * 2);
			
			for (int i = 0; i < array.length; i++)
				g.drawLine(left, top + 2 * i, left + array[i], top + 2 * i);			
		}
	
	private void drawCursors(SortingAlgorithm a, Graphics g, int left, int top)
	{
		g.setColor(Color.RED);	
		int c1 = a.cursorOne();
		if (c1 > -1)		
		{
			c1 = c1 * 2 + top;
			g.drawLine(left, c1, left + lengthOfLine, c1);
		}

		g.setColor(Color.BLUE);	
		int c2 = a.cursorTwo();	
		if (c2 > -1)
		{
			c2 = c2 * 2 + top;
			g.drawLine(left, c2, left + lengthOfLine, c2);
		}		
		
		int[] c3 = a.cursors();
		for (int i = 0; i < c3.length; i++)
		{
			if (c3[i] > -1)
			{
				int temp = c3[i] * 2 + top;
				g.drawLine(left, temp, left + lengthOfLine, temp);
				c3[i] = -1;
			}	
		}
	}
}