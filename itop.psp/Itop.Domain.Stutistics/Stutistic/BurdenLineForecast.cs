//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-12-7 15:17:11
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
	/// ʵ����BurdenLineForecast ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class BurdenLineForecast
	{
		public BurdenLineForecast()
		{}
		#region �ֶ�
		private string _uid=Guid.NewGuid().ToString();
		private int _burdenyear;
		private double _summerdayaverage;
		private double _summerminaverage;
		private double _winterdayaverage;
		private double _winterminaverage;
        private double _summerdata;
        private double _winterdata;
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
		/// ���
		/// </summary>
		public int BurdenYear
		{
			set{ _burdenyear=value;}
			get{return _burdenyear;}
		}
		/// <summary>
		/// �ļ���ƽ��������
		/// </summary>
		public double SummerDayAverage
		{
			set{ _summerdayaverage=value;}
			get{return _summerdayaverage;}
		}
		/// <summary>
		/// �ļ�����С������
		/// </summary>
		public double SummerMinAverage
		{
			set{ _summerminaverage=value;}
			get{return _summerminaverage;}
		}
		/// <summary>
		/// ������ƽ��������
		/// </summary>
		public double WinterDayAverage
		{
			set{ _winterdayaverage=value;}
			get{return _winterdayaverage;}
		}
		/// <summary>
		/// ��������С������
		/// </summary>
		public double WinterMinAverage
		{
			set{ _winterminaverage=value;}
			get{return _winterminaverage;}
		}



        /// <summary>
        /// 
        /// </summary>
        public double SummerData
        {
            set { _summerdata = value; }
            get { return _summerdata; }
        }
        /// <summary>
        /// ������ƽ��������
        /// </summary>
        public double WinterData
        {
            set { _winterdata = value; }
            get { return _winterdata; }
        }
		#endregion ����
	}
}

