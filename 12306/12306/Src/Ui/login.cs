﻿using _12306.Common;
using _12306.Common.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace _12306
{
        public partial class login : Form
        {
            private static log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            private List<PicPoint> _pointList = new List<PicPoint>();
            private LoginResult _loginResult = new LoginResult();
            private CheckIsLoginResult _checkIsLoginResult = new CheckIsLoginResult();
            private UserToken _userToken = new UserToken();
            public login()
            {
                InitializeComponent();
            }
            public LoginResult LoginResult { get { return _loginResult; } }
            public CheckIsLoginResult CheckIsLoginResult { get { return _checkIsLoginResult; } }
            Thread threadLogin;
            public string name;

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
                _pointList.Clear();
                Stream stream = KyfwUtils.GetImageStream();
                Image image = Image.FromStream(stream);
                this.pictureBox1.Image = image;
                SetLoginResult("验证码获取成功");

            }
            private void Login()
            {
                
                string userName = this.txtUserName.Text.Trim();
                string password = this.txtPassword.Text.Trim();
                for (int i = 0; i < 1; i++)
                {
                    try
                    {
                        if (_pointList.Count == 0)
                        {
                            MessageBox.Show("请先选择验证图片！");
                            return;
                        }
                        string vstring = Tools.GetPointsStr(_pointList);
                        SetLoginResult("验证中");
                        bool b = KyfwUtils.CheckVerifyCode(vstring);
                        if (b)
                        {
                            SetLoginResult("验证成功！");
                            SetLoginResult("正在登陆中");
                            _loginResult = KyfwUtils.DoLogin(userName, password);
                            if (LoginResult.result_code == 0)
                            {
                                SetLoginResult("验证登录中");
                                _checkIsLoginResult = KyfwUtils.CheckIsLogin();
                                if (_checkIsLoginResult.result_code == 0)
                                {
                                    SetLoginResult("获取用户信息！");
                                   _userToken = KyfwUtils.Get1206Token(_checkIsLoginResult.newapptk);
                                   if (_userToken.result_code == 0)
                                   {
                                       SetLoginResult("获取用户信息成功！");
                                       SetLoginResult("登录成功！");
                                       this.DialogResult = DialogResult.OK;
                                   }
                                   else
                                   {
                                       SetLoginResult("获取用户信息失败！");
                                       MessageBox.Show(_userToken.result_message);
                                   }
                                }
                                else
                                {
                                    SetLoginResult("验证登录状态失败！");
                                    MessageBox.Show(_checkIsLoginResult.result_message);
                                }
                         

                            }
                            SetLoginResult(LoginResult.result_message);
                            
                        }
                        else
                        {
                            MessageBox.Show("验证失败！");
                            GetLoginCodeThread();
                        }
                    }
                    catch { }
                }
            }
            private delegate void WriteLabelDelegate(object entry);

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
                //try
                //{
                //    threadLogin.Abort();
                //}
                //catch
                //{
                //}


            }
            private void pictureBox1_Click(object sender, MouseEventArgs e)
            {
                _pointList.Add(new PicPoint() { X = e.X, Y = e.Y });

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
