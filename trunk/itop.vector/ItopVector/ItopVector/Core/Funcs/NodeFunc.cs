namespace ItopVector.Core.Func
{
    using ItopVector.Core;
    using ItopVector.Core.Document;
    using System;
    using System.Collections;
    using System.Xml;

    public class NodeFunc
    {
        // Methods
        public NodeFunc()
        {
        }

        public static bool AddEvent(SvgDocument doc, string eventname)
        {
            int num1 = CodeFunc.GetIndexOfString(doc.OuterXml, "function", eventname);
            if (num1 >= 0)
            {
                return false;
            }
            XmlNode node1 = doc.DocumentElement.SelectSingleNode("*[name()='script']");
            XmlElement element1 = doc.CreateElement(doc.Prefix, "script", doc.NamespaceURI);
            if (node1 is XmlElement)
            {
                element1 = (XmlElement) node1;
            }
            element1.SetAttribute("type", doc.NamespaceURI, "text/ecmascript");
            string text1 = string.Empty + "\n\tfunction " + eventname + "\n\t{\n\t\t\n\t}\n\t";
            XmlNode node2 = element1.FirstChild;
            XmlCDataSection section1 = null;
        Label_0092:
            if (node2 is XmlCDataSection)
            {
                section1 = (XmlCDataSection) node2;
            }
            else if (node2 != null)
            {
                node2 = node2.NextSibling;
                goto Label_0092;
            }
            if (section1 == null)
            {
                section1 = doc.CreateCDataSection(text1);
                element1.AppendChild(doc.CreateWhitespace("\n\t"));
                element1.AppendChild(section1);
                element1.AppendChild(doc.CreateWhitespace("\n\t"));
            }
            else
            {
                XmlNode node3 = section1.LastChild;
                section1.Value = section1.Value + text1;
            }
            if (element1.ParentNode == null)
            {
                doc.DocumentElement.PrependChild(element1);
            }
            return true;
        }

        public static XmlNode[] GetPreWhitespaceForNode(XmlNode node)
        {
            ArrayList list1 = new ArrayList(0x10);
            for (XmlNode node1 = node.PreviousSibling; (node1 is XmlWhitespace) || (node1 is XmlSignificantWhitespace); node1 = node1.PreviousSibling)
            {
                if ((node1.Value != "\t") && (node1.Value != " "))
                {
                    break;
                }
                list1.Add(node1.Clone());
            }
            XmlNode[] nodeArray1 = new XmlNode[list1.Count];
            list1.CopyTo(nodeArray1);
            return nodeArray1;
        }

        public static XmlNode GetRefNode(string refid, SvgDocument doc)
        {
            refid = refid.Trim();
            while (refid.EndsWith(";"))
            {
                refid = refid.Substring(0, refid.Length - 1);
            }
            string text1 = refid;
            if (text1.Trim().StartsWith("url"))
            {
                int num1 = text1.Trim().IndexOf("#", 0, text1.Trim().Length);
                int num2 = text1.Trim().IndexOf(")", 0, text1.Trim().Length);
                text1 = text1.Trim().Substring(num1 + 1, (num2 - num1) - 1);
            }
            if (text1.Trim().StartsWith("#"))
            {
                text1 = text1.Trim().Substring(1, text1.Trim().Length - 1);
            }
            string text2 = "//*[@id='" + text1 + "']";
            bool flag1 = doc.AcceptChanges;
            doc.AcceptChanges = false;
            XmlNode node1 = doc.SelectSingleNode(text2);
            doc.AcceptChanges = flag1;
            return node1;
        }

        public static XmlNode GetRefNode(string refid, SvgElement element)
        {
            refid = refid.Trim();
            while (refid.EndsWith(";"))
            {
                refid = refid.Substring(0, refid.Length - 1);
            }
            string text1 = refid;
            if (text1.Trim().StartsWith("url"))
            {
                int num1 = text1.Trim().IndexOf("#", 0, text1.Trim().Length);
                int num2 = text1.Trim().IndexOf(")", 0, text1.Trim().Length);
                text1 = text1.Trim().Substring(num1 + 1, (num2 - num1) - 1);
            }
            if (text1.Trim().StartsWith("#"))
            {
                text1 = text1.Trim().Substring(1, text1.Trim().Length - 1);
            }
            if (element.ID == text1)
            {
                return element;
            }
            string text2 = "//*[@id=\"" + text1 + "\"]";
            bool flag1 = element.OwnerDocument.AcceptChanges;
            element.OwnerDocument.AcceptChanges = false;
            XmlNode node1 = element.SelectSingleNode(text2);
            element.OwnerDocument.AcceptChanges = flag1;
            return node1;
        }

    }
}

