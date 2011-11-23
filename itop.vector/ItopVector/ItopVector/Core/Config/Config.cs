namespace ItopVector.Core.Config
{
    using System;
    using System.Collections;

    public class Config
    {
        // Methods
        static Config()
        {
            ItopVector.Core.Config.Config.ConfigList = new Hashtable(0x10);
        }

        public Config()
        {
        }

        public static string GetLabelForName(string name)
        {
            if (ItopVector.Core.Config.Config.ConfigList.Contains(name))
            {
                return (string) ItopVector.Core.Config.Config.ConfigList[name];
            }
            return string.Empty;
        }


        // Fields
        public static Hashtable ConfigList;
    }
}

