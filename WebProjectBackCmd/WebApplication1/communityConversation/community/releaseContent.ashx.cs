using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Configuration;
using System.Data.OracleClient;

namespace WebApplication1.communityConversation.community
{
    /// <summary>
    /// 用户发表说说
    /// 需要提交的是 社区ID 发表的内容 以及图片的 文件全名
    /// releaseContent 的摘要说明
    /// </summary>
    public class releaseContent : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string communityID = context.Request.QueryString["communityID"].ToString();
            string text = context.Request.QueryString["text"].ToString();
            string contentImg = context.Session["contentImg"].ToString();
            string userEmail = context.Session["userEmail"].ToString();
            bool isOK = false;
            /*
             * 连接数据库
             */
            if (communityID != null && text != null && contentImg != null && userEmail != null)
            {

            }

            context.Response.Write(isOK.ToString());
            
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