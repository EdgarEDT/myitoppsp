using System;
using System.Drawing;
using System.Windows.Forms;

using QuartzTypeLib;
using ItopVector.Core.Document;

namespace ItopVector.Core.Animate
{
    public class AudioAnimate : Animate
    {
        // Methods
        internal AudioAnimate(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
        {
            this.m_objFilterGraph = null;
            this.m_objBasicAudio = null;
            this.m_objMediaEvent = null;
            this.m_objMediaEventEx = null;
            this.m_objMediaPosition = null;
            this.m_objMediaControl = null;
            this.fillColor = Color.Khaki;
            this.fileName = string.Empty;
            this.m_CurrentStatus = MediaStatus.None;
            this.timer = new Timer();
            this.oldtime = 0f;
            this.timertime = 0f;
            this.timer.Interval = 100;
            this.timer.Tick += new EventHandler(this.TimerTick);
        }

        private void CleanUp()
        {
            if (this.m_objMediaControl != null)
            {
                this.m_objMediaControl.Stop();
            }
            if (this.m_objMediaControl != null)
            {
                this.m_objMediaControl = null;
            }
            if (this.m_objMediaPosition != null)
            {
                this.m_objMediaPosition = null;
            }
            if (this.m_objMediaEventEx != null)
            {
                this.m_objMediaEventEx = null;
            }
            if (this.m_objMediaEvent != null)
            {
                this.m_objMediaEvent = null;
            }
            if (this.m_objBasicAudio != null)
            {
                this.m_objBasicAudio = null;
            }
            if (this.m_objFilterGraph != null)
            {
                this.m_objFilterGraph = null;
            }
        }

        public override object GetAnimateResult(float time, DomType domtype)
        {
            if (this.FileName != string.Empty)
            {
                if ((time < base.Begin) || (time > (base.Begin + base.Duration)))
                {
                    if (this.m_CurrentStatus == MediaStatus.Running)
                    {
                        this.m_objMediaControl.Stop();
                        this.m_objMediaPosition.CurrentPosition = 0;
                        this.m_CurrentStatus = MediaStatus.Stopped;
                        this.timer.Stop();
                    }
                }
                else if (time != this.oldtime)
                {
                    if (this.m_CurrentStatus == MediaStatus.None)
                    {
                        this.m_objMediaControl.Run();
                        this.m_objMediaPosition.CurrentPosition = (time - base.Begin) * 1.2f;
                        this.timer.Start();
                    }
                    else if (this.m_CurrentStatus == MediaStatus.Stopped)
                    {
                        this.m_objMediaPosition.CurrentPosition = (time - base.Begin) * 1.2f;
                        this.m_objMediaControl.Run();
                        this.timer.Start();
                    }
                    this.m_CurrentStatus = MediaStatus.Running;
                }
                else if (this.m_CurrentStatus == MediaStatus.Running)
                {
                    this.m_objMediaControl.Stop();
                    this.m_objMediaPosition.CurrentPosition = 0;
                    this.m_CurrentStatus = MediaStatus.Stopped;
                    this.timer.Stop();
                }
            }
            this.oldtime = time;
            return null;
        }

        private void PlayAudio(float time)
        {
            this.CleanUp();
            this.m_objFilterGraph = new FilgraphManagerClass();
            this.m_objFilterGraph.RenderFile(this.FileName);
            this.m_objBasicAudio = this.m_objFilterGraph as IBasicAudio;
            this.m_objMediaEvent = this.m_objFilterGraph as IMediaEvent;
            this.m_objMediaEventEx = this.m_objFilterGraph as IMediaEventEx;
            this.m_objMediaPosition = this.m_objFilterGraph as IMediaPosition;
            this.m_objMediaControl = this.m_objFilterGraph;
            this.m_objMediaControl.Run();
        }

        private void SelectFile(object sender, EventArgs e)
        {
            OpenFileDialog dialog1 = new OpenFileDialog();
            dialog1.Filter = "mp3 文件 (*.mp3)|*.mp3|wave 文件 (*.wav)|*.wav";
            if (DialogResult.OK == dialog1.ShowDialog())
            {
                this.FileName = dialog1.FileName;
                this.CleanUp();
                this.m_objFilterGraph = new FilgraphManagerClass();
                this.m_objFilterGraph.RenderFile(this.FileName);
                this.m_objBasicAudio = this.m_objFilterGraph as IBasicAudio;
                this.m_objMediaEvent = this.m_objFilterGraph as IMediaEvent;
                this.m_objMediaEventEx = this.m_objFilterGraph as IMediaEventEx;
                this.m_objMediaPosition = this.m_objFilterGraph as IMediaPosition;
                this.m_objMediaControl = this.m_objFilterGraph;
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (this.oldtime == this.timertime)
            {
                if (this.m_CurrentStatus == MediaStatus.Running)
                {
                    this.m_objMediaControl.Stop();
                    this.m_CurrentStatus = MediaStatus.Stopped;
                }
                this.timer.Stop();
            }
            else
            {
                this.timertime = this.oldtime;
            }
        }


        // Properties
        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                this.fileName = value;
                this.CleanUp();
                this.m_objFilterGraph = new FilgraphManagerClass();
                this.m_objFilterGraph.RenderFile(this.FileName);
                this.m_objBasicAudio = this.m_objFilterGraph as IBasicAudio;
                this.m_objMediaEvent = this.m_objFilterGraph as IMediaEvent;
                this.m_objMediaEventEx = this.m_objFilterGraph as IMediaEventEx;
                this.m_objMediaPosition = this.m_objFilterGraph as IMediaPosition;
                this.m_objMediaControl = this.m_objFilterGraph;
            }
        }


        // Fields
        private const int EC_COMPLETE = 1;
        private string fileName;
        private Color fillColor;
        private MediaStatus m_CurrentStatus;
        private IBasicAudio m_objBasicAudio;
        private FilgraphManager m_objFilterGraph;
        private IMediaControl m_objMediaControl;
        private IMediaEvent m_objMediaEvent;
        private IMediaEventEx m_objMediaEventEx;
        private IMediaPosition m_objMediaPosition;
        private float oldtime;
        private Timer timer;
        private float timertime;
        private const int WM_APP = 0x8000;
        private const int WM_GRAPHNOTIFY = 0x8001;
        private const int WS_CHILD = 0x40000000;
        private const int WS_CLIPCHILDREN = 0x2000000;

        // Nested Types
        private enum MediaStatus
        {
            // Fields
            None = 0,
            Paused = 2,
            Running = 3,
            Stopped = 1
        }
    }
}

