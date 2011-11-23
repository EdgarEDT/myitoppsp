namespace ItopVector.Core.Document
{
    using ItopVector.Core.Config;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;

    public class SvgDocumentFactory
    {
        // Methods
        public SvgDocumentFactory()
        {
        }

        public static SvgDocument CreateDocument(SizeF size)
        {
            SvgDocument document1 = new SvgDocument();
            string[] textArray1 = new string[9];
            textArray1[0] = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
//            textArray1[1] = "\n<!--"+ItopVector.Core.Config.Config.GetLabelForName("createdstr")+"-->";
            textArray1[2] = "\n<svg id=\"svg\" width=\"";
            int num1 = (int) size.Width;
            textArray1[3] = num1.ToString();
            textArray1[4] = "\" height=\"";
            int num2 = (int) size.Height;
            textArray1[5] = num2.ToString();
            textArray1[6] = "\" xmlns:xlink=\""+SvgDocument.XLinkNamespace;
			textArray1[7] = "\" xmlns:tonli=\""+SvgDocument.TonliNamespace;
			textArray1[8] = "\" >\n</svg>";//xml:space=\"preserve\"
            string text1 = string.Concat(textArray1);
            document1.PreserveWhitespace = true;
            document1.LoadXml(text1);
            return document1;
        }
		public static SvgDocument CreateDocument()
		{
			SvgDocument document1 = new SvgDocument();
			return document1;
		}

        public static SvgDocument CreateDocumentFromFile(string filename)
        {
            Stream stream1 = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            SvgDocument document1 = new SvgDocument();
            XmlTextReader reader1 = new XmlTextReader(stream1, XmlNodeType.Document, document1.XmlParserContext);
            try
            {
                reader1.XmlResolver = null;
                document1.PreserveWhitespace = true;
                document1.XmlResolver = null;
                document1.FilePath = filename;
                document1.Load(reader1);
                //document1.DealLast();
                reader1.Close();
                stream1.Close();
                document1.FileName = Path.GetFileNameWithoutExtension(filename);
                document1.Update = true;
                return document1;
            }
            catch (Exception exception1)
            {
                reader1.Close();
                stream1.Close();
                MessageBox.Show("不合法文档！" + exception1.Message);
            }
            return null;
        }

    }
}

