using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Configuration;
namespace WebApplication1.communityConversation.community
{
    /// <summary>
    /// 获取指定社区的介绍信息 ，包括社区名字,社区描述,社区头像url
    /// getCommunityInfo 的摘要说明
    /// </summary>
    public class getCommunityInfo : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string json = "";
            // 获取需要信息的的社区ID
            string communityID = context.Request.QueryString["communityID"].ToString();
            if (communityID != null || communityID != "")
            {
                /*
                *  连接数据库 并返回一条记录
                */


                /*
                 *  将数据封装成json发送给前台
                */
            }

            context.Response.Write(json);

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