#region Application Builder for C# License
// 
// Licensed under the terms of the "BSD License"
//
// Copyright (c) 2006, Rabbit (zcs602@163.com)
// All rights reserved.
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
// * Redistributions of source code must retain the above copyright notice, 
//   this list of conditions and the following disclaimer. 
// * Redistributions in binary form must reproduce the above copyright notice, 
//   this list of conditions and the following disclaimer in the documentation 
//   and/or other materials provided with the distribution. 
// * Neither the name of the ABC(Application Builder for C#) nor the names of its contributors 
//   may be used to endorse or promote products derived from this software 
//   without specific prior written permission. 
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
// IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, 
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, 
// PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT 
// LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF 
// THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
#endregion
				
				
using System;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace Itop.Client
{
    public partial class SQLDMOHelper
    {
        string ServerName = "";
        string UserName = "";
        string Password = "";
        /// <summary>
        /// 获取指定数据库服务器的数据库列表 
        /// </summary>
        /// <param name="strServerName"></param>
        /// <param name="strUserName"></param>
        /// <param name="strPwd"></param>
        /// <returns></returns>
        /// 
        #region 构造函数
        public SQLDMOHelper(string strServerName)
        {
            ServerName = strServerName;
            InitData(strServerName);
        }


        private void InitData(string strServerName)
        {
            DataSet ds = new DataSet();
            try
            {
                ds.ReadXml(Application.StartupPath + "\\CONFIG.XML");
            }
            catch
            {
                return;
            }
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["Server"].ToString() == strServerName)
                {
                    UserName = dr["UserID"].ToString();
                    Password = dr["PassWord"].ToString();
                }
            }
        }
        #endregion


        #region 返回数据库列表
        public ArrayList GetDbList()
        {
            ArrayList alDbs = new ArrayList();
            SQLDMO.Application sqlApp = new SQLDMO.ApplicationClass();
            SQLDMO.SQLServer svr = new SQLDMO.SQLServerClass();
            try
            {
                svr.Connect(ServerName, UserName, Password);
                foreach (SQLDMO.Database db in svr.Databases)
                {
                    if (db.Name != null)
                        alDbs.Add(db.Name);
                }
            }
            catch (Exception e)
            {
                throw (new Exception("连接数据库出错：" + e.Message));
            }
            finally
            {
                svr.DisConnect();
                sqlApp.Quit();
            }
            return alDbs;
        }
#endregion


        #region 返回字段表
        public DataTable GetColumns(string DatabaseName, string TableName)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ColumnName", typeof(string));
            dt.Columns.Add("ColumnType", typeof(string));
            dt.Columns.Add("ColumnSize", typeof(int));
            dt.Columns.Add("ColumnKey", typeof(bool));
            dt.Columns.Add("ColumnNull", typeof(bool));
            dt.Columns.Add("ColumnID", typeof(int));
            dt.Columns.Add("ColumnRemark", typeof(string));

            SQLDMO.Application sqlApp = new SQLDMO.ApplicationClass();
            SQLDMO.SQLServer2 svr = new SQLDMO.SQLServer2Class();

            try
            {
                svr.Connect(ServerName, UserName, Password);
                SQLDMO.Database2 myDb = new SQLDMO.Database2Class();
                myDb = (SQLDMO.Database2)svr.Databases.Item(DatabaseName, "owner");
                SQLDMO.Table2 myTb = new SQLDMO.Table2Class();

                foreach (SQLDMO.Table2 tb in myDb.Tables)
                {
                    if (tb.Name == TableName)
                        myTb = tb;
                }
                foreach (SQLDMO.Column2 column in myTb.Columns)
                {
                    DataRow dr = dt.NewRow();
                    dr["ColumnName"] = column.Name;
                    dr["ColumnType"] = column.Datatype;
                    dr["ColumnSize"] = column.Length;
                    dr["ColumnKey"] = column.InPrimaryKey;
                    dr["ColumnNull"] = column.AllowNulls;
                    dr["ColumnID"] = column.ID;
                    dr["ColumnRemark"] = column.Properties.Application.ODBCVersionString;
                    
                    
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception e)
            {
                //throw (new Exception("连接数据库出错：" + e.Message));
                ShowError("连接数据库出错：" + e.Message);
                return null;
            }
            finally
            {
                svr.DisConnect();
                sqlApp.Quit();
            }
            return dt;
        }








        //////// public DataTable GetColumns(string DatabaseName, string TableName)
        ////////{
        ////////    DataTable dt = new DataTable();
        ////////    dt.Columns.Add("ColumnName", typeof(string));
        ////////    dt.Columns.Add("ColumnType", typeof(string));
        ////////    dt.Columns.Add("ColumnSize", typeof(int));
        ////////    dt.Columns.Add("ColumnKey", typeof(bool));
        ////////    dt.Columns.Add("ColumnNull", typeof(bool));
        ////////    dt.Columns.Add("ColumnID", typeof(int));
        ////////    dt.Columns.Add("ColumnRemark", typeof(string));

        ////////    SQLDMO.Application sqlApp = new SQLDMO.ApplicationClass();
        ////////    SQLDMO.SQLServer svr = new SQLDMO.SQLServerClass();

        ////////    try
        ////////    {
        ////////        svr.Connect(ServerName, UserName, Password);
        ////////        SQLDMO.Database myDb = new SQLDMO.DatabaseClass();
        ////////        myDb = (SQLDMO.Database)svr.Databases.Item(DatabaseName, "owner");
        ////////        SQLDMO.Table myTb = new SQLDMO.TableClass();

        ////////        foreach (SQLDMO.Table tb in myDb.Tables)
        ////////        {
        ////////            if (tb.Name == TableName)
        ////////                myTb = tb;
        ////////        }
        ////////        foreach (SQLDMO.Column column in myTb.Columns)
        ////////        {
        ////////            DataRow dr = dt.NewRow();
        ////////            dr["ColumnName"] = column.Name;
        ////////            dr["ColumnType"] = column.Datatype;
        ////////            dr["ColumnSize"] = column.Length;
        ////////            dr["ColumnKey"] = column.InPrimaryKey;
        ////////            dr["ColumnNull"] = column.AllowNulls;
        ////////            dr["ColumnID"] = column.ID;
        ////////            dr["ColumnRemark"] = column.;
                    
                    
        ////////            dt.Rows.Add(dr);
        ////////        }
        ////////    }
        ////////    catch (Exception e)
        ////////    {
        ////////        //throw (new Exception("连接数据库出错：" + e.Message));
        ////////        ShowError("连接数据库出错：" + e.Message);
        ////////        return null;
        ////////    }
        ////////    finally
        ////////    {
        ////////        svr.DisConnect();
        ////////        sqlApp.Quit();
        ////////    }
        ////////    return dt;
        ////////}
        #endregion 


        #region 返回表别表
        public ArrayList GetTables(string DatabaseName)
        {
            ArrayList alDbs = new ArrayList();
            SQLDMO.Application sqlApp = new SQLDMO.ApplicationClass();
            SQLDMO.SQLServer svr = new SQLDMO.SQLServerClass();
            
            try
            {
                svr.Connect(ServerName, UserName, Password);
                SQLDMO.Database mydb = new SQLDMO.DatabaseClass();
                mydb = (SQLDMO.Database)svr.Databases.Item(DatabaseName, "owner");

                foreach (SQLDMO.Table db in mydb.Tables)
                {
                    if (db.Name != null)
                        alDbs.Add(db.Name);
                }
            }
            catch (Exception e)
            {
                //throw (new Exception("连接数据库出错：" + e.Message));
                ShowError("连接数据库出错：" + e.Message);
                return null;
            }
            finally
            {
                svr.DisConnect();
                sqlApp.Quit();
            }    
            return alDbs;
        }
        #endregion 


        #region 返回存储过程列表
        public ArrayList GetStored(string DatabaseName)
        {
            ArrayList alDbs = new ArrayList();
            SQLDMO.Application sqlApp = new SQLDMO.ApplicationClass();
            SQLDMO.SQLServer svr = new SQLDMO.SQLServerClass();

            try
            {
                svr.Connect(ServerName, UserName, Password);
                SQLDMO.Database mydb = new SQLDMO.DatabaseClass();
                mydb = (SQLDMO.Database)svr.Databases.Item(DatabaseName, "owner");

                foreach (SQLDMO.StoredProcedure db in mydb.StoredProcedures)
                {
                    if (db.Name != null)
                        alDbs.Add(db.Name);
                }
            }
            catch (Exception e)
            {
                //throw (new Exception("连接数据库出错：" + e.Message));
                ShowError("连接数据库出错：" + e.Message);
                return null;
            }
            finally
            {
                svr.DisConnect();
                sqlApp.Quit();
            }
            return alDbs;
        }

        #endregion


        #region 备份数据库

        /// <summary>
        /// 数据库的备份和实时进度显示 
        /// </summary>
        /// <param name="strDbName"></param>
        /// <param name="strFileName"></param>
        /// <param name="pgbMain"></param>
        /// <returns></returns>
        public bool BackUPDB(string strDbName, string strFileName)
        {
            SQLDMO.SQLServer svr = new SQLDMO.SQLServerClass();
            try
            {
                svr.Connect(ServerName, UserName, Password);
                SQLDMO.Backup bak = new SQLDMO.BackupClass();
                bak.Action = SQLDMO.SQLDMO_BACKUP_TYPE.SQLDMOBackup_Database;
                bak.Initialize = true;
                bak.Files = strFileName;
                bak.Database = strDbName;
                bak.SQLBackup(svr);
                return true;
            }
            catch (Exception err)
            {
                //throw (new Exception("备份数据库失败" + err.Message));
                ShowError("连接数据库出错：" + err.Message);
                return false;
            }
            finally
            {
                svr.DisConnect();
            }
        }
        #endregion


        #region 还原数据库
        /// <summary>
        /// 恢复数据库，恢复前杀死所有与本数据库相关进程
        /// </summary>
        /// <param name="strDbName">数据库名</param>
        /// <param name="strFileName">存放路径</param>
        /// <param name="pgbMain"></param>
        /// <returns></returns>
        public bool RestoreDB(string strDbName, string strFileName)
        {
            SQLDMO.SQLServer svr = new SQLDMO.SQLServerClass();
            try
            {
                svr.Connect(ServerName, UserName, Password);
                //取得所有的进程列表
                SQLDMO.QueryResults qr = svr.EnumProcesses(-1);
                int iColPIDNum = -1;
                int iColDbName = -1;
                //找到和要恢复数据库相关的进程
                for (int i = 1; i <= qr.Columns; i++)
                {
                    string strName = qr.get_ColumnName(i);
                    if (strName.ToUpper().Trim() == "SPID")
                    {
                        iColPIDNum = i;
                    }
                    else if (strName.ToUpper().Trim() == "DBNAME")
                    {
                        iColDbName = i;
                    }
                    if (iColPIDNum != -1 && iColDbName != -1)
                        break;
                }
                //将相关进程杀死
                for (int i = 1; i <= qr.Rows; i++)
                {
                    int lPID = qr.GetColumnLong(i, iColPIDNum);
                    string strDBName = qr.GetColumnString(i, iColDbName);
                    if (strDBName.ToUpper() == strDbName.ToUpper())
                        svr.KillProcess(lPID);
                }

                SQLDMO.Restore res = new SQLDMO.RestoreClass();

                res.Action = SQLDMO.SQLDMO_RESTORE_TYPE.SQLDMORestore_Database;
                res.Files = strFileName;

                res.Database = strDbName;
                res.FileNumber = 1;
                
                res.ReplaceDatabase = true;
                res.SQLRestore(svr);
                
                return true;
            }
            catch (Exception err)
            {
                //throw (new Exception("" + err.Message));
                ShowError("恢复数据库失败,请关闭所有和该数据库连接的程序！" + err.Message);
                return false;
            }
            finally
            {
                svr.DisConnect();
            }
        }
        #endregion


        #region 附加数据库
        public bool AttachDB(string dbName, string dbFile)
        {
            SQLDMO.SQLServer svr = new SQLDMO.SQLServerClass();
            try
            {
                svr.Connect(ServerName, UserName, Password);
                svr.AttachDB(dbName, dbFile);
                return true;
            }
            catch (Exception err)
            {
                //throw (new Exception("！" + err.Message));
                ShowError("附加数据库失败" + err.Message);
                return false;
            }
            finally
            {
                svr.DisConnect();
            }
        }
        #endregion


        #region 创建库

        public bool CreateDB(string dbName, string path)
        {
            // 创建数据库文件
            SQLDMO.SQLServer svr = new SQLDMO.SQLServerClass();
            SQLDMO.DBFile dbFile = new SQLDMO.DBFileClass();
            try
            {
                svr.Connect(ServerName, UserName, Password);
                dbFile.Name = dbName + "_Data";
                dbFile.PhysicalName = Path.Combine(path, dbName + "_Data.MDF");
                dbFile.PrimaryFile = true;
                //dbFile.Size = 2; // 设置初始化大小(MB)
                //dbFile.FileGrowthType = SQLDMO_GROWTH_TYPE.SQLDMOGrowth_MB; // 设置文件增长方式
                //dbFile.FileGrowth=1; // 设置增长幅度

                // 创建日志文件
                SQLDMO._LogFile logFile = new SQLDMO.LogFileClass();
                logFile.Name = dbName + "_Log";
                logFile.PhysicalName = Path.Combine(path, dbName + "_Log.MDF");
                //logFile.Size = 3;
                //logFile.FileGrowthType=SQLDMO_GROWTH_TYPE.SQLDMOGrowth_MB;
                //logFile.FileGrowth=1;

                // 创建数据库
                SQLDMO.Database db = new SQLDMO.DatabaseClass();
                db.Name = dbName;
                db.FileGroups.Item("PRIMARY").DBFiles.Add(dbFile);
                db.TransactionLog.LogFiles.Add(logFile);

                // 建立数据库联接，并添加数据库到服务器
                svr.Databases.Add(db);
                return true;
            }
            catch (Exception err)
            {
                //throw (new Exception("！" + err.Message));
                ShowError("添加数据库失败!" + err.Message);
                return false;
            }
            finally
            {
                svr.DisConnect();
            }
        }
        #endregion


        #region 删除数据库
        public bool KillDB(string dbName)
        {
            SQLDMO.SQLServer svr = new SQLDMO.SQLServerClass();
            try
            {
                svr.Connect(ServerName, UserName, Password);
                svr.KillDatabase(dbName);
                return true;
            }
            catch (Exception err)
            {
                //throw (new Exception("！" + err.Message));
                ShowError("删除数据库失败!" + err.Message);
                return false;
            }
            finally
            {
                svr.DisConnect();
            }
        }
        #endregion


        #region 删除表

        public bool KillTable(string DataBaseName,string tbName)
        {
            SQLDMO.SQLServer svr = new SQLDMO.SQLServerClass();    
            try
            {
                
                svr.Connect(ServerName, UserName, Password);
                svr.Databases.Item(DataBaseName, "owner").Tables.Remove(tbName, "owner");
                return true;
            }
            catch (Exception err)
            {
                //throw (new Exception("！" + err.Message));
                ShowError("删除表失败!" + err.Message);
                return false;
            }
            finally
            {
                svr.DisConnect();
            }
        }
        #endregion


        #region 删除存储过程
        public bool KillStored(string DataBaseName, string tbName)
        {
            SQLDMO.SQLServer svr = new SQLDMO.SQLServerClass();
            try
            {

                svr.Connect(ServerName, UserName, Password);
                svr.Databases.Item(DataBaseName, "owner").StoredProcedures.Remove(tbName, "owner");
                return true;
            }
            catch (Exception err)
            {
                //throw (new Exception("！" + err.Message));
                ShowError("删除存储过程失败!" + err.Message);
                return false;
            }
            finally
            {
                svr.DisConnect();
            }
        }
        #endregion


        #region 添加存储过程
        public bool UpdateStored(string DataBaseName, string StoredName,string StoredText)
        {
            SQLDMO.SQLServer svr = new SQLDMO.SQLServerClass();
            try
            {

                svr.Connect(ServerName, UserName, Password);
                if (StoredName == "")
                {
                    SQLDMO.StoredProcedure spd = new SQLDMO.StoredProcedureClass();
                    spd.Text = StoredText;
                    svr.Databases.Item(DataBaseName, "owner").StoredProcedures.Add(spd);
                }
                else
                {
                    SQLDMO.Database dbs = new SQLDMO.DatabaseClass();
                    SQLDMO.StoredProcedure spd = new SQLDMO.StoredProcedureClass();
                    dbs = (SQLDMO.Database)svr.Databases.Item(DataBaseName, "owner");
                    foreach (SQLDMO.StoredProcedure sp in dbs.StoredProcedures)
                    {
                        if (sp.Name == StoredName)
                            spd = sp;
                    }
                    spd.Alter(StoredText);
                 }
                    return true;
            }
            catch (Exception err)
            {
                //throw (new Exception("！" + err.Message));
                ShowError("修改存储过程失败!" + err.Message);
                return false;
            }
            finally
            {
                svr.DisConnect();
            }
        }
        #endregion


        #region 返回存储过程
        public string GetStoredText(string DataBaseName, string StoredName)
        {
            string getStoredText = "";
            SQLDMO.SQLServer svr = new SQLDMO.SQLServerClass();
            try
            {
                svr.Connect(ServerName, UserName, Password);
                SQLDMO.Database dbs = new SQLDMO.DatabaseClass();
                dbs = (SQLDMO.Database)svr.Databases.Item(DataBaseName, "owner");

                foreach (SQLDMO.StoredProcedure tb in dbs.StoredProcedures)
                {
                    if (tb.Name == StoredName)
                        getStoredText= tb.Text;
                }
            }
            catch (Exception err)
            {
                //throw (new Exception("！" + err.Message));
                ShowError("修改存储过程失败!" + err.Message);
                getStoredText= "";
            }
            finally
            {
                svr.DisConnect();
            }
            return getStoredText;
        }
        #endregion


        #region 添加数据表
        public bool InsertTable(string DatabaseName, string TableName,DataTable dt)
        {
            SQLDMO.Application sqlApp = new SQLDMO.ApplicationClass();
            SQLDMO.SQLServer svr = new SQLDMO.SQLServerClass();
            string keyName = "";
            try
            {
                svr.Connect(ServerName, UserName, Password);
                SQLDMO.Database myDb = new SQLDMO.DatabaseClass();
                myDb = (SQLDMO.Database)svr.Databases.Item(DatabaseName, "owner");

                SQLDMO.Table myTb = new SQLDMO.TableClass();
                myTb.Name = TableName;
                foreach (DataRow dr in dt.Rows)
                {
                    SQLDMO.Column column = new SQLDMO.ColumnClass();
                    column.Name = dr["ColumnName"].ToString();
                    column.Datatype = dr["ColumnType"].ToString();
                    column.Length = int.Parse(dr["ColumnSize"].ToString());

                    if (bool.Parse(dr["ColumnKey"].ToString()))
                    {
                        keyName = column.Name;
                    }
                    column.AllowNulls = bool.Parse(dr["ColumnNull"].ToString());
                    myTb.Columns.Add(column);
                }

                if (keyName != "")
                {
                    SQLDMO.Key key = new SQLDMO.KeyClass();
                    key.Name = keyName;
                    key.Type = SQLDMO.SQLDMO_KEY_TYPE.SQLDMOKey_Primary;
                    key.KeyColumns.Add(keyName);
                    myTb.Keys.Add(key);
                }
                myDb.Tables.Add(myTb);
                
                return true;
            }
            catch (Exception e)
            {
                //throw (new Exception("连接数据库出错：" + e.Message));
                ShowError("添加数据库失败!" + e.Message);
                return false;
            }
            finally
            {
                svr.DisConnect();
                sqlApp.Quit();
            }
        }
        #endregion 



        #region 更新数据表
        public bool UpdateTable(string DatabaseName, string TableName, DataTable dt)
        {
            SQLDMO.Application sqlApp = new SQLDMO.ApplicationClass();
            SQLDMO.SQLServer2 svr = new SQLDMO.SQLServer2Class();
            Hashtable ht1 = new Hashtable();
            Hashtable ht2 = new Hashtable();
            Hashtable ht3 = new Hashtable();
            string keyname1 = "";
            string keyname2 = "";

            try
            {
                svr.Connect(ServerName, UserName, Password);
                SQLDMO.Database2 myDb = new SQLDMO.Database2Class();
                myDb = (SQLDMO.Database2)svr.Databases.Item(DatabaseName, "owner");
                SQLDMO.Table2 myTb = new SQLDMO.Table2Class();

                foreach (SQLDMO.Table2 tb in myDb.Tables)
                {
                    if (tb.Name == TableName)
                        myTb = tb;
                }
                foreach (SQLDMO.Column2 column in myTb.Columns)
                {
                    ht1.Add(column.ID, column);
                    if (column.InPrimaryKey)
                        keyname1 = column.Name;
                }

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ColumnID"] != DBNull.Value)
                    {
                        ht2.Add(dr["ColumnID"].ToString(), dr);
                    }
                    else
                    {
                        ht3.Add("1", dr);
                    }
                    if (bool.Parse(dr["ColumnKey"].ToString()))
                        keyname2 = dr["ColumnName"].ToString();
                }


                foreach (DictionaryEntry de3 in ht3)
                {
                    DataRow dr3 = (DataRow)de3.Value;
                    SQLDMO.Column2 column = new SQLDMO.Column2Class();
                    column.Name = dr3["ColumnName"].ToString();
                    column.Datatype = dr3["ColumnType"].ToString();
                    column.Length = int.Parse(dr3["ColumnSize"].ToString());
                    column.AllowNulls = bool.Parse(dr3["ColumnNull"].ToString());
                    myTb.Columns.Add(column);                 
                }



                foreach (DictionaryEntry de1 in ht1)
                {
                    if (ht2.Contains(de1.Key.ToString()))
                    {
                        DataRow dr = (DataRow)ht2[de1.Key.ToString()];
                        SQLDMO.Column2 co = (SQLDMO.Column2)de1.Value;
                        SQLDMO.Column2 cm = (SQLDMO.Column2)myTb.Columns.Item(co.Name);


                        if (dr["ColumnType"].ToString() == "image" || dr["ColumnType"].ToString() == "text" || dr["ColumnType"].ToString() == "timestamp")
                            continue;


                        if (dr["ColumnName"].ToString() == cm.Name && dr["ColumnType"].ToString() == cm.Datatype && int.Parse(dr["ColumnSize"].ToString()) == cm.Length && bool.Parse(dr["ColumnNull"].ToString()) == cm.AllowNulls)
                            continue;



                        if (dr["ColumnName"].ToString() != cm.Name)
                        {
                            cm.Name = dr["ColumnName"].ToString();
                        }
                        if (dr["ColumnType"].ToString() != cm.Datatype)
                        {
                            switch (dr["ColumnType"].ToString())
                            {
                                case "bigint":
                                    cm.AlterDataType(dr["ColumnType"].ToString(), -1, -1, -1);
                                    break;
                                case "binary":
                                    cm.AlterDataType(dr["ColumnType"].ToString(), int.Parse(dr["ColumnSize"].ToString()), 0, 0);
                                    break;
                                case "bit":
                                    cm.AlterDataType(dr["ColumnType"].ToString(), -1, -1, -1);
                                    break;
                                case "char":
                                    cm.AlterDataType(dr["ColumnType"].ToString(), int.Parse(dr["ColumnSize"].ToString()), 0, 0);
                                    break;
                                case "datetime":
                                    cm.AlterDataType(dr["ColumnType"].ToString(), -1, -1, -1);
                                    break;
                                case "decimal":
                                    cm.AlterDataType(dr["ColumnType"].ToString(), -1, -1, -1);
                                    break;
                                case "float":
                                    cm.AlterDataType(dr["ColumnType"].ToString(), -1, -1, -1);
                                    break;
                                case "image":
                                    cm.AlterDataType(dr["ColumnType"].ToString(), -1, -1, -1);
                                    break;
                                case "int":
                                    cm.AlterDataType(dr["ColumnType"].ToString(), -1, -1, -1);
                                    break;
                                case "money":
                                    cm.AlterDataType(dr["ColumnType"].ToString(), -1, -1, -1);
                                    break;
                                case "nchar":
                                    cm.AlterDataType(dr["ColumnType"].ToString(), int.Parse(dr["ColumnSize"].ToString()), 0, 0);
                                    break;
                                case "ntext":
                                    cm.AlterDataType(dr["ColumnType"].ToString(), -1, -1, -1);
                                    break;
                                case "numeric":
                                    cm.AlterDataType(dr["ColumnType"].ToString(), int.Parse(dr["ColumnSize"].ToString()), 9, 3);
                                    break;
                                case "nvarchar":
                                    cm.AlterDataType(dr["ColumnType"].ToString(), int.Parse(dr["ColumnSize"].ToString()), 0, 0);
                                    break;
                                case "real":
                                    cm.AlterDataType(dr["ColumnType"].ToString(), -1, -1, -1);
                                    break;
                                case "text":
                                    cm.AlterDataType(dr["ColumnType"].ToString(), -1, -1, -1);
                                    break;
                                case "timestamp":
                                    cm.AlterDataType(dr["ColumnType"].ToString(), -1, -1, -1);
                                    break;
                                case "varchar":
                                    cm.AlterDataType(dr["ColumnType"].ToString(), int.Parse(dr["ColumnSize"].ToString()), 0, 0);
                                    break;
                            }
                        }
                        if (dr["ColumnType"].ToString() == "text" || dr["ColumnType"].ToString() == "image" || dr["ColumnType"].ToString() == "timestamp")
                        {
                            continue;
                        }
                        else
                        {
                            cm.AllowNulls = bool.Parse(dr["ColumnNull"].ToString());
                        }
                    }
                    else
                    {
                        SQLDMO.Column2 cm1=new SQLDMO.Column2Class();
                        cm1=(SQLDMO.Column2)de1.Value;
                        myTb.Columns.Remove(cm1.Name);               
                    }
                }




                if (keyname1 != keyname2)
                {
                    if(keyname1!="")
                    myTb.Keys.Remove(keyname1);           
                }
                if (keyname2 != "")
                {
                    SQLDMO.Key key = new SQLDMO.KeyClass();
                    key.Name = keyname2;
                    key.Type = SQLDMO.SQLDMO_KEY_TYPE.SQLDMOKey_Primary;
                    key.KeyColumns.Add(keyname2);
                    myTb.Keys.Add(key);           
                }
                return true;
            }
            catch (Exception e)
            {
                //throw (new Exception("连接数据库出错：" + e.Message));
                ShowError("添加数据库失败!" + e.Message);
                return false;
            }
            finally
            {
                svr.DisConnect();
                sqlApp.Quit();
            }
        }
        #endregion 


        private void ShowError(string err)
        {
            MessageBox.Show(err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }





         ////////////switch (dr["ColumnType"].ToString())
         ////////////           {
         ////////////               case "bigint":
                            
         ////////////                   break;
         ////////////               case "binary":

         ////////////                   break;
         ////////////               case "bit":
                            
         ////////////                   break;
         ////////////               case "char":

         ////////////                   break;
         ////////////               case "datetime":

         ////////////                   break;
         ////////////               case "decimal":

         ////////////                   break;
         ////////////               case "float":

         ////////////                   break;
         ////////////               case "image":

         ////////////                   break;
         ////////////               case "int":

         ////////////                   break;
         ////////////               case "money":

         ////////////                   break;
         ////////////               case "nchar":

         ////////////                   break;
         ////////////               case "ntext":

         ////////////                   break;
         ////////////               case "numeric":

         ////////////                   break;
         ////////////               case "nvarchar":

         ////////////                   break;
         ////////////               case "real":

         ////////////                   break;
         ////////////               case "text":

         ////////////                   break;
         ////////////               case "timestamp":

         ////////////                   break;
         ////////////               case "varchar":

         ////////////                   break;
         ////////////           }
    }
}