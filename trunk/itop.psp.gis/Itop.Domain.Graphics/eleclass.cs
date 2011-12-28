using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Domain.Graphics
{
    public struct eleclass
    {
        public string name;                  //记录元件的名称
        public string suid;               //记录元件的主健
        public string type;              //记录元件类型
        public bool selectflag;           //是否被选中

        public eleclass(string _name, string _suid, string _type, bool _selectflag)
        {

            name = _name;
            suid = _suid;
            type = _type;
            selectflag = _selectflag;
        }
    }
}
