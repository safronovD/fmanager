using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FileManagerWithProfiles
{
    static class Util
    {
        static public void initXMLComponents (ref XmlDocument xDoc, ref XmlNode userNode)
        {
            xDoc = new XmlDocument();
            xDoc.Load(Properties.Settings.Default.xmlPath);

            XmlElement xRoot = xDoc.DocumentElement;

            List<XmlNode> list = xRoot.ChildNodes.Cast<XmlNode>()
                           .Where(user => user["login"].InnerText.Equals(Properties.Settings.Default.userName))
                           .ToList();

            if (list.Count != 1)
            {
                throw new ArgumentException("Users.xml is corrupted!", "XmlDocument");
            }

            userNode = list[0];
        }
    }
}
