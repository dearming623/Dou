using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DouNamespace.DBUtility;

namespace DouNamespace
{
    /// <summary>
    /// 星曜数据访问（SQLite Star 表）。
    /// </summary>
    public class StarList
    {
        public static StarList Instance { get; } = new StarList();

        public static List<Star> doustars;

        public static void Load()
        {
            DataSet ds = DbHelperSQLite.Query("SELECT Id, Name, Type, Position FROM Star ORDER BY Id");

            doustars = new List<Star>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Star newStar = new Star();
                newStar.ID = Convert.ToInt32(row["Id"]);
                newStar.Name = Convert.ToString(row["Name"]);
                newStar.type = Convert.ToInt32(row["Type"]);
                newStar.position = Convert.ToInt32(row["Position"]);
                newStar.Level = 0;
                doustars.Add(newStar);
            }
        }

        public static Star GetStarById(int id)
        {
            Star result = null;

            foreach (Star s in GetDouStars())
            {
                if (s.ID == id)
                {
                    result = s;
                    break;
                }
            }
            return result;
        }

        public static List<Star> GetDouStars()
        {
            if (doustars == null) Load();
            return doustars;
        }
    }
}
