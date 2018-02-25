using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12306.Common.Utils
{
    public class KyfwUtils
    {
        private static log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
            string result = HttpUtils.GetResponse(API.UserCheckVerify, "POST", Tools.GetUrlString(data), API.UserLoginInit);
            CheckVerifyResult cvr = JsonUtils.DeserializeToObj<CheckVerifyResult>(result);
            if (cvr.result_code == "4")
            {
                return true;
            }
            return false;
        }
        public static LoginResult DoLogin(string username, string password)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("username", username);
            data.Add("password", password);
            data.Add("appid", "otn");
            string result = HttpUtils.GetResponse(API.UserLogin12306, "POST", Tools.GetUrlString(data), API.UserLoginInit);
            _log.InfoFormat("DoLogin result:{0}", result);
            LoginResult lr = JsonUtils.DeserializeToObj<LoginResult>(result);
            return lr;
        }
        public static CheckIsLoginResult CheckIsLogin()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("appid", "otn");
            string result = HttpUtils.GetResponse(API.UserAuthUAMTK, "POST", Tools.GetUrlString(data), API.UserLoginInit);
            _log.InfoFormat("CheckIsLogin result:{0}",result);
            CheckIsLoginResult cil = JsonUtils.DeserializeToObj<CheckIsLoginResult>(result);
            return cil;
        }
        public static UserToken Get1206Token(string appTk){
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("tk", appTk);
            string result = HttpUtils.GetResponse(API.UserGetToken, "POST", Tools.GetUrlString(data), API.UserLoginPage);
            _log.InfoFormat("Get1206Token result:{0}",result);
            UserToken ut = JsonUtils.DeserializeToObj<UserToken>(result);
            return ut;
        }
    }
}
