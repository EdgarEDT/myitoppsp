//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-12-6 9:07:37
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
    /// ʵ����BurdenByte ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
    public class BurdenByte
	{
		public BurdenByte()
		{}
		#region �ֶ�
        private int _burdenyear;
		private DateTime _burdendate;
        private string _season;
        private string _areaid;
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// ��
		/// </summary>
        public int BurdenYear
		{
            set { _burdenyear = value; }
            get { return _burdenyear; }
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Season
		{
			set{ _season=value;}
			get{return _season;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public DateTime BurdenDate
		{
			set{ _burdendate=value;}
			get{return _burdendate;}
		}
        /// <summary>
        /// ����id
        /// </summary>
        public string AreaID
        {
            set { _areaid = value; }
            get { return _areaid; }
        }
       
		#endregion ����
	}
}

