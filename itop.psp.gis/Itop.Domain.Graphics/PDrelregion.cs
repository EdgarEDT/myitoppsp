//********************************************************************************/
//
//�˴�����TONLI.NET�����������Զ�����.
//����ʱ��:2011-12-12 14:26:47
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// ʵ����PDrelregion ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PDrelregion
	{
		public PDrelregion()
		{}
		#region �ֶ�
		private string _id=Guid.NewGuid().ToString();
		private string _areaname="";
        private string _ProjectID = "";
		private int _peoplesum;
		private int _year;
		private string _title="";
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
		public string AreaName
		{
			set{ _areaname=value;}
			get{return _areaname;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string ProjectID {
            set { _ProjectID = value; }
            get { return _ProjectID; }
        }
		/// <summary>
		/// 
		/// </summary>
		public int PeopleSum
		{
			set{ _peoplesum=value;}
			get{return _peoplesum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Year
		{
			set{ _year=value;}
			get{return _year;}
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

