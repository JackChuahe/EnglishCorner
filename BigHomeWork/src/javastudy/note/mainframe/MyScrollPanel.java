package javastudy.note.mainframe;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Component;
import java.awt.Dimension;
import java.awt.GridBagLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.List;

import javastudy.imagepanel.MyImagePanel;
import javastudy.note.mainframe.MainFuntionFrame.DeleteImageAction;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.WindowConstants;

/**
 * 自定义滚动视图
 * 
 * @author JackCai
 *
 */
public class MyScrollPanel extends JPanel {
	private List<String> imageUrl; // 图片地址
	private JButton btnPrevious;// 点击向前查看图片
	private JButton btnNext; // 点击查看下一张图片
	private JPanel centerPanel = new JPanel();
	private DeleteImageAction deleteAction = new DeleteImageAction(); // 删除的响应事件
	private int imageIndex;
	private static final int WIDTH = 150;
	private static final int HEIGHT = 230;

	/**
	 * 构造函数
	 * 
	 * @param imageUrl
	 */
	public MyScrollPanel(List<String> imageUrl) {
		this.imageUrl = imageUrl;

		initUI(); // 初始化UI
		addActions(); // 添加事件
	}

	/**
	 * 添加事件
	 */
	private void addActions() {

	}

	/**
	 * 初始化UI
	 */
	private void initUI() {
		this.setLayout(new BorderLayout()); // 设置borderklayout

		btnPrevious = new JButton("<"); // 向前

		btnPrevious.setPreferredSize(new Dimension(15, 50));
		JPanel tempPane1 = new JPanel(new GridBagLayout());
		tempPane1.add(btnPrevious);
		this.add(tempPane1, BorderLayout.WEST);

		btnNext = new JButton(">"); // 向后
		btnNext.setPreferredSize(new Dimension(15, 50));
		JPanel tempPane2 = new JPanel(new GridBagLayout());
		tempPane2.add(btnNext);
		this.add(tempPane2, BorderLayout.EAST);

		centerPanel.setBackground(Color.gray);// 蓝色背景
		centerPanel.setPreferredSize(new Dimension(WIDTH , HEIGHT +9));
		this.add(centerPanel, BorderLayout.CENTER);

		showImage(imageIndex);
	}

	/**
	 * 
	 * @param imageIndex2
	 */
	private void showImage(int imageIndex2) {
		if (imageUrl != null && imageUrl.size() > 0) {
			centerPanel.removeAll();
			MyImagePanel currentImage = new MyImagePanel(
					imageUrl.get(imageIndex2), WIDTH , HEIGHT  );
			currentImage.getMoveToTrash().addActionListener(deleteAction);
			centerPanel.add(currentImage);
		} else {// 显示默认图片
			MyImagePanel defaultImagePanel = new MyImagePanel(
					"res/testPic.jpg", WIDTH - 20 , HEIGHT - 33);
			defaultImagePanel.setEnabled(false);
			centerPanel.add(defaultImagePanel);
		}

		// centerPanel

	}

	/**
	 * 
	 * @param args
	 */
	public static void main(String[] args) {
		JFrame mainFrame = new JFrame("test");
		mainFrame.add(new MyScrollPanel(null));

		mainFrame.pack();
		mainFrame.setLocation(450, 180);
		mainFrame.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
		mainFrame.setVisible(true);
	}

	/***
	 * 删除图片的事件
	 * 
	 * @author JackCai
	 *
	 */
	class DeleteImageAction implements ActionListener {
		@Override
		public void actionPerformed(ActionEvent e) {

			imageUrl.remove(imageIndex);
			showImage(imageIndex % imageUrl.size());
		}
	}
}
