package visualtree;

import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import displaytools.*;

/**
 * DisplayPanel specialized to graph a binary search tree
 * Author:   Sergei Golitsinski.
 * Created:  April 1, 2004.
 * Modified: June 6, 2004.
 */
public class BSTDisplayPanel extends DisplayPanel
{
	private BSTDrawingPanel display;
	private JTextArea output;
	private JTextField insertText;

	public  BSTDisplayPanel()
	{
		super("Binary Search Tree Grapher");
   }
   
   public JTextArea getOutput() { return output; }

	protected JComponent makeDrawingArea()
	{
		return display = new BSTDrawingPanel(this, 500, 300);
	}

	protected JComponent makeOutputArea()
	{ 
		return output = new JTextArea(10, 0); //10 rows, ignores width
	}
	
	protected JComponent makeControlArea()
	{
		JPanel p = new JPanel();
		p.setLayout( new GridLayout(3, 1) );
		p.setBorder( BorderFactory.createEmptyBorder( 10, 15, 10, 0) );		
		p.add(setupInsertPanel());
		p.add(setupTraversePanel());
		p.add(setupResetPanel());
		return p;
	}

	protected JPanel setupInsertPanel()
	{
		JPanel p = new JPanel();
		p.setLayout(new FlowLayout());	
		p.setBorder(BorderFactory.createCompoundBorder(	
			BorderFactory.createTitledBorder("Insert"),
			BorderFactory.createEmptyBorder(0, 5, 5, 5)));		
				
		insertText = new JTextField(5);
		p.add(insertText);
		
		JButton button = new JButton("Insert");
		button.addActionListener(new InsertButtonListener());
		
		p.add(button);
		return p;
	}
	
	protected JPanel setupTraversePanel()
	{
		JPanel p = new JPanel();
		p.setLayout( new BoxLayout( p, BoxLayout.Y_AXIS ) );		
		p.setBorder(BorderFactory.createCompoundBorder(	
			BorderFactory.createTitledBorder("Traverse"),
			BorderFactory.createEmptyBorder(0, 5, 5, 5)));		
				
		JButton preOrder = new JButton("Pre-Order");
		preOrder.addActionListener(
			new TraverseButtonListener(BSTDrawingPanel.PRE_ORDER));

		JButton inOrder = new JButton("In-Order");
		inOrder.addActionListener(
			new TraverseButtonListener(BSTDrawingPanel.IN_ORDER));
		
		JButton postOrder = new JButton("Post-Order");
		postOrder.addActionListener(
			new TraverseButtonListener(BSTDrawingPanel.POST_ORDER));
		
		JButton levelOrder = new JButton("Level-Order");
		levelOrder.addActionListener(
			new TraverseButtonListener(BSTDrawingPanel.LEVEL_ORDER));	

		JButton resetNodes = new JButton("Reset Nodes");
		resetNodes.addActionListener(
			new ResetNodesButtonListener());
							
		p.add(preOrder);
		p.add(inOrder);
		p.add(postOrder);
		p.add(levelOrder);
		p.add(resetNodes);		
		return p;
	}
	
	protected JPanel setupResetPanel()
	{
		JPanel p = new JPanel();
		p.setBorder(BorderFactory.createCompoundBorder(	
			BorderFactory.createTitledBorder("Reset Tree"),
			BorderFactory.createEmptyBorder(0, 5, 5, 5)));		
				
		JButton button = new JButton("Reset");
		button.addActionListener(new ResetTreeButtonListener());
		
		p.add(button);
		return p;
	}
			
			
	/*----------------------------inner classes----------------------------------*/
	private class InsertButtonListener implements ActionListener
	{
		public void actionPerformed( ActionEvent e )
		{
			try { 
					Integer temp = new Integer(insertText.getText());
					display.insertNode(temp);									
					display.repaint(); 
					insertText.setText("");
					insertText.requestFocus();
				}
				catch (Exception ex) {
					System.err.println(ex);
				}				
		}
	}		
	
	private class TraverseButtonListener implements ActionListener
	{
		private int order;
		
		public TraverseButtonListener(int order) { this.order = order;	}
		
		public void actionPerformed( ActionEvent e )
		{
				display.traverseTree(order);
		}
	}
	
	private class ResetTreeButtonListener implements ActionListener
	{
		public void actionPerformed( ActionEvent e )
		{
			display.resetTree(); 
			output.setText("");
		}
	}			
	
	private class ResetNodesButtonListener implements ActionListener
	{
		public void actionPerformed( ActionEvent e )
		{
			display.resetNodes(); 
		}
	}
}