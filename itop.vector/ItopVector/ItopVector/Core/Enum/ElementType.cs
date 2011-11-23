/***********************************************************************
 * Module:  ElementType.cs
 * Author:  Administrator
 * Purpose: Definition of the Enum ElementType
 ***********************************************************************/

using System;

public enum ElementType
{
   /// Fields
   Animate = 13,
   AudioAnimate = 0x13,
   Circle = 12,
   ClipPath = 0x22,
   ColorAnimate = 0x11,
   Defs = 0x17,
   Ellipse = 0x20,
   Empty = 0,
   GradientStop = 0x21,
   GraphPath = 4,
   Group = 2,
   Image = 6,
   Line = 10,
   LinearGradient = 0x1a,
   MotionAnimate = 0x10,
   Path = 5,
   Pattern = 0x1f,
   Polygon = 8,
   PolyLine = 9,
   RadialGradients = 0x1b,
   Rect = 3,
   RotateAnimate = 0x15,
   ScaleAnimate = 20,
   SetAnimate = 0x16,
   SkewAnimate = 0x12,
   SkewXAnimate = 0x1d,
   SkewYAnimate = 30,
   SolidColor = 0x19,
   Stroke = 0x1c,
   SVG = 1,
   SvgElement = 0x23,
   Symbol = 0x18,
   Text = 7,
   TransformAnimate = 14,
   TranslateAnimate = 15,
   Use = 11,
   Enclosure=90,
   Exceptant=100 //其他特殊操作
}