using System;
using System.Data.SQLite;
using System.IO;
using System.Xml;

namespace DouNamespace.DBUtility
{
    /// <summary>
    /// 数据库初始化：建表、星曜基础数据初始化、旧 users.xml 一次性导入。
    /// </summary>
    public static class DbInitializer
    {
        public static void EnsureDatabase()
        {
            CreateTables();
            SeedStars();
            ImportLegacyUsers();
        }

        private static void CreateTables()
        {
            DbHelperSQLite.ExecuteSql(@"
CREATE TABLE IF NOT EXISTS User (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Sex INTEGER NOT NULL DEFAULT 0,
    Year INTEGER NOT NULL DEFAULT 0,
    Month INTEGER NOT NULL DEFAULT 0,
    Day INTEGER NOT NULL DEFAULT 0,
    Hour INTEGER NOT NULL DEFAULT 0,
    Minute INTEGER NOT NULL DEFAULT 0,
    Phone TEXT,
    Mobile TEXT,
    Email TEXT,
    QQ TEXT,
    Notes TEXT,
    GroupName TEXT
)");

            DbHelperSQLite.ExecuteSql(@"
CREATE TABLE IF NOT EXISTS Star (
    Id INTEGER PRIMARY KEY,
    Name TEXT NOT NULL,
    Type INTEGER NOT NULL DEFAULT 0,
    Position INTEGER NOT NULL DEFAULT 0
)");
        }

        private static void SeedStars()
        {
            object count = DbHelperSQLite.GetSingle("SELECT COUNT(1) FROM Star");
            if (count != null && Convert.ToInt32(count) > 0) return;

            using (SQLiteConnection connection = new SQLiteConnection(DbHelperSQLite.connectionString))
            {
                connection.Open();
                using (SQLiteTransaction trans = connection.BeginTransaction())
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(connection))
                    {
                        cmd.Transaction = trans;
                        cmd.CommandText = "INSERT INTO Star (Id, Name, Type, Position) VALUES (@Id, @Name, @Type, @Position)";
                        SQLiteParameter pId = cmd.Parameters.Add("@Id", System.Data.DbType.Int32);
                        SQLiteParameter pName = cmd.Parameters.Add("@Name", System.Data.DbType.String);
                        SQLiteParameter pType = cmd.Parameters.Add("@Type", System.Data.DbType.Int32);
                        SQLiteParameter pPosition = cmd.Parameters.Add("@Position", System.Data.DbType.Int32);

                        foreach (object[] star in StarSeedData)
                        {
                            pId.Value = star[0];
                            pName.Value = star[1];
                            pType.Value = star[2];
                            pPosition.Value = star[3];
                            cmd.ExecuteNonQuery();
                        }
                    }
                    trans.Commit();
                }
            }
        }

        /// <summary>
        /// User 表为空且输出目录存在旧 users.xml 时，一次性导入旧数据。
        /// 导入完成后把旧文件改名为 users.xml.bak，避免重复导入。
        /// </summary>
        private static void ImportLegacyUsers()
        {
            string xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.xml");
            if (!File.Exists(xmlPath)) return;

            object count = DbHelperSQLite.GetSingle("SELECT COUNT(1) FROM User");
            if (count != null && Convert.ToInt32(count) > 0) return;

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(xmlPath);

            using (SQLiteConnection connection = new SQLiteConnection(DbHelperSQLite.connectionString))
            {
                connection.Open();
                using (SQLiteTransaction trans = connection.BeginTransaction())
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(connection))
                    {
                        cmd.Transaction = trans;
                        cmd.CommandText = @"INSERT INTO User (Name, Sex, Year, Month, Day, Hour, Minute, Phone, Mobile, Email, QQ, Notes, GroupName)
VALUES (@Name, @Sex, @Year, @Month, @Day, @Hour, @Minute, @Phone, @Mobile, @Email, @QQ, @Notes, @GroupName)";

                        foreach (XmlNode node in xmldoc.DocumentElement.ChildNodes)
                        {
                            XmlElement element = node as XmlElement;
                            if (element == null) continue;

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@Name", element.GetAttribute("name"));
                            cmd.Parameters.AddWithValue("@Sex", ToInt(element.GetAttribute("sex")));
                            cmd.Parameters.AddWithValue("@Year", ToInt(element.GetAttribute("year")));
                            cmd.Parameters.AddWithValue("@Month", ToInt(element.GetAttribute("month")));
                            cmd.Parameters.AddWithValue("@Day", ToInt(element.GetAttribute("day")));
                            cmd.Parameters.AddWithValue("@Hour", ToInt(element.GetAttribute("hour")));
                            cmd.Parameters.AddWithValue("@Minute", ToInt(element.GetAttribute("minute")));
                            cmd.Parameters.AddWithValue("@Phone", element.GetAttribute("phone"));
                            cmd.Parameters.AddWithValue("@Mobile", element.GetAttribute("mobile"));
                            cmd.Parameters.AddWithValue("@Email", element.GetAttribute("email"));
                            cmd.Parameters.AddWithValue("@QQ", element.GetAttribute("qq"));
                            cmd.Parameters.AddWithValue("@Notes", element.GetAttribute("notes"));
                            cmd.Parameters.AddWithValue("@GroupName", element.GetAttribute("group"));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    trans.Commit();
                }
            }

            string bakPath = xmlPath + ".bak";
            if (File.Exists(bakPath)) File.Delete(bakPath);
            File.Move(xmlPath, bakPath);
        }

        private static int ToInt(string value)
        {
            int result;
            if (int.TryParse(value, out result)) return result;
            return 0;
        }

        /// <summary>
        /// 星曜基础数据（原 doustars.xml）：Id, Name, Type, Position
        /// </summary>
        private static readonly object[][] StarSeedData = new object[][]
        {
            new object[] { 1, "紫微", 1, 1 },
            new object[] { 2, "天机", 1, 1 },
            new object[] { 3, "太阳", 1, 1 },
            new object[] { 4, "武曲", 1, 1 },
            new object[] { 5, "天同", 1, 1 },
            new object[] { 6, "廉贞", 1, 1 },
            new object[] { 7, "天府", 1, 1 },
            new object[] { 8, "太阴", 1, 1 },
            new object[] { 9, "贪狼", 1, 1 },
            new object[] { 10, "巨门", 1, 1 },
            new object[] { 11, "天相", 1, 1 },
            new object[] { 12, "天梁", 1, 1 },
            new object[] { 13, "七杀", 1, 1 },
            new object[] { 14, "破军", 1, 1 },
            new object[] { 15, "文昌", 3, 2 },
            new object[] { 16, "文曲", 3, 2 },
            new object[] { 17, "左辅", 2, 2 },
            new object[] { 18, "右弼", 2, 2 },
            new object[] { 19, "天魁", 2, 2 },
            new object[] { 20, "天钺", 2, 2 },
            new object[] { 21, "禄存", 3, 2 },
            new object[] { 22, "天马", 3, 2 },
            new object[] { 23, "擎羊", 4, 2 },
            new object[] { 24, "陀罗", 4, 2 },
            new object[] { 25, "火星", 4, 2 },
            new object[] { 26, "铃星", 4, 2 },
            new object[] { 27, "化禄", 6, 3 },
            new object[] { 28, "化权", 6, 3 },
            new object[] { 29, "化科", 6, 3 },
            new object[] { 30, "化忌", 6, 3 },
            new object[] { 31, "地空", 5, 4 },
            new object[] { 32, "地劫", 5, 4 },
            new object[] { 33, "天伤", 13, 4 },
            new object[] { 34, "天使", 13, 4 },
            new object[] { 35, "天刑", 8, 4 },
            new object[] { 36, "天姚", 10, 4 },
            new object[] { 37, "天哭", 13, 4 },
            new object[] { 38, "天虚", 13, 4 },
            new object[] { 39, "红鸾", 10, 4 },
            new object[] { 40, "天喜", 10, 4 },
            new object[] { 41, "三台", 12, 4 },
            new object[] { 42, "八座", 12, 4 },
            new object[] { 43, "龙池", 13, 4 },
            new object[] { 44, "凤阁", 13, 4 },
            new object[] { 45, "天才", 13, 4 },
            new object[] { 46, "天寿", 13, 4 },
            new object[] { 47, "恩光", 12, 4 },
            new object[] { 48, "天贵", 12, 4 },
            new object[] { 49, "天官", 12, 4 },
            new object[] { 50, "天福", 12, 4 },
            new object[] { 51, "台辅", 12, 4 },
            new object[] { 52, "封诰", 12, 4 },
            new object[] { 53, "孤辰", 14, 4 },
            new object[] { 54, "寡宿", 13, 4 },
            new object[] { 55, "蜚廉", 13, 4 },
            new object[] { 56, "破碎", 13, 4 },
            new object[] { 57, "解神", 13, 4 },
            new object[] { 58, "天巫", 13, 4 },
            new object[] { 59, "天月", 13, 4 },
            new object[] { 60, "阴煞", 13, 4 },
            new object[] { 61, "天厨", 13, 4 },
            new object[] { 62, "天空", 7, 4 },
            new object[] { 63, "年解", 13, 4 },
            new object[] { 64, "咸池", 10, 4 },
            new object[] { 65, "劫煞", 13, 4 },
            new object[] { 66, "月德", 13, 4 },
            new object[] { 67, "长生", 15, 8 },
            new object[] { 68, "沐浴", 15, 8 },
            new object[] { 69, "冠带", 15, 8 },
            new object[] { 70, "临官", 15, 8 },
            new object[] { 71, "帝旺", 15, 8 },
            new object[] { 72, "衰", 15, 8 },
            new object[] { 73, "病", 15, 8 },
            new object[] { 74, "死", 15, 8 },
            new object[] { 75, "墓", 15, 8 },
            new object[] { 76, "绝", 15, 8 },
            new object[] { 77, "胎", 15, 8 },
            new object[] { 78, "养", 15, 8 },
            new object[] { 79, "博士", 13, 5 },
            new object[] { 80, "力士", 13, 5 },
            new object[] { 81, "青龙", 13, 5 },
            new object[] { 82, "小耗", 13, 5 },
            new object[] { 83, "将军", 13, 5 },
            new object[] { 84, "奏书", 13, 5 },
            new object[] { 85, "蜚廉", 13, 5 },
            new object[] { 86, "喜神", 13, 5 },
            new object[] { 87, "病符", 13, 5 },
            new object[] { 88, "大耗", 13, 5 },
            new object[] { 89, "伏兵", 13, 5 },
            new object[] { 90, "官符", 13, 5 },
            new object[] { 91, "将星", 13, 5 },
            new object[] { 92, "攀鞍", 13, 5 },
            new object[] { 93, "岁驿", 13, 5 },
            new object[] { 94, "息神", 13, 5 },
            new object[] { 95, "华盖", 13, 5 },
            new object[] { 96, "劫煞", 13, 5 },
            new object[] { 97, "灾煞", 13, 5 },
            new object[] { 98, "天煞", 13, 5 },
            new object[] { 99, "指背", 13, 5 },
            new object[] { 100, "咸池", 10, 5 },
            new object[] { 101, "月煞", 13, 5 },
            new object[] { 102, "亡神", 13, 5 },
            new object[] { 103, "岁建", 13, 5 },
            new object[] { 104, "晦气", 13, 5 },
            new object[] { 105, "丧门", 13, 5 },
            new object[] { 106, "贯索", 13, 5 },
            new object[] { 107, "官符", 13, 5 },
            new object[] { 108, "小耗", 13, 5 },
            new object[] { 109, "岁破", 13, 5 },
            new object[] { 110, "龙德", 13, 5 },
            new object[] { 111, "白虎", 13, 5 },
            new object[] { 112, "天德", 13, 5 },
            new object[] { 113, "吊客", 13, 5 },
            new object[] { 114, "病符", 13, 5 },
            new object[] { 115, "正截空", 7, 4 },
            new object[] { 116, "傍截空", 7, 4 },
            new object[] { 117, "正旬空", 7, 4 },
            new object[] { 118, "傍旬空", 7, 4 },
            new object[] { 119, "流昌", 16, 6 },
            new object[] { 120, "流曲", 16, 6 },
            new object[] { 121, "流魁", 16, 6 },
            new object[] { 122, "流钺", 16, 6 },
            new object[] { 123, "流禄", 16, 6 },
            new object[] { 124, "流马", 16, 6 },
            new object[] { 125, "流羊", 16, 6 },
            new object[] { 126, "流陀", 16, 6 },
            new object[] { 127, "流化禄", 16, 0 },
            new object[] { 128, "流化权", 16, 0 },
            new object[] { 129, "流化科", 16, 0 },
            new object[] { 130, "流化忌", 16, 0 },
            new object[] { 131, "大耗", 17, 4 }
        };
    }
}
