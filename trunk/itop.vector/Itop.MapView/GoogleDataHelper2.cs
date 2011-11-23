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
using System.Collections;



namespace Itop.MapView
{
    
    public class GoogleDataHelper2 : IDataHelper
    {
        static IObjectContainer db;
        System.Net.WebClient webClient = new System.Net.WebClient();
        string strImgsvrUrl = "";
        string storeImgPath = "MapData2.map";
        string opt_authtoken = "fzwq2n-g8iTAxm1_cQIhq9zGlbp1KZ815jBvtA";
        public event DownCompleteEventHandler OnDownCompleted;
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
                db = Db4oFactory.OpenFile(config, Application.StartupPath + "\\" + storeImgPath);
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
                database = ConfigurationManager.AppSettings["database2"];
            } catch { }
            if (string.IsNullOrEmpty(database)) database = storeImgPath;
            return database;
        }
        public GoogleDataHelper2()
            : this("datamap2.map") {
        }
        public GoogleDataHelper2(string database)
            : this(database, "m2_") {
        }
        public GoogleDataHelper2(string database, string maptype) {
            mapType = maptype;
            IConfiguration config = Db4oFactory.Configure();
            //config.LockDatabaseFile(false);
            config.ObjectClass(typeof(Itop.MapView.Tables.MapClass)).ObjectField("_picUrl").Indexed(true);

            if (database == "")
                storeImgPath = GetDatabse();
            else
                storeImgPath = database;

            if (db != null)
                db.Close();

            db = Db4oFactory.OpenFile(Application.StartupPath + "\\" + storeImgPath);
        }
        string mapType = "m2_";
        public ClassImage GetImage(string src) {
            if (db == null) return null;
            string key = mapType + src;
            ClassImage img1 = null;
            IQuery query = db.Query();
            query.Constrain(typeof(MapClass));
            query.Descend("_picUrl").Constrain(key);

            IObjectSet result = query.Execute();

            img1 = new ClassImage();
            img1.PicUrl = key;

            if (result.Count > 0) {
                MapClass map = result[0] as MapClass;
                try {
                    img1.PicImage = Bitmap.FromStream(new MemoryStream(map.Stream));
                } catch { }
            } else if (IsDownMap && !downlist.ContainsKey(src)&&downlist.Count<100) {//多线程下载
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                img1.DownUrl = src;
                bw.RunWorkerAsync(img1);
            }

            return img1;
        }
        public ClassImage GetImages(string src)
        {
            if (db == null) return null;
            string key = mapType + src;
            ClassImage img1 = null;
            IQuery query = db.Query();
            query.Constrain(typeof(MapClass));
            query.Descend("_picUrl").Constrain(key);

            IObjectSet result = query.Execute();

            img1 = new ClassImage();
            img1.PicUrl = key;

            if (result.Count > 0)
            {
                MapClass map = result[0] as MapClass;
                try
                {
                    img1.PicImage = Bitmap.FromStream(new MemoryStream(map.Stream));
                }
                catch { }
            }
            else if (IsDownMap && !downlist.ContainsKey(src) && downlist.Count < 10)
            {//多线程下载
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                img1.DownUrl = src;
                bw.RunWorkerAsync(img1);
            }
            else if (IsDownMap && !downlist.ContainsKey(src) && downlist.Count >= 10)
            {
                while (downlist.Count >= 10)
                {

                }
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                img1.DownUrl = src;
                bw.RunWorkerAsync(img1);
            }

            return img1;
        }
        Hashtable downlist = new Hashtable();
        void bw_DoWork(object sender, DoWorkEventArgs e) {
            ClassImage img1 = e.Argument as ClassImage;
            //lock (downlist)
            if (!downlist.ContainsKey(img1.DownUrl)) {
                downlist.Add(img1.DownUrl, img1);//增加下载地址
                img1.PicImage = downloadmap(img1.DownUrl);
                downlist.Remove(img1.DownUrl);//移除下载地址
                if (OnDownCompleted != null)
                    OnDownCompleted(img1);
            }           
        }
        public bool Exists(string key) {
            IQuery query = db.Query();
            query.Constrain(typeof(MapClass));
            query.Descend("_picUrl").Constrain(key);

            return query.Execute().Count > 0 ? true : false;
        }
        private Image downloadmap(string src) {
            Image img = null;
            string url = "cookie=" + opt_authtoken + "&" + src;           
            Uri strU = new Uri(strImgsvrUrl + src);
            try {
                strImgsvrUrl = "http://khm1.google.cn/kh/v=80&";
                //webClient = new System.Net.WebClient();
                //byte[] buff = webClient.DownloadData(strImgsvrUrl + url);       
                HttpProc.WebClient client = new HttpProc.WebClient();
                Stream stream = client.DownloadData(strImgsvrUrl + url);                
                byte[] buff = new byte[stream.Length];
                stream.Read(buff, 0, (int)stream.Length);
                if (buff != null && buff.Length > 0) {
                    img = Bitmap.FromStream(new MemoryStream(buff));
                   
                    db.Set(new MapClass(mapType + src, buff));
                    db.Commit();
                }
            } catch(Exception err)
            { } 
            finally { }
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
            db.Get(map);
            db.Commit();
        }

        #region IDataHelper 成员


        public ClassImage GetImage(string baseUrl, string strPic) {
            strImgsvrUrl = baseUrl;
            return GetImage(strPic);
        }
        public ClassImage GetImages(string baseUrl, string strPic)
        {
            strImgsvrUrl = baseUrl;
            return GetImages(strPic);
        }
        #endregion


    }

}
