namespace ItopVector.Core.Types
{
    using System;
    using System.Collections;
    using System.Reflection;
	using ItopVector.Core.Figure;

    [Serializable]
    public class PointInfoCollection : CollectionBase
    {
        // Methods
        public PointInfoCollection()
        {
            this.startinfo = null;
			this.owerGraph=null;
        }

        public PointInfoCollection(PointInfoCollection value):this()
        {            
            this.AddRange(value);
        }

        public PointInfoCollection(PointInfo[] value):this()
        {            
            this.AddRange(value);
        }

        public int Add(PointInfo value)
        {
            if (value == null)
            {
                return -1;
            }
            int num1 = base.List.Add(value);
			if (this.owerGraph!=null)
			{
				value.OwerCollection=this;
			}
            if ((((num1 - 1) >= 0) && ((num1 - 1) < base.List.Count)) && !value.IsStart)
            {
                PointInfo info1 = (PointInfo) base.List[num1 - 1];
                value.PreInfo = info1;
                info1.NextInfo = value;
                info1.NextControl = value.FirstControl;
            }
            if (value.IsStart)
            {
                this.startinfo = value;
            }
            if (value.IsEnd)
            {
                value.NextInfo = this.startinfo;
            }
            return num1;
        }

        public void AddRange(PointInfo[] value)
        {
            for (int num1 = 0; num1 < value.Length; num1++)
            {
                this.Add(value[num1]);
            }
        }

        public void AddRange(PointInfoCollection value)
        {
            for (int num1 = 0; num1 < value.Count; num1++)
            {
                this.Add(value[num1]);
            }
        }        

        public PointInfoCollection Clone()
        {
            PointInfoCollection collection1 = new PointInfoCollection();
            collection1.AddRange(this);
            return collection1;
        }

        public bool Contains(PointInfo value)
        {
            return base.List.Contains(value);
        }

        public void CopyTo(PointInfo[] array, int index)
        {
            if (array != null)
            {
                base.List.CopyTo(array, index);
            }
        }		
        public new PointInfoEnumerator GetEnumerator()
        {
            return new PointInfoEnumerator(this);
        }

        public int IndexOf(PointInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, PointInfo value)
        {
            base.List.Insert(index, value);
            if ((((index - 1) >= 0) && ((index - 1) < base.List.Count)) && !value.IsStart)
            {
                PointInfo info1 = (PointInfo) base.List[index - 1];
                value.PreInfo = info1;
                info1.NextInfo = value;
                info1.NextControl = value.FirstControl;
            }
            if (value.IsStart)
            {
                this.startinfo = value;
            }
            if (value.IsEnd)
            {
                value.NextInfo = this.startinfo;
            }
        }

        public void Remove(PointInfo value)
        {
            base.List.Remove(value);
        }
//        public void RemoveAt(int index)
//        {
//            base.List.RemoveAt(index);
//        }


        // Properties
        public PointInfo this[int index]
        {
            get
            {
                return (PointInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
		public Graph OwerGraph
		{
			get
			{
				return owerGraph;
			}
			set
			{
				owerGraph= value;
			}
		}


        // Fields
        private PointInfo startinfo;
		private Graph owerGraph;

        // Nested Types
        public class PointInfoEnumerator : IEnumerator
        {
            // Methods
            public PointInfoEnumerator(PointInfoCollection mappings)
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

            bool IEnumerator.MoveNext()
            {
                return this.baseEnumerator.MoveNext();
            }

            void IEnumerator.Reset()
            {
                this.baseEnumerator.Reset();
            }


            // Properties
            public PointInfo Current
            {
                get
                {
                    return (PointInfo) this.baseEnumerator.Current;
                }
            }
			

            // Fields
			
            private IEnumerator baseEnumerator;
			private IEnumerable temp;
			#region IEnumerator ³ÉÔ±

			object System.Collections.IEnumerator.Current
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

