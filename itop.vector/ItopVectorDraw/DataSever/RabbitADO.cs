using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace Rabbit.Data.OleDb
{
	/// <summary>
	/// ���ݿ����ӹ���(OleDb)
	/// </summary>
	public class RabbitADO:IDisposable
	{
		
		/// <summary>
		/// ���ݿ�����ʵ��
		/// 
		/// </summary>
		private OleDbConnection con;//
		/// <summary>
		/// �����ַ���
		/// </summary>
		static string m_ConnectionString;
		/// <summary>
		/// �๲���ʵ��
		/// </summary>
		static RabbitADO instance;//�����ʵ��
		/// <summary>
		/// ��̬���캯��
		/// </summary>
		static RabbitADO(){ instance=new RabbitADO();}
		/// <summary>
		/// ��ӿ�
		/// </summary>

		public static  RabbitADO Instance { get { return instance; } }
		/// <summary>
		/// ���ݿ�����
		/// </summary>
		public OleDbConnection Conn{get{this.CheckConnection();return con;}}
		/// <summary>
		/// ���������ݿ�����
		/// </summary>
		/// <returns>���ݿ�����</returns>
		//
		public static OleDbConnection NewConn()
		{
			return new OleDbConnection(ConnectionString());
		}
		/// <summary>
		/// ���캯��
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
		/// ���ݲ�ѯSQL�õ�DataSet
		/// </summary>
		/// <param name="cmd">SQL��ѯ�ַ���</param>
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
				throw new Exception("SQL��������", se);
			}
			this.Dispose();
			return dataSet;
		}
		/// <summary>
		/// ����OleDbDataAdapter
		/// </summary>
		/// <param name="cmd">SQL��ѯ�ַ���</param>
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
				throw new Exception("SQL��������", se);
			}
			this.Dispose();
			return dataAdapter;
		}
		/// <summary>
		/// ����OleDbDataReader
		/// </summary>
		/// <param name="cmd">SQL��ѯ�ַ���</param>
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
				throw new Exception("���ݿ��ʧ��.", se);
			}
			
			return dr;
		}
		/// <summary>
		/// ִ��SQL���
		/// </summary>
		/// <param name="cmd">SQL��ѯ�ַ���</param>
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
		/// ������ݿ������Ƿ��
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
				throw new Exception("���ݿ��ʧ��!", se);
			}

		}

		/// <summary>
		/// �ӳ��������ļ��õ����ݿ������ַ���
		/// </summary>
		/// <returns>�����ַ���</returns>
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
		/// �ӳ��������ļ��õ���Ӧ�����ַ���
		/// </summary>
		/// <param name="key">�����ļ����м�</param>
		/// <returns></returns>
		public  String GetSettingsValue(string key)
		{
			string sReturn;
			
			sReturn = ConfigurationSettings.AppSettings[key].ToString();

			if (sReturn == null) 
			{
				throw new Exception("�� App.config û����Ӧ��ֵ");
			}			
			return sReturn;

		}
		/// <summary>
		/// �ر��������ӣ��ͷ���Դ
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
				
				throw new Exception("���ݿ�����ʧ��.", e);				
			}
		}	
	}
}

