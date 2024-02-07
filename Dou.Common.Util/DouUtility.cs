#region <<Notes>>
/*----------------------------------------------------------------
 * Copy right (c) 2024  All rights reserved
 * CLR Ver: 4.0.30319.42000
 * Computer: MOLEQ-MING
 * Company: 
 * namespace: Dou.Common.Util
 * Unique: eec7a2ae-9d54-4394-968f-c0178098756d
 * File name: DouUtility
 * Domain: MOLEQ-MING
 * 
 * @author: Ming
 * @email: t8ming@live.com
 * @date: 2/7/2024 18:57:47
 *----------------------------------------------------------------*/
#endregion <<Notes>>

using Dou.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dou.Common.Util
{
    public static class DouUtility
    {

        /// <summary>
        /// 顺数
        /// </summary>
        /// <param name="startIdx"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int DeasilCountZone(int startIdx, int count)
        {
            int i;
            i = startIdx + count;
            i %= 12;
            return i;
        }

        /// <summary>
        /// 顺数
        /// </summary>
        /// <param name="earth">地支</param>
        /// <param name="count">数多少格</param>
        /// <returns></returns>
        public static int DeasilCountZone(Earth earth, int count)
        {
            int i;
            i = (int)earth + count;
            i %= 12;
            return i;
        }

        /// <summary>
        /// 逆数
        /// </summary>
        /// <param name="startIdx"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int WiddershinsCountZone(int startIdx, int count)
        {
            int i;
            i = startIdx - count;
            while (i < 0)
            {
                i += 12;
            }
            return i;
        }

        /// <summary>
        /// 逆数
        /// </summary>
        /// <param name="earth">地支</param>
        /// <param name="count">数多少格</param>
        /// <returns></returns>
        public static int WiddershinsCountZone(Earth earth, int count)
        {
            int i;
            i = (int)earth - count;
            while (i < 0)
            {
                i += 12;
            }
            return i;
        }

        /// <summary>
        /// 对宫
        /// </summary>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        public static int AcrossZone(int zoneId)
        {
            int i;
            i = DeasilCountZone(zoneId, 6);
            return i;
        }
    }
}
