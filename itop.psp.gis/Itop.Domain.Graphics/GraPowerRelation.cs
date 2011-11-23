//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2007-4-29 9:48:55
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// 实体类GraPowerRelation 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class GraPowerRelation
	{
		public GraPowerRelation()
		{}
		#region 字段
		private string _uid="";
        private string _powereachid;
		private string _layerid="";
		#endregion 字段

		#region 属性
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
		#endregion 属性
	}
}

