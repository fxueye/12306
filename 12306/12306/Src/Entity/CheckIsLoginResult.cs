using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12306
{
    public class CheckIsLoginResult
    {
        //"{\"result_message\":\"验证通过\",\"result_code\":0,\"apptk\":null,\"newapptk\":\"TDR1su0x_nNxK435wdec1zqf8KoDUWBI_N7UsgozS1S0\"}"
        public string result_message { get; set; }
        public int result_code { get; set; }
        public string apptk { get; set; }
        public string newapptk { get; set; }
    }
}
