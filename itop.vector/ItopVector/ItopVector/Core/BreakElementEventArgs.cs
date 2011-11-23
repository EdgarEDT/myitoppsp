using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using ItopVector.Core.Interface;


namespace ItopVector.Core
{
    public class BreakElementEventArgs:EventArgs
    {
        public BreakElementEventArgs(ISvgElement element)
        {
            svgelement = element;
            Cancel = false;
        }
        public ISvgElement SvgElement
        {
            get
            {
                return svgelement;
            }
            set
            {
                svgelement = value;
            }
        }
        private ISvgElement svgelement;
        public bool Cancel;
    }
}
