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
	/// IItopVector ��ժҪ˵����
	/// </summary>
	internal interface IItopVector
	{
		SvgDocument SVGDocument{get;set;}	//	�ĵ�����
		PropertyGrid PropertyGrid{get;set;}	//	�����б����
		ToolOperation CurrentOperation{get;set;}	//	��ǰ����
		bool IsModified{get;set;}		//	�ĵ��Ƿ��޸�
		bool IsShowRule{get;set;}	//	�Ƿ���ʾ���
		bool IsShowGrid{get;set;}	//	�Ƿ���ʾ����
		bool IsPasteGrid{get;set;}	//	�Ƿ�ճ��������
		bool IsShowTip{get;set;}	//	�Ƿ���ʾͼԪ��ʾ
		SmoothingMode SmoothingMode{get;set;}	//	ͼ�εĻ�������
		DrawModeType DrawMode{get;set;} // ͼ�εĻ���ģʽ
		SizeF DocumentSize{get;set;}	//	�����ߴ�
		Color DocumentbgColor{get;set;}	//	������ɫ
		bool Scrollable{get;set;}	//	�Ƿ���ʾ������
		float ScaleRatio{get;set;}	//	��ǰ��ͼ���ű���
		Stroke Stroke{get;set;}	//	���ʶ���
		SolidColor Fill{get;set;}	//	�����״�Ļ�ˢ
		Struct.TextStyle TextStyle{get;set;}	//	�ı�������Ϣ
		bool Open(string filename)	;	//	��*.svg�ĵ�
		bool Save(string filename)	;	//	����*.svg�ĵ�
		bool ExportImage(string filename,ImageFormat filetype)	;	//	����ͼƬ(bmp��jpeg��)
		void ExportImage()	;
		void PaperSetup()	;	//	ҳ������
		void Print()	;	//	��ӡ
		void PrintPreview()	;	//	��ӡԤ��
		void Align(AlignType align)	;	//	�Ե�ǰѡ�еĶ���ִ�ж��빦��
		void Clear()	;	//	�������ж���
		void ClearBuffer()	;	//	�������ж���
		//void ClearUndos()	;	//	�������������Ϣ
		void Copy()	;	//	����
		void Cut()	;	//	����
		void Paste()	;	//	ճ��
		void Delete()	;	//	ɾ��
		void Redo()	;	//	����
		void Undo()	;	//	�ָ�
		void Distribute(DistributeType type)	;	//	��ѡ���Ķ��󰴼����зֲ�
		void MakeSameSize(SizeType type)	;	//	������ǰѡ�ж���ʹ���Ǿ߱���ͬ�Ŀ�Ȼ�߶�
		void MatrixSelection(Matrix matrix)	;	//	�Ե�ǰѡ�ж���ִ��ָ���任
		void SelectAll()	;	//	ѡ�����ж���
		void SelectNone()	;	//	����ѡ�ж���
		void Group()	;	//	���
		void UnGroup()	;	//	���
		void ChangeLevel(LevelType level)	;	//	���µ�ǰ�����ڴ�ֱ�����ϵĲ��
		String ExportSymbol(bool wholecontent,bool exportshape,bool createdocument,string id);		//	����ǰ��ͼ���ݻ�ѡ������ΪͼԪ���Զ�����״ 
		void ShowExportSymbolDialog(string filefilter)	;	//	�÷����ṩ��ExportSymbol�����Ŀ��ӻ����ã���ʾ�Ի����Ե���ͼԪ. �ڶԻ����У�������������ز�����Ԥ�����룬�����Խ����뱣�浽�ļ���
		void DocumentChanged(object sender, EventArgs e)	;	//	�ĵ��ı�
		void OperationChanged(object sender, EventArgs e)	;	//	�����ı�

		
	}
}
