//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2008-11-6 11:11:45
//
//********************************************************************************/
using System;
namespace Itop.Domain.Chen
{
	/// <summary>
	/// ʵ����PSP_VolumeBalanceDataSource ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PSP_VolumeBalanceDataSource
	{
		public PSP_VolumeBalanceDataSource()
		{}
		#region �ֶ�
		private string _uid="";
		private string _name="";
		private double _value;
		private int _typeid;
        private string _flag;
		private int _s1;
		private string _s2="";
		private string _s3="";
		private string _s4="";
		private string _s5="";
		private string _s6="";
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// UID
		/// </summary>
		public string UID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Value
		{
			set{ _value=value;}
			get{return _value;}
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
        public string Flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public int S1
		{
			set{ _s1=value;}
			get{return _s1;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string S2
		{
			set{ _s2=value;}
			get{return _s2;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string S3
		{
			set{ _s3=value;}
			get{return _s3;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string S4
		{
			set{ _s4=value;}
			get{return _s4;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string S5
		{
			set{ _s5=value;}
			get{return _s5;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string S6
		{
			set{ _s6=value;}
			get{return _s6;}
		}
		#endregion ����
	}
}

