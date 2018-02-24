﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;

namespace _12306.Common
{
    public class HttpUtils
    {
        public static CookieContainer CookieContainers = new CookieContainer();
        public static string FireFoxAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.2.23) Gecko/20110920 Firefox/3.6.23";
        public static string IE7 = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; InfoPath.2; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET4.0C; .NET4.0E)";
        public static string EDGE = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.79 Safari/537.36 Edge/14.14393";

        public static string Accept = "application/json, text/javascript, */*; q=0.01";
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
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.KeepAlive = true;
                req.Method = method.ToUpper();
                req.AllowAutoRedirect = true;
                req.CookieContainer = CookieContainers;
                req.ContentType = ContentType;
                req.UserAgent = EDGE;
                req.Accept = Accept;
                req.Timeout = 50000;
                if (method.ToUpper() == "POST" && data != null)
                {
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] postBytes = encoding.GetBytes(data); ;
                    req.ContentLength = postBytes.Length;
                    Stream st = req.GetRequestStream();
                    st.Write(postBytes, 0, postBytes.Length);
                    st.Close();
                }
                System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
                {
                    return true;
                };
                Encoding myEncoding = Encoding.GetEncoding("UTF-8");
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                Stream resst = res.GetResponseStream();
                StreamReader sr = new StreamReader(resst, myEncoding);
                string str = sr.ReadToEnd();
                return str;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static string GetResponse(string url, string method, string data, string refer)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.KeepAlive = true;
                req.Method = method.ToUpper();
                req.AllowAutoRedirect = true;
                req.CookieContainer = CookieContainers;
                req.ContentType = ContentType;
                req.UserAgent = EDGE;
                req.Accept = Accept;
                req.Timeout = 50000;
                req.Referer = refer;
                if (method.ToUpper() == "POST" && data != null)
                {
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] postBytes = encoding.GetBytes(data); ;
                    req.ContentLength = postBytes.Length;
                    Stream st = req.GetRequestStream();
                    st.Write(postBytes, 0, postBytes.Length);
                    st.Close();
                }
                System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
                {
                    return true;
                };
                Encoding myEncoding = Encoding.GetEncoding("UTF-8");
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                Stream resst = res.GetResponseStream();
                StreamReader sr = new StreamReader(resst, myEncoding);
                string str = sr.ReadToEnd();
                return str;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static Stream GetResponseImage(string url)
        {
            Stream resst = null;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.KeepAlive = true;
                req.Method = "GET";
                req.AllowAutoRedirect = true;
                req.CookieContainer = CookieContainers;
                req.ContentType = ContentType;
                req.UserAgent = EDGE;
                req.Accept = Accept;
                req.Timeout = 50000;
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
        public static string GetRegexString(string pattern, string source)
        {
            Regex r = new Regex(pattern);
            MatchCollection mc = r.Matches(source);
            return mc[0].Groups[1].Value;
        }
        public static string[] GetRegexStrings(string pattern, string source)
        {
            Regex r = new Regex(pattern);
            MatchCollection mcs = r.Matches(source);
            string[] ret = new string[mcs.Count];
            for (int i = 0; i < mcs.Count; i++)
                ret[i] = mcs[i].Groups[1].Value;
            return ret;
        }
    }
}
