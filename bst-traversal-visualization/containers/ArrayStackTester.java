package containers;

/**
 * tester for ArrayStack
 * Author:   Sergei Golitsinski.
 * Created:  May 24, 2004.
 * Modified: May 24, 2004.
 */
public class ArrayStackTester
{
	public static void main(String[] args)
	{
		ArrayStack test = new ArrayStack();
		test.push(new Integer(1));
		test.push(new Integer(2));
		test.push(new Integer(3));		
		test.push(new Integer(4));		
		System.out.println("size: " + test.size() + ", top element: " + test.peek());
		test.pop();
		System.out.println("size: " + test.size() + ", top element: " + test.peek());		
	}
}