namespace ItopVector.Selector
{
	using System;
	using System.Collections;
	using System.Reflection;
	using System.Runtime.Serialization;

	[Serializable]//, DefaultMember("Item")]
	internal class OutlookBarItemCollection : CollectionBase, IOutlookBarBand
	{
		// Methods
		public OutlookBarItemCollection()
		{
			this.nodeId = string.Empty;
			
			this.items = null;
		}

		protected OutlookBarItemCollection(SerializationInfo info, StreamingContext context)
		{
			
			this.nodeId = info.GetValue("id", typeof(string)) as string;
			this.items = info.GetValue("childs", typeof(Shape[])) as Shape[];
		}

		public int IndexOf(IShape graphPath)
		{
			return base.List.IndexOf(graphPath);
		}

		public void Remove(IShape item)
		{
			if (base.List.Contains(item))
			{
				base.List.Remove(item);
			}
		}

		public void Add(IShape item)
		{
			if (!base.List.Contains(item))
			{
				base.List.Add(item);
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(OutlookBarItemCollection));
			info.AddValue("id", this.nodeId);
			Shape[] shapeArray1 = new Shape[this.Count];
			base.List.CopyTo(shapeArray1, 0);
			info.AddValue("childs", shapeArray1);
		}


		// Properties
		

		public new int Count
		{
			get
			{
				return base.Count;
			}
		}

		public string Id
		{
			get
			{
				return this.nodeId;
			}
			set
			{
				this.nodeId = value;
			}
		}

		public IShape this[int index]
		{
			get
			{
				return (base.List[index] as IShape);
			}
			set
			{
				base.List[index] = value;
			}
		}


		// Fields
		
		private IShape[] items;
		private string nodeId;
	}
}

