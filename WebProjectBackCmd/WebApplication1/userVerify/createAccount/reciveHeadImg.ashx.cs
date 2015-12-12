﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.SessionState;
using System.Configuration;
using System.Data.OracleClient;
using System.Web;
using System.IO;

namespace WebApplication1.userVerify.createAccount
{
    /// <summary>
    /// 接受用户设置的头像 并写入session 和 服务器本地文件
    /// 写入session 中的标识是 userHeadImg
    /// reciveHeadImg 的摘要说明
    /// </summary>
    public class reciveHeadImg : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string userEmail = context.Session["userEmail"].ToString();
            long fileLength = context.Request.Files[0].ContentLength;

            string fileName = context.Request.Files[0].FileName;
            int index = fileName.LastIndexOf('.');
            string type = fileName.Substring(index + 1);
            string sPath = System.Web.HttpContext.Current.Request.MapPath("/");
            DateTime date = DateTime.Now;
            string randomTime = date.Month.ToString() + date.Day.ToString() + date.Hour.ToString() + date.Minute.ToString() + date.Second.ToString() + date.Millisecond.ToString();
            string oppnetDir = "//UserHeadImg//" + userEmail + "_header" + "." + type;
            string path = sPath + oppnetDir;
            Stream input = context.Request.Files[0].InputStream;
            string json = "{}";
            try
            {
                //write
                //如果文件存在，就删除
                if (File.Exists(path))
                {
                    File.Delete(path);
                    //Console.WriteLine("\n\t保存失败！\n错误原因：可能存在相同文件");
                    //return;
                }

                //否则创建文件
                FileStream fs = new FileStream(path, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);

                byte[] buffer = new byte[fileLength];
                int ch = input.ReadByte();
                int i = 0;
                while (ch != -1)
                {
                    buffer[i] = (byte)ch;
                    ch = input.ReadByte();
                    ++i;
                }


                bw.Write(buffer);// 写入流
                bw.Close();  // 关闭流
                fs.Close();  // 关闭流
                context.Session["userHeadImg"] = "../../UserHeadImg/" + userEmail + "_header."  + type;

            }
            catch (Exception e)
            {
                json = "Wrong !Server error!";
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