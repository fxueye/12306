using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using _12306Helper.Helper;
using System.Net;
using System.Diagnostics;
using log4net;

namespace _12306Helper
{
    public partial class login : Form
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private List<PicPoint> _pointList = new List<PicPoint>();
        public login()
        {
            InitializeComponent();
        }
        private void Form1_Activated(object sender, EventArgs e)
        {
        }
        Thread threadLogin;
        public string name;
        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Verify login field
            if (this.txtUserName.Text.Length < 2)
            {
                MessageBox.Show("请输入用户名!");
                return;
            }
            if (this.txtPassword.Text.Length < 2)
            {
                MessageBox.Show("请输入密码!");
                return;
            }
            // work
            threadLogin = new Thread(new ThreadStart(Login));
            threadLogin.Name = "LoginThread";
            threadLogin.Start();
        }
        private void GetLoginCode()
        {
            SetLoginResult("获取验证码中");
            string url = "https://kyfw.12306.cn/passport/captcha/captcha-image?login_site=E&module=login&rand=sjrand&%{0}";
            Stream stream = null;
            Random r = new Random();
            Double rd = r.NextDouble();
            stream = HttpHelper.GetResponseImage(string.Format(url, rd));
            Image image = Image.FromStream(stream);
            this.pictureBox1.Image = image;
            SetLoginResult("验证码获取成功");

        }
        private void Login()
        {
            SetLoginResult("正在登陆中");
            string userName = this.txtUserName.Text.Trim();
            string password = this.txtPassword.Text.Trim();
            string loginUrl = "https://dynamic.12306.cn/otsweb/loginAction.do?method=login";
            for (int i = 0; i < 10000; i++)
            {
                try
                {
                    // Get random number
                    string randomUrl = "https://dynamic.12306.cn/otsweb/loginAction.do?method=loginAysnSuggest";
                    string afterRandom = HttpHelper.GetResponse(randomUrl, "POST", string.Empty);
                    string[] randoms = afterRandom.Split('"');
                    string random = randoms[3];
                    string data = "loginRand=" + random + "&loginUser.user_name=" + userName + "&nameErrorFocus=&user.password=" + password + "&passwordErrorFocus=&randCode="  + "&randErrorFocus=";
                    string afterLogin = HttpHelper.GetResponse(loginUrl, "POST", data);
                    if (afterLogin.Contains("密码输入错误"))
                    {
                        SetLoginResult("密码输入错误，请重新输入密码");
                        break;
                    }
                    else if (afterLogin.Contains("欢迎您"))
                    {
                        SetLoginResult("登陆成功");
                        doc.LoadHtml(afterLogin);
                        string xpath = "/html/body/div/div/div[1]/div[2]/div[1]";
                        name=doc.DocumentNode.SelectSingleNode(xpath).InnerText.Replace("\r\n","").Replace("\t","");
                        this.DialogResult = DialogResult.OK;  
                        break;
                    }
                    else if (afterLogin.Contains("请输入正确的验证码"))
                    {
                        SetLoginResult("登陆失败，请输入正确的验证码");
                        break;
                    }
                    else if (afterLogin.Contains("您的用户已经被锁定"))
                    {
                        SetLoginResult("您的用户已经被锁定,请稍候再试");
                        break;
                    }
                    else if (afterLogin.Contains("当前访问用户过多，请稍后重试"))
                    {
                        SetLoginResult("当前访问用户过多，请稍后重试，进行第" + i.ToString() + "次重试");
                    }
                    else
                    {
                        SetLoginResult("登录失败，进行第" + i.ToString() + "次重试");
                    }
                }
                catch { }
            }
        }
        private delegate void WriteLabelDelegate(object entry);
        private delegate void WriteYZMDelegate(object yzm);
        private void WriteLoginResult(object text)
        {
            this.toolStripStatusLabel1.Text = text.ToString();
        }
        private void SetLoginResult(string text)
        {
            this.statusStrip1.Invoke(new WriteLabelDelegate(WriteLoginResult), text);
        }

        private void GetLoginCodeThread()
        {
            Thread threadGetLoginCode = new Thread(new ThreadStart(GetLoginCode));
            threadGetLoginCode.Name = "GetLoginCodeThread";
            threadGetLoginCode.Start();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            GetLoginCodeThread();
        }
        private void btnLoginStop_Click(object sender, EventArgs e)
        {
            try
            {
                threadLogin.Abort();
            }
            catch
            {
            }
        }
        private void pictureBox1_Click(object sender, MouseEventArgs e)
        {
            //GetLoginCodeThread();
            Console.WriteLine(string.Format("X:{0}Y:{1}",e.X,e.Y));
            log.Debug(string.Format("mouse event X:{0} Y:{1}", e.X, e.Y));
            _pointList.Add(new PicPoint() { X = e.X,Y = e.Y });
            
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://kyfw.12306.cn/otn/forgetPassword/initforgetMyPassword");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://kyfw.12306.cn/otn/regist/init");
        }
    }
}
