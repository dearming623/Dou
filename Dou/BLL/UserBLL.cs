using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DouNamespace.DAL;

namespace DouNamespace.BLL
{
    public class UserBLL
    {

        private readonly UserDAL dal = new UserDAL();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string UserId)
        {
            return dal.Exists(UserId);
        }
      
    }
}
