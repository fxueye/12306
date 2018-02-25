using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;

namespace _12306.Common.Utils
{
    public class HttpUtils
    {
        private static log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static CookieContainer CookieContainers = new CookieContainer();
        public static string FireFoxAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.2.23) Gecko/20110920 Firefox/3.6.23";
        public static string IE7 = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; InfoPath.2; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET4.0C; .NET4.0E)";
        public static string EDGE = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.79 Safari/537.36 Edge/14.14393";
        public static string CHROME = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.87 Safari/537.36";

        public static string ORIGIN = "https://kyfw.12306.cn";
        //public static string Accept = "application/json, text/javascript, */*; q=0.01";
        public static string Accept = "*/*";
        public static string ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method">"POST" or "GET"</param>
        /// <param name="data">when the method is "POST", the data will send to web server, if the method is "GET", the data should be string.empty</param>
        /// <returns></returns>
        public static string GetResponse(string url, string method, string data)
        {
            return GetResponse(url, method, data, "");
        }
        public static string GetResponse(string url, string method, string data, string refer)
        {
            try
            {
                HttpWebRequest req = GetWebRequest(url, method, refer);
                if (method.ToUpper() == "POST" && data != null)
                {
                    byte[] postBytes = Encoding.UTF8.GetBytes(data); ;
                    req.ContentLength = postBytes.Length;
                    Stream st = req.GetRequestStream();
                    st.Write(postBytes, 0, postBytes.Length);
                    st.Close();
                }
                System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
                {
                    return true;
                };
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                string AcceptEncoding = res.ContentEncoding;
                string str = string.Empty;
                if (AcceptEncoding.Contains("gzip"))
                {
                    System.IO.Compression.GZipStream gzsst = new System.IO.Compression.GZipStream(res.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);
                    StreamReader streamReader = new StreamReader(gzsst, Encoding.UTF8);
                    str = streamReader.ReadToEnd();
                }
                else
                {
                    Stream resst = res.GetResponseStream();
                    StreamReader sr = new StreamReader(resst, Encoding.UTF8);
                    str = sr.ReadToEnd();
                }
                return str;
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                return string.Empty;
            }
        }
        public static Stream GetResponseImage(string url)
        {
            Stream resst = null;
            try
            {
                HttpWebRequest req = GetWebRequest(url,"GET","");
                System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
                {
                    return true;
                };
                Encoding myEncoding = Encoding.GetEncoding("UTF-8");
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                resst = res.GetResponseStream();
                return resst;
            }
            catch
            {
                return null;
            }
        }
        public static HttpWebRequest GetWebRequest(string url,string method,string refer)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.KeepAlive = true;
            req.Method = method.ToUpper();
            req.AllowAutoRedirect = true;
            req.CookieContainer = CookieContainers;
            req.ContentType = ContentType;
            req.UserAgent = CHROME;
            req.Accept = Accept;
            req.Timeout = 50000;
            if (refer != "")
                req.Referer = refer;
            //req.Headers.Add("Accept-Encoding", "gzip, deflate");
            //req.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
            //req.Headers.Add("X-Requested-With", "XMLHttpRequest");
            //req.Headers.Add("Origin", ORIGIN);
            return req;
        }
    }
}
