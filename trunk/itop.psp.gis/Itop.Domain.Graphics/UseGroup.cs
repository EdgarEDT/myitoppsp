//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2007-6-5 14:22:23
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// ʵ����UseGroup ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class UseGroup
	{
		public UseGroup()
		{}
		#region �ֶ�
		private string _uid="";
		private string _groupname="";
		private string _content="";
		private string _remark="";
        private string x = "";
        private string y = "";
        private string width = "";
        private string height = "";
      

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
		public string GroupName
		{
			set{ _groupname=value;}
			get{return _groupname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
        public string Width
        {
            get { return width; }
            set { width = value; }
        }
        public string Y
        {
            get { return y; }
            set { y = value; }
        }
        public string X
        {
            get { return x; }
            set { x = value; }
        }
        public string Height
        {
            get { return height; }
            set { height = value; }
        }
		#endregion ����
	}
}

