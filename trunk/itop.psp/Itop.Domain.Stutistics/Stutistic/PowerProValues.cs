//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-11-29 15:03:50
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
	/// ʵ����Values ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PowerProValues
    {
        public PowerProValues()
        { }
        #region �ֶ�
        private int _id;
        private string _typeid;
        private string _typeid1;
        private string _year;
        private string _value;
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
        public string TypeID
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}

        public string TypeID1
        {
            set { _typeid1 = value; }
            get { return _typeid1; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string Year
		{
			set{ _year=value;}
			get{return _year;}
		}
		/// <summary>
		/// 
		/// </summary>
        public string Value
		{
			set{ _value=value;}
			get{return _value;}
		}
		#endregion ����
	}
}

