    using ItopVector.Core;
    using ItopVector.Core.Document;
    using ItopVector.Core.Func;
    using ItopVector.Core.Types;
    using System;
namespace ItopVector.Core.Animate
{

    public class SetAnimate : Animate
    {
        // Methods
        internal SetAnimate(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
        {
        }

        public override object FT(float time, DomType domtype)
        {
            if (time < base.Begin)
            {
                return null;
            }
            if ((time < base.Begin) || (time > (base.Begin + base.Duration)))
            {
                return null;
            }
            string text1 = base.ToWhat;
            switch (domtype)
            {
                case DomType.SvgMatrix:
                {
                    return new Transf(this.Type + "(" + text1.Trim() + ")").Matrix;
                }
                case DomType.SvgNumber:
                {
                    return ItopVector.Core.Func.Number.parseToFloat(text1.Trim(), (SvgElement) this.ParentNode, ItopVector.Core.Func.SvgLengthDirection.Horizontal);
                }
                case DomType.SvgString:
                {
                    return text1.Trim();
                }
                case DomType.SvgColor:
                {
                    return text1.Trim();
                }
                case DomType.SvgPath:
                {
                    return PathFunc.PathDataParse(text1);
                }
                case DomType.SvgPoints:
                {
                    return PointsFunc.PointsParse(text1);
                }
            }
            return base.ToWhat;
        }

    }
}

