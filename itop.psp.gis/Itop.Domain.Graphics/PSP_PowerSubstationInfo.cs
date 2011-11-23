//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2008-7-10 15:12:37
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
    /// <summary>
    /// ʵ����PSP_PowerSubstationInfo ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public class PSP_PowerSubstation_Info
    {
        public PSP_PowerSubstation_Info() { }
        #region �ֶ�
        private string _uid = Guid.NewGuid().ToString().Substring(24);
        private string _areaid = "";
        private string _areaname = "";
        private string _powerid = "";
        private string _powername = "";
        private string _title = "";
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
        private string _s11 = "";
        private string _s12 = "";
        private string _s13 = "";
        private string _s14 = "";
        private string _s15 = "";
        private string _s16 = "";
        private string _s17 = "";
        private string _s18 = "";
        private string _s19 = "";
        private string _s20 = "";
        private string _s21 = "";
        private string _s22 = "";
        private string _s23 = "";
        private string _s24 = "";
        private string _s25 = "";
        private string _s26 = "";
        private string _s27 = "";
        private string _s28 = "";
        private string _s29 = "";
        private string _s30 = "";
        private string _flag = "1";
        private string _code = "";
        private string _isconn = "";
        private string _layerid = "";
        private DateTime _createdate=DateTime.Now;
        #endregion �ֶ�

        #region ����
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
        public string PowerID {
            set { _powerid = value; }
            get { return _powerid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PowerName {
            set { _powername = value; }
            get { return _powername; }
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
        public string S1 {
            set { _s1 = value; }
            get { return _s1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S2 {
            set { _s2 = value; }
            get { return _s2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S3 {
            set { _s3 = value; }
            get { return _s3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S4 {
            set { _s4 = value; }
            get { return _s4; }
        }
        /// <summary>
        /// 
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
        public string S11 {
            set { _s11 = value; }
            get { return _s11; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S12 {
            set { _s12 = value; }
            get { return _s12; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S13 {
            set { _s13 = value; }
            get { return _s13; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S14 {
            set { _s14 = value; }
            get { return _s14; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S15 {
            set { _s15 = value; }
            get { return _s15; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S16 {
            set { _s16 = value; }
            get { return _s16; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S17 {
            set { _s17 = value; }
            get { return _s17; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S18 {
            set { _s18 = value; }
            get { return _s18; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S19 {
            set { _s19 = value; }
            get { return _s19; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S20 {
            set { _s20 = value; }
            get { return _s20; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string S21 {
            set { _s21 = value; }
            get { return _s21; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S22 {
            set { _s22 = value; }
            get { return _s22; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S23 {
            set { _s23 = value; }
            get { return _s23; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S24 {
            set { _s24 = value; }
            get { return _s24; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S25 {
            set { _s25 = value; }
            get { return _s25; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S26 {
            set { _s26 = value; }
            get { return _s26; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S27 {
            set { _s27 = value; }
            get { return _s27; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S28 {
            set { _s28 = value; }
            get { return _s28; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S29 {
            set { _s29 = value; }
            get { return _s29; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S30 {
            set { _s30 = value; }
            get { return _s30; }
        }
        /// <summary>
        /// �Ƿ�Ϊ��״ 1Ϊ��״��2Ϊ�滮
        /// </summary>
        public string Flag {
            set { _flag = value; }
            get { return _flag; }
        }
        /// <summary>
        /// �����滮�豸ID
        /// </summary>
        public string Code {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// �Ƿ����
        /// </summary>
        public string IsConn {
            set { _isconn = value; }
            get { return _isconn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate {
            set { _createdate = value; }
            get { return _createdate; }
        }

        public string LayerID
        {
            set { _layerid = value; }
            get { return _layerid; }
        }
        #endregion ����
    }
}

