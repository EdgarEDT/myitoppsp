using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace ItopVector.DataSever
{
	/// <summary>
	/// ConnectInfo 数据库边接信息
	/// </summary>
	/// 
	public enum DbmsType
	{
		SqlServer,
		Access,
		Odbc,
		Oledb
	}
	public class ConnectInfo
	{
		public string Servername;
		public string Database;
		public string Userid;
		public string Userpwd;
		public int	Timeout=10;
		public string driver;//数据库驱动器
		public string Uid;
		public string Text;

		public DbmsType Dbmstype;

		public ConnectInfo()
		{
			this.Text="服务器";
			this.Dbmstype=DbmsType.Oledb;
			this.Uid=Guid.NewGuid().ToString();
			this.driver="SQLOLEDB";
		}
		public ConnectInfo(string servername,string database,string userid,string userpwd):this()
		{
			this.Database=database;
			this.Userid=userid;
			this.Servername=servername;
			this.Userpwd=userpwd;
		}
		public bool TestConnect(ref string errtext)
		{
			bool flag1=false;
			string connectstring=this.GetConnectString();
			switch(Dbmstype)
			{
				case DbmsType.SqlServer:
					SqlConnection conn=new SqlConnection(connectstring);
					try
					{
						conn.Open();
						if(conn.State == ConnectionState.Open)
							flag1=true;
						conn.Close();
					}
					catch(Exception e )
					{
						errtext=e.Message;
					}

					break;
				case DbmsType.Oledb:
					OleDbConnection conn2=new OleDbConnection(connectstring);
					try
					{
						conn2.Open();
						if(conn2.State == ConnectionState.Open)
							flag1=true;
						conn2.Close();
					}
					catch(Exception e )
					{
						errtext=e.Message;
					}

					break;
				case DbmsType.Access:
					break;
				case DbmsType.Odbc:
					break;
			}
			return flag1;
		}

		public string GetConnectString()
		{
			string connectstring =string.Empty;
			switch(Dbmstype)
			{
				case DbmsType.SqlServer:
					connectstring=string.Format("database={0};server={1};user id={2};password={3};timeout={4}",this.Database,this.Servername,this.Userid,this.Userpwd,this.Timeout);
					break;
				case DbmsType.Oledb:
					string str1=string.Empty;
					
					if(this.Driver.ToLower().LastIndexOf("jet.oledb.4")>=0)
					{
						str1="Provider={5};Data Source={1};User ID={2};Jet OLEDB:Database Password={3};Persist Security Info=False;";
					}
					else
					{
						str1="Provider={5};Data Source={1};User ID={2};Initial Catalog={0};password={3};timeout={4}";
					}

					connectstring=string.Format(str1,this.Database,this.Servername,this.Userid,this.Userpwd,this.Timeout,this.Driver);
				
					break;
				case DbmsType.Access:
					break;
				case DbmsType.Odbc:
					break;
			}
			return connectstring;
		}
		public string Driver
		{
			get
			{
				return this.driver;
			}
			set
			{
				if(value==string.Empty)return;
				this.driver=value;
			}
		}
	}
}
