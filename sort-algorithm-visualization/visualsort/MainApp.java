package visualsort;

/**
 * 
 * Author:   Sergei Golitsinski.
 * Created:  Nov 29, 2004.
 * Modified: Nov 29, 2004.
 */
public class MainApp
{
	public static void main(String[] args)
	{
		int elements = 100;
		String[] classNames = new String[8];
		classNames[0] = "BinaryInsertionsortAlgorithm";
		classNames[1] = "BubblesortAlgorithm";
		classNames[2] = "HeapsortAlgorithm";		
		classNames[3] = "InsertionsortAlgorithm";
		classNames[4] = "OddEvenTransortAlgorithm";
		classNames[5] = "QuicksortAlgorithm";			
		classNames[6] = "SelectionsortAlgorithm";
		classNames[7] = "ShearsortAlgorithm";
		MainPanel p = new MainPanel(classNames, elements);
	}
}