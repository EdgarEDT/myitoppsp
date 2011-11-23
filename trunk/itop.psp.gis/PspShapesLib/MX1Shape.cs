using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Netron.GraphLib.Attributes;
using System.Drawing;
using Netron.GraphLib;
using System.Runtime.Serialization;

namespace PspShapesLib
{
    [Serializable]
    [Description("横母线")]
    [NetronGraphShape("横母线", "57AF94BA-4129-45dc-0005-000000000001", "电气图元", "PspShapesLib.MX1Shape",
         "图元")]
    public class MX1Shape : BaseShape
    {
        public MX1Shape()
            : base() {
            devicetype = "01";
            //设置图元初始大小
            Rectangle = new RectangleF(0, 0, 150, 4);
            //初始连接点
            initConnector();
            //设置可改变大小
            //IsResizable = false;

            this.ShapeColor = Color.Black;
        }
        int connectorNumber = 20;
        int childNum = 5;
        [GraphMLData]
        public int ConnectorNumber {
            get { return childNum; }
            set {
                if (value > 20 || value < 5) return;

                childNum = value;
            }
        }
        public override float Width {
            get {
                return base.Width;
            }
            set {
                base.Width = value;
            }
        }
        protected override void GetPropertyBagValue(object sender, PropertySpecEventArgs e) {
            base.GetPropertyBagValue(sender, e);
            switch (e.Property.Name) {
                case "ConnectorNumber":
                    e.Value = this.ConnectorNumber;
                    break;
            }
        }
        protected override void SetPropertyBagValue(object sender, PropertySpecEventArgs e) {
            base.SetPropertyBagValue(sender, e);
            switch (e.Property.Name) {
                case "ConnectorNumber":
                    this.ConnectorNumber =(int) e.Value;
                    break;
            }
        }
        public override void AddProperties() {
            base.AddProperties();
            if (!Bag.Properties.Contains("ConnectorNumber"))
                Bag.Properties.Add(new PropertySpec("ConnectorNumber", "连接点", typeof(int), "数据", "图元连接点"));

        }
        protected MX1Shape(SerializationInfo info, StreamingContext context)
            : base(info, context) {
            childNum = (int)info.GetValue("childNum", typeof(int));
            for (int i = 1; i <= connectorNumber; i++) {
                try {
                    Connector cr = (Connector)info.GetValue("top" + i, typeof(Connector));
                    cr.BelongsTo = this;
                    Connectors.Add(cr);
                } catch { }

                try {
                    Connector cr = (Connector)info.GetValue("bottom" + i, typeof(Connector));
                    cr.BelongsTo = this;
                    Connectors.Add(cr);
                } catch { }
            }


        }

        protected override void initConnector() {

            for (int i = 1; i <= connectorNumber; i++) {
                Connector cr = new Connector(this, "top" + i, true);
                cr.ConnectorLocation = ConnectorLocation.North;
                Connectors.Add(cr);
                cr = new Connector(this, "bottom" + i, true);
                cr.ConnectorLocation = ConnectorLocation.South;
                Connectors.Add(cr);
            }
        }
        /// <summary>
        /// 得到指定连接点的坐标
        /// </summary>
        /// <param name="c">连接点对象</param>
        /// <returns>浮点坐标</returns>
        public override PointF ConnectionPoint(Connector c) {
            float f1 = Math.Max(15, Rectangle.Width / (childNum+1));

            if (c.Name.StartsWith("top")) {
                int num = int.Parse(c.Name.Substring(3));
                PointF pf = new PointF(Math.Min(Rectangle.X + f1 * num, Rectangle.Right), Rectangle.Y);
                return pf;
            }
            if (c.Name.StartsWith("bottom")) {
                int num = int.Parse(c.Name.Substring(6));
                PointF pf = new PointF(Math.Min(Rectangle.X + f1 * num, Rectangle.Right), Rectangle.Bottom);
                return pf;
            }

            return new PointF(-10, -10);
        }
        public override void Paint(System.Drawing.Graphics g) {
            base.Paint(g);
            g.FillRectangle(new SolidBrush(this.ShapeColor), this.Rectangle);

        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            base.GetObjectData(info, context);
            info.AddValue("childNum", this.connectorNumber, typeof(int));
        }
    }
}
