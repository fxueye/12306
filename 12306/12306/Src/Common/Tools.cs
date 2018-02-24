using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace _12306.Common
{
    public class Tools
    {
        public static string GetPointsStr(List<PicPoint> points)
        {
            string result = string.Empty;
            foreach (var point in points)
            {
                result += string.Format("{0},{1},", point.X, point.Y);
            }
            result = result.Length > 0 ? result.Substring(0, result.Length - 1) : result;
            return result;
        }
        public static string GetUrlString(Dictionary<string, string> data)
        {
            StringBuilder sb = new StringBuilder();
            List<string> keys = new List<string>(data.Keys);
            for (int i = 0; i < keys.Count; i++)
            {
                if (i == keys.Count - 1)
                {
                    sb.Append(keys[i]).Append("=").Append(data[keys[i]]);
                }
                else
                {
                    sb.Append(keys[i]).Append("=").Append(data[keys[i]]).Append("&");
                }
            }
            return sb.ToString();
        }
        
    }
}
