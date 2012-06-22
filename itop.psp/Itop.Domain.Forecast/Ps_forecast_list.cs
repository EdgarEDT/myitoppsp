//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2009-7-27 9:43:57
//
//********************************************************************************/
using System;
namespace Itop.Domain.Forecast
{
	/// <summary>
	/// ʵ����Ps_forecast_list ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Ps_forecast_list
	{
		public Ps_forecast_list()
		{}
		#region �ֶ�
		private string _id="";
		private string _title="";
		private int _startyear;
		private int _endyear;
		private string _userid="";
		private string _col1="";
		private string _col2="";
        private int _ycstartyear;
        private int _ycendyear;

		#endregion �ֶ�

		#region ����
		/// <summary>
		/// GUID
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// Ԥ������
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// ��ʼ���
		/// </summary>
		public int StartYear
		{
			set{ _startyear=value;}
			get{return _startyear;}
		}
		/// <summary>
		/// �������
		/// </summary>
		public int EndYear
		{
			set{ _endyear=value;}
			get{return _endyear;}
		}
		/// <summary>
		/// �û�ID
		/// </summary>
		public string UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// ����1
		/// </summary>
		public string Col1
		{
			set{ _col1=value;}
			get{return _col1;}
		}
		/// <summary>
		/// ����2
		/// </summary>
		public string Col2
		{
			set{ _col2=value;}
			get{return _col2;}
		}
        /// <summary>
        /// Ԥ����ʼ���
        /// </summary>
        public int YcStartYear
        {
            set { _ycstartyear = value; }
            get { return _ycstartyear; }
        }
        /// <summary>
        /// Ԥ��������
        /// </summary>
        public int YcEndYear
        {
            set { _ycendyear = value; }
            get { return _ycendyear; }
        }

		#endregion ����
	}
}

