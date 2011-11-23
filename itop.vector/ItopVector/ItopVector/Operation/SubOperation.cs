namespace ItopVector.DrawArea
{
	using ItopVector.Core;
	using ItopVector.Core.Figure;
	using ItopVector.Core.Interface;
	using ItopVector.Core.Interface.Figure;
	using System;
	using System.Windows.Forms;

	internal class SubOperation : IOperation
	{
		// Methods
		internal SubOperation(MouseArea mc)
		{
			this.currentGraph = null;
			this.mouseAreaControl = null;
			this.mouseAreaControl = mc;
		}

		internal SubOperation(MouseArea mc, IPath path)
		{
			this.currentGraph = null;
			this.mouseAreaControl = null;
			this.mouseAreaControl = mc;
			this.CurrentGraph = path;
		}

		public void Dispose()
		{
			this.mouseAreaControl.SubOperation = null;
		}
		public void OnMouseWheel(MouseEventArgs e)
		{
			// TODO:  ÃÌº” BezierOperation.DealMouseWheel  µœ÷
		}
		public void OnMouseDown(MouseEventArgs e)
		{
		}

		public void OnMouseMove(MouseEventArgs e)
		{
		}

		public void OnMouseUp(MouseEventArgs e)
		{
		}

		public void OnPaint(PaintEventArgs e)
		{
		}

		public bool ProcessDialogKey(Keys keydate)
		{
			return false;
		}

		public bool Redo()
		{
			return false;
		}

		public bool Undo()
		{
			return false;
		}


		// Properties
		public bool CanRedo
		{
			get
			{
				return false;
			}
		}

		public bool CanUndo
		{
			get
			{
				return false;
			}
		}

		public IPath CurrentGraph
		{
			get
			{
				return this.currentGraph;
			}
			set
			{
				if (this.currentGraph != value)
				{
					this.currentGraph = value;
					if (value != null)
					{
						string text1 = ((SvgElement) value).Name;
						if ((value is Polygon) || (value is Polyline))
						{
							if (((this.mouseAreaControl.editingOperation != null) && (this.mouseAreaControl.editingOperation != this.mouseAreaControl.polyOperation)) && (this.mouseAreaControl.editingOperation != this))
							{
								this.mouseAreaControl.editingOperation.Dispose();
							}
							if (this.mouseAreaControl.polyOperation == null)
							{
								this.mouseAreaControl.polyOperation = new PolyOperation(this.mouseAreaControl);
							}
							this.mouseAreaControl.editingOperation = this.mouseAreaControl.polyOperation;
							this.mouseAreaControl.polyOperation.CurrentGraph = (IGraphPath) value;
						}
						else if(value is Line)
						{
							if (((this.mouseAreaControl.editingOperation != null) && (this.mouseAreaControl.editingOperation != this.mouseAreaControl.lineOperation)) && (this.mouseAreaControl.editingOperation != this))
							{
								this.mouseAreaControl.editingOperation.Dispose();
							}
							if (this.mouseAreaControl.lineOperation == null)
							{
								this.mouseAreaControl.lineOperation = new LineOperation(this.mouseAreaControl);
							}
							this.mouseAreaControl.editingOperation = this.mouseAreaControl.lineOperation;
							this.mouseAreaControl.lineOperation.CurrentGraph = (IGraphPath) value;

						}
						else if ((text1 == "path") || (text1 == "animateMotion"))
						{
							if (((this.mouseAreaControl.editingOperation != null) && (this.mouseAreaControl.editingOperation != this.mouseAreaControl.BezierOperation)) && (this.mouseAreaControl.editingOperation != this))
							{
								this.mouseAreaControl.editingOperation.Dispose();
							}
							if (this.mouseAreaControl.BezierOperation == null)
							{
								this.mouseAreaControl.BezierOperation = new BezierOperation(this.mouseAreaControl);
							}
							this.mouseAreaControl.editingOperation = this.mouseAreaControl.BezierOperation;
							this.mouseAreaControl.BezierOperation.CurrentGraph = value;
						}
					}
					else if ((this.mouseAreaControl.editingOperation != this) && (this.mouseAreaControl.editingOperation != null))
					{
						this.mouseAreaControl.editingOperation.Dispose();
						this.mouseAreaControl.editingOperation = this;
					}
				}
			}
		}


		// Fields
		private IPath currentGraph;
		private MouseArea mouseAreaControl;
	}
}

