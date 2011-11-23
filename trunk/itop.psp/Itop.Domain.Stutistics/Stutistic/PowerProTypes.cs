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
	public class PowerProTypes
	{
        public PowerProTypes()
		{}
		#region �ֶ�
		private string _id=Guid.NewGuid().ToString();
		private string _title="";
		private int _flag;
		private string _flag2;
        private string _parentid;
        private string code="";
        private string remark;
        private int startyear;
        private int endyear;
        private string isconn;
        private DateTime createdate;
        private DateTime updatedate;

        private string col1;
        private string col2;
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

		public string ID
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
		public string ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}


        public int StartYear
        {
            set { startyear = value; }
            get { return startyear; }
        }

        public int EndYear
        {
            set { endyear = value; }
            get { return endyear; }
        }

        public DateTime CreateTime
        {
            set { createdate = value; }
            get { return createdate; }
        }

        public DateTime UpdateTime
        {
            set { updatedate = value; }
            get { return updatedate; }
        }


        double? _l1;
        double? _l2;
        double? _l3;
        string _l4 = "";
        double? _l5;
        double? _l6;



        /// <summary>
        /// ̨��
        /// </summary>
        public double? L1
        {
            set { _l1 = value; }
            get { return _l1; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public double? L2
        {
            set { _l2 = value; }
            get { return _l2; }
        }


        /// <summary>
        /// ����
        /// </summary>
        public double? L3
        {
            set { _l3 = value; }
            get { return _l3; }
        }


        /// <summary>
        /// �ͺ�
        /// </summary>
        public string L4
        {
            set { _l4 = value; }
            get { return _l4; }
        }


        /// <summary>
        /// ������
        /// </summary>
        public double? L5
        {
            set { _l5 = value; }
            get { return _l5; }
        }

        /// <summary>
        /// ��󸺺�
        /// </summary>
        public double? L6
        {
            set { _l6 = value; }
            get { return _l6; }
        }

        public string IsConn
        {
            set { isconn = value; }
            get { return isconn; }
        }



		#endregion ����
	}
}

