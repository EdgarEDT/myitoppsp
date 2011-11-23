namespace ItopVector.Core.Func
{
    using ItopVector.Core;
    using ItopVector.Core.Document;
    using ItopVector.Core.Figure;
    using System;
    using System.Collections;
    using System.Text.RegularExpressions;
    using System.Xml;
	using System.Text;

    public class CodeFunc
    {
		static Random random;
        // Methods
        public CodeFunc()
        {

        }
		static CodeFunc()
		{
			random=new Random();
			
		}

        public static string CreateID()
        {
            long num1 = DateTime.Now.ToFileTime();
            return num1.ToString();
        }

        public static string CreateString(SvgDocument doc, string key)
        {
//            int num1 = 0;
            Regex regex1 = new Regex("[A-Za-z]*");
            Match match1 = regex1.Match(key);
            if (match1.Success)
            {
                key = match1.Groups[0].Value;
            }
			while (true)
			{
//				num1=random.Next(0,99999);
				string text1 = key + Guid.NewGuid().ToString().Substring(24);
               
//				if (NodeFunc.GetRefNode(text1, doc) == null)
				{
					return text1;
				}
			}
        }

        public static string CreateString(SvgElement element, string key)
        {
            string text1;
//            int num1 = 0;
            Regex regex1 = new Regex("[A-Za-z]*");
            Match match1 = regex1.Match(key);
            if (match1.Success)
            {
                key = match1.Groups[0].Value;
               
            }
			while (true)
			{
//				num1=random.Next(0,99999);
				text1 = key + Guid.NewGuid().ToString().Substring(24);
            
//				if ( (NodeFunc.GetRefNode(text1, element.OwnerDocument) == null))
				{
					return text1;
				}
			}
          
        }

        public static void FormatElement(SvgElement element)
        {
            if (element == null)
            {
                return;
            }
            XmlNode node1 = element;
            while ((node1.ParentNode != null) && !(node1.ParentNode is XmlDocument))
            {
                node1 = node1.ParentNode;
            }
            if (node1 != element.OwnerDocument.DocumentElement)
            {
                return;
            }
            SvgDocument document1 = element.OwnerDocument;
            bool flag1 = document1.AcceptChanges;
            document1.AcceptChanges = false;
            int num1 = element.NodeDepth;
            XmlNode node2 = element.PreviousSibling;
            while (((node2 != null) && !(node2 is SvgElement)) && (!(element.ParentNode is Text) || !(node2 is XmlText)))
            {
                XmlNode node3 = node2.PreviousSibling;
                element.ParentNode.RemoveChild(node2);
                node2 = node3;
            }
            node2 = element.PreviousSibling;
			
            XmlWhitespace whitespace1 = document1.CreateWhitespace("\n");
            if (node2 == null)
            {
                element.ParentNode.PrependChild(whitespace1);
            }
            else
            {
                element.ParentNode.InsertAfter(whitespace1, node2);
            }
            node2 = whitespace1;
            for (int num2 = 0; num2 < num1; num2++)
            {
                whitespace1 = document1.CreateWhitespace("\t");
                element.ParentNode.InsertAfter(whitespace1, node2);
                node2 = whitespace1;
            }
            bool flag2 = false;
            node2 = element.NextSibling;
        Label_0122:
            if (node2 is XmlWhitespace )
            {
                if (((XmlWhitespace) node2).Value.IndexOf("\n") >= 0)
                {
                    flag2 = true;
                }
                else
                {
                    node2 = node2.NextSibling;
                    goto Label_0122;
                }
            }
            if (!flag2)
            {
                whitespace1 = document1.CreateWhitespace("\n");
                element.ParentNode.InsertAfter(whitespace1, element);
            }
			
            if (whitespace1 == element.ParentNode.LastChild )
            {
                num1 = ((SvgElement) element.ParentNode).NodeDepth;
                node2 = element.ParentNode.LastChild;
                for (int num3 = 0; num3 < num1; num3++)
                {
                    whitespace1 = document1.CreateWhitespace("\t");
                    element.ParentNode.InsertAfter(whitespace1, node2);
                    node2 = whitespace1;
                }
            }

            document1.AcceptChanges = flag1;
            foreach (XmlNode node4 in element.ChildNodes)
            {
                if (node4 is SvgElement)
                {
                    CodeFunc.FormatElement((SvgElement) node4);
                }
            }
        }

        public static string FormatXmlDocumentString(XmlDocument doc)
        {
            string text1 = doc.OuterXml.Trim();
            StringBuilder text2 = new StringBuilder();
            bool flag1 = false;
            ArrayList list1 = new ArrayList(0x10);
            int num1 = 0;
            for (int num2 = 0; num2 < text1.Length; num2++)
            {
                if (text1[num2] == '"')
                {
                    flag1 = !flag1;
                }
                if (((text1[num2] == '<') && !flag1) && (num2 != num1))
                {
                    list1.Add(text2.ToString());
                    num1 = num2;
                    text2 = new StringBuilder();
                }
                if ((text1[num2] != '\r') && (text1[num2] != '\n'))
                {
                    char ch1 = text1[num2];
                    text2.Append(ch1);
                }
            }
            if (num1 != (text1.Length - 1))
            {
                list1.Add(text2.ToString());
            }
            StringBuilder text3 = new StringBuilder();
            int num3 = 0;
            for (int num4 = 0; num4 < list1.Count; num4++)
            {
                string text4 =(string)list1[num4];
                if (num4 == 0)
                {
                    text3.Append( text4.Trim() + "\n");
                }
                else
                {
                    string text5 = ((string)list1[num4 - 1]).Trim();
                    if (((!text5.Trim().EndsWith("?>") && !text5.Trim().StartsWith("</")) && (!text5.Trim().EndsWith("/>") && !text5.Trim().EndsWith("-->"))) && !text5.Trim().StartsWith("<!"))
                    {
                        num3++;
                    }
                    if (text4.Trim().StartsWith("</"))
                    {
                        num3--;
                    }
                    for (int num5 = 0; num5 < num3; num5++)
                    {
                        text3.Append( "\t");
                    }
                    text3.Append( text4.Trim() + "\n");
                }
            }
            return text3.ToString();
        }

        public static int GetIndexOfString(string oristring, string substring1, string substring2)
        {
            string text1 = substring2.Replace("(", @"\(").Replace(")", @"\)");
            string[] textArray1 = new string[5] { @"\s+", substring1, @"\s", text1, "[^a-zA-Z0-9]+" } ;
            string text2 = string.Concat(textArray1);
            Regex regex1 = new Regex(text2, RegexOptions.IgnoreCase);
            for (Match match1 = regex1.Match(oristring); match1.Success; match1 = match1.NextMatch())
            {
                string text3 = match1.Value;
                if (text3.IndexOf(substring2) >= 0)
                {
                    return (((match1.Index + text3.IndexOf(substring2)) + substring2.Length) + 1);
                }
            }
            return -1;
        }

    }
}

