//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-11-29 16:07:48
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
	/// ʵ����PSP_Types ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PowerStuffTypes
	{
        public PowerStuffTypes()
		{}
		#region �ֶ�
		private int _id;
		private string _title="";
		private int _flag;
		private string _flag2;
		private int _parentid;
        private string code;
        private string remark;

        //public double Jingtai
        //{
        //    get { return jingtai; }
        //    set { jingtai = value; }
        //}
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// 
		/// </summary>
        /// 
        //public double Lixi
        //{
        //    set { lixi = value; }
        //    get { return lixi; }
        //}

        public string Code
        {
            set { code = value; }
            get { return code; }
        }

        public string Remark
        {
            set { remark = value; }
            get { return remark; }
        }

		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
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
		public string Flag2
		{
			set{ _flag2=value;}
			get{return _flag2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		#endregion ����
	}
}

