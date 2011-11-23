//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2008-6-28 11:12:34
//
//********************************************************************************/
using System;
namespace Itop.Domain.HistoryValue
{
    /// <summary>
    /// ʵ����PSP_YearVisibleIndex ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public class PSP_YearVisibleIndex
    {
        public PSP_YearVisibleIndex()
        { }
        #region �ֶ�
        private int _id;
        private int _visibleindex;
        private string _year;
        private string _moduleflag = "";
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
        public int VisibleIndex
        {
            set { _visibleindex = value; }
            get { return _visibleindex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Year
        {
            set { _year = value; }
            get { return _year; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModuleFlag
        {
            set { _moduleflag = value; }
            get { return _moduleflag; }
        }
        #endregion ����
    }
}

