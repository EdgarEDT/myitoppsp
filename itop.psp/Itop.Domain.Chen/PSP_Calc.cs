//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2008-7-2 14:54:26
//
//********************************************************************************/
using System;
namespace Itop.Domain.Chen
{
	/// <summary>
	/// ʵ����PSP_Calc ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PSP_Calc
	{
		public PSP_Calc()
		{}
		#region �ֶ�
		private string _id="";
		private string _calcname="";
		private string _flag="";
		private double _value1;
		private double _value2;
		private double _value3;
		private double _value4;
		private double _value5;
		private string _col1="";
		private string _col2="";
		private double _value6;
		private double _value7;
		private double _value8;
		private double _value9;
		private double _value10;
		private double _value11;
		private double _value12;
		private double _value13;
		private double _value14;
		private double _value15;
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// 
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// �㷨����
		/// </summary>
		public string CalcName
		{
			set{ _calcname=value;}
			get{return _calcname;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		/// <summary>
		/// ��1����-Ĭ������
		/// </summary>
		public double Value1
		{
			set{ _value1=value;}
			get{return _value1;}
		}
		/// <summary>
		/// ��2����-ָ��ƽ������
		/// </summary>
		public double Value2
		{
			set{ _value2=value;}
			get{return _value2;}
		}
		/// <summary>
		/// ��3����
		/// </summary>
		public double Value3
		{
			set{ _value3=value;}
			get{return _value3;}
		}
		/// <summary>
		/// ��4����
		/// </summary>
		public double Value4
		{
			set{ _value4=value;}
			get{return _value4;}
		}
		/// <summary>
		/// ��5����
		/// </summary>
		public double Value5
		{
			set{ _value5=value;}
			get{return _value5;}
		}
		/// <summary>
		/// ����1
		/// </summary>
		public string Col1
		{
			set{ _col1=value;}
			get{return _col1;}
		}
		/// <summary>
		/// ����2
		/// </summary>
		public string Col2
		{
			set{ _col2=value;}
			get{return _col2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Value6
		{
			set{ _value6=value;}
			get{return _value6;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Value7
		{
			set{ _value7=value;}
			get{return _value7;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Value8
		{
			set{ _value8=value;}
			get{return _value8;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Value9
		{
			set{ _value9=value;}
			get{return _value9;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Value10
		{
			set{ _value10=value;}
			get{return _value10;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Value11
		{
			set{ _value11=value;}
			get{return _value11;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Value12
		{
			set{ _value12=value;}
			get{return _value12;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Value13
		{
			set{ _value13=value;}
			get{return _value13;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Value14
		{
			set{ _value14=value;}
			get{return _value14;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Value15
		{
			set{ _value15=value;}
			get{return _value15;}
		}
		#endregion ����
	}
}

