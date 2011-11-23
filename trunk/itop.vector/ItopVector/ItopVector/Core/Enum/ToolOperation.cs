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
	Select = 0,//ѡ��
	ShapeTransform = 1,//�ڵ�༭���� 
	Rectangle = 2,//	���ƾ���
	AngleRectangle = 3,//Բ�Ǿ���
	Circle = 4,
	Ellipse = 5,//	������Բ��Բ(��סShift��) 
	Line = 6,//	����ֱ�� 
	PolyLine = 7,//	�������� 
	Polygon = 8,	//	���ƶ���� 
	EqualPolygon = 9,//�ȱ߶����
	Bezier = 10,//	������������ 
	FreeLines = 11,//����ѡ��
	Text = 12,//	�����ı� 
	Image = 13,//	����ͼƬ 
	FreeTransform = 14,//	���ɱ任���� 
	Rotate = 15,//��ת
	Scale = 16,//����
	Skew = 17,//Ť��
	Roam = 18,//	���β��� 
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
	Pie =32,//Բ��
	Arc =33,//Բ��
	ConnectLine =34, //������
	
	WindowZoom = 66,//��������
	XPolyLine=67,
	YPolyLine=68,
	Confines_GuoJie=80,//����
	Confines_ShengJie=81,//ʡ��
	Confines_ShiJie=82,//�н�
	Confines_XianJie=83,//�ؽ�
	Confines_XiangJie=84,//���
	AreaPolygon=85,      //�滮ͼʹ��,���ܺ�Polygonһ��,ֻ������һ������ ��IsArea��
	LeadLine=86,         //�滮ͼʹ�ã����ܺ�PolyLineһ���� ֻ������һ������"IsLead"
	Railroad=89,//��·,
	Enclosure=90,//Χ��(��Ҫ��ȫ��������ѡ��)
	ConnectLine_Polyline=91, //������������
	ConnectLine_Rightangle=92, //ֱ��������
	ConnectLine_Line=93, //ֱ��������
	ConnectLine_Spline=94, //����������.
	InterEnclosure=95, //Χ��(��߼���ѡ��)
    InterEnclosurePrint=96,

	Exceptant=100, //�����������
	XXX=101,
	Symbol = 102, //�ⲿͼԪ
	Distance =103, //������

	Custom01=1001,//���Ʋ���1001-10010
	Custom02=1002,
	Custom03=1003,
	Custom04=1004,
	Custom05=1005,
	Custom06=1006,
	Custom07=1007,
	Custom08=1008,
	Custom09=1009,
	Custom10=1010,
    Custom11=1011,//�ض�
    Custom12=1012,//�ӳ���e��ʼ   
    Custom13=1013,//�ӳ���b��ʼ
    Custom14=1014,//ɾ��
    Custom15=1015//����
}