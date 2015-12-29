package javastudy.note.mainframe;

import java.awt.Color;
import java.awt.Component;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.Graphics;
import java.awt.Image;

import javax.swing.ImageIcon;
import javax.swing.JList;
import javax.swing.JPanel;
import javax.swing.ListCellRenderer;

public class MyCellRender extends JPanel implements ListCellRenderer {
	private static final int WIDTH = 60;
	private static final int HEIGHT = 85;
	private String bookName;
	private String note;
	private String label;
	private ImageIcon icon;
	private Color background;
	private Color foreground;
	private String time;

	@Override
	public Component getListCellRendererComponent(JList list, Object value,
			int index, boolean isSelected, boolean cellHasFocus) {

		MyNoteModel temp = (MyNoteModel) value;
		background = isSelected ? list.getSelectionBackground() : list
				.getBackground();
		foreground = isSelected ? list.getSelectionForeground() : list
				.getForeground(); // 返回该JPanel对象作为单元格绘制器

		if (!temp.getImageUrl().isEmpty()) {
			// 加载图片
			icon = new ImageIcon(temp.getImageUrl().get(0));
		} else {
			icon = new ImageIcon("userImages/default.png");
		}

		//
		this.time = temp.getTime();
		this.bookName = temp.getBookName();
		this.note = temp.getMyNote();
		this.label = temp.getLabel();

		System.out.println("正在重绘cell");
		icon.setImage(icon.getImage().getScaledInstance(WIDTH, HEIGHT,
				Image.SCALE_DEFAULT));

		return this;
	}

	// 重写paintComponent方法，改变JPanel的外观
	public void paintComponent(Graphics g) {

		// int imageWidth = icon.getImage().getWidth(null);
		// int imageHeight = icon.getImage().getHeight(null);
		g.setColor(background);
		g.fillRect(0, 0, getWidth(), getHeight());
		g.setColor(foreground);
		// 绘制好友图标
		g.drawImage(icon.getImage(), 5, 5, null);
		g.setFont(new Font("SansSerif", Font.BOLD, 18)); // 绘制书名
		g.drawString(bookName, WIDTH + 20, 20);
		g.setColor(Color.lightGray);
		g.setFont(new Font("SansSerif", Font.PLAIN, 16)); // 绘制内容
		g.drawString(note.substring(0, note.length() / 2) + "..", WIDTH + 30,
				40);
		g.setColor(Color.darkGray);
		g.setFont(new Font("SansSerif", Font.BOLD, 16)); // 绘制label
		g.drawString("Label : " + label, WIDTH + 30, 70);

		g.setColor(Color.black);
		g.setFont(new Font("SansSerif", Font.PLAIN, 13));
		g.drawString("记录于: " + this.time, this.getWidth() - 150,
				this.getHeight() - 15);
		//System.out.println(this.getHeight());

	}

	// // 通过该方法来设置该ImageCellRenderer的最佳大小
	public Dimension getPreferredSize() {
		return new Dimension(60, 80);
	}

}
