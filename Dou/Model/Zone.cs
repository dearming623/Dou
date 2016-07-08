using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DouNamespace
{
    public class Zone
    {
        /// <summary>
        /// 宫的ID ( 地支 )
        /// </summary>
        public int id;

        /// <summary>
        /// 斗数宫位（命宫, 兄弟, 夫妻...）
        /// </summary>
        public int dou;

        /// <summary>
        /// 身宫
        /// </summary>
        public int body;

        /// <summary>
        /// 十二天干
        /// </summary>
        public int sky;

        /// <summary>
        /// 大限
        /// </summary>
        public int ten_year;

        /// <summary>
        /// 该宫位的所有星曜
        /// </summary>
        public List<Star> stars = new List<Star>();

        /// <summary>
        /// 是否身宫
        /// </summary>
        public bool IsBody;

        /// <summary>
        /// 是否命宫
        /// </summary>
        public bool IsLife;
    }
}
