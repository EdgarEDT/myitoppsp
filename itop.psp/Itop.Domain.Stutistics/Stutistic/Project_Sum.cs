//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2008-9-4 11:04:29
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistics
{
	/// <summary>
	/// ʵ����Project_Sum ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Project_Sum
	{
		public Project_Sum()
		{}
		#region �ֶ�
		private string _uid="";
		private string _type="";
		private string _name="";
		private string _t1="";
		private string _t2="";
		private string _t3="";
        private string _t4 = "";
        private string _t5 = "";
        private string _l1 = "";
        private string _l2 = "";
        private string _l3 = "";
        private string _l4 = "";
        private string _l5 = "";
		private double _num;
		private string _s1="";
		private string _s2="";
		private string _s3="";
		private string _s4="";
		private string _s5="";
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
		/// ����
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// ���ͷ���
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// ����̨��
		/// </summary>
		public string T1
		{
			set{ _t1=value;}
			get{return _t1;}
		}
		/// <summary>
		/// ���߹�ģ
		/// </summary>
		public string T2
		{
			set{ _t2=value;}
			get{return _t2;}
		}
		/// <summary>
		/// ������ʽ
		/// </summary>
		public string T3
		{
			set{ _t3=value;}
			get{return _t3;}
		}

        /// <summary>
        ///�޹�����
        /// </summary>
        public string T4
        {
            set { _t4 = value; }
            get { return _t4; }
        }

        /// <summary>
        ///  ����
        /// </summary>
        public string T5
        {
            set { _t5 = value; }
            get { return _t5; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string L1
        {
            set { _l1 = value; }
            get { return _l1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L2
        {
            set { _l2 = value; }
            get { return _l2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L3
        {
            set { _l3 = value; }
            get { return _l3; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string L4
        {
            set { _l4 = value; }
            get { return _l4; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string L5
        {
            set { _l5 = value; }
            get { return _l5; }
        }










		/// <summary>
		/// ��̬Ͷ��
		/// </summary>
		public double Num
		{
			set{ _num=value;}
			get{return _num;}
		}
		/// <summary>
		/// �Զ�����
		/// </summary>
		public string S1
		{
			set{ _s1=value;}
			get{return _s1;}
		}
		/// <summary>
		/// �Զ�����
		/// </summary>
		public string S2
		{
			set{ _s2=value;}
			get{return _s2;}
		}
		/// <summary>
		/// �Զ�����
		/// </summary>
		public string S3
		{
			set{ _s3=value;}
			get{return _s3;}
		}
		/// <summary>
		/// �Զ�����
		/// </summary>
		public string S4
		{
			set{ _s4=value;}
			get{return _s4;}
		}
		/// <summary>
		/// �Զ�����
		/// </summary>
		public string S5
		{
			set{ _s5=value;}
			get{return _s5;}
		}
		#endregion ����
	}
}

