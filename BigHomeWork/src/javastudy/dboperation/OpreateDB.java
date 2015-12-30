package javastudy.dboperation;

import java.sql.Connection;
import java.util.List;

import javastudy.note.mainframe.MyNoteModel;

/**
 * 数据库操作的包
 * 
 * @author JackCai
 *
 */
public class OpreateDB {
	private static Connection con = null;

	static {
		connectToDB(); // 连接数据库
	}

	/**
	 * 更新至数据库
	 * 
	 * @param myNoteModel
	 * @return
	 */
	public static boolean updateToDB(MyNoteModel myNoteModel) {

		return false;
	}

	/**
	 * 连接数据库
	 */
	private static void connectToDB() {
		// TODO Auto-generated method stub

	}

	/**
	 * 写入到数据库
	 * 
	 * @return
	 */
	public static boolean insertToDB(MyNoteModel myNoteModel) {

		return false;
	}

	/**
	 * close 数据库
	 */
	public static void closeDB() {

	}

	/**
	 * 从数据库检索 以时间的方式进行检索
	 * 
	 * @param startTime
	 * @param endTime
	 */
	public static List<MyNoteModel> searchFromDB(String startTime,
			String endTime) {

		return null;
	}

	/**
	 * 以标签的方式进行检索
	 * 
	 * @param label
	 * @return
	 */
	public static List<MyNoteModel> searchFromDB(String label) {

		return null;
	}

	/**
	 * 检索全部内容
	 * 
	 * @return
	 */
	public static List<MyNoteModel> searchFromDB() {

		return null;
	}

	/**
	 * 以关键字的形式检索 全文检索
	 * 
	 * @param keyWord
	 * @param isKey
	 * @return
	 */
	public static List<MyNoteModel> searchFromDB(String keyWord, boolean isKey) {

		return null;
	}

	/**
	 * 删除
	 * 
	 * @param myNoteModel
	 * @return
	 */
	public static boolean deleteFromDB(MyNoteModel myNoteModel) {

		return false;
	}

}
