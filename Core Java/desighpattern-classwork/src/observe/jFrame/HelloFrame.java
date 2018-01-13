package observe.jFrame;

import java.awt.FlowLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JPanel;

@SuppressWarnings("serial")
public class HelloFrame extends JFrame {

	private JButton button;
	private JPanel panel;

	public HelloFrame() {
		panel = new JPanel();
		button = new JButton();
		createFrame();
	}

	public void createFrame() {
		panel.setLayout(new FlowLayout());
		button.setText("Hello");
		button.addActionListener(new ActionListener() {

			@Override
			public void actionPerformed(ActionEvent arg0) {
				System.out.println("Hello...");
			}
		});
		button.addActionListener(new ActionListener() {

			@Override
			public void actionPerformed(ActionEvent arg0) {
				System.out.println("GoodBye....");
			}
		});
		panel.add(button);
		add(panel);
		setSize(200, 200);
		setVisible(true);
	}
}
