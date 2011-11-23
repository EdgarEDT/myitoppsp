namespace ItopVector.Selector
{
	using System;

	public abstract class DisposeBase
	{
		// Methods
		protected DisposeBase()
		{
		}

		public virtual void Dispose()
		{
			GC.SuppressFinalize(this);
			GC.Collect(0);
		}

	}
}

