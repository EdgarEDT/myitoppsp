using System;
using System.Collections.Generic;
using System.Text;

using Itop.Server.Interface.Login;

namespace Itop.Server.Interface.ObjProp {
    public interface IPropertyTreeAction {
        bool Save(UserInfo userInfo, PropertyTree tree);
    }
}
