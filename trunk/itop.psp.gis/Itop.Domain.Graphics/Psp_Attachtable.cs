//********************************************************************************/
//
//�˴�����TONLI.NET�����������Զ�����.
//����ʱ��:2012-5-22 13:03:50
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
        { }
        #region �ֶ�
        private string _id = "";
        private double _zhi;
        private int _startyear;
        private int _endyear;
        private string _relatetable = "";
        private string _type = "";
        private string _s1 = "";
        private string _s2 = "";
        private string _s3 = "";
        private double _d1;
        private double _d2;
        private double _d3;
        private string _relatetableid = "";
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
        public int startYear
        {
            set { _startyear = value; }
            get { return _startyear; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int endYear
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
        #endregion ����
    }
}

