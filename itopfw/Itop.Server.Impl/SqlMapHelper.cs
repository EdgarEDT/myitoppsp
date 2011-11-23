using System;
using System.Collections.Generic;
using System.Text;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;

namespace Itop.Server.Impl {
    public class SqlMapHelper {
        
        private static ISqlMapper sqlMap;
        public static ISqlMapper DefaultSqlMap {
            get {
                if (sqlMap == null) {
                    DomSqlMapBuilder builder = new DomSqlMapBuilder();
                    sqlMap = builder.Configure("DataConfig\\sqlmap.config");
                    //sqlMap = Mapper.Instance();
                    //从程序集中加载
                    //Assembly assembly = Assembly.Load("Itop.RightManager.Service");
                    //Stream stream = assembly.GetManifestResourceStream("Itop.RightManager.Service.sqlmap.config");
                    

                    //sqlMap =builder.Configure(stream);
                }
                return sqlMap;
            }
           
        }
        /// <summary>
        /// 清除SqlMap对象
        /// </summary>
        public static void Reset() {
            try {
                sqlMap.CloseConnection();

            } 
            catch { }
            finally {
                sqlMap = null;
            }            
        }
    }
}
