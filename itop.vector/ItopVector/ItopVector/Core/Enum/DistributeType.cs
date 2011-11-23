/***********************************************************************
 * Module:  DistributeType.cs
 * Author:  Administrator
 * Purpose: Definition of the Enum DistributeType
 ***********************************************************************/

using System;
/// <summary>
/// 分布枚举
/// </summary>
public enum DistributeType
{
	Top	,	//	以上边缘为参考执行分布 
	Bottom	,	//	以下边缘为参考执行分布 
	Left	,	//	以左边缘为参考执行分布 
	Right	,	//	以右边缘为参考执行分布 
	VerticalCenter	,	//	以垂直中心点为参考执行分布 
	HorizontalCenter		//	以水平中心点为参考执行分布 


}