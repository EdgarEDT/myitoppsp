namespace ItopVector.Core.Types
{
    using System;
    using System.Drawing;
	using System.Text;
    public class PointInfo
    {
        // Methods
        public PointInfo(PointF middle, string str) : this(middle, PointF.Empty, PointF.Empty, str)
        {
        }

        public PointInfo(PointF middle, PointF first, PointF sec, string str)
        {
//            this.MiddlePoint = PointF.Empty;
//            this.FirstControl = PointF.Empty;
//            this.SecondControl = PointF.Empty;
//            this.PointString = string.Empty;
            this.IsStart = false;
            this.IsEnd = false;
            this.PreInfo = null;
            this.NextInfo = null;
            this.SubPath = -1;
            this.NextControl = PointF.Empty;
            this.Command = string.Empty;
            this.MiddlePoint = middle;
            this.pointString = str;
            this.FirstControl = first;
            this.SecondControl = sec;
			this.owerCollection=null;
			this.Index=0;
			this.Rx=this.Ry=0f;
			this.Angle=0f;
			this.SweepFlage=this.LargeArcFlage=0;

        }


        // Properties
        public string NextString
        {
            get
            {
				string str1=string.Empty;
				int num1=OwerCollection.IndexOf(this);

				if (num1<OwerCollection.Count)
				{
					if(OwerCollection.OwerGraph!=null)
					{
						str1=OwerCollection.OwerGraph.GetAttribute("d").Substring(this.Index+this.PointString.Length);
					}					
				}			
				return str1;
            }
            
        }

        public bool Steep
        {
            get
            {
                if (!this.SecondControl.IsEmpty && !this.NextControl.IsEmpty)
                {
                    float single1 = (this.SecondControl.Y - this.MiddlePoint.Y) * (this.MiddlePoint.X - this.NextControl.X);
                    float single2 = (this.MiddlePoint.Y - this.NextControl.Y) * (this.SecondControl.X - this.MiddlePoint.X);
                    if (Math.Abs((float) (single1 - single2)) <= Math.Abs((float) (single1 * 0.1f)))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
		public string PreString
		{
			get
			{				
				string str1=string.Empty;
				if(OwerCollection.OwerGraph!=null)
				{
					str1=OwerCollection.OwerGraph.GetAttribute("d").Substring(0,this.Index);
				}
				
				return str1;
			}			
		}
		public string PointString
		{
			get
			{
				return pointString;
			}
			set
			{
				pointString= value;
			}
		}
		public PointInfoCollection OwerCollection
		{
			get
			{
				return owerCollection;
			}
			set
			{
				owerCollection= value;
			}
		}

		public PointF NextControl
		{
			get
			{
				if (this.Command=="A")
				{
					nextControl=new PointF(this.MiddlePoint.X+this.Rx,this.MiddlePoint.Y);
				}
				return nextControl;
			}
			set
			{
				nextControl= value;
			}
		}
		public PointF SecondControl
		{
			get
			{
				if (this.Command=="A")
				{
					secondControl=new PointF(this.MiddlePoint.X,this.MiddlePoint.Y+this.Ry);
				}
				return secondControl;
			}
			set
			{
				secondControl= value;
			}
		}

        // Fields
        public string Command;
        public PointF FirstControl;
        public bool IsEnd;
        public bool IsStart;
        public PointF MiddlePoint;
        private PointF nextControl;
        public PointInfo NextInfo;
        private string pointString;
        public PointInfo PreInfo;
        private PointF secondControl;
        public int SubPath;
		private PointInfoCollection owerCollection;
		/// <summary>
		/// PointString的第一个字符在d中的位置.
		/// </summary>
		public int Index;
		public float Rx;//半长轴
		public float Ry;//半短轴
		public float Angle;//x轴夹角
		public int LargeArcFlage;//大角
		public int SweepFlage;//顺时针
    }
}

