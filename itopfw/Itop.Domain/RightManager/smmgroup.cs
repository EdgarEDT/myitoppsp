//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-8-17 8:34:05
//
//********************************************************************************/
using System;
namespace Itop.Domain.RightManager
{
	/// <summary>
	/// ʵ����Smmgroup ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Smmgroup
	{
		public Smmgroup()
		{}
		#region Domain
		private string _groupno;
		private string _groupname;
		private string _remark;
		/// <summary>
		/// ��ID
		/// </summary>
		public string Groupno
		{
			set{ _groupno=value;}
			get{return _groupno;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string Groupname
		{
			set{ _groupname=value;}
			get{return _groupname;}
		}
		/// <summary>
		/// ��ע
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Domain
	}
}

