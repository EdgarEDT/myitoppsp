using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Collections;
using System.Data;

namespace Itop.Client.Stutistics
{
    public class Class1
    {
        static public void TreeNodeToDataObject<T>(T dataObject, DevExpress.XtraTreeList.Nodes.TreeListNode treeNode)
        {
            Type type = typeof(T);
            foreach (PropertyInfo pi in type.GetProperties())
            {
                pi.SetValue(dataObject, treeNode.GetValue(pi.Name), null);
            }
        }


        /// <summary>
        /// 通过ID获取元素值
        /// </summary>
        /// <param name="id">XML文档中定义的唯一ID属性</param>
        /// <param name="elementName">对应ID元素下要查找的元素名称</param>
        /// <returns></returns>
        public static string GetXmlElementValueById(string id, string elementName,string xmlpath)
        {
            string outPut = string.Empty;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlpath);

                XmlElement elem = doc.GetElementById(id);
                outPut = elem.Attributes[elementName].Value;
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
            }
            return outPut;
        }

        /// <summary>
        /// 获取XML文档中元素的值
        /// </summary>
        /// <param name="parentNodePath">父级节点位置，如RolesRoot/Roles</param>
        /// <param name="childNodeName">子节点名称，如要在Role节点下找相关元素值</param>
        /// <param name="matchElementName">要进行匹配的元素名称,如通过ID元素值来找匹配</param>
        /// <param name="id">ID元素值</param>
        /// <param name="elementName">需要获取的元素名称</param>
        /// <returns></returns>
        public static Hashtable GetXmlElementValue(string xmlpath)
        {
            Hashtable ht = new Hashtable();
            Hashtable ht1 = new Hashtable();
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(xmlpath);
                XmlNode nli = xd.DocumentElement.ChildNodes[1];
                foreach (XmlNode node in nli.ChildNodes)
                {
                    if (node.ChildNodes.Count > 1)
                        ht.Add(Convert.ToInt32(node.ChildNodes[2].InnerText),node.ChildNodes[1].InnerText);
                }
                


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

            ArrayList al = new ArrayList(ht.Keys);
            al.Sort();
            for (int i = 0; i < al.Count; i++)
            {
                object e = al[i];
                ht1.Add(Convert.ToInt32(e.ToString()), ht[e].ToString());
            }


            return ht1;
        }




        public static ArrayList GetXmlElement(string xmlpath)
        {
            Hashtable ht = new Hashtable();
            ArrayList al1 = new ArrayList();
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(xmlpath);
                XmlNode nli = xd.DocumentElement.ChildNodes[1];
                foreach (XmlNode node in nli.ChildNodes)
                {
                    if (node.ChildNodes.Count > 1)
                        ht.Add(Convert.ToInt32(node.ChildNodes[2].InnerText), node.ChildNodes[1].InnerText);
                }



            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

            ArrayList al = new ArrayList(ht.Keys);
            al.Sort();
            for (int i = 0; i < al.Count; i++)
            {
                object e = al[i];
                al1.Add(ht[e].ToString());
            }
            return al1;
        }

    }

    public class ChoosedYears
    { 
        public int Year = 0;
        public bool WithIncreaseRate = false;
    }
}
