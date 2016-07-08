using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DouNamespace
{
    #region ChineseCalendarException
    /// <summary>
    /// 中国日历异常处理
    /// </summary>
    public class ChineseCalendarException : System.Exception
    {
        public ChineseCalendarException(string msg)
            : base(msg)
        {
        }
    }

    #endregion

    /// <summary>
    /// 中国农历类 版本V1.0 支持 1900.1.31日起至 2049.12.31日止的数据
    /// </summary>
    /// <remarks>
    /// 本程序使用数据来源于网上的万年历查询，并综合了一些其它数据
    /// </remarks>
    public class ChineseCalendar
    {

        #region 内部变量
        private DateTime _date;
        private DateTime _datetime;

        private int _cYear;
        private int _cMonth;
        private int _cDay;
        private bool _cIsLeapMonth; //当月是否闰月
        private bool _cIsLeapYear; //当年是否有闰月
        #endregion

        #region 基础数据
        #region 基本常量
        private const int MinYear = 1900;
        private const int MaxYear = 2050;
        private static DateTime MinDay = new DateTime(1900, 1, 30);
        private static DateTime MaxDay = new DateTime(2049, 12, 31);
        private const int GanZhiStartYear = 1864; //干支计算起始年
        private static DateTime GanZhiStartDay = new DateTime(1899, 12, 22);//起始日
        private const string HZNum = "零一二三四五六七八九";
        #endregion

        #region 阴历数据
        /// <summary>
        /// 来源于网上的农历数据
        /// </summary>
        /// <remarks>
        /// 数据结构如下，共使用17位数据
        /// 第17位：表示闰月天数，0表示29天   1表示30天
        /// 第16位-第5位（共12位）表示12个月，其中第16位表示第一月，如果该月为30天则为1，29天为0
        /// 第4位-第1位（共4位）表示闰月是哪个月，如果当年没有闰月，则置0
        ///</remarks>
        private static int[] LunarDateArray = new int[]{
                0x04BD8,0x04AE0,0x0A570,0x054D5,0x0D260,0x0D950,0x16554,0x056A0,0x09AD0,0x055D2,
                0x04AE0,0x0A5B6,0x0A4D0,0x0D250,0x1D255,0x0B540,0x0D6A0,0x0ADA2,0x095B0,0x14977,
                0x04970,0x0A4B0,0x0B4B5,0x06A50,0x06D40,0x1AB54,0x02B60,0x09570,0x052F2,0x04970,
                0x06566,0x0D4A0,0x0EA50,0x06E95,0x05AD0,0x02B60,0x186E3,0x092E0,0x1C8D7,0x0C950,
                0x0D4A0,0x1D8A6,0x0B550,0x056A0,0x1A5B4,0x025D0,0x092D0,0x0D2B2,0x0A950,0x0B557,
                0x06CA0,0x0B550,0x15355,0x04DA0,0x0A5B0,0x14573,0x052B0,0x0A9A8,0x0E950,0x06AA0,
                0x0AEA6,0x0AB50,0x04B60,0x0AAE4,0x0A570,0x05260,0x0F263,0x0D950,0x05B57,0x056A0,
                0x096D0,0x04DD5,0x04AD0,0x0A4D0,0x0D4D4,0x0D250,0x0D558,0x0B540,0x0B6A0,0x195A6,
                0x095B0,0x049B0,0x0A974,0x0A4B0,0x0B27A,0x06A50,0x06D40,0x0AF46,0x0AB60,0x09570,
                0x04AF5,0x04970,0x064B0,0x074A3,0x0EA50,0x06B58,0x055C0,0x0AB60,0x096D5,0x092E0,
                0x0C960,0x0D954,0x0D4A0,0x0DA50,0x07552,0x056A0,0x0ABB7,0x025D0,0x092D0,0x0CAB5,
                0x0A950,0x0B4A0,0x0BAA4,0x0AD50,0x055D9,0x04BA0,0x0A5B0,0x15176,0x052B0,0x0A930,
                0x07954,0x06AA0,0x0AD50,0x05B52,0x04B60,0x0A6E6,0x0A4E0,0x0D260,0x0EA65,0x0D530,
                0x05AA0,0x076A3,0x096D0,0x04BD7,0x04AD0,0x0A4D0,0x1D0B6,0x0D250,0x0D520,0x0DD45,
                0x0B5A0,0x056D0,0x055B2,0x049B0,0x0A577,0x0A4B0,0x0AA50,0x1B255,0x06D20,0x0ADA0,
                0x14B63        
                };

        #endregion

        #region 农历相关数据
        private static string ganStr = "甲乙丙丁戊己庚辛壬癸";
        private static string zhiStr = "子丑寅卯辰巳午未申酉戌亥";
        private static string nStr1 = "日一二三四五六七八九";
        private static string nStr2 = "初十廿卅";
        private static string[] _monthString =
                {
                    "出错","正月","二月","三月","四月","五月","六月","七月","八月","九月","十月","十一月","腊月"
                };
        #endregion

        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public ChineseCalendar()
        {
        }

        #region ChinaCalendar <公历日期初始化>
        /// <summary>
        /// 用一个标准的公历日期来初使化
        /// </summary>
        /// <param name="dt"></param>
        public ChineseCalendar(DateTime dt)
        {
            int i;
            int leap;
            int temp;
            int offset;

            CheckDateLimit(dt);

            _date = dt.Date;
            _datetime = dt;

            //农历日期计算部分
            leap = 0;
            temp = 0;

            TimeSpan ts = _date - ChineseCalendar.MinDay;//计算两天的基本差距
            offset = ts.Days;

            for (i = MinYear; i <= MaxYear; i++)
            {
                temp = GetChineseYearDays(i);  //求当年农历年天数
                if (offset - temp < 1)
                    break;
                else
                {
                    offset = offset - temp;
                }
            }
            _cYear = i;

            leap = GetChineseLeapMonth(_cYear);//计算该年闰哪个月
            //设定当年是否有闰月
            if (leap > 0)
            {
                _cIsLeapYear = true;
            }
            else
            {
                _cIsLeapYear = false;
            }

            _cIsLeapMonth = false;
            for (i = 1; i <= 12; i++)
            {
                //闰月
                if ((leap > 0) && (i == leap + 1) && (_cIsLeapMonth == false))
                {
                    _cIsLeapMonth = true;
                    i = i - 1;
                    temp = GetChineseLeapMonthDays(_cYear); //计算闰月天数
                }
                else
                {
                    _cIsLeapMonth = false;
                    temp = GetChineseMonthDays(_cYear, i);//计算非闰月天数
                }

                offset = offset - temp;
                if (offset <= 0) break;
            }

            offset = offset + temp;
            _cMonth = i;
            _cDay = offset;
        }
        #endregion

        #region ChinaCalendar <农历日期初始化>
        /// <summary>
        /// 用农历的日期来初使化
        /// </summary>
        /// <param name="cy">农历年</param>
        /// <param name="cm">农历月</param>
        /// <param name="cd">农历日</param>
        /// <param name="LeapFlag">闰月标志</param>
        public ChineseCalendar(int cy, int cm, int cd, bool leapMonthFlag)
        {
            int i, leap, Temp, offset;

            CheckChineseDateLimit(cy, cm, cd, leapMonthFlag);

            _cYear = cy;
            _cMonth = cm;
            _cDay = cd;

            offset = 0;

            for (i = MinYear; i < cy; i++)
            {
                Temp = GetChineseYearDays(i); //求当年农历年天数
                offset = offset + Temp;
            }

            leap = GetChineseLeapMonth(cy);// 计算该年应该闰哪个月
            if (leap != 0)
            {
                this._cIsLeapYear = true;
            }
            else
            {
                this._cIsLeapYear = false;
            }

            if (cm != leap)
            {
                _cIsLeapMonth = false;  //当前日期并非闰月
            }
            else
            {
                _cIsLeapMonth = leapMonthFlag;  //使用用户输入的是否闰月月份
            }


            if ((_cIsLeapYear == false) || //当年没有闰月
                 (cm < leap)) //计算月份小于闰月     
            {
                #region ...
                for (i = 1; i < cm; i++)
                {
                    Temp = GetChineseMonthDays(cy, i);//计算非闰月天数
                    offset = offset + Temp;
                }

                //检查日期是否大于最大天
                if (cd > GetChineseMonthDays(cy, cm))
                {
                    throw new ChineseCalendarException("不合法的农历日期");
                }
                offset = offset + cd; //加上当月的天数
                #endregion
            }
            else   //是闰年，且计算月份大于或等于闰月
            {
                #region ...
                for (i = 1; i < cm; i++)
                {
                    Temp = GetChineseMonthDays(cy, i); //计算非闰月天数
                    offset = offset + Temp;
                }

                if (cm > leap) //计算月大于闰月
                {
                    Temp = GetChineseLeapMonthDays(cy);   //计算闰月天数
                    offset = offset + Temp;               //加上闰月天数

                    if (cd > GetChineseMonthDays(cy, cm))
                    {
                        throw new ChineseCalendarException("不合法的农历日期");
                    }
                    offset = offset + cd;
                }
                else  //计算月等于闰月
                {
                    //如果需要计算的是闰月，则应首先加上与闰月对应的普通月的天数
                    if (this._cIsLeapMonth == true) //计算月为闰月
                    {
                        Temp = GetChineseMonthDays(cy, cm); //计算非闰月天数
                        offset = offset + Temp;
                    }

                    if (cd > GetChineseLeapMonthDays(cy))
                    {
                        throw new ChineseCalendarException("不合法的农历日期");
                    }
                    offset = offset + cd;
                }
                #endregion
            }


            _date = MinDay.AddDays(offset);
        }
        #endregion
        #endregion

        #region 私有函数

        #region GetChineseMonthDays
        //传回农历 y年m月的总天数
        private int GetChineseMonthDays(int year, int month)
        {
            if (BitTest32((LunarDateArray[year - MinYear] & 0x0000FFFF), (16 - month)))
            {
                return 30;
            }
            else
            {
                return 29;
            }
        }
        #endregion

        #region GetChineseLeapMonth
        //传回农历 y年闰哪个月 1-12 , 没闰传回 0
        private int GetChineseLeapMonth(int year)
        {

            return LunarDateArray[year - MinYear] & 0xF;

        }
        #endregion

        #region GetChineseLeapMonthDays
        //传回农历 y年闰月的天数
        private int GetChineseLeapMonthDays(int year)
        {
            if (GetChineseLeapMonth(year) != 0)
            {
                if ((LunarDateArray[year - MinYear] & 0x10000) != 0)
                {
                    return 30;
                }
                else
                {
                    return 29;
                }
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region GetChineseYearDays
        /// <summary>
        /// 取农历年一年的天数
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private int GetChineseYearDays(int year)
        {
            int i, f, sumDay, info;

            sumDay = 348; //29天 X 12个月
            i = 0x8000;
            info = LunarDateArray[year - MinYear] & 0x0FFFF;

            //计算12个月中有多少天为30天
            for (int m = 0; m < 12; m++)
            {
                f = info & i;
                if (f != 0)
                {
                    sumDay++;
                }
                i = i >> 1;
            }
            return sumDay + GetChineseLeapMonthDays(year);
        }
        #endregion

        #region GetChineseHour
        /// <summary>
        /// 获得当前时间的时辰
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        /// 
        private string GetChineseHour(DateTime dt)
        {

            int _hour, _minute, offset, i;
            int indexGan;
            string ganHour, zhiHour;
            string tmpGan;

            //计算时辰的地支
            _hour = dt.Hour;    //获得当前时间小时
            _minute = dt.Minute;  //获得当前时间分钟

            if (_minute != 0) _hour += 1;
            offset = _hour / 2;
            if (offset >= 12) offset = 0;
            //zhiHour = zhiStr[offset].ToString();

            //计算天干
            TimeSpan ts = this._date - GanZhiStartDay;
            i = ts.Days % 60;

            indexGan = ((i % 10 + 1) * 2 - 1) % 10 - 1; //ganStr[i % 10] 为日的天干,(n*2-1) %10得出地支对应,n从1开始
            tmpGan = ganStr.Substring(indexGan) + ganStr.Substring(0, indexGan + 2);//凑齐12位
            //ganHour = ganStr[((i % 10 + 1) * 2 - 1) % 10 - 1].ToString();

            return tmpGan[offset].ToString() + zhiStr[offset].ToString();

        }
        #endregion

        #region CheckDateLimit
        /// <summary>
        /// 检查公历日期是否符合要求
        /// </summary>
        /// <param name="dt"></param>
        private void CheckDateLimit(DateTime dt)
        {
            if ((dt < MinDay) || (dt > MaxDay))
            {
                throw new ChineseCalendarException("超出可转换的日期");
            }
        }
        #endregion

        #region CheckChineseDateLimit
        /// <summary>
        /// 检查农历日期是否合理
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="leapMonth"></param>
        private void CheckChineseDateLimit(int year, int month, int day, bool leapMonth)
        {
            if ((year < MinYear) || (year > MaxYear))
            {
                throw new ChineseCalendarException("非法农历日期");
            }
            if ((month < 1) || (month > 12))
            {
                throw new ChineseCalendarException("非法农历日期");
            }
            if ((day < 1) || (day > 30)) //中国的月最多30天
            {
                throw new ChineseCalendarException("非法农历日期");
            }

            int leap = GetChineseLeapMonth(year);// 计算该年应该闰哪个月
            if ((leapMonth == true) && (month != leap))
            {
                throw new ChineseCalendarException("非法农历日期");
            }


        }
        #endregion

        #region ConvertNumToChineseNum
        /// <summary>
        /// 将0-9转成汉字形式
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private string ConvertNumToChineseNum(char n)
        {
            if ((n < '0') || (n > '9')) return "";
            switch (n)
            {
                case '0':
                    return HZNum[0].ToString();
                case '1':
                    return HZNum[1].ToString();
                case '2':
                    return HZNum[2].ToString();
                case '3':
                    return HZNum[3].ToString();
                case '4':
                    return HZNum[4].ToString();
                case '5':
                    return HZNum[5].ToString();
                case '6':
                    return HZNum[6].ToString();
                case '7':
                    return HZNum[7].ToString();
                case '8':
                    return HZNum[8].ToString();
                case '9':
                    return HZNum[9].ToString();
                default:
                    return "";
            }
        }
        #endregion

        #region BitTest32
        /// <summary>
        /// 测试某位是否为真
        /// </summary>
        /// <param name="num"></param>
        /// <param name="bitpostion"></param>
        /// <returns></returns>
        private bool BitTest32(int num, int bitpostion)
        {

            if ((bitpostion > 31) || (bitpostion < 0))
                throw new Exception("Error Param: bitpostion[0-31]:" + bitpostion.ToString());

            int bit = 1 << bitpostion;

            if ((num & bit) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #endregion

        #region 公历日期
        #region Date
        /// <summary>
        /// 取对应的公历日期
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        #endregion

 

        #region IsLeapYear
        /// <summary>
        /// 当前是否公历闰年
        /// </summary>
        public bool IsLeapYear
        {
            get
            {
                return DateTime.IsLeapYear(this._date.Year);
            }
        }
        #endregion

        #region ChineseHour
        /// <summary>
        /// 时辰
        /// </summary>
        public string ChineseHour
        {
            get
            {
                return GetChineseHour(_datetime);
            }
        }
        #endregion

        #endregion

        #region 农历日期
        #region IsChineseLeapMonth
        /// <summary>
        /// 是否闰月
        /// </summary>
        public bool IsChineseLeapMonth
        {
            get { return this._cIsLeapMonth; }
        }
        #endregion

        #region IsChineseLeapYear
        /// <summary>
        /// 当年是否有闰月
        /// </summary>
        public bool IsChineseLeapYear
        {
            get
            {
                return this._cIsLeapYear;
            }
        }
        #endregion

        #region ChineseDay
        /// <summary>
        /// 农历日
        /// </summary>
        public int ChineseDay
        {
            get { return this._cDay; }
        }
        #endregion

        #region ChineseDayString
        /// <summary>
        /// 农历日中文表示
        /// </summary>
        public string ChineseDayString
        {
            get
            {
                switch (this._cDay)
                {
                    case 0:
                        return "";
                    case 10:
                        return "初十";
                    case 20:
                        return "二十";
                    case 30:
                        return "三十";
                    default:
                        return nStr2[(int)(_cDay / 10)].ToString() + nStr1[_cDay % 10].ToString();

                }
            }
        }
        #endregion

        #region ChineseMonth
        /// <summary>
        /// 农历的月份
        /// </summary>
        public int ChineseMonth
        {
            get { return this._cMonth; }
        }
        #endregion

        #region ChineseMonthString
        /// <summary>
        /// 农历月份字符串
        /// </summary>
        public string ChineseMonthString
        {
            get
            {
                return _monthString[this._cMonth];
            }
        }
        #endregion

        #region ChineseYear
        /// <summary>
        /// 取农历年份
        /// </summary>
        public int ChineseYear
        {
            get { return this._cYear; }
        }
        #endregion

        #region ChineseYearString
        /// <summary>
        /// 取农历年字符串如，一九九七年
        /// </summary>
        public string ChineseYearString
        {
            get
            {
                string tempStr = "";
                string num = this._cYear.ToString();
                for (int i = 0; i < 4; i++)
                {
                    tempStr += ConvertNumToChineseNum(num[i]);
                }
                return tempStr + "年";
            }
        }
        #endregion

        #region ChineseDateString
        /// <summary>
        /// 取农历日期表示法：农历一九九七年正月初五
        /// </summary>
        public string ChineseDateString
        {
            get
            {
                if (this._cIsLeapMonth == true)
                {
                    return "农历" + ChineseYearString + "闰" + ChineseMonthString + ChineseDayString;
                }
                else
                {
                    return "农历" + ChineseYearString + ChineseMonthString + ChineseDayString;
                }
            }
        }
        #endregion





        #endregion

        #region 天干地支
        #region GanZhiYearString
        /// <summary>
        /// 取农历年的干支表示法如 乙丑年
        /// </summary>
        public string GanZhiYearString
        {
            get
            {
                string tempStr;
                int i = (this._cYear - GanZhiStartYear) % 60; //计算干支
                tempStr = ganStr[i % 10].ToString() + zhiStr[i % 12].ToString() + "年";
                return tempStr;
            }
        }
        #endregion

        #region GanZhiMonthString
        /// <summary>
        /// 取干支的月表示字符串，注意农历的闰月不记干支
        /// </summary>
        public string GanZhiMonthString
        {
            get
            {
                //每个月的地支总是固定的,而且总是从寅月开始
                int zhiIndex;
                string zhi;
                if (this._cMonth > 10)
                {
                    zhiIndex = this._cMonth - 10;
                }
                else
                {
                    zhiIndex = this._cMonth + 2;
                }
                zhi = zhiStr[zhiIndex - 1].ToString();

                //根据当年的干支年的干来计算月干的第一个
                int ganIndex = 1;
                string gan;
                int i = (this._cYear - GanZhiStartYear) % 60; //计算干支
                switch (i % 10)
                {
                    #region ...
                    case 0: //甲
                        ganIndex = 3;
                        break;
                    case 1: //乙
                        ganIndex = 5;
                        break;
                    case 2: //丙
                        ganIndex = 7;
                        break;
                    case 3: //丁
                        ganIndex = 9;
                        break;
                    case 4: //戊
                        ganIndex = 1;
                        break;
                    case 5: //己
                        ganIndex = 3;
                        break;
                    case 6: //庚
                        ganIndex = 5;
                        break;
                    case 7: //辛
                        ganIndex = 7;
                        break;
                    case 8: //壬
                        ganIndex = 9;
                        break;
                    case 9: //癸
                        ganIndex = 1;
                        break;
                    #endregion
                }
                gan = ganStr[(ganIndex + this._cMonth - 2) % 10].ToString();

                return gan + zhi + "月";
            }
        }
        #endregion

        #region GanZhiDayString
        /// <summary>
        /// 取干支日表示法
        /// </summary>
        public string GanZhiDayString
        {
            get
            {
                int i, offset;
                TimeSpan ts = this._date - GanZhiStartDay;
                offset = ts.Days;
                i = offset % 60;
                return ganStr[i % 10].ToString() + zhiStr[i % 12].ToString() + "日";
            }
        }
        #endregion

        #region GanZhiDateString
        /// <summary>
        /// 取当前日期的干支表示法如 甲子年乙丑月丙庚日
        /// </summary>
        public string GanZhiDateString
        {
            get
            {
                return GanZhiYearString + GanZhiMonthString + GanZhiDayString;
            }
        }
        #endregion
        #endregion

        #region 方法
        #region NextDay
        /// <summary>
        /// 取下一天
        /// </summary>
        /// <returns></returns>
        public ChineseCalendar NextDay()
        {
            DateTime nextDay = _date.AddDays(1);
            return new ChineseCalendar(nextDay);
        }
        #endregion

        #region PervDay
        /// <summary>
        /// 取前一天
        /// </summary>
        /// <returns></returns>
        public ChineseCalendar PervDay()
        {
            DateTime pervDay = _date.AddDays(-1);
            return new ChineseCalendar(pervDay);
        }
        #endregion
        #endregion


        /// <summary>
        /// 取农历年的干支表示法如 乙丑年
        /// </summary>
        public int YearSky
        {
            get
            {
                int i;
                i = (this._cYear - GanZhiStartYear) % 60;
                i = i % 10;
                return i;
            }
        }

        /// <summary>
        /// 取农历年的干支表示法如 乙丑年
        /// </summary>
        public int YearEarth
        {
            get
            {
                int i;
                i = (this._cYear - GanZhiStartYear) % 60;
                i = i % 12;
                return i;
            }
        }

        /// <summary>
        /// 获取年干
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public int GetYearSky(int year)
        {
            int i;
            i = (year - GanZhiStartYear) % 60;
            i = i % 10;
            return i;
        }

        /// <summary>
        /// 获取年支
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public int GetYearEarth(int year)
        {
            int i;
            i = (year - GanZhiStartYear) % 60;
            i = i % 12;
            return i;
        }
    }
}