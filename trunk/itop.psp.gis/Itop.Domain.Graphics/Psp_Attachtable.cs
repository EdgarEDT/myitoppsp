//********************************************************************************/
//
//�˴�����TONLI.NET�����������Զ�����.
//����ʱ��:2012-6-5 11:40:34
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
    /// <summary>
    /// ʵ����Psp_Attachtable ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public class Psp_Attachtable
    {
        public Psp_Attachtable()
        { ID = Guid.NewGuid().ToString(); }
        #region �ֶ�
        private string _id = "";
        private double _zhi;
        private string _startyear = "";
        private string _endyear = "";
        private string _relatetable = "";
        private string _type = "";
        private string _s1 = "";
        private string _s2 = "";
        private string _s3 = "";
        private double _d1;
        private double _d2;
        private double _d3;
        private string _relatetableid = "";
        private string _s4 = "";
        private string _s5 = "";
        private string _s6 = "";
        private double _d4;
        private double _d5;
        private double _d6;
        #endregion �ֶ�

        #region ����
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double ZHI
        {
            set { _zhi = value; }
            get { return _zhi; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string startYear
        {
            set { _startyear = value; }
            get { return _startyear; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string endYear
        {
            set { _endyear = value; }
            get { return _endyear; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Relatetable
        {
            set { _relatetable = value; }
            get { return _relatetable; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S1
        {
            set { _s1 = value; }
            get { return _s1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S2
        {
            set { _s2 = value; }
            get { return _s2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S3
        {
            set { _s3 = value; }
            get { return _s3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double D1
        {
            set { _d1 = value; }
            get { return _d1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double D2
        {
            set { _d2 = value; }
            get { return _d2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double D3
        {
            set { _d3 = value; }
            get { return _d3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RelatetableID
        {
            set { _relatetableid = value; }
            get { return _relatetableid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S4
        {
            set { _s4 = value; }
            get { return _s4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S5
        {
            set { _s5 = value; }
            get { return _s5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S6
        {
            set { _s6 = value; }
            get { return _s6; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double D4
        {
            set { _d4 = value; }
            get { return _d4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double D5
        {
            set { _d5 = value; }
            get { return _d5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double D6
        {
            set { _d6 = value; }
            get { return _d6; }
        }
        #endregion ����
    }
}

