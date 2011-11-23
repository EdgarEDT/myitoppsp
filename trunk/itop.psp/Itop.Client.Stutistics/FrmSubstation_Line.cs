using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using Itop.Common;
using System.Collections;
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
    public partial class FrmSubstation_Line : FormBase
    {
        int year = DateTime.Now.Year;
        Ps_Substation_As ps = new Ps_Substation_As();
        public Ps_Substation_As PS
        {
            set {
                ps = value;
            }
        
        }


        public FrmSubstation_Line()
        {
            InitializeComponent();
        }

        private void FrmSubstation_Line_Load(object sender, EventArgs e)
        {
            gridBand11.Caption=(year+1)+"定";
            gridBand12.Caption = (year + 2) + "定";
            gridBand13.Caption = (year + 3) + "定";
            gridBand14.Caption = (year + 4) + "定";
            gridBand15.Caption = (year + 5) + "定";
            gridBand16.Caption = (year + 1) + "定";
            gridBand17.Caption = (year + 2) + "定";
            gridBand18.Caption = (year + 3) + "定";
            gridBand19.Caption = (year + 4) + "定";
            gridBand20.Caption = (year + 5) + "定";
            gridColumn9.FieldName = "m" + (year + 1);
            gridColumn10.FieldName = "m" + (year + 2);
            gridColumn11.FieldName = "m" + (year + 3);
            gridColumn12.FieldName = "m" + (year + 4);
            gridColumn13.FieldName = "m" + (year + 5);
            gridColumn14.FieldName = "n" + (year + 1);
            gridColumn15.FieldName = "n" + (year + 2);
            gridColumn16.FieldName = "n" + (year + 3);
            gridColumn17.FieldName = "n" + (year + 4);
            gridColumn18.FieldName = "n" + (year + 5);

            double zdfh = 0;
            object obj = ps.GetType().GetProperty("y" + year).GetValue(ps, null);
            if (obj != null)
            {
                zdfh = Convert.ToDouble(obj);
            }
            gridColumn4.FieldName = "y" + year;

            IList<Ps_Substation_As> li = new List<Ps_Substation_As>();
            Ps_Substation_As ps1 = new Ps_Substation_As();
            Ps_Substation_As ps2 = new Ps_Substation_As();
            Ps_Substation_As ps3 = new Ps_Substation_As();
            DataConverter.CopyTo<Ps_Substation_As>(ps, ps1);
            DataConverter.CopyTo<Ps_Substation_As>(ps, ps2);
            DataConverter.CopyTo<Ps_Substation_As>(ps, ps3);
            switch (ps.XHL)
            { 
                case 2:

                    li.Add(ps);
                    li.Add(ps1);
                    break;
                case 3:
                    li.Add(ps);
                    li.Add(ps1);
                    li.Add(ps2);

                    break;
                case 4:
                    li.Add(ps);
                    li.Add(ps1);
                    li.Add(ps2);
                    li.Add(ps3);
                    break;
                default:
                    li.Add(ps);
                    li.Add(ps1);
                    break;
            
            
            }



            this.gridControl1.DataSource = li;

        }
    }
}