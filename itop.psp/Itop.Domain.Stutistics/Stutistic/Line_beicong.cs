//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2008-6-23 9:09:49
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
    /// <summary>
    /// ʵ����Line_beicong ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public class Line_beicong
    {
        public Line_beicong()
        { }
        #region �ֶ�
        private string _uid = Guid.NewGuid().ToString();
        private string _title = "";
        private string _type = "";
        private string _type2 = "";
        private DateTime _createtime = DateTime.Now;
        private string _classtype = "";
        private string _flag = "";
        #endregion �ֶ�

        #region ����
        /// <summary>
        /// 
        /// </summary>
        public string UID
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
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
        public string Type2
        {
            set { _type2 = value; }
            get { return _type2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClassType
        {
            set { _classtype = value; }
            get { return _classtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Flag
        {
            set { _flag = value; }
            get { return _flag; }
        }
        #endregion ����
    }
}

