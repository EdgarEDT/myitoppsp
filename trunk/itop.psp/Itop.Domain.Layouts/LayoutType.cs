//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-11-16 10:34:18
//
//********************************************************************************/
using System;
namespace Itop.Domain.Layouts
{
	/// <summary>
	/// ʵ����Layout ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class LayoutType
	{
        public LayoutType()
		{}
		#region �ֶ�
        private string uid = "";
        private byte[] excelData; 

		#endregion �ֶ�

		#region ����

        public string UID
        {
            get { return uid; }
            set { uid = value; }
        }

        public byte[] ExcelData
        {
            get { return excelData; }
            set { excelData = value; }
        }


		#endregion ����
	}
}

