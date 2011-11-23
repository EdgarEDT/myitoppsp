namespace ItopVector.Core
{
    using ItopVector.Core.Interface;
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class SvgElementCollection : CollectionBase
    {
        // Events
        public event OnCollectionChangedEventHandler OnCollectionChangedEvent;

        // Methods
        public SvgElementCollection()
        {
            this.NotifyEvent = false;
        }

        public SvgElementCollection(SvgElementCollection value)
        {
            this.NotifyEvent = false;
            this.AddRange(value);
        }

        public SvgElementCollection(ISvgElement[] value)
        {
            this.NotifyEvent = false;
            this.AddRange(value);
        }

        public int Add(ISvgElement value)
        {
            if (value == null)
            {
                return -1;
            }
            int num1 =-1;
			num1 = base.List.IndexOf(value);
			if(!(num1>=0 && num1<=List.Count -1))
			{
				if (value is ItopVector.Core.Figure.ConnectLine)
				{
					num1=0;
					this.List.Insert(0,value);
				}
				else
				{
					num1 =base.List.Add(value);
				}

			}
            if ((this.OnCollectionChangedEvent != null) && this.NotifyEvent)
            {
                this.OnCollectionChangedEvent(this, new CollectionChangedEventArgs(value));
            }
            return num1;
        }

        public void AddRange(ISvgElement[] value)
        {
            bool flag1 = this.NotifyEvent;
            this.NotifyEvent = false;
            for (int num1 = 0; num1 < value.Length; num1++)
            {
				if(!this.Contains(value[num1]))
					this.Add(value[num1]);
            }
            this.NotifyEvent = flag1;
            if ((this.OnCollectionChangedEvent != null) && this.NotifyEvent)
            {
                this.OnCollectionChangedEvent(this, new CollectionChangedEventArgs(value));
            }
        }

        public void AddRange(SvgElementCollection value)
        {
            bool flag1 = this.NotifyEvent;
            this.NotifyEvent = false;
            for (int num1 = 0; num1 < value.Count; num1++)
            {
				if(!this.Contains(value[num1]))
				 this.Add(value[num1]);
            }
            this.NotifyEvent = flag1;
            if ((this.OnCollectionChangedEvent != null) && this.NotifyEvent)
            {
                this.OnCollectionChangedEvent(this, new CollectionChangedEventArgs(value));
            }
        }

        public new  void Clear()
        {
            base.List.Clear();
            if ((this.OnCollectionChangedEvent != null) && this.NotifyEvent)
            {
                this.OnCollectionChangedEvent(this, new CollectionChangedEventArgs(this));
            }
        }

        public SvgElementCollection Clone()
        {
            SvgElementCollection collection1 = new SvgElementCollection();
            collection1.AddRange(this);
            return collection1;
        }

        public bool Contains(ISvgElement value)
        {
            return base.List.Contains(value);
        }

        public void CopyTo(ISvgElement[] array, int index)
        {
            if (array != null)
            {
                base.List.CopyTo(array, index);
            }
        }

        public new  ISvgElementEnumerator GetEnumerator()
        {
            return new ISvgElementEnumerator(this);
        }

        public int IndexOf(ISvgElement value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, ISvgElement value)
        {
            base.List.Insert(index, value);
            if ((this.OnCollectionChangedEvent != null) && this.NotifyEvent)
            {
                this.OnCollectionChangedEvent(this, new CollectionChangedEventArgs(value));
            }
        }
		public void NotifyChange()
		{
			if ((this.OnCollectionChangedEvent != null) && this.NotifyEvent)
			{
				this.OnCollectionChangedEvent(this, new CollectionChangedEventArgs(this));
			}
		}

        public void Remove(ISvgElement value)
        {
			if(!this.Contains(value))return;
            base.List.Remove(value);
            if (this.OnCollectionChangedEvent != null)
            {
                this.OnCollectionChangedEvent(this, new CollectionChangedEventArgs(value));
            }
        }

        public new void RemoveAt(int index)
        {
            ISvgElement element1 = null;
            if ((index >= 0) && (index < base.Count))
            {
                element1 = (ISvgElement) base.List[index];
            }
            base.List.RemoveAt(index);
            if (this.OnCollectionChangedEvent != null)
            {
                this.OnCollectionChangedEvent(this, new CollectionChangedEventArgs(element1));
            }
        }


        // Properties
        public ISvgElement this[int index]
        {
            get
            {
                return (ISvgElement) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }

		public ISvgElement this[string id]
		{
			get{
				foreach(ISvgElement element in List)
				{
                    if(element.ID.Equals(id))return element;
				}
			
				return null;
			}
		}

        // Fields
        public bool NotifyEvent;

        // Nested Types
        public class ISvgElementEnumerator : IEnumerator
        {
            // Methods
            public ISvgElementEnumerator(SvgElementCollection mappings)
            {
                this.temp = mappings;
                this.baseEnumerator = this.temp.GetEnumerator();
            }

            public bool MoveNext()
            {
                return this.baseEnumerator.MoveNext();
            }

            public void Reset()
            {
                this.baseEnumerator.Reset();
            }

//            object IEnumerator.get_Current()
//            {
//                return this.baseEnumerator.Current;
//            }

            bool IEnumerator.MoveNext()
            {
                return this.baseEnumerator.MoveNext();
            }

            void IEnumerator.Reset()
            {
                this.baseEnumerator.Reset();
            }


            // Properties
            public ISvgElement Current
            {
                get
                {
                    return (ISvgElement) this.baseEnumerator.Current;
                }
            }
			

            // Fields
            private IEnumerator baseEnumerator;
			private IEnumerable temp;
			#region IEnumerator ³ÉÔ±

			object IEnumerator.Current
			{
				get
				{
					return this.baseEnumerator.Current;
				}
			}

			#endregion
		}
    }
}

