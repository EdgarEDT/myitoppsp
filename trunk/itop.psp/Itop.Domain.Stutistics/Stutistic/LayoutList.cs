//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2007-8-11 13:17:54
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
	/// ʵ����LayoutList ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class LayoutList
	{
		public LayoutList()
		{}
		#region �ֶ�
		private string _uid=Guid.NewGuid().ToString();
		private string _listname="";
		private string _remark="";
		private DateTime _createdate;
		private string _parentid="";
		private string _types="";
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
		/// ��������
		/// </summary>
		public string ListName
		{
			set{ _listname=value;}
			get{return _listname;}
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
		public string ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Types
		{
			set{ _types=value;}
			get{return _types;}
		}
		#endregion ����
	}
}

