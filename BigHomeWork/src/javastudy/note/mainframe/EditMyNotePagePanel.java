package javastudy.note.mainframe;

import java.awt.Color;
import java.awt.ComponentOrientation;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Font;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Insets;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;
import java.util.List;

import javastudy.dboperation.OpreateDB;

import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTabbedPane;
import javax.swing.JTextArea;
import javax.swing.JTextField;

/**
 * 可编辑的我的页面
 * 
 * @author JackCai
 *
 */
public class EditMyNotePagePanel extends JPanel {
	private MyNoteModel myNote; // 我的读书笔记 一个模型

	private JTextArea sourceContentsText; // 源内容
	private JTextArea notesText; // 读书笔记
	private JTextField labelText; // 标签
	private JButton btnUpdateNote; // 更新
	private JButton btnCancelEdit; // 不编辑状态
	private MyScrollPanel imageScrollPane; // 图片轮换图
	private JButton btnEdit; // 点击编辑按钮
	private JButton btnDelete; // 删除该笔记
	private boolean isEdit = false;
	int count_image = 0; // 记录图片url的数量
	private List<String> imageUrl = new ArrayList<String>(); // 图片地址

	/**
	 * 构造函数
	 * 
	 * @param myNote
	 */
	public EditMyNotePagePanel(MyNoteModel myNote) {
		// count_image = myNote.getImageUrl().size(); // 先获取图片原始数量

		loadMyNote(); // 加载页面
		showContents();// 通过模型加载内容
		addActions(); // 增加点击事件
		setEdit(); // 设置是否可以编辑
	}

	/**
	 * 通过模型加载内容
	 */
	private void showContents() {
		if (myNote == null)
			return; // 若为空 就返回
		sourceContentsText.setText(myNote.getSourceContent());
		notesText.setText(myNote.getMyNote());
		labelText.setText(myNote.getLabel());
	}

	public MyNoteModel getMyNote() {
		return myNote;
	}

	/**
	 * 设置是否可以编辑
	 */
	private void setEdit() {
		sourceContentsText.setEditable(isEdit);
		notesText.setEditable(isEdit);
		labelText.setEditable(isEdit);
		imageScrollPane.setEnabled(isEdit);
		btnUpdateNote.setEnabled(isEdit);
	}

	/**
	 * 实现点击事件
	 */
	private void addActions() {
		// 设置可以编辑i
		btnEdit.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				isEdit = !isEdit;
				if (isEdit) { // 如果是编辑状态
					btnEdit.setText("DisEdit");
					setEdit();
				} else { // 如果当前不是编辑状态
					if (isDisEdit()) { // 若有内容的改变 用户是否同意结束编辑呢
						btnEdit.setText("Edit");
						showContents(); // 恢复内容
						setEdit();
					}
				}

			}
		});

		// 设置取消编辑
		btnCancelEdit.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				prepareForCancelEdit(); // 准备结束编辑
			}

		});

		// 设置更新按钮的响应事件
		btnUpdateNote.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				prepareForUpdate(); // 准备更新
			}

		});

		// 删除该读书笔记
		btnDelete.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				prepareForDelete(); // 准备删除
			}
		});
	}

	/***
	 * 用户是否自动允许结束编辑
	 * 
	 * @return
	 */
	private boolean isDisEdit() {
		boolean isChange = false;
		if (myNote == null) {

			return true;
		}
		if (count_image != imageUrl.size()) {
			isChange = true;
		} else if (!labelText.getText().equals(myNote.getLabel())) {
			isChange = true;
		} else if (!sourceContentsText.getText().equals(
				myNote.getSourceContent())) {
			isChange = true;
		} else if (!notesText.getText().equals(myNote.getMyNote())) {
			isChange = true;
		}

		if (isChange) {
			// 若数据有改变
			// 询问用户是否同意
			JOptionPane.showMessageDialog(this, "你的内容有所改变你确定要结束编辑?", "警惕哟",
					JOptionPane.WARNING_MESSAGE);
			if (true) {
				// 如果用户同意退出编辑
				return true;
			} else {
				return false;
			}
		}
		return true;
	}

	/**
	 * 准备结束编辑
	 */
	private void prepareForCancelEdit() {

		boolean isChange = false;
		if (myNote == null) {

			return;
		}
		if (count_image != imageUrl.size()) {
			isChange = true;
		} else if (!labelText.getText().equals(myNote.getLabel())) {
			isChange = true;
		} else if (!sourceContentsText.getText().equals(
				myNote.getSourceContent())) {
			isChange = true;
		} else if (!notesText.getText().equals(myNote.getMyNote())) {
			isChange = true;
		}

		if (isChange) {
			JOptionPane.showMessageDialog(this, "你的内容有所改变你确定要保存吗", "警惕哟",
					JOptionPane.WARNING_MESSAGE);
			if (true) {
				// 若用户选择直接退出那么就退出吧
				JTabbedPane tabPane = (JTabbedPane) this.getParent();
				tabPane.remove(this); // 删除自己
			}

		} else {
			// 没有内容改变的话
			JTabbedPane tabPane = (JTabbedPane) this.getParent();
			tabPane.remove(this); // 删除自己
		}

	}

	/***
	 * 准备更新
	 */
	private void prepareForUpdate() {
		boolean isChange = false;
		if (count_image != imageUrl.size()) {
			isChange = true;
		} else if (!labelText.getText().equals(myNote.getLabel())) {
			myNote.setLabel(labelText.getText());
			isChange = true;
		} else if (!sourceContentsText.getText().equals(
				myNote.getSourceContent())) {
			myNote.setSourceContent(sourceContentsText.getText());
			isChange = true;
		} else if (!notesText.getText().equals(myNote.getMyNote())) {
			myNote.setMyNote(notesText.getText());
			isChange = true;
		}

		// 如果有数据改变的话
		if (isChange) {
			if (OpreateDB.updateToDB(myNote)) { // 更新至数据库 成功
				JOptionPane.showMessageDialog(this, "更新成功!", "Nice!",
						JOptionPane.YES_OPTION);
			} else {
				// 失败
				JOptionPane.showMessageDialog(this, "更新至数据库失败!", "错误",
						JOptionPane.ERROR_MESSAGE);
			}
		}
	}

	/**
	 * 准备删除
	 */
	private void prepareForDelete() {
		// 先询问用户是否真的要删除吗？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？
		JOptionPane.showMessageDialog(this, "您真的要删除吗？？？？？？？？？？？", "警惕哟",
				JOptionPane.WARNING_MESSAGE);
		// 判断用户选择
		if (false) {
			// 放弃删除
		} else {
			// 同意删除
			OpreateDB.deleteFromDB(myNote);
		}
	}

	/**
	 * 设置内容
	 */
	private void setContent() {

	}

	/**
	 * 设置整个页面是否可编辑
	 */
	private void setEditble() {

	}

	/**
	 * 点击关闭后
	 */
	private void matchContentsChange() {

	}

	/**
	 * 加载内容
	 */
	private void loadMyNote() {

		JPanel addPanel = new JPanel(new GridBagLayout());
		addPanel.setComponentOrientation(ComponentOrientation.LEFT_TO_RIGHT); // 设置控件的摆放方向
		GridBagConstraints gbCon = new GridBagConstraints();

		int y = 0;
		JLabel labelMark = new JLabel("My Label  :");
		labelMark.setFont(new Font("MS Song", Font.BOLD, 15));
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 0;
		gbCon.gridy = y;
		gbCon.weightx = 0;
		gbCon.weighty = 0;
		addPanel.add(labelMark, gbCon);
		/*
		 * JLabel labelBookName = new JLabel("Book name :");
		 * labelBookName.setFont(new Font("MS Song", Font.BOLD, 15)); gbCon.fill
		 * = GridBagConstraints.HORIZONTAL; gbCon.gridx = 0; gbCon.gridy = y;
		 * gbCon.weightx = 0; gbCon.weighty = 0; addPanel.add(labelBookName,
		 * gbCon);
		 * 
		 * // add text field bookNameText = new JTextField(20);
		 * bookNameText.setFont(new Font("MS Song", Font.BOLD, 12)); gbCon.fill
		 * = GridBagConstraints.HORIZONTAL; gbCon.gridx = 1; gbCon.gridy = y;
		 * gbCon.weightx = 1; gbCon.weighty = 0; gbCon.ipady = 8; gbCon.insets =
		 * new Insets(0, 0, 0, 15); addPanel.add(bookNameText, gbCon);
		 */

		// 标签
		y++;

		// add text field
		labelText = new JTextField(20);
		labelText.setFont(new Font("MS Song", Font.BOLD, 12));
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 0;
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
		// JPanel
		// 底部区域
		JPanel bottomPane = new JPanel();
		btnEdit = new JButton("Edit"); // 设置编辑该读书笔记
		btnEdit.setFont(new Font("MS Song", Font.BOLD, 14));
		bottomPane.add(btnEdit);

		btnDelete = new JButton("Delete");
		btnDelete.setFont(new Font("MS Song", Font.BOLD, 14));
		btnDelete.setForeground(Color.RED);
		bottomPane.add(btnDelete);
		bottomPane.setPreferredSize(new Dimension(415, 125));
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 0;
		gbCon.gridy = y;
		gbCon.weightx = 0;
		gbCon.gridwidth = 2;
		gbCon.insets = new Insets(10, 0, 0, 15);
		gbCon.weighty = 0;
		addPanel.add(bottomPane, gbCon);

		Box box = new Box(BoxLayout.Y_AXIS);

		y++;
		JPanel bottomPanel = new JPanel(new FlowLayout(FlowLayout.RIGHT));
		// 取消操作按钮
		btnCancelEdit = new JButton("cancel");
		btnCancelEdit.setFont(new Font("MS Song", Font.BOLD, 13));
		btnCancelEdit.setForeground(Color.red);
		bottomPanel.add(btnCancelEdit);
		// 确定 等提示按钮
		btnUpdateNote = new JButton("update");
		btnUpdateNote.setFont(new Font("MS Song", Font.BOLD, 13));
		btnUpdateNote.setPreferredSize(new Dimension(100, 33));
		btnUpdateNote.setEnabled(isEdit);
		bottomPanel.add(btnUpdateNote);

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

		imageScrollPane = new MyScrollPanel(null);
		gbCon.fill = GridBagConstraints.HORIZONTAL;
		gbCon.gridx = 2;
		gbCon.gridy = 1;
		gbCon.weightx = 0;
		gbCon.gridheight = 5;
		gbCon.insets = new Insets(0, 0, 0, 3);
		gbCon.weighty = 0;
		addPanel.add(new JScrollPane(imageScrollPane), gbCon);

		this.add(addPanel);
		//

	}

}
