namespace ItopVector.Core.Interface
{
    using ItopVector.Core;
    using System;
    using System.Xml;

	/// <summary>
	/// 容器接口
	/// </summary>
    public interface IContainer
    {
        // Methods
		/// <summary>
		/// 参数节点在当前容器类中是否有效
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
        bool IsValidChild(XmlNode element);


        // Properties
		/// <summary>
		/// 元件列表
		/// </summary>
        SvgElementCollection ChildList { get; }

		/// <summary>
		/// 是否展开
		/// </summary>
        bool Expand { get; set; }

    }
}

