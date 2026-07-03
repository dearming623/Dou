using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dou.Common.Util;

namespace DouNamespace
{
    public partial class frmDouApp : Form
    {
        private DateTime dt = DateTime.Now;
        private ChineseCalendar cc = null;
        private User usr = null;
        Plate p = new Plate();

        private string _filterGroup = "全部";
        private string _searchKeyword = string.Empty;
        private string _searchField = "姓名";

        public frmDouApp()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            StarList.Load();

            p = new Plate();
            ChineseCalendar cc = new ChineseCalendar();
            p.LiuYearSky = cc.GetYearSky(Convert.ToInt32(this.cbLiuYear.SelectedItem));

            p.YearSky = cbYesrSky.SelectedIndex;
            p.YearEarth = cbYearEarth.SelectedIndex;
            p.month = cbMonth.SelectedIndex;
            p.day = cbDay.SelectedIndex;
            p.hour = cbHour.SelectedIndex;
            p.Sex = cbSex.SelectedIndex;
            p.Type = 0;
            p.first_name = tbName.Text;
            p.CreatePlate();

            this.bodyDou.LoadInfo(p);
            this.bodyDou.DisplayBodyPlate();

            this.ucDouYear.LoadInfo(p);
            this.ucDouYear.DisplayBodyPlate();

            tabControl1.SelectedTab = tabPage1;
        }

        private void FrmDou_Load(object sender, EventArgs e)
        {
            StarList.Load();

            int i;

            cc = new ChineseCalendar(dt);

            for (i = 0; i < DouSource.SEX.Length; i++)
            {
                cbSex.Items.Add(DouSource.SEX[i]);
            }

            for (i = 0; i < 145; i++)
            {
                cbNewYear.Items.Add(1901 + i);
                cbLiuYear.Items.Add(1901 + i);
            }

            for (i = 0; i < 12; i++)
            {
                cbNewMonth.Items.Add(i + 1);
            }

            for (i = 0; i < 31; i++)
            {
                cbNewDay.Items.Add(i + 1);
            }

            for (i = 0; i < DouSource.SKY.Length; i++)
            {
                cbYesrSky.Items.Add(DouSource.SKY[i]);
            }

            for (i = 0; i < DouSource.EARTH.Length; i++)
            {
                cbYearEarth.Items.Add(DouSource.EARTH[i]);
            }

            for (i = 0; i < 12; i++)
            {
                cbMonth.Items.Add(i + 1);
            }

            for (i = 0; i < 31; i++)
            {
                cbDay.Items.Add(i + 1);
            }

            string temp = "";
            for (i = 0; i < DouSource.HourContent.Length; i++)
            {
                temp = DouSource.HourContent[i];
                cbHour.Items.Add(temp);
            }

            if (comboBox3.Items.Count > 0)
                comboBox3.SelectedIndex = 0;

            InitNewCalendar();
            InitOldCalendar();
            InitLiu();
            DisplayChineseDate();
            RefreshUserList();
        }

        private void RefreshUserList()
        {
            List<User> users = ApplyUserFilter(UserList.FindAll());
            dgvUserInfoList.DataSource = null;
            dgvUserInfoList.DataSource = users;
            LoadUserList();
        }

        private void EditUser(User user)
        {
            if (user == null) return;

            frmUserInfo frmUserinfo = new frmUserInfo();
            frmUserinfo.SetEditMode(user);
            if (frmUserinfo.ShowDialog() == DialogResult.OK)
            {
                RefreshUserList();
            }
        }

        private List<User> ApplyUserFilter(List<User> users)
        {
            IEnumerable<User> result = users;

            if (_filterGroup != "全部" && !string.IsNullOrEmpty(_filterGroup))
            {
                result = result.Where(u => u.Group == _filterGroup);
            }

            if (!string.IsNullOrEmpty(_searchKeyword))
            {
                string kw = _searchKeyword.Trim();
                if (_searchField == "姓名")
                {
                    result = result.Where(u =>
                        u.Name != null &&
                        u.Name.IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (_searchField == "命宫")
                {
                    result = result.Where(u =>
                        UserPlateHelper.GetPalaceEarth(u, 0).IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (_searchField == "夫妻")
                {
                    result = result.Where(u =>
                        UserPlateHelper.GetPalaceEarth(u, 2).IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }

            return result.ToList();
        }

        private User GetSelectedUser()
        {
            if (dgvUserInfoList.CurrentRow != null)
            {
                User gridUser = dgvUserInfoList.CurrentRow.DataBoundItem as User;
                if (gridUser != null) return gridUser;
            }

            if (cbUsers.Text != string.Empty)
            {
                return UserList.FindByName(cbUsers.Text);
            }

            return null;
        }

        private void ApplyUserToSidebar(User user)
        {
            if (user == null) return;

            tbName.Text = user.Name;
            if (user.Sex >= 0 && user.Sex < cbSex.Items.Count)
                cbSex.SelectedIndex = user.Sex;
            cbNewYear.Text = user.Year.ToString();
            cbNewMonth.Text = user.Month.ToString();
            cbNewDay.Text = user.Day.ToString();
            nudNewHour.Value = user.Hour;
            nudNewMinte.Value = user.Minute;
            DisplayChineseDate();
        }

        private void InitLiu()
        {
            if (this.cbLiuYear.Items.Count > 0) this.cbLiuYear.SelectedItem = dt.Year;
        }

        private void LoadUserList()
        {
            int i;
            List<User> users = UserList.FindAll();
            this.cbUsers.Items.Clear();

            for (i = 0; i < users.Count; i++)
            {
                this.cbUsers.Items.Add(users[i].Name);
            }
        }

        private void InitOldCalendar()
        {
            cbYesrSky.SelectedIndex = cc.YearSky;
            cbYearEarth.SelectedIndex = cc.YearEarth;

            if (cbMonth.Items.Count > 0) cbMonth.SelectedItem = cc.ChineseMonth;
            if (cbDay.Items.Count > 0) cbDay.SelectedItem = cc.ChineseDay;
            if (cbHour.Items.Count > 0) cbHour.SelectedIndex = 0;
        }

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
            cbHour.SelectedIndex = UserPlateHelper.ConvertHourToIndex(Convert.ToInt32(nudNewHour.Value));
        }

        private void cbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListControl lc = (ListControl)sender;

            User usr = UserList.FindByName(lc.Text);
            if (usr == null) return;

            ApplyUserToSidebar(usr);
        }

        private User BuildUserFromSidebar(User existing)
        {
            User usr = existing ?? new User();
            usr.Name = tbName.Text;
            usr.Sex = cbSex.SelectedIndex;
            usr.Year = Convert.ToInt32(cbNewYear.Text);
            usr.Month = Convert.ToInt32(cbNewMonth.Text);
            usr.Day = Convert.ToInt32(cbNewDay.Text);
            usr.Hour = Convert.ToInt32(nudNewHour.Value);
            usr.Minute = Convert.ToInt32(nudNewMinte.Value);
            return usr;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("请输入姓名", "无效", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            User selectedUser = GetSelectedUser();
            User sameNameUser = UserList.FindByName(tbName.Text);

            if (selectedUser != null)
            {
                User usr = BuildUserFromSidebar(selectedUser);
                if (sameNameUser != null && sameNameUser.Id != usr.Id)
                {
                    MessageBox.Show("该姓名已被其他用户使用", "无效", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                UserList.Update(usr);
            }
            else if (sameNameUser != null)
            {
                MessageBox.Show("该用户已经存在", "无效", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                User usr = BuildUserFromSidebar(null);
                UserList.Add(usr);
            }

            RefreshUserList();
            MessageBox.Show("保存成功.", "消息");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            User usr = GetSelectedUser();
            if (usr == null)
            {
                MessageBox.Show("请先选择要删除的用户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show(this, "确定要删除该用户吗？", "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                UserList.Delete(usr.Id);
                RefreshUserList();
            }
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frmUserinfo = new frmUserInfo();
            if (frmUserinfo.ShowDialog() == DialogResult.OK)
            {
                RefreshUserList();
            }
        }

        private void modifyUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User usr = GetSelectedUser();
            if (usr == null)
            {
                MessageBox.Show("请先选择要修改的用户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            EditUser(usr);
        }

        private void delUserToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            User usr = GetSelectedUser();
            if (usr == null)
            {
                MessageBox.Show("请先选择要删除的用户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show(this, "确定要删除该用户吗？", "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                UserList.Delete(usr.Id);
                RefreshUserList();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button3_Click(sender, e);
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User user = GetSelectedUser();
            if (user == null)
            {
                MessageBox.Show("请先选择要打开的会员", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ApplyUserToSidebar(user);
            btnGo_Click(sender, e);
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

        private void button3_Click(object sender, EventArgs e)
        {
            _searchField = comboBox3.SelectedItem != null ? comboBox3.SelectedItem.ToString() : "姓名";
            _searchKeyword = textBox1.Text;
            RefreshUserList();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;
            _filterGroup = e.Node.Text;
            RefreshUserList();
        }

        private void dgvUserInfoList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            User user = dgvUserInfoList.Rows[e.RowIndex].DataBoundItem as User;
            if (user == null) return;

            string columnName = dgvUserInfoList.Columns[e.ColumnIndex].Name;
            if (columnName == "sex")
            {
                if (user.Sex >= 0 && user.Sex < DouSource.SEX.Length)
                    e.Value = DouSource.SEX[user.Sex];
                e.FormattingApplied = true;
            }
            else if (columnName == "Column1")
            {
                e.Value = UserPlateHelper.GetSolarBirthdayText(user);
                e.FormattingApplied = true;
            }
            else if (columnName == "Column2")
            {
                e.Value = UserPlateHelper.GetLunarBirthdayText(user);
                e.FormattingApplied = true;
            }
            else if (columnName == "Column3")
            {
                e.Value = UserPlateHelper.GetPalaceEarth(user, 0);
                e.FormattingApplied = true;
            }
            else if (columnName == "Column4")
            {
                e.Value = UserPlateHelper.GetPalaceEarth(user, 2);
                e.FormattingApplied = true;
            }
            else if (columnName == "ColumnGroup")
            {
                e.Value = string.IsNullOrEmpty(user.Group) ? "未分组" : user.Group;
                e.FormattingApplied = true;
            }
        }

        private void dgvUserInfoList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvUserInfoList.Columns[e.ColumnIndex].Name != "Column5") return;

            User user = dgvUserInfoList.Rows[e.RowIndex].DataBoundItem as User;
            EditUser(user);
        }

        private void dgvUserInfoList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvUserInfoList.Columns[e.ColumnIndex].Name == "Column5") return;

            User user = dgvUserInfoList.Rows[e.RowIndex].DataBoundItem as User;
            EditUser(user);
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
                    p.zone_list[i].dou = DouUtility.DeasilCountZone(p.zone_list[i].dou, Math.Abs(cnt));
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
