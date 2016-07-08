using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DouNamespace
{
    public class Star
    {
        public int ID;
        public string Name;
        public string Description;

        /// <summary>
        /// 庙旺平陷
        /// </summary>
        public int Level;

        /// <summary>
        /// 四化星
        /// </summary>
        public Star xstar;

        /// <summary>
        /// 类型
        /// </summary>
        public int type;

        /// <summary>
        /// 位置
        /// </summary>
        public int position;

        /// <summary>
        /// 流四化星
        /// </summary>
        public Star lxstar;

    }
}
