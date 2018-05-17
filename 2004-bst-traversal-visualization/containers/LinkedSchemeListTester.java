package containers;

/**
 * Tester for linked scheme-like lists.
 * Author:   Sergei Golitsinski.
 * Created:  March 7, 2004.
 * Modified: May 25, 2004.
 * Comment:  The "cdr" method screwes up everything!
*/
public class LinkedSchemeListTester
{
	public static void main(String[] args)
	{
		LinkedSchemeList list1 = new LinkedSchemeList();
		list1.cons(new Integer(11));
		list1.cons(new Integer(12));
		list1.cons(new Integer(13));		
		list1.cons(new Integer(14));		
		System.out.println("size of list 1: " + list1.size());
		System.out.println("first element of list 1: " + list1.car());

		LinkedSchemeList list3 = new LinkedSchemeList();
		list3.cons(new Integer(211));
		list3.cons(new Integer(222));
		list3.cons(new Integer(213));		
		list3.cons(new Integer(214));	
		
		LinkedSchemeList list2 = (LinkedSchemeList)list1.cdr();
		System.out.println("size of list 2: " + list2.size());
		System.out.println("first element of list 2: " + list2.car());
		
		System.out.print("list 1: ");		
		list1.print();
		System.out.println();		
		System.out.print("list 2: ");		
		list2.print();
		System.out.println();		
		System.out.print("list 3: ");		
		list3.print();
		
		System.out.println();		
		System.out.print("list1.append(list 3): ");			
		list1.append(list3);			
		list1.print();
		System.out.println();		
		System.out.print("list1.cons(list 2): ");			
		list1.cons(list2);			
		list1.print();		
		System.out.println();

		System.out.println();		
		System.out.print("list 1: ");		
		list1.print();
		
		System.out.println();		
		System.out.print("list 2: ");		
		list2.print();
		
		System.out.println();		
		System.out.print("list 3: ");		
		list3.print();
		System.out.println();
				
		System.out.println("list 1 size: " + list1.size());		
		System.out.println("list 2 size: " + list2.size());	
		System.out.println("list 3 size: " + list3.size());				
		list1.clear();
		list2.clear();		
		list3.clear();		
		System.out.println("list 1 size: " + list1.size());		
		System.out.println("list 2 size: " + list2.size());		
		System.out.println("list 3 size: " + list3.size());		
	}
}