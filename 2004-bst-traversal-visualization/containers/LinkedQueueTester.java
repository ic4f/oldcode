package containers;

/**
 * Tester for linked queues.
 * Author:   Sergei Golitsinski.
 * Created:  March 7, 2004.
 * Modified: May 25, 2004.
 */
public class LinkedQueueTester
{
	public static void main(String[] args)
	{
		LinkedQueue test = new LinkedQueue();
		test.add(new Integer(1));
		test.add(new Integer(2));
		test.add(new Integer(3));		
		test.add(new Integer(4));		
		System.out.println("size: " + test.size() + ", next element to remove: " + test.peek());
		test.remove();
		System.out.println("size: " + test.size() + ", next element to remove: " + test.peek());		
	}
}