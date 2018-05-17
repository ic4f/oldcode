package visualsort;

import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.event.*; 


/**
 * 
 * Author:   Sergei Golitsinski.
 * Created:  Nov 29, 2004.
 * Modified: Nov 29, 2004.
 */
public class MainPanel extends JFrame
{
	private LinesSortingDisplay display; //panel where the sorting takes place
	private JTextArea output;       //additional output area  
	private JPanel selectionPanel;  //panel for selecting algorithms
	private JPanel controlPanel;    //start/reset panel
	private JSlider delaySlider;    //paint delay slider (speed of sorting)
		
	public MainPanel(String[] algorithms, int elements)
	{
		display        = new LinesSortingDisplay(this, 640, 480, elements);
		output         = new JTextArea(10, 0);
		selectionPanel = setupSelectionPanel(algorithms);
		controlPanel   = setupControlPanel();
		delaySlider    = setupSlider();
		
		setTitle("Sorting Demo");
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);		
		getContentPane().add(setupMainPanel(), BorderLayout.CENTER);
		pack();
		show();
		
		disablePanel(controlPanel); //an algorithm must be selected first
	}			
	
	public void done(String name, int basicOps)
	{
		output.append(name + " (" + basicOps + ")\n");
	}
	
	//enables and resets all buttons within a panel
	private void enablePanel(JPanel p)
	{
		Component[] buttons = p.getComponents();
		for (int i = 0; i < buttons.length; i++)
		{
			AbstractButton b = (AbstractButton)buttons[i];			
			b.setEnabled(true);
			b.setSelected(false);
		}		
	}	
	
	//disables all buttons within a panel
	private void disablePanel(JPanel p)
	{
		Component[] buttons = p.getComponents();
		for (int i = 0; i < buttons.length; i++)
			buttons[i].setEnabled(false);
	}		
	
	private JPanel setupMainPanel()
	{
		JPanel p = new JPanel();
		p.setLayout(new BorderLayout());
		p.add(setupDisplayPanel(), BorderLayout.CENTER);
		p.add(setupControlArea(), BorderLayout.EAST);		
		p.setBorder( BorderFactory.createEmptyBorder( 10, 10, 10, 10) );
		return p;
	}

	private JSplitPane setupDisplayPanel() 
	{	
		JSplitPane p = new JSplitPane(JSplitPane.VERTICAL_SPLIT);
		p.setOneTouchExpandable(true);		
		JScrollPane sp1 = new JScrollPane(display);
		JScrollPane sp2 = new JScrollPane(output);
		p.setTopComponent(sp1);
		p.setBottomComponent(sp2);		
		return p;
	}

	private JComponent setupControlArea()
	{
		JPanel p = new JPanel();
		p.setLayout(new BorderLayout());
		p.setBorder( BorderFactory.createEmptyBorder( 10, 10, 1, 0) );		
		p.add(selectionPanel, BorderLayout.NORTH);
		p.add(makeSliderPanel(), BorderLayout.CENTER);	
		p.add(controlPanel, BorderLayout.SOUTH);	
		return p;
	}
	
	private JPanel setupControlPanel()
	{
		JPanel p = new JPanel();
		p.setLayout( new GridLayout(0, 1) );	
		p.setBorder(BorderFactory.createEmptyBorder(10, 2, 0, 1));
		JButton start = new JButton("Start");
		JButton reset = new JButton("Reset");
		start.addActionListener(new StartButtonListener());
		reset.addActionListener(new ResetButtonListener());
		start.setBackground(new Color(51, 204, 0));
		reset.setBackground(Color.RED);
		p.add(start);
		p.add(reset);		
		return p;		
	}
	
	private JPanel makeSliderPanel()
	{		
		JPanel p = new JPanel();
		p.setBorder(BorderFactory.createCompoundBorder(	
			BorderFactory.createTitledBorder("Set Demo Speed"),
			BorderFactory.createEmptyBorder(0, 5, 5, 5)));
			p.add(delaySlider);
		return p;	
	}
	
	private JSlider setupSlider()
	{
		delaySlider = new JSlider(0, 250);
		delaySlider.setMajorTickSpacing(50);
		delaySlider.setMinorTickSpacing(10);
		delaySlider.setPaintTicks(true);
		delaySlider.setPaintLabels(true);
		delaySlider.addChangeListener(new SliderListener());
		return delaySlider;		
	}
	
	private JPanel setupSelectionPanel(String[] algs)
	{
		JPanel p = new JPanel();
		p.setLayout( new GridLayout(0, 1) );	
		p.setBorder(BorderFactory.createCompoundBorder(	
			BorderFactory.createTitledBorder("Select Algorithms"),
			BorderFactory.createEmptyBorder(0, 5, 5, 5)));
		
		String prefix = getClass().getPackage().getName() + ".";
		
		try {			
			for (int i = 0; i < algs.length; i++)
			{
				//init SortingAlgorithm to access its name
				String className = prefix +  algs[i];
				SortingAlgorithm a = 
					(SortingAlgorithm)Class.forName(className).newInstance();

				JToggleButton b = new JToggleButton(a.name());
				b.addActionListener(new SelectButtonListener(className));
				p.add(b);				
			}
		}
		catch (InstantiationException e) { e.printStackTrace(); }
		catch (IllegalAccessException e) { e.printStackTrace(); }
		catch (ClassNotFoundException e) { e.printStackTrace(); }			
				
		return p;
	}
	
	/*-----------------------------inner classes------------------------------*/	
	private class SelectButtonListener implements ActionListener
	{
		private String className;
	
		public SelectButtonListener(String name) { className = name; }
	
		public void actionPerformed( ActionEvent e )
		{
			JToggleButton button = (JToggleButton)e.getSource();
			if (button.isSelected())
				MainPanel.this.display.addAlgorithm(className);
			else
				MainPanel.this.display.removeAlgorithm(className);	
				
			//if the SortingDisplay is empty - disable run/reset
			if (MainPanel.this.display.isEmpty())
				MainPanel.this.disablePanel(controlPanel);
			else
				MainPanel.this.enablePanel(controlPanel);				
		}
	}
	
	private class StartButtonListener implements ActionListener
	{
		public void actionPerformed( ActionEvent e )
		{
			MainPanel.this.disablePanel(selectionPanel);
			MainPanel.this.display.start(delaySlider.getValue());
		}		
	}
	
	private class ResetButtonListener implements ActionListener
	{
		public void actionPerformed( ActionEvent e )
		{
			MainPanel.this.display.reset();
			MainPanel.this.enablePanel(selectionPanel);
			MainPanel.this.disablePanel(controlPanel);
			MainPanel.this.output.setText("");
		}		
	}
	
	private class SliderListener implements ChangeListener
	{
		public void stateChanged(ChangeEvent e) 
		{
			MainPanel.this.display.setDelay(delaySlider.getValue());
		}
	}	
}