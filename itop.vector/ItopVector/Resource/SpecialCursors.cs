/***********************************************************************
 * Module:  SpecialCursors.cs
 * Author:  Administrator
 * Purpose: Definition of the Class ItopVector.SpecialCursors
 ***********************************************************************/

using System;
using ItopVector.Resource;
using System.Windows.Forms;

namespace ItopVector
{
   public class SpecialCursors
   {
      public SpecialCursors()
      {
      }
      
      public static void LoadCursors()
      {
          Type type1 = Type.GetType("ItopVector.SpecialCursors");
          SpecialCursors.EqualScaleCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.ChangeR.cur");
          SpecialCursors.AnchorMoveCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.AnchorMoveCursor.cur");
          SpecialCursors.AnchorCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.AnchorCursor.cur");
          SpecialCursors.drawCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.ShapeDraw.cur");
          SpecialCursors.selectCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.Select.cur");
          SpecialCursors.dotCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.Subpath.cur");
          SpecialCursors.increaseCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.IncreaseView.cur");
          SpecialCursors.decreaseCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.DecreaseView.cur");
          SpecialCursors.NoViewChangeCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.NoView.cur");
          SpecialCursors.handCurosr = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.Roam.cur");
          SpecialCursors.addDotCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.PenAdd.cur");
          SpecialCursors.delDotCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.PenDel.cur");
          SpecialCursors.bezierCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.PenDraw.cur");
          SpecialCursors.moveBezierCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.Drag.cur");
          SpecialCursors.CloseBezierCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.PenClose.cur");
          SpecialCursors.ChangeControlCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.SubpathDrag.cur");
          SpecialCursors.TextCursor = Cursors.IBeam;
          SpecialCursors.DragCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.Drag.cur");
          SpecialCursors.DragInfoCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.DragInfo.cur");
          SpecialCursors.ShapeDragCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.SubpathDrag.cur");
          SpecialCursors.AreaSelectCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.AreaSelect.cur");
          SpecialCursors.HScaleCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.VScale.cur");
          SpecialCursors.VScaleCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.HScale.cur");
          SpecialCursors.TopRightScaleCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.CCWCornerScale.cur");
          SpecialCursors.TopLeftScaleCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.CWCornerScale.cur");
          SpecialCursors.SkewXCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.SkewX.cur");
          SpecialCursors.SkewYCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.SkewY.cur");
          SpecialCursors.RotateCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.Rotate.cur");
          SpecialCursors.CenterPointCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.CenterPoint.cur");
          SpecialCursors.MoveControlCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.SubpathDrag.cur");
          SpecialCursors.GradientTransformCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.GradientTransform.cur");
          SpecialCursors.GradientTranslateCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.ColorTranslate.cur");
          SpecialCursors.ColorPickerCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.ColorPicker.cur");
          SpecialCursors.AddStopCursor = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.AddControl.cur");
          SpecialCursors.Default = Cursors.Default;
          SpecialCursors.MoveRect = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.MoveRect.cur");
          SpecialCursors.PolyAdd = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.polyaddDraw.cur");
          SpecialCursors.PolyDel = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.polydelDraw.cur");
          SpecialCursors.MovePath = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.movepath.cur");
          SpecialCursors.ChangeEnd = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.PenEnd.cur");
          SpecialCursors.PolyDraw = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.polydraw.cur");
		  SpecialCursors.WindowZoom = ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.WindowZoom.cur");
		  SpecialCursors.MeasureCursor = ResourceHelper.LoadCursor(type1,"ItopVector.Resource.Cursors.measure.cur");
		  PolyBreak =ResourceHelper.LoadCursor(type1, "ItopVector.Resource.Cursors.polybreakDraw.cur");
      }
   
      /// Methods
      static SpecialCursors()
      {
          SpecialCursors.EqualScaleCursor = Cursors.Default;
          SpecialCursors.AnchorMoveCursor = Cursors.Default;
          SpecialCursors.AnchorCursor = Cursors.Default;
          SpecialCursors.drawCursor = Cursors.Default;
          SpecialCursors.selectCursor = Cursors.Default;
          SpecialCursors.dotCursor = Cursors.Default;
          SpecialCursors.increaseCursor = Cursors.Default;
          SpecialCursors.decreaseCursor = Cursors.Default;
          SpecialCursors.NoViewChangeCursor = Cursors.Default;
          SpecialCursors.handCurosr = Cursors.Default;
          SpecialCursors.addDotCursor = Cursors.Default;
          SpecialCursors.delDotCursor = Cursors.Default;
          SpecialCursors.bezierCursor = Cursors.Default;
          SpecialCursors.moveBezierCursor = Cursors.Default;
          SpecialCursors.CloseBezierCursor = Cursors.Default;
          SpecialCursors.ChangeControlCursor = Cursors.Default;
          SpecialCursors.FreeDrawCursor = Cursors.Default;
          SpecialCursors.TextCursor = Cursors.IBeam;
          SpecialCursors.DragCursor = Cursors.Default;
          SpecialCursors.DragInfoCursor = Cursors.Default;
          SpecialCursors.ShapeDragCursor = Cursors.Default;
          SpecialCursors.ColorSelectCursor = Cursors.Default;
          SpecialCursors.AreaSelectCursor = Cursors.Default;
          SpecialCursors.VScaleCursor = Cursors.Default;
          SpecialCursors.HScaleCursor = Cursors.Default;
          SpecialCursors.TopRightScaleCursor = Cursors.Default;
          SpecialCursors.TopLeftScaleCursor = Cursors.Default;
          SpecialCursors.SkewXCursor = Cursors.Default;
          SpecialCursors.SkewYCursor = Cursors.Default;
          SpecialCursors.RotateCursor = Cursors.Default;
          SpecialCursors.CenterPointCursor = Cursors.Default;
          SpecialCursors.MoveControlCursor = Cursors.Default;
          SpecialCursors.PaintBottleCursor = Cursors.Default;
          SpecialCursors.InkBottleCursor = Cursors.Default;
          SpecialCursors.GradientTransformCursor = Cursors.Default;
          SpecialCursors.GradientTranslateCursor = Cursors.Default;
          SpecialCursors.ColorPickerCursor = Cursors.Default;
          SpecialCursors.AddStopCursor = Cursors.Default;
          SpecialCursors.Default = Cursors.Default;
          SpecialCursors.MoveRect = Cursors.Default;
          SpecialCursors.PolyAdd = Cursors.Default;
          SpecialCursors.PolyDel = Cursors.Default;
          SpecialCursors.PolyDraw = Cursors.Default;
          SpecialCursors.MovePath = Cursors.Default;
          SpecialCursors.ChangeEnd = Cursors.Default;
		  SpecialCursors.WindowZoom = Cursors.Default;
		  SpecialCursors.MeasureCursor = Cursors.Default;
		  PolyBreak =Cursors.Default;
		  
      }
   
      /// Fields
      public static Cursor addDotCursor;
      public static Cursor AddStopCursor;
      public static Cursor AnchorCursor;
      public static Cursor AnchorMoveCursor;
      public static Cursor AreaSelectCursor;
      public static Cursor bezierCursor;
      public static Cursor CenterPointCursor;
      public static Cursor ChangeControlCursor;
      public static Cursor ChangeEnd;
      public static Cursor CloseBezierCursor;
      public static Cursor ColorPickerCursor;
      public static Cursor ColorSelectCursor;
      public static Cursor decreaseCursor;
      public static Cursor Default;
      public static Cursor delDotCursor;
      public static Cursor dotCursor;
      public static Cursor DragCursor;
      public static Cursor DragInfoCursor;
      public static Cursor drawCursor;
      public static Cursor EqualScaleCursor;
      public static Cursor FreeDrawCursor;
      public static Cursor GradientTransformCursor;
      public static Cursor GradientTranslateCursor;
      public static Cursor handCurosr;
      public static Cursor HScaleCursor;
      public static Cursor increaseCursor;
      public static Cursor InkBottleCursor;
      public static Cursor moveBezierCursor;
      public static Cursor MoveControlCursor;
      public static Cursor MovePath;
      public static Cursor MoveRect;
      public static Cursor NoViewChangeCursor;
      public static Cursor PaintBottleCursor;
      public static Cursor PolyAdd;
      public static Cursor PolyDel;
      public static Cursor PolyBreak;
      public static Cursor PolyDraw;
      public static Cursor RotateCursor;
      public static Cursor selectCursor;
      public static Cursor ShapeDragCursor;
      public static Cursor SkewXCursor;
      public static Cursor SkewYCursor;
      public static Cursor TextCursor;
      public static Cursor TopLeftScaleCursor;
      public static Cursor TopRightScaleCursor;
      public static Cursor VScaleCursor;
      public static Cursor WindowZoom;
	   public static Cursor MeasureCursor;
       
   }
}