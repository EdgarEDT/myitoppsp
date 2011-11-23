namespace ItopVector.Core.Func
{
    using System;

    public class DomTypeFunc
    {
        // Methods
        public DomTypeFunc()
        {
        }

        public static DomType GetTypeOfAttributeName(string attributename)
        {
            attributename = attributename.ToLower().Trim();
            switch (attributename)
            {
                case "fill":
                case "stroke":
                case "stop-color":
                {
                    return DomType.SvgColor;
                }
                case "transform":
                case "gradienttransform":
                case "patterntransform":
                {
                    return DomType.SvgMatrix;
                }
                case "x":
                case "x1":
                case "x2":
                case "y1":
                case "y2":
                case "fx":
                case "fy":
                case "r":
                case "opacity":
                case "fill-opacity":
                case "stroke-opacity":
                case "stop-opacity":
                case "stop-offset":
                case "offset":
                case "y":
                case "rx":
                case "ry":
                case "cx":
                case "cy":
                case "dy":
                case "dx":
                case "width":
                case "height":
                case "font-size":
                case "stroke-width":
                case "scale":
                {
                    return DomType.SvgNumber;
                }
                case "points":
                {
                    return DomType.SvgPoints;
                }
                case "d":
                case "path":
                {
                    return DomType.SvgPath;
                }
                case "begin":
                case "end":
                case "dur":
                {
                    return DomType.SvgTime;
                }
                case "xlink:href":
                case "href":
                {
                    return DomType.SvgLink;
                }
            }
            return DomType.SvgString;
        }

    }
}

