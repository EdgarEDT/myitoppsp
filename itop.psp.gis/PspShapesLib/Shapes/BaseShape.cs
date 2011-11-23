using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Netron.GraphLib.Attributes;
using Netron.GraphLib.UI;
using Netron.GraphLib.Interfaces;
using Netron.GraphLib;
using System.Xml.Serialization;

namespace PspShapesLib
{
    /// <summary>
    /// 任务图元
    /// </summary>
    //[Serializable]
    //[Description("任务")]
    //[NetronGraphShape("任务", "57AF94BA-4129-45dc-0001-000000000001", "电气图元", "PspShapesLib.BaseShape",
    //     "图元")]
    public class BaseShape : Shape, ISerializable
    {
        #region 私有字段
        //图元连接点
        protected Connector leftConnector;
        protected Connector rightConnector;
        protected Connector topConnector;
        protected Connector bottomConnector;

        protected string deviceid;
        protected string devicetype;
        protected int gradation=0;
        private float angle = 0;

        
       
        #endregion

        #region 属性

        [GraphMLData]
        public string DeviceType {
            get { return devicetype; }
            set { devicetype = value; }
        }
        /// <summary>
        /// 设备关联ID
        /// </summary>
        [GraphMLData]
        public string DeviceID {
            get { return deviceid; }
            set { deviceid = value; }
        }
        [GraphMLData]
        public int Grade
        {
            get { return gradation; }
            set { gradation = value; }
        }
        [GraphMLData]
        public virtual float Angle {
            get { return angle; }
            set { angle = value; }
        }
        [GraphMLData]
        public override Color ShapeColor {
            get {
                return base.ShapeColor;
            }
            set {
                base.ShapeColor = value;
            }
        }
        [GraphMLData]
        public override Color TextColor {
            get {
                return base.TextColor;
            }
            set {
                base.TextColor = value;
            }
        }
        
        protected override string FontFamily {
            get {
                return base.FontFamily;
            }
            set {
                base.FontFamily = value;
            }
        }

        //[GraphMLData]
        //[DisplayName("日期")]
        //[Description("任务时间")]
        //[Category("任务块")]
        //public string TaskDate {
        //    get {
        //        return mTaskDate;
        //    }
        //    set {
        //        mTaskDate = value;
        //    }
        //}

        
        #endregion

        #region 对象构造

        public BaseShape()
            : base() {
            ////设置图元初始大小
            //Rectangle = new RectangleF(0, 0, 40, 40);
            ////初始连接点
            //initConnector();
            ////设置可改变大小
            //IsResizable = true;

            //填充色
            this.ShapeColor = Color.White;
            this.Text = "";
            this.Font = new Font("宋体", 9);

        }



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="site"></param>
        public BaseShape(IGraphSite site)
            : base(site) {
            //设置图元初始大小
            Rectangle = new RectangleF(0, 0, 70, 70);
            //初始连接点
            initConnector();
            //设置可改变大小
            IsResizable = true;
        }
        /// <summary>
        /// 序列化构造
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected BaseShape(SerializationInfo info, StreamingContext context)
            : base(info, context) {
            this.deviceid = (string)info.GetValue("deviceid", typeof(string));
            this.devicetype = (string)info.GetValue("devicetype", typeof(string));
            this.angle = (float)info.GetValue("angle", typeof(float));
            this.gradation = (int)info.GetValue("gradation", typeof(int));
        }

        protected virtual void initConnector() {
            //初始连接点
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
        #endregion
       
        
        #region 方法
        public virtual ShapeCollection GetChildShapes() {
            ShapeCollection sc = new ShapeCollection();
            foreach (Connector cr in this.Connectors) {
                foreach (Connection cn in cr.Connections) {
                    sc.Add(cn.From == cr ? cn.To.BelongsTo : cn.From.BelongsTo);
                }
            }
            return sc;
        }
        /// <summary>
        /// 获取变换矩阵
        /// </summary>
        /// <returns></returns>
        public Matrix GetTransForm() {
            Matrix mx = new Matrix();
            mx.RotateAt(Angle, new PointF(Rectangle.Left + Rectangle.Width / 2, Rectangle.Y + Rectangle.Height / 2));
            return mx;
        }
        public void NotifyChanged() {
            this.Invalidate();
        }
        /// <summary>
        /// 图元缩略图
        /// </summary>
        /// <returns></returns>
        public override Bitmap GetThumbnail() {
            Bitmap bmp = null;
            try {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PspShapesLib.Resources.BaseShape.bmp");

                bmp = Bitmap.FromStream(stream) as Bitmap;
                stream.Close();
                stream = null;
            } catch (Exception exc) {
                Trace.WriteLine(exc.Message, "ShapesLib.GetThumbnail");
            }
            return bmp;
        }
        //右键菜单
        public override MenuItem[] ShapeMenu() {
            MenuItem[] items = new MenuItem[2];
            items[0] = new MenuItem("设备参数");
            items[1] = new MenuItem("重置设备");
            items[1].Click += new EventHandler(BaseShape_Click);
            return items;
        }

        void BaseShape_Click(object sender, EventArgs e) {
            this.deviceid = "";
        }

        /// <summary>
        /// 图元绘制方法
        /// </summary>
        /// <param name="g">图元设备</param>
        public override void Paint(Graphics g) {
            base.Paint(g);
            if (ShowLabel) {
                g.DrawString(this.Text, this.Font, this.TextBrush, new PointF(Rectangle.Right + 3, Rectangle.Top + Rectangle.Height / 2));
            }
        }

        /// <summary>
        /// 得到指定连接点的坐标
        /// </summary>
        /// <param name="c">连接点对象</param>
        /// <returns>浮点坐标</returns>
        public override PointF ConnectionPoint(Connector c) {

            if (c == leftConnector) return new PointF(Rectangle.Left, Rectangle.Top + (Rectangle.Height * 1 / 2));
            if (c == rightConnector) return new PointF(Rectangle.Right, Rectangle.Top + Rectangle.Height / 2);
            if (c == topConnector) return new PointF(Rectangle.Left + Rectangle.Width / 2, Rectangle.Top);
            if (c == bottomConnector) return new PointF(Rectangle.Left + Rectangle.Width / 2, Rectangle.Bottom);


            return new PointF(0, 0);
        }        
        protected override void GetPropertyBagValue(object sender, PropertySpecEventArgs e) {
            base.GetPropertyBagValue(sender, e);
            switch (e.Property.Name) {
                case "UID":
                    e.Value = this.UID.ToString();
                    
                    break;
                case "Angle":
                    e.Value = this.angle;

                    break;
            }
        }
        public override void AddProperties() {
            base.AddProperties();
            Bag.Properties.Add(new PropertySpec("UID","标识", typeof(string), "数据", "图元的唯一标识"));
            //Bag.Properties.Add(new PropertySpec("Angle", "角度", typeof(float), "数据", "图元旋转角度"));

            Bag.Properties.Remove("Layer");
            //Bag.Properties.Remove("ShowLabel");
            Bag.Properties.Remove("URL");

        }
        protected override void SetPropertyBagValue(object sender, PropertySpecEventArgs e) {
            base.SetPropertyBagValue(sender, e);
            switch (e.Property.Name) {
                case "Angle":
                    this.Angle =(float)e.Value;
                    break;
            }
        }
        
        
        #endregion

        #region ISerializable 方法
        
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            base.GetObjectData(info, context);
            foreach (Connector cr in this.Connectors) {
                info.AddValue(cr.Name, cr, typeof(Connector));
            }
            info.AddValue("deviceid", this.deviceid, typeof(string));
            info.AddValue("devicetype", this.devicetype, typeof(string));
            info.AddValue("angle", this.angle, typeof(float));
            info.AddValue("gradation", this.gradation, typeof(int));
        }

        #endregion
    }
}