using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12306.Common
{
    public class Common
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
    }
}
