    using ItopVector.Core.Document;
    using ItopVector.Core.Func;
    using System;
namespace ItopVector.Core.Animate
{

    public class TransformAnimate : Animate
    {
        // Methods
        internal TransformAnimate(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
        {
        }


        // Properties
        public override string Type
        {
            get
            {
                return this.GetAttribute("type");
            }
            set
            {
                AttributeFunc.SetAttributeValue(this, "type", value);
            }
        }

    }
}

