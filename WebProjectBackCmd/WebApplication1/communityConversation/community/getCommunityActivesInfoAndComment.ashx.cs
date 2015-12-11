using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Configuration;
using System.Data.OracleClient;
using System.Collections;

namespace WebApplication1.communityConversation.community
{
    /// <summary>
    /// 获取指定社区的所有动态和评论信息
    /// getCommunityActivesInfoAndComment 的摘要说明
    /// </summary>
    public class getCommunityActivesInfoAndComment : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
   
            context.Response.ContentType = "text/plain";
            string communityID = context.Request.QueryString["communityID"].ToString();
            string json = "";

            if (communityID != null || communityID != "")
            {
                string connectionStr = ConfigurationManager.ConnectionStrings["oracleCon"].ConnectionString;
                OracleConnection myConnection = new OracleConnection(connectionStr);
                OracleCommand myCommand = new OracleCommand("communityOperation.getCommunityAllContents", myConnection);
                myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                //设置参数  
                OracleParameter[] prams = { 
                                          new OracleParameter("v_communityID", OracleType.VarChar,20),  
                                          new OracleParameter("my_cursor",OracleType.Cursor),
                                      };
                //设置参数类型
                prams[0].Direction = System.Data.ParameterDirection.Input;
                prams[1].Direction = System.Data.ParameterDirection.ReturnValue;
                //设置参数值
                prams[0].Value = communityID;

                //将参数加入到连接实例
                for (int i = 0; i < prams.Length; i++)
                {
                    myCommand.Parameters.Add(prams[i]);
                }
                //连接
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                OracleDataReader myDataReader = (OracleDataReader)prams[1].Value; //获取游标数据集
                ArrayList contents = new ArrayList();          // content数组
                while (myDataReader.Read())
                {
                    MyContentRecord tempContent = new MyContentRecord();
                    tempContent.contentID = myDataReader[0].ToString();
                    tempContent.text = myDataReader[1].ToString();
                    tempContent.textPicUrl = myDataReader[2].ToString();
                    tempContent.submitTime = myDataReader[3].ToString();
                    tempContent.submitUserHeadImg = myDataReader[4].ToString();
                    tempContent.submitUserName = myDataReader[5].ToString();
                    contents.Add(tempContent);
                }
                myDataReader.Close();

                /*
                 * 再通过content的ID 逐个去获取每一个content的评论 即评论者的headImg url
                 */
                ArrayList contentComments = new ArrayList();
                for (int i =0; i < contents.Count; i++)
                {
                    MyContentRecord tempContent = (MyContentRecord)contents[i];  // 提出第一条数据

                    OracleCommand tempCommand = new OracleCommand("communityOperation.GetContentComments", myConnection);
                    tempCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    //设置参数  
                    OracleParameter[] tempPrams = { 
                                          new OracleParameter("v_contentID", OracleType.VarChar,20),  
                                          new OracleParameter("my_cursor",OracleType.Cursor),
                                      };
                    //设置参数类型
                    tempPrams[0].Direction = System.Data.ParameterDirection.Input;
                    tempPrams[1].Direction = System.Data.ParameterDirection.ReturnValue;
                    //设置参数值
                    tempPrams[0].Value = tempContent.contentID;

                    //将参数加入到连接实例
                    for (int j = 0; j < tempPrams.Length; j++)
                    {
                        tempCommand.Parameters.Add(tempPrams[j]);
                    }

                    tempCommand.ExecuteNonQuery();
                    OracleDataReader tempDataReader = (OracleDataReader)tempPrams[1].Value; //获取游标数据集

                    ArrayList tempComments = new ArrayList();
                    // 读取第一content的所有评论 并保存在列表中
                    while(tempDataReader.Read()){
                        MyCommentRecord tempComment = new MyCommentRecord();
                        tempComment.commentUserHeadImg = tempDataReader[0].ToString();
                        tempComment.commentUserName = tempDataReader[1].ToString();
                        tempComment.commentSubmitTime = tempDataReader[2].ToString();
                        tempComment.commentContent = tempDataReader[3].ToString();
                        tempComments.Add(tempComment);
                    }
                    tempDataReader.Close();
                    contentComments.Add(tempComments);   // 将当前这个content的所有评论加入到总的评论中去
                }
                myConnection.Close();   // 关闭数据库连接

                /*
                 * 封装成完全的json代码
                 */
                json += "{ \"contents\":[";

                for (int count = 0; count < contents.Count; count++)
                {
                    // 将每一个content的基本数据进行封装
                    json += "{";
                    MyContentRecord tempContent = (MyContentRecord)contents[count];
                    json += "\"contentID\":\"" + tempContent.contentID + "\",";   // 加上contentID
                    json += "\"submitUserName\":\"" + tempContent.submitUserName + "\",";
                    json += "\"submitUserHeadImg\":\"" + tempContent.submitUserHeadImg + "\",";
                    json += "\"submitTime\":\"" + tempContent.submitTime + "\",";
                    json += "\"text\":\"" + tempContent.text + "\",";
                    json += "\"textPicUrl\":\"" + tempContent.textPicUrl + "\",";

                    // 封装每一个内容的所有评论
                    json += "\"comments\":[";
                    ArrayList tempList = (ArrayList)contentComments[count];
                    for (int innerCount = 0; innerCount < tempList.Count; innerCount++)
                    {
                        MyCommentRecord tempComment = (MyCommentRecord)tempList[innerCount];
                        json += "{";

                        json += "\"commentUserHeadImg\":\"" + tempComment.commentUserHeadImg + "\",";
                        json += "\"commentUserName\":\"" + tempComment.commentUserName + "\",";
                        json += "\"commentSubmitTime\":\"" + tempComment.commentSubmitTime + "\",";
                        json += "\"commentContent\":\"" + tempComment.commentContent + "\"";

                        json += "}";
                        if (innerCount != (tempList.Count - 1))
                        {
                            json += ",";
                        }
                    }

                    json += "]}";
                    // 分隔每一个记录
                    if (count != (contents.Count - 1))
                    {
                        json += ",";
                    }
                }

                json += "]}";
            }


            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    /// <summary>
    /// 接受contents 的自定义类
    /// </summary>
    class MyContentRecord
    {
        public string contentID;
        public string text;
        public string textPicUrl;
        public string submitTime;
        public string submitUserHeadImg;
        public string submitUserName;
    }

    /// <summary>
    /// 接收comments 的自定义类
    /// </summary>
    class MyCommentRecord
    {
        public string commentUserHeadImg;
        public string commentUserName;
        public string commentSubmitTime;
        public string commentContent;
        
    }
}