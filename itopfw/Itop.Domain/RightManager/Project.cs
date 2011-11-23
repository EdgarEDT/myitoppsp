//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-9-18 17:11:53
//
//********************************************************************************/
using System;
namespace Itop.Domain.RightManager
{
	/// <summary>
	/// 实体类Project 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Project 
	{
		public Project()  
		{}
		#region 字段
		private string _uid="";
		private int _sortid;
		private string _projectcode="";
		private string _projectname="";
		private string _projectmanager="";
		private string _address="";
		private string _projectstate="";
		private DateTime _createdate;
		private DateTime _startdate;
		private DateTime _plancompletedate;
		private DateTime _completedate;
		private DateTime _qualitydate;
		private DateTime _becomeeffective;
		private DateTime _createtime;
		private string _createusername="";
		private DateTime _updatetime;
		private string _updateusername="";
		private string _isguidang="";
		private string _guidangname="";
		private DateTime _guidangtime;
		#endregion 字段

		#region 属性
		/// <summary>
		/// UID
		/// </summary>
		public string UID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// 排序编号
		/// </summary>
		public int SortID
		{
			set{ _sortid=value;}
			get{return _sortid;}
		}
		/// <summary>
		/// 项目编号
		/// </summary>
		public string ProjectCode
		{
			set{ _projectcode=value;}
			get{return _projectcode;}
		}
		/// <summary>
		/// 项目名称
		/// </summary>
		public string ProjectName
		{
			set{ _projectname=value;}
			get{return _projectname;}
		}
		/// <summary>
		/// 项目经理
		/// </summary>
		public string ProjectManager
		{
			set{ _projectmanager=value;}
			get{return _projectmanager;}
		}
		/// <summary>
		/// 工地地址
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 工程状态
		/// </summary>
		public string ProjectState
		{
			set{ _projectstate=value;}
			get{return _projectstate;}
		}
		/// <summary>
		/// 合同签订日期
		/// </summary>
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 工程开工日期
		/// </summary>
		public DateTime StartDate
		{
			set{ _startdate=value;}
			get{return _startdate;}
		}
		/// <summary>
		/// 计划完成日期
		/// </summary>
		public DateTime PlanCompleteDate
		{
			set{ _plancompletedate=value;}
			get{return _plancompletedate;}
		}
		/// <summary>
		/// 实际完成日期
		/// </summary>
		public DateTime CompleteDate
		{
			set{ _completedate=value;}
			get{return _completedate;}
		}
		/// <summary>
		/// 质保截止日期
		/// </summary>
		public DateTime QualityDate
		{
			set{ _qualitydate=value;}
			get{return _qualitydate;}
		}
		/// <summary>
		/// 合同生效日期
		/// </summary>
		public DateTime BecomeEffective
		{
			set{ _becomeeffective=value;}
			get{return _becomeeffective;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 创建操作人员姓名
		/// </summary>
		public string CreateUserName
		{
			set{ _createusername=value;}
			get{return _createusername;}
		}
		/// <summary>
		/// 更新日期
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 更新的操作人员的姓名
		/// </summary>
		public string UpdateUserName
		{
			set{ _updateusername=value;}
			get{return _updateusername;}
		}
		/// <summary>
		/// 是否归档
		/// </summary>
		public string IsGuiDang
		{
			set{ _isguidang=value;}
			get{return _isguidang;}
		}
		/// <summary>
		/// 归档人员
		/// </summary>
		public string GuiDangName
		{
			set{ _guidangname=value;}
			get{return _guidangname;}
		}
		/// <summary>
		/// 归档时间
		/// </summary>
		public DateTime GuiDangTime
		{
			set{ _guidangtime=value;}
			get{return _guidangtime;}
		}
		#endregion 属性
	}
}

