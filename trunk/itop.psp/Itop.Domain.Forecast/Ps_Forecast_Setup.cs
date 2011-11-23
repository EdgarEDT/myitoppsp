//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2009-7-30 9:26:51
//
//********************************************************************************/
using System;
namespace Itop.Domain.Forecast
{
	/// <summary>
	/// ʵ����Ps_Forecast_Setup ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Ps_Forecast_Setup
	{
		public Ps_Forecast_Setup()
		{}
		#region �ֶ�
		private string _id="";
		private int _forecast;
		private string _forecastid="";
		private int _startyear;
		private int _endyear;
		private string _col1="";
		private string _col2="";
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
		/// 
		/// </summary>
		public int Forecast
		{
			set{ _forecast=value;}
			get{return _forecast;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ForecastID
		{
			set{ _forecastid=value;}
			get{return _forecastid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int StartYear
		{
			set{ _startyear=value;}
			get{return _startyear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int EndYear
		{
			set{ _endyear=value;}
			get{return _endyear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Col1
		{
			set{ _col1=value;}
			get{return _col1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Col2
		{
			set{ _col2=value;}
			get{return _col2;}
		}
		#endregion ����
	}
}

