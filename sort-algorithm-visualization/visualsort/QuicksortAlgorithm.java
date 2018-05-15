package visualsort;

/**
 * 
 * Author:   Sergei Golitsinski.
 * Created:  Nov 30, 2004.
 * Modified: Nov 30, 2004.
 */
public class QuicksortAlgorithm extends SortingAlgorithm
{
	public QuicksortAlgorithm() {}

	public void sort(int[] a)
	{
		quicksort(0, a.length - 1, a);
	}	

	public String name() { return "quicksort"; }	
	
	private void quicksort(int left, int right, int[] a)
	{
		animate(left, right);
		if (left < right)
		{
			int s = partition(left, right, a);
			quicksort(left, s, a);
			quicksort(s + 1, right, a);
		}		
	}

	private int partition(int left, int right, int[] a) 
	{
		animate(left, right);
		int pivot = a[left];
		int i = left - 1;
		int j = right + 1;
	
		while (i < j)
		{
			do { i++; incrementBasicOps(); } while (a[i] < pivot);
			 
			do { j--; incrementBasicOps(); }	while (a[j] > pivot);
							
			if (i < j) 
				swap(i, j, a);
		}
		return j;	
	}	

	private void swap(int v1, int v2, int[] a) 
	{			
		int temp = a[v1];
		a[v1] = a[v2];
		a[v2] = temp;
		incrementBasicOps();
		animate();
	}	
}