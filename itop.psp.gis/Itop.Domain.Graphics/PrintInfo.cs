//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2007-8-6 16:38:54
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// ʵ����PrintInfo ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PrintInfo
	{
		public PrintInfo()
		{}
		#region �ֶ�
		private string _uid="";
        private string _svguid = "";
		private string _col1="";
		private string _col2="";
		private string _col3="";
		private string _col4="";
		private string _col5="";
		private string _col6="";
		private string _col7="";
		private string _col8="";
		private string _col9="";
		private string _col10="";
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
        public string SvgUID
        {
            set { _svguid = value; }
            get { return _svguid; }
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
		/// <summary>
		/// 
		/// </summary>
		public string Col8
		{
			set{ _col8=value;}
			get{return _col8;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Col9
		{
			set{ _col9=value;}
			get{return _col9;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Col10
		{
			set{ _col10=value;}
			get{return _col10;}
		}
		#endregion ����
	}
}

