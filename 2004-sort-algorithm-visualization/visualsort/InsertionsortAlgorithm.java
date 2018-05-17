package visualsort;

/**
 * 
 * Author:   Sergei Golitsinski.
 * Created:  Nov 28, 2004.
 * Modified: Nov 28, 2004.
 */
public class InsertionsortAlgorithm extends SortingAlgorithm
{
	public InsertionsortAlgorithm() {}
	
	public void sort(int[] a)
	{
		int v;
		int j;
		for (int i = 1; i < a.length; i++)
		{
			v = a[i];
			j = i - 1;
			while ( (j >= 0) && (a[j] > v) )
			{
				incrementBasicOps();
				a[j + 1] = a[j];
				j--;
				animate(i, j);								
			}
			a[j + 1] = v;
			animate(i, j);	
		}
	}		
	
	public String name() { return "insertion sort"; }
}