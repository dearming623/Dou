using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace DouNamespace
{
    public class StarList
    {

        public static StarList Instance { get; } = new StarList();

        public static List<Star> doustars;

        public static void Load()
        {
            try
            {
                string fileName;
                fileName = Directory.GetCurrentDirectory() + "\\doustars.xml";
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(fileName);

                doustars = new List<Star>();
                XmlNodeList topM = xmldoc.DocumentElement.ChildNodes;
                foreach (XmlElement element in topM)
                {
                    Star newStar = new Star();
                    newStar.ID = Convert.ToInt32(element.Attributes["id"].Value);
                    newStar.Name = element.Attributes["name"].Value;
                    newStar.type = Convert.ToInt32(element.Attributes["type"].Value);
                    newStar.position = Convert.ToInt32(element.Attributes["position"].Value);
                    newStar.Level = 0;
                    doustars.Add(newStar);
                }
            }
            catch (Exception ex)
            {

                throw ex;
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
            return doustars;
        }


    }
}

