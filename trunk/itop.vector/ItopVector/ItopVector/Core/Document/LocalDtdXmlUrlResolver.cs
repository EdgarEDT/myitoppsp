namespace ItopVector.Core.Document
{
    using ItopVector.Core.Config;
    using System;
    using System.Collections;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Xml;

    public class LocalDtdXmlUrlResolver : XmlUrlResolver
    {
        // Methods
        public LocalDtdXmlUrlResolver()
        {
            this.baseUri = new Uri(new Uri("http://www.w3.org"), "Graphics/SVG/1.1/DTD/svg11.dtd", false);
            this.knownDtds = new Hashtable();
        }

        public void AddDtd(string publicIdentifier, string localFile)
        {
            if (!this.knownDtds.ContainsKey(publicIdentifier))
            {
                this.knownDtds.Add(publicIdentifier, localFile);
            }
        }

        public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
        {
            string text1 = absoluteUri.AbsoluteUri;
            int num1 = text1.IndexOf("-/W3C");
            if (num1 >= 0)
            {
                text1 = text1.Substring(num1, text1.Length - num1);
            }
            string text2 = string.Empty;
            char ch1 = Path.AltDirectorySeparatorChar;
            if (text1.LastIndexOf(ch1.ToString()) >= 0)
            {
                text2 = Application.StartupPath + @"\dtd" + text1.Substring(text1.LastIndexOf(ch1.ToString()), text1.Length - text1.LastIndexOf(ch1.ToString()));
            }
            if ((absoluteUri != null) && (this.knownDtds.ContainsKey(text1) || File.Exists(text2)))
            {
                string text3 = Assembly.GetEntryAssembly().CodeBase.Substring(8);
                text3 = text3.Substring(0, text3.LastIndexOf("/"));
                text3 = text3.Replace("/", @"\");
                string text4 = text2;
                if (this.knownDtds.ContainsKey(text1))
                {
                    string text5 = Path.Combine(text3, (string) this.knownDtds[text1]);
                    text4 = text5;
                }
                try
                {
                    return new FileStream(text4, FileMode.Open, FileAccess.Read, FileShare.Read);
                }
                catch
                {
                    throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("notfinddtd") + text4);
                }
            }
            return base.GetEntity(absoluteUri, role, ofObjectToReturn);
        }

        public override Uri ResolveUri(Uri baseUri, string relativeUri)
        {
            return base.ResolveUri(baseUri, relativeUri);
        }


        // Properties
        public Uri BaseURI
        {
            get
            {
                return this.baseUri;
            }
            set
            {
                this.baseUri = value;
            }
        }


        // Fields
        private Uri baseUri;
        private Hashtable knownDtds;
    }
}

