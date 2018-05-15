package visualsort;

/**
 * 
 * Author:   Sergei Golitsinski.
 * Created:  Dec 22, 2004.
 * Modified: Dec 22, 2004.
 */
public class SelectionsortAlgorithm extends SortingAlgorithm
{
	public SelectionsortAlgorithm() {}
	
	public void sort(int[] a)
	{
		for (int i = 0; i < a.length - 1; i++)
		{
			int min = i;
			for (int j = i + 1; j <  a.length; j++)
			{
				if (a[j] < a[min])
					min = j;
				animate(i,j);
				incrementBasicOps();
			}				
			int temp = a[i];
			a[i] = a[min];
			a[min] = temp;
			animate(i, a.length - 1);
		}
	}		
	
	public String name() { return "selection sort"; }
}
