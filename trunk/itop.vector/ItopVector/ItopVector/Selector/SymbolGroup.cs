namespace ItopVector.Selector
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Xml;
	using ItopVector.Core.Document;

    internal class SymbolGroup
    {
        // Methods
        static SymbolGroup()
        {
//            SymbolGroup.random = new Random();
        }

        public SymbolGroup()
        {
        }

        public static SvgDocument LoadSymbol(string filename)
        {
            SvgDocument fadde2;
            if (!File.Exists(filename))
            {
                return null;
            }
            SvgDocument doc = new SvgDocument();
            
            try
            {
                doc.XmlResolver = null;
                doc.Load(filename);
				doc.AcceptChanges=false;
				doc.FilePath = filename;
                fadde2 = doc;
            }
            catch (Exception exception1)
            {
                Console.Write(exception1.Message);
                fadde2 = null;
            }

            return fadde2;

        }

        // Fields
//        private static Random random;

    }
}

