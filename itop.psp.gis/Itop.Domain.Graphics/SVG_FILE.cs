//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2009-03-23 9:56:25
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// ʵ����SVG_FILE ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class SVG_FILE
	{
		public SVG_FILE()
		{}
		#region �ֶ�
		private string _suid="";
		private string _name="";
		private string _xml="";
		private DateTime _mdate;
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// 
		/// </summary>
		public string SUID
		{
			set{ _suid=value;}
			get{return _suid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NAME
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string XML
		{
			set{ _xml=value;}
			get{return _xml;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime MDATE
		{
			set{ _mdate=value;}
			get{return _mdate;}
		}
		#endregion ����
	}
}

