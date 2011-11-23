using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using Itop.Client.Base;
namespace Itop.TLPsp
{
    public partial class OpenFile : FormBase
    {
        bool fkk=true;
        public OpenFile(int op)
        {
            InitializeComponent();

            simpleButton2.Visible = true;
            if (op == 0)
            {
                fkk = false;
            }
            else
            {
                fkk = true;
            }
            InitData();
           
            
            
            //this.Column3.Visible = Column3Visble;
        }

        public void InitData()
        {
            
            PSPDIR dir = new PSPDIR();
            //if (simpleButton2.Visible == false)
            //simpleButton2.Visible
            if (fkk == false)
            {
                dir.FileType = "潮流";
                IList list = Services.BaseService.GetList("SelectPSPDIRByFileType",dir);
                DataTable dataSvg = Itop.Common.DataConverter.ToDataTable(list, typeof(PSPDIR));
                dataGridView1.DataSource = dataSvg;
            }//this.textBox7.TextChanged += new System.EventHandler(this.textBox7_TextChanged);
            else
            {
                //dir.FileType = "潮流";
                //IList list = Services.BaseService.GetList("SelectPSPDIRByFileType", dir);
                //DataTable datasvg = Itop.Common.DataConverter.ToDataTable(list, typeof(PSPDIR));
                //datasvg.
                
                dir.FileType = "短路";
                IList list2 = Services.BaseService.GetList("SelectPSPDIRByFileType", dir);
                DataTable datasvg2 = Itop.Common.DataConverter.ToDataTable(list2, typeof(PSPDIR));
                //datasvg.Merge(datasvg2);
                dataGridView1.DataSource = datasvg2;
            }
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                if ((MessageBox.Show(this, "确定要删除文件：" + dataGridView1.CurrentRow.Cells["Column1"].Value + "?", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
                {
                    string guid = dataGridView1.SelectedRows[0].Cells["Column2"].Value.ToString();
                    Services.BaseService.DeleteByKey<PSPDIR>(guid);
                    Services.BaseService.DeleteByKey<SVGFILE>(guid);
                    PSPDEV pspDev = new PSPDEV();
                    pspDev.SvgUID = guid;
                    IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUID", pspDev);
                    foreach (PSPDEV dev in list)
                    {
                        Services.BaseService.Delete<PSPDEV>(dev);
                    }
                    InitData(); 
                } 
                else
                {
                    return;
                }
               
            }
                  
        }  
        public string FileType
        {
            get
            {
                if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows[0].DataBoundItem != null)
                {
                    return dataGridView1.SelectedRows[0].Cells["Column3"].Value.ToString();
                }
                else
                {
                    return null;
                }
            }
       
        }
        public bool button2Visble
        {
            get
            {
                return simpleButton2.Visible;
            }
            set
            {
                int i = 1;
                simpleButton2.Visible = value;
            }
        }

        public bool Column3Visble
        {
            get
            {
                return Column3.Visible;
            }
            set
            {
                Column3.Visible = value;
            }
        }
        public bool fuu
        {
            get
            {
                return fkk;
            }
            set
            {
                fkk = value;
            }
        }
        public string FileGUID
        {
            get 
            {
                if(dataGridView1.SelectedRows.Count>0&&dataGridView1.SelectedRows[0].DataBoundItem!=null)
                {
                    return dataGridView1.SelectedRows[0].Cells["Column2"].Value.ToString(); 
                }
                else
                {
                    return null;
                }
                                
                
            }
        }

        private void simpleButton2_VisibleChanged(object sender, EventArgs e)
        {
            if (fuu == false)
                fkk = false;
            //fkk = 0;
            //Column3.Visible = false;
            //if (fk == false)
            //{ simpleButton2.Visible = false; }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow!=null)
            {                
                string guid = dataGridView1.SelectedRows[0].Cells["Column2"].Value.ToString();
                PSPDIR pspDIR = new PSPDIR();
                pspDIR.FileGUID = guid;
                pspDIR = Services.BaseService.GetOneByKey<PSPDIR>(pspDIR);
                SVGFILE svgFile = new SVGFILE();
                svgFile.SUID = guid;
                svgFile = Services.BaseService.GetOneByKey<SVGFILE>(svgFile);
                if (pspDIR!=null&&svgFile!=null)
                {
                    frmSvgFile frmFile = new frmSvgFile(pspDIR);
                    if (frmFile.ShowDialog() == DialogResult.OK)
                    {
                        pspDIR.FileName = frmFile.SvgFileName;
                        svgFile.FILENAME = frmFile.SvgFileName;
                        Services.BaseService.Update<PSPDIR>(pspDIR);
                        Services.BaseService.Update<SVGFILE>(svgFile);
                        InitData();
                    }
                }                
            }
        }
    }
}