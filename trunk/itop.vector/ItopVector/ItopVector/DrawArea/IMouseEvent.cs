using System;

namespace ItopVector.DrawArea
{
	/// <summary>
	/// ͼԪ����¼��ӿ�
	/// </summary>
	public interface IMouseEvent
	{
		/// <summary>
		/// ����������
		/// </summary>
		event SvgElementEventHandler LeftClick;
		/// <summary>
		/// ��������Ҽ�
		/// </summary>
		event SvgElementEventHandler RightClick;
		/// <summary>
		/// ˫��������
		/// </summary>
		event SvgElementEventHandler DoubleLeftClick;
		/// <summary>
		/// ˫������Ҽ�
		/// </summary>
		event SvgElementEventHandler DoubleRightClick;
		/// <summary>
		/// �����ͼԪ���ƶ�
		/// </summary>
		event SvgElementEventHandler MoveOver;
		/// <summary>
		///  ������ͼԪ
		/// </summary>
		event SvgElementEventHandler MoveIn;
		/// <summary>
		///  ����Ƴ�ͼԪ
		/// </summary>
		event SvgElementEventHandler MoveOut;
	}
}
