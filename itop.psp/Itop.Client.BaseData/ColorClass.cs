using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Itop.Domain.BaseDatas;
using Itop.Client.Common;

namespace Itop.Client.BaseData
{
    class ColorClass
    {
        public ColorClass()
        { }


        public Color GetColor(double value)
        {
            Color cl = new Color();
            BaseColor bc = (BaseColor)Services.BaseService.GetObject("SelectBaseColorByColor",value);
            cl= ColorTranslator.FromOle(bc.Color);
            return cl;
        }

    }
}
