//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2008-6-17 9:26:22
//
//********************************************************************************/
using System;
namespace Itop.Domain.HistoryValue
{
    /// <summary>
    /// ʵ����PSP_GDPValues ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public class PSP_GDPValues
    {
        public PSP_GDPValues()
        { }
        #region �ֶ�
        private int _id;
        private int _typeid;
        private int _year;
        private double _value;
        #endregion �ֶ�

        #region ����
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TypeID
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Year
        {
            set { _year = value; }
            get { return _year; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Value
        {
            set { _value = value; }
            get { return _value; }
        }
        #endregion ����
    }
}

