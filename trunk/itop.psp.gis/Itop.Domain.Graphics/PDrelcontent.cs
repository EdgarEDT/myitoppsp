//********************************************************************************/
//
//�˴�����TONLI.NET�����������Զ�����.
//����ʱ��:2011-12-12 14:37:20
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// ʵ����PDrelcontent ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PDrelcontent
	{
		public PDrelcontent()
		{}
		#region �ֶ�
		private string _id="";
		private string _parentid="";
		private DateTime _tddatetime;
		private double _tdtime;
		private int _peopleregion;
        private string _TDtype= "";
		private double _avgfh;
		private string _s1="";
		private string _s2="";
		private string _s3="";
		private string _s4="";
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// 
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime TDdatetime
		{
			set{ _tddatetime=value;}
			get{return _tddatetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double TDtime
		{
			set{ _tdtime=value;}
			get{return _tdtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int PeopleRegion
		{
			set{ _peopleregion=value;}
			get{return _peopleregion;}
		}
		/// <summary>
		/// 
		/// </summary>
        public string TDtype
		{
            set { _TDtype = value; }
            get { return _TDtype; }
		}
		/// <summary>
		/// 
		/// </summary>
		public double AvgFH
		{
			set{ _avgfh=value;}
			get{return _avgfh;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string S1
		{
			set{ _s1=value;}
			get{return _s1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string S2
		{
			set{ _s2=value;}
			get{return _s2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string S3
		{
			set{ _s3=value;}
			get{return _s3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string S4
		{
			set{ _s4=value;}
			get{return _s4;}
		}
		#endregion ����
	}
}

