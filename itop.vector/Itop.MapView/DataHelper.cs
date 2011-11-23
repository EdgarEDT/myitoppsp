using System;
using System.Text;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using System.Windows.Forms;
using Db4objects.Db4o.Config;
using System.Drawing;
using System.IO;
using Itop.MapView.Tables;
using System.ComponentModel;
using System.Configuration;
using System.Drawing.Imaging;



namespace Itop.MapView {
    public class DataHelper : Itop.MapView.IDataHelper {
        static IObjectContainer db;
        System.Net.WebClient webClient = new System.Net.WebClient();
        string strImgsvrUrl = "http://dimg.51ditu.com/";// "http://mappng.baidu.com/maplite/mapbank/baidu/";
        string storeImgPath = "MapData.yap";//"BaiduData.yap";

        public string ImgsvrUrl {
            get { return strImgsvrUrl; }
            set { strImgsvrUrl = value; }
        }
        public string ImgPath {
            get { return storeImgPath; }
            set {
                storeImgPath = value;
                if (db != null) { db.Close(); db = null; }
                IConfiguration config = Db4oFactory.CloneConfiguration();
                //config.LockDatabaseFile(false);
                config.ObjectClass(typeof(Itop.MapView.Tables.MapClass)).ObjectField("_picUrl").Indexed(true);
                db = Db4oFactory.OpenFile(config,Application.StartupPath + "\\" + storeImgPath);
            }
        }
        bool isdownmap = false;

        public bool IsDownMap {
            get { return isdownmap; }
            set { isdownmap = value; }
        }
        public IObjectContainer Db {
            set { db = value; }
            get { return db; }
        }
        string GetDatabse() {
            string database = string.Empty;
            try {
                database = ConfigurationManager.AppSettings["database"];
            } catch { }
            if (string.IsNullOrEmpty(database)) database = storeImgPath;
            return database;
        }
        public DataHelper()
            : this("") {
        }
        public DataHelper(string database) {
            IConfiguration config = Db4oFactory.Configure();
            //config.LockDatabaseFile(false);
            config.ObjectClass(typeof(Itop.MapView.Tables.MapClass)).ObjectField("_picUrl").Indexed(true);
            if (db != null) db.Close();
            if (database == "")
                storeImgPath = GetDatabse();
            else
                storeImgPath = database;
            db = Db4oFactory.OpenFile(Application.StartupPath + "\\" + storeImgPath);
        }
        public  ClassImage GetImage(string src) {
            ClassImage img1 = null;
            IQuery query = db.Query();
            query.Constrain(typeof(MapClass));
            query.Descend("_picUrl").Constrain(src);

            IObjectSet result = query.Execute();

            img1 = new ClassImage();
            img1.PicUrl = src;

            if (result.Count > 0) {
                MapClass map = result[0] as MapClass;
                try {
                    img1.PicImage = Bitmap.FromStream(new MemoryStream(map.Stream));
                } catch { }
            } else if (IsDownMap) {
                img1.PicImage = downloadmap(src);
            }

            return img1;
        }
        
        public ClassImage GetImageBak(string src) {
            ClassImage img1 = null;
            IQuery query = db.Query();
            query.Constrain(typeof(MapClass));
            query.Descend("_picUrl").Constrain("_" + src);

            IObjectSet result = query.Execute();

            img1 = new ClassImage();
            img1.PicUrl = src;

            if (result.Count > 0) {
                MapClass map = result[0] as MapClass;

                img1.PicImage = Bitmap.FromStream(new MemoryStream(map.Stream));
            } else {
            }
            return img1;
        }

        private Image downloadmap(string src) {
            Image img = null;
            try {
                byte[] buff = webClient.DownloadData(strImgsvrUrl + src);
                if (buff != null && buff.Length > 0) {
                    img = Bitmap.FromStream(new MemoryStream(buff));
                    db.Set(new MapClass(src, buff));
                }
            } catch { }
            return img;
        }
        internal void Delete(string p) {
            MapClass map = new MapClass();
            map.PicUrl = p;
            IObjectSet result = db.Get(map);
            foreach (MapClass obj in result) {
                db.Delete(obj);
            }
        }
        public void SetImage(ClassImage obj) {
            MapClass map = new MapClass();
            MemoryStream stream = new MemoryStream();
            map.PicUrl = obj.PicUrl;
            obj.PicImage.Save(stream, ImageFormat.Png);
            map.Stream = stream.ToArray();
            stream.Close();
            Delete(obj.PicUrl);
            db.Set(map);
            db.Commit();
        }

        #region IDataHelper 成员


        public ClassImage GetImage(string baseUrl, string strPic) {
            throw new Exception("The method or operation is not implemented.");
        }
        public ClassImage GetImages(string baseUrl, string strPic)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion

        #region IDataHelper 成员


        public event DownCompleteEventHandler OnDownCompleted;

        #endregion
    }

}
