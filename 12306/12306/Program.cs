using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _12306
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            login l = new login();
            if (l.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new main(l.name));
            }
            else
            {
                Application.Exit();
            }
            
        }
    }
}
