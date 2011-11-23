using System.Runtime.InteropServices;

namespace ItopVector.DrawArea
{
	/// <summary>
	/// 参考线结构
	/// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RefLine
    {
        public int Pos;
        public bool Hori;
        public bool IsEmpty;
        public RefLine(int pos, bool hori)
        {
            this.Pos = pos;
            this.Hori = hori;
            this.IsEmpty = false;
        }
        public static RefLine Empty
        {
            get
            {
                RefLine line1 = new RefLine(0, true);
                line1.IsEmpty = true;
                return line1;
            }
        }
    }
}

