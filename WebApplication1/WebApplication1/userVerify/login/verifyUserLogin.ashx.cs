using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
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
            // 先进行base64解码
            string pwd = paraseBase64(rawPwd);
            //再进行md5 加密
            string md5Pwd = md5Value(pwd);

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

        protected string paraseBase64(string rawPwd)
        {
            string pwd = "";

            return pwd;
        }


        protected string md5Value(string pwd)
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
    }
}