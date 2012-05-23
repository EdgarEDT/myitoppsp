using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Itop.Client
{
    class SqlCode
    {
        

        public static SqlConnection GetConn(DataRow row)
        {
            string connstr = "Server=" + row["Server"].ToString() + ";database=master;uid=" + row["userid"].ToString() + ";pwd=" + row["password"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            return conn;      
        }



        public static DataTable GetDataTable(DataRow row,string sql,string dbname)
        {
            string connstr = "Server=" + row["Server"].ToString() + ";database=" + dbname + ";uid=" + row["userid"].ToString() + ";pwd=" + row["password"].ToString();
            SqlConnection conn = new SqlConnection(connstr);

            try
            {
                SqlDataAdapter ada = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                ada.Fill(dt);
                return dt;

            }
            catch
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }






    }
}
