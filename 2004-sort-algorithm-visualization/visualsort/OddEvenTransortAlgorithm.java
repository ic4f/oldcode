package visualsort;

/**
 * 
 * Author:   Andrew Kitchen.
 * Created:  22 Nov 1995
 * Modified: Dec 21, 2004 by Sergei Golitsinski
 */
public class OddEvenTransortAlgorithm extends SortingAlgorithm
{
	public OddEvenTransortAlgorithm() {}
	
	public void sort(int[] a)
	{
		animate(0,a.length-1);
	  	for (int i = 0; i < a.length/2; i++ ) 
	  	{
			for (int j = 0; j+1 < a.length; j += 2) 
				if (a[j] > a[j+1]) 
				{
					incrementBasicOps();
					int T = a[j];
					a[j] = a[j+1];
					a[j+1] = T;
				}
				animate(); animate();
				
		  		for (int j = 1; j+1 < a.length; j += 2) 
					if (a[j] > a[j+1]) 
					{
						incrementBasicOps();
						int T = a[j];
					 	a[j] = a[j+1];
					 	a[j+1] = T;
					}
					animate(); animate();
	  			}	
	  			animate(-1,-1);			
  	}
  		
	public String name() { return "odd-even transposition sort"; }
}
