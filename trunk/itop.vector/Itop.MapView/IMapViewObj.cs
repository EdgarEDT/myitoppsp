using System;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Drawing;
namespace Itop.MapView
{
    public interface IMapViewObj
    {
        double CountLength(System.Drawing.PointF p1, System.Drawing.PointF p2);
        double CountLength(System.Drawing.PointF[] pts);
        double CountLength(LongLat p1, LongLat p2);
        System.Drawing.Image CreateMap(int width, int height, int nScaleLevel, double fLongitude, double fLatitude);
        List<ClassImage> GetMapList(int width, int height, int nScaleLevel, double fLongitude, double fLatitude);
        string GetMiles(int scale);
        IntXY getXY(double _Longitude, double _Latitude);
        IntXY getXY(decimal _Longitude, decimal _Latitude);
        double Latitude { get; set; }
        double Longgitude { get; set; }
        int PicWidth{ get; set; }
        LongLat OffSet(LongLat longlat1, int scale, int x, int y);
        LongLat OffSet(LongLat longlat1, int scale, float x, float y);
        LongLat OffSet(int x, int y);
        LongLat OffSetZero(int level, float x, float y);
        LongLat OffSetZero(int x, int y);
        void Paint(System.Drawing.Graphics g, int width, int height, int nScaleLevel, double fLongitude, double fLatitude,ImageAttributes imageAttributes);
        LongLat ParseToLongLat(int x, int y);
        LongLat ParseToLongLat(float x, float y);
        System.Drawing.PointF ParseToPoint(double _Longitude, double _Latitude);
        System.Drawing.PointF ParseToPoint(decimal _Longitude, decimal _Latitude);
        LongLat ZeroLongLat { get; set; }
        int Getlevel(float ScaleUnit);
        string[] ScaleRange();
        IList<ClassImage> GetMapList(int width, int height, System.Drawing.Point point);
        List<ClassImage> GetMapList(int width, int height, System.Drawing.PointF point);
        void ClearBuff();
        void SetImage(ClassImage image);
        ClassImage FindImage(Point p);
        IDataHelper DataHelper{ get; set; }
        int Getlevel(float p, out float nn);
        event DownCompleteEventHandler OnDownCompleted;
        int Getlevel2(float p, out float nn);
    }
}
