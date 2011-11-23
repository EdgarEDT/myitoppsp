//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2010-3-10 8:32:23
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
    /// <summary>
    /// 实体类PSP_Substation_Info 变电站信息电气图形专用
    /// </summary>
    [Serializable]
    public class PSP_Substation_Info
    {
        public PSP_Substation_Info() { }
        #region 字段
        private string _uid = "bdz_"+Guid.NewGuid().ToString().Substring(24);
        private string _areaid = "";
        private string _areaname = "";
        private string _title = "";
        private int _l1;
        private double _l2;
        private int _l3;
        private string _l4 = "";
        private string _l5 = "";
        private string _l6 = "";
        private string _l7 = "";
        private string _l8 = "";
        private double _l9;
        private double _l10;
        private string _s1 = "";
        private string _s2 = "";
        private string _s3 = "";
        private string _s4 = "";
        private string _s5 = "";
        private string _s6 = "";
        private string _s7 = "";
        private string _s8 = "";
        private string _s9 = "";
        private string _s10 = "";
        private string _flag = "";
        private string _code = "";
        private string _isconn = "";
        private string _l11 = "";
        private string _l12 = "";
        private string _l13 = "";
        private string _l14 = "";
        private DateTime? _createdate = DateTime.Now;
        private string _l15 = "";
        private string _l16 = "";
        private string _l17 = "";
        private string _l18 = "";
        private string _l19 = "";
        private string _l20 = "";
        private string _l21 = "";
        private string _l22 = "";
        private string _l23 = "";
        private string _l24 = "";
        private string _l25 = "";
        private string _l26 = "";
        private string _l27 = "";
        private string _l28 = "";
        private string _l29 = "";
        private string _layerid = "";
        private string _eleid = "";
        private string _dq = "";
        #endregion 字段

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string UID {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AreaID {
            set { _areaid = value; }
            get { return _areaid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AreaName {
            set { _areaname = value; }
            get { return _areaname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int L1 {
            set { _l1 = value; }
            get { return _l1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double L2 {
            set { _l2 = value; }
            get { return _l2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int L3 {
            set { _l3 = value; }
            get { return _l3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L4 {
            set { _l4 = value; }
            get { return _l4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L5 {
            set { _l5 = value; }
            get { return _l5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L6 {
            set { _l6 = value; }
            get { return _l6; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L7 {
            set { _l7 = value; }
            get { return _l7; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L8 {
            set { _l8 = value; }
            get { return _l8; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double L9 {
            set { _l9 = value; }
            get { return _l9; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double L10 {
            set { _l10 = value; }
            get { return _l10; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S1 {
            set { _s1 = value; }
            get { return _s1; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string S2 {
            set { _s2 = value; }
            get { return _s2; }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public string S3 {
            set { _s3 = value; }
            get { return _s3; }
        }
        /// <summary>
        /// 专变/公变
        /// </summary>
        public string S4 {
            set { _s4 = value; }
            get { return _s4; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public string S5 {
            set { _s5 = value; }
            get { return _s5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S6 {
            set { _s6 = value; }
            get { return _s6; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S7 {
            set { _s7 = value; }
            get { return _s7; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S8 {
            set { _s8 = value; }
            get { return _s8; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S9 {
            set { _s9 = value; }
            get { return _s9; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S10 {
            set { _s10 = value; }
            get { return _s10; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Flag {
            set { _flag = value; }
            get { return _flag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Code {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsConn {
            set { _isconn = value; }
            get { return _isconn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L11 {
            set { _l11 = value; }
            get { return _l11; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L12 {
            set { _l12 = value; }
            get { return _l12; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L13 {
            set { _l13 = value; }
            get { return _l13; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L14 {
            set { _l14 = value; }
            get { return _l14; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateDate {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L15 {
            set { _l15 = value; }
            get { return _l15; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L16 {
            set { _l16 = value; }
            get { return _l16; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L17 {
            set { _l17 = value; }
            get { return _l17; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L18 {
            set { _l18 = value; }
            get { return _l18; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L19 {
            set { _l19 = value; }
            get { return _l19; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L20 {
            set { _l20 = value; }
            get { return _l20; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L21 {
            set { _l21 = value; }
            get { return _l21; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L22 {
            set { _l22 = value; }
            get { return _l22; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L23 {
            set { _l23 = value; }
            get { return _l23; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L24 {
            set { _l24 = value; }
            get { return _l24; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L25 {
            set { _l25 = value; }
            get { return _l25; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L26 {
            set { _l26 = value; }
            get { return _l26; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L27 {
            set { _l27 = value; }
            get { return _l27; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L28 {
            set { _l28 = value; }
            get { return _l28; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L29 {
            set { _l29 = value; }
            get { return _l29; }
        }

        public string LayerID
        {
            set { _layerid = value; }
            get { return _layerid; }
        }

        public string EleID
        {
            set { _eleid = value; }
            get { return _eleid; }
        }
        public string DQ
        {
            set { _dq = value; }
            get { return _dq; }
        }
        #endregion 属性
    }
}

