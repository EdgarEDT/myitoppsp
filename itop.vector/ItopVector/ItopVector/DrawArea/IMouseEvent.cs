using System;

namespace ItopVector.DrawArea
{
	/// <summary>
	/// 图元鼠标事件接口
	/// </summary>
	public interface IMouseEvent
	{
		/// <summary>
		/// 单击鼠标左键
		/// </summary>
		event SvgElementEventHandler LeftClick;
		/// <summary>
		/// 单击鼠标右键
		/// </summary>
		event SvgElementEventHandler RightClick;
		/// <summary>
		/// 双击鼠标左键
		/// </summary>
		event SvgElementEventHandler DoubleLeftClick;
		/// <summary>
		/// 双击鼠标右键
		/// </summary>
		event SvgElementEventHandler DoubleRightClick;
		/// <summary>
		/// 鼠标在图元上移动
		/// </summary>
		event SvgElementEventHandler MoveOver;
		/// <summary>
		///  鼠标进入图元
		/// </summary>
		event SvgElementEventHandler MoveIn;
		/// <summary>
		///  鼠标移出图元
		/// </summary>
		event SvgElementEventHandler MoveOut;
	}
}
