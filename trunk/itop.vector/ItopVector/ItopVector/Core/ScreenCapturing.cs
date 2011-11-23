using System;
using System.Drawing;
using ItopVector.Core.Win32;

namespace ItopVector.Core
{
	/// <summary>
	/// ScreenCapturing 的摘要说明。
	/// </summary>
	public class ScreenCapturing
	{
		public ScreenCapturing()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public static Bitmap CaptureDesktopWindow()
		{			
			//创建屏幕句柄
			Graphics grpWindow = Graphics.FromHwnd(IntPtr.Zero);

			//创建一幅保存图像
			Bitmap bitmap = new Bitmap((int)grpWindow.VisibleClipBounds.Width, (int)grpWindow.VisibleClipBounds.Height, grpWindow);
				
			//创建bitmap相关的Grp类
			Graphics grpBitmap = Graphics.FromImage(bitmap);
			
			//窗口上下文
			IntPtr hdcWindow = grpWindow.GetHdc();
			
			//图片的上下文
			IntPtr hdcBitmap = grpBitmap.GetHdc();
			
			//拷贝
			Gdi32.BitBlt(hdcBitmap, 0, 0, bitmap.Width, bitmap.Height, hdcWindow, 0, 0, 0x00CC0020);
			
			//释放关联grpBitmap句柄
			grpBitmap.ReleaseHdc(hdcBitmap);
			
			//释放关联grpWindow句柄
			grpWindow.ReleaseHdc(hdcWindow);

			//释放grpBitmap对象
			grpBitmap.Dispose();

			//释放grpWindow对象
			grpWindow.Dispose();

			//返回图像
			return bitmap;			
		}
	}
}
