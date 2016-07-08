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
    public partial class ucLifeDou : UserControl
    {
        public ucLifeDou()
        {
            InitializeComponent();
        }

        private Plate _plate = null;

        /// <summary>
        /// 加载命盘数据
        /// </summary>
        /// <param name="p"></param>
        public void LoadInfo(Plate p)
        {
            if (p != null) {
                _plate = p;
            }

            ////加载所有星曜
            //StarList.Load();

            ////起盘安星曜
            //p.CreatePlate(p.YearSky, p.YearEarth, p.month, p.day, p.hour, p.Sex, p.Type, p.first_name, "");

        }

        /// <summary>
        /// 显示命盘
        /// </summary>
        /// <param name="p"></param>
        public void DisplayBodyPlate()
        {

            this.douZone1.Reset();
            this.douZone2.Reset();
            this.douZone3.Reset();
            this.douZone4.Reset();
            this.douZone5.Reset();
            this.douZone6.Reset();
            this.douZone7.Reset();
            this.douZone8.Reset();
            this.douZone9.Reset();
            this.douZone10.Reset();
            this.douZone11.Reset();
            this.douZone12.Reset();

            // 显示盘中的信息，出生年月等等
            this.plateInfo1.SetSource(_plate);

            this.douZone1.DisplayByZone(_plate.zone_list[0]);
            this.douZone2.DisplayByZone(_plate.zone_list[1]);
            this.douZone3.DisplayByZone(_plate.zone_list[2]);
            this.douZone4.DisplayByZone(_plate.zone_list[3]);
            this.douZone5.DisplayByZone(_plate.zone_list[4]);
            this.douZone6.DisplayByZone(_plate.zone_list[5]);
            this.douZone7.DisplayByZone(_plate.zone_list[6]);
            this.douZone8.DisplayByZone(_plate.zone_list[7]);
            this.douZone9.DisplayByZone(_plate.zone_list[8]);
            this.douZone10.DisplayByZone(_plate.zone_list[9]);
            this.douZone11.DisplayByZone(_plate.zone_list[10]);
            this.douZone12.DisplayByZone(_plate.zone_list[11]);

        }

        /// <summary>
        /// 显示流星曜
        /// </summary>
        public void DisplayAllLiuYao()
        {
            this.douZone1.LoadLiuYao();
            this.douZone2.LoadLiuYao();
            this.douZone3.LoadLiuYao();
            this.douZone4.LoadLiuYao();
            this.douZone5.LoadLiuYao();
            this.douZone6.LoadLiuYao();
            this.douZone7.LoadLiuYao();
            this.douZone8.LoadLiuYao();
            this.douZone9.LoadLiuYao();
            this.douZone10.LoadLiuYao();
            this.douZone11.LoadLiuYao();
            this.douZone12.LoadLiuYao();

        }

        /// <summary>
        /// 清除所有流耀星
        /// </summary>
        public void ClearAllLiuYao()
        {
            this.douZone1.ResstLiuYao();
            this.douZone2.ResstLiuYao();
            this.douZone3.ResstLiuYao();
            this.douZone4.ResstLiuYao();
            this.douZone5.ResstLiuYao();
            this.douZone6.ResstLiuYao();
            this.douZone7.ResstLiuYao();
            this.douZone8.ResstLiuYao();
            this.douZone9.ResstLiuYao();
            this.douZone10.ResstLiuYao();
            this.douZone11.ResstLiuYao();
            this.douZone12.ResstLiuYao();

        }

        ///// <summary>
        ///// 设置全部流曜
        ///// </summary>
        ///// <param name="p"></param>
        //public void SetAllLiuYao(Plate p)
        //{
        //    ClearAllLiuYao();

        //    if (p != null)
        //    {
        //        this.douZone1.LoadLiuYao();
        //        this.douZone2.LoadLiuYao();
        //        this.douZone3.LoadLiuYao();
        //        this.douZone4.LoadLiuYao();
        //        this.douZone5.LoadLiuYao();
        //        this.douZone6.LoadLiuYao();
        //        this.douZone7.LoadLiuYao();
        //        this.douZone8.LoadLiuYao();
        //        this.douZone9.LoadLiuYao();
        //        this.douZone10.LoadLiuYao();
        //        this.douZone11.LoadLiuYao();
        //        this.douZone12.LoadLiuYao();
        //    }
        //}

       

    }
}
