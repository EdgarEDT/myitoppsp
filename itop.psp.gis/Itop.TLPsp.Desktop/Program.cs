using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Itop.TLPSP.DEVICE;
using Itop.TLPsp.Graphical;

namespace Itop.TLPsp.Desktop
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Itop.Server.Impl.BaseService bs = new Itop.Server.Impl.BaseService();
            UCDeviceBase.DataService = bs;
            Itop.Client.Common.Services.BaseService = bs;
            new Itop.Domain.Graphics.LayerFile();
            Itop.Client.MIS.ProgUID = "rabbit";
            
            Application.Run(new Form1());
        }
    }
}