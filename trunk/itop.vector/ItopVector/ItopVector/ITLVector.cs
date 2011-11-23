using System;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using ItopVector.Core.Paint;
using ItopVector.Core.Document;
using ItopVector;
using System.Drawing.Imaging;

namespace ItopVector
{
	/// <summary>
	/// IItopVector 的摘要说明。
	/// </summary>
	internal interface IItopVector
	{
		SvgDocument SVGDocument{get;set;}	//	文档对象
		PropertyGrid PropertyGrid{get;set;}	//	属性列表对象
		ToolOperation CurrentOperation{get;set;}	//	当前操作
		bool IsModified{get;set;}		//	文档是否被修改
		bool IsShowRule{get;set;}	//	是否显示标尺
		bool IsShowGrid{get;set;}	//	是否显示网格
		bool IsPasteGrid{get;set;}	//	是否粘附到网格
		bool IsShowTip{get;set;}	//	是否显示图元提示
		SmoothingMode SmoothingMode{get;set;}	//	图形的绘制质量
		DrawModeType DrawMode{get;set;} // 图形的绘制模式
		SizeF DocumentSize{get;set;}	//	画布尺寸
		Color DocumentbgColor{get;set;}	//	画布底色
		bool Scrollable{get;set;}	//	是否显示滚动条
		float ScaleRatio{get;set;}	//	当前视图缩放比例
		Stroke Stroke{get;set;}	//	画笔对象
		SolidColor Fill{get;set;}	//	填充形状的画刷
		Struct.TextStyle TextStyle{get;set;}	//	文本绘制信息
		bool Open(string filename)	;	//	打开*.svg文档
		bool Save(string filename)	;	//	保存*.svg文档
		bool ExportImage(string filename,ImageFormat filetype)	;	//	导出图片(bmp、jpeg等)
		void ExportImage()	;
		void PaperSetup()	;	//	页面设置
		void Print()	;	//	打印
		void PrintPreview()	;	//	打印预览
		void Align(AlignType align)	;	//	对当前选中的对象执行对齐功能
		void Clear()	;	//	消除所有对象
		void ClearBuffer()	;	//	消除所有对象
		//void ClearUndos()	;	//	清除所有重作信息
		void Copy()	;	//	复制
		void Cut()	;	//	剪切
		void Paste()	;	//	粘贴
		void Delete()	;	//	删除
		void Redo()	;	//	撤消
		void Undo()	;	//	恢复
		void Distribute(DistributeType type)	;	//	将选定的对象按间距进行分布
		void MakeSameSize(SizeType type)	;	//	调整当前选中对象，使他们具备相同的宽度或高度
		void MatrixSelection(Matrix matrix)	;	//	对当前选中对象执行指定变换
		void SelectAll()	;	//	选中所有对象
		void SelectNone()	;	//	消除选中对象
		void Group()	;	//	组合
		void UnGroup()	;	//	拆分
		void ChangeLevel(LevelType level)	;	//	更新当前对象在垂直方向上的层次
		String ExportSymbol(bool wholecontent,bool exportshape,bool createdocument,string id);		//	将当前绘图内容或选区导出为图元或自定义形状 
		void ShowExportSymbolDialog(string filefilter)	;	//	该方法提供了ExportSymbol方法的可视化调用，显示对话框以导出图元. 在对话框中，您可以设置相关参数，预览代码，并可以将代码保存到文件中
		void DocumentChanged(object sender, EventArgs e)	;	//	文档改变
		void OperationChanged(object sender, EventArgs e)	;	//	操作改变

		
	}
}
