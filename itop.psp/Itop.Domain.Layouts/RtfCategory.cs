//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-11-17 13:52:32
//
//********************************************************************************/
using System;
namespace Itop.Domain.Layouts
{
	/// <summary>
	/// ʵ����RtfCategory ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class RtfCategory
	{
		public RtfCategory()
		{}
		#region �ֶ�
		private string _uid=Guid.NewGuid().ToString();
		private string _title="";
		private string _parentid="";
		private decimal _sortno;
		private byte[] _rtfcontents=new byte [0];
		private int _ifparent;
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// Ŀ¼��ʶ
		/// </summary>
		public string UID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// Ŀ¼����
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// �ϼ�Ŀ¼��ʶ
		/// </summary>
		public string ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// Ŀ¼˳���
		/// </summary>
		public decimal SortNo
		{
			set{ _sortno=value;}
			get{return _sortno;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public byte[] RtfContents
		{
			set{ _rtfcontents=value;}
			get{return _rtfcontents;}
		}
		/// <summary>
		/// �Ƿ����¼�Ŀ¼
		/// </summary>
		public int IfParent
		{
			set{ _ifparent=value;}
			get{return _ifparent;}
		}
		#endregion ����
	}
}

