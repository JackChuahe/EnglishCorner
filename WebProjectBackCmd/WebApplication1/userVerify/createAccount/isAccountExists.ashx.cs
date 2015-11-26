using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

            context.Response.Write(isExists.ToString());
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