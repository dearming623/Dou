using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DouNamespace.BLL;

namespace DouNamespace
{
    public partial class frmUserInfo : Form
    {
        public frmUserInfo()
        {
            InitializeComponent();

            Load();
        }

        private void Load()
        {
            int i;

            //性别
            for (i = 0; i < DouSource.SEX.Length; i++)
            {
                cbSex.Items.Add(DouSource.SEX[i]);
            }

            // LoadSex();

            //公历年
            for (i = 0; i < 145; i++)
            {
                cbNewYear.Items.Add(1901 + i);
            }

            //公历月
            for (i = 0; i < 12; i++)
            {
                cbNewMonth.Items.Add(i + 1);
            }

            //公历日
            for (i = 0; i < 31; i++)
            {
                cbNewDay.Items.Add(i + 1);
            }

            //农历年
            for (i = 0; i < DouSource.SKY.Length; i++)
            {
                cbYesrSky.Items.Add(DouSource.SKY[i]);
            }

            for (i = 0; i < DouSource.EARTH.Length; i++)
            {
                cbYearEarth.Items.Add(DouSource.EARTH[i]);
            }

            //农历月
            for (i = 0; i < 12; i++)
            {
                cbMonth.Items.Add(i + 1);
            }

            //农历日
            for (i = 0; i < 31; i++)
            {
                cbDay.Items.Add(i + 1);
            }

            // 农历时辰
            string temp = "";
            for (i = 0; i < DouSource.HourContent.Length; i++)
            {
                temp = DouSource.HourContent[i];
                cbHour.Items.Add(temp);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UserBLL bll = new UserBLL();
            bool b = bll.Exists("1");
            MessageBox.Show(b.ToString());

            if(Vaildation() == false) return;
            User usr;
            usr = new User();
            usr.Id = UserList.GenerateUserID();
            usr.Name = tbName.Text;
            usr.Sex = cbSex.SelectedIndex;
            usr.Year = Convert.ToInt32(cbNewYear.Text);
            usr.Month = Convert.ToInt32(cbNewMonth.Text);
            usr.Day = Convert.ToInt32(cbNewDay.Text);
            usr.Hour = Convert.ToInt32(nudNewHour.Value);
            usr.Minute = Convert.ToInt32(nudNewMinte.Value);

            UserList.Add(usr);

            MessageBox.Show("保存成功.", "消息");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            //this.Dispose();
        }

        /// <summary>
        /// 验证输入资料
        /// </summary>
        private bool Vaildation()
        {
            if (tbName.Text == "")
            {
                MessageBox.Show("请输入姓名","无效",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }


        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="usr"></param>
        public  void LoadData(User usr)
        {
            tbName.Text = usr.Name ;
            cbSex.SelectedIndex = usr.Sex;
            cbNewYear.Text = usr.Year.ToString();
            cbNewMonth.Text = usr.Month.ToString();
            cbNewDay.Text = usr.Day.ToString();
            nudNewHour.Value = usr.Hour;
            nudNewMinte.Value = usr.Minute;
        }
    }
}
