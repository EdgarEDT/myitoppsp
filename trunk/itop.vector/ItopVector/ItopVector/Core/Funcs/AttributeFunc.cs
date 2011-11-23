namespace ItopVector.Core.Func
{
    using ItopVector.Core;
    using ItopVector.Core.Document;
    using ItopVector.Core.Figure;
    using ItopVector.Core.Interface.Figure;
    using System;
    using System.Collections;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class AttributeFunc
    {
        // Methods
        static AttributeFunc()
        {
            string[] textArray1 = new string[10] { "fill", "stroke", "fill-opacity", "stroke-opacity", "stroke-width", "stroke-dashoffset", "stroke-dasharray", "stroke-miterlimit","hatch-color","hatch-style" } ;
            AttributeFunc.StyleAttributes = new ArrayList(textArray1);
        }

        public AttributeFunc()
        {
        }

        public static object FindAttribute(string key, SvgElement node)
        {
            if (node.SvgAnimAttributes.ContainsKey(key))
            {
                return node.SvgAnimAttributes[key];
            }
            if ((node is Text) && ((Text) node).OwnerTextElement.SvgAnimAttributes.ContainsKey(key))
            {
                return ((Text) node).OwnerTextElement.SvgAnimAttributes[key];
            }
            return string.Empty;
        }

        public static object GetDefaultValue(SvgElement element, string attributename)
        {
            string text1;
            DomType type1 = DomTypeFunc.GetTypeOfAttributeName(attributename);
            if ((text1 = attributename) != null)
            {
                text1 = string.IsInterned(text1);
                if (((text1 == "fill-opacity") || (text1 == "opacity")) || (((text1 == "stroke-opacity") || (text1 == "stroke-width")) || (text1 == "stop-opacity")))
                {
                    return 1f;
                }
            }
            switch (type1)
            {
                case DomType.SvgNumber:
                {
                    return 0f;
                }
                case DomType.SvgString:
                {
                    return string.Empty;
                }
                case DomType.SvgColor:
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }

        public static string GetPropertyStringFromCSS(XmlElement node, string propertyname)
        {
            string text1 = string.Empty;
            string text2 = ";+" + propertyname + @"\s*:\s*[^;]*\s*;+";
            Regex regex1 = new Regex(text2, RegexOptions.IgnoreCase);
            string text3 = string.Empty;
            XmlAttribute attribute1 = node.Attributes["style"];
            if (attribute1 != null)
            {
                text1 = attribute1.Value.Trim();
                Match match1 = regex1.Match(";" + text1 + ";");
                if (match1.Success)
                {
                    char[] chArray1 = new char[1] { ':' } ;
                    string[] textArray1 = match1.Value.Split(chArray1);
                    text3 = textArray1[textArray1.Length - 1].Trim();
                }
            }
            return text3;
        }

        public static object ParseAttribute(string key, SvgElement node, bool isadd)
        {
            if (node == null)
            {
                return null;
            }
            if (key == "href")
            {
                key = "xlink:href";
            }
            object obj1 = null;
            for (XmlNode node1 = node; node1 is SvgElement; node1 = node1.ParentNode)
            {
                obj1 = AttributeFunc.FindAttribute(key, (SvgElement) node1);
                if ((obj1 != null) && (obj1.ToString() != string.Empty))
                {
                    break;
                }
                if (((SvgElement) node1).UseElement != null)
                {
                    node1 = ((SvgElement) node1).UseElement;
                    continue;
                }
            }
            if (obj1 == null)
            {
                return string.Empty;
            }
            return obj1;
        }

        public static void SetAttributeValue(XmlElement node, string attributename, string attributevalue)
        {
            if (node is SvgElement)
            {
                ((SvgElement) node).ParseAttribute(attributename, attributevalue, true);
            }
            if (AttributeFunc.StyleAttributes.Contains(attributename.Trim().ToLower()) && (node is IGraph))
            {
				
                XmlAttribute attribute1 = node.Attributes["style"];
                string text1 = attributename + ":" + attributevalue + ";";
                if (attribute1 != null)
                {
                    string text2 = attribute1.Value.Trim();
                    Regex regex1 = new Regex(@"(;|\s)+" + attributename + @"\s*:\s*\S*\s*(;|\s)+", RegexOptions.IgnoreCase);
                    Match match1 = regex1.Match(" " + text2.Replace(";", " ") + " ");
                    if (match1.Success)
                    {
                        string text3 = match1.Value.Trim() + ";";
                        text2 = " " + text2 + " ";
                        text2 = text2.Substring(0, match1.Index + 1) + text1 + text2.Substring(match1.Index + match1.Length, (text2.Length - match1.Index) - match1.Length);
                    }
                    else
                    {
                        text2 = text2 + text1;
                    }
					if (node is SvgElement)
					{						
						(node as SvgElement).BeforeChangeValueStr = attribute1.Value;
					}
                    attribute1.Value = text2.Trim();
                }
                else
                {
					
                    node.SetAttribute("style", text1);
                }
				((IGraph)node).NotifyChange();
				
            }
            else
            {
                SvgDocument document1 = (SvgDocument) node.OwnerDocument;
                XmlAttribute attribute2 = node.Attributes[attributename];
                SvgElement element1 = null;
                if (node is SvgElement)
                {
                    element1 = (SvgElement) node;
                    document1.ChangeElements.Add(element1);
                }
                try
                {
                    if (attribute2 == null)
                    {
                        bool flag1 = document1.AcceptChanges;
                        document1.AcceptChanges = false;
                        string text4 = document1.Prefix;
                        string text5 = document1.NamespaceURI;
                        string text6 = attributename;
                        if ((attributename == "xlink:href") || (attributename == "href"))
                        {
                            text4 = "xlink";
                            text6 = "href";
                            text5 = SvgDocument.XLinkNamespace;
                        }
						
                        attribute2 = document1.CreateAttribute(text4, text6, text5);
                        attribute2.Value = attributevalue;
                        document1.AcceptChanges = flag1;
                        node.SetAttributeNode(attribute2);
                    }
                    else
                    {
                        if (element1 != null)
                        {
                            string text7 = attribute2.Value;
                            element1.BeforeChangeValueStr = text7;
                        }
                        element1.SetAttribute(attribute2.Name, attributevalue);
                    }
					if(node.ParentNode!=null)
					{		
						((IGraph)node).NotifyChange();
					}

                }
                catch (Exception exception1)
                {
                    throw exception1;
                }

            }
			
        }


        // Fields
        public static ArrayList StyleAttributes;
    }
}

