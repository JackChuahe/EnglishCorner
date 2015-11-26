using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Data.OracleClient;
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
            string userEmail = context.Request.QueryString["userEmail"];
            string rawPwd = context.Request.QueryString["password"];
            //先进行base64解码
            string pwdWithToken = paraseBase64(rawPwd);
            // 去token
            string pwd = deleteToken(pwdWithToken,context.Session["token"].ToString());
            //再进行md5 加密
            //string md5Pwd = md5Value(pwd);


            {
                string connectionString = "Data Source=222.196.200.38/orcl.196.200.45;User ID=encor;PassWord=encor";
                string queryString = "select count(*) from allusers where useremail='" + userEmail + "' and pwd='"+pwd+"'";
                OracleConnection myConnection = new OracleConnection(connectionString);
                OracleCommand myORACCommand = myConnection.CreateCommand();
                myORACCommand.CommandText = queryString;
                myConnection.Open();
                OracleDataReader myDataReader = myORACCommand.ExecuteReader();
                if (myDataReader.Read())
                {
                    isRight = true;
                }
                myDataReader.Close();
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

        private string deleteToken(string pwdWithToken,string token)
        {

            string pwd = pwdWithToken.Replace(token, "");

            return pwd;
        }

        public string paraseBase64(string rawPwd)
        {
            string pwdWithToken = base64(rawPwd, false);

            return pwdWithToken;
        }


        public string md5Value(string pwd)
        {
            string md5Pwd = "";

            return md5Pwd;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        // base64
        static public string base64(string s, bool c)
        {
            if (c)
            {
                //编码
                return System.Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(s));
            }
            else
            {
                //解码
                return System.Text.Encoding.Default.GetString(System.Convert.FromBase64String(s));
            }
        }
    }
}