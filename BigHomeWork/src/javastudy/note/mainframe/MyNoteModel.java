package javastudy.note.mainframe;

import java.util.ArrayList;
import java.util.List;

/**
 * 自定义模型
 * 
 * @author JackCai
 *
 */
public class MyNoteModel {
	// 模型
	private List<String> imageUrl = new ArrayList<String>(); // 图片地址
	private String bookName; // 书名
	private String sourceContent; // 原文
	private String myNote; // 我的笔记
	private String Label; // 标签
	private String time;  //时间
	public String getTime() {
		return time;
	}

	public void setTime(String time) {
		this.time = time;
	}

	public List<String> getImageUrl() {
		return imageUrl;
	}

	public void setImageUrl(List<String> imageUrl) {
		this.imageUrl = imageUrl;
	}

	public String getBookName() {
		return bookName;
	}

	public void setBookName(String bookName) {
		this.bookName = bookName;
	}

	public String getSourceContent() {
		return sourceContent;
	}

	public void setSourceContent(String sourceContent) {
		this.sourceContent = sourceContent;
	}

	public String getMyNote() {
		return myNote;
	}

	public void setMyNote(String myNote) {
		this.myNote = myNote;
	}

	public String getLabel() {
		return Label;
	}

	public void setLabel(String label) {
		Label = label;
	}
}