//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-10-19 14:54:59
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// ʵ����Itop.Planning.SVGFILE ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class SVGFILE
	{
		public SVGFILE()
		{}
		#region �ֶ�
		private string _suid="";
		private string _filename="";
		private string _parentid="";
		private string _svgdata="";
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
		public string FILENAME
		{
			set{ _filename=value;}
			get{return _filename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PARENTID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SVGDATA
		{
			set{ _svgdata=value;}
			get{return _svgdata;}
		}
		#endregion ����
	}
}

