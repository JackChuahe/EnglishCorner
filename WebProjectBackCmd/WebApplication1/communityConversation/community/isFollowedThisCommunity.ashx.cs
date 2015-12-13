using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Data.OracleClient;
using System.Configuration;

namespace WebApplication1.communityConversation.community
{
    /// <summary>
    /// 判断该用户是否已经跟随这个社区 ,若跟随了 返回 true
    /// isFollowedThisCommunity 的摘要说明
    /// </summary>
    public class isFollowedThisCommunity : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
           context.Response.ContentType = "text/plain";
            Object obj = context.Session["userEmail"];

            Object v_communityID = context.Request.QueryString["communityID"];
            bool isFollowed = false;
            if (obj != null && v_communityID != null)
            {
                 string userEmail = obj.ToString();
                 string communityID = v_communityID.ToString();
                //string userEmail = "601825672@qq.com";
                // 若登录过
                //访问数据库
                string connectionStr = ConfigurationManager.ConnectionStrings["oracleCon"].ConnectionString;
                OracleConnection myConnection = new OracleConnection(connectionStr);
                OracleCommand myOracleCommand = new OracleCommand("communityOperation.isFollowed", myConnection);
                myOracleCommand.CommandType = System.Data.CommandType.StoredProcedure; // 设置为调用存储过程
                OracleParameter[] prams = {
                                               new OracleParameter("v_userEmail",OracleType.VarChar,50),
                                               new OracleParameter("v_communityID",OracleType.VarChar,20),
                                               new OracleParameter("result",OracleType.VarChar,1)
                                           };
                // 设置参数类型
                prams[0].Direction = System.Data.ParameterDirection.Input;
                prams[1].Direction = System.Data.ParameterDirection.Input;
                prams[2].Direction = System.Data.ParameterDirection.ReturnValue;   // 接收返回值
                // 为参数设置值
                prams[0].Value = userEmail;
                prams[1].Value = communityID;
                //将参数加入到连接实体中
                for (int i = 0; i < prams.Length;i++ )
                {
                    myOracleCommand.Parameters.Add(prams[i]);
                }
                // 连接数据库
                myConnection.Open();
                myOracleCommand.ExecuteNonQuery();   // 执行过程

                string result= prams[2].Value.ToString();
                myConnection.Close();
                

                if(result.Equals("1")){
                    isFollowed = true;
                }
               

            }


            context.Response.Write(isFollowed.ToString());   // 返回值
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