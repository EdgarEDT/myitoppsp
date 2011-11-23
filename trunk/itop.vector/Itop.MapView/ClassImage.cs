using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Itop.MapView {
    public class ClassImage {
        private string _picUrl = "";
        private Image _picImage = null;
        private int _left = 0;
        private int _top = 0;
        private bool _isDiscard = true;
        private double longitude = 0;
        private double latitude = 0;
        private string downUrl = "";

        public string DownUrl {
            get { return downUrl; }
            set { downUrl = value; }
        }
        int picWidth = 300;

        
        public ClassImage() {

        }
        public Rectangle Bounds {
            get {
                return new Rectangle(Left, Top, PicWidth, PicWidth);
            }
        }
        public int PicWidth {
            get { return picWidth; }
            set { picWidth = value; }
        }
        public int Width {
            get { return PicWidth; }
        }
        public int Height {
            get { return PicWidth; }
        }
        public int Right {
            get { return _left + PicWidth; }
        }
        public int Bottom {
            get { return _top + PicWidth; }
        }
        public string PicUrl {
            get { return _picUrl; }
            set { _picUrl = value; }
        }

        public Image PicImage {
            get { return _picImage; }
            set { _picImage = value; }
        }

        public int Left {
            get { return _left; }
            set { _left = value; }
        }

        public int Top {
            get { return _top; }
            set { _top = value; }
        }

        public bool IsDiscard {
            get { return _isDiscard; }
            set { _isDiscard = value; }
        }

        //左顶点纬度
        public double Latitude {
            get { return latitude; }
            set { latitude = value; }
        }

        //左顶点经度
        public double Longitude {
            get { return longitude; }
            set { longitude = value; }
        }

        public Rectangle GetBounds() {
            return Bounds;
        }
    }
}
