//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2009-09-11 11:34:18
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// ʵ����PSP_SubstationUserNum ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PSP_SubstationUserNum
	{
		public PSP_SubstationUserNum()
		{}
		#region �ֶ�
		private string _uid="";
		private string _userid="";
		private string _substationid="";
		private string _subparid="";
		private int _num;
		private string _remark="";
		private string _col1="";
		private string _col2="";
		private string _col3="";
		private string _col4="";
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
		public string userID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SubStationID
		{
			set{ _substationid=value;}
			get{return _substationid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SubParID
		{
			set{ _subparid=value;}
			get{return _subparid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int num
		{
			set{ _num=value;}
			get{return _num;}
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
		#endregion ����
	}
}

