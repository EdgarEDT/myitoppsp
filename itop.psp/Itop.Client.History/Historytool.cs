using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Client.History
{
    class Historytool
    {
        /// <summary>
        /// 返回字符串小括号小间的部分,仅限一对括号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FindUnits(string str)
        {
            try
            {
                if (str.Contains("(") && str.Contains(")"))
                {
                    return str.Substring(str.IndexOf("(") + 1, str.IndexOf(")") - str.IndexOf("(") - 1);
                }
                else if (str.Contains("（") && str.Contains("）"))
                {
                    return str.Substring(str.IndexOf("（") + 1, str.IndexOf("）") - str.IndexOf("（") - 1);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception)
            {

                return string.Empty;
            }
           
        }
    }
}
