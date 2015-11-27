using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace WebApplication1
{
    public class Md5
    {

        public static string endcodeMd5(string pwd)
        {
            byte[] data = System.Text.Encoding.Default.GetBytes(pwd);
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] md5data = md5.ComputeHash(data);//计算data字节数组的哈希值 
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length - 1; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }

    }
}