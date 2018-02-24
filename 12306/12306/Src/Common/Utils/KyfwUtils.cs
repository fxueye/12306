using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12306.Common
{
    public class KyfwUtils
    {
        public static Stream GetImageStream()
        {
            Stream stream = null;
            Random r = new Random();
            Double rd = r.NextDouble();
            stream = HttpUtils.GetResponseImage(string.Format(API.UserGetVerifyImg, rd));
            return stream;
        }
        public static bool CheckVerifyCode(string verifyCode)
        {
            Dictionary<string,string> data = new Dictionary<string,string>();
            data.Add("answer",verifyCode);
            data.Add("login_site","E");
            data.Add("rand","sjrand");
            string v = HttpUtils.GetResponse(API.UserCheckVerify, "POST", Common.GetUrlString(data), API.UserLoginInit);

            return false;
        }
    }
}
