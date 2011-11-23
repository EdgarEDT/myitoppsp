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
	public class PowerProjectTypes
	{
        public PowerProjectTypes()
		{}
		#region �ֶ�
		private int _id;
		private string _title="";
		private int _flag;
		private string _flag2;
		private int _parentid;
        private int dy;
        private int? dy1;
        private double sgm;
        private double stz;
        private string jsxz;
        private string gznr;
        private string gm;
        private string gcfl;
        private string remark;
        private string typeName="";

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
        public int Dy
        {
            set { dy = value; }
            get { return dy; }
        }

        public int? Dy1
        {
            set { dy1 = value; }
            get { return dy1; }
        }

        public string Jsxz
        {
            set { jsxz = value; }
            get { return jsxz; }
        }

        public string Gznr
        {
            set { gznr = value; }
            get { return gznr; }
        }

        public string Gm
        {
            set { gm = value; }
            get { return gm; }
        }

        public string Gcfl
        {
            set { gcfl = value; }
            get { return gcfl; }
        }

        public string Remark
        {
            set { remark = value; }
            get { return remark; }
        }

        public double Sgm
        {
            set { sgm = value; }
            get { return sgm; }
        }

        public double Stz
        {
            set { stz = value; }
            get { return stz; }
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



        public string TypeName
        {
            set { typeName = value; }
            get { return typeName; }
        }
		#endregion ����
	}
}

