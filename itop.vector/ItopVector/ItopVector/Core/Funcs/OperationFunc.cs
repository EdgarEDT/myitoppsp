namespace ItopVector.Core.Func
{
	using System;

	public class OperationFunc
	{
		// Methods
		public OperationFunc()
		{
		}

		public static bool IsColorOperation(ToolOperation operation)
		{
			switch (operation)
			{
				case ToolOperation.GradientTransform:
				case ToolOperation.ColorPicker:
				case ToolOperation.InkBottle:
				case ToolOperation.PaintBottle:
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsDrawOperation(ToolOperation operation)
		{
			ToolOperation operation1 = operation;
			switch (operation1)
			{
				case ToolOperation.Rectangle:
				case ToolOperation.AngleRectangle:
				case ToolOperation.Circle:
				case ToolOperation.Ellipse:
				case ToolOperation.Line:
				case ToolOperation.PolyLine:
				case ToolOperation.XPolyLine:
				case ToolOperation.YPolyLine:
				case ToolOperation.Confines_GuoJie:
				case ToolOperation.Confines_ShengJie:
				case ToolOperation.Confines_ShiJie:
				case ToolOperation.Confines_XianJie:
				case ToolOperation.Confines_XiangJie:
				case ToolOperation.LeadLine:
				case ToolOperation.Railroad:
				case ToolOperation.Polygon:
				case ToolOperation.Enclosure:
				case ToolOperation.InterEnclosure:
                case ToolOperation.InterEnclosurePrint:
				case ToolOperation.AreaPolygon:
				case ToolOperation.EqualPolygon:
				case ToolOperation.Bezier:
				case ToolOperation.FreeLines:
				case ToolOperation.Text:
				case ToolOperation.Image:
				case ToolOperation.Pie:
				case ToolOperation.Arc:
				case ToolOperation.ConnectLine:
				{
					break;
				}
				default:
				{
					if (operation1 != ToolOperation.PreShape)
					{
						return false;
					}
					break;
				}
			}
			return true;
		}
		public static bool IsPieOperator(ToolOperation op)
		{
			if (op != ToolOperation.Pie)
			{
				return (op == ToolOperation.Arc);
			}
			return true;
		}

		public static bool IsSelectOperation(ToolOperation operation)
		{
			switch (operation)
			{
				case ToolOperation.Select:
				case ToolOperation.ShapeTransform:
                case ToolOperation.Custom11:
                case ToolOperation.Custom12:
                case ToolOperation.Custom13:
                case ToolOperation.Custom14:
                case ToolOperation.Custom15:
				case ToolOperation.ColorSelect:
				case ToolOperation.AreaSelect:
				case ToolOperation.WindowZoom:
				case ToolOperation.Exceptant:
				{
					return true;
				}
			}
			return false;
		}
		public static bool IsConnectLineOperation(ToolOperation operation)
		{
			switch(operation)
			{
				case ToolOperation.ConnectLine_Line:
				case ToolOperation.ConnectLine_Polyline:
				case ToolOperation.ConnectLine_Rightangle:
				case ToolOperation.ConnectLine_Spline:
				{
					return true;
				}
			}
			return false;
		}
		public static bool IsTransformOperation(ToolOperation operation)
		{
			switch (operation)
			{
				case ToolOperation.FreeTransform:
				case ToolOperation.Rotate:
				case ToolOperation.Scale:
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsViewOperation(ToolOperation operation)
		{
			switch (operation)
			{
				case ToolOperation.Roam:
				case ToolOperation.IncreaseView:
				case ToolOperation.DecreaseView:
				{
					return true;
				}
			}
			return false;
		}

	}
}

