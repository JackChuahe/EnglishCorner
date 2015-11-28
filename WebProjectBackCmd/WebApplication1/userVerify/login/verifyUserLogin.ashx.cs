using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Data.OracleClient;
using System.Configuration;
using WebApplication1.common.class_;

namespace WebApplication1.userVerify.login
{
    /// <summary>
    /// verifyUserLogin 的摘要说明
    /// </summary>
    public class verifyUserLogin : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            bool isRight = false;

            // 取得客户端传来的密码
            string userEmail = context.Request.Form["userEmail"];
            string rawPwd = context.Request.Form["password"];


            //string userEmail = context.Request.QueryString["userEmail"];
            //string rawPwd = context.Request.QueryString["password"];
            //先进行base64解码
            string pwdWithToken = MyBase64.decodeBase64(rawPwd);
            // 去token
            string pwd = deleteToken(pwdWithToken,context.Session["token"].ToString());
            //再进行md5 加密
            string md5Pwd = Md5.endcodeMd5(pwd);       // 调用md5类 
            //md5Pwd = "    "
            {
                string connectionString = ConfigurationManager.ConnectionStrings["oracleCon"].ConnectionString;   //获取 webConfig 文件中的 数据库连接字符串
                OracleConnection myConnection = new OracleConnection(connectionString);
                OracleCommand myOracleCommand = new OracleCommand("userVerifyPK.verifyLogin", myConnection); // 调用过程
                myOracleCommand.CommandType = System.Data.CommandType.StoredProcedure;  // 设置访问类型为存储过程
                //设置参数
                OracleParameter[] paramters = {
                        new OracleParameter("v_userEmail",OracleType.VarChar,50),
                        new OracleParameter("v_pwd",OracleType.VarChar,80),
                        new OracleParameter("isRight",OracleType.VarChar,2)   // 返回 '1' 代表成功 '0' 则失败
                };
                //设置参数类型
                paramters[0].Direction = System.Data.ParameterDirection.Input;
                paramters[1].Direction = System.Data.ParameterDirection.Input;
                paramters[2].Direction = System.Data.ParameterDirection.ReturnValue;
                //设置in 类型参数的值
                paramters[0].Value = userEmail;
                paramters[1].Value = md5Pwd;
                //把参数加入到实体
                for (int i = 0; i < paramters.Length; i++)
                {
                    myOracleCommand.Parameters.Add(paramters[i]);
                }
                myConnection.Open();  // 连接数据库
                myOracleCommand.ExecuteNonQuery();   // 执行过程
                string result = paramters[2].Value.ToString();  // 获取返回结果
                if (result.Equals("1"))
                {
                    isRight = true;
                }

                myConnection.Close();
            }

            /*
             * 连接数据库,将邮箱和密码传入进去,若返回true 那么就登录成功并设置相应的session
             * 否则就返回为false 登录失败 
             * 利用用户验证包中的function  verifyLogin(userEmail,pwd) return boolean;
             * 
             */
            if (isRight)
            {
                context.Session["userEmail"] = userEmail;  // 若登录成功则设置cookie
            }

            context.Response.Write(isRight.ToString());  // 将结果返回给客户端
        }

        /// <summary>
        /// 去掉密码的token
        /// </summary>
        /// <param name="pwdWithToken"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private string deleteToken(string pwdWithToken,string token)
        {

            string pwd = pwdWithToken.Replace(token, "");

            return pwd;
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