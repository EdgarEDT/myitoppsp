
using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.Common;

//using Microsoft.Practices.EnterpriseLibrary.Data;
//using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;

namespace Itop.Server {
    /// <summary>
    /// 主数据模块
    /// </summary>
    static public class MainDataModule {
        /// <summary>
        /// 获得数据库连接
        /// </summary>
        /// <returns>数据库连接对象</returns>
        //static public Database CreateDatabase() {
        //    return DatabaseFactory.CreateDatabase("ABC_DB_Connection");
        //}

        /// <summary>
        /// 获得系统参数
        /// </summary>
        /// <param name="propName">参数名称</param>
        /// <returns>参数值</returns>
        static private string GetApplicationProperty(string propName) {
            throw new Exception("方法没有实现！");
        }

        private const string ApplicationCaptionPropName = "AppCaption";

        /// <summary>
        /// 应用程序的标题
        /// </summary>
        static public string ApplicationCaption {
            get { return "智高点管理系统2011"; }
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(string plaintext) {
            //return plaintext;

            if (string.IsNullOrEmpty(plaintext)){
                return string.Empty;
            }
            return new EncryptString().Encrypt3DES(plaintext, "Itop");
            //return Cryptographer.EncryptSymmetric("Itop", plaintext);
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <returns>明文</returns>
        public static string Decrypt(string ciphertext) {
            //return ciphertext;

            if (string.IsNullOrEmpty(ciphertext)) {
                return string.Empty;
            }
            return new EncryptString().Decrypt3DES(ciphertext, "Itop");
            //return Cryptographer.DecryptSymmetric("Itop", ciphertext);
        }
    }
}
