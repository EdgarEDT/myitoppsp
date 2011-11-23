using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Drawing.Imaging;
using System.Collections.Generic;


namespace Itop.MapView
{
    /// <summary>
    /// 百度地图
    /// </summary>
    public class MapViewObj : MapViewBase
    {
        #region 字段
        string[] strMiles = new string[] { "1800", "800", "400", "200", "100", "40", "20", "10", "5", "2", "1", "0.4", "0.2", "0.1", "0.05","0.025" };
        //float[] nMiles = new float[] { 4500, 2000, 1000, 500, 250, 100, 50, 25, 10, 5, 2.5f, 1, 0.5f, 0.25f };
        static string[] strLevel = new string[] { "W", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13" ,"14"};
        static private double[] d2H = new double[] { 90, 40, 20, 10, 5, 2, 1, 0.5, 0.2, 0.1, 0.05, 0.02, 0.01, 0.005, 0.0025,0.00125 };
        static private double[] qxl3 = new double[] { 90 * 0.8, 40 * 0.8, 20 * 0.8, 10 * 0.8, 5 * 0.8, 2 * 0.8, 1 * 0.8, 0.5 * 0.8, 0.2 * 0.8, 0.1 * 0.8, 0.05 * 0.8, 0.02 * 0.8, 0.01 * 0.8, 0.005 * 0.8, 0.0025 * 0.8, 0.00125 * 0.8 };

        int[] g6Y = new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 50, 50, 50, 50, 50 ,50};
        double[] nScale ={ 0.004 / 18, 0.0005, 0.001, 0.002, 0.004, 0.01, 0.02, 0.04, .1, .2, .4, 1, 2, 4, 8,16 };
        //		string strImgsvrUrl = "http://www.tonli.com/mapview/";
        private ArrayList listImages = new ArrayList();
        IDataHelper dataHelper;
        //DataHelper dataHelper2 = new DataHelper();

        public override IDataHelper DataHelper
        {
            get { return dataHelper; }
            set { dataHelper = value; }
        }
        private int nOffset = 0;
        int scaleLevel = 2;//缩放等级
        double longgitude = 117.79;//经度
        double latitude = 30.89;//纬度

        int nPerWidth = 300;

        string imageBasePath = Application.StartupPath + "\\mapbank\\baidu\\";//图片目录

        LongLat zeroLongLat = new LongLat(117.740, 30.916);
        LongLat defautLongLat = new LongLat(117.79, 30.89);
        PointF defaultLeftTop = PointF.Empty;
        
        #endregion 字段

        #region 属性
        public override int PicWidth
        {
            get { return nPerWidth; }
            set { nPerWidth = value; }
        }
        public int TypeOffset
        {
            get { return nOffset; }
            set { nOffset = value; }
        }
        /// <summary>
        /// 初始(0,0)点经纬度
        /// </summary>
        public override LongLat ZeroLongLat
        {
            get { return zeroLongLat; }
            set { zeroLongLat = value; }
        }
        /// <summary>
        /// 左上角坐标
        /// </summary>
        public PointF DefaultLeftTop
        {
            get { return defaultLeftTop; }
            set
            {
                defaultLeftTop = value;
                defautLongLat = new LongLat(longgitude, latitude);
            }
        }
        //当前中心点经度
        public override double Longgitude
        {
            get { return longgitude; }
            set { longgitude = value; }
        }
        //当前中心点纬度
        public override double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        public string ImageBasePath
        {
            get { return imageBasePath; }
            set { imageBasePath = value; }
        }

        public int ScaleLevel
        {
            get { return scaleLevel; }
        }
        #endregion
        public MapViewObj()
        {
            dataHelper = new DataHelper();
            //DataHelper.IsDownMap = true;
            //dataHelper.ImgPath = "mapData.yap";
            dataHelper.ImgsvrUrl = "http://mappng.baidu.com/maplite/mapbank/baidu/";
        }
        public MapViewObj(string database)
        {
            dataHelper = new DataHelper(database);
            dataHelper.ImgsvrUrl = "http://mappng.baidu.com/maplite/mapbank/baidu/";
        }

        public override int Getlevel(float ScaleUnit)
        {
            int nScale = -1;
            switch (Convert.ToInt32(ScaleUnit * 1000))
            {
                case 10:
                    nScale = 5;
                    break;
                case 20:
                    nScale = 6;
                    break;
                case 40:
                    nScale = 7;
                    break;
                case 100:
                    nScale = 8;
                    break;
                case 200:
                    nScale = 9;
                    break;
                case 400:
                    nScale = 10;
                    break;
                case 1000:
                    nScale = 11;
                    break;
                case 2000:
                    nScale = 12;
                    break;
                case 4000:
                    nScale = 13;
                    break;
                case 8000:
                    nScale = 14;
                    break;
                case 16000:
                    nScale = 15;
                    break;
                default:
                    if (ScaleUnit > 8) return nScale = 14;
                    else
                        return -1;
            }
            return nScale;
        }
        public override int Getlevel2(float ScaleUnit, out float scale)
        {
            int nScale = -1;
            float scaleUnit = ScaleUnit*100;
            scale = 1;
            if (scaleUnit >= 1600) { nScale = 15; scale = scaleUnit / 1600; }
            else if (scaleUnit >= 800) { nScale = 14; scale = scaleUnit / 800; }
            else if (scaleUnit >= 400) { nScale = 13; scale = scaleUnit / 400; }
            else if (scaleUnit >= 200) { nScale = 12; scale = scaleUnit / 200; }
            else if (scaleUnit >= 100) { nScale = 11; scale = scaleUnit / 100; }
            else if (scaleUnit >= 40) { nScale = 10; scale = scaleUnit / 40; }
            else if (scaleUnit >= 20) { nScale = 9; scale = scaleUnit / 20; }
            else if (scaleUnit >= 10) { nScale = 8; scale = scaleUnit / 10; }
            else if (scaleUnit >= 4) { nScale = 7; scale = scaleUnit / 4; }
            else if (scaleUnit >= 2) { nScale = 6; scale = scaleUnit / 2; }
            else if (scaleUnit >= 1) { nScale = 5; scale = scaleUnit / 1; }
            else return -1;

            return nScale;
        }
        public override int Getlevel(float ScaleUnit, out float scale)
        {
            int nScale = -1;
            scale = 1;
            if (ScaleUnit >= 8) { nScale = 14; scale = ScaleUnit / 8; }
            else if (ScaleUnit <= 0.02) { nScale = 5; scale = ScaleUnit / .02f; }
            else if (ScaleUnit <= 0.04) { nScale = 6; scale = ScaleUnit / .04f; }
            else if (ScaleUnit <= 0.1) { nScale = 7; scale = ScaleUnit / .1f; }
            else if (ScaleUnit <= 0.2) { nScale = 8; scale = ScaleUnit / .2f; }
            else if (ScaleUnit <= 0.4) { nScale = 9; scale = ScaleUnit / .4f; }
            else if (ScaleUnit <= 1) { nScale = 10; scale = ScaleUnit / 1f; }
            else if (ScaleUnit <= 2) { nScale = 11; scale = ScaleUnit / 2; }
            else if (ScaleUnit <= 4) { nScale = 12; scale = ScaleUnit / 4; }
            else if (ScaleUnit <= 8) { nScale = 13; scale = ScaleUnit / 8; }
            else return -1;
            if (nScale < 14) nScale++;
            return nScale;
        }
        public override string GetMiles(int scale)
        {
            return strMiles[scale];
        }
        /// <summary>
        /// 反回长度（单位公里）
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public override double CountLength(PointF p1, PointF p2)
        {
            return Math.Round(Math.Sqrt(Math.Pow((p1.X - p2.X), 2) + Math.Pow((p1.Y - p2.Y) * 0.8, 2)) / 60 / 2.5, 4);
        }
        /// <summary>
        /// 绘制地图 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="nScaleLevel"></param>
        /// <param name="fLongitude"></param>
        /// <param name="fLatitude"></param>
        public override void Paint(Graphics g, int width, int height, int nScaleLevel, double fLongitude, double fLatitude, ImageAttributes imagAttributes)
        {
            try
            {
                longgitude = fLongitude;
                latitude = fLatitude;
                scaleLevel = nScaleLevel;
                ImageAttributes imageA = new ImageAttributes();
                int le = 2;
                if (nScaleLevel == 7)
                { le = 2; };
                if (nScaleLevel == 6)
                { le = 2; };


                int nRows = (int)(Math.Ceiling((double)height / (double)nPerWidth / 2.0d) + le);
                int nCols = (int)(Math.Ceiling((double)width / (double)nPerWidth / 2.0d) + le);

                if (listImages.Count > 50)
                {
                    listImages.Clear();
                    GC.Collect();
                }

                foreach (ClassImage img in listImages)
                {
                    img.IsDiscard = true;
                }

                int nCenterX = width / 2;
                int nCenterY = height / 2;

                //this.$i024=qxl3[this.nScaleLevel];
                int offsetX = nCenterX - (int)Math.Round(((fLongitude * 100000) % (d2H[nScaleLevel] * 100000)) * nPerWidth / (d2H[nScaleLevel] * 100000));
                int offsetY = 0;
                if (fLatitude >= 0)
                {
                    offsetY = nCenterY - nPerWidth + (int)Math.Round(((fLatitude * 100000) % (qxl3[nScaleLevel] * 100000)) * nPerWidth / (qxl3[nScaleLevel] * 100000));
                }
                else
                {
                    offsetY = nCenterY + (int)Math.Round(((fLatitude * 100000) % (qxl3[nScaleLevel] * 100000)) * nPerWidth / (qxl3[nScaleLevel] * 100000));
                }
                //if (nOffset==0)
                {
                    if (nScaleLevel == 11)
                        offsetY -= 150;
                    else if (nScaleLevel == 8)
                        offsetY += 75;
                }
                //string strImgsvrUrl = "http://mappng.baidu.com/maplite/mapbank/baidu/";
                //System.Net.WebClient webClient = new System.Net.WebClient();
                for (int nRow = nRows; nRow >= -(nRows + 1); nRow--)
                //for (int nRow = nRows; nRow >= 0; nRow--)
                {
                    for (int nCol = -(nCols); nCol <= nCols; nCol++)
                    {

                        try
                        {
                            string strPic = strLevel[nScaleLevel] + "/";
                            string strPicPath = imageBasePath + strLevel[nScaleLevel] + "\\";

                            int L04m = (int)(Math.Floor((double)((fLongitude + d2H[nScaleLevel] / 100000) / d2H[nScaleLevel])) + nCol);
                            int j93co = (int)(Math.Floor((double)((fLatitude + qxl3[nScaleLevel] / 100000) / qxl3[nScaleLevel])) + nRow);

                            int U63K2 = (int)(360 / d2H[nScaleLevel]);
                            L04m = L04m % U63K2;
                            if (L04m >= (U63K2 / 2)) L04m -= U63K2;
                            if (L04m < (-U63K2 / 2)) L04m += U63K2;
                            int X1 = (int)(Math.Floor((double)(L04m / g6Y[nScaleLevel])));
                            int Y1 = (int)(Math.Floor((double)(j93co / g6Y[nScaleLevel])));
                            {
                                if (X1 < 0) X1 += 1;
                                if (Y1 < 0) Y1 += 1;
                            }
                            int X2 = (L04m) - X1 * g6Y[nScaleLevel];
                            int Y2 = (j93co) - Y1 * g6Y[nScaleLevel];

                            strPic += X1 + "_" + Y1 + "/";
                            strPic += X2 + "_" + Y2 + ".png";

                            strPicPath += X1 + "_" + Y1 + "\\";
                            strPicPath += X2 + "_" + Y2 + ".png";

                            int nPicLeft = nCol * nPerWidth + offsetX;
                            int nPicTop = -nRow * nPerWidth + offsetY;

                            //不在显示范围内
                            //if (nPicLeft < -nPerWidth || nPicLeft > width || nPicTop > height || nPicTop < -nPerWidth) {
                            //    continue;
                            //}
                            ClassImage img = FindImage(strPic);
                            if (img == null)
                            {
                                img = dataHelper.GetImage(strPic);
                                if (listImages.Count < 50)
                                    listImages.Add(img);
                            }

                            img.IsDiscard = false;
                            img.Left = nPicLeft;
                            img.Top = nPicTop;
                            //img.Longitude = fLongitude - (nCenterX - nPicLeft) * d2H[nScaleLevel] / nPerWidth;
                            //img.Latitude = fLatitude + (nCenterY - nPicTop) * qxl3[nScaleLevel] / nPerWidth;

                            Rectangle rect = new Rectangle(img.Left, img.Top, nPerWidth, nPerWidth);
                            if (img.PicImage != null && g != null)
                                //g.DrawImage(img.PicImage, img.Left, img.Top, nPerWidth, nPerWidth);
                                g.DrawImage(img.PicImage, rect, 0, 0, nPerWidth, nPerWidth, GraphicsUnit.Pixel, imagAttributes);
                            if (ShowMapInfo) {
                                g.DrawRectangle(Pens.Red, img.Left, img.Top, nPerWidth, nPerWidth);

                                //string s = string.Format("x{0}:y{1}", img.Left, img.Top);//图片左上角坐标
                                //string s = "(" + img.Longitude.ToString(nfInfo) + "," + img.Latitude.ToString(nfInfo) + ")";//经纬度
                                string s = img.PicUrl;//图片名
                                g.DrawString(s, new Font("宋体", 10), Brushes.Red, new Rectangle(img.Left, img.Top, 300, 300));
                            }
                        }
                        catch (Exception e3)
                        {
                            string a = "";
                        }
                    }
                }
                //					string s = string.Format("{0}行{1}列", nRows, nCols);
                //					string s = string.Format("经{0}：纬{1}", longgitude, latitude);//显示中心点经纬度
                //			
                //					g.DrawString(s, new Font("宋体", 10), Brushes.Red, 20, 40);
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message);
            }
        }
        public override void Paint(Graphics g, int width, int height, int nScaleLevel, double fLongitude, double fLatitude)
        {
            longgitude = fLongitude;
            latitude = fLatitude;
            scaleLevel = nScaleLevel;
            ImageAttributes imageA = new ImageAttributes();


            int nRows = (int)(Math.Ceiling((double)height / (double)nPerWidth / 2.0d));
            int nCols = (int)Math.Ceiling((double)width / (double)nPerWidth / 2.0d);

            if (listImages.Count > 50)
            {
                listImages.Clear();
                GC.Collect();
            }

            foreach (ClassImage img in listImages)
            {
                img.IsDiscard = true;
            }

            int nCenterX = width / 2;
            int nCenterY = height / 2;

            //this.$i024=qxl3[this.nScaleLevel];
            int offsetX = nCenterX - (int)Math.Round(((fLongitude * 100000) % (d2H[nScaleLevel] * 100000)) * nPerWidth / (d2H[nScaleLevel] * 100000));
            int offsetY = 0;
            if (fLatitude >= 0)
            {
                offsetY = nCenterY - nPerWidth + (int)Math.Round(((fLatitude * 100000) % (qxl3[nScaleLevel] * 100000)) * nPerWidth / (qxl3[nScaleLevel] * 100000));
            }
            else
            {
                offsetY = nCenterY + (int)Math.Round(((fLatitude * 100000) % (qxl3[nScaleLevel] * 100000)) * nPerWidth / (qxl3[nScaleLevel] * 100000));
            }
            //if (nOffset == 0)
            {
                if (nScaleLevel == 11)
                    offsetY -= 150;
                else if (nScaleLevel == 8)
                    offsetY += 75;
            }
            //string strImgsvrUrl = "http://mappng.baidu.com/maplite/mapbank/baidu/";
            //System.Net.WebClient webClient = new System.Net.WebClient();
            for (int nRow = nRows; nRow >= -(nRows + 1); nRow--)
            {
                for (int nCol = -(nCols); nCol <= nCols; nCol++)
                {
                    string strPic = strLevel[nScaleLevel] + "/";
                    string strPicPath = imageBasePath + strLevel[nScaleLevel] + "\\";

                    int L04m = (int)(Math.Floor((double)((fLongitude + d2H[nScaleLevel] / 100000) / d2H[nScaleLevel])) + nCol);
                    int j93co = (int)(Math.Floor((double)((fLatitude + qxl3[nScaleLevel] / 100000) / qxl3[nScaleLevel])) + nRow);

                    int U63K2 = (int)(360 / d2H[nScaleLevel]);
                    L04m = L04m % U63K2;
                    if (L04m >= (U63K2 / 2)) L04m -= U63K2;
                    if (L04m < (-U63K2 / 2)) L04m += U63K2;
                    int X1 = (int)(Math.Floor((double)(L04m / g6Y[nScaleLevel])));
                    int Y1 = (int)(Math.Floor((double)(j93co / g6Y[nScaleLevel])));
                    {
                        if (X1 < 0) X1 += 1;
                        if (Y1 < 0) Y1 += 1;
                    }
                    int X2 = (L04m) - X1 * g6Y[nScaleLevel];
                    int Y2 = (j93co) - Y1 * g6Y[nScaleLevel];

                    strPic += X1 + "_" + Y1 + "/";
                    strPic += X2 + "_" + Y2 + ".png";

                    strPicPath += X1 + "_" + Y1 + "\\";
                    strPicPath += X2 + "_" + Y2 + ".png";

                    int nPicLeft = nCol * nPerWidth + offsetX;
                    int nPicTop = -nRow * nPerWidth + offsetY;

                    //不在显示范围内
                    //if (nPicLeft < -nPerWidth || nPicLeft > width || nPicTop > height || nPicTop < -nPerWidth) {
                    //    continue;
                    //}
                    ClassImage img = FindImage(strPic);
                    if (img == null)
                    {
                        img = dataHelper.GetImage(strPic);
                        if (listImages.Count < 50)
                            listImages.Add(img);
                    }

                    img.IsDiscard = false;
                    img.Left = nPicLeft;
                    img.Top = nPicTop;
                    //img.Longitude = fLongitude - (nCenterX - nPicLeft) * d2H[nScaleLevel] / nPerWidth;
                    //img.Latitude = fLatitude + (nCenterY - nPicTop) * qxl3[nScaleLevel] / nPerWidth;

                    Rectangle rect = new Rectangle(img.Left, img.Top, nPerWidth, nPerWidth);
                    if (img.PicImage != null && g != null)
                        g.DrawImage(img.PicImage, img.Left, img.Top, nPerWidth, nPerWidth);
                    //g.DrawImage(img.PicImage, rect, img.Left, img.Top, nPerWidth, nPerWidth, GraphicsUnit.Pixel, imagAttributes);
                    if (ShowMapInfo) {
                        g.DrawRectangle(Pens.Red, img.Left, img.Top, nPerWidth, nPerWidth);
                        //string s = string.Format("x{0}:y{1}", img.Left, img.Top);//图片左上角坐标
                        //string s = "(" + img.Longitude.ToString(nfInfo) + "," + img.Latitude.ToString(nfInfo) + ")";//经纬度
                        string s = img.PicUrl;//图片名
                        g.DrawString(s, new Font("宋体", 10), Brushes.Red, new Rectangle(img.Left, img.Top, 300, 300));
                    }

                }
            }
            //					string s = string.Format("{0}行{1}列", nRows, nCols);
            //					string s = string.Format("经{0}：纬{1}", longgitude, latitude);//显示中心点经纬度
            //			
            //					g.DrawString(s, new Font("宋体", 10), Brushes.Red, 20, 40);
        }
        /// <summary>
        /// 创建地图到Image中
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="nScaleLevel"></param>
        /// <param name="fLongitude"></param>
        /// <param name="fLatitude"></param>
        /// <returns></returns>
        public override Image CreateMap(int width, int height, int nScaleLevel, double fLongitude, double fLatitude)
        {

            Image image1 = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image1);
            g.Clear(Color.FromArgb(0xEB, 0xEA, 0xE8));
            ImageAttributes imageAttributes = new ImageAttributes();

            //Paint(g, width, height, nScaleLevel, fLongitude, fLatitude,imageAttributes);
            Paint(g, width, height, nScaleLevel, fLongitude, fLatitude);
            return image1;
        }
        /// <summary>
        /// 返回偏移指定象素后的经纬度
        /// </summary>
        /// <param name="x">经度偏移象素</param>
        /// <param name="y">纬度编移象素</param>
        /// <returns></returns>
        public override LongLat OffSet(int x, int y)
        {
            double d1 = longgitude - (x * d2H[scaleLevel] / nPerWidth);
            double d2 = latitude + (y * qxl3[scaleLevel] / nPerWidth);
            return new LongLat(d1, d2);
        }
        /// <summary>
        /// x,y 加负号
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override LongLat OffSetZero(int x, int y)
        {
            double d1 = ZeroLongLat.Longitude - (x * d2H[scaleLevel] / nPerWidth);
            double d2 = ZeroLongLat.Latitude + (y * qxl3[scaleLevel] / nPerWidth);
            return new LongLat(d1, d2);
        }
        //<summary>

        //</summary>
        //<param name="x"></param>
        //<param name="y"></param>
        //<returns></returns>
        public override LongLat ParseToLongLat(int x, int y)
        {
            double d1 = ZeroLongLat.Longitude + (x * d2H[11] / nPerWidth);
            double d2 = ZeroLongLat.Latitude - (y * qxl3[11] / nPerWidth);
            return new LongLat(d1, d2);
        }
        public override  LongLat ParseToLongLat(float x, float y) {
            double d1 = ZeroLongLat.Longitude + ((double)x * d2H[11] / nPerWidth);
            double d2 = ZeroLongLat.Latitude - ((double)y * qxl3[11] / nPerWidth);
            return new LongLat(d1, d2);
        }
        /// <summary>
        /// x,y 加负号
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override LongLat OffSet(LongLat longlat1, int scale, int x, int y)
        {
            double d1 = longlat1.Longitude - (x * d2H[scale] / nPerWidth);
            double d2 = longlat1.Latitude + (y * qxl3[scale] / nPerWidth);
            return new LongLat(d1, d2);
        }
        public override PointF ParseToPoint(double _Longitude, double _Latitude)
        {
            double x = (-ZeroLongLat.Longitude + _Longitude) * nPerWidth / d2H[scaleLevel];//*nScale[scaleLevel];
            double y = (-_Latitude + ZeroLongLat.Latitude) * nPerWidth / qxl3[scaleLevel];//*nScale[scaleLevel];
            return new PointF((float)x, (float)y);
        }
        public override IntXY getXY(double _Longitude, double _Latitude)
        {
            double x = (ZeroLongLat.Longitude - _Longitude) * nPerWidth / d2H[scaleLevel];
            double y = (_Latitude - ZeroLongLat.Latitude) * nPerWidth / qxl3[scaleLevel];
            return new IntXY(x, y);
        }
        public override string[] ScaleRange()
        {
            string[] range1 = new string[] {"1600%", "800%", "400%", "200%", "100%", "40%", "20%", "10%", "4%", "2%", "1%" };
            return range1;
        }

        public static string[] ScaleRanges {
            get {
                string[] range1 = new string[] {"1600%", "800%", "400%", "200%", "100%", "40%", "20%", "10%", "4%", "2%", "1%" };
                return range1;
            }
        }
        #region 私有方法


        public override ClassImage FindImage(Point p)
        {
            foreach (ClassImage img in listImages)
            {
                if (!img.IsDiscard)
                {
                    Rectangle rt = new Rectangle(img.Left, img.Top, nPerWidth, nPerWidth);
                    if (rt.Contains(p))
                        return img;
                }
            }
            return null;
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
        #endregion

        #region IMapViewObj 成员



        public override LongLat OffSet(LongLat longlat1, int scale, float x, float y)
        {
            double d1 = longlat1.Longitude - (Convert.ToDouble(x) * d2H[scale] / nPerWidth);
            double d2 = longlat1.Latitude + (Convert.ToDouble(y) * qxl3[scale] / nPerWidth);
            return new LongLat(d1, d2);
        }
        //public LongLat OffSetZero(int x, int y)
        //{
        //    double d1 = ZeroLongLat.Longitude - (x * d2H[scaleLevel] / nPerWidth);
        //    double d2 = ZeroLongLat.Latitude + (y * qxl3[scaleLevel] / nPerWidth);
        //    return new LongLat(d1, d2);
        //}

        public override LongLat OffSetZero(int level, float x, float y)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        //public LongLat OffSetZero(float x, float y)
        //{
        //    double d1 = ZeroLongLat.Longitude - ((Convert.Todouble(x)) * d2H[scaleLevel] / nPerWidth);
        //    double d2 = ZeroLongLat.Latitude + ((Convert.Todouble(y)) * qxl3[scaleLevel] / nPerWidth);
        //    return new LongLat(d1, d2);
        //}

        public override double Scaleoffset(int scale)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        public override List<ClassImage> GetMapList(int width, int height, PointF center)
        {
            LongLat longlat = LongLat.Empty;

            longlat = ParseToLongLat(center.X, center.Y);

            return GetMapList(width, height, ScaleLevel, longlat.Longitude, longlat.Latitude);

        }
        public override List<ClassImage> GetMapList(int width, int height, int nScaleLevel, double fLongitude, double fLatitude)
        {
            longgitude = fLongitude;
            latitude = fLatitude;
            scaleLevel = nScaleLevel;

            int nRows = (int)Math.Ceiling((double)height / nPerWidth / 2);
            int nCols = (int)Math.Ceiling((double)width / nPerWidth / 2);


            int nCenterX = width / 2;
            int nCenterY = height / 2;

            int offsetX = nCenterX - (int)Math.Round(((fLongitude * 100000) % (d2H[nScaleLevel] * 100000)) * nPerWidth / (d2H[nScaleLevel] * 100000));
            int offsetY = 0;
            if (fLatitude >= 0)
            {
                offsetY = nCenterY - nPerWidth + (int)Math.Round(((fLatitude * 100000) % (qxl3[nScaleLevel] * 100000)) * nPerWidth / (qxl3[nScaleLevel] * 100000));
            }
            else
            {
                offsetY = nCenterY + (int)Math.Round(((fLatitude * 100000) % (qxl3[nScaleLevel] * 100000)) * nPerWidth / (qxl3[nScaleLevel] * 100000));
            }
            if (nScaleLevel == 11)
                offsetY -= 150;
            else if (nScaleLevel == 8)
                offsetY += 75;
            List<ClassImage> retlist = new List<ClassImage>();
            for (int nRow = nRows; nRow >= -(nRows); nRow--)
            {
                for (int nCol = -(nCols); nCol <= nCols; nCol++)
                {
                    string strPic = strLevel[nScaleLevel] + "/";
                    string strPicPath = imageBasePath + strLevel[nScaleLevel] + "\\";

                    int L04m = (int)(Math.Floor((fLongitude + d2H[nScaleLevel] / 100000) / d2H[nScaleLevel]) + nCol);
                    int j93co = (int)(Math.Floor((fLatitude + qxl3[nScaleLevel] / 100000) / qxl3[nScaleLevel]) + nRow);

                    int U63K2 = (int)(360 / d2H[nScaleLevel]);
                    L04m = L04m % U63K2;
                    if (L04m >= (U63K2 / 2)) L04m -= U63K2;
                    if (L04m < (-U63K2 / 2)) L04m += U63K2;
                    int X1 = (int)(Math.Floor((double)(L04m / g6Y[nScaleLevel])));
                    int Y1 = (int)(Math.Floor((double)(j93co / g6Y[nScaleLevel])));
                    {
                        if (X1 < 0) X1 += 1;
                        if (Y1 < 0) Y1 += 1;
                    }
                    int X2 = (L04m) - X1 * g6Y[nScaleLevel];
                    int Y2 = (j93co) - Y1 * g6Y[nScaleLevel];

                    strPic += X1 + "_" + Y1 + "/";
                    strPic += X2 + "_" + Y2 + ".png";

                    strPicPath += X1 + "_" + Y1 + "\\";
                    strPicPath += X2 + "_" + Y2 + ".png";

                    int nPicLeft = nCol * nPerWidth + offsetX;
                    int nPicTop = -nRow * nPerWidth + offsetY;
                    ClassImage image = dataHelper.GetImage(strPic);
                    image.PicWidth = nPerWidth;
                    image.Left = nPicLeft;
                    image.Top = nPicTop;
                    if (image.PicImage == null)
                    {

                    }
                    retlist.Add(image);
                }
            }
            return retlist;

        }

        public override void ClearBuff()
        {
            listImages.Clear();
        }

        #region IMapViewObj 成员


        public override IList<ClassImage> GetMapList(int width, int height, Point point)
        {
            LongLat longlat = LongLat.Empty;

            longlat = ParseToLongLat((int)(point.X), (int)(point.Y));

            return GetMapList(width, height, ScaleLevel, longlat.Longitude, longlat.Latitude);

        }

        #endregion

        #region IMapViewObj 成员


        public override void SetImage(ClassImage image)
        {
            dataHelper.SetImage(image);
        }

        #endregion
    }



}
