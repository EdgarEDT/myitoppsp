//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2007-5-12 10:48:10
//
//********************************************************************************/
using System;
using System.Drawing;
namespace Itop.Domain.Forecast
{
	/// <summary>
	/// ʵ����FORBaseColor ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class FORBaseColor
	{
		public FORBaseColor()
		{}
		#region �ֶ�
		private string _uid=Guid.NewGuid().ToString();
		private string _title="";
		private int _color;
		private double _maxvalue;
		private double _minvalue;
		private string _remark="";
        private Color _color1 =new Color();
		private DateTime _createtime;
		private DateTime _updatetime;
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public string UID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// ��ɫ
		/// </summary>
		public int Color
		{
			set{ _color=value;}
			get{return _color;}
		}

        public Color Color1
        {
            set { _color1 = value; }
            get { return _color1; }
        }
		/// <summary>
		/// ���ֵ
		/// </summary>
		public double MaxValue
		{
			set{ _maxvalue=value;}
			get{return _maxvalue;}
		}
		/// <summary>
		/// ��Сֵ
		/// </summary>
		public double MinValue
		{
			set{ _minvalue=value;}
			get{return _minvalue;}
		}
		/// <summary>
		/// ��ע
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// �޸�ʱ��
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		#endregion ����
	}
}

