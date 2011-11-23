
using System;
/// <summary>
/// 绘制模式
/// </summary>
public enum DrawModeType
{
	Normal, //即时绘制，效果好，速度慢
	ScreenImage,//截屏模式，速度最快
	MemoryImage,//内存图片,速度快	 
}