using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Itop.Frame.Model;
using System.Collections;
using System.Data;

namespace Itop.WebFrame {
    public partial class webdesk : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!X.IsAjaxRequest) {
                createMenu();
            }
        }

        private void createMenu() {
            IList<sysprog> list = Global.SqlMapper.GetList<sysprog>(null);
            DataTable dt = ConvertHelper.ToDataTable((IList)list, typeof(sysprog));

            createDesk(dt.Select("iscore=100"));

            //menu

            createSubMenu(MyDesktop.StartMenu.Items, dt, "root");
        }

        private void createDesk(DataRow[] dataRow) {
            foreach (DataRow item in dataRow) {
                sysprog prog = ConvertHelper.RowToObject<sysprog>(item);
                MyDesktop.Shortcuts.Add(new DesktopShortcut() { ShortcutID = prog.id, Text = prog.ProgName, IconCls = "shortcut-icon icon-grid48" });

            }
        }

        private void createSubMenu(ItemsCollection<Component> itemsCollection, DataTable dt, string p) {
            DataRow[] rows = dt.Select("parentid='" + p + "'");
            foreach (DataRow row in rows) {
                sysprog prog = ConvertHelper.RowToObject<sysprog>(row);
                Ext.Net.MenuItem menu = new Ext.Net.MenuItem(prog.ProgName);
                if (prog.IsGroup == "1") {
                    menu.Icon = Icon.Folder;
                    createSubMenu(menu.Menu, dt, prog.id);
                } else {
                    menu.Icon = Icon.World;
                    //menu.ID = prog.id;
                    menu.Listeners.Click.Handler = "showmodule(#{MyDesktop},'" + prog.id + "');";
                }
                itemsCollection.Add(menu);
            }
        }

        private void createSubMenu(MenuCollection menuCollection, DataTable dt, string p) {
            DataRow[] rows = dt.Select("parentid='" + p + "'");
            if (rows == null || rows.Length == 0) return;
            Ext.Net.Menu menu = new Ext.Net.Menu();
            foreach (DataRow row in rows) {
                sysprog prog = ConvertHelper.RowToObject<sysprog>(row);
                Ext.Net.MenuItem item = new Ext.Net.MenuItem(prog.ProgName);
                if (prog.IsGroup == "1") {
                    item.Icon = Icon.Folder;
                    createSubMenu(item.Menu, dt, prog.id);
                } else {
                    item.Icon = Icon.ApplicationForm;
                    item.Listeners.Click.Handler = "showmodule(#{MyDesktop},'" + prog.id + "');";
                }
                menu.Add(item);
            }
            menuCollection.Add(menu);
        }
        private object[] TestData {
            get {
                DateTime now = DateTime.Now;

                return new object[]
            {
                new object[] { "3m Co", 71.72, 0.02, 0.03, now },
                new object[] { "Alcoa Inc", 29.01, 0.42, 1.47, now },
                new object[] { "Altria Group Inc", 83.81, 0.28, 0.34, now },
                new object[] { "American Express Company", 52.55, 0.01, 0.02, now },
                
            };
            }
        }

        protected void MyData_Refresh(object sender, StoreRefreshDataEventArgs e) {
            this.Store1.DataSource = this.TestData;
            this.Store1.DataBind();
        }

        protected void Logout_Click(object sender, DirectEventArgs e) {
            // Logout from Authenticated Session
            this.Response.Redirect("Default.aspx");
        }
        [DirectMethod(Namespace = "util", ShowMask = true)]
        public sysprog geturl(string id) {
            sysprog prog = Global.SqlMapper.GetOneByKey<sysprog>(id);

            string url = "#";
            if (prog != null)
                url = prog.ProgClass;

            return prog;
        }
        [DirectMethod]
        public Customer AddCustomer() {
            Customer customer = new Customer();

            customer.ID = 99;
            customer.FirstName = this.txtFirstName.Text;
            customer.LastName = this.txtLastName.Text;
            customer.Company = this.txtCompany.Text;
            customer.Country = new Country(this.cmbCountry.SelectedItem.Value);
            customer.Premium = this.chkPremium.Checked;
            customer.DateCreated = DateTime.Now;

            return customer;
        }

        // Define Customer Class
        public class Customer {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Company { get; set; }
            public Country Country { get; set; }
            public bool Premium { get; set; }
            public DateTime DateCreated { get; set; }
        }

        // Define Country Class
        public class Country {
            public Country(string name) {
                this.Name = name;
            }

            public string Name { get; set; }
        }

        protected void GetQuickSearchItems(object sender, StoreRefreshDataEventArgs e) {
            string filter = e.Parameters["Filter"];

            if (!string.IsNullOrEmpty(filter)) {
                QuickSearchStore.DataSource = new List<object>
              {
                  new { SearchItem = filter + " 1" },
                  new { SearchItem = filter + " 2" },
                  new { SearchItem = filter + " 3" },
                  new { SearchItem = filter + " 4" },
                  new { SearchItem = filter + " 5" },
                  new { SearchItem = filter + " 6" },
                  new { SearchItem = filter + " 7" },
                  new { SearchItem = filter + " 8" },
                  new { SearchItem = filter + " 9" },
                  new { SearchItem = filter + " 10" }
              };
            }

            QuickSearchStore.DataBind();
        }
    }
}
