using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Configuration;
using System.Data.OracleClient;
using System.Collections;


namespace WebApplication1.videos.videos
{
    /// <summary>
    /// 根据前台传过来的 请求的video的类型 进行向数据库请求 并封装成相应的json 数据返回
    /// getVideosInfo 的摘要说明
    /// </summary>
    public class getVideosInfo : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string v_type = context.Request.QueryString["v_type"].ToString(); // 获取请求video的类型
            string json = "";   // 需要返回的json
            if (v_type != null && v_type != "")
            {
                /*
               *  连接数据库 并返回一条记录
               */
                string connectionStr = ConfigurationManager.ConnectionStrings["oracleCon"].ConnectionString;
                OracleConnection myConnection = new OracleConnection(connectionStr);
                OracleCommand myCommand = new OracleCommand("pk_videoOperations.getViodesInfo", myConnection);
                myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                //设置参数  
                OracleParameter[] prams = { 
                                          new OracleParameter("v_type", OracleType.VarChar,20),  
                                          new OracleParameter("my_cursor",OracleType.Cursor),
                                      };
                //设置参数类型
                prams[0].Direction = System.Data.ParameterDirection.Input;
                prams[1].Direction = System.Data.ParameterDirection.ReturnValue;
                //设置参数值
                prams[0].Value = v_type;

                //将参数加入到连接实例
                for (int i = 0; i < prams.Length; i++)
                {
                    myCommand.Parameters.Add(prams[i]);
                }
                //连接
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                OracleDataReader myDataReader = (OracleDataReader)prams[1].Value; //获取游标数据集

                ArrayList myVideos = new ArrayList();
                while (myDataReader.Read())
                {
                    MyVideo tempVideo = new MyVideo();
                    tempVideo.videoID = myDataReader[0].ToString();
                    tempVideo.videoName = myDataReader[1].ToString();
                    tempVideo.videoUrl = myDataReader[2].ToString();
                    tempVideo.videoImgeUrl = myDataReader[3].ToString();
                    tempVideo.videoShortDesc = myDataReader[4].ToString();
                    tempVideo.videoDesc = myDataReader[5].ToString();
                    tempVideo.videoType = myDataReader[6].ToString();
                    myVideos.Add(tempVideo);

                }
                myDataReader.Close();// 关闭连接
                myConnection.Close();

                /*
                *  将数据封装成json发送给前台
                */
                json += "{"; 
                string type = "";
                for (int i = 0; i < myVideos.Count; i++)
                {
                    MyVideo tempVideo = (MyVideo)myVideos[i];
                    if (!tempVideo.videoType.Equals(type))
                    {
                        //写个头 和尾部
                        if (i != 0)
                        {
                            json += "],";
                        }
                        json += "\"" + tempVideo.videoType + "\":[";
                        type = tempVideo.videoType;
                    }
                    else
                    {
                        json += ",";
                    }

                    json += "{ \"videoID\":\"" + tempVideo.videoID + "\" , \"videoName\":\"" + tempVideo.videoName + "\" , \"videoUrl\":\"" + tempVideo.videoUrl + "\" , \"videoImgeUrl\":\"" + tempVideo.videoImgeUrl + "\" , \"videoShortDesc\":\"" + tempVideo.videoShortDesc + "\" , \"videoDesc\":\"" + tempVideo.videoDesc + "\" , \"videoType\":\"" + tempVideo.videoType + "\" }";

                }
                json += "]}"; 

            }

            context.Response.Write(json);  // 写回前台
 
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }


    /// <summary>
    ///  自定义Video类
    /// </summary>
    class MyVideo
    {
       public string videoID;
       public string videoName;
       public string videoUrl;
       public string videoImgeUrl;
       public string videoShortDesc;
       public string videoDesc;
       public string videoType;

    }
}