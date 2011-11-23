using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Client.Common
{
    public class ComboBoxItem : IConvertible
    {
        #region 字段
        protected object _key = null;
        protected string _text = "";
        #endregion

        #region 构造方法
        protected ComboBoxItem()
        {

        }

        public ComboBoxItem(object key, string text)
        {
            _key = key;
            _text = text;
        }
        #endregion

        #region 属性
        public object Key
        {
            get { return _key; }
        }

        public string Text
        {
            get { return _text; }
        }
        #endregion

        #region 实现接口
        // 摘要:
        //     返回此实例的 System.TypeCode。
        //
        // 返回结果:
        //     枚举常数，它是实现该接口的类或值类型的 System.TypeCode。
        public TypeCode GetTypeCode() { throw new NotSupportedException(); }
        //
        // 摘要:
        //     使用指定的区域性特定格式设置信息将此实例的值转换为等效的 Boolean 值。
        //
        // 参数:
        //   provider:
        //     System.IFormatProvider 接口实现，提供区域性特定的格式设置信息。
        //
        // 返回结果:
        //     与此实例的值等效的 Boolean 值。
        public bool ToBoolean(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // 摘要:
        //     使用指定的区域性特定格式设置信息将该实例的值转换为等效的 8 位无符号整数。
        //
        // 参数:
        //   provider:
        //     System.IFormatProvider 接口实现，提供区域性特定的格式设置信息。
        //
        // 返回结果:
        //     与该实例的值等效的 8 位无符号整数。
        public byte ToByte(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // 摘要:
        //     使用指定的区域性特定格式设置信息将此实例的值转换为等效的 Unicode 字符。
        //
        // 参数:
        //   provider:
        //     System.IFormatProvider 接口实现，提供区域性特定的格式设置信息。
        //
        // 返回结果:
        //     与此实例的值等效的 Unicode 字符。
        public char ToChar(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // 摘要:
        //     使用指定的区域性特定格式设置信息将此实例的值转换为等效的 System.DateTime。
        //
        // 参数:
        //   provider:
        //     System.IFormatProvider 接口实现，提供区域性特定的格式设置信息。
        //
        // 返回结果:
        //     与此实例的值等效的 System.DateTime 实例。
        public DateTime ToDateTime(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // 摘要:
        //     使用指定的区域性特定格式设置信息将此实例的值转换为等效的 System.Decimal 数字。
        //
        // 参数:
        //   provider:
        //     System.IFormatProvider 接口实现，提供区域性特定的格式设置信息。
        //
        // 返回结果:
        //     与此实例的值等效的 System.Decimal 数字。
        public decimal ToDecimal(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // 摘要:
        //     使用指定的区域性特定格式设置信息将此实例的值转换为等效的双精度浮点数字。
        //
        // 参数:
        //   provider:
        //     System.IFormatProvider 接口实现，提供区域性特定的格式设置信息。
        //
        // 返回结果:
        //     与此实例的值等效的双精度浮点数字。
        public double ToDouble(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // 摘要:
        //     使用指定的区域性特定格式设置信息将此实例的值转换为等效的 16 位有符号整数。
        //
        // 参数:
        //   provider:
        //     System.IFormatProvider 接口实现，提供区域性特定的格式设置信息。
        //
        // 返回结果:
        //     与此实例的值等效的 16 位有符号整数。
        public short ToInt16(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // 摘要:
        //     使用指定的区域性特定格式设置信息将此实例的值转换为等效的 32 位有符号整数。
        //
        // 参数:
        //   provider:
        //     System.IFormatProvider 接口实现，提供区域性特定的格式设置信息。
        //
        // 返回结果:
        //     与此实例的值等效的 32 位有符号整数。
        public int ToInt32(IFormatProvider provider) { return (int)_key; }
        //
        // 摘要:
        //     使用指定的区域性特定格式设置信息将此实例的值转换为等效的 64 位有符号整数。
        //
        // 参数:
        //   provider:
        //     System.IFormatProvider 接口实现，提供区域性特定的格式设置信息。
        //
        // 返回结果:
        //     与此实例的值等效的 64 位有符号整数。
        public long ToInt64(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // 摘要:
        //     使用指定的区域性特定格式设置信息将此实例的值转换为等效的 8 位有符号整数。
        //
        // 参数:
        //   provider:
        //     System.IFormatProvider 接口实现，提供区域性特定的格式设置信息。
        //
        // 返回结果:
        //     与此实例的值等效的 8 位有符号整数。
        public sbyte ToSByte(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // 摘要:
        //     使用指定的区域性特定格式设置信息将此实例的值转换为等效的单精度浮点数字。
        //
        // 参数:
        //   provider:
        //     System.IFormatProvider 接口实现，提供区域性特定的格式设置信息。
        //
        // 返回结果:
        //     与此实例的值等效的单精度浮点数字。
        public float ToSingle(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // 摘要:
        //     使用指定的区域性特定格式设置信息将此实例的值转换为等效的 System.String。
        //
        // 参数:
        //   provider:
        //     System.IFormatProvider 接口实现，提供区域性特定的格式设置信息。
        //
        // 返回结果:
        //     与此实例的值等效的 System.String 实例。
        public string ToString(IFormatProvider provider) { return _text; }
        //
        // 摘要:
        //     使用指定的区域性特定格式设置信息将此实例的值转换为具有等效值的指定 System.Type 的 System.Object。
        //
        // 参数:
        //   provider:
        //     System.IFormatProvider 接口实现，提供区域性特定的格式设置信息。
        //
        //   conversionType:
        //     要将此实例的值转换为的 System.Type。
        //
        // 返回结果:
        //     其值与此实例值等效的 conversionType 类型的 System.Object 实例。
        public object ToType(Type conversionType, IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // 摘要:
        //     使用指定的区域性特定格式设置信息将该实例的值转换为等效的 16 位无符号整数。
        //
        // 参数:
        //   provider:
        //     System.IFormatProvider 接口实现，提供区域性特定的格式设置信息。
        //
        // 返回结果:
        //     与该实例的值等效的 16 位无符号整数。
        public ushort ToUInt16(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // 摘要:
        //     使用指定的区域性特定格式设置信息将该实例的值转换为等效的 32 位无符号整数。
        //
        // 参数:
        //   provider:
        //     System.IFormatProvider 接口实现，提供区域性特定的格式设置信息。
        //
        // 返回结果:
        //     与该实例的值等效的 32 位无符号整数。
        public uint ToUInt32(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // 摘要:
        //     使用指定的区域性特定格式设置信息将该实例的值转换为等效的 64 位无符号整数。
        //
        // 参数:
        //   provider:
        //     System.IFormatProvider 接口实现，提供区域性特定的格式设置信息。
        //
        // 返回结果:
        //     与该实例的值等效的 64 位无符号整数。
        public ulong ToUInt64(IFormatProvider provider) { throw new NotSupportedException(); }
        #endregion

        #region 重写虚函数
        public override string ToString()
        {
            return _text;
        }
        #endregion
    }
}
