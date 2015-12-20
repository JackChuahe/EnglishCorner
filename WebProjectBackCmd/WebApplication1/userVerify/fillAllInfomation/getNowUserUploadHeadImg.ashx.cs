using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebApplication1.userVerify.fillAllInfomation
{
    /// <summary>
    /// getNowUserUploadHeadImg 的摘要说明
    /// 获取用户上传上来的的个人信息的个人头像
    /// </summary>
    public class getNowUserUploadHeadImg : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string picUrl = (string)context.Session["userHeadImg"];
            if (picUrl != null && picUrl != "")
            {
                //若有图片
                context.Response.Write(picUrl);
            }
            else
            {
                context.Response.Write("");
            }

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