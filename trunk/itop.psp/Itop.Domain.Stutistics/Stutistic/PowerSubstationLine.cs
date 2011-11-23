//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2007-7-9 15:48:20
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
	/// ʵ����PowerSubstationLine ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PowerSubstationLine
	{
		public PowerSubstationLine()
		{}
		#region �ֶ�
		private string _uid=Guid.NewGuid().ToString();
		private string _title="";
		private string _type="";
        private string _type2 = "";
        private string _classtype = "";
		private string _flag="";
		private DateTime _createtime;
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
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// �ֶ�S
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}

        /// <summary>
        /// �ֶ�S
        /// </summary>
        public string Type2
        {
            set { _type2 = value; }
            get { return _type2; }
        }


        /// <summary>
        /// �ֶ�S
        /// </summary>
        public string ClassType
        {
            set { _classtype = value; }
            get { return _classtype; }
        }

		/// <summary>
		/// ��� 1���վ 2��·
		/// </summary>
		public string Flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion ����
	}
}

