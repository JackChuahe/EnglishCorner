using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
namespace WebApplication1.userVerify.login
{
    /// <summary>
    /// setServerToken 的摘要说明
    /// 用于登录前设置服务器的token 来实现对密码浏览器本地和服务器端进行加解密
    /// </summary>
    public class setServerToken : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //先生成一个token字符串
            string token = Environment.TickCount.ToString();   // 确定位数
            context.Session["token"] = token;
            context.Session["tokenLength"] = token.Length;     // 将token的lenth记录下来
  
            context.Response.Write(token);  // 返回token
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