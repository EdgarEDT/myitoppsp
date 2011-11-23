//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2008-9-16 9:33:24
//
//********************************************************************************/
using System;
namespace Itop.Domain.HistoryValue
{
	/// <summary>
	/// ʵ����PSP_BaseYearRate ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PSP_BaseYearRate
	{
		public PSP_BaseYearRate()
		{}
		#region �ֶ�
		private string _uid="";
		private string _baseyear="";
		private string _yearrate="";
        private int _typeid = 0;
        private string _s1 = "";
        private string _s2 = "";
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// 
		/// </summary>
		public string UID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BaseYear
		{
			set{ _baseyear=value;}
			get{return _baseyear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string YearRate
		{
			set{ _yearrate=value;}
			get{return _yearrate;}
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
		#endregion ����
	}
}

