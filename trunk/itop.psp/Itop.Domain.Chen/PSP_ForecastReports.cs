//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-12-4 12:57:05
//
//********************************************************************************/
using System;
namespace Itop.Domain.HistoryValue
{
	/// <summary>
	/// ʵ����PSP_ForecastReports ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PSP_ForecastReports
	{
		public PSP_ForecastReports()
		{}
		#region �ֶ�
		private int _id;
		private string _title="";
		private int _startyear;
		private int _endyear;
		private int _historyyears;
		private int _flag;
        private string _projectid;
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int StartYear
		{
			set{ _startyear=value;}
			get{return _startyear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int EndYear
		{
			set{ _endyear=value;}
			get{return _endyear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int HistoryYears
		{
			set{ _historyyears=value;}
			get{return _historyyears;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
		#endregion ����
	}
}

