using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DouNamespace
{
    public partial class ucPlateInfo : UserControl
    {
        public ucPlateInfo()
        {
            InitializeComponent();
        }

        public void SetSource(Plate plate)
        {
            // 姓名
            lblName.Text = string.Format("{0} {1}",plate.last_name,plate.first_name);
            
            lblChsBrithday.Text = "";

            // 盘的类型，天盘，地盘，人盘
            lblType.Text = DouSource.PlateType[plate.Type];

            // 天干
            lblSky.Text = DouSource.SKY[plate.YearSky];

            // 地支
            lblEarth.Text = DouSource.EARTH[plate.YearEarth];

            // 五行
            lblFiveX.Text = DouSource.FiveX[plate.Five_X];

            //出生月
            int month = plate.month + 1;
            lblMonth.Text =string.Format("{0}",month);

            //出生日
            int day = plate.day + 1;
            lblDay.Text = string.Format("{0}", day);

            //时辰
            lblHour.Text = DouSource.EARTH[plate.hour];

            //阴阳
            lblYinyang.Text = DouSource.YinYang[plate.YinYang] ;

            //性别
            lblSex.Text = DouSource.SEX[plate.Sex];

            //身主
            lblBodyStar.Text = StarList.GetStarById(plate.body_star).Name;

            // 命主
            lblLifeStar.Text = StarList.GetStarById(plate.life_star).Name;

        }
    }
}
