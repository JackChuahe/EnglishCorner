using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.communityConversation.community
{
    /// <summary>
    /// 获取指定community的所有用户的头像的url地址
    /// getCommunityUsersHeadImg 的摘要说明
    /// </summary>
    public class getCommunityUsersHeadImg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string json = "";
            string communityID = context.Request.QueryString["communityID"].ToString();
            if (communityID != null || communityID != "") 
            { 
                /*
                 * 访问数据库并返回该社区所有用户的头像
                 */


                /*
                 *  将数据封装成json 
                 */
            }

            // 返回给前端
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