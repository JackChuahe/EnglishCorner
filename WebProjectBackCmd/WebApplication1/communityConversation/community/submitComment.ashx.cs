using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Configuration;
using System.Data.OracleClient;

namespace WebApplication1.communityConversation.community
{
    /// <summary>
    /// 用将要提交对某个内容的comment 接收comment，并且写入数据库
    /// submitComment 的摘要说明
    /// </summary>
    public class submitComment : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string userEmail = context.Session["userEmail"].ToString();
            string contentID = context.Request.QueryString["v_contentID"].ToString();
            string text = context.Request.QueryString["text"].ToString();
            bool isOK = false;
            /*
             * 提交数据
             */
            if (userEmail != null && userEmail != null && contentID != null && text != null)
            {
                //开始访问数据库并且将这条数据插入
                string connectionStr = ConfigurationManager.ConnectionStrings["oracleCon"].ConnectionString;
                OracleConnection myConnection = new OracleConnection(connectionStr);
                OracleCommand myOracleCommand = new OracleCommand("communityOperation.insertAComment", myConnection); // 调用包 进行插入数据
                myOracleCommand.CommandType = System.Data.CommandType.StoredProcedure; // 设置为访问存储类型形式
                //设置参数  
                OracleParameter[] prams = { 
                                          new OracleParameter("v_contentID", OracleType.VarChar,20),  
                                          new OracleParameter("v_userEmail",OracleType.VarChar,50),
                                          new OracleParameter("v_text",OracleType.VarChar,500),
                                          new OracleParameter("isOK",OracleType.VarChar,2)
                                      };
                //设置参数类型
                prams[0].Direction = System.Data.ParameterDirection.Input;
                prams[1].Direction = System.Data.ParameterDirection.Input;
                prams[2].Direction = System.Data.ParameterDirection.Input;
                prams[3].Direction = System.Data.ParameterDirection.ReturnValue;
                //设置参数值
                prams[0].Value = contentID;
                prams[1].Value = userEmail;
                prams[2].Value = text;
                //将参数加入到连接实例
                for (int i = 0; i < prams.Length; i++)
                {
                    myOracleCommand.Parameters.Add(prams[i]);
                }
                //连接
                myConnection.Open();
                myOracleCommand.ExecuteNonQuery();
                string result = prams[3].Value.ToString();
                if (result.Equals("1"))
                {
                    isOK = true;
                }
                myConnection.Close();
            }//if end

            context.Response.Write(isOK.ToString());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}