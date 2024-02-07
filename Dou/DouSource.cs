using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DouNamespace
{
    public static class DouSource
    {
        /// <summary>
        /// 天干（甲乙丙丁戊己庚辛壬癸）
        /// </summary>
        public static string[] SKY = new string[] { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };

        /// <summary>
        /// 地支(子丑寅卯辰巳午未申酉戌亥)
        /// </summary>
        public static string[] EARTH = new string[] { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };

        /// <summary>
        /// 斗数宫位（命宫, 兄弟, 夫妻...）
        /// </summary>
        public static string[] ZoneName = new string[] { "命宫", "兄弟", "夫妻", "子女", "财帛", "疾厄", "迁移", "交友", "事业", "田宅", "福德", "父母" };

        /// <summary>
        /// 五行（水二局, 木三局, 金四局...）
        /// </summary>
        public static string[] FiveX = new string[] { " ", " ", "水二局", "木三局", "金四局", "土五局", "火六局" };

        /// <summary>
        /// 阴阳
        /// </summary>
        public static string[] YinYang = new string[] { "阴", "阳" };

        /// <summary>
        /// 性别
        /// </summary>
        public static string[] SEX = new string[] { "女", "男" };

        /// <summary>
        /// 身宫
        /// </summary>
        public static string[] BODY = new string[] { " ", "身宫" };

        /// <summary>
        /// 星曜等级
        /// </summary>
        public static string[] LEVEL = new string[] { "", "庙", "旺", "平", "闲", "陷" };

        /// <summary>
        /// 诸星庙陷总表, 1-[庙]、2-[旺]、3-[平]、4-[闲]、5-[陷],
        /// </summary>
        public static int[,] MainStarLevel = new int[,]{
                                                                    {-1,0,1,2,3,4,5,6,7,8,9,10,11},
                                                                    {1,3,1,1,2,5,2,1,1,2,3,4,2},
                                                                    {2,1,5,2,2,1,3,1,5,3,2,1,3},
                                                                    {3,5,5,2,1,2,2,1,3,4,4,5,5},
                                                                    {4,2,1,4,5,1,3,2,1,3,2,1,3},
                                                                    {5,2,5,4,1,3,1,5,5,2,3,3,1},
                                                                    {6,3,2,1,4,2,5,3,1,1,3,2,5},
                                                                    {7,1,1,1,3,1,3,2,1,3,5,1,2},
                                                                    {8,1,1,4,5,4,5,5,3,3,2,2,1},
                                                                    {9,2,1,3,4,1,5,2,1,3,3,1,5},
                                                                    {10,2,2,1,1,3,3,2,5,1,1,2,2},
                                                                    {11,1,1,1,5,2,3,2,4,1,5,4,2},
                                                                    {12,1,2,1,1,2,5,1,2,5,4,2,5},
                                                                    {13,2,1,1,5,2,3,2,2,1,4,1,3},
                                                                    {14,1,2,5,2,2,4,1,1,5,5,2,3},
                                                             };

        /// <summary>
        /// 时辰详细内容
        /// </summary>
        public static string[] HourContent = new string[] { "子 (00:00 - 00:59)", 
                                                                                "丑 (01:00 - 02:59)", 
                                                                                "寅 (03:00 - 04:59)", 
                                                                                "卯 (05:00 - 06:59)", 
                                                                                "辰 (07:00 - 08:59)", 
                                                                                "巳 (09:00 - 10:59)", 
                                                                                "午 (11:00 - 12:59)", 
                                                                                "未 (13:00 - 14:59)",
                                                                                "申 (15:00 - 19:59)",
                                                                                "酉 (17:00 - 18:59)", 
                                                                                "戌 (19:00 - 20:59)", 
                                                                                "亥 (21:00 - 22:59)",
                                                                                "子 (23:00 - 00:00)"};

        public static string[] PlateType = new string[] { "天盘"," 地盘", "人盘" };
    }

    

    

}
