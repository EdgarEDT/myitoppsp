namespace ItopVector.Core.Types
{
    using ItopVector.Core.Func;
    using ItopVector.Core.Interface;
    using System;
    using System.Drawing.Drawing2D;
    using System.Text.RegularExpressions;

    public class Transf : ITransf
    {
        // Methods
        public Transf()
        {
            this.angle = 0f;
            this.matrix = new System.Drawing.Drawing2D.Matrix();
        }
		public Transf(Matrix matrix1)
		{
			this.angle = 0f;
			this.matrix = matrix1;
		}
        public Transf(string str1)
        {
            this.angle = 0f;
            this.matrix = new System.Drawing.Drawing2D.Matrix();
            Regex regex1 = new Regex(@"([A-Za-z]+)\s*\(([\-0-9e\.\,\s]+)\)");
            for (Match match1 = regex1.Match(str1); match1.Success; match1 = match1.NextMatch())
            {
                string text1 = match1.Value.Trim();
                int num1 = text1.IndexOf("(");
                string text2 = text1.Substring(0, num1);
                string text3 = text1.Substring(num1 + 1, (text1.Length - num1) - 2);
                Regex regex2 = new Regex(@"[\s\,]+");
                text3 = regex2.Replace(text3, ",");
                char[] chArray1 = new char[1] { ',' } ;
                string[] textArray1 = text3.Split(chArray1);
                int num2 = textArray1.GetLength(0);
                float[] singleArray1 = new float[num2];
                for (int num3 = 0; num3 < num2; num3++)
                {
                    singleArray1.SetValue(ItopVector.Core.Func.Number.ParseFloatStr(textArray1[num3]), num3);
                }
                if (text2 == "translate")
                {
                    if (num2 == 1)
                    {
                        this.setTranslate(singleArray1[0], 0f);
                        goto Label_0261;
                    }
                    if (num2 == 2)
                    {
                        this.setTranslate(singleArray1[0], singleArray1[1]);
                        goto Label_0261;
                    }
                    throw new ApplicationException("translate 参数错误：" + text1);
                }
                if (text2 == "rotate")
                {
                    if (num2 == 1)
                    {
                        this.setRotate(singleArray1[0]);
                        goto Label_0261;
                    }
                    if (num2 == 3)
                    {
                        this.setRotate(singleArray1[0], singleArray1[1], singleArray1[2]);
                        goto Label_0261;
                    }
                    throw new ApplicationException("rotate 参数错误:" + text1);
                }
                if (text2 == "scale")
                {
                    if (num2 == 1)
                    {
                        this.setScale(singleArray1[0], singleArray1[0]);
                        goto Label_0261;
                    }
                    if (num2 == 2)
                    {
                        this.setScale(singleArray1[0], singleArray1[1]);
                        goto Label_0261;
                    }
                    throw new ApplicationException("scale 参数错误");
                }
                if (text2 == "skewX")
                {
                    if (num2 == 1)
                    {
                        this.setSkewX(singleArray1[0]);
                        goto Label_0261;
                    }
                    throw new ApplicationException("skewX 参数错误");
                }
                if (text2 == "skewY")
                {
                    if (num2 == 1)
                    {
                        this.setSkewY(singleArray1[0]);
                        goto Label_0261;
                    }
                    throw new ApplicationException("skewY 参数错误");
                }
                if (text2 == "matrix")
                {
                    if (num2 != 6)
                    {
                        throw new ApplicationException("matrix 参数错误");
                    }
                    System.Drawing.Drawing2D.Matrix matrix1 = new System.Drawing.Drawing2D.Matrix(singleArray1[0], singleArray1[1], singleArray1[2], singleArray1[3], singleArray1[4], singleArray1[5]);
                    this.setMatrix(matrix1);
                }
            Label_0261:;
            }
        }

        public void setMatrix(System.Drawing.Drawing2D.Matrix matrix1)
        {
            this.type = 1;
            this.Matrix.Multiply(matrix1);
        }

        public void setRotate(float angle)
        {
            this.type = 4;
            angle = angle;
            this.matrix.Rotate(angle);
        }

        public void setRotate(float angle, float cx, float cy)
        {
            this.type = 4;
            angle = angle;
            this.matrix.Translate(cx, cy);
            this.matrix.Rotate(angle);
            this.matrix.Translate(-cx, -cy);
        }

        public void setScale(float sx, float sy)
        {
            this.type = 3;
            this.matrix.Scale(sx, sy);
        }

        public void setSkewX(float angle)
        {
            this.type = 5;
            angle = angle;
            this.matrix.Shear((float) Math.Tan((angle * 3.1415926535897931) / 180), 0f);
        }

        public void setSkewY(float angle)
        {
            this.type = 6;
            angle = angle;
            this.matrix.Shear(0f, (float) Math.Tan((angle * 3.1415926535897931) / 180));
        }

        public void setTranslate(float tx, float ty)
        {
            this.type = 2;
            this.matrix.Translate(tx, ty);
        }

        public override string ToString()
        {
            string text3 = "";
            string[] textArray1 = new string[14];
            textArray1[0] = text3;
            textArray1[1] = "matrix(";
            double num1 = Math.Round((double) this.matrix.Elements[0], 2);
            textArray1[2] = num1.ToString();
            textArray1[3] = " ";
            double num2 = Math.Round((double) this.matrix.Elements[1], 2);
            textArray1[4] = num2.ToString();
            textArray1[5] = " ";
            double num3 = Math.Round((double) this.matrix.Elements[2], 2);
            textArray1[6] = num3.ToString();
            textArray1[7] = " ";
            double num4 = Math.Round((double) this.matrix.Elements[3], 2);
            textArray1[8] = num4.ToString();
            textArray1[9] = " ";
            double num5 = Math.Round((double) this.matrix.Elements[4], 2);
            textArray1[10] = num5.ToString();
            textArray1[11] = " ";
            double num6 = Math.Round((double) this.matrix.Elements[5], 2);
            textArray1[12] = num6.ToString();
            textArray1[13] = ")";
            return string.Concat(textArray1);
        }


        // Properties
        public float Angle
        {
            get
            {
                return this.angle;
            }
        }

        public System.Drawing.Drawing2D.Matrix Matrix
        {
            get
            {
                return this.matrix;
            }
            set
            {
                this.matrix = value;
            }
        }

        public short Type
        {
            get
            {
                return this.type;
            }
        }


        // Fields
        private float angle;
        private System.Drawing.Drawing2D.Matrix matrix;
        private short type;
    }
}

