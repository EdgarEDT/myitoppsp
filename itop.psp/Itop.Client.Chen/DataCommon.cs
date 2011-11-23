using System;
using System.Collections.Generic;
using System.Text;
//using Microsoft.Office.Interop.Excel;
using System.Data;

namespace Itop.Client.Chen
{
    public class DataCommon
    {
        public System.Data.DataTable GetSortTable(System.Data.DataTable dt,string Column,bool bl)
        {
            string sort = " asc";
            if (!bl)
                sort = " desc";

            DataView dv = dt.DefaultView;
            dv.Sort = Column + sort;
            System.Data.DataTable dt2 = dv.ToTable();
            return dt2;
        }

        





    }
}
