//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2009-3-23 15:23:53
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
	/// ʵ����PSP_BigUser_Years ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PSP_BigUser_Years
	{
		public PSP_BigUser_Years()
		{}
		#region �ֶ�
		private int _id;
		private string _year="";
		private int _flag;
		private int _flag2;
		private int _itemid;
		private string _s1="";
		private string _s2="";
		private string _s3="";
		private string _s4="";
		private string _s5="";
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
		public string Year
		{
			set{ _year=value;}
			get{return _year;}
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
		public int Flag2
		{
			set{ _flag2=value;}
			get{return _flag2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ItemID
		{
			set{ _itemid=value;}
			get{return _itemid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string S1
		{
			set{ _s1=value;}
			get{return _s1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string S2
		{
			set{ _s2=value;}
			get{return _s2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string S3
		{
			set{ _s3=value;}
			get{return _s3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string S4
		{
			set{ _s4=value;}
			get{return _s4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string S5
		{
			set{ _s5=value;}
			get{return _s5;}
		}
		#endregion ����
	}
}

