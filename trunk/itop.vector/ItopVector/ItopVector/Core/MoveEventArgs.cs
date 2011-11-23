using System;
using System.Collections.Generic;
using System.Text;
using ItopVector.Core.Interface;
using System.Drawing;

namespace ItopVector.Core
{
    public class MoveEventArgs:EventArgs
    {
        public MoveEventArgs(ISvgElement element,PointF before,PointF after)
        {
            svgelement = element;
            beforeMove = before;
            afterMove = after;
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
        public PointF BeforeMove
        {
            get
            {
                return beforeMove;
            }
            set
            {
                beforeMove = value;
            }
        }
        public PointF AfterMove
        {
            get
            {
                return afterMove;
            }
            set
            {
                afterMove = value;
            }
        }
        private ISvgElement svgelement;
        public bool Cancel;
        private PointF beforeMove;
        private PointF afterMove;
    }
}
