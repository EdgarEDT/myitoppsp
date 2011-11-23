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

    public abstract class MapViewBase : IMapViewObj
    {

        public MapViewBase() {
        }
        public event DownCompleteEventHandler OnDownCompleted;
        public virtual bool ShowMapInfo {
            get { return showmapinfor; }
            set { showmapinfor = value; }
        }
        private bool showmapinfor=false;
        public int ZoneWide = 3; //带宽
        double X0 = 0, Y0 = 0;
        protected void downCompleted(ClassImage image) {
            if (OnDownCompleted != null) {
                OnDownCompleted(image);
            }
        }
        /// <summary>
        /// 54坐标转经纬度
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public double[] XY54toLB(double X, double Y) {
            int ProjNo;
            ////带宽 
            double longitude1, latitude1, longitude0, latitude0, xval, yval;
            double e1, e2, f, a, ee, NN, T, C, M, D, R, u, fai, iPI;
            iPI = 0.0174532925199433; ////3.1415926535898/180.0; 
            a = 6378245.0; f = 1.0 / 298.3; //54年北京坐标系参数

            ProjNo = (int)(X / 1000000L); //查找带号
            if (ZoneWide == 6)
                longitude0 = (ProjNo - 1) * ZoneWide + ZoneWide / 2.0;
            else
                longitude0 = (ProjNo) * ZoneWide;
            //longitude0 = 108; //河池地区
            longitude0 = longitude0 * iPI; //中央经线 
            X0 = ProjNo * 1000000L + 500000L;
            Y0 = 0;
            xval = X - X0;
            yval = Y - Y0; //带内大地坐标 
            e2 = 2 * f - f * f;
            e1 = (1.0 - Math.Sqrt(1 - e2)) / (1.0 + Math.Sqrt(1.0 - e2));
            ee = e2 / (1.0 - e2);
            M = yval; u = M / (a * (1.0 - e2 / 4.0 - 3 * e2 * e2 / 64.0 - 5 * e2 * e2 * e2 / 256.0));
            fai = u + (3 * e1 / 2.0 - 27 * e1 * e1 * e1 / 32.0) * Math.Sin(2 * u) + (21 * e1 * e1 / 16.0 - 55 * e1 * e1 * e1 * e1 / 32.0) * Math.Sin(4 * u) + (151 * e1 * e1 * e1 / 96.0) * Math.Sin(6 * u) + (1097 * e1 * e1 * e1 * e1 / 512.0) * Math.Sin(8 * u);
            C = ee * Math.Cos(fai) * Math.Cos(fai);
            T = Math.Tan(fai) * Math.Tan(fai);
            NN = a / Math.Sqrt(1.0 - e2 * Math.Sin(fai) * Math.Sin(fai));
            R = a * (1.0 - e2) / Math.Sqrt((1.0 - e2 * Math.Sin(fai) * Math.Sin(fai)) * (1.0 - e2 * Math.Sin(fai) * Math.Sin(fai)) * (1.0 - e2 * Math.Sin(fai) * Math.Sin(fai)));
            D = xval / NN;
            //计算经度(Longitude) 纬度(Latitude) 
            longitude1 = longitude0 + (D - (1 + 2 * T + C) * D * D * D / 6.0 + (5.0 - 2 * C + 28 * T - 3 * C * C + 8 * ee + 24 * T * T) * D * D * D * D * D / 120.0) / Math.Cos(fai);
            latitude1 = fai - (NN * Math.Tan(fai) / R) * (D * D / 2.0 - (5 + 3 * T + 10 * C - 4 * C * C - 9 * ee) * D * D * D * D / 24.0 + (61 + 90 * T + 298 * C + 45 * T * T - 256 * ee - 3 * C * C) * D * D * D * D * D * D / 720.0); //转换为度        DD 
            double longitude;
            double latitude;
            longitude = longitude1 / iPI;
            latitude = latitude1 / iPI;
            return new double[] { longitude, latitude };
        }
        /// <summary>
        /// 经纬度转５４坐标
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        public double[] LBtoXY54(double longitude, double latitude) {
            int ProjNo = 0;
            double longitude1, latitude1, longitude0, latitude0, xval, yval; ////0.0174532925199433; 
            double a, f, e2, ee, NN, T, C, A, M, iPI; iPI = 3.1415926535898 / 180.0;
            a = 6378245.0; f = 1.0 / 298.3; //54年北京坐标系参数 
            ProjNo = (int)(longitude / ZoneWide);
            if (ZoneWide == 6)
                longitude0 = ProjNo * ZoneWide + ZoneWide / 2.0;
            else
                longitude0 = ProjNo * ZoneWide;
            //longitude0 = 108; //河池地区
            longitude0 = longitude0 * iPI;
            latitude0 = 0;
            longitude1 = longitude * iPI; //经度转换为弧度 
            latitude1 = latitude * iPI; //纬度转换为弧度 
            e2 = 2.0 * f - f * f; ee = e2 * (1.0 - e2);
            NN = a / Math.Sqrt(1.0 - e2 * Math.Sin(latitude1) * Math.Sin(latitude1));
            T = Math.Tan(latitude1) * Math.Tan(latitude1);
            C = ee * Math.Cos(latitude1) * Math.Cos(latitude1);
            A = (longitude1 - longitude0) * Math.Cos(latitude1);
            M = a * ((1.0 - e2 / 4.0 - 3.0 * e2 * e2 / 64.0 - 5 * e2 * e2 * e2 / 256.0) * latitude1 - (3 * e2 / 8.0 + 3 * e2 * e2 / 32.0 + 45 * e2 * e2 * e2 / 1024.0) * Math.Sin(2 * latitude1) + (15 * e2 * e2 / 256.0 + 45 * e2 * e2 * e2 / 1024.0) * Math.Sin(4.0 * latitude1) - (35 * e2 * e2 * e2 / 3072.0) * Math.Sin(6 * latitude1));
            xval = NN * (A + (1.0 - T + C) * A * A * A / 6.0 + (5.0 - 18 * T + T * T + 72 * C - 58 * ee) * A * A * A * A * A / 120.0);
            yval = M + NN * Math.Tan(latitude1) * (A * A / 2.0 + (5.0 - T + 9 * C + 4 * C * C) * A * A * A * A / 24.0 + (61.0 - 58 * T + T * T + 600 * C - 330 * ee) * A * A * A * A * A * A / 720.0);
            if (ZoneWide == 6)
                X0 = 1000000L * (ProjNo + 1) + 500000L;
            else
                X0 = 1000000L * ProjNo + 500000L;
            Y0 = 0;
            //X0 = 0;
            xval = xval + X0;
            yval = yval + Y0;
            return new double[] { xval, yval };
        }
        public abstract int Getlevel(float ScaleUnit);
        public abstract int Getlevel2(float ScaleUnit, out float scale);
        public abstract int Getlevel(float ScaleUnit, out float scale);
        public abstract string GetMiles(int scale);
        /// <summary>
        /// 反回长度（单位公里）
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public abstract double CountLength(PointF p1, PointF p2);
        /// <summary>
        /// 绘制地图 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="nScaleLevel"></param>
        /// <param name="fLongitude"></param>
        /// <param name="fLatitude"></param>
        public abstract void Paint(Graphics g, int width, int height, int nScaleLevel, double fLongitude, double fLatitude, ImageAttributes imagAttributes);
        public abstract void Paint(Graphics g, int width, int height, int nScaleLevel, double fLongitude, double fLatitude);
        /// <summary>
        /// 创建地图到Image中
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="nScaleLevel"></param>
        /// <param name="fLongitude"></param>
        /// <param name="fLatitude"></param>
        /// <returns></returns>
        public abstract Image CreateMap(int width, int height, int nScaleLevel, double fLongitude, double fLatitude);
        /// <summary>
        /// 返回偏移指定象素后的经纬度
        /// </summary>
        /// <param name="x">经度偏移象素</param>
        /// <param name="y">纬度编移象素</param>
        /// <returns></returns>
        public abstract LongLat OffSet(int x, int y);
        /// <summary>
        /// x,y 加负号
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public abstract LongLat OffSetZero(int x, int y);
        //<summary>

        //</summary>
        //<param name="x"></param>
        //<param name="y"></param>
        //<returns></returns>
        public abstract LongLat ParseToLongLat(int x, int y);
        /// <summary>
        /// x,y 加负号
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public abstract LongLat OffSet(LongLat longlat1, int scale, int x, int y);
        public abstract PointF ParseToPoint(double _Longitude, double _Latitude);
        public abstract IntXY getXY(double _Longitude, double _Latitude);
        public abstract string[] ScaleRange();
        public abstract IDataHelper DataHelper { get; set; }

        #region 私有方法
        public abstract ClassImage FindImage(Point p);
        #endregion

        #region IMapViewObj 成员



        public abstract LongLat OffSet(LongLat longlat1, int scale, float x, float y);
        //public LongLat OffSetZero(int x, int y)
        //{
        //    double d1 = ZeroLongLat.Longitude - (x * d2H[scaleLevel] / nPerWidth);
        //    double d2 = ZeroLongLat.Latitude + (y * qxl3[scaleLevel] / nPerWidth);
        //    return new LongLat(d1, d2);
        //}

        public abstract LongLat OffSetZero(int level, float x, float y);

        //public LongLat OffSetZero(float x, float y)
        //{
        //    double d1 = ZeroLongLat.Longitude - ((Convert.Todouble(x)) * d2H[scaleLevel] / nPerWidth);
        //    double d2 = ZeroLongLat.Latitude + ((Convert.Todouble(y)) * qxl3[scaleLevel] / nPerWidth);
        //    return new LongLat(d1, d2);
        //}

        public abstract double Scaleoffset(int scale);



        
        public abstract List<ClassImage> GetMapList(int width, int height, int nScaleLevel, double fLongitude, double fLatitude);

        public abstract void ClearBuff();

        public abstract IList<ClassImage> GetMapList(int width, int height, Point point);
        public abstract List<ClassImage> GetMapList(int width, int height, PointF point);

        public abstract void SetImage(ClassImage image);

        #endregion

        #region IMapViewObj 成员


        public abstract double Latitude {
            get;
            set;
        }

        public abstract double Longgitude {
            get;
            set;
        }

        public abstract int PicWidth {
            get;
            set;
        }

        public abstract LongLat ZeroLongLat {
            get;
            set;
        }

        #endregion

        #region IMapViewObj 成员

        public virtual PointF ParseToPoint(decimal _Longitude, decimal _Latitude) {
            return ParseToPoint((double)_Longitude, (double)_Latitude);
        }
        public virtual IntXY getXY(decimal _Longitude, decimal _Latitude) {
            return getXY((double)_Longitude, (double)_Latitude);
        }
        public abstract LongLat ParseToLongLat(float x, float y);

        #endregion

        #region IMapViewObj 成员


        public virtual double CountLength(PointF[] pts) {
            throw new Exception("方法没有完成！");
        }

        public virtual double CountLength(LongLat p1, LongLat p2) {
            throw new Exception("方法没有完成");
        }

        #endregion
    }



}
