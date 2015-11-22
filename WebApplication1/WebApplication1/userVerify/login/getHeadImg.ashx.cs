using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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