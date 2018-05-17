package displaytools;

import java.awt.*;
import javax.swing.*;

/**
 * Sets up a panel with a display area and controls area. 
 * Contains a panel for custom painting.
 * Author:   Sergei Golitsinski.
 * Created:  April 1, 2004.
 * Modified: June 6, 2004.
 */

public abstract class DisplayPanel extends JFrame
{
	public  DisplayPanel(String title)
	{
		setTitle(title);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);		
		getContentPane().add(setupMainPanel(), BorderLayout.CENTER);
		pack();
		show();
   }
	
	private JPanel setupMainPanel()
	{
		JPanel p = new JPanel();
		p.setLayout(new BorderLayout());
		p.add(setupDisplayPanel(), BorderLayout.CENTER);
		p.add( makeControlArea(), BorderLayout.EAST);		
		p.setBorder( BorderFactory.createEmptyBorder( 10, 10, 10, 10) );
		return p;
	}

	private JSplitPane setupDisplayPanel() 
	{	
		JSplitPane p = new JSplitPane(JSplitPane.VERTICAL_SPLIT);
		p.setOneTouchExpandable(true);
		JScrollPane sp1 = new JScrollPane(makeDrawingArea());
		JScrollPane sp2 = new JScrollPane(makeOutputArea());		
		p.setTopComponent(sp1);
		p.setBottomComponent(sp2);		
		return p;
	}
	
	// control area factory
	protected abstract JComponent makeControlArea(); 
	
	// drawing area factory	 
	protected abstract JComponent makeDrawingArea();
	
	protected abstract JComponent makeOutputArea();
}