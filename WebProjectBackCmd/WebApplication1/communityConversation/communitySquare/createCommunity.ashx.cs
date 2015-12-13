using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Configuration;
using System.Data.OracleClient;

namespace WebApplication1.communityConversation.communitySquare
{
    /// <summary>
    /// 该后台程序的功能是作为 前台创建社区功能准备的
    /// 作为创建社区的后台支持
    /// createCommunity 的摘要说明
    /// </summary>
    public class createCommunity : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string communityName = (string)context.Request.QueryString["communityName"];
            string desc = (string)context.Request.QueryString["desc"];
            string communityHeadImg = (string)context.Session["newCommunityHeadImg"];

            if (communityName != null && desc != null && communityHeadImg != null)
            {

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