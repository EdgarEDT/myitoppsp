using System;
using System.Collections.Generic;
using System.Text;
/*
 这个类是用来保存公共调用的方法
 */
namespace Itop.VogliteVillageSheets.Function
{
    class PublicFunction
    {
        /// <summary>
        /// 用来全部锁住工作簿的单元格
        /// </summary>
        /// <param name="obj"></param>
        public void LockSheets(FarPoint.Win.Spread.SheetView obj)
        {
            for(int j=0;j<obj.ColumnCount;++j)
            {
                obj.Columns[j].Locked = true;
            }
        }
    }
}
