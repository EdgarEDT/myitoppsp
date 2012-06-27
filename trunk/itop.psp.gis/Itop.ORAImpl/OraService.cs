using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Itop.ORAInterFace;

namespace Itop.ORAImpl {
    public class OraService : MarshalByRefObject, IOraService {
        private OracleConnection con;
        public string config = "";
        public DataSet GetDataSet(string sql, string _config) {
            config = _config;
            this.CheckConnection();

            DataSet dataSet = new DataSet();

            try {
                OracleCommand dataCommand = new OracleCommand(sql, con);
                OracleDataAdapter dataAdapter = new OracleDataAdapter();
                dataAdapter.SelectCommand = dataCommand;
                dataAdapter.Fill(dataSet, "recordSet");

            } catch (Exception se) {
                throw new Exception("Error in SQL" + se.Message);
            } finally {
                closeConn();
            }
            return dataSet;
        }
        private void CheckConnection() {
            try {
                con = new OracleConnection(config);

                if (con.State != ConnectionState.Open)
                    con.Open();
            } catch (Exception se) {
                //ErrorLog el = new ErrorLog(se);
                throw new Exception("Error in SQL" + se.Message);
            }
        }
        public void closeConn() {
            try {
                if (con.State == ConnectionState.Open)
                    con.Close();
            } catch (Exception e) {
                //throw new Exception("Failed to Open connection.", e);

            }
        }
    }
}
