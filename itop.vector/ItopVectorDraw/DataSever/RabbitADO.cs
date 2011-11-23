using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace Rabbit.Data.OleDb
{
	/// <summary>
	/// 数据库连接管理(OleDb)
	/// </summary>
	public class RabbitADO:IDisposable
	{
		
		/// <summary>
		/// 数据库连接实例
		/// 
		/// </summary>
		private OleDbConnection con;//
		/// <summary>
		/// 连接字符串
		/// </summary>
		static string m_ConnectionString;
		/// <summary>
		/// 类共享的实例
		/// </summary>
		static RabbitADO instance;//共享的实例
		/// <summary>
		/// 静态构造函数
		/// </summary>
		static RabbitADO(){ instance=new RabbitADO();}
		/// <summary>
		/// 类接口
		/// </summary>

		public static  RabbitADO Instance { get { return instance; } }
		/// <summary>
		/// 数据库连接
		/// </summary>
		public OleDbConnection Conn{get{this.CheckConnection();return con;}}
		/// <summary>
		/// 创建新数据库连接
		/// </summary>
		/// <returns>数据库连接</returns>
		//
		public static OleDbConnection NewConn()
		{
			return new OleDbConnection(ConnectionString());
		}
		/// <summary>
		/// 构造函数
		/// </summary>
		public RabbitADO()
		{			
			con = new OleDbConnection(ConnectionString());		
		}
		public RabbitADO(string connectionstring)
		{
			con= new OleDbConnection(connectionstring);
		}
		/// <summary>
		/// 根据查询SQL得到DataSet
		/// </summary>
		/// <param name="cmd">SQL查询字符串</param>
		/// <returns></returns>
		public DataSet GetDataSet(string cmd)
		{
			this.CheckConnection();
			
			DataSet dataSet = new DataSet();

			try
			{
				OleDbCommand dataCommand = new OleDbCommand(cmd,con);
				OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
				dataAdapter.SelectCommand = dataCommand;
						
				dataAdapter.Fill(dataSet, "recordSet");
							
			}
			catch(OleDbException se)
			{
				//ErrorLog el = new ErrorLog(se);
				throw new Exception("SQL语名出错", se);
			}
			this.Dispose();
			return dataSet;
		}
		/// <summary>
		/// 创建OleDbDataAdapter
		/// </summary>
		/// <param name="cmd">SQL查询字符串</param>
		/// <returns>OleDbDataAdapter</returns>
		public  OleDbDataAdapter  GetOleDbDataAdapter(string cmd)
		{
			this.CheckConnection();
			OleDbDataAdapter dataAdapter=null;
			try
			{
				OleDbCommand dataCommand = new OleDbCommand(cmd,con);
				dataAdapter = new OleDbDataAdapter();
				dataAdapter.SelectCommand = dataCommand;
				OleDbCommandBuilder cb = new OleDbCommandBuilder(dataAdapter);
			}
			catch(OleDbException se)
			{
				throw new Exception("SQL语名出错", se);
			}
			this.Dispose();
			return dataAdapter;
		}
		/// <summary>
		/// 创建OleDbDataReader
		/// </summary>
		/// <param name="cmd">SQL查询字符串</param>
		/// <returns></returns>
		public OleDbDataReader GetOleDbDataReader(String cmd)
		{
			this.CheckConnection();

			OleDbDataReader dr = null;
			
			try
			{
				OleDbCommand dc = new OleDbCommand(cmd, con);
				dr = dc.ExecuteReader();
				return dr;
			}
			catch(OleDbException se)
			{
				throw new Exception("数据库打开失败.", se);
			}
			
			return dr;
		}
		/// <summary>
		/// 执行SQL语句
		/// </summary>
		/// <param name="cmd">SQL查询字符串</param>
		public void ExecuteUpdate(string cmd)
		{
			this.CheckConnection();
			try
			{
				OleDbCommand dc = new OleDbCommand(cmd, con);
				dc.ExecuteNonQuery();
			}
			catch(OleDbException se)
			{
				throw new Exception(se.Message, se);
			}
			this.Dispose();
			return; 			
		}
		public void updateSVGFile(string sql,string svg,byte[] bt)
		{
			this.CheckConnection();
			try
			{
				OleDbCommand dataCommand = new OleDbCommand(sql,con);
				OleDbParameter op1=new OleDbParameter("svgdata",svg);
				dataCommand.Parameters.Add(op1);
				OleDbParameter op2=new OleDbParameter("imgdata", OleDbType.Binary, (int)bt.Length, ParameterDirection.Input, false,0, 0, null, DataRowVersion.Current, bt);
				dataCommand.Parameters.Add(op2);
				dataCommand.ExecuteNonQuery();
			}
			catch(OleDbException se)
			{
				throw new Exception(se.Message, se);
			}
			this.Dispose();
		}
		public byte[] getSVGFile(string sql)
		{
			this.CheckConnection();
			byte[] data=null;
			try
			{
				DataSet ds = new DataSet();
				OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(sql,con);
				oleDbDataAdapter.Fill(ds,"table0");
				data=(byte[])ds.Tables[0].Rows[0]["imgdata"];
				ds.Clear();
				oleDbDataAdapter.Dispose();
				return data;
			}
			catch(Exception e1){return data;}
		}
		/// <summary>
		/// 检查数据库连接是否打开
		/// </summary>
		private void CheckConnection()
		{

			try
			{
				if (con.State != ConnectionState.Open)
					con.Open();
			}
			catch (OleDbException se)
			{
				throw new Exception("数据库打开失败!", se);
			}

		}

		/// <summary>
		/// 从程序配置文件得到数据库连接字符串
		/// </summary>
		/// <returns>连接字符串</returns>
		public static  string ConnectionString()
		{
			if (m_ConnectionString == null) 
			{
				try
				{
//					m_ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
				}catch{}
	
				if (m_ConnectionString == null) 
				{
					//throw new Exception("Connect string value not set in App.config");
				}
			}
			return m_ConnectionString;
			
		}
		/// <summary>
		/// 从程序配置文件得到相应键的字符串
		/// </summary>
		/// <param name="key">配置文件的中键</param>
		/// <returns></returns>
		public  String GetSettingsValue(string key)
		{
			string sReturn;
			
			sReturn = ConfigurationSettings.AppSettings[key].ToString();

			if (sReturn == null) 
			{
				throw new Exception("在 App.config 没有相应键值");
			}			
			return sReturn;

		}
		/// <summary>
		/// 关闭数据连接，释放资源
		/// </summary>
		public void Dispose()
		{
			try
			{
				if (con!=null && con.State == ConnectionState.Open)
					con.Close();
			}
			catch(OleDbException e)
			{
				
				throw new Exception("数据库连接失败.", e);				
			}
		}	
	}
}

