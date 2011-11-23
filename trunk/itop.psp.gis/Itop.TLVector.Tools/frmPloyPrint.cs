using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmPloyPrint : FormBase
    {
        IList<Dlph> list;
        public frmPloyPrint()
        {
            InitializeComponent();
            list = new List<Dlph>();
        }
        public void InitDate(string eleid,string svguid)
        {
            glebeProperty gle = new glebeProperty();
            gle.EleID = eleid;
            gle.SvgUID = svguid;
            gle=(glebeProperty)Services.BaseService.GetObject("SelectglebePropertyByEleID",gle);
            if (gle!=null)
            {
            //string strCon = gle.SelSonArea;
            Hashtable hs1 = new Hashtable();
            Hashtable hs2 = new Hashtable();
            if (gle.SelSonArea != "")
            {

                string[] selArea = gle.SelSonArea.Split(";".ToCharArray());
                for (int i = 0; i < selArea.Length; i++)
                {
                    if (selArea[i] != "")
                    {
                        string[] _SonArea = selArea[i].Split(",".ToCharArray());
                        hs1.Add(_SonArea[0], _SonArea[1]);
                    }
                }
                IEnumerator col= hs1.Keys.GetEnumerator();
                while(col.MoveNext())
                {
                    string key=(string)col.Current;
                    glebeProperty _p=new glebeProperty();
                    _p.EleID=key;
                    _p.SvgUID=svguid;
                    _p=(glebeProperty) Services.BaseService.GetObject("SelectglebePropertyByEleIDForPrint",_p);
                    Dlph d=new Dlph();
                    d.Notes = _p.TypeUID;
                    if ((string)hs1[key] == "") { d.Number1 = 0; }
                    else { d.Number1 = Convert.ToDecimal(hs1[key]); }
                    d.Number2 =  _p.Burthen * (d.Number1 / _p.Area);

                    if (!hs2.Contains(_p.TypeUID))
                    {
                        hs2.Add(_p.TypeUID, d);
                    }
                    else
                    {
                        Dlph d1 = (Dlph)hs2[_p.TypeUID];
                        d1.Number1 = d1.Number1 + d.Number1;
                        d1.Number2 = d1.Number2 + d.Number2;
                        //d1.Number2 = Convert.ToDecimal(Convert.ToDecimal(d1.Number2).ToString("#####.####"));
                        hs2[_p.TypeUID] = d1;
                    }

                    
                }
            }
            IEnumerator col2=hs2.Values.GetEnumerator();
            while(col2.MoveNext()){
                Dlph d = (Dlph)col2.Current;
                d.Number2 = Convert.ToDecimal(Convert.ToDecimal(d.Number2).ToString("#####.####"));
                list.Add(d);
            }
            gridControl.DataSource = list;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ComponentPrint.ShowPreview(this.gridControl, "区域统计报表");
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
             saveFileDialog1.Filter = "Excel文件 (*.xls)|*.xls";
            saveFileDialog1.ShowDialog();
            string fname = saveFileDialog1.FileName;
            if (fname != "")
            {
                gridControl.ExportToExcelOld(fname);
                MessageBox.Show("导出成功。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            
        }
    }
}