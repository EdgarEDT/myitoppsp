namespace ItopVector.Core.Func
{
    using ItopVector.Core;
    using ItopVector.Core.Config;
    using ItopVector.Core.Figure;
    using ItopVector.Core.Interface.Figure;
    using ItopVector.Core.Types;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Text;
    using System.Text.RegularExpressions;

    public class PathFunc
    {
        // Methods
        static PathFunc()
        {
            PathFunc.startinfo = null;
        }

        public PathFunc()
        {
        }

        public static RectangleF GetBounds(GraphicsPath path)
        {
            return GetBounds(path,new Matrix());
        }
		public static RectangleF GetBounds(GraphicsPath path,Matrix matrix)
		{
			GraphicsPath path1 = (GraphicsPath) path.Clone();
			path1.Flatten(matrix, 0.5f);
			return path1.GetBounds();
		}


        public static float[] GetCoords(string segment)
        {
            float[] singleArray3 = new float[6];
            float[] singleArray1 = singleArray3;
            segment = segment.Trim();
            segment = segment.Substring(1);
            segment = segment.Trim();
            if (segment != "")
            {
                Regex regex1 = new Regex(@"[\s\,]+");
                Regex regex2 = new Regex(@"([0-9])\-");
                segment = regex1.Replace(segment, ",");
                segment = regex2.Replace(segment, "$1,-");
                char[] chArray1 = new char[1] { ',' } ;
                string[] textArray1 = segment.Split(chArray1);
                singleArray1 = new float[textArray1.Length];
                for (int num1 = 0; num1 < singleArray1.Length; num1++)
                {
                    if (num1 >= textArray1.GetLength(0))
                    {
                        break;
                    }
                    string text1 = (string) textArray1.GetValue(num1);
                    try
                    {
                        singleArray1.SetValue(ItopVector.Core.Func.Number.ParseFloatStr((string) textArray1.GetValue(num1)), num1);
                    }
                    catch (Exception exception1)
                    {
                        throw exception1;
                    }
                }
            }
            return singleArray1;
        }

        public static GraphicsPath GetPathFromGraph(IGraph graph)
        {
            GraphicsPath path1 = new GraphicsPath();
            if (graph is ItopVector.Core.Figure.Group)
            {
                SvgElementCollection.ISvgElementEnumerator enumerator1 = ((ItopVector.Core.Figure.Group) graph).GraphList.GetEnumerator();
                while (enumerator1.MoveNext())
                {
                    SvgElement element1 = (SvgElement) enumerator1.Current;
                    if (element1 is IGraph)
                    {
                        GraphicsPath path2 = PathFunc.GetPathFromGraph((IGraph) element1);
                        if (path2.PointCount > 0)
                        {
                            path1.StartFigure();
                            path1.AddPath(path2, false);
                        }
                    }
                }
                return path1;
            }
            path1 = (GraphicsPath) graph.GPath.Clone();
            path1.Transform(graph.Transform.Matrix);
            return path1;
        }

        public static string GetPathString(GraphicsPath path)
        {
            string text1 = string.Empty;
            StringBuilder builder1 = new StringBuilder();
            for (int num1 = 0; num1 < path.PointCount; num1++)
            {
                int num3;
                int num4;
                PointF tf1 = path.PathPoints[num1];
                switch (path.PathTypes[num1])
                {
                    case 0:
                    {
                        builder1.Append("M ");
                        builder1.Append(tf1.X.ToString()+" "+tf1.Y.ToString()+ " " ) ;                        
                        goto Label_0336;
                    }
                    case 1:
                    {
                        builder1.Append("L ");
                        builder1.Append(tf1.X.ToString()+ " "+ tf1.Y.ToString()+ " " ) ;
                        
                        goto Label_0336;
                    }
                    case 2:
                    case 130:
                    {
                        goto Label_0336;
                    }
                    case 3:
                    {
                        builder1.Append("C ");
                        num3 = num1;
                        goto Label_01D0;
                    }
                    case 0x80:
                    {
                        builder1.Append("Z");
                        goto Label_0336;
                    }
                    case 0x81:
                    {
                        goto Label_02C9;
                    }
                    case 0x83:
                    {
                        builder1.Append("C ");
                        num4 = num1;
                        goto Label_028F;
                    }
                    default:
                    {
                        goto Label_0336;
                    }
                }
            Label_0148:
                tf1 = path.PathPoints[num3];
                builder1.Append(tf1.X.ToString()+ " "+ tf1.Y.ToString()+ " " ) ;
                if (path.PathTypes[num3] == 0x83)
                {
                    builder1.Append("Z");
                }
                num3++;
            Label_01D0:
                if (num3 <= Math.Min((int) (num1 + 2), (int) (path.PathPoints.Length - 1)))
                {
                    goto Label_0148;
                }
                num1 = Math.Min((int) (num1 + 2), (int) (path.PathPoints.Length - 1));
                goto Label_0336;
            Label_0223:
                tf1 = path.PathPoints[num4];
                builder1.Append(tf1.X.ToString()+ " "+ tf1.Y.ToString()+ " " ) ;
                
                num4++;
            Label_028F:
                if (num4 <= Math.Min((int) (num1 + 2), (int) (path.PathPoints.Length - 1)))
                {
                    goto Label_0223;
                }
                builder1.Append("Z");
                num1 = Math.Min((int) (num1 + 2), (int) (path.PathPoints.Length - 1));
                goto Label_0336;
            Label_02C9:
                builder1.Append("L ");
                builder1.Append(tf1.X.ToString()+ " "+ tf1.Y.ToString()+ " " ) ;
                builder1.Append("Z");
            Label_0336:;
            }
            return builder1.ToString();
        }
		public static string GetPathString2(GraphicsPath path)
		{
			string text1 = string.Empty;
			StringBuilder builder1 = new StringBuilder();
			for (int num1 = 0; num1 < path.PointCount; num1++)
			{
				int num3;
				int num4;
				PointF tf1 = path.PathPoints[num1];
				switch (path.PathTypes[num1])
				{
					case 0:
					{
						text1 = text1 + "M ";
						string text3 = text1;
						string[] textArray1 = new string[5] { text3, tf1.X.ToString(), " ", tf1.Y.ToString(), " " } ;
						text1 = string.Concat(textArray1);
						goto Label_0336;
					}
					case 1:
					{
						text1 = text1 + "L ";
						string text4 = text1;
						string[] textArray2 = new string[5] { text4, tf1.X.ToString(), " ", tf1.Y.ToString(), " " } ;
						text1 = string.Concat(textArray2);
						goto Label_0336;
					}
					case 2:
					case 130:
					{
						goto Label_0336;
					}
					case 3:
					{
						text1 = text1 + "C ";
						num3 = num1;
						goto Label_01D0;
					}
					case 0x80:
					{
						text1 = text1 + "Z";
						goto Label_0336;
					}
					case 0x81:
					{
						goto Label_02C9;
					}
					case 0x83:
					{
						text1 = text1 + "C ";
						num4 = num1;
						goto Label_028F;
					}
					default:
					{
						goto Label_0336;
					}
				}
			Label_0148:
				tf1 = path.PathPoints[num3];
				string text5 = text1;
				string[] textArray3 = new string[5] { text5, tf1.X.ToString(), " ", tf1.Y.ToString(), " " } ;
				text1 = string.Concat(textArray3);
				if (path.PathTypes[num3] == 0x83)
				{
					text1 = text1 + "Z";
				}
				num3++;
			Label_01D0:
				if (num3 <= Math.Min((int) (num1 + 2), (int) (path.PathPoints.Length - 1)))
				{
					goto Label_0148;
				}
				num1 = Math.Min((int) (num1 + 2), (int) (path.PathPoints.Length - 1));
				goto Label_0336;
			Label_0223:
				tf1 = path.PathPoints[num4];
				string text6 = text1;
				string[] textArray4 = new string[5] { text6, tf1.X.ToString(), " ", tf1.Y.ToString(), " " } ;
				text1 = string.Concat(textArray4);
				num4++;
			Label_028F:
				if (num4 <= Math.Min((int) (num1 + 2), (int) (path.PathPoints.Length - 1)))
				{
					goto Label_0223;
				}
				text1 = text1 + "Z";
				num1 = Math.Min((int) (num1 + 2), (int) (path.PathPoints.Length - 1));
				goto Label_0336;
			Label_02C9:
				text1 = text1 + "L ";
				string text7 = text1;
				string[] textArray5 = new string[5] { text7, tf1.X.ToString(), " ", tf1.Y.ToString(), " " } ;
				text1 = string.Concat(textArray5);
				text1 = text1 + "Z";
			Label_0336:;
			}
			return text1;
		}
        public static GraphicsPath PathDataParse(string text)
        {
            PointInfoCollection collection1 = new PointInfoCollection();
            return PathFunc.PathDataParse(text, collection1);
        }

        public static GraphicsPath PathDataParse(string text, PointInfoCollection pointsInfo)
        {
            PathFunc.startinfo = null;
            int num1 = -1;
            pointsInfo.Clear();
            GraphicsPath path1 = new GraphicsPath();
            GraphicsPathIterator iterator1 = new GraphicsPathIterator(path1);
            PointF tf1 = PointF.Empty;
            string text1 = "";
			int nLength=text.Length;
            Regex regex1 = new Regex("[A-DF-Za-df-z][^A-DF-Za-df-z]*");
            PointF tf2 = PointF.Empty;
            Match match1 = regex1.Match(text);
            int num2 = 0;
            //StringBuilder text2 = new StringBuilder();
            //string text3 = text;
            PointF tf3 = PointF.Empty;
            PointF tf4 = PointF.Empty;
            PointInfo info1 = null;
			StringBuilder text4=new StringBuilder(text);
            while (match1.Success)
            {
                float single1;
                float single2;
                float single3;
                float single4;
                float single5;
                float single6;
                ExtendedGraphicsPath path2;
                int num3 = match1.Index;
				text4.Remove(0,text1.Length);
//                text4 =new StringBuilder(text3.Substring(num3, text3.Length - match1.Index));
                if (info1 != null)
                {
                    //info1.NextString =match1.Value.Trim();// text4.ToString();//match1.Value.Trim();
                }
                text1 = match1.Value.Trim();
                char ch1 = (char) text1.ToCharArray(0, 1).GetValue(0);
                float[] singleArray1 = PathFunc.GetCoords(text1);
                PointInfo info2 = null;
                char ch2 = ch1;
                if (ch2 <= 'Z')
                {
                    if (ch2 <= 'H')
                    {
                        switch (ch2)
                        {
                            case 'A':
                            {
                                path2 = new ExtendedGraphicsPath(path1);
                                if (singleArray1.Length != 7)
                                {
                                    throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidpathformat") + text1);
                                }
								isArc=true;
                                path2.AddArc(tf2, new PointF(singleArray1[5], singleArray1[6]), singleArray1[0], singleArray1[1], (singleArray1[3] != 0f) && true, (singleArray1[4] != 0f) && true, singleArray1[2]);
                                goto Label_092D;
                            }
                            case 'B':
                            {
                                goto Label_0FEB;
                            }
                            case 'C':
                            {
								PointF pf1=tf2;
								int num10=0;
								
                                if (singleArray1.Length < 6)
                                {
                                    throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidpathformat") + text1);
                                }
								
								drawArc:
                                path1.AddBezier(pf1.X, pf1.Y, singleArray1[num10], singleArray1[num10+1], singleArray1[num10+2], singleArray1[num10+3], singleArray1[num10+4], singleArray1[num10+5]);
							
								pf1=new PointF(singleArray1[num10+4],singleArray1[num10+5]);
								
								
								if((num10+6)<singleArray1.Length)
								{
									
//									info2 = new PointInfo(new PointF(singleArray1[num10+4], singleArray1[num10+5]), new PointF(singleArray1[num10+0], singleArray1[num10+1]), new PointF(singleArray1[num10+2], singleArray1[num10+3]), text1);
//									info2.Command="C";
//									pointsInfo.Add(info2);
									num10+=6;
									goto drawArc;

								}
								if (num10>0)
								{
									tf1 = new PointF(singleArray1[num10+2], singleArray1[num10+3]);
									tf2 = new PointF(singleArray1[num10+4], singleArray1[num10+5]);
									
									info2 = new PointInfo(new PointF(singleArray1[num10+4], singleArray1[num10+5]), new PointF(singleArray1[num10+0], singleArray1[num10+1]), new PointF(singleArray1[num10+2], singleArray1[num10+3]), text1);
									info2.Command="C";
									goto Label_1001;
								}

                                goto Label_0872;
                            }
                            case 'H':
                            {
                                goto Label_033D;
                            }
                        }
                        goto Label_0FEB;
                    }
                    switch (ch2)
                    {
                        case 'L':
                        {
//                            if (singleArray1.Length != 2)
//                            {
//                                throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidpathformat") + text1);
//                            }
                            //path1.AddLine(tf2, new PointF(singleArray1[0], singleArray1[1]));
							PointF[] ptfs=new PointF[singleArray1.Length/2+1];
							ptfs[0]=tf2;
							int index1=0;
							for (int i = 1; i < singleArray1.Length; i++)
							{
								index1++;
								ptfs[index1]=new PointF(singleArray1[i-1],singleArray1[i]);
								i++;
							}
							path1.AddLines(ptfs);
                            info2 = new PointInfo(new PointF(singleArray1[0], singleArray1[1]), text1);
                            goto Label_02B5;
                        }
                        case 'M':
                        {
                            path1.StartFigure();
                            if (singleArray1.Length != 2)
                            {
                                throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidpathformat") + text1);
                            }
                            tf2 = new PointF(singleArray1[0], singleArray1[1]);
                            info2 = new PointInfo(tf2, text1);
                            info2.IsStart = true;
							isArc=false;
                            PathFunc.startinfo = info2;
                            //info2.PreString = text1;//text2.ToString();
                            goto Label_0205;
                        }
                        case 'N':
                        case 'O':
                        case 'P':
                        case 'R':
                        case 'U':
                        {
                            goto Label_0FEB;
                        }
                        case 'Q':
                        {
                            single1 = tf2.X + (((singleArray1[0] - tf2.X) * 2f) / 3f);
                            single2 = tf2.Y + (((singleArray1[1] - tf2.Y) * 2f) / 3f);
                            single3 = singleArray1[0] + ((singleArray1[2] - singleArray1[0]) / 3f);
                            single4 = singleArray1[1] + ((singleArray1[3] - singleArray1[1]) / 3f);
                            if (singleArray1.Length != 4)
                            {
                                throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidpathformat") + text1);
                            }
                            path1.AddBezier(tf2.X, tf2.Y, single1, single2, single3, single4, singleArray1[2], singleArray1[3]);
                            goto Label_0579;
                        }
                        case 'S':
                        {
                            if (singleArray1.Length != 4)
                            {
                                throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidpathformat") + text1);
                            }
                            if (!tf1.IsEmpty)
                            {
                                goto Label_0755;
                            }
                            path1.AddBezier(tf2.X, tf2.Y, tf2.X, tf2.Y, singleArray1[0], singleArray1[1], singleArray1[2], singleArray1[3]);
                            pointsInfo.Add(new PointInfo(new PointF(singleArray1[2], singleArray1[3]), tf2, new PointF(singleArray1[0], singleArray1[1]), text1));
                            goto Label_07C4;
                        }
                        case 'T':
                        {
                            if (!tf1.IsEmpty)
                            {
                                goto Label_05D2;
                            }
                            single5 = tf2.X;
                            single6 = tf2.Y;
                            goto Label_0600;
                        }
                        case 'V':
                        {
                            if (singleArray1.Length != 1)
                            {
                                throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidpathformat") + text1);
                            }
                            path1.AddLine(tf2, new PointF(tf2.X, singleArray1[0]));
                            goto Label_0439;
                        }
                        case 'Z':
                        {
                            goto Label_0F72;
                        }
                    }
                    goto Label_0FEB;
                }
                if (ch2 <= 'h')
                {
                    switch (ch2)
                    {
                        case 'a':
                        {
                            path2 = new ExtendedGraphicsPath(path1);
                            if (singleArray1.Length != 7)
                            {
                                throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidpathformat") + text1);
                            }
                            path2.AddArc(tf2, new PointF(singleArray1[5] + tf2.X, singleArray1[6] + tf2.Y), singleArray1[0], singleArray1[1], (singleArray1[3] != 0f) && true, (singleArray1[4] != 0f) && true, singleArray1[2]);
                            goto Label_09F7;
                        }
                        case 'b':
                        {
                            goto Label_0FEB;
                        }
                        case 'c':
                        {
                            if (singleArray1.Length != 6)
                            {
                                throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("unnabeparsepath") + text1);
                            }
                            path1.AddBezier(tf2.X, tf2.Y, (float) (singleArray1[0] + tf2.X), (float) (singleArray1[1] + tf2.Y), (float) (singleArray1[2] + tf2.X), (float) (singleArray1[3] + tf2.Y), (float) (singleArray1[4] + tf2.X), (float) (singleArray1[5] + tf2.Y));
                            goto Label_0EF0;
                        }
                        case 'h':
                        {
                            goto Label_0397;
                        }
                    }
                    goto Label_0FEB;
                }
                switch (ch2)
                {
                    case 'l':
                    {
                        if (singleArray1.Length != 2)
                        {
                            throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidpathformat") + text1);
                        }
                        path1.AddLine(tf2, new PointF(singleArray1[0] + tf2.X, singleArray1[1] + tf2.Y));
                        goto Label_030E;
                    }
                    case 'm':
                    {
                        path1.StartFigure();
                        if (singleArray1.Length != 2)
                        {
                            throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidpathformat") + text1);
                        }
                        tf2 = new PointF(singleArray1[0], singleArray1[1]);
                        info2 = new PointInfo(tf2, text1);
                        info2.IsStart = true;
						isArc=false;
                        PathFunc.startinfo = info2;
                        goto Label_025F;
                    }
                    case 'n':
                    case 'o':
                    case 'p':
                    case 'r':
                    case 'u':
                    {
                        goto Label_0FEB;
                    }
                    case 'q':
                    {
                        single1 = tf2.X + ((singleArray1[0] * 2f) / 3f);
                        single2 = tf2.Y + ((singleArray1[1] * 2f) / 3f);
                        single3 = (singleArray1[0] + tf2.X) + ((singleArray1[2] - singleArray1[0]) / 3f);
                        single4 = (singleArray1[1] + tf2.Y) + ((singleArray1[3] - singleArray1[1]) / 3f);
                        if (singleArray1.Length != 4)
                        {
                            throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidpathformat") + text1);
                        }
                        path1.AddBezier(tf2.X, tf2.Y, single1, single2, single3, single4, (float) (singleArray1[2] + tf2.X), (float) (singleArray1[3] + tf2.Y));
                        goto Label_0B0D;
                    }
                    case 's':
                    {
                        if (singleArray1.Length != 4)
                        {
                            throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidpathformat") + text1);
                        }
                        if (!tf1.IsEmpty)
                        {
                            goto Label_0D63;
                        }
                        path1.AddBezier(tf2.X, tf2.Y, tf2.X, tf2.Y, (float) (singleArray1[0] + tf2.X), (float) (singleArray1[1] + tf2.Y), (float) (singleArray1[2] + tf2.X), (float) (singleArray1[3] + tf2.Y));
                        goto Label_0DF2;
                    }
                    case 't':
                    {
                        if (!tf1.IsEmpty)
                        {
                            goto Label_0B98;
                        }
                        single5 = tf2.X;
                        single6 = tf2.Y;
                        goto Label_0BC6;
                    }
                    case 'v':
                    {
                        if (singleArray1.Length != 1)
                        {
                            throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidpathformat") + text1);
                        }
                        path1.AddLine(tf2, new PointF(tf2.X, singleArray1[0] + tf2.Y));
                        goto Label_049B;
                    }
                    case 'z':
                    {
                        goto Label_0F72;
                    }
                    default:
                    {
                        goto Label_0FEB;
                    }
                }
            Label_0205://M
                num2++;
                goto Label_1001;
            Label_025F:
                num2++;
                goto Label_1001;
            Label_02B5:
                tf2 = new PointF(singleArray1[0], singleArray1[1]);
                goto Label_1001;
            Label_030E:
                tf2 = new PointF(singleArray1[0] + tf2.X, singleArray1[1] + tf2.Y);
                info2 = new PointInfo(tf2, text1);
                goto Label_1001;
            Label_033D:
                if (singleArray1.Length == 1)
                {
                    path1.AddLine(tf2, new PointF(singleArray1[0], tf2.Y));
                }
                else
                {
                    throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidpathformat") + text1);
                }
                tf2 = new PointF(singleArray1[0], tf2.Y);
                info2 = new PointInfo(tf2, text1);
                goto Label_1001;
            Label_0397:
                if (singleArray1.Length == 1)
                {
                    path1.AddLine(tf2, new PointF(singleArray1[0] + tf2.X, tf2.Y));
                }
                else
                {
                    throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidpathformat") + text1);
                }
                tf2 = new PointF(singleArray1[0] + tf2.X, tf2.Y);
                info2 = new PointInfo(tf2, text1);
                goto Label_1001;
            Label_0439:
                tf2 = new PointF(tf2.X, singleArray1[0]);
                info2 = new PointInfo(tf2, text1);
                goto Label_1001;
            Label_049B:
                tf2 = new PointF(tf2.X, singleArray1[0] + tf2.Y);
                info2 = new PointInfo(tf2, text1);
                goto Label_1001;
            Label_0579:
                tf1 = new PointF(single3, single4);
                tf2 = new PointF(singleArray1[2], singleArray1[3]);
                info2 = new PointInfo(tf2, new PointF(single1, single2), new PointF(single3, single4), text1);
                goto Label_1001;
            Label_05D2:
                single5 = (2f * tf2.X) - tf1.X;
                single6 = (2f * tf2.Y) - tf1.Y;
            Label_0600:
                single1 = tf2.X + (((single5 - tf2.X) * 2f) / 3f);
                single2 = tf2.Y + (((single6 - tf2.Y) * 2f) / 3f);
                single3 = single5 + ((singleArray1[0] - single5) / 3f);
                single4 = single6 + ((singleArray1[1] - single6) / 3f);
                if (singleArray1.Length != 2)
                {
                    throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidpathformat") + text1);
                }
                path1.AddBezier(tf2.X, tf2.Y, single1, single2, single3, single4, singleArray1[0], singleArray1[1]);
                tf1 = new PointF(single3, single4);
                tf2 = new PointF(singleArray1[0], singleArray1[1]);
                info2 = new PointInfo(tf2, new PointF(single1, single2), new PointF(single3, single4), text1);
                goto Label_1001;
            Label_0755:
                single1 = (2f * tf2.X) - tf1.X;
                single2 = (2f * tf2.Y) - tf1.Y;
                path1.AddBezier(tf2.X, tf2.Y, single1, single2, singleArray1[0], singleArray1[1], singleArray1[2], singleArray1[3]);
            Label_07C4:
                tf1 = new PointF(singleArray1[0], singleArray1[1]);
                tf2 = new PointF(singleArray1[2], singleArray1[3]);
                info2 = new PointInfo(path1.GetLastPoint(), path1.PathPoints[path1.PointCount - 3], path1.PathPoints[path1.PointCount - 2], text1);
                goto Label_1001;
            Label_0872:
                tf1 = new PointF(singleArray1[2], singleArray1[3]);
                tf2 = new PointF(singleArray1[4], singleArray1[5]);
                info2 = new PointInfo(new PointF(singleArray1[4], singleArray1[5]), new PointF(singleArray1[0], singleArray1[1]), new PointF(singleArray1[2], singleArray1[3]), text1);
                goto Label_1001;
            Label_092D://A
                tf1 = PointF.Empty;
                tf2 = path1.GetLastPoint();//, path1.PathPoints[path1.PointCount - 3], path1.PathPoints[path1.PointCount - 2]
                info2 = new PointInfo(tf2,PointF.Empty,new PointF(tf2.X+singleArray1[0],tf2.Y), text1);
				info2.NextControl=new PointF(tf2.X,tf2.Y+singleArray1[1]);
				info2.Rx=singleArray1[0];
				info2.Ry=singleArray1[1];
				info2.Angle=singleArray1[2];
				info2.LargeArcFlage=(int)singleArray1[3];
				info2.SweepFlage=(int)singleArray1[4];
                goto Label_1001;
            Label_09F7:
                tf1 = PointF.Empty;
                tf2 = path1.GetLastPoint();
                info2 = new PointInfo(path1.GetLastPoint(), path1.PathPoints[path1.PointCount - 3], path1.PathPoints[path1.PointCount - 2], text1);
                goto Label_1001;
            Label_0B0D:
                tf1 = new PointF(single3, single4);
                tf2 = new PointF(singleArray1[2] + tf2.X, tf2.Y + singleArray1[3]);
                info2 = new PointInfo(path1.GetLastPoint(), path1.PathPoints[path1.PointCount - 3], path1.PathPoints[path1.PointCount - 2], text1);
                goto Label_1001;
            Label_0B98:
                single5 = (2f * tf2.X) - tf1.X;
                single6 = (2f * tf2.Y) - tf1.Y;
            Label_0BC6:
                single1 = tf2.X + (((single5 - tf2.X) * 2f) / 3f);
                single2 = tf2.Y + (((single6 - tf2.Y) * 2f) / 3f);
                single3 = single5 + (((singleArray1[0] + tf2.X) - single5) / 3f);
                single4 = single6 + (((singleArray1[1] + tf2.Y) - single6) / 3f);
                if (singleArray1.Length != 2)
                {
                    throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidpathformat") + text1);
                }
                path1.AddBezier(tf2.X, tf2.Y, single1, single2, single3, single4, (float) (singleArray1[0] + tf2.X), (float) (singleArray1[1] + tf2.Y));
                tf1 = new PointF(single3, single4);
                tf2 = new PointF(singleArray1[0] + tf2.X, tf2.Y + singleArray1[1]);
                info2 = new PointInfo(path1.GetLastPoint(), path1.PathPoints[path1.PointCount - 3], path1.PathPoints[path1.PointCount - 2], text1);
                goto Label_1001;
            Label_0D63:
                single1 = (2f * tf2.X) - tf1.X;
                single2 = (2f * tf2.Y) - tf1.Y;
                path1.AddBezier(tf2.X, tf2.Y, single1, single2, (float) (singleArray1[0] + tf2.X), (float) (singleArray1[1] + tf2.Y), (float) (singleArray1[2] + tf2.X), (float) (singleArray1[3] + tf2.Y));
            Label_0DF2:
                tf1 = new PointF(singleArray1[0] + tf2.X, tf2.Y + singleArray1[1]);
                tf2 = new PointF(singleArray1[2] + tf2.X, tf2.Y + singleArray1[3]);
                info2 = new PointInfo(path1.GetLastPoint(), path1.PathPoints[path1.PointCount - 3], path1.PathPoints[path1.PointCount - 2], text1);
                goto Label_1001;
            Label_0EF0:
                tf1 = new PointF(singleArray1[2] + tf2.X, tf2.Y + singleArray1[3]);
                tf2 = new PointF(singleArray1[4] + tf2.X, tf2.Y + singleArray1[5]);
                info2 = new PointInfo(path1.GetLastPoint(), path1.PathPoints[path1.PointCount - 3], path1.PathPoints[path1.PointCount - 2], text1);
                goto Label_1001;
            Label_0F72://Z
                path1.CloseFigure();
                if (pointsInfo.Count > 0)
                {
                    PointInfo info3 = pointsInfo[pointsInfo.Count - 1];
                    if ((PathFunc.startinfo != null) && (info3 != PathFunc.startinfo))
                    {   PathFunc.startinfo.PreInfo = info3;
						info3.NextInfo = PathFunc.startinfo;	              
						if(!isArc)
						{   
							PathFunc.startinfo.SecondControl = info3.SecondControl;
						}
//                        info3.NextControl = PathFunc.startinfo.NextControl;
                        info3.IsEnd = true;
                    }
                }
                num2++;
                goto Label_1001;
            Label_0FEB://error
                throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("unnabeparsepath") + text);
            Label_1001:
                if (info2 != null)
                {
                    info2.Command = ch1.ToString();
                    pointsInfo.Add(info2);
                    //info2.PreString = text1;//text2.ToString();
                    info2.SubPath = num2;
					info2.Index=num3;
                }

                info1 = info2;
                //text2.Append(match1.Value);
                match1 = match1.NextMatch();
                num1++;
            }
            return path1;
        }


        // Fields
        private static PointInfo startinfo;
		private static bool isArc;//Õ÷‘≤ª°
    }
}

