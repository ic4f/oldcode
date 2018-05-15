package visualsort;

/**
 * 
 * Author:   James Gosling.
 * Created:  Jan 31, 1995.
 * Modified: Dec 21, 2004 by Sergei Golitsinski
 */
public class BubblesortAlgorithm extends SortingAlgorithm
{
	public BubblesortAlgorithm() {}
	
	public void sort(int[] array)
	{
		for (int i = array.length; --i >= 0; ) 
	  	{
			boolean flipped = false;
			for (int j = 0; j < i; j++) 
			{
				if (array[j] > array[j+1]) 
				{
					int T = array[j];
					array[j] = array[j+1];
					array[j+1] = T;
					flipped = true;					
		  		}
				incrementBasicOps();
		  		animate(i,j);
			}
			if (!flipped) 
				return;
		}
	}	
	
	public String name() { return "bubble sort"; }
}