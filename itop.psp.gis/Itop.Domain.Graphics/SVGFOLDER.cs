//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-10-19 15:19:53
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// ʵ����SVGFOLDER ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class SVGFOLDER
	{
		public SVGFOLDER()
		{}
		#region �ֶ�
		private string _suid="";
		private string _foldername="";
		private string _parentid="";
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// 
		/// </summary>
		public string SUID
		{
			set{ _suid=value;}
			get{return _suid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FOLDERNAME
		{
			set{ _foldername=value;}
			get{return _foldername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PARENTID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		#endregion ����
	}
}

