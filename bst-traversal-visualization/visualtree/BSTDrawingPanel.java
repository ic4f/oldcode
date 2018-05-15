package visualtree;

import java.awt.*;
import javax.swing.*;
import trees.*;

/**
 * 
 * Author:   Sergei Golitsinski.
 * Created:  Jun 6, 2004.
 * Modified: Jul 13, 2004.
 */
public class BSTDrawingPanel extends JPanel
{
	public static int PRE_ORDER   = 0;
	public static int IN_ORDER    = 1;
	public static int POST_ORDER  = 2;
	public static int LEVEL_ORDER = 3;
		
	private BinarySearchTree tree;
	private BSTDisplayPanel parent;
	
	public BSTDrawingPanel(BSTDisplayPanel panel, int width, int height)
	{
		super();
		setBackground(Color.WHITE);
		tree = new BinarySearchTree();
		parent = panel;
		setPreferredSize(new Dimension(width, height));		
	}
	
	public void insertNode(Comparable c)
	{
		tree.insert(new VisualNode(c));
		parent.getOutput().append("inserting " + c.toString() + "\n");	
		setCoordinates(tree.getRoot(), initialX(), initialY(), 0);
		adjustCoordinates(tree.getRoot(), null);	
	}

	public void paint( Graphics g )
	{
		super.paint(g);		
		if (!tree.isEmpty())
		{
			resetPanelSize();
			paintNodes(tree.getRoot(), g);
		}			
	}	
	
	public void resetTree()
	{
		tree.clear();
		repaint();
	}
	
	public void resetNodes()
	{
		if (!tree.isEmpty())
			for (BinaryTreeIterator i = tree.makePreOrderIterator(); i.hasNext();)
			{
				BinaryTreeNode n = (BinaryTreeNode)i.next();
				VisualNode vn = (VisualNode)n.getKey();	
				vn.setState(VisualNode.TO_VISIT);	
			}	
		repaint();
	}	
	
	public void traverseTree(int order)
	{		
		if (tree.isEmpty())
			return;
			
		BinaryTreeIterator iterator = null;
		if (order == PRE_ORDER)
		{
			iterator = tree.makePreOrderIterator();
			parent.getOutput().append("\nPre-order traversal: ");			
		}
		else if (order == IN_ORDER)
		{
			iterator = tree.makeInOrderIterator();
			parent.getOutput().append("\nIn-order traversal: ");
		}					
		else if (order == POST_ORDER)
		{
			iterator = tree.makePostOrderIterator();
			parent.getOutput().append("\nPost-order traversal: ");
		}			
		else if (order == LEVEL_ORDER)
		{
			iterator = tree.makeLevelOrderIterator();		
			parent.getOutput().append("\nLevel-order traversal: ");
		}		
	
		while (iterator.hasNext())
		{
			BinaryTreeNode n = (BinaryTreeNode)iterator.next();
			VisualNode vn = (VisualNode)n.getKey();			
			vn.setState(VisualNode.CURRENT);	
			paint(getGraphics());
			parent.getOutput().append(vn.getValue().toString() + " ");							
			try { Thread.sleep(500); }
			catch (InterruptedException e) { }
			vn.setState(VisualNode.VISITED);	
		}	
		repaint();
	}
	
		
	/*------------------------PRIVATE METHODS-------------------------*/	
	private void adjustCoordinates(BinaryTreeNode node, BinaryTreeNode parent)
	{		
		if (parent != null)
			adjustNodeX(node, parent);
						 
		if (node.getLeft() != null)
			adjustCoordinates(node.getLeft(), node);
		if (node.getRight() != null)
			adjustCoordinates(node.getRight(), node);			
	}		
	
	private void adjustNodeX(BinaryTreeNode node, BinaryTreeNode parent)
	{				
		VisualNode parentNode = (VisualNode)parent.getKey();			
		VisualNode thisNode = (VisualNode)node.getKey();
	
		for (BinaryTreeIterator i = tree.makePreOrderIterator(); i.hasNext();)
		{
			BinaryTreeNode n = (BinaryTreeNode)i.next();
			VisualNode newNode = (VisualNode)n.getKey();
	
			if (nodesCollide(thisNode, newNode))
			{
				parentNode.setX(parentNode.getX() - incrementX());
				setCoordinates(parent, parentNode.getX(), parentNode.getY(), parentNode.getLevel());
				adjustCoordinates(tree.getRoot(), null);
			}
		}	
	}
		
	private int initialX() { return getWidth()/2; }
	
	private int initialY() { return 20;	}

	private int incrementX() { return 10;}
	
	private int incrementY() { return 40;}
	
	private boolean nodesCollide(VisualNode left, VisualNode right)
	{
		return (	(left.getLevel() == right.getLevel()) &&
				 	(left.compareTo(right) == -1) &&
				 	(left.getX() + incrementX() * 2) > right.getX()	);
	}
	
	private void paintNodes(BinaryTreeNode node, Graphics g)
	{
		VisualNode vn = (VisualNode)node.getKey();
		if (vn.getState() == VisualNode.CURRENT)
			g.setColor(Color.RED);
		else if (vn.getState() == VisualNode.VISITED)
			g.setColor(Color.BLUE);
				
		g.drawString(vn.getValue().toString(), vn.getX(), vn.getY());
		g.setColor(Color.BLACK);
		
		if (node.getLeft() != null)
			paintBranch(vn.getX(), vn.getY(), node.getLeft(), g);
		if (node.getRight() != null)
			paintBranch(vn.getX(), vn.getY(), node.getRight(), g);
	}
	
	private void paintBranch(int x, int y, BinaryTreeNode child, Graphics g)
	{
		VisualNode vn = (VisualNode)child.getKey();
		g.drawLine(x+2,y+2, vn.getX()+2, vn.getY()-12);	
		paintNodes(child, g);		
	}
	
	private void resetPanelSize()
	{
		int width = treeBreadth() * incrementX();
		int height = tree.levels() * incrementY() + initialY();
		setPreferredSize(new Dimension(width, height));
		revalidate();
	}
	
	private void setCoordinates(BinaryTreeNode node, int x, int y, int level)
	{		
		VisualNode vn = (VisualNode)node.getKey();
		vn.setLevel(level);
		vn.setX(x);
		vn.setY(y);
		level++;
			
		if (node.getLeft() != null)
			setCoordinates(node.getLeft(), x-incrementX(), y+incrementY(), level);
		if (node.getRight() != null)
			setCoordinates(node.getRight(), x+incrementX(), y+incrementY(), level);			
	}
		
	private int treeBreadth()
	{
		int currentLevel = 0;
		int currentNodes = 0;
		int maxNodes = 0;
		
		for (BinaryTreeIterator i = tree.makeLevelOrderIterator(); i.hasNext();)
		{
			BinaryTreeNode n = (BinaryTreeNode)i.next();
			VisualNode vn = (VisualNode)n.getKey();

			if (vn.getLevel() == currentLevel)
				currentNodes++;
			else
			{
				currentLevel = vn.getLevel();
				currentNodes = 1;
			}
			maxNodes = Math.max(maxNodes, currentNodes);
		}	
		return maxNodes;
	}
}


