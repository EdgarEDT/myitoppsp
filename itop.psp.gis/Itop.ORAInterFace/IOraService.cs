using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Itop.ORAInterFace
{
    public interface IOraService
    {
        DataSet GetDataSet(string sql, string cfg);
    }
}
