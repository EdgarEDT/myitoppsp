//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-12-11 15:21:28
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
	/// ʵ����PowerEachTotal ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PowerEachTotal
	{
		public PowerEachTotal()
		{}
		#region �ֶ�
		private string _uid=Guid.NewGuid().ToString();
		private string _stuffname="";
		private string _lengths="";
		private string _lcount="";
		private string _total="";
		private string _volume="";
		private string _type="";
		private double _issum;
		private double _itsum;
		private string _remark="";
		private DateTime _createdate;
		private string _powerlineuid="";
        private string parentid = "";
		private int _sortid;
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// 
		/// </summary>
        /// 
        public string ParentID
        {
            set { parentid = value; }
            get { return parentid; }
        }

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
		/// ��·�ܳ���
		/// </summary>
		public string Lengths
		{
			set{ _lengths=value;}
			get{return _lengths;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string LCount
		{
			set{ _lcount=value;}
			get{return _lcount;}
		}
		/// <summary>
		/// ����̨��
		/// </summary>
		public string Total
		{
			set{ _total=value;}
			get{return _total;}
		}
		/// <summary>
		/// ����������
		/// </summary>
		public string Volume
		{
			set{ _volume=value;}
			get{return _volume;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// ʮһ�徲̬Ͷ�ʺϼ�
		/// </summary>
		public double IsSum
		{
			set{ _issum=value;}
			get{return _issum;}
		}
		/// <summary>
		/// ʮһ�嶯̬Ͷ�ʺϼ�
		/// </summary>
		public double ItSum
		{
			set{ _itsum=value;}
			get{return _itsum;}
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
		/// 
		/// </summary>
		public int SortID
		{
			set{ _sortid=value;}
			get{return _sortid;}
		}
		#endregion ����
	}
}

