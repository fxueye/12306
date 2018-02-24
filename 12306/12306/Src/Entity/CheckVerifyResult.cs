using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12306
{
    //"{\"result_message\":\"验证码校验成功\",\"result_code\":\"4\"}"
    public class CheckVerifyResult
    {
        public string result_message { get; set; }
        public string result_code { get; set; }
    }
}
