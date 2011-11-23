using System;
namespace Itop.MapView
{
    public interface IDataHelper
    {
        Db4objects.Db4o.IObjectContainer Db { set; get;}
        ClassImage GetImage(string src);
        string ImgPath { get; set; }
        string ImgsvrUrl { get; set; }
        bool IsDownMap { get; set; }
        void SetImage(ClassImage obj);
        event DownCompleteEventHandler OnDownCompleted;
        ClassImage GetImage(string baseUrl, string strPic);
        ClassImage GetImages(string baseUrl, string strPic);//下载使用
    }
}
