//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2007-4-23 8:38:49
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
	/// ʵ����BurdenMonth ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class BurdenYear
	{
        public BurdenYear()
		{}
		#region �ֶ�
		private string _uid=Guid.NewGuid().ToString();
		private int _burdenyear=0;
        private DateTime _burdendate = DateTime.Now;
		private double _values=0.0;
        private string _areaid = "";
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
		public int BurdenYears
		{
			set{ _burdenyear=value;}
			get{return _burdenyear;}
		}

        public DateTime BurdenDate
        {
            set { _burdendate = value; }
            get { return _burdendate; }
        }

		/// <summary>
		/// 
		/// </summary>
		public double Values
		{
            set { _values = value; }
            get { return _values; }
		}
        public string AreaID
        {
            set { _areaid = value; }
            get { return _areaid; }
        }
		#endregion ����
	}
}

