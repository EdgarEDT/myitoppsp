using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Domain.Graphics
{
    public struct eleclass
    {
        public string name;                  //��¼Ԫ��������
        public string suid;               //��¼Ԫ��������
        public string type;              //��¼Ԫ������
        public bool selectflag;           //�Ƿ�ѡ��

        public eleclass(string _name, string _suid, string _type, bool _selectflag)
        {

            name = _name;
            suid = _suid;
            type = _type;
            selectflag = _selectflag;
        }
    }
}
