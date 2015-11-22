using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.userVerify.createAccount
{
    /// <summary>
    /// getCaptchValue 的摘要说明
    /// 前端获取验证码的值,这样验证码就可以直接在本地进行验证
    /// </summary>
    public class getCaptchValue : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string captch = context.Session["captch"].ToString();   // 获取验证码值

            context.Response.Write(captch);  //返回给前端
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