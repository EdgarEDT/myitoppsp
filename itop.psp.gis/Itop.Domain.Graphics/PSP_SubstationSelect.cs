//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2009-09-04 11:40:09
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// ʵ����PSP_SubstationSelect ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PSP_SubstationSelect
	{
		public PSP_SubstationSelect()
		{}
		#region �ֶ�
		private string _uid="";
		private string _sname="";
		private string _eleid="";
		private string _svgid="";
		private string _remark="";
		private string _col1="";
		private string _col2="";
		private string _col3="";
		private string _col4="";
		private string _col5="";
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
		public string EleID
		{
			set{ _eleid=value;}
			get{return _eleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SvgID
		{
			set{ _svgid=value;}
			get{return _svgid;}
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
		/// <summary>
		/// 
		/// </summary>
		public string col4
		{
			set{ _col4=value;}
			get{return _col4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string col5
		{
			set{ _col5=value;}
			get{return _col5;}
		}
		#endregion ����
	}
}

