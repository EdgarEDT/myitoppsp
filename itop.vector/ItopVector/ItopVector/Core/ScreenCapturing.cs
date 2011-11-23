using System;
using System.Drawing;
using ItopVector.Core.Win32;

namespace ItopVector.Core
{
	/// <summary>
	/// ScreenCapturing ��ժҪ˵����
	/// </summary>
	public class ScreenCapturing
	{
		public ScreenCapturing()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public static Bitmap CaptureDesktopWindow()
		{			
			//������Ļ���
			Graphics grpWindow = Graphics.FromHwnd(IntPtr.Zero);

			//����һ������ͼ��
			Bitmap bitmap = new Bitmap((int)grpWindow.VisibleClipBounds.Width, (int)grpWindow.VisibleClipBounds.Height, grpWindow);
				
			//����bitmap��ص�Grp��
			Graphics grpBitmap = Graphics.FromImage(bitmap);
			
			//����������
			IntPtr hdcWindow = grpWindow.GetHdc();
			
			//ͼƬ��������
			IntPtr hdcBitmap = grpBitmap.GetHdc();
			
			//����
			Gdi32.BitBlt(hdcBitmap, 0, 0, bitmap.Width, bitmap.Height, hdcWindow, 0, 0, 0x00CC0020);
			
			//�ͷŹ���grpBitmap���
			grpBitmap.ReleaseHdc(hdcBitmap);
			
			//�ͷŹ���grpWindow���
			grpWindow.ReleaseHdc(hdcWindow);

			//�ͷ�grpBitmap����
			grpBitmap.Dispose();

			//�ͷ�grpWindow����
			grpWindow.Dispose();

			//����ͼ��
			return bitmap;			
		}
	}
}
