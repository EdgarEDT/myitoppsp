//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2008-6-27 15:13:55
//
//********************************************************************************/
using System;
namespace Itop.Domain.HistoryValue
{
    /// <summary>
    /// ʵ����PSP_GDPBaseYear ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public class PSP_GDPBaseYear
    {
        public PSP_GDPBaseYear()
        { }
        #region �ֶ�
        private int _id;
        private string _baseyear = "";
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
        public string BaseYear
        {
            set { _baseyear = value; }
            get { return _baseyear; }
        }
        #endregion ����
    }
}

