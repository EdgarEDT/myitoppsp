//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2009-9-28 9:50:25
//
//********************************************************************************/
using System;
namespace Itop.Domain.BaseData
{
	/// <summary>
	/// ʵ����Ps_Volume ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Ps_Volume
	{
		public Ps_Volume()
		{}
		#region �ֶ�
		private string _id="";
		private int _years;
		private double _maxpw;
		private double _yearendvolume;
		private double _watervolume;
		private double _firevolume;
		private double _backupvolume;
		private double _toolsvolume;
		private double _maxvolume;
		private double _balkvolume;
		private double _balkwatervolume;
		private double _balkfirevolume;
		private double _balancevolume;
		private double _feedpw;
		private double _getpw;
		private double _breakpw;
		private double _getps;
		private string _iswaterfire="";
		private double _iswaterfirepst;
		private double _isgetpwpst;
		private DateTime _createtime;
		private string _createuser="";
		private string _col1="";
		private string _col2="";
		private string _col3="";
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// 
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// ���
		/// </summary>
		public int Years
		{
			set{ _years=value;}
			get{return _years;}
		}
		/// <summary>
		/// ��󹩵縺������
		/// </summary>
		public double MaxPw
		{
			set{ _maxpw=value;}
			get{return _maxpw;}
		}
		/// <summary>
		/// ��ĩװ������
		/// </summary>
		public double YearEndVolume
		{
			set{ _yearendvolume=value;}
			get{return _yearendvolume;}
		}
		/// <summary>
		/// ˮ��װ������
		/// </summary>
		public double WaterVolume
		{
			set{ _watervolume=value;}
			get{return _watervolume;}
		}
		/// <summary>
		/// ���װ������
		/// </summary>
		public double FireVolume
		{
			set{ _firevolume=value;}
			get{return _firevolume;}
		}
		/// <summary>
		/// װ����������
		/// </summary>
		public double BackupVolume
		{
			set{ _backupvolume=value;}
			get{return _backupvolume;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public double ToolsVolume
		{
			set{ _toolsvolume=value;}
			get{return _toolsvolume;}
		}
		/// <summary>
		/// ��󵥻�����
		/// </summary>
		public double MaxVolume
		{
			set{ _maxvolume=value;}
			get{return _maxvolume;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public double balkVolume
		{
			set{ _balkvolume=value;}
			get{return _balkvolume;}
		}
		/// <summary>
		/// ˮ����������
		/// </summary>
		public double balkWaterVolume
		{
			set{ _balkwatervolume=value;}
			get{return _balkwatervolume;}
		}
		/// <summary>
		/// �����������
		/// </summary>
		public double balkFireVolume
		{
			set{ _balkfirevolume=value;}
			get{return _balkfirevolume;}
		}
		/// <summary>
		/// ƽ������
		/// </summary>
		public double BalanceVolume
		{
			set{ _balancevolume=value;}
			get{return _balancevolume;}
		}
		/// <summary>
		/// �ɹ�����
		/// </summary>
		public double FeedPw
		{
			set{ _feedpw=value;}
			get{return _feedpw;}
		}
		/// <summary>
		/// �������ܵ���
		/// </summary>
		public double GetPw
		{
			set{ _getpw=value;}
			get{return _getpw;}
		}
		/// <summary>
		/// ����ӯ��+������-��ƽ��
		/// </summary>
		public double BreakPw
		{
			set{ _breakpw=value;}
			get{return _breakpw;}
		}
		/// <summary>
		/// ���ܵ����
		/// </summary>
		public double GetPs
		{
			set{ _getps=value;}
			get{return _getps;}
		}
		/// <summary>
		/// �Ƿ�ˮ������
		/// </summary>
		public string IsWaterFire
		{
			set{ _iswaterfire=value;}
			get{return _iswaterfire;}
		}
		/// <summary>
		/// ˮ�������ٷֱ�
		/// </summary>
		public double IsWaterFirePst
		{
			set{ _iswaterfirepst=value;}
			get{return _iswaterfirepst;}
		}
		/// <summary>
		/// �ɹ������ٷֱ�
		/// </summary>
		public double IsGetPwPst
		{
			set{ _isgetpwpst=value;}
			get{return _isgetpwpst;}
		}
		/// <summary>
		/// ���ʱ��
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// �����
		/// </summary>
		public string CreateUser
		{
			set{ _createuser=value;}
			get{return _createuser;}
		}
		/// <summary>
		/// ����1
		/// </summary>
		public string Col1
		{
			set{ _col1=value;}
			get{return _col1;}
		}
		/// <summary>
		/// ����2
		/// </summary>
		public string Col2
		{
			set{ _col2=value;}
			get{return _col2;}
		}
		/// <summary>
		/// ����3
		/// </summary>
		public string Col3
		{
			set{ _col3=value;}
			get{return _col3;}
		}
		#endregion ����
	}
}

