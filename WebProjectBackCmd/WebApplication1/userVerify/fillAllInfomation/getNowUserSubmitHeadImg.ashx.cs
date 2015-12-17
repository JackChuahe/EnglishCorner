using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebApplication1.communityConversation.community
{
    /// <summary>
    /// getNowSubmitImg 的摘要说明
    /// 获取当前用户正在上传的content的图片的 url
    /// </summary>
    public class getNowUserSubmitHeadImg : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string picUrl = (string)context.Session["contentImg"];
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