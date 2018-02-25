using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12306
{
    public class UserToken
    {
        //"{\"apptk\":\"8SqwRq4djM5YEPgW3SQadsX6GcOshdtYNZGmEAbcS1S0\",\"newapptk\":\"HwRbUBIRymFrfYsepW48FypjZHPnET9Gz0cxVQ36S1S0\",\"result_code\":0,\"result_message\":\"验证通过\",\"username\":\"史可威\"}"
        public string result_message { get; set; }
        public int result_code { get; set; }
        public string apptk { get; set; }
        public string newapptk { get; set; }
        public string username { get; set; }
    }
}
