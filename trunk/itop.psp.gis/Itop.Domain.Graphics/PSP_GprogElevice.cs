//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2010-4-23 9:34:51
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// ʵ����PSP_GprogElevice ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PSP_GprogElevice
	{
		public PSP_GprogElevice()
		{}
		#region �ֶ�
		private string _gproguid="";
		private string _devicesuid="";
		private string _type="";
		private string _ztstatus="";
		private string _jqstatus="";
		private string _zqstatus="";
		private string _yqstatus="";
		private string _l1="";
		private string _l2="";
		private string _l3="";
		private string _l4="";
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// 
		/// </summary>
		public string GprogUID
		{
			set{ _gproguid=value;}
			get{return _gproguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DeviceSUID
		{
			set{ _devicesuid=value;}
			get{return _devicesuid;}
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
		public string ZTstatus
		{
			set{ _ztstatus=value;}
			get{return _ztstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string JQstatus
		{
			set{ _jqstatus=value;}
			get{return _jqstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZQstatus
		{
			set{ _zqstatus=value;}
			get{return _zqstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string YQstatus
		{
			set{ _yqstatus=value;}
			get{return _yqstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L1
		{
			set{ _l1=value;}
			get{return _l1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L2
		{
			set{ _l2=value;}
			get{return _l2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L3
		{
			set{ _l3=value;}
			get{return _l3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L4
		{
			set{ _l4=value;}
			get{return _l4;}
		}
		#endregion ����
	}
}

