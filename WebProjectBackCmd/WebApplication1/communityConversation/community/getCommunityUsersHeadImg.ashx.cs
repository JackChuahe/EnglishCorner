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
    /// 获取指定community的所有用户的头像的url地址
    /// getCommunityUsersHeadImg 的摘要说明
    /// </summary>
    public class getCommunityUsersHeadImg : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string json = "";
            string communityID = context.Request.QueryString["communityID"].ToString();
            if (communityID != null || communityID != "") 
            {
                /*
                               *  连接数据库 并返回一条记录
                               */
                string connectionStr = ConfigurationManager.ConnectionStrings["oracleCon"].ConnectionString;
                OracleConnection myConnection = new OracleConnection(connectionStr);
                OracleCommand myCommand = new OracleCommand("communityOperation.getCommunityUsersHeadImg", myConnection);
                myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                //设置参数  
                OracleParameter[] prams = { 
                                          new OracleParameter("v_communityID", OracleType.VarChar,20),  
                                          new OracleParameter("my_cursor",OracleType.Cursor),
                                      };
                //设置参数类型
                prams[0].Direction = System.Data.ParameterDirection.Input;
                prams[1].Direction = System.Data.ParameterDirection.ReturnValue;
                //设置参数值
                prams[0].Value = communityID;

                //将参数加入到连接实例
                for (int i = 0; i < prams.Length; i++)
                {
                    myCommand.Parameters.Add(prams[i]);
                }
                //连接
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                OracleDataReader myDataReader = (OracleDataReader)prams[1].Value; //获取游标数据集
                /*
                *  将数据封装成json
                */
                json += "{ \"headImages\":[";
                while (myDataReader.Read())
                {
                    json += "{\"url\":\""+myDataReader[0]+"\"},";
                }
                json += "{\"url\":\"\"}";
                json += "]}";
                myDataReader.Close();
                myConnection.Close();



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