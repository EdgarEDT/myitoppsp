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
    [Description("开闭所")]
    [NetronGraphShape("开闭所", "57AF94BA-4129-45dc-0056-000000000001", "电气图元", "PspShapesLib.KBSShape",
         "图元")]
    public class KBSShape : BaseShape
    {
        public KBSShape()
            : base() {
            devicetype = "56";
            //设置图元初始大小
            Rectangle = new RectangleF(0, 0,30, 30);
            //初始连接点
            initConnector();
            //设置可改变大小
            IsResizable = true;

            this.ShapeColor = Color.Red;
        }
        int connectorNumber = 5;
        int childNum = 5;
        public override System.Windows.Forms.MenuItem[] ShapeMenu() {
            List<MenuItem> list = new List<MenuItem>();
            
            MenuItem　menu1 = new MenuItem(ShapeColor.ToArgb() == Color.Red.ToArgb() ? "退出运行" : "投入运行");
            menu1.Click += new EventHandler(KBSShape_Click);
            list.Add(menu1);
            list.Add(new MenuItem("-"));
            list.AddRange(base.ShapeMenu());
            return list.ToArray() ;
        }

        void KBSShape_Click(object sender, EventArgs e)
        {
            if (((MenuItem)sender).Text == "投入运行")
                ShapeColor = Color.Red;
            else
                ShapeColor = Color.Green;
        }
        protected KBSShape(SerializationInfo info, StreamingContext context)
            : base(info, context) {
                for (int i = 1; i <= connectorNumber; i++)
                {     

                    try
                    {
                        Connector cr = (Connector)info.GetValue("bottom" + i, typeof(Connector));
                        cr.BelongsTo = this;
                        Connectors.Add(cr);
                    }
                    catch { }
                }
            //leftConnector = (Connector)info.GetValue("leftconnector", typeof(Connector));
            //rightConnector = (Connector)info.GetValue("rightconnector", typeof(Connector));
            //topConnector = (Connector)info.GetValue("topconnector", typeof(Connector));
            //bottomConnector = (Connector)info.GetValue("bottomconnector", typeof(Connector));

            //leftConnector.BelongsTo = this;
            //rightConnector.BelongsTo = this;
            //topConnector.BelongsTo = this;
            //bottomConnector.BelongsTo = this;

            //Connectors.Add(leftConnector);
            //Connectors.Add(rightConnector);
            //Connectors.Add(topConnector);
            //Connectors.Add(bottomConnector);
        }
        protected override void initConnector() {
            //leftConnector = new Connector(this, "leftconnector", true);
            //leftConnector.ConnectorLocation = ConnectorLocation.West;
            //Connectors.Add(leftConnector);

            //topConnector = new Connector(this, "topconnector", true);
            //topConnector.ConnectorLocation = ConnectorLocation.North;
            //Connectors.Add(topConnector);

            //rightConnector = new Connector(this, "rightconnector", true);
            //rightConnector.ConnectorLocation = ConnectorLocation.East;
            //Connectors.Add(rightConnector);

            //bottomConnector = new Connector(this, "bottomconnector", true);
            //bottomConnector.ConnectorLocation = ConnectorLocation.South;
            //Connectors.Add(bottomConnector);
            for (int i = 1; i <= connectorNumber; i++)
            {
                //Connector cr = new Connector(this, "top" + i, true);
                //cr.ConnectorLocation = ConnectorLocation.North;
                //Connectors.Add(cr);
                Connector cr = new Connector(this, "bottom" + i, true);
                cr.ConnectorLocation = ConnectorLocation.South;
                Connectors.Add(cr);
            }
        }
        /// <summary>
        /// 得到指定连接点的坐标
        /// </summary>
        /// <param name="c">连接点对象</param>
        /// <returns>浮点坐标</returns>
        public override PointF ConnectionPoint(Connector c)
        {
            float f1 =((this.Rectangle.Right - this.Rectangle.Width / 10)-( this.Rectangle.X + this.Rectangle.Width / 10))/(childNum+1);

            //if (c.Name.StartsWith("top"))
            //{
            //    int num = int.Parse(c.Name.Substring(3));
            //    PointF pf = new PointF(Math.Min(Rectangle.X + f1 * num, Rectangle.Right), Rectangle.Y);
            //    return pf;
            //}
            if (c.Name.StartsWith("bottom"))
            {
                int num = int.Parse(c.Name.Substring(6));
                PointF pf = new PointF((this.Rectangle.X + this.Rectangle.Width / 10 + f1*num ),(this.Rectangle.Y + this.Rectangle.Height / 10));
                return pf;
            }

            return new PointF(-5, -5);
        }
        public override void Paint(System.Drawing.Graphics g) {
            base.Paint(g);
            //g.DrawRectangle(new Pen(this.ShapeColor), new Rectangle(0,0,20,20)); 
            //g.FillRectangle(new SolidBrush(this.ShapeColor), this.Rectangle);
            GraphicsPath gp = new GraphicsPath();
            gp.AddRectangle(Rectangle);
            g.DrawPath(new Pen(ShapeColor), gp);
            gp.AddLine(new Point((int)(this.Rectangle.X + this.Rectangle.Width / 10),(int)(this.Rectangle.Y + this.Rectangle.Height /10)), new Point((int)(this.Rectangle.Right - this.Rectangle.Width / 10), (int)(this.Rectangle.Y + this.Rectangle.Height / 10)));
            g.DrawPath(new Pen(ShapeColor), gp);
        }       
        
        public override void AddProperties() {
            base.AddProperties();
            Bag.Properties.Remove("ShapeColor");
        }
    }
}
