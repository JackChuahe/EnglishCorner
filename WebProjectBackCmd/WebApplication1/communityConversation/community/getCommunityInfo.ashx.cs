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
                string connectionStr = ConfigurationManager.ConnectionStrings["oracleCon"].ConnectionString;
                OracleConnection myConnection = new OracleConnection(connectionStr);
                OracleCommand myCommand = new OracleCommand("communityOperation.getCommunityInfo", myConnection);
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
                *  将数据封装成json发送给前台
                */
                while (myDataReader.Read())
                {
                    json += "{ \"communityName\":\"" + myDataReader[0] + "\" , \"communityDesc\":\"" + myDataReader[1] + "\" , \"headImgUrl\":\"" +myDataReader[2] +  "\" }";
                }
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