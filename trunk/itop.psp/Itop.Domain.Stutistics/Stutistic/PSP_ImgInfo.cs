//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2008-9-8 10:17:28
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistics
{
	/// <summary>
	/// ʵ����PSP_ImgInfo ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PSP_ImgInfo
	{
		public PSP_ImgInfo()
		{}
		#region �ֶ�
		private string _uid="";
		private string _name="";
		private string _remark="";
		private byte[] _image;
		private int _orderid;
		private string _treeid="";
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
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
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
		public byte[] Image
		{
			set{ _image=value;}
			get{return _image;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int orderID
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TreeID
		{
			set{ _treeid=value;}
			get{return _treeid;}
		}
		#endregion ����
	}
}

