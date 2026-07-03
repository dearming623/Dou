using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DouNamespace.DBUtility;

namespace DouNamespace
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                DbInitializer.EnsureDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库初始化失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new frmDouApp());
        }
    }
}
