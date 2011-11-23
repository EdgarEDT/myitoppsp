namespace ItopVector.Selector
{
	using System;
	using System.ComponentModel;
	using System.IO;
	using System.Windows.Forms;
	using ItopVector.Core.Document;

	[ToolboxItem(false)]
	public class ShapeSelector : OutlookBar
	{
		// Methods
		public ShapeSelector(string configpath)
		{
			this.svgDocument = null;
			this.currentSymbol = null;
			if (File.Exists(configpath))
			{
				try
				{
					this.svgDocument = new ShapeGroup(configpath, false);
					this.InitData(this.svgDocument);
					this.svgDocument.Dispose();
					this.svgDocument = null;
				}
				catch
				{
				}
			}
			base.CreateShapePanel();
			this.SelectedPathIndex = 0;
			base.SetStyle(ControlStyles.DoubleBuffer | (ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint), true);
		}

		private int IndexOf(Shape item)
		{
			int num1 = base.SelectedIndex;
			if (num1 >= 0)
			{
				OutlookBarItemCollection items = base.selectorGroups[num1] as OutlookBarItemCollection;
				if (items != null)
				{
					return items.IndexOf(item);
				}
			}
			return -1;
		}

		private void InitData(ShapeGroup svgDocument)
		{
			if (svgDocument != null)
			{
				OutlookBarItemCollection[] eabceArray1 =svgDocument.BarGroups;
				if ((eabceArray1 != null) && (eabceArray1.Length > 0))
				{
					for (int num1 = 0; num1 < eabceArray1.Length; num1++)
					{
						OutlookBarItemCollection items = eabceArray1[num1];
						base.selectorGroups.Add(items);
					}
				}
			}
		}


		// Properties
		public Shape SelectedShape
		{
			get
			{
				return (base.SelectedObject as Shape);
			}
			set
			{
				this.SelectedPathIndex = this.IndexOf(this.currentSymbol);
			}
		}


		// Fields
		private ShapeGroup svgDocument;
		private Shape currentSymbol;
	}
}

