package javastudy.note.mainframe;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Component;
import java.awt.ComponentOrientation;
import java.awt.Dimension;
import java.awt.EventQueue;
import java.awt.FlowLayout;
import java.awt.Font;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Insets;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.util.ArrayList;
import java.util.List;

import javastudy.calendar.MyCalendarChooser;
import javastudy.imagepanel.MyImagePanel;

import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.DefaultListModel;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JDialog;
import javax.swing.JFileChooser;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JList;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTabbedPane;
import javax.swing.JTextArea;
import javax.swing.JTextField;
import javax.swing.ListSelectionModel;
import javax.swing.UIManager;
import javax.swing.WindowConstants;
import javax.swing.event.ChangeEvent;
import javax.swing.event.ChangeListener;
import javax.swing.event.ListSelectionEvent;
import javax.swing.event.ListSelectionListener;
import javax.swing.filechooser.FileNameExtensionFilter;

/**
 * 主功能界面
 * 
 * @author JackCai
 *
 */
public class MainFuntionFrame {
	private JFrame mainFrame = new JFrame();
	private JTabbedPane tabPane = new JTabbedPane(JTabbedPane.TOP,
			JTabbedPane.SCROLL_TAB_LAYOUT);
	private JTextField bookNameText; // 书名
	private JTextArea sourceContentsText; // 源内容
	private JTextArea notesText; // 读书笔记
	private JTextField labelText; // 标签
	private JPanel imagePanel; // 底部图片区域
	private JButton btnAddPic; // 添加照片的按钮
	private JButton btnSubmitNote; // 确认提交笔记 添加读书笔记按钮
	private JButton btnClearContents; // 清除所有数据
	private MyCalendarChooser calendar = new MyCalendarChooser(); // 日历
	private JPanel imagesPanel = new JPanel(new FlowLayout(FlowLayout.LEFT)); // 图片展示区
	private List<String> imagesPath = new ArrayList<String>();// 存储图片的路径
	private DeleteImageAction deleteAction = new DeleteImageAction(); // 删除的响应事件
	private JPanel myNotePage = new JPanel(new GridBagLayout()); // 我的笔记页面
	private JButton btnSearch = new JButton("Search"); // 搜索按钮
	private List<MyNoteModel> myNotes = new ArrayList<MyNoteModel>(); // 我的笔记的模型数组
	private JList list; // 数据
	private DefaultListModel<MyNoteModel> listModel = new DefaultListModel<MyNoteModel>(); // 模型
	private JComboBox searchComBox; // 第二个界面搜索使用的comBox
	private JTextField searchTextField; // 搜索框
	private JTextArea searchTextArea; // 全文检索使用的搜索框
	private JPanel alterPanel; // 搜索栏上面可替换的panel

	/**
	 * 构造函数
	 */
	public MainFuntionFrame() {
		initUI(); // 初始化界面

	}

	/**
	 * 增加点击事件
	 */
	private void addActions() {

		// 增加照片的按钮的事件
		btnAddPic.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				// MyImagePanel mip = new MyImagePanel("res/testPic.jpg");
				if (imagesPath.size() >= 5) {
					JOptionPane.showMessageDialog(null,
							"More Than 5 pictures!!", "Error",
							JOptionPane.ERROR_MESSAGE);
					return;
				}//
				JFileChooser chooser = new JFileChooser();
				FileNameExtensionFilter filter = new FileNameExtensionFilter(
						"jpg/gif/png", "jpg", "gif", "png");
				chooser.setFileFilter(filter);
				int returnValue = chooser.showOpenDialog(mainFrame);
				if (returnValue == JFileChooser.APPROVE_OPTION) {
					MyImagePanel mip = new MyImagePanel(chooser
							.getSelectedFile().getPath());
					imagesPanel.add(mip);
					imagesPanel.updateUI(); // 立即更新视图
					imagesPath.add(chooser.getSelectedFile().getPath());
					JButton btn = mip.getMoveToTrash();
					btn.addActionListener(deleteAction);
				}
			}
		});

		// 清除事件 点击之后清除所有数据
		btnClearContents.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				clearAll();
			}
		});
	}

	/**
	 * 初始化界面
	 */
	private void initUI() {
		// add list

		// add tab
		tabPane.add("   Add new note  ", null);
		tabPane.add("   My notes    ", null);
		tabPane.setToolTipTextAt(1, "my notes");
		tabPane.setToolTipTextAt(0, "add notes");
		// addTab(String title, Icon icon, Component component, String tip)
		//
		tabPane.setPreferredSize(new Dimension(755, 460));
		mainFrame.add(tabPane);
		// 添加一个固定的标题
		JPanel topTitle = new JPanel(new FlowLayout(FlowLayout.RIGHT));
		topTitle.setPreferredSize(new Dimension(710, 50));
		JButton cancel = new JButton("Exit"); // 注销按钮
		topTitle.add(cancel);
		mainFrame.add(topTitle, BorderLayout.NORTH);

		// 添加事件
		tabPane.addChangeListener(new ChangeListener() {
			@Override
			public void stateChanged(ChangeEvent e) {
				if (tabPane.getSelectedComponent() == null) {
					// 获取所选Tab页
					int n = tabPane.getSelectedIndex();
					// 为指定标前页加载内容
					loadTab(n);
				}
			}
		});

		loadTab(0); // 默认加载第一个页面
	}

	/**
	 * load frame contents
	 * 
	 * @param n
	 */
	private void loadTab(int n) {
		if (n == 0) {// 若是第一个页面
			loadAddNoteFrame();

		} else if (n == 1) { // 若是第二个页面

			LoadMyNotes(); // 加载我的笔记
		}

	}

	/**
	 * 加载我的笔记
	 */
	private void LoadMyNotes() {
		// set list
		list = new JList(listModel);
		list.setCellRenderer(new MyCellRender());
		list.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);
		testModel(); // 测试数据
		list.setVisibleRowCount(4);
		//
		myNotePage.setComponentOrientation(ComponentOrientation.LEFT_TO_RIGHT);
		GridBagConstraints gbCon = new GridBagConstraints();
		searchComBox = new JComboBox();
		searchComBox.setPreferredSize(new Dimension(100, 32));
		searchComBox.addItem("All");
		searchComBox.addItem("Content All");
		searchComBox.addItem("Label");
		searchComBox.addItem("Time");
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 0;
		gbCon.gridy = 0;
		myNotePage.add(searchComBox, gbCon);

		alterPanel = new JPanel(new FlowLayout(FlowLayout.LEFT)); //
		alterPanel.setPreferredSize(new Dimension(455, 45));
		// panel.setBackground(Color.BLUE);
		searchTextField = new JTextField(36);
		searchTextField.setFont(new Font("MS Song", Font.BOLD, 13));
		searchTextField.setPreferredSize(new Dimension(10, 35));
		alterPanel.add(searchTextField);

		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 1;
		gbCon.gridy = 0;
		gbCon.insets = new Insets(5, 7, 5, 7);
		myNotePage.add(alterPanel, gbCon);

		// btnSearch;
		btnSearch.setPreferredSize(new Dimension(70, 35));
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 2;
		gbCon.gridy = 0;
		gbCon.insets = new Insets(5, 0, 5, 10);
		myNotePage.add(btnSearch, gbCon);

		// list
		list.setBackground(Color.getHSBColor(120, 100, 100));
		// list.setPreferredSize(new Dimension(200, 150));
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 0;
		gbCon.gridy = 1;
		gbCon.gridwidth = 3;
		gbCon.insets = new Insets(5, 0, 5, 10);

		// JScrollPane jsp = new JScrollPane();
		// jsp.setPreferredSize(new Dimension(400, 350));
		// jsp.add(list);
		myNotePage.add(new JScrollPane(list), gbCon);

		tabPane.setComponentAt(1, myNotePage); // 加入到tab
		addActionForMyNotePage(); // 添加事件
	}

	/**
	 * 添加事件
	 */
	private void addActionForMyNotePage() {
		searchComBox.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				loadSearchBar(searchComBox.getSelectedIndex());
			}

		});
		// list事件
		list.addListSelectionListener(new ListSelectionListener() {
			@Override
			public void valueChanged(ListSelectionEvent e) {

			}

		});

		//list的双击和右击事件
		list.addMouseListener(new MouseAdapter() {
			public void mouseClicked(MouseEvent e) {
				// 右键事件
				if (e.isMetaDown()) {// 检测鼠标右键单击
					// System.out.println(e.getX() + "   " + e.getY());
					int index = e.getY() / 80;  //获取当前被右键的项
					list.setSelectedIndex(index);   //设置被显示

				} else if (e.getClickCount() == 2) {// 处理双击事件

				}
			}
		});

	}

	/**
	 * 动态初始化 搜索栏
	 * 
	 * @param selectedIndex
	 */
	private void loadSearchBar(int selectedIndex) {
		myNotePage.remove(alterPanel);

		switch (selectedIndex) {
		case 0:
			addTextFiledSearchBar();
			break;
		case 1:
			addTextFiledSearchBar();
			// addTextAreaSearchBar();
			break;
		case 2:
			addTextFiledSearchBar();
			break;
		case 3:
			break;
		}
		myNotePage.updateUI();
	}

	/**
	 * 添加读书笔记的页面
	 */
	private void loadAddNoteFrame() {

		JPanel addPanel = new JPanel(new GridBagLayout());
		addPanel.setComponentOrientation(ComponentOrientation.LEFT_TO_RIGHT); // 设置控件的摆放方向
		GridBagConstraints gbCon = new GridBagConstraints();

		int y = 0;
		JLabel labelBookName = new JLabel("Book name :");
		labelBookName.setFont(new Font("MS Song", Font.BOLD, 15));
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 0;
		gbCon.gridy = y;
		gbCon.weightx = 0;
		gbCon.weighty = 0;
		addPanel.add(labelBookName, gbCon);

		// add text field
		bookNameText = new JTextField(20);
		bookNameText.setFont(new Font("MS Song", Font.BOLD, 12));
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 1;
		gbCon.gridy = y;
		gbCon.weightx = 1;
		gbCon.weighty = 0;
		gbCon.ipady = 8;
		gbCon.insets = new Insets(0, 0, 0, 15);
		addPanel.add(bookNameText, gbCon);

		// 标签
		y++;
		JLabel labelMark = new JLabel("My Label  :");
		labelMark.setFont(new Font("MS Song", Font.BOLD, 15));
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 0;
		gbCon.gridy = y;
		gbCon.weightx = 0;
		gbCon.weighty = 0;
		addPanel.add(labelMark, gbCon);

		// add text field
		labelText = new JTextField(20);
		labelText.setFont(new Font("MS Song", Font.BOLD, 12));
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 1;
		gbCon.gridy = y;
		gbCon.weightx = 1;
		gbCon.weighty = 0;
		gbCon.ipady = 8;
		gbCon.insets = new Insets(0, 0, 0, 15);
		addPanel.add(labelText, gbCon);

		y++;
		// sourceContentsText
		JLabel labelSource = new JLabel("Source content  : ");
		labelSource.setFont(new Font("MS Song", Font.PLAIN, 12));
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 0;
		gbCon.gridy = y;
		gbCon.weightx = 0;
		gbCon.weighty = 0;
		gbCon.ipady = 0;
		gbCon.insets = new Insets(0, 0, 0, 0);
		addPanel.add(labelSource, gbCon);

		y++;
		sourceContentsText = new JTextArea(4, 20); // 设置原文 的输入区域
		sourceContentsText.setFont(new Font("MS Song", Font.PLAIN, 12));
		sourceContentsText.setLineWrap(true); // 设置换行
		sourceContentsText.setWrapStyleWord(true); // 设置不分离单词
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 0;
		gbCon.gridy = y;
		gbCon.weightx = 0;
		gbCon.gridwidth = 2;
		gbCon.insets = new Insets(10, 0, 0, 15);
		gbCon.weighty = 0;
		addPanel.add(new JScrollPane(sourceContentsText), gbCon);

		// / 第三行
		y++;
		// sourceContentsText
		JLabel labelNote = new JLabel(" My notes  : ");
		labelNote.setFont(new Font("MS Song", Font.PLAIN, 12));
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 0;
		gbCon.gridy = y;
		gbCon.weightx = 0;
		gbCon.weighty = 0;
		gbCon.ipady = 0;
		gbCon.gridwidth = 1;
		gbCon.insets = new Insets(0, 0, 0, 0);
		addPanel.add(labelNote, gbCon);

		y++;
		notesText = new JTextArea(5, 20); // 设置原文 的输入区域
		notesText.setFont(new Font("MS Song", Font.PLAIN, 11));
		notesText.setLineWrap(true); // 设置换行
		notesText.setWrapStyleWord(true); // 设置不分离单词
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 0;
		gbCon.gridy = y;
		gbCon.weightx = 0;
		gbCon.gridwidth = 2;
		gbCon.insets = new Insets(10, 0, 0, 15);
		gbCon.weighty = 0;
		addPanel.add(new JScrollPane(notesText), gbCon);

		y++;
		// 底部图片区域
		imagePanel = new JPanel(new FlowLayout(FlowLayout.LEFT, 5, 0)); // 底部放置图片区域
		// 加入图片区域
		imagesPanel.setBackground(Color.GRAY);
		imagesPanel.setPreferredSize(new Dimension(415, 125));

		imagePanel.add(imagesPanel);
		// 加入一个button
		btnAddPic = new JButton("Add picture");
		imagePanel.add(btnAddPic);

		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 0;
		gbCon.gridy = y;
		gbCon.weightx = 0;
		gbCon.gridwidth = 2;
		gbCon.insets = new Insets(10, 0, 0, 15);
		gbCon.weighty = 0;
		addPanel.add(imagePanel, gbCon);

		Box box = new Box(BoxLayout.Y_AXIS);

		y++;
		JPanel bottomPanel = new JPanel(new FlowLayout(FlowLayout.RIGHT));
		// 清除所有数据 按钮
		btnClearContents = new JButton("clear all");
		btnClearContents.setForeground(Color.red);
		bottomPanel.add(btnClearContents);
		// 确定 等提示按钮
		btnSubmitNote = new JButton("submit note");
		btnSubmitNote.setPreferredSize(new Dimension(100, 33));
		bottomPanel.add(btnSubmitNote);

		JPanel tempPane = new JPanel();
		tempPane.setPreferredSize(new Dimension(50, 55));

		box.add(tempPane);
		box.add(bottomPanel);

		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 2;
		gbCon.gridy = y - 1;
		gbCon.weightx = 0;
		gbCon.gridwidth = 1;
		gbCon.insets = new Insets(10, 0, 0, 5);
		gbCon.weighty = 0;
		addPanel.add(box, gbCon);
		// btnClearContents;

		// 日历提示
		JLabel calendarLabel = new JLabel("choose time:");
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 2;
		gbCon.gridy = 0;
		gbCon.weightx = 0;
		gbCon.gridwidth = 0;
		gbCon.insets = new Insets(0, 0, 0, 15);
		gbCon.weighty = 0;
		addPanel.add(calendarLabel, gbCon);

		// 日历
		// calendar = new JPanel();
		calendar.setBackground(Color.GREEN);
		calendar.setPreferredSize(new Dimension(210, 200));
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 2;
		gbCon.gridy = 1;
		gbCon.weightx = 0;
		gbCon.gridheight = 5;
		gbCon.insets = new Insets(0, 0, 0, 5);
		gbCon.weighty = 0;
		addPanel.add(new JScrollPane(calendar), gbCon);

		tabPane.setComponentAt(0, addPanel); // 加入到tab中

		addActions(); // 增加点击事件
		//
	}

	// 自定义删除按钮
	class DeleteImageAction implements ActionListener {
		@Override
		public void actionPerformed(ActionEvent e) {
			int i = 0;
			for (Component comp : imagesPanel.getComponents()) {
				MyImagePanel tempPane = (MyImagePanel) comp;
				if (tempPane.getMoveToTrash() == e.getSource()) {
					imagesPath.remove(i);
					imagesPanel.remove(comp); // 删除按钮
					imagesPanel.updateUI();
					break;
				}
				i++;
			}
			// imagesPath
		}
	}

	/**
	 * 清除所有数据
	 */
	private void clearAll() {
		bookNameText.setText("");
		sourceContentsText.setText("");
		notesText.setText("");
		labelText.setText("");
		imagesPanel.removeAll();
		imagesPath.clear();
		imagesPanel.updateUI();
	}

	/**
	 * 显示主界面
	 */
	public void showMainFrame() {
		mainFrame.setResizable(false); // 不可改变大小
		mainFrame.setLocation(250, 90);
		mainFrame.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
		mainFrame.pack();
		mainFrame.setVisible(true);
	}

	public static void main(String[] args) {
		/*
		 * 使用默认皮肤开启浏览器
		 */
		/*
		 * 下面几句，你们可能看不太懂，这里，其实暂时不懂也没关系，这其实是因为swing不是线程安全的！为了线程安全而采取了以下的措施，去掉的话，可能会报错
		 * 。
		 */

		EventQueue.invokeLater(new Runnable() {
			@Override
			public void run() {
				JFrame.setDefaultLookAndFeelDecorated(true);
				JDialog.setDefaultLookAndFeelDecorated(true);
				try {
					/*
					 * 想要修改皮肤的话，只需要更改，下面这个函数的参数，具体改成什么样，可以打开，Referenced
					 * Libraries -> 点击substance.jar ->
					 * 找到org.jvnet.substance.skin这个包 ->
					 * 将下面的SubstanceDustCoffeeLookAndFeel 替换成
					 * 刚刚打开的包下的任意一个“Substance....LookAndFeel”即可
					 */
					UIManager
							.setLookAndFeel(new org.jvnet.substance.skin.SubstanceDustCoffeeLookAndFeel());
					// 例如按照上面的步骤，可以看见一个叫，"SubstanceOfficeBlue2007LookAndFeel.class"，想要替换成这个皮肤，就可以向下面这样写
					// UIManager
					// .setLookAndFeel(new
					// org.jvnet.substance.skin.SubstanceOfficeBlue2007LookAndFeel());
					// 运行一下，皮肤就可以换了
					// 想要详细了解的同学，可以去百度这个第三方包：substance.jar
				} catch (Exception e) {
					System.err.println(e.getMessage());
				}

				new MainFuntionFrame().showMainFrame(); // 显示界面
				// browser.setVisible(true);
			}
		});

	}

	/**
	 * 当点击用标签的方式搜索的时候 出现一个搜索栏
	 */
	public void addTextFiledSearchBar() {
		GridBagConstraints gbCon = new GridBagConstraints();
		alterPanel = new JPanel(new FlowLayout(FlowLayout.LEFT)); //
		alterPanel.setPreferredSize(new Dimension(455, 45));
		// panel.setBackground(Color.BLUE);
		searchTextField = new JTextField(40);
		searchTextField.setFont(new Font("MS Song", Font.BOLD, 13));
		searchTextField.setPreferredSize(new Dimension(10, 35));
		alterPanel.add(searchTextField);

		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 1;
		gbCon.gridy = 0;
		gbCon.insets = new Insets(5, 5, 5, 10);
		myNotePage.add(alterPanel, gbCon);
	}

	/**
	 * 当点击了搜索全文后
	 */
	private void addTextAreaSearchBar() {

		GridBagConstraints gbCon = new GridBagConstraints();
		alterPanel = new JPanel(new FlowLayout(FlowLayout.LEFT)); //
		alterPanel.setPreferredSize(new Dimension(455, 60));
		// panel.setBackground(Color.BLUE);
		searchTextArea = new JTextArea(3, 40);
		// searchTextArea.setPreferredSize(new Dimension(10, 35));
		alterPanel.add(new JScrollPane(searchTextArea));

		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 1;
		gbCon.gridy = 0;
		gbCon.insets = new Insets(5, 5, 15, 10);
		myNotePage.add(alterPanel, gbCon);
	}

	/**
	 * 测试模型数据
	 */
	private void testModel() {

		for (int i = 0; i <= 10; i++) {
			MyNoteModel t1 = new MyNoteModel();
			t1.setBookName("How to get a girlFriend");
			t1.getImageUrl().add("res/testPic.jpg");
			t1.setLabel("This is my Label my Labe");
			t1.setMyNote("this is my note my note my note");
			t1.setSourceContent("this is my soursce content my source content");
			t1.setTime("2015-12-26");
			listModel.addElement(t1);

		}

	}

	// static {
	// try {
	// UIManager
	// .setLookAndFeel("com.sun.java.swing.plaf.nimbus.NimbusLookAndFeel");
	// } catch (Throwable e) {
	// e.printStackTrace();
	// }
	// }

	/*
	 * static { try { for (LookAndFeelInfo info :
	 * UIManager.getInstalledLookAndFeels()) { if
	 * ("Nimbus".equals(info.getName())) {
	 * UIManager.setLookAndFeel(info.getClassName()); break; } } } catch
	 * (Exception e) { // If Nimbus is not available, you can set the GUI to
	 * another look // and feel. } }
	 */
}
