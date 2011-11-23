namespace ItopVector.Property
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using ItopVector.Core;
    using ItopVector.Core.Func;
    using ItopVector.Design;
    using ItopVector.Core.Figure;


    internal class PropertyImage : PropertyBase
    {
        // Methods
        public PropertyImage(SvgElement render)
            : base(render)
        {
        }
        [Category("ͼƬ"), Description("ָ������ͼƬ��͸���ȣ���(0-1)���ֱ�ʾ"), Browsable(true), Editor(typeof(NumberEditor), typeof(UITypeEditor))]
        public Struct.Float ͼƬ͸����
        {
            get
            {
                if (base.svgElement != null)
                {
                    float single2 = 1f;
                    object obj1 = AttributeFunc.FindAttribute("opacity", base.svgElement);
                    if (obj1 is float)
                    {
                        single2 = (float)obj1;
                    }

                    return new Struct.Float(single2);
                }
                return new Struct.Float(1f);
            }
            set
            {
                if (base.svgElement != null)
                {
                    string text1 = value.F.ToString();

                    base.SetAttributeValue("opacity", text1);
                }
            }
        }
        public double ԭʼ��С���� {
            get { 
                Image img = base.svgElement as Image;
                double d1 =Math.Round( img.Height / img.RefImage.Height,8);
                double d2 = Math.Round( img.Width / img.RefImage.Width,8);

                return d1 < d2 ? d1 : d2;
            }
            set {
                Image img = base.svgElement as Image;
                
                img.ResetSize();
                //ԭʼ��ȱ��� = value * img.RefImage.Width;
                //ԭʼ�߶ȱ��� = value * img.RefImage.Height;
                base.SetAttributeValue("width", string.Format("{0}", value * img.RefImage.Width));
                base.SetAttributeValue("height", string.Format("{0}", value * img.RefImage.Height));
            }
        }


        public double ԭʼ��ȱ���
        {

            get
            {
                if (base.svgElement != null)
                {
                    Image img = base.svgElement as Image;
                    return Math.Round(img.Width / img.RefImage.Width, 8);
                }
                return 0d;
            }
            set
            {
                if (base.svgElement != null)
                {
                    Image img = base.svgElement as Image;
                    base.SetAttributeValue("width", string.Format("{0}", value * img.RefImage.Width));
                }
            }
        }
        public double ԭʼ�߶ȱ���
        {

            get
            {
                if (base.svgElement != null)
                {
                    Image img = base.svgElement as Image;
                    return Math.Round(img.Height / img.RefImage.Height,8);
                }
                return 0d;
            }
            set
            {

                if (base.svgElement != null)
                {
                    Image img = base.svgElement as Image;
                    base.SetAttributeValue("height", string.Format("{0}", value * img.RefImage.Height));
                }
            }
        }
        public System.Drawing.Color ͸��ɫ
        {
            get
            {
                Image img = base.svgElement as Image;
                return img.ThansparencyKey;
            }

            set
            {

                Image img = base.svgElement as Image;
                img.ThansparencyKey = value;
            }

        }
        public float X����
        {

            get
            {
                if (base.svgElement != null)
                {
                    Image img = base.svgElement as Image;

                    System.Drawing.PointF p1 = new System.Drawing.PointF(img.X, img.Y);
                    System.Drawing.PointF[] pnt = new System.Drawing.PointF[1];
                    pnt[0] = p1;
                    img.Transform.Matrix.TransformPoints(pnt);
                    return pnt[0].X;
                }
                return 0f;
            }
            set
            {
                if (base.svgElement != null)
                {
                    Image img = base.svgElement as Image;
                    //img.Transform.setMatrix(img.GraphTransform.Matrix);
                    img.X = value;
            
                }
            }
        }
        public float Y����
        {

            get
            {
                if (base.svgElement != null)
                {
                    Image img = base.svgElement as Image;

                    System.Drawing.PointF p1 = new System.Drawing.PointF(img.X, img.Y);
                    System.Drawing.PointF[] pnt = new System.Drawing.PointF[1];
                    pnt[0] = p1;
                    img.Transform.Matrix.TransformPoints(pnt);
                    return pnt[0].Y;
                }
                return 0f;
            }
            set
            {
                if (base.svgElement != null)
                {
                    Image img = base.svgElement as Image;
                    img.Y = value;
                    //base.SetAttributeValue("Y", string.Format("{0}", value));
                }
            }
        }
        public string ͼƬ {
            get {
                Image obj = base.svgElement as Image;
                if (obj != null)
                    return obj.BackgroundImageFile;
                else
                    return string.Empty;
            }
            set {
                Image obj = this.svgElement as Image;
                if (obj != null)
                    obj.BackgroundImageFile = value;
            }
        }
        [Browsable(false)]
        public bool �Ƿ���ʾ
        {

            get
            {
                if (base.svgElement != null)
                {
                    Image img = base.svgElement as Image;
                    return img.DrawVisible;
                }
                return true;
            }
            set
            {

                if (base.svgElement != null)
                {
                    Image img = base.svgElement as Image;
                    img.DrawVisible = value;
                }
            }
        }
    }
}

