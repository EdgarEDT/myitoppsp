//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2010-03-02 9:14:43
//
//********************************************************************************/
using System;
namespace Itop.Domain.PWTable
{
	/// <summary>
	/// ʵ����PW_tb3c ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PW_tb3c
	{
		public PW_tb3c()
		{}
		#region �ֶ�
		private string _uid="";
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

