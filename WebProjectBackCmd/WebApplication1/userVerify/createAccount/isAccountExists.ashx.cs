using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.OracleClient;
namespace WebApplication1.userVerify.createAccount
{
    /// <summary>
    /// isAccountExists 的摘要说明
    /// 判断该邮箱是否已经存在了
    /// </summary>
    public class isAccountExists : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            // 获取userEmail
            string userEmail = context.Request.QueryString["userEmail"];
            bool isExists = false;
            /*
             * 连接数据库,传入userEmail,然后判断该邮箱是否已经存在了或者还没有存在
             * 调用数据库中的 用户验证的包 function isAlreadyExistsAccount(userEmail) return boolean;
             * 若已经存在,则返回给前端true  ，否则为false;
             * 
             */
            {
                string connectionStr = ConfigurationManager.ConnectionStrings["oracleCon"].ConnectionString;
                OracleConnection myConnection = new OracleConnection(connectionStr);
                OracleCommand myOracleCommand = new OracleCommand("userVerifyPK.isAlreadyExistsAccount", myConnection);
                myOracleCommand.CommandType = System.Data.CommandType.StoredProcedure;  //设置存储类型
                //设置参数  
                OracleParameter[] prams = { 
                                          new OracleParameter("v_useremail", OracleType.VarChar,50),  
                                          new OracleParameter("isExist",OracleType.VarChar,2)
                                      };
                // 设置参数的类型
                prams[0].Direction = System.Data.ParameterDirection.Input;
                prams[1].Direction = System.Data.ParameterDirection.ReturnValue;
                //将参数赋值
                prams[0].Value = userEmail;
                //将参数加入到连接实体
                for (int i = 0; i < prams.Length; i++)
                {
                    myOracleCommand.Parameters.Add(prams[i]);
                }
                //连接oracle数据库
                myConnection.Open();
                myOracleCommand.ExecuteNonQuery();      // 执行过程
                string result = prams[1].Value.ToString();
                if (result.Equals("1"))
                {
                    isExists = true;
                }
                myConnection.Close(); // 关闭连接

            }

            context.Response.Write(isExists.ToString());  // 返回给前端
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