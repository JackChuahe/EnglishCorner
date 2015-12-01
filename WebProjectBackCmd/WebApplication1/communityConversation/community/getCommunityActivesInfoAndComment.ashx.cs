using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.communityConversation.community
{
    /// <summary>
    /// 获取指定社区的所有动态和评论信息
    /// getCommunityActivesInfoAndComment 的摘要说明
    /// </summary>
    public class getCommunityActivesInfoAndComment : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
   
            context.Response.ContentType = "text/plain";
            string communityID = context.Request.QueryString["communityID"].ToString();
            string json = "";

            if (communityID != null || communityID != "")
            {
                /*
                 * 访问数据库 先获得所有的content 动态
                 */

                /*
                 * 再通过content的ID 逐个去获取每一个content的评论 即评论者的headImg url
                 */

                /*
                 * 封装成完全的json代码
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