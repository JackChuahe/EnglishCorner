using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using WebApplication1.common.class_;
using System.Configuration;
using System.Data.OracleClient;
using System.IO;
using System.Text;
namespace WebApplication1.userVerify.createAccount
{
    /// <summary>
    /// createTheAccount 的摘要说明
    /// 当注册页面的验证码通过  邮箱确认无误后,就可以开始提交数据了
    /// 这个部分主要是实现用户注册,插入到数据库,并且发出激活邮箱
    /// </summary>
    public class createTheAccount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string first_name = context.Request.Form["first_name"];
            string last_name = context.Request.Form["last_name"];
            string userEmail = context.Request.Form["userEmail"];
            string rawPwd = context.Request.Form["pwd"];

            bool isOK = false;

            //首先获得用户的各种信息,应该是post请求中查询字符串或者是json数据
            //解析出基本的信息包括userEmail
            //首先发送邮箱验证码,若邮箱验证码能正常发送成功的话,那么就进行下面加密密码并的插入到数据库


            if (!sendEmail(userEmail))   //  如果发送失败
            {
                //
            }
            else   // 如果发送成功
            {

                //现对密码进行解码
                //最后生成md5密码
                string pwdWithToken = MyBase64.decodeBase64(rawPwd);   // 解码
                string pwd = deleteToken(pwdWithToken,context.Session["token"].ToString());
                string md5Pwd = Md5.endcodeMd5(pwd);
                // 写入数据库
                string connectionStr = ConfigurationManager.ConnectionStrings["oracleCon"].ConnectionString;
                OracleConnection myConnection = new OracleConnection(connectionStr);
                OracleCommand myOracleCommand = new OracleCommand("userVerifyPK.insertUserBaseInfo", myConnection); // 调用包 进行插入数据
                myOracleCommand.CommandType = System.Data.CommandType.StoredProcedure; // 设置为访问存储类型形式
                //设置参数  
                OracleParameter[] prams = { 
                                          new OracleParameter("v_userEmail", OracleType.VarChar,50),  
                                          new OracleParameter("v_pwd",OracleType.VarChar,80),
                                          new OracleParameter("first_name",OracleType.VarChar,15),
                                          new OracleParameter("last_name",OracleType.VarChar,15),
                                          new OracleParameter("isOK",OracleType.VarChar,2)
                                      };
                //设置参数类型
                prams[0].Direction = System.Data.ParameterDirection.Input;
                prams[1].Direction = System.Data.ParameterDirection.Input;
                prams[2].Direction = System.Data.ParameterDirection.Input;
                prams[3].Direction = System.Data.ParameterDirection.Input;
                prams[4].Direction = System.Data.ParameterDirection.ReturnValue;
                //设置参数值
                prams[0].Value = userEmail;
                prams[1].Value = md5Pwd;
                prams[2].Value = first_name;
                prams[3].Value = last_name;
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
            /*
             * 发送邮箱验证
             * 若正常发送无异常,则进行下面的解密码 和 加密 插入数据库操作
             */

        }

        private string deleteToken(string pwdWithToken,string token)
        {
            string pwd = pwdWithToken.Replace(token, "");
            return pwd;
            
        }
        /// <summary>
        /// 发送邮箱验证信息
        /// </summary>
        /// <param name="userEmail"></param>
        private bool sendEmail(string userEmail)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("601825672@qq.com");   //发件人的邮箱地址
                msg.Subject = "English Corner Account Verify";  //邮件主题
                msg.Body = getBody(userEmail);//邮件正文
                msg.To.Add(userEmail);
                msg.IsBodyHtml = true;  //邮件正文是否支持html的值
                SmtpClient sc = new SmtpClient();
                sc.Host = "smtp.qq.com";//smtp.qq.com
                sc.Port = 25;
                NetworkCredential nc = new NetworkCredential("601825672", "cai601825672");  //验证凭据 1607977350：是邮箱账号，********：是邮箱密码
                sc.Credentials = nc;
                sc.Send(msg);
                return true;
            }
            catch (Exception my)
            {
                // 错误的邮箱
                return false;
            }
        }

        /// <summary>
        /// 生成发送邮箱验证的html页面
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        private string getBody(string userEmail)
        {
            string path = "../../common/txt/Email.html";
            string href = "222.196.200.38";
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            String body = "";
            while ((line = sr.ReadLine()) != null)
            {
                body += line;
            }
            sr.Close();
            body.Replace("NO1REPLACE",userEmail);
            body.Replace("NO2REPLACE", href);
            return body;
        }




        private string paraseBase64(string rawPwd)
        {
            string pwd = "";
            //先进行base64 解码
            
            //再去掉token,形成原始密码

            return pwd;
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