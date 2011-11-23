//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2008-11-10 9:01:43
//
//********************************************************************************/
using System;
namespace Itop.Domain.Chen
{
	/// <summary>
	/// ʵ����PSP_VolumeBalance_Calc ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PSP_VolumeBalance_Calc
	{
		public PSP_VolumeBalance_Calc()
		{}
		#region �ֶ�
		private string _uid=Guid.NewGuid().ToString();
		private string _title="";
		private string _lx1="";
		private string _lx2="";
		private double _vol=0.0;
		private string _type="";
		private string _flag="";
		private DateTime _createtime;
		private int _sort;
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
		/// ��Ŀ����
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string LX1
		{
			set{ _lx1=value;}
			get{return _lx1;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string LX2
		{
			set{ _lx2=value;}
			get{return _lx2;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public double Vol
		{
			set{ _vol=value;}
			get{return _vol;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// ���
		/// </summary>
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
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
		#endregion ����
	}
}

