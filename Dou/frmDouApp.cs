using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using Dou.Common.Util;

namespace DouNamespace
{
    public partial class frmDouApp : Form
    {
        private DateTime dt = DateTime.Now;
        private ChineseCalendar cc = null;
        private User usr = null;
        Plate p = new Plate();

        public frmDouApp()
        {
            InitializeComponent();
        }



        private void btnGo_Click(object sender, EventArgs e)
        {

           
            //DouManager dm = new DouManager();
            //dm.CreatePlate(1983,5,13,5,1,1,"Ming","Deng");

            //MessageBox.Show(DouUtility.DeasilCountZone(0, 3).ToString());
            StarList.Load();

            p = new Plate();
           // p.LiuYear = Convert.ToInt32(this.cbLiuYear.SelectedItem);
            ChineseCalendar cc = new ChineseCalendar();
            p.LiuYearSky = cc.GetYearSky(Convert.ToInt32(this.cbLiuYear.SelectedItem));

            p.YearSky = cbYesrSky.SelectedIndex;
            p.YearEarth = cbYearEarth.SelectedIndex;
            p.month = cbMonth.SelectedIndex;
            p.day = cbDay.SelectedIndex;
            p.hour = cbHour.SelectedIndex;
            p.Sex = cbSex.SelectedIndex;
            p.Type = 0;
            p.first_name = "";
            p.CreatePlate();

            
            //p.CreatePlate(cbYesrSky.SelectedIndex, cbYearEarth.SelectedIndex, cbMonth.SelectedIndex, cbDay.SelectedIndex, cbHour.SelectedIndex, cbSex.SelectedIndex, 0, tbName.Text, "");
            

            this.bodyDou.LoadInfo(p);
            this.bodyDou.DisplayBodyPlate();

            this.ucDouYear.LoadInfo(p);
            this.ucDouYear.DisplayBodyPlate();

            tabControl1.SelectedTab = tabPage1;

        }

      

        private void FrmDou_Load(object sender, EventArgs e)
        {
            int i, j, k;

            List<User> userlist = UserList.FindAll();

            dgvUserInfoList.DataSource = userlist;

            for ( i = 0; i < userlist.Count; i++)
            {
                //
            }


            cc = new ChineseCalendar(dt);

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
                cbLiuYear.Items.Add(1901 + i);
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

            InitNewCalendar();
            InitOldCalendar();
            InitLiu();
            DisplayChineseDate();
            LoadUserList();
        }

        /// <summary>
        /// 初始化流年
        /// </summary>
        private void InitLiu()
        {
            if (this.cbLiuYear.Items.Count > 0) this.cbLiuYear.SelectedItem = dt.Year;
        }

        /// <summary>
        /// 加载用户到combox
        /// </summary>
        private void LoadUserList()
        {
            // 加载用户
            int i;
            List<User> users = new List<User>();
            this.cbUsers.Items.Clear();

            users = UserList.FindAll();

            for (i = 0; i < users.Count; i++)
            {
                this.cbUsers.Items.Add(users[i].Name);
            }
        }

        /// <summary>
        /// 初始化旧历
        /// </summary>
        private void InitOldCalendar()
        {
            cbYesrSky.SelectedIndex = cc.YearSky;
            cbYearEarth.SelectedIndex = cc.YearEarth;

            if (cbMonth.Items.Count > 0) cbMonth.SelectedItem = cc.ChineseMonth;
            if (cbDay.Items.Count > 0) cbDay.SelectedItem = cc.ChineseDay;
            if (cbHour.Items.Count > 0) cbHour.SelectedIndex = 0;

        }

        /// <summary>
        /// 初始化新历
        /// </summary>
        private void InitNewCalendar()
        {
            nudNewHour.Value = dt.Hour;
            nudNewMinte.Value = dt.Minute;

            if (cbSex.Items.Count > 0) cbSex.SelectedIndex = 0;
            if (cbNewYear.Items.Count > 0) cbNewYear.SelectedItem = dt.Year;
            if (cbNewMonth.Items.Count > 0) cbNewMonth.SelectedItem = dt.Month;
            if (cbNewDay.Items.Count > 0) cbNewDay.SelectedItem = dt.Day;
            
        }

        public void DisplayChineseDate()
        {
            if (Vaildate() == 0) return;
            dt = Convert.ToDateTime(cbNewYear.SelectedItem.ToString() + "-" + cbNewMonth.SelectedItem.ToString() + "-" + cbNewDay.SelectedItem.ToString());
            cc = new ChineseCalendar(dt);
            InitOldCalendar();

        }

        public int Vaildate()
        {
            if (cbNewYear.SelectedItem == null)
            {
                cbNewYear.Focus();
                return 0;
            }
            if (cbNewMonth.SelectedItem == null)
            {
                cbNewMonth.Focus();
                return 0;
            }
            if (cbNewDay.SelectedItem == null)
            {
                cbNewDay.Focus();
                return 0;
            }
            return 1;
        }

        private void cbYear0_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayChineseDate();
        }

        private void cbMonth0_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayChineseDate();
        }

        private void cbDay0_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayChineseDate();
        }

        private void nudHour_ValueChanged(object sender, EventArgs e)
        {
            if (cbHour.Items.Count == 0) return;
            int ihour = Convert.ToInt32(nudNewHour.Value);
            switch (ihour)
            {
                case 0:
                    cbHour.SelectedIndex = 0;
                    break;
                case 1:
                case 2:
                    cbHour.SelectedIndex = 1;
                    break;
                case 3:
                case 4:
                    cbHour.SelectedIndex = 2;
                    break;
                case 5:
                case 6:
                    cbHour.SelectedIndex = 3;
                    break;
                case 7:
                case 8:
                    cbHour.SelectedIndex = 4;
                    break;
                case 9:
                case 10:
                    cbHour.SelectedIndex = 5;
                    break;
                case 11:
                case 12:
                    cbHour.SelectedIndex = 6;
                    break;
                case 13:
                case 14:
                    cbHour.SelectedIndex = 7;
                    break;
                case 15:
                case 16:
                    cbHour.SelectedIndex = 8;
                    break;
                case 17:
                case 18:
                    cbHour.SelectedIndex = 9;
                    break;
                case 19:
                case 20:
                    cbHour.SelectedIndex = 10;
                    break;
                case 21:
                case 22:
                    cbHour.SelectedIndex = 11;
                    break;
                case 23:
                    cbHour.SelectedIndex = 0;
                    break;

                default:
                    break;
            }
        }

        private void cbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListControl lc = (ListControl)sender;

            User usr;
            usr = UserList.FindByName(lc.Text);

            this.tbName.Text = usr.Name;
            this.cbSex.SelectedIndex = usr.Sex;
            this.cbNewYear.Text = usr.Year.ToString();
            this.cbNewMonth.Text = usr.Month.ToString();
            this.cbNewDay.Text = usr.Day.ToString();
            this.nudNewHour.Value = usr.Hour;
            this.nudNewMinte.Value = usr.Minute;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            User usr;
            //usr = UserList.FindByName(this.tbName.Text);
            //if (UserList.IsExist(usr))
            //{
            //    MessageBox.Show("该用户已经存在");
            //}
            //else
            //{
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
            //}   

            this.LoadUserList();

            MessageBox.Show("保存成功.", "消息");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection cn = new SQLiteConnection( "Data Source=Test.db3;Pooling=true;FailIfMissing=false"))
            {
                //在打开数据库时，会判断数据库是否存在，如果不存在，则在当前目录下创建一个
                cn.Open();

                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = cn;

                    //建立表，如果表已经存在，则报错
                    cmd.CommandText = "CREATE TABLE [test] (id int, name nvarchar(20))";
                    cmd.ExecuteNonQuery();

                    //插入测试数据
                    for (int i = 2; i < 5; i++)
                    {
                        cmd.CommandText = string.Format("INSERT INTO [test] VALUES ({0}, '中文测试')", i);
                        cmd.ExecuteNonQuery();
                    }

                    for (int i = 5; i < 10; i++)
                    {
                        cmd.CommandText = string.Format("INSERT INTO [test] VALUES ({0}, 'English Test')", i);
                        cmd.ExecuteNonQuery();
                    }

                    //读取数据
                    cmd.CommandText = "SELECT * FROM [test]";
                    using (SQLiteDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            Console.WriteLine("第{0} 条：{1}", dr.GetValue(0), dr.GetString(1));
                        }
                    }
                }
            }

            User usr;
            if (this.cbUsers.Text == "") return;

            if (MessageBox.Show(this, "Do you want to delete the user ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                usr = UserList.FindByName(this.cbUsers.Text);
                if (usr != null) UserList.Delete(usr.Id);

                LoadUserList();
            }
        }

     
        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frmUserinfo = new frmUserInfo();
            frmUserinfo.ShowDialog();
        }

        private void modifyUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User usr = new User();

            frmUserInfo frmUserinfo = new frmUserInfo();
            frmUserinfo.LoadData(usr);
            frmUserinfo.ShowDialog();
        }

        private void delUserToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Do you want to delete the user ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                User usr;
                usr = UserList.FindByName(this.cbUsers.Text);
                if (usr != null) UserList.Delete(usr.Id);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("建设中");
        }

        private void btnGoForLiuYear_Click(object sender, EventArgs e)
        {
            int year;
            int yearEarth;
            int cnt;

            year = Convert.ToInt32(cbLiuYear.Text);
            yearEarth = cc.GetYearEarth(year);

        
            cnt = p.GetIDForLifeZone() - yearEarth;

            for (int i = 0; i < p.zone_list.Count; i++)
            {
                if (cnt >= 0)
                {
                    p.zone_list[i].dou = DouUtility.WiddershinsCountZone(p.zone_list[i].dou, cnt);
                }
                else
                {
                    p.zone_list[i].dou = DouUtility.DeasilCountZone(p.zone_list[i].dou,Math.Abs( cnt));
                }
            }

            p.LiuYearSky = cc.GetYearSky(Convert.ToInt32(this.cbLiuYear.SelectedItem));
            p.SetLiuYao();


            ucDouYear.LoadInfo(this.p);
            ucDouYear.DisplayBodyPlate();
            ucDouYear.DisplayAllLiuYao();
        }

       
    }
}
