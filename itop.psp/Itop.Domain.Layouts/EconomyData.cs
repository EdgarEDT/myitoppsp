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
    public class EconomyData
	{
        public EconomyData()
		{}
		#region �ֶ�
		private int s1 = 0;
        private double s2 = 0;
        private double s3 = 0;
		#endregion �ֶ�

		#region ����

        public int S1
        {
            get { return s1; }
            set { s1 = value; }
        }

        public double S2
        {
            get { return s2; }
            set { s2 = value; }
        }
        public double S3
        {
            get { return s3; }
            set { s3 = value; }
        }

		#endregion ����
	}
}

