//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2007-4-29 9:48:55
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// ʵ����GraPowerRelation ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class GraPowerRelation
	{
		public GraPowerRelation()
		{}
		#region �ֶ�
		private string _uid="";
        private string _powereachid;
		private string _layerid="";
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
        public string PowerEachID
		{
			set{ _powereachid=value;}
			get{return _powereachid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LayerID
		{
			set{ _layerid=value;}
			get{return _layerid;}
		}
		#endregion ����
	}
}

