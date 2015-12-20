using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using WebApplication1.common.class_;
using System.Configuration;
using System.Data.OracleClient;

namespace WebApplication1.userVerify.createAccount
{
    /// <summary>
    /// 设置用户的头像和个人信息
    /// 将用户上传的数据和已经上穿的头像进行更新至数据库
    /// setProfileInfo 的摘要说明
    /// </summary>
    public class setProfileInfo : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            bool isOK = false;
            string userEmail = (string)context.Session["userEmail"];
            string headImg = (string)context.Session["userHeadImg"];
            userEmail = "993687952@qq.com";
            if(userEmail == null)
            {
                context.Response.Write("False");
                return ;
            }

            if (headImg == null || headImg.Equals(""))
            {
                //用户没有上传头像的情况下
                headImg = "../../UserHeadImg/default.png";
            }


            //
            // 获取前台传入的相关数据  生日信息，性别，等
            string v_birthday = context.Request.Form["birthday"];
            string gender = context.Request.Form["gender"];
            DateTime dt;
            //格式化时间
            if (v_birthday != null && !v_birthday.Equals(""))
            {
                dt = Convert.ToDateTime(v_birthday);
            }
            else
            {
                dt = Convert.ToDateTime("2015-1-1");
            }
            //开始连接数据库
            if (dt != null && gender != null )
            {
                string connectionStr = ConfigurationManager.ConnectionStrings["oracleCon"].ConnectionString;
                OracleConnection myConnection = new OracleConnection(connectionStr);
                OracleCommand myOracleCommand = new OracleCommand("userVerifyPK.updateUserInfo", myConnection); // 调用包 进行插入数据
                myOracleCommand.CommandType = System.Data.CommandType.StoredProcedure; // 设置为访问存储类型形式
                //设置参数  
                OracleParameter[] prams = { 
                                          new OracleParameter("v_userEmail", OracleType.VarChar,50),  
                                          new OracleParameter("v_headImg",OracleType.VarChar,60),
                                          new OracleParameter("v_birthday",OracleType.DateTime),
                                          new OracleParameter("v_gender",OracleType.VarChar,8),
                                           new OracleParameter("result",OracleType.VarChar,1),

                                      };
                //设置参数类型
                prams[0].Direction = System.Data.ParameterDirection.Input;
                prams[1].Direction = System.Data.ParameterDirection.Input;
                prams[2].Direction = System.Data.ParameterDirection.Input;
                prams[3].Direction = System.Data.ParameterDirection.Input;
                prams[4].Direction = System.Data.ParameterDirection.ReturnValue;
                //设置参数值
                prams[0].Value = userEmail;
                prams[1].Value = headImg;
                prams[2].Value = dt;
                prams[3].Value = gender;

                //将参数加入到连接实例
                for (int i = 0; i < prams.Length; i++)
                {
                    myOracleCommand.Parameters.Add(prams[i]);
                }
                //连接
                myConnection.Open();
                myOracleCommand.ExecuteNonQuery();
                string result = prams[4].Value.ToString();
                myConnection.Close();
                if (result.Equals("1"))
                {
                    isOK = true;
                }


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