using System;

namespace Itop.MapView.Tables
{
	/// <summary>
	/// MapClass 的摘要说明。
	/// </summary>
	public class MapClass
	{
		string _picUrl = string.Empty;
		byte[] stream = null;
		public MapClass() { }
		public MapClass(string src, byte[] bytes) 
		{
			_picUrl = src;
			stream = bytes;
		}
		public string PicUrl 
		{
			get { return _picUrl; }
			set { _picUrl = value; }
		}
		public byte[] Stream 
		{
			get { return stream; }
			set { stream = value; }
		}
	}
}
