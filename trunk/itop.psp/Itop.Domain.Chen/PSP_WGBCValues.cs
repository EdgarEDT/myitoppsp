//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-12-11 9:42:35
//
//********************************************************************************/
using System;
namespace Itop.Domain.HistoryValue
{
	/// <summary>
	/// ʵ����PSP_WGBCValues ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PSP_WGBCValues
	{
		public PSP_WGBCValues()
		{}
		#region �ֶ�
		private int _id;
		private int _reportid;
		private int _typeid;
		private int _parenttypeid;
		private string _col1="";
		private string _col2="";
		private string _col3="";
		private string _col4="";
		private string _col5="";
		private string _col6="";
		private string _col7="";
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ReportID
		{
			set{ _reportid=value;}
			get{return _reportid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int TypeID
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ParentTypeID
		{
			set{ _parenttypeid=value;}
			get{return _parenttypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Col1
		{
			set{ _col1=value;}
			get{return _col1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Col2
		{
			set{ _col2=value;}
			get{return _col2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Col3
		{
			set{ _col3=value;}
			get{return _col3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Col4
		{
			set{ _col4=value;}
			get{return _col4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Col5
		{
			set{ _col5=value;}
			get{return _col5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Col6
		{
			set{ _col6=value;}
			get{return _col6;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Col7
		{
			set{ _col7=value;}
			get{return _col7;}
		}
		#endregion ����
	}
}

