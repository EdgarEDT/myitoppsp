//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-9-18 17:11:53
//
//********************************************************************************/
using System;
namespace Itop.Domain.RightManager
{
	/// <summary>
	/// ʵ����Project ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Project 
	{
		public Project()  
		{}
		#region �ֶ�
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
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// UID
		/// </summary>
		public string UID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public int SortID
		{
			set{ _sortid=value;}
			get{return _sortid;}
		}
		/// <summary>
		/// ��Ŀ���
		/// </summary>
		public string ProjectCode
		{
			set{ _projectcode=value;}
			get{return _projectcode;}
		}
		/// <summary>
		/// ��Ŀ����
		/// </summary>
		public string ProjectName
		{
			set{ _projectname=value;}
			get{return _projectname;}
		}
		/// <summary>
		/// ��Ŀ����
		/// </summary>
		public string ProjectManager
		{
			set{ _projectmanager=value;}
			get{return _projectmanager;}
		}
		/// <summary>
		/// ���ص�ַ
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// ����״̬
		/// </summary>
		public string ProjectState
		{
			set{ _projectstate=value;}
			get{return _projectstate;}
		}
		/// <summary>
		/// ��ͬǩ������
		/// </summary>
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// ���̿�������
		/// </summary>
		public DateTime StartDate
		{
			set{ _startdate=value;}
			get{return _startdate;}
		}
		/// <summary>
		/// �ƻ��������
		/// </summary>
		public DateTime PlanCompleteDate
		{
			set{ _plancompletedate=value;}
			get{return _plancompletedate;}
		}
		/// <summary>
		/// ʵ���������
		/// </summary>
		public DateTime CompleteDate
		{
			set{ _completedate=value;}
			get{return _completedate;}
		}
		/// <summary>
		/// �ʱ���ֹ����
		/// </summary>
		public DateTime QualityDate
		{
			set{ _qualitydate=value;}
			get{return _qualitydate;}
		}
		/// <summary>
		/// ��ͬ��Ч����
		/// </summary>
		public DateTime BecomeEffective
		{
			set{ _becomeeffective=value;}
			get{return _becomeeffective;}
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
		/// ����������Ա����
		/// </summary>
		public string CreateUserName
		{
			set{ _createusername=value;}
			get{return _createusername;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// ���µĲ�����Ա������
		/// </summary>
		public string UpdateUserName
		{
			set{ _updateusername=value;}
			get{return _updateusername;}
		}
		/// <summary>
		/// �Ƿ�鵵
		/// </summary>
		public string IsGuiDang
		{
			set{ _isguidang=value;}
			get{return _isguidang;}
		}
		/// <summary>
		/// �鵵��Ա
		/// </summary>
		public string GuiDangName
		{
			set{ _guidangname=value;}
			get{return _guidangname;}
		}
		/// <summary>
		/// �鵵ʱ��
		/// </summary>
		public DateTime GuiDangTime
		{
			set{ _guidangtime=value;}
			get{return _guidangtime;}
		}
		#endregion ����
	}
}

