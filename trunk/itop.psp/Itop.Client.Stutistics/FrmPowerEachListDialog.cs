
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using Itop.Common;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using System.Xml;
using Itop.Domain.Stutistics;
using System.Collections;
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
	public partial class FrmPowerEachListDialog : FormBase
	{

		#region 字段
		protected bool _isCreate = false;
		protected PowerEachList _obj = null;
        private bool isp = false;
        private DataTable dt=new DataTable();
        public bool bl = true;
        public bool bbl = true;
        //private string sid = "";
        private bool isjsxm = false;

        public bool IsJSXM
        {
            get { return isjsxm; }
            set { isjsxm = value; }
        }


        //public string Sid
        //{
        //    get { return sid; }
        //    set { sid = value; }
        //}


		#endregion

		#region 构造方法
		public FrmPowerEachListDialog()
		{
			InitializeComponent();
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// true:创建对象　false:修改对象
		/// </summary>
		public bool IsCreate
		{
			get { return _isCreate; }
			set { _isCreate = value; }
		}

        public bool IsPower
        {
            get { return isp; }
            set { isp = value; }
        }


		/// <summary>
		/// 所邦定的对象
		/// </summary>
		public PowerEachList Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region 事件处理
		private void FrmPowerEachListDialog_Load(object sender, EventArgs e)
		{
			IList<PowerEachList> list = new List<PowerEachList>();
			list.Add(Object);
			this.vGridControl.DataSource = list;
            simpleButton1.Visible = true;
            InitPicData();
            if (isjsxm)
            {
                //if (bbl == false)
                //{
                //    simpleButton1.Visible = false;
                //    InitPicData();
                //}
                //else
                //{
                //    simpleButton1.Visible = true;
                //    InitPicData();
                //}

               
             
            }
          
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (!InputCheck())
			{
				return;
			}

			if (SaveRecord())
			{
				DialogResult = DialogResult.OK;
			}
		}
		#endregion

		#region 辅助方法
		protected bool InputCheck()
        {
            if (_obj.ListName == "")
            {
                MsgBox.Show("分类名称不能为空！");
                return false;

            }
			return true;
		}

		protected bool SaveRecord()
		{
			//创建/修改 对象
			try
			{
				if (IsCreate)
				{
                    //_obj.UID = (int)Services.BaseService.Create<PowerEachList>(_obj);
                    if (!isp)
                    {
                        Common.Services.BaseService.Create("InsertPowerEachList", _obj);
                        //_obj.UID = (string)Common.Services.BaseService.Create("InsertPowerEachList", _obj);
                    }
                    else
                    {
                        if (bl)
                            Common.Services.BaseService.Create("InsertPowerEachListBy", _obj);   
                        else
                            Common.Services.BaseService.Create("InsertPowerEachList", _obj);   
                    }
				}
				else
				{
					Services.BaseService.Update<PowerEachList>(_obj);
				}
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				HandleException.TryCatch(exc);
				return false ;
			}

			//操作已成功
			return true;
		}
		#endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string str = "";

            try
            {
                str = System.Configuration.ConfigurationSettings.AppSettings["SvgID"];

            }
            catch { }
            DataTable dt = new DataTable();
            dt.Columns.Add("A");
            dt.Columns.Add("B");
            dt.Columns.Add("C", typeof(bool));
            dt.Columns.Add("D");

            IList<LayerGrade> li = Services.BaseService.GetList<LayerGrade>("SelectLayerGradeListBySvgDataUid", str);
            IList<SVG_LAYER> li1 = Services.BaseService.GetList<SVG_LAYER>("SelectSVG_LAYERBySvgID", str);

            DataTable dt1 = Itop.Common.DataConverter.ToDataTable((IList)li1);

            foreach (LayerGrade node in li)
            {
                DataRow row = dt.NewRow();
                row["A"] = node.SUID;
                row["B"] = node.Name;
                row["C"] = false;
                row["D"] = node.ParentID;
                dt.Rows.Add(row);
                DataRow[] rows = dt1.Select("YearID='" + node.SUID + "'");
                foreach (DataRow row1 in rows)
                {
                    DataRow row2 = dt.NewRow();
                    row2["A"] = row1["SUID"].ToString();
                    row2["B"] = row1["NAME"].ToString();
                    row2["C"] = false;
                    row2["D"] = node.SUID;
                    dt.Rows.Add(row2);
                }
            }



            //////SVGFILE sf = Services.BaseService.GetOneByKey<SVGFILE>(str);
            //////XmlDocument xd = new XmlDocument();
            //////xd.LoadXml(sf.SVGDATA);

            //////DataTable dt = new DataTable();
            //////dt.Columns.Add("A");
            //////dt.Columns.Add("B");
            //////dt.Columns.Add("C", typeof(bool));


            //////XmlNodeList nli = xd.GetElementsByTagName("layer");


            //////foreach (XmlNode node in nli)
            //////{
            //////    XmlElement xe = (XmlElement)node;
            //////    if (xe.GetAttribute("layerType") == "电网规划层")
            //////    {
            //////        DataRow row = dt.NewRow();
            //////        row["A"] = xe.GetAttribute("id");
            //////        row["B"] = xe.GetAttribute("label");
            //////        row["C"] = false;
            //////        dt.Rows.Add(row);
            //////    }
            //////}




            //foreach (DataRow rws in dt.Rows)
            //{
            //    rws["C"] = false;
            //}

            PowerPicSelect ppsn = new PowerPicSelect();
            ppsn.EachListID = _obj.UID;

            IList<PowerPicSelect> liss = Services.BaseService.GetList<PowerPicSelect>("SelectPowerPicSelectList", ppsn);

            //foreach (PowerPicSelect pps in liss)
            //{
            //    foreach (DataRow rw in dt.Rows)
            //    {
            //        if (pps.PicSelectID == rw["A"].ToString())
            //            rw["C"] = true;
            //    }
            //}



            //FrmPicTypeSelect fpt = new FrmPicTypeSelect();
            FrmPicTreeSelect fpt = new FrmPicTreeSelect();
            fpt.DT = dt;
            if (fpt.ShowDialog() == DialogResult.OK)
            {
                dt = fpt.DT;

                int c = 0;
                foreach (PowerPicSelect pps1 in liss)
                {
                    c = 0;
                    foreach (DataRow rw in dt.Rows)
                    {
                        if (pps1.PicSelectID == rw["A"].ToString() && (bool)rw["C"])
                            c = 1;
                    }
                    if (c == 0)
                    {
                        Services.BaseService.Delete<PowerPicSelect>(pps1);
                    }
                }


                foreach (DataRow rw1 in dt.Rows)
                {
                    c = 0;
                    if ((bool)rw1["C"])
                    {
                        foreach (PowerPicSelect pps2 in liss)
                        {
                            if (pps2.PicSelectID == rw1["A"].ToString())
                                c = 1;
                        }
                        if (c == 0)
                        {
                            PowerPicSelect pp3 = new PowerPicSelect();
                            pp3.EachListID = _obj.UID;
                            pp3.PicSelectID = rw1["A"].ToString();

                            Services.BaseService.Create<PowerPicSelect>(pp3);
                        }
                    }
                }
            }










            //foreach (DataRow rws in dt.Rows)
            //{
            //    rws["C"] = false;
            //}

            //PowerPicSelect ppsn = new PowerPicSelect();
            //ppsn.EachListID = _obj.UID;

            //IList<PowerPicSelect> liss = Services.BaseService.GetList<PowerPicSelect>("SelectPowerPicSelectList", ppsn);

            //foreach (PowerPicSelect pps in liss)
            //{
            //    foreach (DataRow rw in dt.Rows)
            //    {
            //        if (pps.PicSelectID == rw["A"].ToString())
            //            rw["C"] = true;
            //    }
            //}




            //FrmPicTypeSelect fpt = new FrmPicTypeSelect();
            //fpt.DT = dt;
            //if (fpt.ShowDialog() == DialogResult.OK)
            //{
            //    dt = fpt.DT;

            //    int c = 0;
            //    foreach (PowerPicSelect pps1 in liss)
            //    {
            //        c = 0;
            //        foreach (DataRow rw in dt.Rows)
            //        {
            //            if (pps1.PicSelectID == rw["A"].ToString() && (bool)rw["C"])
            //                c = 1;
            //        }
            //        if (c == 0)
            //        {
            //            Services.BaseService.Delete<PowerPicSelect>(pps1);
            //        }
            //    }


            //    foreach (DataRow rw1 in dt.Rows)
            //    {
            //        c = 0;
            //        if ((bool)rw1["C"])
            //        {
            //            foreach (PowerPicSelect pps2 in liss)
            //            {
            //                if (pps2.PicSelectID == rw1["A"].ToString())
            //                    c = 1;
            //            }
            //            if (c == 0)
            //            {
            //                PowerPicSelect pp3 = new PowerPicSelect();
            //                pp3.EachListID = _obj.UID;
            //                pp3.PicSelectID = rw1["A"].ToString();

            //                Services.BaseService.Create<PowerPicSelect>(pp3);
            //            }
            //        }
            //    }
            //}
        }


        private void InitPicData()
        {
            //SVGFILE sf = Services.BaseService.GetOneByKey<SVGFILE>("26474eb6-cd92-4e84-a579-2f33946acf1a");

            string str="";

            try
            {
                str = System.Configuration.ConfigurationSettings.AppSettings["SvgID"];

            }
            catch { }
            dt.Columns.Add("A");
            dt.Columns.Add("B");
            dt.Columns.Add("C", typeof(bool));
            dt.Columns.Add("D");

            IList<LayerGrade> li = Services.BaseService.GetList<LayerGrade>("SelectLayerGradeListBySvgDataUid", str);
            IList<SVG_LAYER> li1 = Services.BaseService.GetList<SVG_LAYER>("SelectSVG_LAYERBySvgID", str);

            DataTable dt1 = Itop.Common.DataConverter.ToDataTable((IList)li1);

            foreach (LayerGrade node in li)
            {
                DataRow row = dt.NewRow();
                row["A"] = node.SUID;
                row["B"] = node.Name;
                row["C"] = false;
                row["D"] = node.ParentID;
                dt.Rows.Add(row);
                DataRow[] rows = dt1.Select("YearID='" + node.SUID + "'");
                foreach (DataRow row1 in rows)
                {
                    DataRow row2 = dt.NewRow();
                    row2["A"] = row1["SUID"].ToString();
                    row2["B"] = row1["NAME"].ToString();
                    row2["C"] = false;
                    row2["D"] = node.SUID;
                    dt.Rows.Add(row2);
                }
            }


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
	}
}
