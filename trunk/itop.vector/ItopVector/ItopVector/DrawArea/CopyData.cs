namespace ItopVector.DrawArea
{
    using System;

    [Serializable]
    public class CopyData
    {
        // Methods
        public CopyData(string text)
        {
            this.xmlstr = string.Empty;
            this.xmlstr = text;
        }


        // Properties
        public string XmlStr
        {
            get
            {
                return this.xmlstr;
            }
        }


        // Fields
        private string xmlstr;
    }
}

