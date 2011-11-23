using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Netron.GraphLib.Attributes;
using System.Drawing;
using Netron.GraphLib;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PspShapesLib
{
    [Serializable]
    [Description("线路")]
    [NetronGraphShape("线路", "57AF94BA-4129-45dc-0007-000000000001", "电气图元", "PspShapesLib.XLShape",
         "图元")]
    public class XLShape : BaseShape
    {
        public XLShape()
            : base() {
            devicetype = "05";
            //设置图元初始大小
            Rectangle = new RectangleF(0, 0, 12, 20);
            //初始连接点
            initConnector();
            //设置可改变大小
            //IsResizable = true;

            this.ShapeColor = Color.Black;
        }

        public override float Angle {
            get {
                return base.Angle;
            }
            set {
                base.Angle = value;
                int n = (int)value % 360;
                switch (n) {
                    case 0:
                        setConnection(bottomConnector);
                        break;
                    case 90:
                        setConnection(leftConnector);
                        break;
                    case 180:
                        setConnection(topConnector);
                        break;
                    case 270:
                        setConnection(rightConnector);
                        break;
                }
            }
        }
        private void setConnection(Connector cor) {
            foreach (Connector r in this.Connectors) {
                if (r.Connections.Count > 0) {
                    foreach (Connection c in r.Connections) {
                        if (c.From == r)
                            c.From = cor;
                        else
                            c.To = cor;
                    }
                }
            }
        }
        protected XLShape(SerializationInfo info, StreamingContext context)
            : base(info, context) {
            leftConnector = (Connector)info.GetValue("leftconnector", typeof(Connector));
            rightConnector = (Connector)info.GetValue("rightconnector", typeof(Connector));
            topConnector = (Connector)info.GetValue("topconnector", typeof(Connector));
            bottomConnector = (Connector)info.GetValue("bottomconnector", typeof(Connector));

            leftConnector.BelongsTo = this;
            rightConnector.BelongsTo = this;
            topConnector.BelongsTo = this;
            bottomConnector.BelongsTo = this;

            Connectors.Add(leftConnector);
            Connectors.Add(rightConnector);
            Connectors.Add(topConnector);
            Connectors.Add(bottomConnector);
        }
        protected override void initConnector() {
            leftConnector = new Connector(this, "leftconnector", true);
            leftConnector.ConnectorLocation = ConnectorLocation.West;
            Connectors.Add(leftConnector);

            rightConnector = new Connector(this, "rightconnector", true);
            rightConnector.ConnectorLocation = ConnectorLocation.East;
            Connectors.Add(rightConnector);

            topConnector = new Connector(this, "topconnector", true);
            topConnector.ConnectorLocation = ConnectorLocation.North;
            Connectors.Add(topConnector);

            bottomConnector = new Connector(this, "bottomconnector", true);
            bottomConnector.ConnectorLocation = ConnectorLocation.South;
            Connectors.Add(bottomConnector);
        }
        public override void Paint(System.Drawing.Graphics g) {
            //base.Paint(g);
            GraphicsPath gp = new GraphicsPath();
            PointF pf1 = new PointF(Rectangle.Left + Width / 2, Rectangle.Top);
            PointF pf2 = new PointF(Rectangle.Left, Rectangle.Bottom);
            PointF pf3 = new PointF(Rectangle.Right, Rectangle.Bottom);
            PointF[] pfs = new PointF[] { pf1, pf2, pf3 };
            GetTransForm().TransformPoints(pfs);
            gp.AddLines(pfs);
            g.FillPath(new SolidBrush(ShapeColor), gp);

            //显示线路名
            if (ShowLabel) {
                Matrix m = new Matrix();
                int mod = (int)Angle % 360;
                PointF pf5 = new PointF(Rectangle.Right + 3, Rectangle.Top + Rectangle.Height / 2);
                if (mod == 0 || mod == 180) {
                    pf5 = new PointF(Rectangle.Right + 12, Rectangle.Bottom);
                    m.RotateAt(90, pf5);
                }
                GraphicsContainer gc = g.BeginContainer();
                g.Transform = m;
                g.DrawString(this.Text, this.Font, this.TextBrush, pf5);
                g.EndContainer(gc);
            }
        }
        public override MenuItem[] ShapeMenu() {
            
            List<MenuItem> list = new List<MenuItem>();
            list.AddRange(base.ShapeMenu());
            list.Add(new MenuItem("-"));
            list.Add(new MenuItem("定位到地理信息图"));

            return list.ToArray();
        }

        public override void AddProperties() {
            base.AddProperties();
            if (!Bag.Properties.Contains("Angle"))
                Bag.Properties.Add(new PropertySpec("Angle", "角度", typeof(float), "数据", "图元旋转角度"));

        }
    }
}
