//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2010-01-07 9:16:07
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// ʵ����Psp_ProgLayerList ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Psp_ProgLayerList
	{
		public Psp_ProgLayerList()
		{}
		#region �ֶ�
		private string _uid="";
		private string _proguid="";
		private string _layergradeid="";
		private string _col1="";
		private string _col2="";
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
		public string ProgUID
		{
			set{ _proguid=value;}
			get{return _proguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LayerGradeID
		{
			set{ _layergradeid=value;}
			get{return _layergradeid;}
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
		#endregion ����
	}
}

