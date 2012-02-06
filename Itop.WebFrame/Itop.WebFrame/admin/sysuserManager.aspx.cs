using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Itop.Frame.Model;

namespace Itop.WebFrame {
    public partial class sysuserManager : Page {
        protected void Page_Load(object sender, EventArgs e) {
            BindData();
        }

        private IList<sysuser> getUsers() {

            return Global.SqlMapper.GetList<sysuser>(null);
        }
        protected void MyData_Refresh(object sender, StoreRefreshDataEventArgs e) {
            this.Store1.DataSource = this.getUsers();
            this.Store1.DataBind();
        }
        [DirectMethod(Namespace = "sysuser", ShowMask = true)]
        public sysuser createUser() {

            return new sysuser() { UserName = "新用户", LoginID = "0000" };
        }
        private static int curId = 7;
        private static object lockObj = new object();

        private int NewId {
            get {
                return System.Threading.Interlocked.Increment(ref curId);
            }
        }

        protected virtual List<sysuser> CurrentData {
            get {
                var persons = this.Session["sysusers"];

                if (persons == null) {
                    persons = Global.SqlMapper.GetList<sysuser>(null);
                    this.Session["sysusers"] = persons;
                }

                return (List<sysuser>)persons;
            }
        }

        protected virtual string AddPerson(sysuser person) {
            lock (lockObj) {
                var persons = this.CurrentData;
                person.id = person.CreateID();
                persons.Add(person);
                this.Session["sysusers"] = persons;
                Global.SqlMapper.Create<sysuser>(person);
                return person.id;
            }
        }

        protected virtual void DeletePerson(string id) {
            lock (lockObj) {
                var persons = this.CurrentData;
                sysuser person = null;

                foreach (sysuser p in persons) {
                    if (p.id == id) {
                        person = p;
                        break;
                    }
                }

                if (person == null) {
                    throw new Exception("sysuser not found");
                }

                persons.Remove(person);
                Global.SqlMapper.DeleteByKey<sysuser>(person.id);
                this.Session["sysusers"] = persons;
            }
        }

        protected virtual void UpdatePerson(sysuser person) {
            lock (lockObj) {
                var persons = this.CurrentData;
                sysuser updatingPerson = null;

                foreach (sysuser p in persons) {
                    if (p.id == person.id) {
                        updatingPerson = p;
                        break;
                    }
                }

                if (updatingPerson == null) {
                    throw new Exception("sysuser not found");
                }
                ConvertHelper.CopyTo(person, updatingPerson);
                Global.SqlMapper.Update<sysuser>(updatingPerson);
                this.Session["sysusers"] = persons;
            }
        }

        protected virtual void BindData() {
            if (X.IsAjaxRequest) {
                return;
            }

            this.Store1.DataSource = this.CurrentData;
            this.Store1.DataBind();
        }

        protected virtual void HandleChanges(object sender, BeforeStoreChangedEventArgs e) {
            ChangeRecords<sysuser> persons = e.DataHandler.ObjectData<sysuser>();

            foreach (sysuser created in persons.Created) {
                string tempId = created.id;
                string newId = this.AddPerson(created);

                if (Store1.UseIdConfirmation) {
                    e.ConfirmationList.ConfirmRecord(tempId.ToString(), newId.ToString());
                } else {
                    Store1.UpdateRecordId(tempId, newId);
                }

            }

            foreach (sysuser deleted in persons.Deleted) {
                this.DeletePerson(deleted.id);

                if (Store1.UseIdConfirmation) {
                    e.ConfirmationList.ConfirmRecord(deleted.id);
                }
            }

            foreach (sysuser updated in persons.Updated) {
                this.UpdatePerson(updated);

                if (Store1.UseIdConfirmation) {
                    e.ConfirmationList.ConfirmRecord(updated.id);
                }
            }
            e.Cancel = true;
        }
    }
}
