using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            //首先获得用户的各种信息,应该是post请求中查询字符串或者是json数据
            //解析出基本的信息包括userEmail
            //首先发送邮箱验证码,若邮箱验证码能正常发送成功的话,那么就进行下面加密密码并的插入到数据库

            /*
             * 发送邮箱验证
             * 若正常发送无异常,则进行下面的解密码 和 加密 插入数据库操作
             */

            //现对密码进行解码
            //最后生成md5密码
            string rawPwd = "";
            string pwd = paraseBase64(rawPwd);
            string md5Pwd = getMd5Value(pwd);
        }

        private string getMd5Value(string pwd)
        {
            string md5Pwd = "";
            //对密码进行md5散列加密
            return md5Pwd;
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