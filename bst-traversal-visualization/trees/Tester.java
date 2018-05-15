package trees;
/**
 * Tester for binary trees.
 * Author:   Sergei Golitsinski.
 * Created:  May 24, 2004.
 * Modified: May 24, 2004.
 */
public class Tester
{
	public static void main(String[] args)
	{
		// construct trees & insert nodes
		BinarySearchTree tree1 = new BinarySearchTree();
		TraversableBinarySearchTree tree2 = new TraversableBinarySearchTree();		
		System.out.println("insert 11");
		tree1.insert(new Integer(11));
		tree2.insert(new Integer(11));		
		System.out.println("insert 2");		
		tree1.insert(new Integer(2));
		tree2.insert(new Integer(2));		
		System.out.println("insert 1");		
		tree1.insert(new Integer(1));		
		tree2.insert(new Integer(1));		
		System.out.println("insert 7");		
		tree1.insert(new Integer(7));
		tree2.insert(new Integer(7));		
		System.out.println("insert 3");		
		tree1.insert(new Integer(3));
		tree2.insert(new Integer(3));	
		System.out.println("insert 20");		
		tree1.insert(new Integer(20));
		tree2.insert(new Integer(20));		
		System.out.println("insert 17");		
		tree1.insert(new Integer(17));
		tree2.insert(new Integer(17));		
		System.out.println("insert 23");		
		tree1.insert(new Integer(23));				
		tree2.insert(new Integer(23));	
						
		
		/* ------- pre-order traverse ----------*/
		System.out.println("\n Test the tree pre-order traversal:");	
		tree2.traverse(1);
		
		System.out.println("\n TEST THE PRE-ORDER ITERATOR:");				
	
		for (BinaryTreeIterator i = tree1.makePreOrderIterator(); i.hasNext();)
		{
			BinaryTreeNode n = (BinaryTreeNode)i.next();
			if (n == null) 
				System.out.println("node: null");	
			else
				System.out.println("node: " + n.getKey());			
		}		
	
		/* ------- in-order traverse ----------*/		
		System.out.println("\n Test the tree in-order traversal:");	
		tree2.traverse(2);
		
		System.out.println("\n TEST THE IN-ORDER ITERATOR:");				
	
		for (BinaryTreeIterator i = tree1.makeInOrderIterator(); i.hasNext();)
		{
			BinaryTreeNode n = (BinaryTreeNode)i.next();
			if (n == null) 
				System.out.println("node: null");	
			else
				System.out.println("node: " + n.getKey());			
		}	
				
		/* ------- post-order traverse ----------*/				
		System.out.println("\n Test the tree post-order traversal:");	
		tree2.traverse(3);

		System.out.println("\n TEST THE POST-ORDER ITERATOR:");
		
		for (BinaryTreeIterator i = tree1.makePostOrderIterator(); i.hasNext();)
		{
			BinaryTreeNode n = (BinaryTreeNode)i.next();
			if (n == null) 
				System.out.println("node: null");	
			else
				System.out.println("node: " + n.getKey());			
		}
		
		/* ------- level-order traverse ----------*/		
		System.out.println("\n TEST THE LEVEL-ORDER ITERATOR:");
		
		for (BinaryTreeIterator i = tree1.makeLevelOrderIterator(); i.hasNext();)
		{
			BinaryTreeNode n = (BinaryTreeNode)i.next();
			if (n == null) 
				System.out.println("node: null");	
			else
				System.out.println("node: " + n.getKey());			
		}	

		/* ------- get tree data ----------*/
		System.out.println("\nTree size: " + tree1.size());		
		System.out.println("Tree levels: " + tree1.levels());				
		System.out.println("\ntree contains 23: " + tree1.contains(new Integer(23)));	
		System.out.println("\ntree contains 123: " + tree1.contains(new Integer(123)));	

		System.out.println("\nSearch for 23:");
		if (tree1.find(new Integer(23)) != null)
			System.out.println("23 found!");			
		else			
			System.out.println("23 NOT found!");				
			
		System.out.println("\nSearch for 123:");
		if (tree1.find(new Integer(123)) != null)
			System.out.println("123 found!");			
		else			
			System.out.println("123 NOT found!");		
						
		System.out.println("\nAverage number of comparisons during successful search: " + tree1.avgSuccessfulSearch());		
		System.out.println("\nAverage number of comparisons during unsuccessful search: " + tree1.avgUnsuccessfulSearch());				
	}
}