//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2009-12-21 15:58:11
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// ʵ����PSP_GProg ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PSP_GProg
	{
		public PSP_GProg()
		{}
		#region �ֶ�
		private string _uid="";
		private string _progname="";
		private string _notes="";
		private string _col1="";
		private string _col2="";
		private string _col3="";
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
		public string ProgName
		{
			set{ _progname=value;}
			get{return _progname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Notes
		{
			set{ _notes=value;}
			get{return _notes;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string col1
		{
			set{ _col1=value;}
			get{return _col1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string col2
		{
			set{ _col2=value;}
			get{return _col2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string col3
		{
			set{ _col3=value;}
			get{return _col3;}
		}
		#endregion ����
	}
}

