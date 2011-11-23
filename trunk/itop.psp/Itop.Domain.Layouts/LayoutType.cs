//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-11-16 10:34:18
//
//********************************************************************************/
using System;
namespace Itop.Domain.Layouts
{
	/// <summary>
	/// 实体类Layout 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class LayoutType
	{
        public LayoutType()
		{}
		#region 字段
        private string uid = "";
        private byte[] excelData; 

		#endregion 字段

		#region 属性

        public string UID
        {
            get { return uid; }
            set { uid = value; }
        }

        public byte[] ExcelData
        {
            get { return excelData; }
            set { excelData = value; }
        }


		#endregion 属性
	}
}

