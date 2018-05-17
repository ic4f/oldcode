package visualsort;

import java.util.Random;

/**
 * 
 * Author:   Sergei Golitsinski.
 * Created:  Nov 28, 2004.
 * Modified: Nov 28, 2004.
 */
public class ArrayFactory
{
	public static final int RANDOM = 0;
	public static final int ASCENDING = 1;
	public static final int DESCENDING = 2;
	private int[] array;
	
	public ArrayFactory(int type, int elements, int pictureWidth) 
	{
		array = makeArray(type, elements, pictureWidth);
	}
	
	//returns the same randomly created array while arrayfactory is alive
	public int[] getArray()
	{
		int[] temp = new int[array.length];
		for (int i = 0; i < temp.length; i++)
			temp[i] = array[i];
		return temp;
	}
	
	private int[] makeArray(int type, int elements, int pictureWidth)
	{
		int[] array = new int[elements];
		
		if (type == RANDOM)
		{		
			Random r = new Random();
			for (int i = 0; i < array.length; i++)
			{
				int newPos = getPosition(array, r.nextInt(array.length-1));
				array[newPos] = pictureWidth / elements * i + 1;
			}
		}
		else if (type == ASCENDING)
			for (int i = 0; i < array.length; i++)
				array[i] = pictureWidth / elements * i + 1;
		
		else if (type == DESCENDING)
		{
			int j = array.length;
			for (int i = 0; i < array.length; i++)
				array[i] = pictureWidth / elements * j-- + 1;
		}		
		return array;
	}

	private int getPosition(int[] target, int pos)
	{
		if (target[pos] == 0)
			return pos;
		else	
			if (pos == target.length-1)
				return getPosition(target, 0);
			else
				return getPosition(target, pos+1);
	}
}