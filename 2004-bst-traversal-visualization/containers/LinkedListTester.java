package containers;

/**
 * Tester for linked lists.
 * Author:   Sergei Golitsinski.
 * Created:  March 7, 2004.
 * Modified: May 25, 2004.
 */
public class LinkedListTester
{
	public static void main(String[] args)
	{
		LinkedList list = new LinkedList();
		list.addFront(new Integer(11));
		list.addFront(new Integer(311));
		list.addFront(new Integer(411));
		list.addFront(new Integer(151));						
		
		System.out.println("size of list: " + list.size());
		System.out.println("first element of list: " + list.getFront());

		System.out.println();		
		System.out.print("list: ");		
		list.print();
		
		list.addRear(new Character('T'));			

		System.out.println();		
		System.out.print("list: ");		
		list.print();
	
		list.removeRear();

		System.out.println();		
		System.out.print("list: ");		
		list.print();
		System.out.println();		
		System.out.println("last element of list: " + list.getRear());
		
		String test = "test";
		System.out.println("list contains new string object? " + list.contains(test));
		list.addRear(test);
		System.out.println("Object added. List contains new string object? " + list.contains(test));							
	}
}