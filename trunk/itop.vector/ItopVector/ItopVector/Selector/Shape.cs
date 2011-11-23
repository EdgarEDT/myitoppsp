namespace ItopVector.Selector
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.Serialization;
	using System.Text;
	using ItopVector.Core.Func;
	using ItopVector.Core;

    public class Shape : IShape
    {
        // Methods
        protected Shape(SerializationInfo info, StreamingContext context)
        {
			this.pathString = string.Empty;
			this.graphPath = null;
			PointF[] tfArray1 = info.GetValue("points", typeof(PointF[])) as PointF[];
			byte[] buffer1 = info.GetValue("types", typeof(byte[])) as byte[];
			if (((tfArray1 != null) && (buffer1 != null)) && (tfArray1.Length == buffer1.Length))
			{
				this.graphPath = new GraphicsPath(tfArray1, buffer1);
				this.pathString = PathFunc.GetPathString(this.graphPath);
			}
			this.id = info.GetValue("id", typeof(string)) as string;
			tfArray1 = null;
			buffer1 = null;
            
        }
		internal Shape(string pathstr, string id)
		{
			this.pathString = pathstr;
			this.id = id;
			this.graphPath=PathFunc.PathDataParse(pathstr);
		}
		private  string ToStringPath(GraphicsPath GPath)
		{
			StringBuilder builder1 = new StringBuilder(300);
			for (int num1 = 0; num1 < GPath.PointCount; num1++)
			{
				int num3;
				int num4;
				PointF tf1 = GPath.PathPoints[num1];
				switch (GPath.PathTypes[num1])
				{
					case 0:
					{
						builder1.Append("M ");
						builder1.Append(tf1.X.ToString() + " " + tf1.Y.ToString() + " ");
						goto Label_03C0;
					}
					case 1:
					{
						builder1.Append("L ");
						builder1.Append(tf1.X.ToString() + " " + tf1.Y.ToString() + " ");
						goto Label_03C0;
					}
					case 2:
					case 130:
					{
						goto Label_03C0;
					}
					case 3:
					{
						builder1.Append("C ");
						num3 = num1;
						goto Label_020C;
					}
					case 0x80:
					{
						builder1.Append("Z");
						goto Label_03C0;
					}
					case 0x81:
					{
						goto Label_0336;
					}
					case 0x83:
					{
						builder1.Append("C ");
						num4 = num1;
						goto Label_02EA;
					}
					default:
					{
						goto Label_03C0;
					}
				}
			Label_0174:
				tf1 = GPath.PathPoints[num3];
				builder1.Append(tf1.X.ToString() + " " + tf1.Y.ToString() + " ");
				if (GPath.PathTypes[num3] == 0x83)
				{
					builder1.Append("Z");
				}
				num3++;
			Label_020C:
				if (num3 <= Math.Min((int) (num1 + 2), (int) (GPath.PathPoints.Length - 1)))
				{
					goto Label_0174;
				}
				num1 = Math.Min((int) (num1 + 2), (int) (GPath.PathPoints.Length - 1));
				goto Label_03C0;
			Label_027D:
				tf1 = GPath.PathPoints[num4];
				builder1.Append(tf1.X.ToString() + " " + tf1.Y.ToString() + " ");
				num4++;
			Label_02EA:
				if (num4 <= Math.Min((int) (num1 + 2), (int) (GPath.PathPoints.Length - 1)))
				{
					goto Label_027D;
				}
				builder1.Append("Z");
				num1 = Math.Min((int) (num1 + 2), (int) (GPath.PathPoints.Length - 1));
				goto Label_03C0;
			Label_0336:
				builder1.Append("L ");
				builder1.Append(tf1.X.ToString() + " " + tf1.Y.ToString() + " ");
				builder1.Append("Z");
			Label_03C0:;
			}
			return builder1.ToString();
		}

        

        // Properties
        

        internal string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        internal GraphicsPath GraphPath
        {
            get
            {
                return this.graphPath;
            }
        }
		internal string  PathString 
		{
			get
			{
				return this.pathString;
			}
		}

		#region IShape ³ÉÔ±

		public SvgElement OwnerElement
		{
			get
			{
				return null;
			}
		}
		public string Label
		{
			get
			{
				return id;
			}
		}

		#endregion

        // Fields

        private string pathString;
        private GraphicsPath graphPath;
        private string id;
    }
}

