package javastudy.calendar;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

public class TestTime {

	public static void main(String[] args) {
		// Calendar cal = Calendar.getInstance();
		// Date init = cal.getTime();
		int month = 12;
		int year = 2015;
		SimpleDateFormat df = new SimpleDateFormat("MM");//设置日期格式
		System.out.println(df.format(new Date()));// new Date()为获取当前系统时间
		/*
		 * Calendar time = Calendar.getInstance(); // 下面代码设置开始日期，注：不要设置为周末 //
		 * 假设设置年(2011)月(8)日(18)，注：如果是8月，设置时候要减1，如下： if (month == 8) { month--; }
		 * time.set(year, month, 27); int day = time.get(Calendar.DAY_OF_WEEK);
		 * // 一周第一天是在java里是星期天，所以要减1 System.out.println("星期" + (day));
		 * //System.out.println(cal.getWeekYear());
		 */
		SimpleDateFormat sd = new SimpleDateFormat("yyyy-MM-dd");
		SimpleDateFormat sdw = new SimpleDateFormat("E");

		Date d = null;
		try {
			d = sd.parse("2016-2-1");

		} catch (ParseException e) {
			System.err.println(e.getMessage());
		}

		Calendar time = Calendar.getInstance();
		time.setTime(d);
		System.out.println(time.get(Calendar.DAY_OF_WEEK)-1);
		
		System.out.println(getWeek(d));
		// System.out.println(time.getTime());
	}

	public static String getWeek(Date date) {

		SimpleDateFormat sdf = new SimpleDateFormat("EEEE");
		String week = sdf.format(date);
		return week;
	}
}
