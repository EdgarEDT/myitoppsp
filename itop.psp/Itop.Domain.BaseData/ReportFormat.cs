//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2007-5-17 14:51:51
//
//********************************************************************************/
using System;
namespace Itop.Domain.BaseDatas
{
	/// <summary>
	/// ʵ����ReportFormat ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class ReportFormat
	{
		public ReportFormat()
		{}
		#region �ֶ�
		private string _uid=Guid.NewGuid().ToString();
		private string _title="";
		private byte[] _bytereport;
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
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public byte[] ByteReport
		{
			set{ _bytereport=value;}
			get{return _bytereport;}
		}
		#endregion ����
	}
}

