//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2008-9-15 11:57:20
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
    /// <summary>
    /// ʵ����PSP_EachList ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public class PSP_EachList
    {
        public PSP_EachList()
        { }
        #region �ֶ�
        private int _id;
        private string _listname = "";
        private string _remark = "";
        private DateTime _createdate;
        private string _parentid = "";
        private string _types = "";
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
        /// ��������
        /// </summary>
        public string ListName
        {
            set { _listname = value; }
            get { return _listname; }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// ���ڵ�
        /// </summary>
        public string ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Types
        {
            set { _types = value; }
            get { return _types; }
        }
        #endregion ����
    }
}

