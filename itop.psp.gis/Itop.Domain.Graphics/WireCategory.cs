//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2008-10-9 14:42:35
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
    /// <summary>
    /// ʵ����WireCategory ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public class WireCategory
    {
        public WireCategory()
        {
            SUID = Guid.NewGuid().ToString();
            _wirelead = 0;
        }
        #region �ֶ�
        private string _suid = "";
        private string _wiretype = "";
        private double _wirer;
        private double _wiretq;
        private double _wiregndc;
        private decimal _wirechange;
        private string _wirelevel="";
        private double _zeror;
        private double _zerotq;
        private double _zerogndc;
        private double _wirelead;
        private string _type;
        private double _gzl;
        private double _xftime;
        #endregion �ֶ�

        #region ����
        /// <summary>
        /// 
        /// 
        /// </summary>
        public string WireType
        {
            set { _wiretype = value; }
            get { return _wiretype; }
        }
        /// <summary>
        /// 
        /// 
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        public string SUID
        {
            set { _suid = value; }
            get { return _suid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double WireR
        {
            set { _wirer = value; }
            get { return _wirer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double WireTQ
        {
            set { _wiretq = value; }
            get { return _wiretq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double WireGNDC
        {
            set { _wiregndc = value; }
            get { return _wiregndc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal WireChange
        {
            set { _wirechange = value; }
            get { return _wirechange; }
        }
        public string WireLevel
        {
            set { _wirelevel = value; }
            get { return _wirelevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double ZeroR
        {
            set { _zeror = value; }
            get { return _zeror; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double ZeroTQ
        {
            set { _zerotq = value; }
            get { return _zerotq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double ZeroGNDC
        {
            set { _zerogndc = value; }
            get { return _zerogndc; }
        }
        public double WireLead
        {
            set { _wirelead = value; }
            get { return _wirelead; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double gzl
        {
            set { _gzl = value; }
            get { return _gzl; }
        }
        public double xftime
        {
            set { _xftime= value; }
            get { return _xftime; }
        }
        #endregion ����
    }
}

