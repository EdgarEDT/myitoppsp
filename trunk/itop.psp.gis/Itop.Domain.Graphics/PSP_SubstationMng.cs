//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2009-11-25 14:11:21
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// ʵ����PSP_SubstationMng ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PSP_SubstationMng
	{
		public PSP_SubstationMng()
		{}
		#region �ֶ�
		private string _uid="";
		private string _sname="";
		private string _type="";
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
		public string SName
		{
			set{ _sname=value;}
			get{return _sname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string type
		{
			set{ _type=value;}
			get{return _type;}
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

