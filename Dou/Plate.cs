using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace DouNamespace
{
    /// <summary>
    /// 
    /// Copyright (C) 2009-2010 By DengYiMing
    /// </summary>
    public class Plate
    {
        // public string plate_id;
        public string first_name;
        public string last_name;

        public int year;
        public int month;
        public int day;
        public int hour;
        /// <summary>
        /// 五行
        /// </summary>
        public int Five_X;

        /// <summary>
        /// 0-女,1-男
        /// </summary>
        public int Sex;

        /// <summary>
        ///  0-阴 1-阳
        /// </summary>
        public int YinYang;

        /// <summary>
        /// 年干
        /// </summary>
        public int YearSky;

        /// <summary>
        /// 年支
        /// </summary>
        public int YearEarth;

        /// <summary>
        /// 类型（天盘,地盘,人盘）
        /// </summary>
        public int Type;

        /// <summary>
        /// 命主
        /// </summary>
        public int life_star;

        /// <summary>
        /// 身主
        /// </summary>
        public int body_star;

        /// <summary>
        ///  所有12个宫位
        /// </summary>
        public List<Zone> zone_list = new List<Zone>();

        ///// <summary>
        ///// 流年
        ///// </summary>
        //public int LiuYear;

        /// <summary>
        /// 流年-年干
        /// </summary>
        public int LiuYearSky;

        /// <summary>
        /// 0 - 阳男阴女
        /// 1 - 阴男阳女
        /// </summary>
        public int IsDeasil = 0;

        /// <summary>
        /// 获取对应位置的宫
        /// </summary>
        /// <param name="id">宫ID</param>
        /// <returns></returns>
        private Zone GetZoneById(int id)
        {
            Zone result = null;
            foreach (Zone item in zone_list)
            {
                if (item.id == id)
                {
                    result = item;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        ///  获取命宫
        /// </summary>
        /// <returns></returns>
        private Zone GetZoneOfLife()
        {
            int life = 0;
            life = GetLifeInZone();

            return GetZoneById(life);
        }

        /// <summary>
        /// 获取命宫所在ID
        /// </summary>
        /// <returns></returns>
        private int GetLifeInZone()
        {
            int life = 0;
            foreach (Zone item in zone_list)
            {
                if (item.IsLife)
                {
                    life = item.id;
                    break;
                }
            }

            return life;
        }

        /// <summary>
        /// 获取身宫所在ID
        /// </summary>
        /// <returns></returns>
        private int GetBodyInZone()
        {
            int body = 0;
            foreach (Zone item in zone_list)
            {
                if (item.IsBody)
                {
                    body = item.id;
                    break;
                }
            }
            return body;
        }

        /// <summary>
        ///  用星曜代号获取所在的宫号
        /// </summary>
        /// <param name="starId"></param>
        /// <returns></returns>
        private int GetZoneByStar(int starId)
        {
            int result = -1;
            foreach (Zone z in zone_list)
            {
                foreach (Star s in z.stars)
                {
                    if (s == null) break;
                    if (s.ID == starId)
                    {
                        result = z.id;
                        break;
                    }
                }
                if (result != -1) break;
            }
            return result;
        }

        /// <summary>
        /// 用星曜获取所在的宫号
        /// </summary>
        /// <param name="s">星曜</param>
        /// <returns></returns>
        private int GetZoneByStar(Star s)
        {
            int zoneNo = 0;
            foreach (Zone x in zone_list)
            {
                foreach (Star y in x.stars)
                {
                    if (y.ID == s.ID) zoneNo = x.id;
                    break;
                }
            }
            return zoneNo;
        }

        /// <summary>
        /// 获取命盘中的星曜
        /// </summary>
        /// <param name="starid"></param>
        /// <returns></returns>
        private Star GetStarInZone(int starid)
        {
            int k = -1;
            Star result = null;
            foreach (Zone z in zone_list)
            {
                foreach (Star s in z.stars)
                {
                    if (s.ID == starid)
                    {
                        k = s.ID;
                        result = s;
                        break;
                    }
                }
                if (k != -1) break;
            }
            return result;
        }

        /// <summary>
        /// 安星曜到指定宫中
        /// </summary>
        /// <param name="zoneNo">宫的号</param>
        /// <param name="starNo">星曜号</param>
        private void SetStarToZone(int zoneNo, int starNo)
        {
            Star s = new Star();
            s = StarList.GetStarById(starNo);
            GetZoneById(zoneNo).stars.Add(s);
        }

        /// <summary>
        /// 删除星耀
        /// </summary>
        /// <param name="zoneNo">宫的号</param>
        /// <param name="starNo">星曜号</param>
        private void RemoveStar(int starNo)
        {
            Star s = new Star();
            s = StarList.GetStarById(starNo);

            for (int i = 0; i < zone_list.Count; i++)
            {
                zone_list[i].stars.Remove(s);
            }

        }

        public void CreatePlate()
        {
            // fill 12 zones in plate 
            for (int i = 0; i < 12; i++)
            {
                Zone plateZone = new Zone();
                plateZone.id = i;
                zone_list.Add(plateZone);
            }

            // set all stars
            SetLifeDou();
            SetBodyDou();
            SetAllDou();
            SetAllSky();
            Set5Xing();
            SetYinYang();
            SetIsDeasil();//
            SetAllTenYear();
            Set_s1();
            Set_s7();
            SetAllMainStar();
            SetEffectiveStar();
            Set4Hua();
            Set_s19_s20();
            Set_s21_s23_s24();
            Set_s25();
            Set_s26();
            Set_s49();
            Set_s50();
            Set_s61();
            Set_s115_s116();
            Set_s117_s118();
            Set_s22();
            Set_s62();
            Set_s38();
            Set_s37();
            Set_s39();
            Set_s40();
            Set_s53();
            Set_s54();
            Set_s65();
            Set_s131();
            Set_s55();
            Set_s56();
            Set_s64();
            Set_s66();
            Set_s63();
            Set_s44();
            Set_s45();
            Set_s46();
            Set_s43();
            Set_s51();
            Set_s52();
            Set_s36();
            Set_s35();
            Set_s57();
            Set_s58();
            Set_s59();
            Set_s60();
            Set_s33();
            Set_s34();
            Set_s41();
            Set_s42();
            Set_s47();
            Set_s48();
            Set_life_star();
            Set_body_star();
            Set_longlife_12();
            Set_grandage_12();
            Set_general_star_12();
            Set_doctor_12();
            SetStarLevel();
            //SetLiuChangLiuQu();
            //SetLiuKuiLiuYue();
            //SetLiuYangLiutuoLiulu();
        }

        /// <summary>
        /// 起盘
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="sex"></param>
        /// <param name="type"></param>
        /// <param name="first"></param>
        /// <param name="last"></param>
        //public void CreatePlate(int year, int month, int day, int hour, int sex, int type, string first, string last)
        public void CreatePlate(int sky, int earth, int month, int day, int hour, int sex, int type, string first, string last)
        {

            // create new plate 
            // this.year = year;
            this.month = month;
            this.day = day;
            this.hour = hour;
            this.Five_X = 0;
            this.Sex = sex;
            this.YinYang = 0;
            this.YearSky = sky;
            this.YearEarth = earth;
            this.Type = type;
            this.life_star = 0;
            this.body_star = 0;
            this.first_name = first;
            this.last_name = last;

            this.CreatePlate();


        }


        /// <summary>
        ///  定 阳男阴女/阴男阳女
        /// </summary>
        private void SetIsDeasil()
        {
            if (YinYang == 1 && Sex == 1 || YinYang == 0 && Sex == 0) // 阳男阴女
            {
                IsDeasil = 1;
            }
            else//阴男阳女
            {
                IsDeasil = 0;
            }
        }

        /// <summary>
        /// step - 1 安命宫
        /// </summary>
        private void SetLifeDou()
        {
            int i, j;
            i = DouUtility.DeasilCountZone(2, this.month);
            j = DouUtility.WiddershinsCountZone(i, this.hour);
            GetZoneById(j).dou = 0;
            GetZoneById(j).IsLife = true;
        }

        /// <summary>
        ///  step - 1安身宫
        /// </summary>
        private void SetBodyDou()
        {
            int i, j;
            i = DouUtility.DeasilCountZone(2, this.month);
            j = DouUtility.DeasilCountZone(i, this.hour);
            GetZoneById(j).body = 1;
            GetZoneById(j).IsBody = true;
        }

        /// <summary>
        ///  定十二宫
        /// </summary>
        private void SetAllDou()
        {
            int life, i, j;

            life = GetLifeInZone();
            i = life;
            for (j = 0; j < 12; j++)
            {
                GetZoneById(i).dou = j;
                i = DouUtility.WiddershinsCountZone(i, 1);
            }
        }

        /// <summary>
        ///  定十二天干
        ///  甲己之年丙遁寅 乙庚之岁戊先行 丙辛之年起庚寅 丁壬起壬戊癸甲 遁干化气必逢生
        /// </summary>
        private void SetAllSky()
        {
            int startIdx, i, j;
            startIdx = 0;

            switch (YearSky)
            {
                case 0:
                case 5:
                    startIdx = 2;
                    break;
                case 1:
                case 6:
                    startIdx = 4;
                    break;
                case 2:
                case 7:
                    startIdx = 6;
                    break;
                case 3:
                case 8:
                    startIdx = 8;
                    break;
                case 4:
                case 9:
                    startIdx = 0;
                    break;
                default:
                    break;
            }

            j = 2;
            for (i = 0; i < 12; i++)
            {
                GetZoneById(j).sky = (startIdx + i) % 10;
                j = DouUtility.DeasilCountZone(j, 1);
            }
        }

        /// <summary>
        /// 定五行
        /// </summary>
        private void Set5Xing()
        {
            int sky, earth;
            sky = GetZoneOfLife().sky;
            earth = GetZoneOfLife().id;

            int i = 0, j = 0, k = 0, key = 0;
            if (sky == 0 || sky == 1) { i = 4; j = 2; k = 6; }
            if (sky == 2 || sky == 3) { i = 2; j = 6; k = 5; }
            if (sky == 4 || sky == 5) { i = 6; j = 5; k = 3; }
            if (sky == 6 || sky == 7) { i = 5; j = 3; k = 4; }
            if (sky == 8 || sky == 9) { i = 3; j = 4; k = 2; }

            if (earth == 0 || earth == 1 || earth == 6 || earth == 7) key = i;
            if (earth == 2 || earth == 3 || earth == 8 || earth == 9) key = j;
            if (earth == 4 || earth == 5 || earth == 10 || earth == 11) key = k;

            this.Five_X = key;
        }

        /// <summary>
        /// 定阴阳
        /// </summary>
        private void SetYinYang()
        {
            this.YinYang = (this.YearSky + 1) % 2;
        }

        /// <summary>
        ///  安大限
        /// </summary>
        private void SetAllTenYear()
        {
            int five_x, ten_year;
            int i, j;

            ten_year = 0;

            five_x = this.Five_X;

            //if (sex == 1 && yinyang == 1) IsDeasil = 1;
            //if (sex == 1 && yinyang == 0) IsDeasil = -1;
            //if (sex == 0 && yinyang == 1) IsDeasil = -1;
            //if (sex == 0 && yinyang == 0) IsDeasil = 1;

            j = GetLifeInZone();
            for (i = 0; i < 12; i++)
            {
                ten_year = five_x + i * 10;
                GetZoneById(j).ten_year = ten_year;
                if (IsDeasil == 1)
                {
                    j = DouUtility.DeasilCountZone(j, 1);
                }
                else
                {
                    j = DouUtility.WiddershinsCountZone(j, 1);
                }
            }
        }

        /// <summary>
        ///  紫微
        /// </summary>
        private void Set_s1()
        {
            int @v1, @v2, @v3 = 0, @v4;
            int day;
            day = this.day + 1;
            @v1 = day / this.Five_X;
            @v2 = day % this.Five_X;

            if (@v2 == 5 || @v2 == 0) @v3 = 3;

            if (@v2 == 4)
            {
                if (Five_X == 6) @v3 = 6;
                if (Five_X == 5) @v3 = 3;
            }
            if (@v2 == 3)
            {
                if (Five_X == 6) @v3 = 1;
                if (Five_X == 5) @v3 = 6;
                if (Five_X == 4) @v3 = 3;
            }
            if (@v2 == 2)
            {
                if (Five_X == 6) @v3 = 8;
                if (Five_X == 5) @v3 = 1;
                if (Five_X == 4) @v3 = 6;
                if (Five_X == 3) @v3 = 3;
            }
            if (@v2 == 1)
            {
                if (Five_X == 6) @v3 = 11;
                if (Five_X == 5) @v3 = 8;
                if (Five_X == 4) @v3 = 1;
                if (Five_X == 3) @v3 = 6;
                if (Five_X == 2) @v3 = 3;
            }

            @v4 = @v3 + @v1 - 1;
            if (@v4 > 12) @v4 = @v4 - 12;

            @v4 = @v4 - 1;

            SetStarToZone(@v4, 1);
        }

        /// <summary>
        /// 安天府
        /// </summary>
        private void Set_s7()
        {
            int ziwei, targetZone;
            targetZone = 0;
            ziwei = GetZoneByStar(1);

            if (ziwei == 0) targetZone = 4;
            if (ziwei == 1) targetZone = 3;
            if (ziwei == 2) targetZone = 2;
            if (ziwei == 3) targetZone = 1;
            if (ziwei == 4) targetZone = 0;
            if (ziwei == 5) targetZone = 11;
            if (ziwei == 6) targetZone = 10;
            if (ziwei == 7) targetZone = 9;
            if (ziwei == 8) targetZone = 8;
            if (ziwei == 9) targetZone = 7;
            if (ziwei == 10) targetZone = 6;
            if (ziwei == 11) targetZone = 5;

            SetStarToZone(targetZone, 7);
        }

        /// <summary>
        /// 安十四正曜(天机,太阳,武曲,天同,廉贞,太阴,贪狼,巨门,天相,七杀,破军)
        /// </summary>
        private void SetAllMainStar()
        {
            int ziwei, tianfu, targetZone;
            targetZone = 0;
            ziwei = GetZoneByStar(1);
            tianfu = GetZoneByStar(7);

            //  天机
            targetZone = DouUtility.WiddershinsCountZone(ziwei, 1);
            SetStarToZone(targetZone, 2);

            //  太阳
            targetZone = DouUtility.WiddershinsCountZone(targetZone, 2);
            SetStarToZone(targetZone, 3);

            // 武曲
            targetZone = DouUtility.WiddershinsCountZone(targetZone, 1);
            SetStarToZone(targetZone, 4);

            // 天同
            targetZone = DouUtility.WiddershinsCountZone(targetZone, 1);
            SetStarToZone(targetZone, 5);

            // 廉贞
            targetZone = DouUtility.WiddershinsCountZone(targetZone, 3);
            SetStarToZone(targetZone, 6);

            // 太阴
            targetZone = DouUtility.DeasilCountZone(tianfu, 1);
            SetStarToZone(targetZone, 8);

            // 贪狼
            targetZone = DouUtility.DeasilCountZone(targetZone, 1);
            SetStarToZone(targetZone, 9);

            // 巨门
            targetZone = DouUtility.DeasilCountZone(targetZone, 1);
            SetStarToZone(targetZone, 10);

            // 天相
            targetZone = DouUtility.DeasilCountZone(targetZone, 1);
            SetStarToZone(targetZone, 11);

            // 天梁
            targetZone = DouUtility.DeasilCountZone(targetZone, 1);
            SetStarToZone(targetZone, 12);

            // 七杀
            targetZone = DouUtility.DeasilCountZone(targetZone, 1);
            SetStarToZone(targetZone, 13);

            // 破军
            targetZone = DouUtility.DeasilCountZone(targetZone, 4);
            SetStarToZone(targetZone, 14);

        }

        /// <summary>
        /// step - 9 安辅弼昌曲空劫诀 (左辅,右弼,文曲,文昌,地劫,地空)
        /// </summary>
        private void SetEffectiveStar()
        {
            int target = 0;

            //	左辅
            target = DouUtility.DeasilCountZone(4, this.month); // 辰宫上顺正寻左辅
            SetStarToZone(target, 17);

            //	右弼
            target = DouUtility.WiddershinsCountZone(10, this.month); // 戌宫上逆正右弼当
            SetStarToZone(target, 18);

            //	文曲
            target = DouUtility.DeasilCountZone(4, this.hour); // 辰宫上顺时文曲位
            SetStarToZone(target, 16);

            //	文昌
            target = DouUtility.WiddershinsCountZone(10, this.hour); // 戌宫上逆时觅文昌
            SetStarToZone(target, 15);

            //  地劫
            target = DouUtility.DeasilCountZone(11, this.hour); // 亥宫上子时顺安劫
            SetStarToZone(target, 32);

            //	地空
            target = DouUtility.WiddershinsCountZone(11, this.hour); // 亥宫逆回便是地空亡
            SetStarToZone(target, 31);
        }

        /// <summary>
        /// step - 10 安四化星诀 (化禄,化权,化科,化忌)
        /// </summary>
        private void Set4Hua()
        {
            int hualu, huaquan, huake, huaji;
            hualu = 0; huaquan = 0; huake = 0; huaji = 0;
            if (YearSky == 0) { hualu = 6; huaquan = 14; huake = 4; huaji = 3; }
            if (YearSky == 1) { hualu = 2; huaquan = 12; huake = 1; huaji = 8; }
            if (YearSky == 2) { hualu = 5; huaquan = 2; huake = 15; huaji = 6; }
            if (YearSky == 3) { hualu = 8; huaquan = 5; huake = 2; huaji = 10; }
            if (YearSky == 4) { hualu = 9; huaquan = 8; huake = 3; huaji = 2; }
            if (YearSky == 5) { hualu = 4; huaquan = 9; huake = 12; huaji = 16; }
            if (YearSky == 6) { hualu = 3; huaquan = 4; huake = 7; huaji = 5; }
            if (YearSky == 7) { hualu = 10; huaquan = 3; huake = 16; huaji = 15; }
            if (YearSky == 8) { hualu = 12; huaquan = 1; huake = 7; huaji = 4; }
            if (YearSky == 9) { hualu = 14; huaquan = 10; huake = 8; huaji = 9; }

            // 化禄
            GetStarInZone(hualu).xstar = StarList.GetStarById(27);

            // 化权
            GetStarInZone(huaquan).xstar = StarList.GetStarById(28);

            // 化科
            GetStarInZone(huake).xstar = StarList.GetStarById(29);

            // 化忌
            GetStarInZone(huaji).xstar = StarList.GetStarById(30);
        }

        /// <summary>
        /// step -11 定魁钺诀(年干) 天魁,天钺
        /// </summary>
        private void Set_s19_s20()
        {
            int sky, tiankui, tianyue;
            tiankui = 0;
            tianyue = 0;
            sky = this.YearSky;

            if (sky == 0 || sky == 4 || sky == 6) { tiankui = 1; tianyue = 7; }
            if (sky == 1 || sky == 5) { tiankui = 0; tianyue = 8; }
            if (sky == 2 || sky == 3) { tiankui = 11; tianyue = 9; }
            if (sky == 8 || sky == 9) { tiankui = 3; tianyue = 5; }
            if (sky == 7) { tiankui = 6; tianyue = 2; }

            // 天魁
            SetStarToZone(tiankui, 19);

            // 天钺
            SetStarToZone(tianyue, 20);
        }

        /// <summary>
        /// step -12 定禄存、羊、陀诀(年干)  禄存,擎羊,陀罗
        /// </summary>
        private void Set_s21_s23_s24()
        {
            int sky, lucun, target;
            lucun = 0;
            target = 0;
            sky = this.YearSky;

            if (sky == 0) lucun = 2;
            if (sky == 1) lucun = 3;
            if (sky == 2 || sky == 4) lucun = 5;
            if (sky == 3 || sky == 5) lucun = 6;
            if (sky == 6) lucun = 8;
            if (sky == 7) lucun = 9;
            if (sky == 8) lucun = 11;
            if (sky == 9) lucun = 0;

            // 禄存
            SetStarToZone(lucun, 21);

            // 擎羊
            target = DouUtility.DeasilCountZone(lucun, 1);
            SetStarToZone(target, 23);

            //	陀罗
            target = DouUtility.WiddershinsCountZone(lucun, 1);
            SetStarToZone(target, 24);
        }

        /// <summary>
        /// 火星
        /// </summary>
        private void Set_s25()
        {
            int earth, hour, target;
            earth = this.YearEarth;
            hour = this.hour;
            target = 0;

            if (earth == 0 || earth == 4 || earth == 8) target = 2;
            if (earth == 1 || earth == 5 || earth == 9) target = 3;
            if (earth == 2 || earth == 6 || earth == 10) target = 1;
            if (earth == 3 || earth == 7 || earth == 11) target = 9;

            target = DouUtility.DeasilCountZone(target, hour);
            SetStarToZone(target, 25);
        }

        /// <summary>
        /// 铃星
        /// </summary>
        private void Set_s26()
        {
            int earth, hour, target;
            earth = this.YearEarth;
            hour = this.hour;
            target = 0;

            if (earth == 2 || earth == 6 || earth == 10)
            {
                target = 3;
            }
            else
            {
                target = 10;
            }

            target = DouUtility.DeasilCountZone(target, hour);
            SetStarToZone(target, 26);
        }

        /// <summary>
        /// 天官
        /// </summary>
        private void Set_s49()
        {
            int sky, target;
            target = 0;
            sky = this.YearSky;

            if (sky == 3) target = 2;
            if (sky == 4) target = 3;
            if (sky == 1) target = 4;
            if (sky == 2) target = 5;
            if (sky == 9) target = 6;
            if (sky == 0) target = 7;
            if (sky == 5 || sky == 7) target = 9;
            if (sky == 8) target = 10;
            if (sky == 6) target = 11;

            SetStarToZone(target, 49);
        }

        /// <summary>
        /// 天福
        /// </summary>
        private void Set_s50()
        {
            int sky, target;
            target = 0;
            sky = this.YearSky;

            if (sky == 2) target = 0;
            if (sky == 5) target = 2;
            if (sky == 4) target = 3;
            if (sky == 7 || sky == 9) target = 5;
            if (sky == 6 || sky == 8) target = 6;
            if (sky == 1) target = 8;
            if (sky == 0) target = 9;
            if (sky == 3) target = 11;

            SetStarToZone(target, 50);
        }

        /// <summary>
        /// 天厨
        /// </summary>
        private void Set_s61()
        {
            int sky, target;
            target = 0;
            sky = this.YearSky;

            if (sky == 0 || sky == 3) target = 5;
            if (sky == 1 || sky == 4 || sky == 7) target = 6;
            if (sky == 2) target = 2;
            if (sky == 5) target = 8;
            if (sky == 6) target = 2;
            if (sky == 8) target = 9;
            if (sky == 9) target = 11;

            SetStarToZone(target, 61);
        }

        /// <summary>
        /// 正截空
        /// 傍截空
        /// </summary>
        private void Set_s115_s116()
        {
            int zheng, bang;
            zheng = 0;
            bang = 0;
            if (YearSky == 0 || YearSky == 5) { zheng = 8; bang = 9; }
            if (YearSky == 1 || YearSky == 6) { zheng = 6; bang = 7; }
            if (YearSky == 2 || YearSky == 7) { zheng = 4; bang = 5; }
            if (YearSky == 3 || YearSky == 8) { zheng = 2; bang = 3; }
            if (YearSky == 4 || YearSky == 9) { zheng = 0; bang = 1; }

            if (YinYang == 0)
            {
                zheng = zheng + bang;
                bang = zheng - bang;
                zheng = zheng - bang;
            }

            // 正截空
            SetStarToZone(zheng, 115);
            // 傍截空
            SetStarToZone(bang, 116);
        }

        /// <summary>
        /// 正旬空
        /// 傍旬空
        /// </summary>
        private void Set_s117_s118()
        {
            int zheng, bang, target, count;
            zheng = 0;
            bang = 0;

            count = 9 - YearSky;

            target = DouUtility.DeasilCountZone(YearEarth, count + 2);
            if (target % 2 == 0)
            {
                zheng = target;
                bang = target + 1;
            }
            else
            {
                zheng = target - 1;
                bang = target;
            }

            if (YinYang == 0)
            {
                zheng = zheng + bang;
                bang = zheng - bang;
                zheng = zheng - bang;
            }

            // 正旬空
            SetStarToZone(zheng, 117);

            // 傍旬空
            SetStarToZone(bang, 118);
        }

        /// <summary>
        /// 天马
        /// </summary>
        private void Set_s22()
        {
            int target;
            target = 0;

            if (YearEarth == 0 || YearEarth == 4 || YearEarth == 8) target = 2;
            if (YearEarth == 1 || YearEarth == 5 || YearEarth == 9) target = 11;
            if (YearEarth == 2 || YearEarth == 6 || YearEarth == 10) target = 8;
            if (YearEarth == 3 || YearEarth == 7 || YearEarth == 11) target = 5;

            SetStarToZone(target, 22);
        }

        /// <summary>
        /// 天空
        /// </summary>
        private void Set_s62()
        {
            int target;
            target = DouUtility.DeasilCountZone(1, YearEarth);
            SetStarToZone(target, 62);
        }

        /// <summary>
        /// 天虚
        /// </summary>
        private void Set_s38()
        {
            int target;
            target = 0;
            target = DouUtility.DeasilCountZone((int)Earth.WU, YearEarth);
            SetStarToZone(target, 38);
        }

        /// <summary>
        /// 天哭
        /// </summary>
        private void Set_s37()
        {
            int target;
            target = 0;
            target = DouUtility.WiddershinsCountZone((int)Earth.WU, YearEarth);
            SetStarToZone(target, 37);
        }

        /// <summary>
        /// 红鸾
        /// </summary>
        private void Set_s39()
        {
            int target;
            target = 0;
            target = DouUtility.WiddershinsCountZone((int)Earth.MAO, YearEarth);
            SetStarToZone(target, 39);
        }

        /// <summary>
        /// 天喜
        /// </summary>
        private void Set_s40()
        {
            int target;
            target = 0;
            target = DouUtility.WiddershinsCountZone((int)Earth.YOU, YearEarth);
            SetStarToZone(target, 40);
        }

        /// <summary>
        /// 孤辰
        /// </summary>
        private void Set_s53()
        {
            int target;
            target = 0;

            if (YearEarth == 0 || YearEarth == 1 || YearEarth == 11) target = 2;
            if (YearEarth == 2 || YearEarth == 3 || YearEarth == 4) target = 5;
            if (YearEarth == 5 || YearEarth == 6 || YearEarth == 7) target = 8;
            if (YearEarth == 8 || YearEarth == 9 || YearEarth == 10) target = 11;

            SetStarToZone(target, 53);
        }

        /// <summary>
        /// 寡宿
        /// </summary>
        private void Set_s54()
        {
            int target;
            target = 0;

            if (YearEarth == 0 || YearEarth == 1 || YearEarth == 11) target = 10;
            if (YearEarth == 2 || YearEarth == 3 || YearEarth == 4) target = 1;
            if (YearEarth == 5 || YearEarth == 6 || YearEarth == 7) target = 4;
            if (YearEarth == 8 || YearEarth == 9 || YearEarth == 10) target = 7;

            SetStarToZone(target, 54);
        }

        /// <summary>
        /// 劫煞
        /// </summary>
        private void Set_s65()
        {
            int target;
            target = 0;
            if (YearEarth == 0 || YearEarth == 4 || YearEarth == 8) target = 5;
            if (YearEarth == 1 || YearEarth == 5 || YearEarth == 9) target = 2;
            if (YearEarth == 2 || YearEarth == 6 || YearEarth == 10) target = 11;
            if (YearEarth == 3 || YearEarth == 7 || YearEarth == 11) target = 8;

            SetStarToZone(target, 65);
        }

        /// <summary>
        /// 大耗
        /// </summary>
        private void Set_s131()
        {
            int target = 0;

            target = DouUtility.AcrossZone(YearEarth);
            if (YearEarth % 2 == 0)
            {
                target = DouUtility.DeasilCountZone(target, 1);
            }
            else
            {
                target = DouUtility.WiddershinsCountZone(target, 1);
            }
            SetStarToZone(target, 131);
        }

        /// <summary>
        /// 蜚廉
        /// </summary>
        private void Set_s55()
        {
            int target = 0;

            if (YearEarth == 0) target = 8;
            if (YearEarth == 1) target = 9;
            if (YearEarth == 2) target = 10;
            if (YearEarth == 3) target = 5;
            if (YearEarth == 4) target = 6;
            if (YearEarth == 5) target = 7;
            if (YearEarth == 6) target = 2;
            if (YearEarth == 7) target = 3;
            if (YearEarth == 8) target = 4;
            if (YearEarth == 9) target = 11;
            if (YearEarth == 10) target = 0;
            if (YearEarth == 11) target = 1;

            SetStarToZone(target, 55);
        }

        /// <summary>
        /// 破碎
        /// </summary>
        private void Set_s56()
        {
            int target = 0;

            if (YearEarth == 0 || YearEarth == 3 || YearEarth == 6 || YearEarth == 9) target = 5;
            if (YearEarth == 1 || YearEarth == 4 || YearEarth == 7 || YearEarth == 10) target = 1;
            if (YearEarth == 2 || YearEarth == 5 || YearEarth == 8 || YearEarth == 11) target = 9;

            SetStarToZone(target, 56);
        }

        /// <summary>
        /// 华盖
        /// </summary>
        private void Set_s95()
        {
            int i = 0;
            switch (YearEarth)
            {
                case 0:
                case 4:
                case 8:
                    i = 4;
                    break;
                case 1:
                case 5:
                case 9:
                    i = 1;
                    break;
                case 2:
                case 6:
                case 10:
                    i = 10;
                    break;
                case 3:
                case 7:
                case 11:
                    i = 7;
                    break;
            }
            SetStarToZone(i, 95);
        }

        /// <summary>
        /// 咸池
        /// </summary>
        private void Set_s64()
        {
            int i = 0;
            switch (YearEarth)
            {
                case 0:
                case 4:
                case 8:
                    i = 9;
                    break;
                case 1:
                case 5:
                case 9:
                    i = 6;
                    break;
                case 2:
                case 6:
                case 10:
                    i = 3;
                    break;
                case 3:
                case 7:
                case 11:
                    i = 0;
                    break;
            }
            SetStarToZone(i, 64);
        }

        /// <summary>
        /// 龙德
        /// </summary>
        private void Set_s110()
        {
            int i = 0;
            i = DouUtility.DeasilCountZone(Earth.WEI, YearEarth);
            SetStarToZone(i, 110);
        }

        /// <summary>
        /// 月德
        /// </summary>
        private void Set_s66()
        {
            int i = 0;
            i = DouUtility.DeasilCountZone(Earth.SI, YearEarth);
            SetStarToZone(i, 66);
        }

        /// <summary>
        /// 天德
        /// </summary>
        private void Set_s112()
        {
            int i = 0;
            i = DouUtility.DeasilCountZone(Earth.YOU, YearEarth);
            SetStarToZone(i, 112);
        }

        /// <summary>
        /// 年解
        /// </summary>
        private void Set_s63()
        {
            int i = 0;
            i = DouUtility.WiddershinsCountZone(Earth.XU, YearEarth);
            SetStarToZone(i, 63);
        }

        /// <summary>
        /// 天才
        /// </summary>
        private void Set_s45()
        {
            int life = 0, target = 0;
            life = GetLifeInZone();
            target = DouUtility.DeasilCountZone(life, YearEarth);
            SetStarToZone(target, 45);
        }

        /// <summary>
        /// 天寿
        /// </summary>
        private void Set_s46()
        {
            int body = 0, target = 0;
            body = this.GetBodyInZone();
            target = DouUtility.DeasilCountZone(body, YearEarth);
            SetStarToZone(target, 46);

        }

        /// <summary>
        /// 龙池
        /// </summary>
        private void Set_s43()
        {
            int i = 0;
            i = DouUtility.DeasilCountZone(Earth.CHEN, YearEarth);
            SetStarToZone(i, 43);
        }

        /// <summary>
        /// 凤阁
        /// </summary>
        private void Set_s44()
        {
            int i = 0;
            i = DouUtility.WiddershinsCountZone(Earth.XU, YearEarth);
            SetStarToZone(i, 44);
        }

        /// <summary>
        /// 台辅
        /// </summary>
        private void Set_s51()
        {
            int wenqu = 0, target = 0;
            wenqu = GetZoneByStar(16);
            target = DouUtility.DeasilCountZone(wenqu, 2);
            SetStarToZone(target, 51);
        }

        /// <summary>
        /// 封诰
        /// </summary>
        private void Set_s52()
        {
            int wenqu = 0, target = 0;
            wenqu = GetZoneByStar(16);
            target = DouUtility.WiddershinsCountZone(wenqu, 2);
            SetStarToZone(target, 52);
        }

        /// <summary>
        /// 天刑
        /// 天刑酉上正月轮数至生月便住脚
        /// </summary>
        private void Set_s35()
        {
            int target = 0;
            target = DouUtility.DeasilCountZone(9, month);
            SetStarToZone(target, 35);
        }

        /// <summary>
        /// 天姚
        /// 天姚丑上顺正月 
        /// </summary>
        private void Set_s36()
        {
            int target = 0;
            target = DouUtility.DeasilCountZone(1, month);
            SetStarToZone(target, 36);
        }

        /// <summary>
        /// 解神
        /// 单月冲宫见解神
        /// </summary>
        private void Set_s57()
        {
            int target = 0;
            switch (month)
            {
                case 0:
                case 1:
                    target = 8;
                    break;
                case 2:
                case 3:
                    target = 10;
                    break;
                case 4:
                case 5:
                    target = 0;
                    break;
                case 6:
                case 7:
                    target = 2;
                    break;
                case 8:
                case 9:
                    target = 4;
                    break;
                case 10:
                case 11:
                    target = 6;
                    break;
            }

            SetStarToZone(target, 57);
        }

        /// <summary>
        /// 天巫
        /// 双月还依单月辰巳申寅亥天巫位 
        /// </summary>
        private void Set_s58()
        {
            int target = 0;
            switch (month)
            {
                case 0:
                case 4:
                case 8:
                    target = 5;
                    break;
                case 1:
                case 5:
                case 9:
                    target = 8;
                    break;
                case 2:
                case 6:
                case 10:
                    target = 2;
                    break;
                case 3:
                case 7:
                case 11:
                    target = 11;
                    break;
            }
            SetStarToZone(target, 58);
        }

        /// <summary>
        /// 天月
        /// </summary>
        private void Set_s59()
        {
            int target = 0;
            switch (month)
            {
                case 3:
                case 8:
                case 11:
                    target = 2;
                    break;
                case 5:
                    target = 3;
                    break;
                case 2:
                    target = 4;
                    break;
                case 1:
                    target = 5;
                    break;
                case 9:
                    target = 6;
                    break;
                case 4:
                case 7:
                    target = 7;
                    break;
                case 0:
                case 10:
                    target = 10;
                    break;
                case 6:
                    target = 11;
                    break;
            }
            SetStarToZone(target, 59);
        }

        /// <summary>
        /// 阴煞
        /// </summary>
        private void Set_s60()
        {
            int target = 0;
            switch (month)
            {
                case 0:
                case 6:
                    target = 2;
                    break;
                case 1:
                case 7:
                    target = 0;
                    break;
                case 2:
                case 8:
                    target = 10;
                    break;
                case 3:
                case 9:
                    target = 8;
                    break;
                case 4:
                case 10:
                    target = 6;
                    break;
                case 5:
                case 11:
                    target = 4;
                    break;
            }
            SetStarToZone(target, 60);
        }

        /// <summary>
        /// 天伤
        /// </summary>
        private void Set_s33()
        {
            int target = 0;
            if (IsDeasil == 0)
            {
                target = GetZoneById(7).dou;
            }
            else
            {
                target = GetZoneById(5).dou;
            }
            SetStarToZone(target, 33);
        }

        /// <summary>
        /// 天使
        /// </summary>
        private void Set_s34()
        {
            int target = 0;
            if (IsDeasil == 0)
            {
                target = GetZoneById(5).dou;
            }
            else
            {
                target = GetZoneById(7).dou;
            }
            SetStarToZone(target, 34);
        }

        /// <summary>
        /// 三台
        /// </summary>
        private void Set_s41()
        {
            int zuofu = 0, target = 0;
            zuofu = GetZoneByStar(17);
            target = DouUtility.DeasilCountZone(zuofu, day);
            SetStarToZone(target, 41);
        }

        /// <summary>
        /// 八座 
        /// </summary>
        private void Set_s42()
        {
            int youba = 0, target = 0;
            youba = GetZoneByStar(18);
            target = DouUtility.WiddershinsCountZone(youba, day);
            SetStarToZone(target, 42);
        }

        /// <summary>
        /// 恩光
        /// </summary>
        private void Set_s47()
        {
            int wenchang = 0, target = 0;
            wenchang = GetZoneByStar(15);
            target = DouUtility.DeasilCountZone(wenchang, day);
            target = DouUtility.WiddershinsCountZone(target, 1);
            SetStarToZone(target, 47);
        }

        /// <summary>
        /// 天贵
        /// </summary>
        private void Set_s48()
        {
            int wenqu = 0, target = 0;
            wenqu = GetZoneByStar(16);
            target = DouUtility.DeasilCountZone(wenqu, day);
            target = DouUtility.WiddershinsCountZone(target, 1);
            SetStarToZone(target, 48);
        }

        /// <summary>
        /// 命主
        /// </summary>
        private void Set_life_star()
        {
            int star = 0;
            switch (YearEarth)
            {
                case 0:
                    star = 9;
                    break;
                case 1:
                case 11:
                    star = 10;
                    break;
                case 2:
                case 10:
                    star = 21;
                    break;
                case 3:
                case 9:
                    star = 16;
                    break;
                case 4:
                case 8:
                    star = 6;
                    break;
                case 5:
                case 7:
                    star = 4;
                    break;
                case 6:
                    star = 14;
                    break;
            }
            life_star = star;
        }

        /// <summary>
        /// 身主
        /// </summary>
        private void Set_body_star()
        {
            int target = 0;
            switch (YearEarth)
            {
                case 0:
                case 6:
                    target = 25;
                    break;
                case 1:
                case 7:
                    target = 11;
                    break;
                case 2:
                case 8:
                    target = 12;
                    break;
                case 3:
                case 9:
                    target = 5;
                    break;
                case 4:
                case 10:
                    target = 15;
                    break;
                case 5:
                case 11:
                    target = 2;
                    break;
            }
            body_star = target;
        }

        /// <summary>
        /// 安长生十二神(五行局)阳男阴女顺行，阴男阳女逆行。
        /// </summary>
        private void Set_longlife_12()
        {
            int target = 0, star = 67;
            if (Five_X == 2 || Five_X == 5) target = 8;
            if (Five_X == 3) target = 11;
            if (Five_X == 4) target = 5;
            if (Five_X == 6) target = 2;

            for (int i = 0; i < 12; i++)
            {
                SetStarToZone(target, star);
                star++;
                if (YinYang == 1 && Sex == 1 || YinYang == 0 && Sex == 0) // 阳男阴女
                {
                    target = DouUtility.DeasilCountZone(target, 1);
                }
                else //阴男阳女
                {
                    target = DouUtility.WiddershinsCountZone(target, 1);
                }
            }
        }

        /// <summary>
        /// 安太岁十二神(年支)
        /// </summary>
        private void Set_grandage_12()
        {
            int target, star = 103;
            target = YearEarth;
            for (int i = 0; i < 12; i++)
            {
                SetStarToZone(target, star);
                star++;
                target = DouUtility.DeasilCountZone(target, 1);
            }
        }

        /// <summary>
        /// 安将前诸星(年支)
        /// </summary>
        private void Set_general_star_12()
        {
            int target = 0, star = 91;
            switch (YearEarth)
            {
                case 0:
                case 4:
                case 8:
                    target = 0;
                    break;
                case 1:
                case 5:
                case 9:
                    target = 9;
                    break;
                case 2:
                case 6:
                case 10:
                    target = 6;
                    break;
                case 3:
                case 7:
                case 11:
                    target = 3;
                    break;
            }

            for (int i = 0; i < 12; i++)
            {
                SetStarToZone(target, star);
                star++;
                target = DouUtility.DeasilCountZone(target, 1);
            }
        }

        /// <summary>
        /// 安生年博士十二神(以出生年干定禄存起，分顺逆)
        /// </summary>
        private void Set_doctor_12()
        {
            int target = 0, star = 79, lucun = 0;

            lucun = GetZoneByStar(21);
            target = lucun;

            for (int i = 0; i < 12; i++)
            {
                SetStarToZone(target, star);
                star++;
                if (YinYang == 1 && Sex == 1 || YinYang == 0 && Sex == 0) // 阳男阴女
                {
                    target = DouUtility.DeasilCountZone(target, 1);
                }
                else //阴男阳女
                {
                    target = DouUtility.WiddershinsCountZone(target, 1);
                }
            }
        }

        /// <summary>
        /// step - 42 流昌流曲
        /// </summary>
        private void SetLiuChangLiuQu()
        {
            int liuchang, liuqu;
            liuchang = 0; liuqu = 0;

            if (this.LiuYearSky == 0) { liuchang = 5; liuqu = 9; }
            if (this.LiuYearSky == 1) { liuchang = 6; liuqu = 8; }
            if (this.LiuYearSky == 2) { liuchang = 8; liuqu = 6; }
            if (this.LiuYearSky == 3) { liuchang = 9; liuqu = 5; }
            if (this.LiuYearSky == 4) { liuchang = 8; liuqu = 6; }
            if (this.LiuYearSky == 5) { liuchang = 9; liuqu = 5; }
            if (this.LiuYearSky == 6) { liuchang = 11; liuqu = 3; }
            if (this.LiuYearSky == 7) { liuchang = 0; liuqu = 2; }
            if (this.LiuYearSky == 8) { liuchang = 2; liuqu = 0; }
            if (this.LiuYearSky == 9) { liuchang = 3; liuqu = 11; }


            // 流昌
            SetStarToZone(liuchang, 119);

            // 流曲
            SetStarToZone(liuqu, 120);

        }

        /// <summary>
        /// 流魁 流钺
        /// </summary>
        private void SetLiuKuiLiuYue()
        {
            int tiankui, tianyue;
            tiankui = 0;
            tianyue = 0;

            if (this.LiuYearSky == 0 || this.LiuYearSky == 4 || this.LiuYearSky == 6) { tiankui = 1; tianyue = 7; }
            if (this.LiuYearSky == 1 || this.LiuYearSky == 5) { tiankui = 0; tianyue = 8; }
            if (this.LiuYearSky == 2 || this.LiuYearSky == 3) { tiankui = 11; tianyue = 9; }
            if (this.LiuYearSky == 8 || this.LiuYearSky == 9) { tiankui = 3; tianyue = 5; }
            if (this.LiuYearSky == 7) { tiankui = 6; tianyue = 2; }

            // 流魁
            SetStarToZone(tiankui, 121);

            // 流钺
            SetStarToZone(tianyue, 122);
        }

        /// <summary>
        /// 流羊 流陀 流禄
        /// </summary>
        private void SetLiuYangLiutuoLiulu()
        {
            int lucun, target;
            lucun = 0;
            target = 0;


            if (this.LiuYearSky == 0) lucun = 2;
            if (this.LiuYearSky == 1) lucun = 3;
            if (this.LiuYearSky == 2 || this.LiuYearSky == 4) lucun = 5;
            if (this.LiuYearSky == 3 || this.LiuYearSky == 5) lucun = 6;
            if (this.LiuYearSky == 6) lucun = 8;
            if (this.LiuYearSky == 7) lucun = 9;
            if (this.LiuYearSky == 8) lucun = 11;
            if (this.LiuYearSky == 9) lucun = 0;

            // 流禄
            SetStarToZone(lucun, 123);

            // 流禄
            target = DouUtility.DeasilCountZone(lucun, 1);
            SetStarToZone(target, 125);

            // 流陀
            target = DouUtility.WiddershinsCountZone(lucun, 1);
            SetStarToZone(target, 126);
        }


        /// <summary>
        /// 流四化星 (流化禄,流化权,流化科,流化忌)
        /// </summary>
        private void SetLiu4Hua()
        {

            int hualu, huaquan, huake, huaji;
            hualu = 0; huaquan = 0; huake = 0; huaji = 0;
            if (this.LiuYearSky == 0) { hualu = 6; huaquan = 14; huake = 4; huaji = 3; }
            if (this.LiuYearSky == 1) { hualu = 2; huaquan = 12; huake = 1; huaji = 8; }
            if (this.LiuYearSky == 2) { hualu = 5; huaquan = 2; huake = 15; huaji = 6; }
            if (this.LiuYearSky == 3) { hualu = 8; huaquan = 5; huake = 2; huaji = 10; }
            if (this.LiuYearSky == 4) { hualu = 9; huaquan = 8; huake = 3; huaji = 2; }
            if (this.LiuYearSky == 5) { hualu = 4; huaquan = 9; huake = 12; huaji = 16; }
            if (this.LiuYearSky == 6) { hualu = 3; huaquan = 4; huake = 7; huaji = 5; }
            if (this.LiuYearSky == 7) { hualu = 10; huaquan = 3; huake = 16; huaji = 15; }
            if (this.LiuYearSky == 8) { hualu = 12; huaquan = 1; huake = 7; huaji = 4; }
            if (this.LiuYearSky == 9) { hualu = 14; huaquan = 10; huake = 8; huaji = 9; }

            // 流化禄
            GetStarInZone(hualu).lxstar = StarList.GetStarById(127);

            // 流化权
            GetStarInZone(huaquan).lxstar = StarList.GetStarById(128);

            // 流化科
            GetStarInZone(huake).lxstar = StarList.GetStarById(129);

            // 流化忌
            GetStarInZone(huaji).lxstar = StarList.GetStarById(130);
        }

        /// <summary>
        /// step - 43 流曜
        /// </summary>
        public void SetLiuYao()
        {
            RemoveLiuYao();

            SetLiuChangLiuQu();
            SetLiuKuiLiuYue();
            SetLiuYangLiutuoLiulu();
            SetLiu4Hua();

        }

        /// <summary>
        /// 移除所有流耀星
        /// </summary>
        public void RemoveLiuYao()
        {
            RemoveStar(119);
            RemoveStar(120);
            RemoveStar(121);
            RemoveStar(122);
            RemoveStar(123);
            RemoveStar(125);
            RemoveStar(126);

            foreach (Zone z in zone_list)
            {
                foreach (Star s in z.stars)
                {
                    s.lxstar = null;
                }
            }


        }

        /// <summary>
        /// step - 44 定小限
        /// </summary>
        private void XiaoXian()
        {
        }

        /// <summary>
        /// 设置各星的等级
        /// </summary>
        private void SetStarLevel()
        {
            int starid, zoneNo;
            for (int i = 0; i < 15; i++)
            {
                starid = DouSource.MainStarLevel[i, 0];
                zoneNo = GetZoneByStar(starid);

                for (int j = 1; j < 13; j++)
                {
                    if (zoneNo == DouSource.MainStarLevel[0, j])
                    {
                        GetStarInZone(starid).Level = DouSource.MainStarLevel[i, j];
                    }
                }
            }
        }

        /// <summary>
        /// 获取命宫宫位ID
        /// </summary>
        /// <returns></returns>
        public int GetIDForLifeZone()
        {
            int life = 0;
            foreach (Zone item in zone_list)
            {
                if (item.dou == 0)
                {
                    life = item.id;
                    break;
                }
            }

            return life;
        }
    }

}
