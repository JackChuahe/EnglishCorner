package javastudy.imagepanel;

import java.awt.Color;
import java.awt.Cursor;
import java.awt.Dimension;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Image;
import java.awt.Insets;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.event.MouseMotionListener;

import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;

public class MyImagePanel extends JPanel {
	private static final int WIDTH = 60;
	private static final int HEIGHT = 85;
	private JButton moveToTrash; // 删除按钮
	private MyImagePanel self = this;
	private Color defaultColor = this.getBackground();

	public JButton getMoveToTrash() {
		return moveToTrash;
	}

	private String imagePath;

	// 构造函数
	public MyImagePanel(String imagePath) {
		this.imagePath = imagePath;
		initUI(); // 初始化ui
		addAction(); // 添加事件
	}

	/**
	 * 
	 */
	private void addAction() {
		moveToTrash.addMouseMotionListener(new MouseMotionListener() {
			@Override
			public void mouseDragged(MouseEvent e) {
			}

			@Override
			public void mouseMoved(MouseEvent e) {
				moveToTrash.setCursor(Cursor
						.getPredefinedCursor(Cursor.HAND_CURSOR));

			}
		});
		// 改变颜色
		moveToTrash.addMouseListener(new MouseAdapter() {
			public void mouseExited(MouseEvent e) {
				moveToTrash.setCursor(Cursor
						.getPredefinedCursor(Cursor.DEFAULT_CURSOR));
			}
		});

		// 事件
		this.addMouseMotionListener(new MouseMotionListener() {
			@Override
			public void mouseDragged(MouseEvent e) {
			}

			@Override
			public void mouseMoved(MouseEvent e) {
				self.setBackground(Color.orange);
				self.setCursor(Cursor.getPredefinedCursor(Cursor.HAND_CURSOR));

			}
		});

		// 改变颜色
		this.addMouseListener(new MouseAdapter() {
			public void mouseExited(MouseEvent e) {
				self.setBackground(defaultColor);
				self.setCursor(Cursor
						.getPredefinedCursor(Cursor.DEFAULT_CURSOR));

			}
		});
	}

	/**
	 * 初始化UI界面
	 */
	private void initUI() {
		this.setLayout(new GridBagLayout());
		GridBagConstraints gbCon = new GridBagConstraints();

		JPanel tempPane1 = new JPanel();
		tempPane1.setPreferredSize(new Dimension(15, 1));
		// jbtn.setLocation(WIDTH - 20, 0);
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 0;
		gbCon.gridy = 0;
		gbCon.gridwidth = 1;
		this.add(tempPane1, gbCon);

		JPanel tempPane = new JPanel();
		tempPane.setPreferredSize(new Dimension(WIDTH - 15, 1));
		// jbtn.setLocation(WIDTH - 20, 0);
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 1;
		gbCon.gridy = 0;
		gbCon.gridwidth = 1;
		this.add(tempPane, gbCon);

		// this.setPreferredSize(new Dimension(WIDTH, HEIGHT)); // 设置宽和高
		ImageIcon img = new ImageIcon(imagePath);
		img.setImage(img.getImage().getScaledInstance(WIDTH, HEIGHT,
				Image.SCALE_DEFAULT));

		JLabel imageLabel = new JLabel(img);
		imageLabel.setPreferredSize(new Dimension(WIDTH, HEIGHT));
		gbCon.insets = new Insets(2, 3, 3, 3);
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 0;
		gbCon.gridy = 1;
		gbCon.gridwidth = 2;
		this.add(imageLabel, gbCon);

		ImageIcon imgTrash = new ImageIcon("res/trash.png");
		imgTrash.setImage(imgTrash.getImage().getScaledInstance(20, 20,
				Image.SCALE_DEFAULT));

		moveToTrash = new JButton(imgTrash);
		// jbtn.setForeground(Color.black);
		// jbtn.setBackground(Color.red);
		// jbtn.setLocation(WIDTH - 20, 0);
		moveToTrash.setPreferredSize(new Dimension(20, 20));
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 0;
		gbCon.gridy = 2;
		gbCon.gridwidth = 1;
		this.add(moveToTrash, gbCon);

		// JButton

	}

}
