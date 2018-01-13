package observe.jFrame;

import java.awt.FlowLayout;
import java.awt.event.ItemEvent;
import java.awt.event.ItemListener;

import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.JToggleButton;

public class ObserveFrame {

	public static void main(String[] args) {
		JFrame frame = new JFrame();
		JPanel panel = new JPanel();
		panel.setLayout(new FlowLayout());

		JToggleButton button = new JToggleButton();
		button.setText("Hello");
		button.addItemListener(new ItemListener() {

			@Override
			public void itemStateChanged(ItemEvent arg0) {
				if (button.isSelected()) {
					System.out.println("Hello......");
				} else {
					System.out.println("GoodBye....");
				}
			}
		});

		panel.add(button);
		frame.add(panel);
		frame.setSize(400, 400);
		frame.setVisible(true);
	}
}
