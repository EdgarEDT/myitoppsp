//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-12-6 9:07:37
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
	/// ʵ����BurdenLine ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class BurdenLine
	{
		public BurdenLine()
		{}
		#region �ֶ�
		private string _uid=Guid.NewGuid().ToString();
		private string _season="";
		private DateTime _burdendate;
		private double _hour1;
		private double _hour2;
		private double _hour3;
		private double _hour4;
		private double _hour5;
		private double _hour6;
		private double _hour7;
		private double _hour8;
		private double _hour9;
		private double _hour10;
		private double _hour11;
		private double _hour12;
		private double _hour13;
		private double _hour14;
		private double _hour15;
		private double _hour16;
		private double _hour17;
		private double _hour18;
		private double _hour19;
		private double _hour20;
		private double _hour21;
		private double _hour22;
		private double _hour23;
		private double _hour24;
		private bool _istype;
        private bool isMaxDate;
		private double _dayaverage;
		private double _minaverage;
        private string _areaid;

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
		public string Season
		{
			set{ _season=value;}
			get{return _season;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public DateTime BurdenDate
		{
			set{ _burdendate=value;}
			get{return _burdendate;}
		}
		/// <summary>
		/// 1��
		/// </summary>
		public double Hour1
		{
			set{ _hour1=value;}
			get{return _hour1;}
		}
		/// <summary>
		/// 2��
		/// </summary>
		public double Hour2
		{
			set{ _hour2=value;}
			get{return _hour2;}
		}
		/// <summary>
		/// 3��
		/// </summary>
		public double Hour3
		{
			set{ _hour3=value;}
			get{return _hour3;}
		}
		/// <summary>
		/// 4��
		/// </summary>
		public double Hour4
		{
			set{ _hour4=value;}
			get{return _hour4;}
		}
		/// <summary>
		/// 5��
		/// </summary>
		public double Hour5
		{
			set{ _hour5=value;}
			get{return _hour5;}
		}
		/// <summary>
		/// 6��
		/// </summary>
		public double Hour6
		{
			set{ _hour6=value;}
			get{return _hour6;}
		}
		/// <summary>
		/// 7��
		/// </summary>
		public double Hour7
		{
			set{ _hour7=value;}
			get{return _hour7;}
		}
		/// <summary>
		/// 8��
		/// </summary>
		public double Hour8
		{
			set{ _hour8=value;}
			get{return _hour8;}
		}
		/// <summary>
		/// 9��
		/// </summary>
		public double Hour9
		{
			set{ _hour9=value;}
			get{return _hour9;}
		}
		/// <summary>
		/// 10��
		/// </summary>
		public double Hour10
		{
			set{ _hour10=value;}
			get{return _hour10;}
		}
		/// <summary>
		/// 11��
		/// </summary>
		public double Hour11
		{
			set{ _hour11=value;}
			get{return _hour11;}
		}
		/// <summary>
		/// 12��
		/// </summary>
		public double Hour12
		{
			set{ _hour12=value;}
			get{return _hour12;}
		}
		/// <summary>
		/// 13��
		/// </summary>
		public double Hour13
		{
			set{ _hour13=value;}
			get{return _hour13;}
		}
		/// <summary>
		/// 14��
		/// </summary>
		public double Hour14
		{
			set{ _hour14=value;}
			get{return _hour14;}
		}
		/// <summary>
		/// 15��
		/// </summary>
		public double Hour15
		{
			set{ _hour15=value;}
			get{return _hour15;}
		}
		/// <summary>
		/// 16��
		/// </summary>
		public double Hour16
		{
			set{ _hour16=value;}
			get{return _hour16;}
		}
		/// <summary>
		/// 17��
		/// </summary>
		public double Hour17
		{
			set{ _hour17=value;}
			get{return _hour17;}
		}
		/// <summary>
		/// 18��
		/// </summary>
		public double Hour18
		{
			set{ _hour18=value;}
			get{return _hour18;}
		}
		/// <summary>
		/// 19��
		/// </summary>
		public double Hour19
		{
			set{ _hour19=value;}
			get{return _hour19;}
		}
		/// <summary>
		/// 20��
		/// </summary>
		public double Hour20
		{
			set{ _hour20=value;}
			get{return _hour20;}
		}
		/// <summary>
		/// 21��
		/// </summary>
		public double Hour21
		{
			set{ _hour21=value;}
			get{return _hour21;}
		}
		/// <summary>
		/// 22��
		/// </summary>
		public double Hour22
		{
			set{ _hour22=value;}
			get{return _hour22;}
		}
		/// <summary>
		/// 23��
		/// </summary>
		public double Hour23
		{
			set{ _hour23=value;}
			get{return _hour23;}
		}
		/// <summary>
		/// 24��
		/// </summary>
		public double Hour24
		{
			set{ _hour24=value;}
			get{return _hour24;}
		}
		/// <summary>
        /// �Ƿ�Ϊ������
		/// </summary>
		public bool IsType
		{
			set{ _istype=value;}
			get{return _istype;}
		}

        /// <summary>
        /// �Ƿ�Ϊ������IsMaxDate
        /// </summary>
        public bool IsMaxDate
        {
            set { isMaxDate = value; }
            get { return isMaxDate; }
        }
		/// <summary>
		/// ƽ������
		/// </summary>
		public double DayAverage
		{
			set{ _dayaverage=value;}
			get{return _dayaverage;}
		}
		/// <summary>
		/// ��С����
		/// </summary>
		public double MinAverage
		{
			set{ _minaverage=value;}
			get{return _minaverage;}
		}

        public string AreaID
        {
            set { _areaid = value; }
            get { return _areaid; }
        }
		#endregion ����
	}
}

