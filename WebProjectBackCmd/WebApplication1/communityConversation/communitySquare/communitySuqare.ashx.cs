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
    /// 当前台请求社区广场的时候，返回前面 社区的所有信息
    /// communitySuqare 的摘要说明
    /// </summary>
    public class communitySuqare : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            /*
             *  连接数据库 返回所有的社区
             */
            string json = "";

            {
                /*
                *  连接数据库 并返回所有的数据库中的社区的记录
                */
                string connectionStr = ConfigurationManager.ConnectionStrings["oracleCon"].ConnectionString;
                OracleConnection myConnection = new OracleConnection(connectionStr);
                OracleCommand myCommand = new OracleCommand("communityOperation.getCommunitiesInfo", myConnection);
                myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                //设置参数  
                OracleParameter[] prams = {  
                                          new OracleParameter("my_cursor",OracleType.Cursor),
                                      };
                //设置参数类型
                prams[0].Direction = System.Data.ParameterDirection.ReturnValue;
                //设置参数值
                //prams[0].Value = communityID;

                //将参数加入到连接实例

                myCommand.Parameters.Add(prams[0]);

                //连接
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                OracleDataReader myDataReader = (OracleDataReader)prams[0].Value; //获取游标数据集
                /*
                *  将数据封装成json发送给前台
                */
                json += "{\"communities\":[";
                if (myDataReader.Read()) {
                    json += "{ \"communityID\":\"" + myDataReader[0] + "\" , \"communityName\":\"" + myDataReader[1] + "\" , \"communityPic\":\"" + myDataReader[2] + "\" , \"userName\":\"" + myDataReader[3] + "\" , \"headImageUrl\":\"" + myDataReader[4] + "\" }";
                }
                while (myDataReader.Read())
                {
                    json += ",{ \"communityID\":\"" + myDataReader[0] + "\" , \"communityName\":\"" + myDataReader[1] + "\" , \"communityPic\":\"" + myDataReader[2] + "\" , \"userName\":\"" + myDataReader[3] + "\" , \"headImageUrl\":\"" + myDataReader[4] + "\" }";
                }
                json += "]}";
                myDataReader.Close();
                myConnection.Close();


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