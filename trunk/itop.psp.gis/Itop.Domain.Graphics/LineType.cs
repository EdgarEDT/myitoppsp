//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2007-4-2 10:22:45
//
//********************************************************************************/
using System;
using System.Drawing;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// ʵ����LineType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class LineType
	{
		public LineType()
		{}
		#region �ֶ�
		private string _uid="";
		private string _typename="";
		private string _color="";
		private string _obligatefield1="";
        private Color _objcolor ;

       
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
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Color
		{
			set{ _color=value;}
			get{return _color;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ObligateField1
		{
			set{ _obligatefield1=value;}
			get{return _obligatefield1;}
		}
        public Color ObjColor
        {
            get { return _objcolor; }
            set { _objcolor = value; }
        }
		#endregion ����
	}
}

