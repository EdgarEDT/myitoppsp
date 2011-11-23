//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-12-8 14:40:25
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
	/// ʵ����PowerStuff ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PowerStuff
	{
		public PowerStuff()
		{}
		#region �ֶ�
		private string _uid=Guid.NewGuid().ToString();
		private string _stuffname="";
		private string _volume="";
		private string _total="";
		private string _type="";
		private string _lengths="";
		private string _remark="";
		private DateTime _createdate;
		private string _powerlineuid="";
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
		/// ����
		/// </summary>
		public string StuffName
		{
			set{ _stuffname=value;}
			get{return _stuffname;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string Volume
		{
			set{ _volume=value;}
			get{return _volume;}
		}
		/// <summary>
		/// ����豸̨��
		/// </summary>
		public string Total
		{
			set{ _total=value;}
			get{return _total;}
		}
		/// <summary>
		/// �����ͺ�
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
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
		/// ��ע
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// ����ʱ��
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
		#endregion ����
	}
}

