/***********************************************************************
 * Module:  MousePoint.cs
 * Author:  Administrator
 * Purpose: Definition of the Enum MousePoint
 ***********************************************************************/

using System;

public enum MousePoint
{
   /// Fields
   CenterPoint = 0x12,
   Flip = 0x16,
   None = 0x17,
   Rotate = 15,
   RotateFromCenter = 0x10,
   ScaleBottomLeft = 5,
   ScaleBottomMiddle = 6,
   ScaleBottomRight = 7,
   ScaleFromCenter = 8,
   ScaleMiddleLeft = 3,
   ScaleMiddleRight = 4,
   ScaleTopLeft = 0,
   ScaleTopMiddle = 1,
   ScaleTopRight = 2,
   ShapeMoveControl = 20,
   ShapeMoveLine = 0x15,
   ShapeMovePoint = 0x13,
   SkewXBottom = 10,
   SkewXFromCenter = 11,
   SkewXTop = 9,
   SkewYFromCenter = 14,
   SkewYLeft = 12,
   SkewYRight = 13,
   Translate = 0x11
}