//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-12-6 17:25:56
//
//********************************************************************************/
using System;
namespace Itop.Domain.HistoryValue
{
	/// <summary>
	/// ʵ����PSP_CapBalance ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PSP_CapBalance
	{
		public PSP_CapBalance()
		{}
		#region �ֶ�
		private int _id;
		private int _flag;
		private int _typeid;
		private int _year;
		private double _col1;
		private double _col2;
		private double _col3;
		private double _col4;
		private double _col5;
		private double _col6;
		private double _col7;
		private double _col8;
		private double _col9;
		private double _col10;
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
		public int Flag
		{
			set{ _flag=value;}
			get{return _flag;}
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
		public int Year
		{
			set{ _year=value;}
			get{return _year;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Col1
		{
			set{ _col1=value;}
			get{return _col1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Col2
		{
			set{ _col2=value;}
			get{return _col2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Col3
		{
			set{ _col3=value;}
			get{return _col3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Col4
		{
			set{ _col4=value;}
			get{return _col4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Col5
		{
			set{ _col5=value;}
			get{return _col5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Col6
		{
			set{ _col6=value;}
			get{return _col6;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Col7
		{
			set{ _col7=value;}
			get{return _col7;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Col8
		{
			set{ _col8=value;}
			get{return _col8;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Col9
		{
			set{ _col9=value;}
			get{return _col9;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Col10
		{
			set{ _col10=value;}
			get{return _col10;}
		}
		#endregion ����
	}
}

