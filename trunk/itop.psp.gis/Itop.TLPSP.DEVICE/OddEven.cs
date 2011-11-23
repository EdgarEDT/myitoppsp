using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.TLPSP.DEVICE
{
    ///   <SUMMARY> 
    ///  判断一个整数是奇数还是偶数。
    ///   </SUMMARY> 
    class OddEven
    {
        static private int s = 1;
        static public bool IsEven(int a)
        {
            return ((a & s) == 0);
        }
        static public bool IsOdd(int a)
        {
            return !IsEven(a);
        }
    }
}
