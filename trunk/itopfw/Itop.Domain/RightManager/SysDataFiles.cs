

//********************************************************************************/
//
//�˴�����TONLI.NET�����������Զ�����.
//����ʱ��:2012-5-28 9:38:17
//
//********************************************************************************/
using System;
namespace Itop.Domain
{
	/// <summary>
	/// ʵ����SysDataFiles ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class SysDataFiles
	{
		public SysDataFiles()
		{}
		#region �ֶ�
		private string _id=Guid.NewGuid().ToString();
		private string _filename="";
		private int _filesize;
		private byte[] _files;
		private string _filedesc="";
		private int _sort;
		private string _remark="";
		private DateTime _createdate;
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
		public string FileName
		{
			set{ _filename=value;}
			get{return _filename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int FileSize
		{
			set{ _filesize=value;}
			get{return _filesize;}
		}
		/// <summary>
		/// 
		/// </summary>
		public byte[] Files
		{
			set{ _files=value;}
			get{return _files;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FileDesc
		{
			set{ _filedesc=value;}
			get{return _filedesc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		#endregion ����
	}
}

