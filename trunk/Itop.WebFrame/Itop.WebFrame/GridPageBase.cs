using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ext.Net;

namespace Itop.WebFrame {
    public class GridPageBase<T>:System.Web.UI.Page {
        protected virtual void Page_Load(object sender, EventArgs e) {
            if (!X.IsAjaxRequest) {
                InitGridColumn();
                BindData();
            }            
        }
        private static int curId = 7;
        private static object lockObj = new object();
        private Store Store1;
        protected string keyfield="id";
        private Type modeltype=typeof(T);
        private int NewId {
            get {
                return System.Threading.Interlocked.Increment(ref curId);
            }
        }
        protected virtual Store DataStore {
            get { return Store1; }
            set { if (value != Store1)Store1 = value; }
        }
        protected virtual List<T> CurrentData {
            get {
                var persons = this.Session[modeltype.Name];

                if (persons == null) {
                    persons = Global.SqlMapper.GetList<T>(null);
                    this.Session[modeltype.Name] = persons;
                }

                return (List<T>)persons;
            }
        }
        private string CreateID() {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff")+NewId;
        }
        private void setID(T person) {

        }
        private string getID(T person) {
            return modeltype.GetProperty(keyfield).GetValue(person, null).ToString();
        }
        protected virtual string AddPerson(T person) {
            lock (lockObj) {
                var persons = this.CurrentData;
                modeltype.GetProperty(keyfield).SetValue(person, CreateID(), null);
                persons.Add(person);
                this.Session[modeltype.Name] = persons;
                Global.SqlMapper.Create<T>(person);
                return getID(person);
            }
        }

        protected virtual void DeletePerson(string id) {
            lock (lockObj) {
                var persons = this.CurrentData;
                T person = default(T);
                
                foreach (T p in persons) {
                    if (getID(p) == id) {
                        person = p;
                        break;
                    }
                }

                if (person == null) {
                    throw new Exception("T not found");
                }

                persons.Remove(person);
                Global.SqlMapper.DeleteByKey<T>(person);
                this.Session[modeltype.Name] = persons;
            }
        }

        protected virtual void UpdatePerson(T person) {
            lock (lockObj) {
                var persons = this.CurrentData;
                T updatingPerson = default(T);
                string id = getID(person);
                foreach (T p in persons) {
                    if (getID(p) == id) {
                        updatingPerson = p;
                        break;
                    }
                }

                if (updatingPerson == null) {
                    throw new Exception("T not found");
                }
                ConvertHelper.CopyTo(person, updatingPerson);
                Global.SqlMapper.Update<T>(updatingPerson);
                this.Session[modeltype.Name] = persons;
            }
        }
        protected virtual void Data_Refresh(object sender, StoreRefreshDataEventArgs e) {
            BindData();
        }
        protected virtual void BindData() {
           
            this.Store1.DataSource = this.CurrentData;
            this.Store1.DataBind();
        }
        protected virtual void InitGridColumn() {

        }
        protected virtual void HandleChanges(object sender, BeforeStoreChangedEventArgs e) {
            ChangeRecords<T> persons = e.DataHandler.ObjectData<T>();

            foreach (T created in persons.Created) {
                string tempId = getID(created);
                string newId = this.AddPerson(created);

                if (Store1.UseIdConfirmation) {
                    e.ConfirmationList.ConfirmRecord(tempId.ToString(), newId.ToString());
                } else {
                    Store1.UpdateRecordId(tempId, newId);
                }

            }

            foreach (T deleted in persons.Deleted) {
                string id = getID(deleted);
                this.DeletePerson(id);

                if (Store1.UseIdConfirmation) {
                    e.ConfirmationList.ConfirmRecord(id);
                }
            }

            foreach (T updated in persons.Updated) {
                this.UpdatePerson(updated);

                if (Store1.UseIdConfirmation) {
                    e.ConfirmationList.ConfirmRecord(getID(updated));
                }
            }
            e.Cancel = true;
        }
    }
}
