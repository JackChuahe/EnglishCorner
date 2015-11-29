using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Data.OracleClient;
using System.Configuration;

namespace WebApplication1.common.HttpRequest
{
    /// <summary>
    /// 若用户登录成功，则可以返回其头像和用户名 和是否登录成功的标识
    /// getHeadImgAndUserEmail 的摘要说明
    /// </summary>
    public class getHeadImgAndUserEmail : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string returnJson = "";
            Object obj = context.Session["userEmail"];

           // if (obj != null)
            if(true)
            {
                //string userEmail = obj.ToString();
                string userEmail = "601825672@qq.com";
                // 若登录过
                //访问数据库
                string connectionStr = ConfigurationManager.ConnectionStrings["oracleCon"].ConnectionString;
                OracleConnection myConnection = new OracleConnection(connectionStr);
                OracleCommand myOracleCommand = new OracleCommand("commonOpreationPK.getHeadImgAndUserEmail", myConnection);
                myOracleCommand.CommandType = System.Data.CommandType.StoredProcedure; // 设置为调用存储过程
                OracleParameter[] prams = {
                                               new OracleParameter("v_userEmail",OracleType.VarChar,50),
                                               new OracleParameter("v_lastname",OracleType.VarChar,15),
                                               new OracleParameter("userHeadImgUrl",OracleType.VarChar,50)
                                           };
                // 设置参数类型
                prams[0].Direction = System.Data.ParameterDirection.Input;
                prams[1].Direction = System.Data.ParameterDirection.Output;
                prams[2].Direction = System.Data.ParameterDirection.ReturnValue;   // 接收返回值
                // 为参数设置值
                prams[0].Value = userEmail;
                //将参数加入到连接实体中
                for (int i = 0; i < prams.Length;i++ )
                {
                    myOracleCommand.Parameters.Add(prams[i]);
                }
                // 连接数据库
                myConnection.Open();
                myOracleCommand.ExecuteNonQuery();   // 执行过程
                string last_name = prams[1].Value.ToString();
                string img_url = prams[2].Value.ToString();

                myConnection.Close();
                //拼接json
                returnJson = "{ \"last_name\":\"" + last_name + "\" , \"headImgUrl\":\"" + img_url + "\" , \"isLogin\":\"true\"}";

            }
            else
            {
                // 未登录过
                returnJson = ""; ;

            }

            context.Response.Write(returnJson);   // 返回值
  
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