using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Configuration;

namespace WebApplication1.userVerify.login
{
    /// <summary>
    /// getHeadImg 的摘要说明
    /// 用于在登录前返回用户的头像并显示
    /// </summary>
    public class getHeadImg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string url = "";
            string userEmail = context.Request.QueryString["userEmail"].Trim();  // 获取查询字符串
  
            /*
             *  连接数据库,根据穿过来的get请求的emil地址,进行返回用户头像的url
             *  调用数据库中的  用户验证包中的function getUserHeadImg(email in ,url out)return  boolean;
             *  若数据库中有该人的头像则为 true， 并返回url 赋值给程序中的变量string url,否则为false 
             */
            {
                string connectionString = ConfigurationManager.ConnectionStrings["oracleCon"].ConnectionString;
                OracleConnection myConnection = new OracleConnection(connectionString);
                // 调用userVerifyPK包下面的 function getUserHeadImg(email in varchar) return varchar 方法
                OracleCommand myORACCommand = new OracleCommand("userVerifyPK.getUserHeadImg", myConnection);
                myORACCommand.CommandType = System.Data.CommandType.StoredProcedure;   // 设置访问类型为存储类型
                //设置参数  
                OracleParameter[] prams = { 
                                          new OracleParameter("email", OracleType.VarChar,50),  // in
                                          new OracleParameter("url",OracleType.VarChar,50)   // 接收 return 返回值
                                      };
                prams[0].Direction = System.Data.ParameterDirection.Input;         // 设置参数类型 in  out in out 或者 return类型
                prams[1].Direction = System.Data.ParameterDirection.ReturnValue;     // 设置为返回值类型。
                // 将参数赋值
                prams[0].Value = userEmail;
                //加入到连接实例中
                for (int i = 0; i < prams.Length; i++)
                {
                    myORACCommand.Parameters.Add(prams[i]);
                }
                //连接oracle数据库
                myConnection.Open();
                myORACCommand.ExecuteNonQuery();      // 执行过程
                url = prams[1].Value.ToString();         // 获取返回值
                myConnection.Close();  // 关闭数据库连接
            }

            context.Response.Write(url);   // 返回头像url
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