//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2011-1-12 13:17:10
//
//********************************************************************************/
using System;
namespace Itop.Domain.Forecast
{
	/// <summary>
	/// ʵ���� ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Ps_HistoryType
	{
		public Ps_HistoryType ()
		{}
		#region �ֶ�
		private string _id="";
		private string _typename="";
		private int _sort;
		private string _parentid="";
		private string _units="";
		private string _flag="";
		private string _remark="";
		private double _num1;
		private double _num2;
		private string _col1="";
		private string _col2="";
		private string _col3="";
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// ����ʶ
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// �������
		/// </summary>
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// �����ʶ
		/// </summary>
		public string ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// ���λ�������λ��#�ָ���
		/// </summary>
		public string Units
		{
			set{ _units=value;}
			get{return _units;}
		}
		/// <summary>
		/// ����־ 
		/// </summary>
		public string Flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		/// <summary>
		/// ���ע
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public double Num1
		{
			set{ _num1=value;}
			get{return _num1;}
		}
		/// <summary>
        /// ����
		/// </summary>
		public double Num2
		{
			set{ _num2=value;}
			get{return _num2;}
		}
		/// <summary>
        /// ����
		/// </summary>
		public string Col1
		{
			set{ _col1=value;}
			get{return _col1;}
		}
		/// <summary>
        /// ����
		/// </summary>
		public string Col2
		{
			set{ _col2=value;}
			get{return _col2;}
		}
		/// <summary>
        /// ����
		/// </summary>
		public string Col3
		{
			set{ _col3=value;}
			get{return _col3;}
		}
		#endregion ����
	}
}

