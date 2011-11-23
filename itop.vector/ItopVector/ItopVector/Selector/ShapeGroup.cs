namespace ItopVector.Selector
{
	using System;
	using System.Collections;
	using System.Runtime.Serialization;
	using System.Xml;

	[Serializable]
	internal class ShapeGroup : DisposeBase, ISerializable, IDisposable
	{
		// Methods
		protected ShapeGroup(SerializationInfo info, StreamingContext context)
		{
			this.barGroups = null;
			this.barGroups = info.GetValue("groups", typeof(OutlookBarItemCollection[])) as OutlookBarItemCollection[];
		}

		public ShapeGroup(string filepath, bool symbol)
		{
			this.barGroups = null;
			XmlDocument document1 = new XmlDocument();
			document1.Load(filepath);
			XmlNodeList list1 = document1.GetElementsByTagName("group", document1.NamespaceURI);
			ArrayList list2 = new ArrayList();
			if ((list1 != null) && (list1.Count > 0))
			{
				for (int num1 = 0; num1 < list1.Count; num1++)
				{
					XmlElement element1 = list1[num1] as XmlElement;
					if (element1 != null)
					{
						string text1 = element1.GetAttribute("id");
						if (text1.Trim().Length > 0)
						{
							OutlookBarItemCollection items = new OutlookBarItemCollection();
							items.Id = text1.Trim();
							XmlNodeList list3 = element1.GetElementsByTagName("path");
							if ((list3 != null) && (list3.Count > 0))
							{
								for (int num2 = 0; num2 < list3.Count; num2++)
								{
									XmlElement element2 = list3[num2] as XmlElement;
									if (element2 != null)
									{
										string text2 = element2.GetAttribute("id", element2.NamespaceURI);
										if (text2.Trim().Length > 0)
										{
											string text3 = element2.GetAttribute("d");
											if (text3.Trim().Length > 0)
											{
												Shape shape1 = new Shape(text3, text2);
												items.Add(shape1);
											}
											text3 = null;
										}
										text2 = null;
									}
								}
								if (items.Count > 0)
								{
									list2.Add(items);
								}
							}
						}
					}
				}
			}
			if (list2.Count > 0)
			{
				this.barGroups = new OutlookBarItemCollection[list2.Count];
				list2.CopyTo(this.barGroups);
			}
			list2 = null;
			list1 = null;
			document1 = null;
		}

		public override void Dispose()
		{
			this.barGroups = null;
			base.Dispose();
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(ShapeGroup));
			if ((this.barGroups != null) && (this.barGroups.Length > 0))
			{
				info.AddValue("groups", this.barGroups);
			}
		}


		// Properties
		internal OutlookBarItemCollection[] BarGroups
		{
			get
			{
				return this.barGroups;
			}
		}


		// Fields
		private OutlookBarItemCollection[] barGroups;
	}
}

