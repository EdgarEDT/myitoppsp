/***********************************************************************
 * Module:  KeyInfo.cs
 * Author:  Administrator
 * Purpose: Definition of the Struct KeyInfo
 ***********************************************************************/

using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct KeyInfo
{
   public KeyInfo(int time, byte type, int controllength)
   {
       this.keytime = time;
       this.keytype = type;
       this.controlLength = controllength;
   }
   
   public KeyInfo(int time, byte type)
   {
       this.keytime = time;
       this.keytype = type;
       this.controlLength = 0;
   }
   
   public static bool operator ==(KeyInfo info1, KeyInfo info2)
   {
       if (info1.keytime == info2.keytime)
       {
           return true;
       }
       return false;
   }
   
   public static bool operator !=(KeyInfo info1, KeyInfo info2)
   {
       if (info1.keytime != info2.keytime)
       {
           return true;
       }
       return false;
   }
   
   public static bool operator >(KeyInfo info1, KeyInfo info2)
   {
       if (info1.keytime > info2.keytime)
       {
           return true;
       }
       return false;
   }
   
   public static bool operator <(KeyInfo info1, KeyInfo info2)
   {
       if (info1.keytime < info2.keytime)
       {
           return true;
       }
       return false;
   }
   
   public static bool operator <=(KeyInfo info1, KeyInfo info2)
   {
       if (info1.keytime <= info2.keytime)
       {
           return true;
       }
       return false;
   }
   
   public static bool operator >=(KeyInfo info1, KeyInfo info2)
   {
       if (info1.keytime >= info2.keytime)
       {
           return true;
       }
       return false;
   }

	public override int GetHashCode()
	{
		return base.GetHashCode ();
	}
	public override bool Equals(object obj)
	{
		return base.Equals (obj);
	}


   public int keytime;
   public byte keytype;
   public int controlLength;

}