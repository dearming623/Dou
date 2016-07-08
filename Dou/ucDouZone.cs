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
    public partial class ucDouZone : UserControl
    {
        private Zone _zone;

        public ucDouZone()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 显示宫位所有星曜
        /// </summary>
        /// <param name="zone"></param>
        public void DisplayByZone(Zone zone)
        {
            this._zone = zone;

            // 显示是否身宫
            lblBody.Text = DouSource.BODY[zone.body];

            // 显示十二长生
            Star longstar = zone.stars.Find(
            delegate(Star s)
            {
                return s.position == 8;
            }
            );
            lblCycle.Text = longstar.Name;

            //显示宫的名
            lblDou.Text = DouSource.ZoneName[zone.dou];

            //显示主星曜
            List<Star> mainStarList = zone.stars.FindAll(
                delegate(Star s)
                {
                    return s.position == 1;
                }
            );

            for (int i = 0; i < mainStarList.Count; i++)
            {
                // 主星
                foreach (Control ctrl in Controls)
                {
                    string name = string.Format("{0}{1}", "lblMStar", i);
                    if (ctrl.Name == name)
                    {
                        // ctrl.GetType().GetProperty("Text").GetValue(ctrl, null).ToString();
                        ctrl.GetType().GetProperty("Text").SetValue(ctrl, mainStarList[i].Name, null);
                    }
                }

                // 四化星
                if (mainStarList[i].xstar != null)
                {
                    foreach (Control ctrl in Controls)
                    {
                        string name = string.Format("{0}{1}", "lblMStarX", i);
                        if (ctrl.Name == name)
                        {
                            ctrl.GetType().GetProperty("Text").SetValue(ctrl, mainStarList[i].xstar.Name.Substring(1, 1), null);
                            ctrl.GetType().GetProperty("BackColor").SetValue(ctrl, Color.GreenYellow, null);
                        }

                    }
                }

                // 四流化星
                if (mainStarList[i].lxstar != null)
                {
                    foreach (Control ctrl in Controls)
                    {
                        string name = string.Format("{0}{1}", "lblMlStarX", i);
                        if (ctrl.Name == name)
                        {
                            ctrl.GetType().GetProperty("Text").SetValue(ctrl, mainStarList[i].lxstar.Name.Substring(2, 1), null);
                            ctrl.GetType().GetProperty("BackColor").SetValue(ctrl, Color.SkyBlue, null);
                        }

                    }
                }

                // 星的亮度状态
                if (mainStarList[i].Level != 0)
                {
                    foreach (Control ctrl in Controls)
                    {
                        string level = string.Format("{0}{1}", "lblMStarLevl", i);
                        if (ctrl.Name == level)
                        {
                            ctrl.GetType().GetProperty("Text").SetValue(ctrl, DouSource.LEVEL[mainStarList[i].Level], null);
                            ctrl.GetType().GetProperty("BackColor").SetValue(ctrl, Color.GreenYellow, null);
                        }
                    }
                }
            }

            //显示辅助星曜
            List<Star> helpStarList = zone.stars.FindAll(
               delegate(Star s)
               {
                   return s.position == 2;
               }
           );
            Star tempStar;
            for (int i = 0; i < helpStarList.Count; i++)
            {
                tempStar = helpStarList[i];
                foreach (Control ctrl in Controls)
                {
                    string name = string.Format("{0}{1}", "lblHelp", i + 1);
                    if (ctrl.Name == name)
                    {
                        ctrl.GetType().GetProperty("Text").SetValue(ctrl, helpStarList[i].Name, null);
                        if (tempStar.ID == 23 || tempStar.ID == 24 || tempStar.ID == 25 || tempStar.ID == 26)
                        {
                            // ctrl.GetType().GetProperty("BackColor").SetValue(ctrl, Color.Firebrick, null);
                            ctrl.GetType().GetProperty("ForeColor").SetValue(ctrl, Color.Red, null);
                        }
                    }
                }
                if (helpStarList[i].xstar != null)
                {
                    foreach (Control ctrl in Controls)
                    {
                        string name = string.Format("{0}{1}", "lblHelpX", i + 1);
                        if (ctrl.Name == name)
                        {
                            ctrl.GetType().GetProperty("Text").SetValue(ctrl, helpStarList[i].xstar.Name.Substring(1, 1), null);
                            ctrl.GetType().GetProperty("BackColor").SetValue(ctrl, Color.GreenYellow, null);
                        }
                    }
                }

                // 星的亮度状态
                if (helpStarList[i].Level != 0)
                {
                    foreach (Control ctrl in Controls)
                    {
                        string level = string.Format("{0}{1}", "lblHelpLevl", i);
                        if (ctrl.Name == level)
                        {

                            //ctrl.GetType().GetProperty("BorderStyle").SetValue(ctrl, BorderStyle.FixedSingle, null);
                            ctrl.GetType().GetProperty("Text").SetValue(ctrl, DouSource.LEVEL[helpStarList[i].Level], null);
                            ctrl.GetType().GetProperty("BackColor").SetValue(ctrl, Color.GreenYellow, null);
                        }
                    }
                }
            }


            //显示杂星曜
            List<Star> topStarList = zone.stars.FindAll(
               delegate(Star s)
               {
                   return s.position == 4;
               }
           );

            for (int i = 0; i < topStarList.Count; i++)
            {
                foreach (Control ctrl in Controls)
                {
                    string name = string.Format("{0}{1}", "lblTop", i);
                    if (ctrl.Name == name)
                    {
                        //ctrl.GetType().GetProperty("BorderStyle").SetValue(ctrl, BorderStyle.FixedSingle, null);
                        ctrl.GetType().GetProperty("Text").SetValue(ctrl, topStarList[i].Name, null);
                    }
                }
            }

            //显示十二星曜
            List<Star> dwnStarList = zone.stars.FindAll(
              delegate(Star s)
              {
                  return s.position == 5;
              }
          );


            for (int i = 0; i < dwnStarList.Count; i++)
            {
                foreach (Control ctrl in Controls)
                {
                    string name = string.Format("{0}{1}", "lblDwn", i);
                    if (ctrl.Name == name)
                    {
                        ctrl.GetType().GetProperty("Text").SetValue(ctrl, dwnStarList[i].Name, null);
                    }
                }
            }


            // 显示流年星lblLiu0
            LoadLiuYao();


            // 大限
            int start, end;
            start = zone.ten_year;
            end = start + 9;
            lblTenYear.Text = string.Format("{0} - {1}", start, end);

            //天干
            lblSky.Text = DouSource.SKY[zone.sky];

            //地支
            lblEarth.Text = DouSource.EARTH[zone.id];

        }

        /// <summary>
        /// 显示流年星lblLiu0
        /// </summary>
        public void LoadLiuYao()
        {
            Star tempStar;

            List<Star> liuStarList = _zone.stars.FindAll(
              delegate(Star s)
              {
                  return s.position == 6;
              }
          );
            for (int i = 0; i < liuStarList.Count; i++)
            {
                tempStar = liuStarList[i];
                foreach (Control ctrl in Controls)
                {
                    string name = string.Format("{0}{1}", "lblLiu", i);
                    if (ctrl.Name == name)
                    {
                        ctrl.GetType().GetProperty("Text").SetValue(ctrl, liuStarList[i].Name, null);
                        ctrl.GetType().GetProperty("BackColor").SetValue(ctrl, Color.SkyBlue, null);
                        //ctrl.GetType().GetProperty("ForeColor").SetValue(ctrl, Color.CornflowerBlue, null);


                        if (tempStar.ID == 125 || tempStar.ID == 126)
                        {
                            // ctrl.GetType().GetProperty("BackColor").SetValue(ctrl, Color.Firebrick, null);
                            ctrl.GetType().GetProperty("ForeColor").SetValue(ctrl, Color.Red, null);
                        }

                    }
                }
            }
        }

        public void Reset()
        {
            foreach (Control ctrl in Controls)
            {
                ctrl.GetType().GetProperty("Text").SetValue(ctrl, "", null);
                ctrl.GetType().GetProperty("BackColor").SetValue(ctrl, Color.Transparent, null);
            }
        }

        public void ResstLiuYao()
        {
            foreach (Control ctrl in Controls)
            {
                if (ctrl.Name.IndexOf("lblLiu") >= 0)
                {
                    ctrl.GetType().GetProperty("Text").SetValue(ctrl, "", null);
                    ctrl.GetType().GetProperty("BackColor").SetValue(ctrl, Color.Transparent, null);
                }
            }
        }




    }
}
