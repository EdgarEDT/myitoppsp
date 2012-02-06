using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Itop.Frame.Model;

namespace Itop.WebFrame.admin {
    public partial class WebUCsysgroup : WebUserControlBase<sysgroup> {
        protected override void Page_Load(object sender, EventArgs e) {
            base.DataStore = sysgroupStore;
            base.Page_Load(sender, e);
        }
        
    }
}