using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12306
{
    //"{\"result_message\":\"登录成功\",\"result_code\":0,\"uamtk\":\"w32B99uQJdJkwyIsMhomDtzY4nxAUFkX577lYgxhS1S0\"}"
    public class LoginResult
    {
        public string result_message { get; set; }
        public int result_code { get; set; }
        public string uamtk { get; set; }
    }
}
