using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.common.class_
{
    public class MyBase64
    {
        /// <summary>
        /// base64 编码
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public static string  encodeBase64(string rawData)
        {
            return System.Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(rawData));
        }

        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public static string decodeBase64(string rawData)
        {
            //解码
            return System.Text.Encoding.Default.GetString(System.Convert.FromBase64String(rawData));
        }


    }
}