using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.TLPSP.DEVICE
{
    internal class ShapeHelper
    {
        static Dictionary<string, string> typedic;
        static ShapeHelper() {
            typedic=  new Dictionary<string, string>();
            typedic.Add("01","57AF94BA-4129-45dc-0005-000000000001");
            typedic.Add("101", "57AF94BA-4129-45dc-0006-000000000001");
            typedic.Add("72", "57AF94BA-4129-45dc-2002-000000000001");
            typedic.Add("03", "57AF94BA-4129-45dc-0004-000000000001");
            typedic.Add("05", "57AF94BA-4129-45dc-0007-000000000001");
            typedic.Add("02", "57AF94BA-4129-45dc-0002-000000000001");
            typedic.Add("70", "57AF94BA-4129-45dc-0008-000000000001");
            typedic.Add("71", "57AF94BA-4129-45dc-0003-000000000001");
            typedic.Add("56", "57AF94BA-4129-45dc-0056-000000000001");
            typedic.Add("54", "57AF94BA-4129-45dc-0056-000000000001");
            typedic.Add("58", "57AF94BA-4129-45dc-0056-000000000001");
        }
        public static string GetShapeKey(string type){
            return typedic[type];
        }
        
    }
}
