using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace DouNamespace
{
    public class UserList
    {
        public static string fileName = "users.xml";
        public static string filepath = Directory.GetCurrentDirectory() + "\\" + fileName;

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public static List<User> FindAll()
        {
            try
            {
                CheckFile(filepath);

                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(filepath);

                List<User> users = new List<User>();
                XmlNodeList topM = xmldoc.DocumentElement.ChildNodes;
                foreach (XmlElement element in topM)
                {
                    User usr = new User();
                    usr.Id = Convert.ToInt32(element.Attributes["id"].Value);
                    usr.Name = element.Attributes["name"].Value;
                    usr.Sex = Convert.ToInt32(element.Attributes["sex"].Value);
                    usr.Year = Convert.ToInt32(element.Attributes["year"].Value);
                    usr.Month = Convert.ToInt32(element.Attributes["month"].Value);
                    usr.Day = Convert.ToInt32(element.Attributes["day"].Value);
                    usr.Hour = Convert.ToInt32(element.Attributes["hour"].Value);
                    usr.Minute = Convert.ToInt32(element.Attributes["minute"].Value);
                    users.Add(usr);

                }

                return users;
            }
            catch (Exception ex)
            {
                //throw ex;
                return new List<User>();
            }

        }

        /// <summary>
        /// 添加新用户
        /// </summary>
        /// <param name="usr"></param>
        public static void Add(User usr)
        {
            try
            {
                CheckFile(filepath);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(filepath);
                XmlElement newUsr = xmldoc.CreateElement("user");
                newUsr.SetAttribute("id", Convert.ToString(usr.Id));
                newUsr.SetAttribute("name", Convert.ToString(usr.Name));
                newUsr.SetAttribute("sex", Convert.ToString(usr.Sex));
                newUsr.SetAttribute("year", Convert.ToString(usr.Year));
                newUsr.SetAttribute("month", Convert.ToString(usr.Month));
                newUsr.SetAttribute("day", Convert.ToString(usr.Day));
                newUsr.SetAttribute("hour", Convert.ToString(usr.Hour));
                newUsr.SetAttribute("minute", Convert.ToString(usr.Minute));
                xmldoc.DocumentElement.AppendChild(newUsr);
                xmldoc.Save(filepath);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="uid"></param>
        public static void Delete(int uid)
        {
            try
            {
                CheckFile(filepath);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(filepath);

                XmlNode xmlNode;
                xmlNode = xmldoc.SelectSingleNode("Users/user[@id='" + uid.ToString() + "']");
                xmldoc.DocumentElement.RemoveChild(xmlNode);
                xmldoc.Save(filepath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 通过用户名获取用户对象
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static User FindByName(string userName)
        {
            try
            {
                User usr = null;
                CheckFile(filepath);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(filepath);

                XmlNode xmlNode;
                xmlNode = xmldoc.SelectSingleNode("Users/user[@name='" + userName + "']");
                if (xmlNode != null)
                {
                    usr = new User();
                    usr.Id = Convert.ToInt32(xmlNode.Attributes["id"].Value);
                    usr.Name = xmlNode.Attributes["name"].Value;
                    usr.Sex = Convert.ToInt32(xmlNode.Attributes["sex"].Value);
                    usr.Year = Convert.ToInt32(xmlNode.Attributes["year"].Value);
                    usr.Month = Convert.ToInt32(xmlNode.Attributes["month"].Value);
                    usr.Day = Convert.ToInt32(xmlNode.Attributes["day"].Value);
                    usr.Hour = Convert.ToInt32(xmlNode.Attributes["hour"].Value);
                    usr.Minute = Convert.ToInt32(xmlNode.Attributes["minute"].Value);
                }
                return usr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 用户是否存在
        /// </summary>
        /// <param name="usr"></param>
        /// <returns></returns>
        public static bool IsExist(User usr)
        {
            try
            {
                bool result = false;

                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(filepath);

                XmlNode xmlNode;
                xmlNode = xmldoc.SelectSingleNode("Users/user[@name='" + usr.Name + "']");
                if (xmlNode != null) result = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  获取独有用户ID
        /// </summary>
        /// <returns></returns>
        public static int GenerateUserID()
        {
            try
            {
                int uid = 0;
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(filepath);

                XmlNodeList xmlNodeLst;
                xmlNodeLst = xmldoc.SelectSingleNode("Users").ChildNodes;

                List<int> uidList = new List<int>();
                if (xmlNodeLst.Count > 0)
                {
                    for (int i = 0; i < xmlNodeLst.Count; i++)
                    {
                        uid = Convert.ToInt32(xmlNodeLst.Item(i).Attributes["id"].Value);
                        uidList.Add(uid);
                    }

                    uid = uidList.Max() + 1;
                }

                return uid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 检查用户列表xml文件
        /// </summary>
        /// <param name="filepath"></param>
        private static void CheckFile(string filepath)
        {
            try
            {
                if (!File.Exists(filepath))
                {
                    // File.Create(filepath);

                    XmlTextWriter oXML = new XmlTextWriter(filepath, Encoding.UTF8);
                    oXML.Formatting = Formatting.None;
                    oXML.WriteStartDocument();
                    oXML.WriteStartElement("Users");
                    oXML.WriteEndElement();
                    oXML.Flush();
                    oXML.Close();

                    //XmlDocument xmldoc = new XmlDocument();
                    //xmldoc.Load(filepath);
                    //XmlElement newUsr = xmldoc.CreateElement("user");
                    //newUsr.SetAttribute("id","0");
                    //newUsr.SetAttribute("name", "Test");
                    //newUsr.SetAttribute("sex", "1");
                    //newUsr.SetAttribute("year","2000");
                    //newUsr.SetAttribute("month","10");
                    //newUsr.SetAttribute("day", "10");
                    //xmldoc.Save(filepath);
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
