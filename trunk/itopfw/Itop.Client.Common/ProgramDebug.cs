using System;
using System.Collections.Generic;
using System.Text;
using Itop.Client.Option;

namespace Itop.Client.Common
{
    public class ProgramDebug
    {
        public static bool ShowDebugInfo
        {
            get 
            {
                return Settings.GetValue("ShowDebugInfo") == "ÊÇ";
            }
        }
    }
}
