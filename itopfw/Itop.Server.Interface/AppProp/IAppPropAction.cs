			
using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using Itop.Server.Interface.Login;

namespace Itop.Server.Interface.AppProp {
    public interface IAppPropAction {
        string GetAppProperty(string propName);
    }
}
