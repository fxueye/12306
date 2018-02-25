using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace _12306.Common
{
    public class API
    {
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie(string A_0, string A_1, string A_2);

        //用户登录Init
        public static string UserLoginInit = "https://kyfw.12306.cn/otn/login/init";
        //检测用户是否登录
        public static string UserAuthUAMTK = "https://kyfw.12306.cn/passport/web/auth/uamtk";
        //获取登录验证码
        public static string UserGetVerifyImg = "https://kyfw.12306.cn/passport/captcha/captcha-image?login_site=E&module=login&rand=sjrand&{0}";
        //检测登录验证码
        public static string UserCheckVerify = "https://kyfw.12306.cn/passport/captcha/captcha-check";
        //登录12306
        public static string UserLogin12306 = "https://kyfw.12306.cn/passport/web/login";
        //获取登录信息
        public static string UserGetToken = "https://kyfw.12306.cn/otn/uamauthclient";
        //提交订单
        public static string OrderSubmitOrderURL = "https://kyfw.12306.cn/otn/leftTicket/submitOrderRequest";
        //初始化订单页面
        public static string OrderInitOrderURL = "https://kyfw.12306.cn/otn/confirmPassenger/initDc";
        //检测订单
        public static string OrderCheckedURL = "https://kyfw.12306.cn/otn/confirmPassenger/checkOrderInfo";
        //获取车票数和排队人数
        public static string OrderGetCountURL = "https://kyfw.12306.cn/otn/confirmPassenger/getQueueCount";
        //加入购买队列
        public static string OrderJoinBuyQueue = "https://kyfw.12306.cn/otn/confirmPassenger/confirmSingleForQueue";
        //获取取票码
        public static string OrderGetTicketCode = "https://kyfw.12306.cn/otn/confirmPassenger/queryOrderWaitTime?random=1516252521719&tourFlag=dc&_json_att=&REPEAT_SUBMIT_TOKEN=%s";
        //获取站台信息
        public static string QueryGetStation = "https://kyfw.12306.cn/otn/resources/js/framework/station_name.js?station_version=1.9042";
        //查询车次初始化
        public static string QueryScheduleInit = "https://kyfw.12306.cn/otn/leftTicket/init";
        //车次查询日志
        public static string QueryScheduleLog = "https://kyfw.12306.cn/otn/leftTicket/log?leftTicketDTO.train_date=%s&leftTicketDTO.from_station=%s&leftTicketDTO.to_station=%s&purpose_codes=ADULT";
        //查询车次信息
        public static string QuerySchedule = "https://kyfw.12306.cn/otn/%s?leftTicketDTO.train_date=%s&leftTicketDTO.from_station=%s&leftTicketDTO.to_station=%s&purpose_codes=ADULT";
        //查询乘客信息
        public static string QueryPassenger = "https://kyfw.12306.cn/otn/confirmPassenger/getPassengerDTOs";
        //登录页面
        public static string UserLoginPage = "https://kyfw.12306.cn/otn/passport?redirect=/otn/login/userLogin";
    }
}
