package containers;

/**
 * Tester for linked stacks.
 * Author:   Sergei Golitsinski.
 * Created:  March 7, 2004.
 * Modified: May 25, 2004.
 */
public class LinkedStackTester
{
	public static void main(String[] args)
	{
		LinkedStack test = new LinkedStack();
		test.push(new Integer(1));
		test.push(new Integer(2));
		test.push(new Integer(3));		
		test.push(new Integer(4));		
		System.out.println("size: " + test.size() + ", top element: " + test.peek());
		test.pop();
		System.out.println("size: " + test.size() + ", top element: " + test.peek());		
	}
}