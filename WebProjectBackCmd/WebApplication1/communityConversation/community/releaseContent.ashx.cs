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
    /// 处理用户发表的说说 并写入数据库 
    /// 需要提交的是 社区ID 发表的内容 以及图片的 文件全名
    /// 插入成功则返回 true 否则是false;
    /// releaseContent 的摘要说明
    /// </summary>
    public class releaseContent : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string communityID = context.Request.QueryString["communityID"].ToString();
            string text = context.Request.QueryString["text"].ToString();

            string contentImg = context.Session["contentImg"].ToString();
            string userEmail = context.Session["userEmail"].ToString();
            bool isOK = false;
            /*
             * 连接数据库
             */
            if (communityID != null && text != null && contentImg != null && userEmail != null)
            {
                /*
*  连接数据库 并返回一条记录
*/
                string connectionStr = ConfigurationManager.ConnectionStrings["oracleCon"].ConnectionString;
                OracleConnection myConnection = new OracleConnection(connectionStr);
                OracleCommand myCommand = new OracleCommand("communityOperation.insertUserContent", myConnection);
                myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                //设置参数  
                OracleParameter[] prams = { 
                                          new OracleParameter("v_userEmail",OracleType.VarChar,20),
                                          new OracleParameter("v_communityID", OracleType.VarChar,20),  
                                          new OracleParameter("v_text",OracleType.VarChar,500),
                                          new OracleParameter("v_picUrl",OracleType.VarChar,60),
                                          new OracleParameter("isOK",OracleType.VarChar,2)
                                      };
                //设置参数类型
                prams[0].Direction = System.Data.ParameterDirection.Input;
                prams[1].Direction = System.Data.ParameterDirection.Input;
                prams[2].Direction = System.Data.ParameterDirection.Input;
                prams[3].Direction = System.Data.ParameterDirection.Input;
                prams[4].Direction = System.Data.ParameterDirection.ReturnValue;
                //设置参数值
                prams[0].Value = userEmail;
                prams[1].Value = communityID;
                prams[2].Value = text;
                prams[3].Value = contentImg;

                //将参数加入到连接实例
                for (int i = 0; i < prams.Length; i++)
                {
                    myCommand.Parameters.Add(prams[i]);
                }
                //连接
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                // 是否插入成功
                if (prams[4].Value.ToString().Equals("1"))
                {
                    isOK = true;
                }

                myConnection.Close();
            }

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