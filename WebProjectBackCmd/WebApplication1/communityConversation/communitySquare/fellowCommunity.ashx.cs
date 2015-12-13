using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Configuration;
using System.Data.OracleClient;

namespace WebApplication1.communityConversation.communitySquare
{
    /// <summary>
    /// 为用户加入新的community
    /// 传入参数为 communityID 
    /// fellowCommunity 的摘要说明
    /// </summary>
    public class fellowCommunity : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string userEmail = (string)context.Session["userEmail"];
            string communityID = context.Request.Form["communityID"];
            string isOK = "0";

            if (userEmail != null && communityID != null)
            {
                /*
                 *  连接数据库
                 */
                string connectionStr = ConfigurationManager.ConnectionStrings["oracleCon"].ConnectionString;
                OracleConnection myConnection = new OracleConnection(connectionStr);
                OracleCommand myOracleCommand = new OracleCommand("communityOperation.fellowCommunity", myConnection); // 调用包 进行插入数据
                myOracleCommand.CommandType = System.Data.CommandType.StoredProcedure; // 设置为访问存储类型形式
                //设置参数  
                OracleParameter[] prams = { 
                                          new OracleParameter("v_userEmail", OracleType.VarChar,50),  
                                          new OracleParameter("v_communityID",OracleType.VarChar,20),
                                           new OracleParameter("result",OracleType.VarChar,1),

                                      };
                //设置参数类型
                prams[0].Direction = System.Data.ParameterDirection.Input;
                prams[1].Direction = System.Data.ParameterDirection.Input;
                prams[2].Direction = System.Data.ParameterDirection.ReturnValue;
                //设置参数值
                prams[0].Value = userEmail;
                prams[1].Value = communityID;

                //将参数加入到连接实例
                for (int i = 0; i < prams.Length; i++)
                {
                    myOracleCommand.Parameters.Add(prams[i]);
                }
                //连接
                myConnection.Open();
                myOracleCommand.ExecuteNonQuery();
                isOK = prams[2].Value.ToString();
                myConnection.Close();
            }

            context.Response.Write(isOK);
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