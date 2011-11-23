//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-8-21 15:02:30
//
//********************************************************************************/
using System;
namespace Itop.Domain
{
	/// <summary>
	/// ʵ����SAppProps ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class SAppProps
	{
		public SAppProps()
		{}
		#region Domain
		private int _propid;
		private string _propname;
		private string _propvalue;
		private string _proptype;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public int PropId
		{
			set{ _propid=value;}
			get{return _propid;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string PropName
		{
			set{ _propname=value;}
			get{return _propname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PropValue
		{
			set{ _propvalue=value;}
			get{return _propvalue;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string PropType
		{
			set{ _proptype=value;}
			get{return _proptype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Domain
	}
}

