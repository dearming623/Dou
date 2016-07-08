using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Data.SQLite;
using DouNamespace.DBUtility;

namespace DouNamespace.DAL
{
    /// <summary>
    /// 产品类
    /// </summary>
    public class UserDAL
    {
        //在这里可以更换数据库,支持多数据库，支持采用加密方式实现
        //DbHelperSQLP DbHelperSQL = new DbHelperSQLP(PubConstant.GetConnectionString("ConnectionString2"));

        #region  基本成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ProductId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserInfo");
            strSql.Append(" where ID= @Id");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@Id", DbType.Int32) 
				};
            parameters[0].Value = ProductId;
            return DbHelperSQLite.Exists(strSql.ToString(), parameters);
        }

         public static bool Exists(string strSql, params SQLiteParameter[] cmdParms)
        {
            object obj = DbHelperSQLite.GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
      


        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public void Add(Maticsoft.Model.Product model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("insert into P_Product(");
        //    strSql.Append("ProductId,BrandId,CategoryId,Name,Descn,Image,ImageSmall,Price,VipPrice,Cheapness)");
        //    strSql.Append(" values (");
        //    strSql.Append("@ProductId,@BrandId,@CategoryId,@Name,@Descn,@Image,@ImageSmall,@Price,@VipPrice,@Cheapness)");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ProductId", SqlDbType.VarChar,20),
        //            new SqlParameter("@BrandId", SqlDbType.VarChar,20),
        //        new SqlParameter("@CategoryId", SqlDbType.VarChar,20),
        //            new SqlParameter("@Name", SqlDbType.VarChar,80),
        //            new SqlParameter("@Descn",SqlDbType.Text),
        //            new SqlParameter("@Image", SqlDbType.VarChar,150),
        //        new SqlParameter("@ImageSmall", SqlDbType.VarChar,150),
        //            new SqlParameter("@Price", SqlDbType.Money,8),
        //    new SqlParameter("@VipPrice", SqlDbType.Money,8),
        //        new SqlParameter("@Cheapness", SqlDbType.Int)
        //    };
        //    parameters[0].Value = model.ProductId;
        //    parameters[1].Value = model.BrandId;
        //    parameters[2].Value = model.CategoryId;
        //    parameters[3].Value = model.Name;
        //    parameters[4].Value = model.Descn;
        //    parameters[5].Value = model.Image;
        //    parameters[6].Value = model.ImageSmall;
        //    parameters[7].Value = model.Price;
        //    parameters[8].Value = model.VipPrice;
        //    parameters[9].Value = model.Cheapness;

        //    DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        //}
        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public void Update(Maticsoft.Model.Product model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("update P_Product set ");
        //    strSql.Append("BrandId=@BrandId,");
        //    strSql.Append("CategoryId=@CategoryId,");
        //    strSql.Append("Name=@Name,");
        //    strSql.Append("Descn=@Descn,");
        //    strSql.Append("Image=@Image,");
        //    strSql.Append("ImageSmall=@ImageSmall,");
        //    strSql.Append("Price=@Price,");
        //    strSql.Append("VipPrice=@VipPrice,");
        //    strSql.Append("Cheapness=@Cheapness");
        //    strSql.Append(" where ProductId=@ProductId");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@BrandId", SqlDbType.VarChar,20),
        //            new SqlParameter("@CategoryId", SqlDbType.VarChar,20),
        //            new SqlParameter("@ProductId", SqlDbType.VarChar,20),
        //            new SqlParameter("@Name", SqlDbType.VarChar,80),
        //            new SqlParameter("@Descn", SqlDbType.Text),
        //            new SqlParameter("@Image", SqlDbType.VarChar,150),
        //            new SqlParameter("@ImageSmall", SqlDbType.VarChar,150),
        //            new SqlParameter("@Price", SqlDbType.Money,8),
        //            new SqlParameter("@VipPrice", SqlDbType.Money,8),
        //        new SqlParameter("@Cheapness", SqlDbType.Int)
            
        //    };
        //    parameters[0].Value = model.BrandId;
        //    parameters[1].Value = model.CategoryId;
        //    parameters[2].Value = model.ProductId;
        //    parameters[3].Value = model.Name;
        //    parameters[4].Value = model.Descn;
        //    parameters[5].Value = model.Image;
        //    parameters[6].Value = model.ImageSmall;
        //    parameters[7].Value = model.Price;
        //    parameters[8].Value = model.VipPrice;
        //    parameters[9].Value = model.Cheapness;

        //    DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        //}

        ///// <summary>
        ///// 删除一条数据
        ///// </summary>
        //public void Delete(string ProductId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("delete P_Product ");
        //    strSql.Append(" where ProductId=@ProductId");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ProductId", SqlDbType.VarChar)
        //        };
        //    parameters[0].Value = ProductId;
        //    DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        //}

      
        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public Maticsoft.Model.Product GetModel(string ProductId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select * from P_Product ");
        //    strSql.Append(" where ProductId=@ProductId");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ProductId", SqlDbType.VarChar)};
        //    parameters[0].Value = ProductId;
        //    Maticsoft.Model.Product model = new Maticsoft.Model.Product();
        //    DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
        //    model.ProductId = ProductId;
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
        //        {
        //            //model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
        //        }
        //        model.BrandId = ds.Tables[0].Rows[0]["BrandId"].ToString();
        //        model.CategoryId = ds.Tables[0].Rows[0]["CategoryId"].ToString();
        //        model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
        //        model.Descn = ds.Tables[0].Rows[0]["Descn"].ToString();
        //        model.Image = ds.Tables[0].Rows[0]["Image"].ToString();
        //        model.ImageSmall = ds.Tables[0].Rows[0]["ImageSmall"].ToString();
        //        if (ds.Tables[0].Rows[0]["Price"].ToString() != "")
        //        {
        //            model.Price = decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
        //        }
        //        if (ds.Tables[0].Rows[0]["VipPrice"].ToString() != "")
        //        {
        //            model.VipPrice = decimal.Parse(ds.Tables[0].Rows[0]["VipPrice"].ToString());
        //        }
        //        if (ds.Tables[0].Rows[0]["Cheapness"].ToString() != "")
        //        {
        //            model.Cheapness = int.Parse(ds.Tables[0].Rows[0]["Cheapness"].ToString());
        //        }
        //        return model;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //#endregion

        //#region  查询

        ///// <summary>
        ///// 获得记录总数
        ///// </summary>
        //public int GetCount(string strWhere)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select count(1) ");
        //    strSql.Append(" FROM P_Product ");
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" where " + strWhere);
        //    }
        //    object obj = DbHelperSQL.GetSingle(strSql.ToString());
        //    if (obj == null)
        //    {
        //        return 1;
        //    }
        //    else
        //    {
        //        return int.Parse(obj.ToString());
        //    }
        //}
        
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        //public DataSet GetList(string strWhere)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select * ");
        //    strSql.Append(" FROM P_Product ");
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" where " + strWhere);
        //    }
        //    strSql.Append(" order by ID desc");
        //    return DbHelperSQL.Query(strSql.ToString());
        //}
                        
        ///// <summary>
        ///// 得到列表
        ///// </summary>
        ///// <param name="top">返回的行数，-1为无限制</param>
        ///// <param name="strWhere">条件</param>
        ///// <param name="FiledOrder">排序字段</param>
        ///// <returns></returns>
        //public DataSet GetList(int top, string strWhere, string FiledOrder)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select ");
        //    if (top > 0)
        //    {
        //        strSql.Append(" top " + top.ToString());
        //    }
        //    strSql.Append(" * ");
        //    strSql.Append(" from P_Product ");

        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" where " + strWhere);
        //    }
        //    if (FiledOrder.Trim() != "")
        //    {
        //        strSql.Append(" order by " + FiledOrder);
        //    }
        //    return DbHelperSQL.Query(strSql.ToString());
        //}
        
        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public DataSet GetList(int PageSize, int PageIndex, string strWhere)
        //{
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@tblName", SqlDbType.VarChar, 255),
        //            new SqlParameter("@fldName", SqlDbType.VarChar, 255),
        //            new SqlParameter("@PageSize", SqlDbType.Int),
        //            new SqlParameter("@PageIndex", SqlDbType.Int),
        //            new SqlParameter("@IsReCount", SqlDbType.Bit),
        //            new SqlParameter("@OrderType", SqlDbType.Bit),
        //            new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
        //            };
        //    parameters[0].Value = "P_Product";
        //    parameters[1].Value = "Id";
        //    parameters[2].Value = PageSize;
        //    parameters[3].Value = PageIndex;
        //    parameters[4].Value = 0;
        //    parameters[5].Value = 0;
        //    parameters[6].Value = strWhere;
        //    return DbHelperSQL.RunProcedure("UP_GetRecordByPage", parameters, "ds");
        //}

        //#endregion

        //#region Linq查询
        ///// <summary>
        ///// 查询表对象的集合
        ///// </summary>
        ///// <returns></returns>
        //public Table<Maticsoft.Model.Product> GetTableModelList()
        //{
        //    string connectionString = PubConstant.GetConnectionString("ConnectionString");
        //    Maticsoft.Model.CodematicDataContext db = new Maticsoft.Model.CodematicDataContext(connectionString);
        //    Table<Maticsoft.Model.Product> modellist = db.GetTable<Maticsoft.Model.Product>();
        //    return modellist; 
        //}
        ///// <summary>
        ///// 查询条件对象的集合
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<Maticsoft.Model.Product> GetQueryModelList()
        //{
        //    string connectionString = PubConstant.GetConnectionString("ConnectionString");
        //    Maticsoft.Model.CodematicDataContext db = new Maticsoft.Model.CodematicDataContext(connectionString);
            
        //    //创建查询
        //    return from model in db.Product
        //           //where model.CategoryId == "1"
        //           orderby model.ProductId descending
        //           select model;
        //}
        ///// <summary>
        ///// 直接执行 SQL 查询 (LINQ to SQL)
        ///// </summary>
        ///// <param name="strWhere"></param>
        ///// <returns></returns>
        //public IEnumerable<Maticsoft.Model.Product> GetQueryModelList(string strWhere)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select * ");
        //    strSql.Append(" FROM P_Product ");
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" where " + strWhere);
        //    }
        //    strSql.Append(" order by ID desc");
        //    string connectionString = PubConstant.GetConnectionString("ConnectionString");
        //    DataContext db = new DataContext(connectionString);            
        //    return db.ExecuteQuery<Maticsoft.Model.Product>(strSql.ToString());
        //}


        #endregion

       

    }
}
