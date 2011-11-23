using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Netron.GraphLib.Attributes;
using System.Drawing;
using Netron.GraphLib;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace PspShapesLib
{
    [Serializable]
    [Description("开关")]
    [NetronGraphShape("开关", "57AF94BA-4129-45dc-0003-000000000001", "电气图元", "PspShapesLib.KGShape",
         "图元")]
    public class KGShape : BaseShape
    {
        public KGShape()
            : base() {
            devicetype = "71";
            //设置图元初始大小
            Rectangle = new RectangleF(0, 0, 15, 15);
            //初始连接点
            initConnector();
            //设置可改变大小
            IsResizable = true;

            this.ShapeColor = Color.Red;
        }
        public override System.Windows.Forms.MenuItem[] ShapeMenu() {
            List<MenuItem> list = new List<MenuItem>();
            
            MenuItem　menu1 = new MenuItem(ShapeColor.ToArgb() == Color.Red.ToArgb() ? "退出运行" : "投入运行");
            menu1.Click += new EventHandler(KGShape_Click);
            list.Add(menu1);
            list.Add(new MenuItem("-"));
            list.AddRange(base.ShapeMenu());
            return list.ToArray() ;
        }

        void KGShape_Click(object sender, EventArgs e) {
            if (((MenuItem)sender).Text == "投入运行")
                ShapeColor = Color.Red;
            else
                ShapeColor = Color.Green;
        }
        protected KGShape(SerializationInfo info, StreamingContext context)
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

            topConnector = new Connector(this, "topconnector", true);
            topConnector.ConnectorLocation = ConnectorLocation.North;
            Connectors.Add(topConnector);

            rightConnector = new Connector(this, "rightconnector", true);
            rightConnector.ConnectorLocation = ConnectorLocation.East;
            Connectors.Add(rightConnector);

            bottomConnector = new Connector(this, "bottomconnector", true);
            bottomConnector.ConnectorLocation = ConnectorLocation.South;
            Connectors.Add(bottomConnector);
        }
        public override void Paint(System.Drawing.Graphics g) {
            base.Paint(g);
            g.FillRectangle(new SolidBrush(this.ShapeColor), this.Rectangle);

        }
        
        public override void AddProperties() {
            base.AddProperties();
            Bag.Properties.Remove("ShapeColor");
        }
    }
}
