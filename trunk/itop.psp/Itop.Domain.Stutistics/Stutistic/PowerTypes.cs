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
	public class PowerTypes
	{
		public PowerTypes()
		{}
		#region �ֶ�
		private int _id;
		private string _title="";
		private int _flag;
		private string _flag2;
		private int _parentid;
        private double lixi;
        private double yubei;
        private double dongtai;
        private double jingtai;

        public double Jingtai
        {
            get { return jingtai; }
            set { jingtai = value; }
        }
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// 
		/// </summary>
        /// 
        public double Lixi
        {
            set { lixi = value; }
            get { return lixi; }
        }

        public double Yubei
        {
            set { yubei = value; }
            get { return yubei; }
        }

        public double Dongtai
        {
            set { dongtai = value; }
            get { return dongtai; }
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

