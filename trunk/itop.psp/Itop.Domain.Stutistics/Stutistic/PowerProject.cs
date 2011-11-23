//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-12-8 16:25:05
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
	/// ʵ����PowerProject ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PowerProject
	{
		public PowerProject()
		{}
		#region �ֶ�
		private string _uid=Guid.NewGuid().ToString();
		private string _stuffname="";
		private string _total="";
		private string _volume="";
		private string _lengths="";
		private string _type="";
		private string _remark="";
		private DateTime _createdate;
		private string _powerlineuid="";
		private string _planstartyear="";
		private string _planendyear="";
        private string _parentid = "";
        private int _sortid = 0;
		#endregion �ֶ�

		#region ����



        /// <summary>
        /// 
        /// </summary>
        /// 
        public int SortID
        {
            set { _sortid = value; }
            get { return _sortid; }
        }

        public string ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string UID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// ��·����
		/// </summary>
		public string StuffName
		{
			set{ _stuffname=value;}
			get{return _stuffname;}
		}
		/// <summary>
		/// ̨��
		/// </summary>
		public string Total
		{
			set{ _total=value;}
			get{return _total;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Volume
		{
			set{ _volume=value;}
			get{return _volume;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Lengths
		{
			set{ _lengths=value;}
			get{return _lengths;}
		}
		/// <summary>
		/// �ͺ�
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PowerLineUID
		{
			set{ _powerlineuid=value;}
			get{return _powerlineuid;}
		}
		/// <summary>
		/// �ƻ�����ʱ��
		/// </summary>
		public string PlanStartYear
		{
			set{ _planstartyear=value;}
			get{return _planstartyear;}
		}
		/// <summary>
		/// Ԥ��Ͷ��ʱ��
		/// </summary>
		public string PlanEndYear
		{
			set{ _planendyear=value;}
			get{return _planendyear;}
		}
		#endregion ����
	}
}

