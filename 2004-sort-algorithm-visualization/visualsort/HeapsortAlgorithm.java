package visualsort;

/**
 * 
 * Author:   Jason Harrison.
 * Created:  Jun 23, 1995
 * Modified: Dec 22, 2004 by Sergei Golitsinski.
 */
public class HeapsortAlgorithm extends SortingAlgorithm
{
	public HeapsortAlgorithm() {}

	public void sort(int[] a)
	{
		int N = a.length;
		for (int k = N/2; k > 0; k--) 
		{
			downheap(a, k, N);
			animate();
		}
		do 
		{
			int T = a[0];
			a[0] = a[N - 1];
			a[N - 1] = T;
			N = N - 1;
			animate(N);
			downheap(a, 1, N);
			incrementBasicOps();
		} 
		while (N > 1);
	}		
	
	private void downheap(int a[], int k, int N)
	{
  		int T = a[k - 1];
  		while (k <= N/2) 
  		{
			int j = k + k;
			if ((j < N) && (a[j - 1] < a[j])) 
				j++;
			
			if (T >= a[j - 1]) 
				break;
			else 
			{
				a[k - 1] = a[j - 1];
				k = j;
	  			animate();
			}
			incrementBasicOps();//2 comparisons
			incrementBasicOps();
  		}
		a[k - 1] = T;
		animate();
	}
	
	public String name() { return "heap sort"; }
}