/***********************************************************************
 * Module:  ToolOperation.cs
 * Author:  Administrator
 * Purpose: Definition of the Enum ToolOperation
 ***********************************************************************/

using System;

public enum ToolOperation
{
	/// Fields
	/// 
	Select = 0,//选择
	ShapeTransform = 1,//节点编辑操作 
	Rectangle = 2,//	绘制矩形
	AngleRectangle = 3,//圆角矩形
	Circle = 4,
	Ellipse = 5,//	绘制椭圆或圆(按住Shift键) 
	Line = 6,//	绘制直线 
	PolyLine = 7,//	绘制折线 
	Polygon = 8,	//	绘制多边形 
	EqualPolygon = 9,//等边多边形
	Bezier = 10,//	构造自由曲线 
	FreeLines = 11,//锁套选择
	Text = 12,//	构造文本 
	Image = 13,//	插入图片 
	FreeTransform = 14,//	自由变换操作 
	Rotate = 15,//旋转
	Scale = 16,//缩放
	Skew = 17,//扭曲
	Roam = 18,//	漫游操作 
	IncreaseView = 19,
	DecreaseView = 20,
	None = 21,
	ColorSelect = 22,
	AreaSelect = 23,
	GradientTransform = 24,	
	ColorPicker = 25,
	InkBottle = 26,
	PaintBottle = 27,
	ConvertAnchor = 28,
	FreePath = 29,//	
	PreShape = 30,	
	Flip = 31,	
	Pie =32,//圆角
	Arc =33,//圆弧
	ConnectLine =34, //连接线
	
	WindowZoom = 66,//窗口缩放
	XPolyLine=67,
	YPolyLine=68,
	Confines_GuoJie=80,//国界
	Confines_ShengJie=81,//省界
	Confines_ShiJie=82,//市界
	Confines_XianJie=83,//县界
	Confines_XiangJie=84,//乡界
	AreaPolygon=85,      //规划图使用,功能和Polygon一样,只是增加一个属性 “IsArea”
	LeadLine=86,         //规划图使用，功能和PolyLine一样， 只是增加一个属性"IsLead"
	Railroad=89,//铁路,
	Enclosure=90,//围栏(需要完全包括才能选中)
	ConnectLine_Polyline=91, //曲折线连接线
	ConnectLine_Rightangle=92, //直角连接线
	ConnectLine_Line=93, //直线连接线
	ConnectLine_Spline=94, //曲线连接线.
	InterEnclosure=95, //围栏(搭边即可选中)
    InterEnclosurePrint=96,

	Exceptant=100, //其他特殊操作
	XXX=101,
	Symbol = 102, //外部图元
	Distance =103, //测距操作

	Custom01=1001,//定制操作1001-10010
	Custom02=1002,
	Custom03=1003,
	Custom04=1004,
	Custom05=1005,
	Custom06=1006,
	Custom07=1007,
	Custom08=1008,
	Custom09=1009,
	Custom10=1010,
    Custom11=1011,//截断
    Custom12=1012,//延长从e开始   
    Custom13=1013,//延长从b开始
    Custom14=1014,//删除
    Custom15=1015//增加
}