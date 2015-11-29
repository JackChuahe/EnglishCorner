using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using WebApplication1.common.class_;
using System.Data.OracleClient;
using System.Configuration;

namespace WebApplication1.userVerify.createAccount
{
    /// <summary>
    /// 当用户收到邮箱,点击链接后进行链接的后台
    /// 前台传来的是base64编码后的userEmail
    /// 为用户激活,设置session
    /// userActive 的摘要说明
    /// </summary>
    public class userActive : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string userEmailWithBase64 = context.Request.QueryString["userEmail"].ToString().Trim();
            string userEmail = MyBase64.decodeBase64(userEmailWithBase64);
            bool isActiveOk = false;
            /*
             *  连接数据库 为用户激活
             *  调用 userVerify 包的 function userActive(v_userEmail in varchar2) return varchar
             */
            {
                string connectionStr = ConfigurationManager.ConnectionStrings["oracleCon"].ConnectionString;
                OracleConnection myConnection = new OracleConnection(connectionStr);//创建一个连接的对象 
                OracleCommand myOracleCommand = new OracleCommand("userVerifyPK.userActive", myConnection); // 创建一个调用过程的执行对象
                myOracleCommand.CommandType = System.Data.CommandType.StoredProcedure;  // 设置访问类型为存储过程
                // 设置参数个数 并设置参数类型
                OracleParameter[] prams = {
                                          new OracleParameter("v_userEmail",OracleType.VarChar,50),
                                          new OracleParameter("result",OracleType.VarChar,2)
                                      };
                //设置参数的参数模式 将第一个参数设置为 in 类型 第二参数设置为函数返回值return 类型
                prams[0].Direction = System.Data.ParameterDirection.Input;
                prams[1].Direction = System.Data.ParameterDirection.ReturnValue;
                //设置参数的值
                prams[0].Value = userEmail;
                //将参数加入连接实体
                for (int i = 0; i < prams.Length; i++)
                {
                    myOracleCommand.Parameters.Add(prams[i]);
                }
                // 打开数据库连接
                myConnection.Open();
                myOracleCommand.ExecuteNonQuery();   // 执行过程
                string result = prams[1].Value.ToString();
                myConnection.Close();
                if (result.Equals("1"))
                {
                    // 激活成功
                    isActiveOk = true;
                    context.Session["userEmail"] = userEmail;     // 设置seesion
                }

            }

            context.Response.Write(isActiveOk.ToString());  // 写回前端
            
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