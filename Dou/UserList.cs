using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using DouNamespace.DBUtility;

namespace DouNamespace
{
    /// <summary>
    /// 用户数据访问（SQLite User 表）。
    /// </summary>
    public class UserList
    {
        private const string SelectColumns = "SELECT Id, Name, Sex, Year, Month, Day, Hour, Minute, Phone, Mobile, Email, QQ, Notes, GroupName FROM User";

        private static User ReadUserFromRow(DataRow row)
        {
            User usr = new User();
            usr.Id = Convert.ToInt32(row["Id"]);
            usr.Name = Convert.ToString(row["Name"]);
            usr.Sex = Convert.ToInt32(row["Sex"]);
            usr.Year = Convert.ToInt32(row["Year"]);
            usr.Month = Convert.ToInt32(row["Month"]);
            usr.Day = Convert.ToInt32(row["Day"]);
            usr.Hour = Convert.ToInt32(row["Hour"]);
            usr.Minute = Convert.ToInt32(row["Minute"]);
            usr.Phone = Convert.ToString(row["Phone"]);
            usr.Mobile = Convert.ToString(row["Mobile"]);
            usr.Email = Convert.ToString(row["Email"]);
            usr.QQ = Convert.ToString(row["QQ"]);
            usr.Notes = Convert.ToString(row["Notes"]);
            usr.Group = Convert.ToString(row["GroupName"]);
            return usr;
        }

        private static SQLiteParameter[] BuildParameters(User usr)
        {
            return new SQLiteParameter[]
            {
                new SQLiteParameter("@Name", usr.Name),
                new SQLiteParameter("@Sex", usr.Sex),
                new SQLiteParameter("@Year", usr.Year),
                new SQLiteParameter("@Month", usr.Month),
                new SQLiteParameter("@Day", usr.Day),
                new SQLiteParameter("@Hour", usr.Hour),
                new SQLiteParameter("@Minute", usr.Minute),
                new SQLiteParameter("@Phone", usr.Phone ?? string.Empty),
                new SQLiteParameter("@Mobile", usr.Mobile ?? string.Empty),
                new SQLiteParameter("@Email", usr.Email ?? string.Empty),
                new SQLiteParameter("@QQ", usr.QQ ?? string.Empty),
                new SQLiteParameter("@Notes", usr.Notes ?? string.Empty),
                new SQLiteParameter("@GroupName", usr.Group ?? string.Empty)
            };
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        public static List<User> FindAll()
        {
            DataSet ds = DbHelperSQLite.Query(SelectColumns + " ORDER BY Id");
            List<User> users = new List<User>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                users.Add(ReadUserFromRow(row));
            }
            return users;
        }

        /// <summary>
        /// 添加新用户，成功后回填自增 Id
        /// </summary>
        public static void Add(User usr)
        {
            string sql = @"INSERT INTO User (Name, Sex, Year, Month, Day, Hour, Minute, Phone, Mobile, Email, QQ, Notes, GroupName)
VALUES (@Name, @Sex, @Year, @Month, @Day, @Hour, @Minute, @Phone, @Mobile, @Email, @QQ, @Notes, @GroupName);
SELECT last_insert_rowid();";
            object newId = DbHelperSQLite.GetSingle(sql, BuildParameters(usr));
            if (newId != null)
            {
                usr.Id = Convert.ToInt32(newId);
            }
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        public static void Update(User usr)
        {
            string sql = @"UPDATE User SET
Name=@Name, Sex=@Sex, Year=@Year, Month=@Month, Day=@Day, Hour=@Hour, Minute=@Minute,
Phone=@Phone, Mobile=@Mobile, Email=@Email, QQ=@QQ, Notes=@Notes, GroupName=@GroupName
WHERE Id=@Id";
            List<SQLiteParameter> parameters = new List<SQLiteParameter>(BuildParameters(usr));
            parameters.Add(new SQLiteParameter("@Id", usr.Id));
            int rows = DbHelperSQLite.ExecuteSql(sql, parameters.ToArray());
            if (rows == 0)
            {
                throw new Exception("用户不存在");
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        public static void Delete(int uid)
        {
            DbHelperSQLite.ExecuteSql("DELETE FROM User WHERE Id=@Id", new SQLiteParameter("@Id", uid));
        }

        /// <summary>
        /// 通过用户名获取用户对象
        /// </summary>
        public static User FindByName(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return null;

            DataSet ds = DbHelperSQLite.Query(SelectColumns + " WHERE Name=@Name LIMIT 1", new SQLiteParameter("@Name", userName));
            if (ds.Tables[0].Rows.Count == 0) return null;
            return ReadUserFromRow(ds.Tables[0].Rows[0]);
        }

        /// <summary>
        /// 通过ID获取用户对象
        /// </summary>
        public static User FindById(int id)
        {
            DataSet ds = DbHelperSQLite.Query(SelectColumns + " WHERE Id=@Id LIMIT 1", new SQLiteParameter("@Id", id));
            if (ds.Tables[0].Rows.Count == 0) return null;
            return ReadUserFromRow(ds.Tables[0].Rows[0]);
        }

        /// <summary>
        /// 用户是否存在（按姓名）
        /// </summary>
        public static bool IsExist(User usr)
        {
            return DbHelperSQLite.Exists("SELECT COUNT(1) FROM User WHERE Name=@Name", new SQLiteParameter("@Name", usr.Name));
        }
    }
}
