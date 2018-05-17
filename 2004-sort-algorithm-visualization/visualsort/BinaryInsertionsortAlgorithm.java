package visualsort;

/**
 * 
 * Author:   Sergei Golitsinski.
 * Created:  Dec 22, 2004.
 * Modified: Dec 22, 2004.
 */
public class BinaryInsertionsortAlgorithm extends SortingAlgorithm
{
	public BinaryInsertionsortAlgorithm() {}
	
	public void sort(int[] a)
	{
		for (int i = 1; i < a.length; i++) 
		{
			int v = a[i];
			int insertPosition = findInsertPosition(a, 0, i, a[i]);
			for (int j = i - 1; j >= insertPosition; j--)
			{
				a[j + 1] = a[j];
				incrementBasicOps();
				animate(i, j);	
			}
			a[insertPosition] = v;
	
		}
	}		
	
	private int findInsertPosition(int[] input, int left, int right, int value)
	{
		if (left == right)
			return left;
        
		int midpoint = (left + right) / 2;
        
		if (value > input[midpoint])
			return findInsertPosition(input, midpoint + 1, right, value);
		else if (value < input[midpoint])
			return findInsertPosition(input, left, midpoint, value);
        
		return midpoint;		
	}	
	
	public String name() { return "binary insertion sort"; }
}
