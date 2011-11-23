				
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;


using Itop.Server.Interface.AppProp;
using Itop.Server.Interface.Login;
using Itop.Server.Impl.Login;
using Itop.Domain;

namespace Itop.Server.Impl.AppProp {
    public class AppPropAction: MarshalByRefObject, IAppPropAction {
        #region IAppPropAction Members

        public string GetAppProperty(string propName) {
            SAppProps prop = SqlMapHelper.DefaultSqlMap.QueryForObject<SAppProps>("SelectSAppPropsByPropName", propName);

            if (prop == null) return string.Empty;
            return prop.PropValue;
        }

        #endregion
    }
}
