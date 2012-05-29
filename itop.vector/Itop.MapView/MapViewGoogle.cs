using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MapLib;
using System.Threading;
using System.Configuration;

namespace Itop.MapView
{
    public class MapViewGoogle : MapViewBase
    {
        #region field
        LongLat Offsize;
        IDataHelper dataHelper;
        IDataHelper dataHelper2;
        IDataHelper dataHelper3;
        IDataHelper dataHelper4;
        MapLib.GoogleMapHelper mapHelper;
        private List<ClassImage> listImages = new List<ClassImage>();
        MapLib.TileType tileType = MapLib.TileType.Map;

        public MapLib.TileType TileType {
            get { return tileType; }
            set { tileType = value; }
        }
        int scaleLevel = -1;//缩放等级

        public int ScaleLevel {
            get { return scaleLevel; }
            set { scaleLevel = value; }
        }
        double longgitude = 122.79;//经度
        double latitude = 41.85;//纬度
        static int nPerWidth = 256;
        int _width, _height;
        LongLat zeroLongLat = LongLat.Empty;
        PointF zeroCenter = PointF.Empty;
        Point centerP;

        public Point CenterP {
            get { return centerP; }
            set { centerP = value; }
        }
        int imageCacheNum = 50;
        Dictionary<TileType, IDataHelper> dataHelpers = new Dictionary<TileType, IDataHelper>();
        #endregion
        public MapViewGoogle()
            : this("") {
        }
        public MapViewGoogle(string mapfile) {
            dataHelper = new GoogleDataHelper("", "m1_");
            dataHelper2 = new GoogleDataHelper2("", "m2_");
            dataHelper3 = new GoogleDataHelper3("", "m3_");
            dataHelper4 = new GoogleDataHelper4("", "m4_");
            dataHelper.OnDownCompleted += new DownCompleteEventHandler(dataHelper_OnDownCompleted);
            dataHelper2.OnDownCompleted += new DownCompleteEventHandler(dataHelper_OnDownCompleted);
            dataHelper3.OnDownCompleted += new DownCompleteEventHandler(dataHelper_OnDownCompleted);
            dataHelper4.OnDownCompleted += new DownCompleteEventHandler(dataHelper_OnDownCompleted);
            mapHelper = new MapLib.GoogleMapHelper();
            baseNames = new Dictionary<TileType, string>();
            baseNames.Add(TileType.Map, "m1_");
            baseNames.Add(TileType.Terrain, "m4_");
            baseNames.Add(TileType.Photos, "m2_");
            baseNames.Add(TileType.PhotosLabel, "m3_");
            dataHelpers.Add(TileType.Map, dataHelper);
            dataHelpers.Add(TileType.Photos, dataHelper2);
            dataHelpers.Add(TileType.PhotosLabel, dataHelper3);
            dataHelpers.Add(TileType.Terrain, dataHelper4);
            try
            {
                double d1 = double.Parse(ConfigurationManager.AppSettings["offsizejd"]);
                double d2 = double.Parse(ConfigurationManager.AppSettings["offsizewd"]);
                Offsize = new LongLat(d1, d2);
            }
            catch { }
        }

        void dataHelper_OnDownCompleted(ClassImage mapclass) {
            base.downCompleted(mapclass);            
        }
        
        public bool IsDownMap {
            set {
                dataHelper.IsDownMap = value;
                dataHelper2.IsDownMap = value;
                dataHelper3.IsDownMap = value;
                dataHelper4.IsDownMap = value;
            }
            get { return dataHelper.IsDownMap; }
        }
        public override int Getlevel(float ScaleUnit) {
            int nScale = -1;
            switch (Convert.ToInt32(ScaleUnit * 1000)) {
                case 4:
                    nScale = 8;
                    break;
                case 8:
                    nScale = 9;
                    break;
                case 16:
                    nScale = 10;
                    break;
                case 31:
                    nScale = 11;
                    break;
                case 62:
                    nScale = 12;
                    break;
                case 125:
                    nScale = 13;
                    break;
                case 250:
                    nScale = 14;
                    break;
                case 500:
                    nScale = 15;
                    break;
                case 1000:
                    nScale = 16;
                    break;
                case 2000:
                    nScale = 17;
                    break;
                case 4000:
                    nScale = 18;
                    break;
                case 8000:
                    nScale = 19;
                    break;
                default:
                    return -1;
            }
            return nScale;
        }
        public void SetTileType(int type) {
            tileType = (TileType)type;
        }
        public override int Getlevel2(float ScaleUnit, out float scale) {
            scale = 1;
            return -1;
        }

        public override int Getlevel(float ScaleUnit, out float scale) {
            scale = 1;
            return -1;
        }

        public override string GetMiles(int level) {
            PointF pf1 = new PointF(0, 0);
            PointF pf2 = new PointF(60, 0);

            return Math.Round(CountLength(pf1, pf2, level), 3) + "";
        }
        public override double CountLength(System.Drawing.PointF p1, System.Drawing.PointF p2) {
            PointF pf1 = zeroCenter;
            PointF pf2 = new PointF(pf1.X + p1.X, pf1.Y + p1.Y);
            PointF pf3 = new PointF(pf1.X + p2.X, pf1.Y + p2.Y);
            pf2 = mapHelper.PixelTolatLng(pf2, Getlevel(1));
            pf3 = mapHelper.PixelTolatLng(pf3, Getlevel(1));

            return mapHelper.GetDistanceByJW(new double[] { pf2.Y, pf2.X }, new double[] { pf3.Y, pf3.X }) / 1000;
        }
        public double CountLength(System.Drawing.PointF p1, System.Drawing.PointF p2, int level) {
            PointF pf1 = mapHelper.LatLngToPixelF((float)zeroLongLat.Latitude, (float)zeroLongLat.Longitude, level);
            PointF pf2 = new PointF(pf1.X + p1.X, pf1.Y + p1.Y);
            PointF pf3 = new PointF(pf1.X + p2.X, pf1.Y + p2.Y);
            pf2 = mapHelper.PixelTolatLng(pf2, level);
            pf3 = mapHelper.PixelTolatLng(pf3, level);

            return mapHelper.GetDistanceByJW(new double[] { pf2.Y, pf2.X }, new double[] { pf3.Y, pf3.X }) / 1000;
        }
        public override double CountLength(PointF[] pts) {
            double len = 0;
            if (pts.Length < 2) return 0;
            PointF pf1 = mapHelper.PixelTolatLng(new PointF(pts[0].X + zeroCenter.X, pts[0].Y + zeroCenter.Y), Getlevel(1));
            for (int i = 1; i < pts.Length; i++) {
                PointF pf2 = mapHelper.PixelTolatLng(new PointF(pts[i].X + zeroCenter.X, pts[i].Y + zeroCenter.Y), Getlevel(1));
                len += mapHelper.GetDistanceByJW(new double[] { pf2.Y, pf2.X }, new double[] { pf1.Y, pf1.X });
                pf1 = pf2;
            }
            return len / 1000;
        }
        public override double CountLength(LongLat p1, LongLat p2) {
            return mapHelper.GetDistanceByJW(new double[] { p1.Longitude, p1.Latitude }, new double[] { p2.Longitude, p2.Latitude }) / 1000;
        }
        public override void Paint(System.Drawing.Graphics g, int width, int height, int nScaleLevel, double fLongitude, double fLatitude, System.Drawing.Imaging.ImageAttributes imagAttributes) {
            _width = width;
            _height = height;
            longgitude = fLongitude;
            latitude = fLatitude;
            if (scaleLevel != nScaleLevel) {
                scaleLevel = nScaleLevel;

            }
            centerP = Point.Empty;
            if (listImages.Count > imageCacheNum) {
                listImages.Clear();
            }

            foreach (ClassImage img in listImages) {
                img.IsDiscard = true;
            }
            float lat = (float)fLatitude;
            float lng = (float)fLongitude;
            
            int zoom = nScaleLevel;
            Size size = new Size(width, height);
            if (centerP.IsEmpty)
                centerP = latLngToPixel(lat, lng, zoom);
            if (tileType != TileType.Photos)
            {
                lng +=(float) Offsize.Longitude;// 0.004180908203125f;
                lat += (float)Offsize.Latitude;// 0.002651214599609375f;
            }
            IList<MapLib.MapTile> list = mapHelper.GetTiles(lat, lng, width, height, zoom, tileType);

            //IList<MapLib.MapTile> list = mapHelper.GetTiles(lat, lng, 1000, 1000, zoom, tileType);
            foreach (MapTile tile in list) {
                string strPic = tile.Name;
                int nPicLeft = tile.Left;
                int nPicTop = tile.Top;
                drawImage(g,tile, tileType, imagAttributes);

                if (ShowMapInfo) {
                    g.DrawRectangle(Pens.Red, nPicLeft, nPicTop, nPerWidth, nPerWidth);
                    string s = strPic;//图片名
                    g.DrawString(s, new Font("宋体", 10), Brushes.Red, new Rectangle(nPicLeft, nPicTop, nPerWidth, nPerWidth));
                   
                }
            }
            //LongLat ll= ParseToLongLat(centerP.X - width / 2f, centerP.Y - height / 2f);108.07016754150391,,,
            if (ShowMapInfo) {                                
                string s2 = string.Format("\r\n  经{0}：纬{1}", longgitude, latitude);
                g.DrawString(s2, new Font("宋体", 10), Brushes.Red, 0, 0);
            }
        }
        
        Dictionary<TileType, string> baseNames;
        //private void drawImage(System.Drawing.Graphics g, MapTile tile, TileType tileType, ImageAttributes imageA)
        //{
        //    ClassImage img = FindImage(baseNames[tileType] + tile.Name);
        //    string baseUrl = tile.Url;
        //    IDataHelper data = dataHelpers[tileType];

        //    if (img == null)
        //    {
        //        img = data.GetImage(baseUrl, tile.Name);
        //        if (img == null)
        //        {
        //            img = new ClassImage();
        //        }
        //        if (img.PicImage != null)
        //            listImages.Add(img);
        //    }
        //    img.IsDiscard = false;
        //    img.Left = tile.Left;
        //    img.Top = tile.Top;
        //    if (img.Left < -nPerWidth || img.Left > _width || img.Top > _height || img.Top < -nPerWidth)
        //    {
        //        return;
        //    }
        //    Rectangle rect = new Rectangle(img.Left, img.Top, nPerWidth, nPerWidth);

        //    if (img.PicImage != null && g != null)
        //        g.DrawImage(img.PicImage, rect, 0, 0, nPerWidth, nPerWidth, GraphicsUnit.Pixel, imageA);
        //}
        private void drawImage(MapTile tile, TileType tileType, ImageAttributes imageA)
        {
            ClassImage img = FindImage(baseNames[tileType] + tile.Name);
            string baseUrl = tile.Url;
            IDataHelper data = dataHelpers[tileType];

            if (img == null)
            {
                img = data.GetImages(baseUrl, tile.Name);
            }
            img.IsDiscard = false;
            img.Left = tile.Left;
            img.Top = tile.Top;
            if (img.Left < -nPerWidth || img.Left > _width || img.Top > _height || img.Top < -nPerWidth)
            {
                return;
            }

        }
        IList<MapLib.MapTile> list1 = new List<MapLib.MapTile>();
        public void DownImages(int width, int height, int nScaleLevel, double fLongitude, double fLatitude)
        {
            ImageAttributes imageA = new ImageAttributes();
            Color color = ColorTranslator.FromHtml("#f2efe9");
            //Color color = Color.FromArgb(229, 227, 223);
            imageA.SetColorKey(color, color);

            _width = width;
            _height = height;
            longgitude = fLongitude;
            latitude = fLatitude;
            if (scaleLevel != nScaleLevel)
            {
                scaleLevel = nScaleLevel;

            }
            centerP = Point.Empty;
            if (listImages.Count > imageCacheNum)
            {
                listImages.Clear();
            }

            foreach (ClassImage img in listImages)
            {
                img.IsDiscard = true;
            }
            float lat = (float)fLatitude;
            float lng = (float)fLongitude;
            int zoom = nScaleLevel;
            Size size = new Size(width, height);
            if (centerP.IsEmpty)
                centerP = latLngToPixel(lat, lng, zoom);
            lat =(float)24;
            lng = (float)106;
            for (double i = 0.5; i <= 3.5;i = i+0.5 )
            {               
                lng = (float)(106 + i - 0.025);
                Point pt1 = latLngToPixel((float)(22.5), (float)(lng - 0.25), zoom);
                Point pt2 = latLngToPixel((float)(25.5), (float)(lng + 0.25), zoom);
                width = Math.Abs(pt1.X - pt2.X);
                height = Math.Abs(pt1.Y - pt2.Y);
                list1 = mapHelper.GetTiles(lat, lng, width, height, zoom, tileType);
                System.Threading.Thread TDownImage = new System.Threading.Thread(new ThreadStart(DownImage));
                TDownImage.Start();
            }




        }
        private void DownImage()
        {
            ImageAttributes imageA = new ImageAttributes();
            Color color = ColorTranslator.FromHtml("#f2efe9");
            //Color color = Color.FromArgb(229, 227, 223);
            imageA.SetColorKey(color, color);
            foreach (MapTile tile in list1)
            {
                string strPic = tile.Name;
                int nPicLeft = tile.Left;
                int nPicTop = tile.Top;

                drawImage(tile, tileType, imageA);

            }
        }
        private void drawImage(System.Drawing.Graphics g, MapTile tile, TileType tileType, ImageAttributes imageA) {
            ClassImage img = FindImage(baseNames[tileType] + tile.Name);
            string baseUrl = tile.Url;
            IDataHelper data = dataHelpers[tileType];
            
            if (img == null) {
                img = data.GetImage(baseUrl, tile.Name);
                if (img == null) {
                    img = new ClassImage();
                }
                if (img.PicImage != null)
                    listImages.Add(img);
            }
            img.IsDiscard = false;
            img.Left = tile.Left;
            img.Top = tile.Top;
            if (img.Left < -nPerWidth || img.Left > _width || img.Top > _height || img.Top < -nPerWidth) {
                return;
            }
            Rectangle rect = new Rectangle(img.Left, img.Top, nPerWidth, nPerWidth);

            if (img.PicImage != null && g != null)
                g.DrawImage(img.PicImage, rect, 0, 0, nPerWidth, nPerWidth, GraphicsUnit.Pixel, imageA);
        }

        public override void Paint(System.Drawing.Graphics g, int width, int height, int nScaleLevel, double fLongitude, double fLatitude) {
            ImageAttributes imageA = new ImageAttributes();
            Color color = ColorTranslator.FromHtml("#f2efe9");
            //Color color = Color.FromArgb(229, 227, 223);
            imageA.SetColorKey(color, color);

            Paint(g, width, height, nScaleLevel, fLongitude, fLatitude, imageA);
        }

        public override System.Drawing.Image CreateMap(int width, int height, int nScaleLevel, double fLongitude, double fLatitude) {
            Image image1 = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image1);
            //g.Clear(Color.FromArgb(0xEB, 0xEA, 0xE8));
            g.Clear(Color.White);
            ImageAttributes imageAttributes = new ImageAttributes();

            //Paint(g, width, height, nScaleLevel, fLongitude, fLatitude,imageAttributes);
            Paint(g, width, height, nScaleLevel, fLongitude, fLatitude);
            return image1;
        }

        public override LongLat OffSet(int x, int y) {
            return OffSet(zeroLongLat, Getlevel(1), -x, -y);
        }

        public override LongLat OffSetZero(int x, int y) {
            return OffSet(zeroLongLat, scaleLevel, x, y);
        }

        public override LongLat ParseToLongLat(int x, int y) {
            return OffSet(zeroLongLat, Getlevel(1), -x, -y);
        }

        public override LongLat OffSet(LongLat longlat1, int scale, int x, int y) {
            Point pt = latLngToPixel((float)longlat1.Latitude, (float)longlat1.Longitude, scale);
            pt.Offset(-x, -y);
            PointF pt2 = PixelTolatLng(pt, scale);
            return new LongLat(pt2.Y, pt2.X);
        }

        public override System.Drawing.PointF ParseToPoint(double _Longitude, double _Latitude) {
            PointF ptf1 = mapHelper.LatLngToPixel((float)zeroLongLat.Latitude, (float)zeroLongLat.Longitude, scaleLevel);
            PointF ptf2 = mapHelper.LatLngToPixel((float)_Latitude, (float)_Longitude, scaleLevel);

            return new PointF((ptf2.X - ptf1.X), (ptf2.Y - ptf1.Y));
        }

        public override IntXY getXY(double _Longitude, double _Latitude) {
            PointF ptf1 = zeroCenter;
            PointF ptf2 = mapHelper.LatLngToPixel((float)_Latitude, (float)_Longitude, Getlevel(1));

            return new IntXY(ptf2.X - ptf1.X, ptf2.Y - ptf1.Y);
        }

        public override string[] ScaleRange() {
            string[] range1 = new string[] { "800%", "400%", "200%", "100%", "50%", "25%", "12.5%", "6.25%", "3.125%", "1.5625%", "0.78125%" };//, "0.390625%"
            return range1;
        }
        public static string[] ScaleRanges {
            get {
                string[] range1 = new string[] { "800%", "400%", "200%", "100%", "50%", "25%", "12.5%", "6.25%", "3.125%", "1.5625%", "0.78125%" };//, "0.390625%" 
                return range1;
            }
        }

        public override IDataHelper DataHelper {
            get { return dataHelper; }
            set { dataHelper = value; }
        }

        public override ClassImage FindImage(System.Drawing.Point p) {
            foreach (ClassImage img in listImages) {
                if (!img.IsDiscard) {
                    Rectangle rt = new Rectangle(img.Left, img.Top, nPerWidth, nPerWidth);
                    if (rt.Contains(p))
                        return img;
                }
            }
            return null;
        }
        private ClassImage FindImage(string strPic) {
            foreach (ClassImage img in listImages) {
                if (img.PicUrl == strPic) {
                    return img;
                }
            }
            return null;
        }
        public override LongLat OffSet(LongLat longlat1, int scale, float x, float y) {
            PointF pt = mapHelper.LatLngToPixelF((float)longlat1.Latitude, (float)longlat1.Longitude, scale);
            pt.X += -x;
            pt.Y += -y;
            PointF pt2 = PixelTolatLng(pt, scale);
            return new LongLat(pt2.Y, pt2.X);
        }

        public override LongLat OffSetZero(int level, float x, float y) {
            return OffSet(zeroLongLat, Getlevel(1), -x, -y);
        }

        public override double Scaleoffset(int scale) {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ClassImage> GetMapList(int width, int height, System.Drawing.PointF center) {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ClassImage> GetMapList(int width, int height, int nScaleLevel, double fLongitude, double fLatitude) {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void ClearBuff() {
            listImages.Clear();
        }

        public override IList<ClassImage> GetMapList(int width, int height, System.Drawing.Point point) {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void SetImage(ClassImage image) {
            dataHelper.SetImage(image);
        }

        public override double Latitude {
            get {
                return latitude;
            }
            set {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public override double Longgitude {
            get {
                return longgitude;
            }
            set {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public override int PicWidth {
            get {
                return nPerWidth;
            }
            set {
                nPerWidth = value;
            }
        }

        public override LongLat ZeroLongLat {
            get {
                return zeroLongLat;
            }
            set {
                zeroLongLat = value;
                zeroCenter = mapHelper.LatLngToPixelF((float)zeroLongLat.Latitude, (float)zeroLongLat.Longitude, Getlevel(1));
            }
        }

        public override LongLat ParseToLongLat(float x, float y) {
            PointF pf = PixelTolatLng(new PointF(x, y), scaleLevel);
            return new LongLat(pf.Y, pf.X);
        }

        

        public PointF GetLatLng() {

            return PixelTolatLng(centerP, scaleLevel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pixel">(27034,12405)</param>
        /// <param name="zoom">7</param>
        /// <returns></returns>
        private PointF PixelTolatLng(PointF pixel, float zoom) {

            return mapHelper.PixelTolatLng(pixel, zoom);
        }
        private string getBaseUrl(string[] urls, int x, int y) {
            return mapHelper.GetBaseUrl(urls, x, y);
        }
        private string getTile(Point tile, float zoom) {
            return mapHelper.GetTile(tile, zoom);
        }
        private string getTileUrl(string[] baseUrls, Point tile, float zoom) {
            return mapHelper.GetTileUrl(baseUrls, tile, zoom);
        }
        private Point latLngToPixel(float lat, float lng, float zoom) {
            return mapHelper.LatLngToPixel(lat, lng, zoom);
        }
    }
}
