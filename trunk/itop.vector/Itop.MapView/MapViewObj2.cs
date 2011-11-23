using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using Itop.MapView.Tables;
using System.Drawing.Imaging;

namespace Itop.MapView
{
    /// <summary>
    /// 灵图
    /// </summary>
    public class MapViewObj2 : Itop.MapView.IMapViewObj
    {
        #region 字段
        //static string[] strMiles = new string[] { "1800", "800", "400", "200", "100", "40", "20", "10", "4", "2", "1", "0.4", "0.2", "0.1" };
        string[] strMiles = new string[] { "629.15", "314.57", "157.29", "78.64", "39.32", "19.66", "9.83", "4.92", "2.46", "1.23", "0.614", "0.31", "0.15", "0.08","0.04","0.02","0.01","0.005" };
        //string[] strLevel = new string[] { "W", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
        //string strImgsvrUrl = "http://www.tonli.com/mapview/";
        private List<ClassImage> listImages = new List<ClassImage>();
        public event DownCompleteEventHandler OnDownCompleted;
        int scaleLevel = 2;//缩放等级
        //double longgitude = 117.79m;//经度
        //double latitude = 30.89m;//纬度
        double longgitude = 122.79;//经度
        double latitude = 41.85;//纬度
        int nPerWidth = 200;
        LongLat zeroLongLat = new LongLat(117.740, 30.916);
        //double zeroLong = 117.7105m;//坐标圆点经度
        //double zeroLat = 31.0152m;//坐标圆点纬度
        IDataHelper dataHelper = new DataHelper();

        public IDataHelper DataHelper
        {
            get { return dataHelper; }
            set { dataHelper = value; }
        }
        DataHelper dataHelper2 = new DataHelper();
        int imgSize = 200;
        int baseUnits = 256;
        string imgURLs = "http://dimg.51ditu.com/";
        int methodConfig = 8;
        int zoom = 2;
        int Long = 11640969;
        int Lat = 3989945;
        #endregion 字段
        #region 属性
        public int ScaleLevel {
            get { return scaleLevel; }
        }
        public double Longgitude
        {
            get { return longgitude; }
            set { longgitude = value; }
        }
        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        public  LongLat ZeroLongLat
        {
            get { return zeroLongLat; }
            set { zeroLongLat = value; }

        }

        #endregion

        public MapViewObj2()
        {
            //DataHelper.IsDownMap = true;
            //dataHelper.RefreshData();
            //dataHelper.ImgPath = "LTData.yap";
            dataHelper.ImgsvrUrl = "http://dimg.51ditu.com/";
        }
        Control gispainter;
        //public MapViewObj2(Control painter)
        //{
        //    gispainter = painter;
        //    DataHelper.OnDownComplete = new DownCompleteDelegate(downloadcomplete);
        //}
       
        public int Getlevel(float ScaleUnit)
        {
            int nScale=7;
            switch (Convert.ToInt32(ScaleUnit * 1000000))
            {
                case 1900:
                    nScale = 2;
                    break;
                case 3900:
                    nScale = 3;
                    break;
                case 7800:
                    nScale = 4;
                    break;
                case 15600:
                    nScale = 5;
                    break;
                case 31200:
                    nScale = 6;
                    break;
                case 62500:
                    nScale = 7;
                    break;
                case 125000:
                    nScale = 8;
                    break;
                case 250000:
                    nScale = 9;
                    break;
                case 500000:
                    nScale = 10;
                    break;
                case 1000000:
                    nScale = 11;
                    break;
                case 2000000:
                    nScale = 12;
                    break;
                case 4000000:
                    nScale = 13;
                    break;
                case 8000000:
                    nScale = 13;
                    break;
                default:
                    return -1;
            }
            return 13-nScale;
        }
        public string GetMiles(int scale)
        {

            return strMiles[13-scale];
        }
        public double CountLength(PointF p1, PointF p2)
        {
            return Math.Round((Math.Sqrt(Math.Pow((p1.X - p2.X), 2) + Math.Pow((p1.Y - p2.Y), 2)) / 60) * 0.31, 4);
        }

        string getMapImagesUrl(int iw, int ow, int pw)
        {
            pw += 8 - methodConfig;
            int aw = (int)Math.Ceiling((12 - pw) / 4d);
            int sw = 0, dw = 0, fw = 0;
            string gw = "";
            for (int hw = 0; hw < aw; hw++)
            {
                int jw = 1 << (4 * (aw - hw));
                int kw = ((iw - sw * fw) / jw);
                int lw = ((ow - dw * fw) / jw);
                gw += ((kw > 9) ? kw.ToString() : "0" + kw.ToString()) + "" + ((lw > 9) ? lw.ToString() : "0" + lw.ToString()) + "/";
                sw = kw;
                dw = lw;
                fw = jw;

            };
            string zw = imgURLs;
            double xw = (((iw) & ((1 << 20) - 1)) + (((ow) & ((1 << 20) - 1)) * Math.Pow(2, 20)) + (((pw) & ((1 << 8) - 1)) * Math.Pow(2, 40)));
            return pw + "/" + gw + xw + ".png";
        }
        int[] toMapId(int[] iw, int ow)
        {
            int pw = baseUnits * (int)Math.Pow(2, ow);
            int aw = (int)(iw[0] / pw);
            int sw = (int)(iw[1] / pw);
            return new int[] { aw, sw, (iw[0] - aw * pw) * imgSize / pw, (iw[1] - sw * pw) * imgSize / pw };

        }
        public void Paint(Graphics g, int width, int height, int nScaleLevel, double fLongitude, double fLatitude, ImageAttributes imageAttributes)
        {
            longgitude = fLongitude;
            latitude = fLatitude;
            scaleLevel = nScaleLevel;
            
            int[] iw = new int[] { Convert.ToInt32(fLongitude * 100000), Convert.ToInt32(fLatitude * 100000) };
            double sw = Math.Pow(2, scaleLevel) * 256 / imgSize;
            int[] gw = toMapId(iw, scaleLevel);
            int hw = imgSize;
            int jw = gw[0] - Convert.ToInt32(Math.Ceiling((width / 2d - gw[2]) / hw));
            int kw = gw[1] - Convert.ToInt32(Math.Ceiling((height / 2d - gw[3]) / hw));
            int lw = gw[0] + Convert.ToInt32(Math.Ceiling((width / 2d + gw[2]) / hw) - 1);
            int zw = gw[1] + Convert.ToInt32(Math.Ceiling((height / 2d + gw[3]) / hw) - 1);

            double[] cw = new double[] { -iw[0] / sw, iw[1] / sw };  

            for (int mw = jw; mw <= lw; mw++)
            {
                for (int _w = kw; _w <= zw; _w++)
                {

                    int nPicLeft = (mw * imgSize) + Convert.ToInt32(cw[0]) + width / 2;
                    int nPicTop = (-1 - _w) * imgSize + Convert.ToInt32(cw[1]) + height / 2;

                    string strPic = getMapImagesUrl(mw, _w, scaleLevel);
                    //不在显示范围内
                    //if (nPicLeft < -nPerWidth || nPicLeft > width || nPicTop > height || nPicTop < -nPerWidth) {
                    //    continue;
                    //}
                    ClassImage img = FindImage(strPic);

                    if (img == null)
                    {
                        img = dataHelper.GetImage(strPic);
                        if (img == null)
                        {
                            img = new ClassImage();
                            img.PicUrl = strPic;
                        }
                        listImages.Add(img);
                    }
                    img.IsDiscard = false;
                    img.Left = nPicLeft;
                    img.Top = nPicTop;
                    Rectangle rect=new Rectangle(img.Left,img.Top,1000,1000);
                    if (img.PicImage != null && g != null)
                        //g.DrawImage(img.PicImage, img.Left, img.Top, nPerWidth, nPerWidth);
                        g.DrawImage(img.PicImage, rect, 0, 0, 1000,1000, GraphicsUnit.Pixel, imageAttributes);
                    
                }

            }


            double f1 = 0.00582;
            double f2 = 0.00205;
            //d1 = d1 - Convert.ToDecimal(f1);
            //d2 = d2 - Convert.ToDecimal(f2);
            double loggitude1 = longgitude - f1;
            double latitude1 = latitude - f2;

            //string s = string.Format("经{0}：纬{1}", loggitude1, latitude1);
            //g.DrawString(s, new Font("宋体", 10), Brushes.Red, 0, 0);

        }
        public void Paint2(Graphics g, int width, int height, int nScaleLevel, double fLongitude, double fLatitude, ImageAttributes imageAttributes)
        {
            longgitude = fLongitude;
            latitude = fLatitude;
            scaleLevel = nScaleLevel;
            //if (nScaleLevel == 0)
            //{
            //    longgitude -= Convert.ToDecimal(0.12288);
            //    latitude += Convert.ToDecimal(0.1496);
            //}
            int[] iw = new int[] { Convert.ToInt32(fLongitude * 100000), Convert.ToInt32(fLatitude * 100000) };
            double sw = Math.Pow(2, scaleLevel) * 256 / imgSize;
            int[] gw = toMapId(iw, scaleLevel);
            int hw = imgSize;
            int jw = gw[0] - Convert.ToInt32(Math.Ceiling((width / 2d - gw[2]) / hw));
            int kw = gw[1] - Convert.ToInt32(Math.Ceiling((height / 2d - gw[3]) / hw));
            int lw = gw[0] + Convert.ToInt32(Math.Ceiling((width / 2d + gw[2]) / hw) - 1);
            int zw = gw[1] + Convert.ToInt32(Math.Ceiling((height / 2d + gw[3]) / hw) - 1);

            double[] cw = new double[] { -iw[0] / sw, iw[1] / sw };


            if (listImages.Count > 50) {
                listImages.Clear();
                GC.Collect();
            }

            foreach (ClassImage img in listImages) {
                img.IsDiscard = true;
            }

            for (int mw = jw; mw <= lw; mw++)
            {
                for (int _w = kw; _w <= zw; _w++)
                {

                    int nPicLeft = (mw * imgSize) + Convert.ToInt32(cw[0]) + width / 2;
                    int nPicTop = (-1 - _w) * imgSize + Convert.ToInt32(cw[1]) + height / 2;

                    string strPic = getMapImagesUrl(mw, _w, scaleLevel);
                    //不在显示范围内
                    //if (nPicLeft < -nPerWidth || nPicLeft > width || nPicTop > height || nPicTop < -nPerWidth) {
                    //    continue;
                    //}
                    ClassImage img = FindImage(strPic);

                    if (img == null)
                    {
                        img = dataHelper.GetImage(strPic);
                        if (img == null)
                        {
                            img = new ClassImage();
                            img.PicUrl = strPic;
                        }
                        listImages.Add(img);
                    }
                    img.IsDiscard = false;
                    img.Left = nPicLeft;
                    img.Top = nPicTop;
                    Rectangle rect = new Rectangle(img.Left, img.Top, 1000, 1000);
                    if (img.PicImage != null && g != null)
                        //g.DrawImage(img.PicImage, img.Left, img.Top, nPerWidth, nPerWidth);
                        g.DrawImage(img.PicImage, rect, 0, 0, 1000, 1000, GraphicsUnit.Pixel, imageAttributes);
         ////           g.DrawRectangle(Pens.Red, img.Left, img.Top, nPerWidth, nPerWidth);
                    string s = img.PicUrl;//图片名
         ////           g.DrawString(s, new Font("宋体", 10), Brushes.Red, new Rectangle(img.Left, img.Top, 200, 200));
                }
            }


            double f1 = 0.00582;
            double f2 = 0.00205;
            //d1 = d1 - Convert.ToDecimal(f1);
            //d2 = d2 - Convert.ToDecimal(f2);
            double loggitude1 = longgitude - f1;
            double latitude1 = latitude - f2;

            //string s = string.Format("经{0}：纬{1}", loggitude1, latitude1);
            //g.DrawString(s, new Font("宋体", 10), Brushes.Red, 0, 0);

        }
        
        public Image CreateMap(int width, int height, int nScaleLevel, double fLongitude, double fLatitude)
        {
            Image image1 = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image1);
            ImageAttributes imageAttributes=new ImageAttributes();
            //Color color = ColorTranslator.FromHtml("#F4F4FB");
            //imageAttributes.SetColorKey(color, color);
            Paint2(g, width, height, nScaleLevel, fLongitude, fLatitude,imageAttributes);
            return image1;
        }
        /// <summary>
        /// 返回偏移指定象素后的经纬度
        /// </summary>
        /// <param name="x">经度偏移象素</param>
        /// <param name="y">纬度编移象素</param>
        /// <returns></returns>
        public LongLat OffSet(int x, int y)
        {
            double d1 = longgitude - (x * Math.Pow(2, scaleLevel) * 256 / 200 / 100000);
            double d2 = latitude + (y * Math.Pow(2, scaleLevel) * 256 / 200 / 100000);
            return new LongLat(d1, d2);
        }
        public LongLat ParseToLongLat(int x, int y)
        {
            double d1 = ZeroLongLat.Longitude + (x * Math.Pow(2, 2) * 256 / 200 / 100000);
            double d2 = ZeroLongLat.Latitude - (y * Math.Pow(2, 2) * 256 / 200 / 100000);
            return new LongLat(d1, d2);
        }
        public LongLat ParseToLongLat(float x, float y) {
            double d1 = ZeroLongLat.Longitude + (x * Math.Pow(2, 2) * 256 / 200 / 100000);
            double d2 = ZeroLongLat.Latitude - (y * Math.Pow(2, 2) * 256 / 200 / 100000);
            return new LongLat(d1, d2);
        }
        public LongLat OffSetZero(float x, float y)
        {
            double d1 = ZeroLongLat.Longitude - (x * Math.Pow(2, scaleLevel) * 256 / 200 / 100000);
            double d2 = ZeroLongLat.Latitude + (y * Math.Pow(2, scaleLevel) * 256 / 200 / 100000);
            return new LongLat(d1, d2);
        }
        public LongLat OffSetZero(int x, int y)
        {
            double d1 = ZeroLongLat.Longitude - (x * Math.Pow(2, scaleLevel) * 256 / 200 / 100000);
            double d2 = ZeroLongLat.Latitude + (y * Math.Pow(2, scaleLevel) * 256 / 200 / 100000);
            return new LongLat(d1, d2);
        }
        public  LongLat OffSetZero(int level,float x, float y)
        {
            double d1 = ZeroLongLat.Longitude - (x * Math.Pow(2, level) * 256 / 200 / 100000);
            double d2 = ZeroLongLat.Latitude + (y * Math.Pow(2, level) * 256 / 200 / 100000);
            return new LongLat(d1, d2);
        }
        public PointF ParseToPoint(double _Longitude, double _Latitude)
        {
            double x = (-ZeroLongLat.Longitude + _Longitude) / ((Math.Pow(2, scaleLevel) * 256 / 200 / 100000));//*nScale[scaleLevel];
            double y = (-_Latitude + ZeroLongLat.Latitude) / ((Math.Pow(2, scaleLevel) * 256 / 200 / 100000));
            return new PointF((float)x, (float)y);
        }
        public IntXY getXY(double _Longitude, double _Latitude)
        {
            double x = (ZeroLongLat.Longitude - _Longitude) / ((Math.Pow(2, scaleLevel) * 256 / 200 / 100000));
            double y = (_Latitude - ZeroLongLat.Latitude) / ((Math.Pow(2, scaleLevel) * 256 / 200 / 100000));
            return new IntXY(x, y);
        }
        public LongLat OffSet(LongLat longlat1, int scale, int x, int y)
        {
            double d1 = longlat1.Longitude - (x*Math.Pow(2, scale) * 256 / 200 / 100000);
            double d2 = longlat1.Latitude + (y * Math.Pow(2, scale) * 256 / 200 / 100000);
            return new LongLat(d1, d2);
        }
        public LongLat OffSet(LongLat longlat1, int scale, float x, float y)
        {
            double newscale = Scaleoffset(scale);
            double d1 = longlat1.Longitude - (x *newscale);
            double d2 = longlat1.Latitude + (y * newscale);
            return new LongLat(d1, d2);
            // double f1 = 0.00578;
            //double f2 = 0.00205;
            //d1 = d1 - Convert.ToDecimal(f1);
            //d2 = d2 - Convert.ToDecimal(f2);
        }        
        public double Scaleoffset(int scale)
        {
            double f11 = Math.Pow(2, scale) * 256 / 200 / 100000;                       
            return f11;
        }
        public string[] ScaleRange()
        {
            return new string[] { "400%", "200%", "100%", "50%", "25%", "12.5%", "6.25%", "3.125%", "1.5625%" };
        }
        public static string[] ScaleRanges{
            get { return new string[] { "800%", "400%", "200%", "100%", "50%", "25%", "12.5%", "6.25%", "3.125%", "1.5625%" }; }
        }

        #region 私有方法

        void Paint(Graphics g, int width, int height, int nScaleLevel, Point centerPoint)
        {

            //double dlong = zeroLong - (centerPoint.X * d2H[scaleLevel] / nPerWidth);
            //double dlat = zeroLat + (centerPoint.Y * qxl3[scaleLevel] / nPerWidth);
            //Paint(g, width, height, nScaleLevel, dlong, dlat);
        }
        private ClassImage FindImage(string strPic)
        {
            foreach (ClassImage img in listImages)
            {
                if (img.PicUrl == strPic)
                {
                    return img;
                }
            }
            return null;
        }
        private void downloadcomplete(MapClass obj)
        {
            if (gispainter != null)
            {
                ClassImage img = FindImage(obj.PicUrl);
                if (img != null)
                {
                    if (obj.Stream != null)
                    {
                        img.PicImage = Bitmap.FromStream(new MemoryStream(obj.Stream)); //DataHelper.GetImage(obj.PicUrl).PicImage;
                    }
                    if (Invalidate != null)
                    {
                        Invalidate(new Rectangle(img.Left, img.Top, 200, 200), true);
                    }
                }
            }
        }
        public invalidateDelegate Invalidate;
        #endregion

        //internal void Remove(Point p)
        //{
        //    foreach (ClassImage img in listImages)
        //    {
        //        if (!img.IsDiscard)
        //        {
        //            Rectangle rt = new Rectangle(img.Left, img.Top, 300, 300);
        //            if (rt.Contains(p))
        //                DataHelper.Delete(img.PicUrl);
        //        }
        //    }
        //}
        //internal void Remove(string strPic)
        //{
        //    DataHelper.Delete(strPic);
        //}
        //internal void Remove(ClassImage cImage)
        //{
        //    listImages.Remove(cImage);
        //    DataHelper.Delete(cImage.PicUrl);
        //}
        public ClassImage FindImage(Point p)
        {
            foreach (ClassImage img in listImages)
            {
                if (!img.IsDiscard)
                {
                    Rectangle rt = new Rectangle(img.Left, img.Top, 200, 200);
                    if (rt.Contains(p))
                        return img;
                }
            }
            return null;
        }
        //internal void Remove()
        //{
        //    int j = listImages.Count - 1;
        //    for (int i = j; i > 0; i--)
        //    {
        //        ClassImage img = listImages[i];
        //        if (!img.IsDiscard)
        //        {

        //            DataHelper.Delete(img.PicUrl);
        //            //listImages.RemoveAt(i);
        //        }

        //    }

        #region IMapViewObj 成员


        public List<ClassImage> GetMapList(int width, int height, PointF center) {
            LongLat longlat = LongLat.Empty;

            longlat = ParseToLongLat((int)(center.X), (int)(center.Y));

            return GetMapList(width, height, ScaleLevel, longlat.Longitude, longlat.Latitude);

        }
        public List<ClassImage> GetMapList(int width, int height, int nScaleLevel, double fLongitude, double fLatitude) {
            longgitude = fLongitude;
            latitude = fLatitude;

            int[] iw = new int[] { Convert.ToInt32(fLongitude * 100000), Convert.ToInt32(fLatitude * 100000) };
            double sw = Math.Pow(2, scaleLevel) * 256 / imgSize;
            int[] gw = toMapId(iw, scaleLevel);
            int hw = imgSize;
            int jw = gw[0] - Convert.ToInt32(Math.Ceiling((width / 2d - gw[2]) / hw));
            int kw = gw[1] - Convert.ToInt32(Math.Ceiling((height / 2d - gw[3]) / hw));
            int lw = gw[0] + Convert.ToInt32(Math.Ceiling((width / 2d + gw[2]) / hw) - 1);
            int zw = gw[1] + Convert.ToInt32(Math.Ceiling((height / 2d + gw[3]) / hw) - 1);
            double[] cw = new double[] { -iw[0] / sw, iw[1] / sw };

            List<ClassImage> retlist = new List<ClassImage>();
            for (int mw = jw; mw <= lw; mw++) {
                for (int _w = kw; _w <= zw; _w++) {

                    int nPicLeft = (mw * imgSize) + Convert.ToInt32(cw[0]) + width / 2;
                    int nPicTop = (-1 - _w) * imgSize + Convert.ToInt32(cw[1]) + height / 2;

                    string strPic = getMapImagesUrl(mw, _w, scaleLevel);

                    ClassImage image = dataHelper.GetImage(strPic);
                    image.PicWidth = nPerWidth;
                    image.Left = nPicLeft;
                    image.Top = nPicTop;
                    if (image.PicImage == null) {

                    }
                    retlist.Add(image);                   
                }
            }

            return retlist;
        }

        public void ClearBuff() {
            listImages.Clear();
        }

        public void SetImage(ClassImage image) {
            dataHelper.SetImage(image);
        }

        public int PicWidth {
            get {
                return nPerWidth;
            }
            set {
                nPerWidth = value;
            }
        }

        public IList<ClassImage> GetMapList(int width, int height, Point point) {
            LongLat longlat = LongLat.Empty;

            longlat = ParseToLongLat((int)(point.X), (int)(point.Y));

            return GetMapList(width, height, ScaleLevel, longlat.Longitude, longlat.Latitude);
        }

        public int Getlevel(float ScaleUnit, out float scale) {
            int nScale = -1;
            scale = 1;
            if (ScaleUnit >= 4) { nScale = 0; scale = ScaleUnit / 4; } 
            else if (ScaleUnit >= 2) { nScale = 1; scale = ScaleUnit / 2; } 
            else if (ScaleUnit >= 1) { nScale = 2; scale = ScaleUnit / 1; } 
            else if (ScaleUnit >= 0.5) { nScale = 3; scale = ScaleUnit / .4f; } 
            else if (ScaleUnit >= 0.25) { nScale = 4; scale = ScaleUnit / .2f; } 
            else if (ScaleUnit >= 0.125) { nScale = 5; scale = ScaleUnit / .1f; } 
            else if (ScaleUnit >= 0.0625) { nScale = 6; scale = ScaleUnit / .04f; } 
            else if (ScaleUnit >= 0.03125) { nScale = 7; scale = ScaleUnit / .02f; } 
            else if (ScaleUnit >= 0.0015625) { nScale = 8; scale = ScaleUnit / .01f; } 
            else return -1;

            return nScale;
        }

        #endregion

        #region IMapViewObj 成员


        public int Getlevel2(float p, out float nn) {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion


        #region IMapViewObj 成员


        public PointF ParseToPoint(decimal _Longitude, decimal _Latitude) {
            return ParseToPoint((double)_Longitude, (double)_Latitude);
        }

        #endregion

        #region IMapViewObj 成员


        public IntXY getXY(decimal _Longitude, decimal _Latitude) {
            return getXY((double)_Longitude, (double)_Latitude);
        }

        #endregion

        #region IMapViewObj 成员


        public double CountLength(PointF[] pts) {
            throw new Exception("The method or operation is not implemented.");
        }

        public double CountLength(LongLat p1, LongLat p2) {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }

    }
    public delegate void invalidateDelegate(Rectangle rect, bool freshchild);
    


