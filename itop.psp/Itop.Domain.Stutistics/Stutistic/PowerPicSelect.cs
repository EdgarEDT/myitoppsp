//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2007-5-15 15:55:06
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistics
{
	/// <summary>
	/// ʵ����PowerPicSelect ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PowerPicSelect
	{
		public PowerPicSelect()
		{}
		#region �ֶ�
		private string _uid=Guid.NewGuid().ToString();
		private string _eachlistid="";
		private string _picselectid="";
		private string _picselectname="";
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
		public string EachListID
		{
			set{ _eachlistid=value;}
			get{return _eachlistid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PicSelectID
		{
			set{ _picselectid=value;}
			get{return _picselectid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PicSelectName
		{
			set{ _picselectname=value;}
			get{return _picselectname;}
		}
		#endregion ����
	}
}

