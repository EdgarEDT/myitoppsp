//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-11-29 15:03:50
//
//********************************************************************************/
using System;
namespace Itop.Domain.HistoryValue
{
	/// <summary>
	/// ʵ����Values ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PSP_P_Values
    {
        public PSP_P_Values()
        { }
        #region �ֶ�
        private int _id;
        private int _typeid;
        private int _year;
        private double _value;
        private int _flag2;
        #endregion �ֶ�

		#region ����
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int TypeID
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Year
		{
			set{ _year=value;}
			get{return _year;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Value
		{
			set{ _value=value;}
			get{return _value;}
		}

        /// <summary>
        /// 
        /// </summary>
        public int Flag2
        {
            set { _flag2 = value; }
            get { return _flag2; }
        }


        string _caption = "";
        public string Caption
        {
            set { _caption = value; }
            get { return _caption; }
        }

		#endregion ����
	}
}

