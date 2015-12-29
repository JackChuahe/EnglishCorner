package javastudy.calendar;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.Vector;

import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JLabel;
import javax.swing.JPanel;

public class MyCalendarChooser extends JPanel {
	private String[] months = { "1", "2", "3", "4", "5", "6", "7", "8", "9",
			"10", "11", "12" }; // 月
	private static int[] day_num_of_month = { -1, 31, 29, 31, 30, 31, 30, 31,
			31, 30, 31, 30, 31 };
	private static final int YEAR_LEN = 5; // 最长 5 年
	private static final int DAY_ROWNUM = 7; // 行
	private static final int DAY_COLNUM = 7; // 列

	private DayContentModel[][] daysModel; // 日历 天的 模型
	private JButton[][] dayBtns; // 日历 天的 按钮

	private Vector<String> years = new Vector<String>(); // 年的模型
	private JComboBox yearBox; // 年的 combox
	private JComboBox monthBox; // 月 combox
	private JButton btnToday;// 今天
	private JButton btnOk;// 确定

	private String defaultYear; // 默认的年
	private String defaultMonth; // 默认的月
	private String defaultDay; // 默认的日期

	// ///////////////////////
	private String year;

	public String getYear() {
		return year;
	}

	public String getMonth() {
		return month;
	}

	public String getDay() {
		return day;
	}

	private String month;
	private String day;

	public MyCalendarChooser() {

		initComBox();
		initData();
		initUI();
		initCenter();

		initBottom();
		addActions();

	}

	/***
	 * 添加事件处理
	 */
	private void addActions() {
		yearBox.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				// System.err.println(e.getSource());
				selectYearOrDay(); // 更新界面
			}
		});
		monthBox.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				selectYearOrDay(); // 更新界面
			}
		});

		// 点击了today之后
		btnToday.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				today();
			}
		});
	}

	/**
	 * bottom panel
	 */
	private void initBottom() {
		JPanel bottomPanel = new JPanel(new FlowLayout(FlowLayout.RIGHT));
		btnToday = new JButton("Today");
		btnOk = new JButton("OK");
		btnToday.setForeground(Color.red);

		bottomPanel.add(btnToday);
		bottomPanel.add(btnOk);

		this.add(bottomPanel, BorderLayout.SOUTH);
	}

	/**
	 * 初始化数据
	 */
	private void initData() {
		daysModel = new DayContentModel[DAY_ROWNUM - 1][];
		for (int i = 0; i < DAY_ROWNUM - 1; i++) {
			daysModel[i] = new DayContentModel[DAY_COLNUM];
		}
		// for()
		for (int i = 0; i < DAY_ROWNUM - 1; i++) {
			for (int j = 0; j < DAY_COLNUM; j++) {
				daysModel[i][j] = new DayContentModel();
				daysModel[i][j].setDayNumber(i + "");
			}
		}

		dayBtns = new JButton[DAY_ROWNUM - 1][];
		for (int i = 0; i < DAY_ROWNUM - 1; i++) {
			dayBtns[i] = new JButton[DAY_COLNUM];
		}
		// for() button
		MyButtonActionListener btnActionListener = new MyButtonActionListener();
		for (int i = 0; i < DAY_ROWNUM - 1; i++) {
			for (int j = 0; j < DAY_COLNUM; j++) {
				dayBtns[i][j] = new JButton(daysModel[i][j].getDayNumber());
				dayBtns[i][j].setEnabled(daysModel[i][j].isEnable());
				dayBtns[i][j].setPreferredSize(new Dimension(20, 15));
				dayBtns[i][j].addActionListener(btnActionListener);
			}
		}
	}

	// 初始化中心位置
	private void initCenter() {
		//
		JPanel centerPanel = new JPanel(new GridLayout(DAY_ROWNUM, DAY_COLNUM,
				1, 1));
		// 第一行的数据
		centerPanel.add(new JLabel("Sun"));
		centerPanel.add(new JLabel("Mon"));
		centerPanel.add(new JLabel("Thu"));
		centerPanel.add(new JLabel("Wen"));
		centerPanel.add(new JLabel("Thur"));
		centerPanel.add(new JLabel("Fir"));
		centerPanel.add(new JLabel("Sta"));

		for (JButton[] btns : dayBtns) {
			for (JButton btn : btns) {
				centerPanel.add(btn);
			}
		}

		this.add(centerPanel, BorderLayout.CENTER);
		int year = Integer.parseInt(yearBox.getItemAt(0).toString());
		int month = Integer.parseInt(monthBox.getSelectedItem().toString());
		updateDays(year, month); // 更新界面
		clearAll(); // 先清楚所有界面效果
		today();// 初始化 设置
	}

	/**
	 * 初始化Combox
	 */
	private void initComBox() {
		// SimpleDateFormat df = new
		// SimpleDateFormat("yyyy-MM-dd HH:mm:ss");//设置日期格式
		// System.out.println(df.format(new Date()));// new Date()为获取当前系统时间
		// 初始化

		yearBox = new JComboBox(); // 年的 combox
		monthBox = new JComboBox(months); // 月 combox
		SimpleDateFormat df = new SimpleDateFormat("MM");// 设置日期格式
		int month = Integer.parseInt(df.format(new Date()));// n获取当前月份
		df = new SimpleDateFormat("yyyy");// 设置日期格式
		int yearAdd = Integer.parseInt(df.format(new Date()));// n获取当前年份
		df = new SimpleDateFormat("dd");// 设置日期格式

		defaultDay = df.format(new Date());// 设置默认日期
		defaultYear = yearAdd + ""; // 设置默认年
		defaultMonth = month + ""; // 设置默认月

		this.day = defaultDay;
		this.month = defaultMonth + "";
		this.year = defaultYear + "";

		monthBox.setMaximumRowCount(5);
		monthBox.setSelectedIndex(month - 1);

		// 初始化年
		for (int i = 0; i < YEAR_LEN; i++) {
			years.addElement(yearAdd + i + "");//
			yearBox.addItem(yearAdd + i + ""); // 初始化 年
		}

		//

		/*
		 * cal.add(Calendar.DAY_OF_MONTH, -3); Date start = cal.getTime();
		 * cal.add(Calendar.DAY_OF_MONTH, 8); Date end = cal.getTime();
		 */

	}



	/**
	 * 初始化界面
	 */
	private void initUI() {
		this.setLayout(new BorderLayout()); // 设置为borderlayout
		// north
		JPanel top = new JPanel(new FlowLayout(FlowLayout.CENTER));
		top.add(yearBox);
		top.add(monthBox);

		this.add(top, BorderLayout.NORTH);

	}

	/**
	 * 更新天数
	 */
	private void updateDays(int year, int month) {
		SimpleDateFormat sd = new SimpleDateFormat("yyyy-MM-dd");
		SimpleDateFormat sdw = new SimpleDateFormat("E");

		Date date = null;
		try {
			date = sd.parse(year + "-" + month + "-" + "1"); // 获取为星期几

		} catch (ParseException e) {
			System.err.println(e.getMessage());
		}

		Calendar time = Calendar.getInstance();
		time.setTime(date);
		int weekOfDay = time.get(Calendar.DAY_OF_WEEK) - 1; // 获取到 某年的第一天是星期几
		weekOfDay = weekOfDay % 7;
		int totalDays = getDay(year, month);
		// for() 更新模型中的数据
		int daySum = 1;
		int leftDay = 1;
		for (int i = 0; i < DAY_ROWNUM - 1; i++) {
			for (int j = 0; j < DAY_COLNUM; j++) {
				if (daySum > totalDays) {

					daysModel[i][j].setEnable(false);
					daysModel[i][j].setDayNumber(leftDay + "");
					leftDay++;
				} else if (j >= weekOfDay || i > 0) {
					// 开始设置模型的值
					daysModel[i][j].setEnable(true);
					daysModel[i][j].setDayNumber(daySum + "");
					daySum++;
				} else {
					daysModel[i][j].setEnable(false);
					daysModel[i][j].setDayNumber("");
				}

			}
		}

		updateCalendar();// 通过模型去更新视图
	}

	/**
	 * 更新视图
	 */
	private void updateCalendar() {
		for (int i = 0; i < DAY_ROWNUM - 1; i++) {
			for (int j = 0; j < DAY_COLNUM; j++) {
				dayBtns[i][j].setText(daysModel[i][j].getDayNumber());
				dayBtns[i][j].setEnabled(daysModel[i][j].isEnable());
			}
		}
	}

	/**
	 * 
	 * @param year
	 * @param month
	 * @return
	 */
	private int getDay(int year, int month) {
		if (isLeapYear(year)) {
			// 是瑞年
			day_num_of_month[2] = 29;
		} else {
			day_num_of_month[2] = 28;
		}
		return day_num_of_month[month];
	}

	/**
	 * 判断是否是闰年
	 * 
	 * @param year
	 * @return
	 */
	public boolean isLeapYear(int year) {
		if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0) {
			return true;
		} else
			return false;
	}

	/**
	 * 
	 * @param year
	 * @param month
	 */
	public void selectYearOrDay() {
		int year = Integer.parseInt(yearBox.getSelectedItem().toString());
		int month = Integer.parseInt(monthBox.getSelectedItem().toString());

		setValue(Integer.parseInt(defaultYear), Integer.parseInt(defaultMonth),
				Integer.parseInt(defaultDay)); // 当翻动年 或者月的时候 将时间设置为默认日期
		clearAll();
		updateDays(year, month); // 更新界面
	}

	public static void main(String[] args) {
		new MyCalendarChooser().updateDays(2015, 12);
	}

	/**
	 * 被选中了某天
	 */
	public void selectDayBtn(JButton btn) {
		// 设置被选中被红色
		for (JButton[] btns : dayBtns) {
			for (JButton _btn : btns) {

				if (btn == _btn) {
					setSelectBtnColor(btn);
					setValue(Integer.parseInt(yearBox.getSelectedItem()
							.toString()), Integer.parseInt(monthBox
							.getSelectedItem().toString()),
							Integer.parseInt(btn.getText())); // 当翻动年 或者月的时候
																// 将时间设置为默认日期
				} else {
					_btn.setBackground(Color.WHITE);
					_btn.setForeground(Color.BLACK);
				}
			}
		}
	}

	/**
	 * 
	 */
	public void setSelectBtnColor(JButton btn) {
		btn.setBackground(Color.red);
		btn.setForeground(Color.BLACK);
	}

	/**
	 * 添加事件监听
	 * 
	 * @author JackCai
	 *
	 */
	class MyButtonActionListener implements ActionListener {
		@Override
		public void actionPerformed(ActionEvent e) {
			selectDayBtn((JButton) e.getSource());
		}
	}

	// 当点击today后
	public void today() {
		SimpleDateFormat df = new SimpleDateFormat("MM");// 设置日期格式
		int month = Integer.parseInt(df.format(new Date()));// n获取当前月份
		df = new SimpleDateFormat("dd");// 设置日期格式
		String tempStr = df.format(new Date());// 获得当前日期

		yearBox.setSelectedIndex(0);
		monthBox.setSelectedIndex(month - 1);

		setValue(Integer.parseInt(defaultYear), Integer.parseInt(defaultMonth),
				Integer.parseInt(defaultDay)); // 当翻动年 或者月的时候 将时间设置为默认日期

		// 设置被选中被红色
		for (JButton[] btns : dayBtns) {
			for (JButton btn : btns) {

				if (btn.getText().equals(tempStr) && btn.isEnabled())
					setSelectBtnColor(btn);
				else {
					btn.setBackground(Color.WHITE);
					btn.setForeground(Color.BLACK);
				}
			}
		}
	}

	/**
	 * 当切换日期后
	 */
	public void clearAll() {
		for (JButton[] btns : dayBtns) {
			for (JButton btn : btns) {

				if (btn.isEnabled()) {
					btn.setBackground(Color.WHITE);
					btn.setForeground(Color.BLACK);
				}
			}
		}
	}

	/**
	 * 设置选中的值
	 * 
	 * @param year
	 * @param month
	 * @param day
	 */
	private void setValue(int year, int month, int day) {
		this.year = year + "";
		this.month = month + "";
		this.day = day + "";
	}
}

/**
 * 日历bottom 的模型
 * 
 * @author JackCai
 *
 */
class DayContentModel {
	private String dayNumber; // button 上面显示的值
	private boolean isEnable; // 是否可以点击

	public String getDayNumber() {
		return dayNumber;
	}

	public void setDayNumber(String dayNumber) {
		this.dayNumber = dayNumber;
	}

	public boolean isEnable() {
		return isEnable;
	}

	public void setEnable(boolean isEnable) {
		this.isEnable = isEnable;
	}

}
