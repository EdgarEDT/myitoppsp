//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2010-09-21 14:12:47
//
//********************************************************************************/
using System;
namespace Itop.Domain.Table
{
	/// <summary>
	/// ʵ����Ps_Table_Enterprise ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Ps_Table_Enterprise
	{
		public Ps_Table_Enterprise()
		{}
		#region �ֶ�
		private string _uid="";
		private string _sname="";
		private string _stype="";
		private string _dq="";
		private string _col1="";
        private string _projectid = "";
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
		public string SName
		{
			set{ _sname=value;}
			get{return _sname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SType
		{
			set{ _stype=value;}
			get{return _stype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DQ
		{
			set{ _dq=value;}
			get{return _dq;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string col1
		{
			set{ _col1=value;}
			get{return _col1;}
		}
        public string ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
		#endregion ����
	}
}

