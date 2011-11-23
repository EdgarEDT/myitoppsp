using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.IO;
using System.Collections;
using System.Data.Common;

using IBatisNet.DataMapper.Configuration;
using IBatisNet.DataMapper;
using Itop.Server.Interface;
using Itop.Domain.RightManager;
using Itop.Server;

namespace Itop.Server.Impl {
    public class SmmuserService : MarshalByRefObject, ISmmuserService {
        Type t;
        public SmmuserService() {
            t = typeof(Smmuser);
        }
        #region IBaseService<Smmuser> ≥…‘±

        public IList<Smmuser> GetStrongList() {
            
            IList<Smmuser> list= SqlMapHelper.DefaultSqlMap.QueryForList<Smmuser>(string.Format("Select{0}List", t.Name), null);

            foreach (Smmuser user in list)
            {
                user.Password = MainDataModule.Decrypt(user.Password);
            }
            return list;
        }

        public IList GetList() {
           
            IList list = SqlMapHelper.DefaultSqlMap.QueryForList(string.Format("Select{0}List", t.Name), null);
            foreach(Smmuser user in list){
                user.Password = MainDataModule.Decrypt(user.Password);
            }
            return list;
        }

        public Smmuser GetOneByKey(int key) {

            Smmuser user = SqlMapHelper.DefaultSqlMap.QueryForObject<Smmuser>(string.Format("Select{0}ByKey", t.Name), key);
            if(user!=null)
                user.Password = MainDataModule.Decrypt(user.Password);

            return user;

        }

        public Smmuser GetOneByKey(string key) {

            Smmuser user = SqlMapHelper.DefaultSqlMap.QueryForObject<Smmuser>(string.Format("Select{0}ByKey", t.Name), key);
            if (user != null)
                user.Password = MainDataModule.Decrypt(user.Password);
            return user;
        }

        public Smmuser GetOneByKey( Smmuser data) {

            Smmuser user= SqlMapHelper.DefaultSqlMap.QueryForObject<Smmuser>(string.Format("Select{0}ByKey", t.Name), data);
            if (user != null)
                user.Password = MainDataModule.Decrypt(user.Password);
            return user;
        }

        public object Create( Smmuser data) {

            data.Password = MainDataModule.Encrypt(data.Password);
            return SqlMapHelper.DefaultSqlMap.Insert(string.Format("Insert{0}", t.Name), data);
        }

        public int Update(Smmuser data) {

            data.Password = MainDataModule.Encrypt(data.Password);
            return SqlMapHelper.DefaultSqlMap.Update(string.Format("Update{0}", t.Name), data);
            //System.Windows.Forms.MessageBox.Show(t.Name);
        }

        public string Update(IList list) {

            return string.Empty;
        }

        public int Delete( Smmuser data) {
            
            return SqlMapHelper.DefaultSqlMap.Delete(string.Format("Delete{0}", t.Name), data);
        }

        public int  DeleteByKey(int key) {
            
            return SqlMapHelper.DefaultSqlMap.Delete(string.Format("Delete{0}ByKey", t.Name), key);
        }

        public int DeleteByKey(string key) {
            
            return SqlMapHelper.DefaultSqlMap.Delete(string.Format("Delete{0}ByKey", t.Name), key);
        }

        public int GetRowCount( Smmuser data) {
           
            return (int)SqlMapHelper.DefaultSqlMap.QueryForObject(string.Format("Select{0}CountByObject", t.Name), data);
        }

        #endregion
    }
}
