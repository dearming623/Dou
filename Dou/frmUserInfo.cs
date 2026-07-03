using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DouNamespace
{
    public partial class frmUserInfo : Form
    {
        private bool _isEdit = false;
        private int _editId = 0;
        private DateTime _birthDate = DateTime.Now;
        private ChineseCalendar _calendar = null;

        public static readonly string[] UserGroups = new string[] { "亲戚", "好友", "同事", "关注" };

        public frmUserInfo()
        {
            InitializeComponent();
            InitFormControls();
        }

        private void InitFormControls()
        {
            int i;

            for (i = 0; i < DouSource.SEX.Length; i++)
            {
                cbSex.Items.Add(DouSource.SEX[i]);
            }

            for (i = 0; i < 145; i++)
            {
                cbNewYear.Items.Add(1901 + i);
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

            for (i = 0; i < DouSource.HourContent.Length; i++)
            {
                cbHour.Items.Add(DouSource.HourContent[i]);
            }

            comboBox1.Items.Clear();
            for (i = 0; i < UserGroups.Length; i++)
            {
                comboBox1.Items.Add(UserGroups[i]);
            }

            cbNewYear.SelectedIndexChanged += SolarDateChanged;
            cbNewMonth.SelectedIndexChanged += SolarDateChanged;
            cbNewDay.SelectedIndexChanged += SolarDateChanged;
            nudNewHour.ValueChanged += nudNewHour_ValueChanged;

            if (!_isEdit)
            {
                InitDefaultBirthDate();
            }

            cbYesrSky.Enabled = false;
            cbYearEarth.Enabled = false;
            cbMonth.Enabled = false;
            cbDay.Enabled = false;
            cbHour.Enabled = false;
        }

        private void InitDefaultBirthDate()
        {
            DateTime now = DateTime.Now;
            cbSex.SelectedIndex = 0;
            cbNewYear.SelectedItem = now.Year;
            cbNewMonth.SelectedItem = now.Month;
            cbNewDay.SelectedItem = now.Day;
            nudNewHour.Value = now.Hour;
            nudNewMinte.Value = now.Minute;
            DisplayChineseDate();
        }

        private void SolarDateChanged(object sender, EventArgs e)
        {
            DisplayChineseDate();
        }

        private void nudNewHour_ValueChanged(object sender, EventArgs e)
        {
            if (cbHour.Items.Count == 0) return;
            cbHour.SelectedIndex = UserPlateHelper.ConvertHourToIndex(Convert.ToInt32(nudNewHour.Value));
        }

        private void DisplayChineseDate()
        {
            if (cbNewYear.SelectedItem == null || cbNewMonth.SelectedItem == null || cbNewDay.SelectedItem == null)
                return;

            try
            {
                _birthDate = Convert.ToDateTime(
                    cbNewYear.SelectedItem + "-" + cbNewMonth.SelectedItem + "-" + cbNewDay.SelectedItem);
                _calendar = new ChineseCalendar(_birthDate);

                if (cbYesrSky.Items.Count > 0) cbYesrSky.SelectedIndex = _calendar.YearSky;
                if (cbYearEarth.Items.Count > 0) cbYearEarth.SelectedIndex = _calendar.YearEarth;
                if (cbMonth.Items.Count > 0) cbMonth.SelectedItem = _calendar.ChineseMonth;
                if (cbDay.Items.Count > 0) cbDay.SelectedItem = _calendar.ChineseDay;
                nudNewHour_ValueChanged(null, EventArgs.Empty);
            }
            catch (Exception)
            {
            }
        }

        public void SetEditMode(User usr)
        {
            _isEdit = true;
            _editId = usr.Id;
            LoadData(usr);
            this.Text = "修改用户";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Vaildation() == false) return;

            User usr = BuildUserFromForm();
            if (_isEdit)
            {
                usr.Id = _editId;
                User existing = UserList.FindByName(usr.Name);
                if (existing != null && existing.Id != usr.Id)
                {
                    MessageBox.Show("该姓名已被其他用户使用", "无效", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                UserList.Update(usr);
            }
            else
            {
                User existing = UserList.FindByName(usr.Name);
                if (existing != null)
                {
                    MessageBox.Show("该用户已经存在", "无效", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                UserList.Add(usr);
            }

            MessageBox.Show("保存成功.", "消息");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private User BuildUserFromForm()
        {
            User usr = new User();
            usr.Name = tbName.Text;
            usr.Sex = cbSex.SelectedIndex;
            usr.Year = Convert.ToInt32(cbNewYear.Text);
            usr.Month = Convert.ToInt32(cbNewMonth.Text);
            usr.Day = Convert.ToInt32(cbNewDay.Text);
            usr.Hour = Convert.ToInt32(nudNewHour.Value);
            usr.Minute = Convert.ToInt32(nudNewMinte.Value);
            usr.Phone = textBox1.Text;
            usr.Mobile = textBox2.Text;
            usr.Email = textBox3.Text;
            usr.QQ = textBox4.Text;
            usr.Notes = textBox5.Text;
            usr.Group = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : string.Empty;
            return usr;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool Vaildation()
        {
            if (tbName.Text == "")
            {
                MessageBox.Show("请输入姓名", "无效", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (cbNewYear.SelectedItem == null || cbNewMonth.SelectedItem == null || cbNewDay.SelectedItem == null)
            {
                MessageBox.Show("请选择完整的公历生日", "无效", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public void LoadData(User usr)
        {
            if (usr == null) return;

            tbName.Text = usr.Name;
            if (usr.Sex >= 0 && usr.Sex < cbSex.Items.Count)
                cbSex.SelectedIndex = usr.Sex;
            cbNewYear.Text = usr.Year.ToString();
            cbNewMonth.Text = usr.Month.ToString();
            cbNewDay.Text = usr.Day.ToString();
            nudNewHour.Value = Math.Max(0, Math.Min(23, usr.Hour));
            nudNewMinte.Value = Math.Max(0, Math.Min(59, usr.Minute));
            DisplayChineseDate();

            textBox1.Text = usr.Phone ?? string.Empty;
            textBox2.Text = usr.Mobile ?? string.Empty;
            textBox3.Text = usr.Email ?? string.Empty;
            textBox4.Text = usr.QQ ?? string.Empty;
            textBox5.Text = usr.Notes ?? string.Empty;

            comboBox1.SelectedItem = null;
            if (!string.IsNullOrEmpty(usr.Group))
            {
                for (int i = 0; i < comboBox1.Items.Count; i++)
                {
                    if (comboBox1.Items[i].ToString() == usr.Group)
                    {
                        comboBox1.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
    }
}
