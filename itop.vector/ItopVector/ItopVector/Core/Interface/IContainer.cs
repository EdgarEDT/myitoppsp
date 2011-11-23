namespace ItopVector.Core.Interface
{
    using ItopVector.Core;
    using System;
    using System.Xml;

	/// <summary>
	/// �����ӿ�
	/// </summary>
    public interface IContainer
    {
        // Methods
		/// <summary>
		/// �����ڵ��ڵ�ǰ���������Ƿ���Ч
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
        bool IsValidChild(XmlNode element);


        // Properties
		/// <summary>
		/// Ԫ���б�
		/// </summary>
        SvgElementCollection ChildList { get; }

		/// <summary>
		/// �Ƿ�չ��
		/// </summary>
        bool Expand { get; set; }

    }
}

