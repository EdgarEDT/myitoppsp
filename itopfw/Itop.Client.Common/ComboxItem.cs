using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Client.Common
{
    public class ComboBoxItem : IConvertible
    {
        #region �ֶ�
        protected object _key = null;
        protected string _text = "";
        #endregion

        #region ���췽��
        protected ComboBoxItem()
        {

        }

        public ComboBoxItem(object key, string text)
        {
            _key = key;
            _text = text;
        }
        #endregion

        #region ����
        public object Key
        {
            get { return _key; }
        }

        public string Text
        {
            get { return _text; }
        }
        #endregion

        #region ʵ�ֽӿ�
        // ժҪ:
        //     ���ش�ʵ���� System.TypeCode��
        //
        // ���ؽ��:
        //     ö�ٳ���������ʵ�ָýӿڵ����ֵ���͵� System.TypeCode��
        public TypeCode GetTypeCode() { throw new NotSupportedException(); }
        //
        // ժҪ:
        //     ʹ��ָ�����������ض���ʽ������Ϣ����ʵ����ֵת��Ϊ��Ч�� Boolean ֵ��
        //
        // ����:
        //   provider:
        //     System.IFormatProvider �ӿ�ʵ�֣��ṩ�������ض��ĸ�ʽ������Ϣ��
        //
        // ���ؽ��:
        //     ���ʵ����ֵ��Ч�� Boolean ֵ��
        public bool ToBoolean(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // ժҪ:
        //     ʹ��ָ�����������ض���ʽ������Ϣ����ʵ����ֵת��Ϊ��Ч�� 8 λ�޷���������
        //
        // ����:
        //   provider:
        //     System.IFormatProvider �ӿ�ʵ�֣��ṩ�������ض��ĸ�ʽ������Ϣ��
        //
        // ���ؽ��:
        //     ���ʵ����ֵ��Ч�� 8 λ�޷���������
        public byte ToByte(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // ժҪ:
        //     ʹ��ָ�����������ض���ʽ������Ϣ����ʵ����ֵת��Ϊ��Ч�� Unicode �ַ���
        //
        // ����:
        //   provider:
        //     System.IFormatProvider �ӿ�ʵ�֣��ṩ�������ض��ĸ�ʽ������Ϣ��
        //
        // ���ؽ��:
        //     ���ʵ����ֵ��Ч�� Unicode �ַ���
        public char ToChar(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // ժҪ:
        //     ʹ��ָ�����������ض���ʽ������Ϣ����ʵ����ֵת��Ϊ��Ч�� System.DateTime��
        //
        // ����:
        //   provider:
        //     System.IFormatProvider �ӿ�ʵ�֣��ṩ�������ض��ĸ�ʽ������Ϣ��
        //
        // ���ؽ��:
        //     ���ʵ����ֵ��Ч�� System.DateTime ʵ����
        public DateTime ToDateTime(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // ժҪ:
        //     ʹ��ָ�����������ض���ʽ������Ϣ����ʵ����ֵת��Ϊ��Ч�� System.Decimal ���֡�
        //
        // ����:
        //   provider:
        //     System.IFormatProvider �ӿ�ʵ�֣��ṩ�������ض��ĸ�ʽ������Ϣ��
        //
        // ���ؽ��:
        //     ���ʵ����ֵ��Ч�� System.Decimal ���֡�
        public decimal ToDecimal(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // ժҪ:
        //     ʹ��ָ�����������ض���ʽ������Ϣ����ʵ����ֵת��Ϊ��Ч��˫���ȸ������֡�
        //
        // ����:
        //   provider:
        //     System.IFormatProvider �ӿ�ʵ�֣��ṩ�������ض��ĸ�ʽ������Ϣ��
        //
        // ���ؽ��:
        //     ���ʵ����ֵ��Ч��˫���ȸ������֡�
        public double ToDouble(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // ժҪ:
        //     ʹ��ָ�����������ض���ʽ������Ϣ����ʵ����ֵת��Ϊ��Ч�� 16 λ�з���������
        //
        // ����:
        //   provider:
        //     System.IFormatProvider �ӿ�ʵ�֣��ṩ�������ض��ĸ�ʽ������Ϣ��
        //
        // ���ؽ��:
        //     ���ʵ����ֵ��Ч�� 16 λ�з���������
        public short ToInt16(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // ժҪ:
        //     ʹ��ָ�����������ض���ʽ������Ϣ����ʵ����ֵת��Ϊ��Ч�� 32 λ�з���������
        //
        // ����:
        //   provider:
        //     System.IFormatProvider �ӿ�ʵ�֣��ṩ�������ض��ĸ�ʽ������Ϣ��
        //
        // ���ؽ��:
        //     ���ʵ����ֵ��Ч�� 32 λ�з���������
        public int ToInt32(IFormatProvider provider) { return (int)_key; }
        //
        // ժҪ:
        //     ʹ��ָ�����������ض���ʽ������Ϣ����ʵ����ֵת��Ϊ��Ч�� 64 λ�з���������
        //
        // ����:
        //   provider:
        //     System.IFormatProvider �ӿ�ʵ�֣��ṩ�������ض��ĸ�ʽ������Ϣ��
        //
        // ���ؽ��:
        //     ���ʵ����ֵ��Ч�� 64 λ�з���������
        public long ToInt64(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // ժҪ:
        //     ʹ��ָ�����������ض���ʽ������Ϣ����ʵ����ֵת��Ϊ��Ч�� 8 λ�з���������
        //
        // ����:
        //   provider:
        //     System.IFormatProvider �ӿ�ʵ�֣��ṩ�������ض��ĸ�ʽ������Ϣ��
        //
        // ���ؽ��:
        //     ���ʵ����ֵ��Ч�� 8 λ�з���������
        public sbyte ToSByte(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // ժҪ:
        //     ʹ��ָ�����������ض���ʽ������Ϣ����ʵ����ֵת��Ϊ��Ч�ĵ����ȸ������֡�
        //
        // ����:
        //   provider:
        //     System.IFormatProvider �ӿ�ʵ�֣��ṩ�������ض��ĸ�ʽ������Ϣ��
        //
        // ���ؽ��:
        //     ���ʵ����ֵ��Ч�ĵ����ȸ������֡�
        public float ToSingle(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // ժҪ:
        //     ʹ��ָ�����������ض���ʽ������Ϣ����ʵ����ֵת��Ϊ��Ч�� System.String��
        //
        // ����:
        //   provider:
        //     System.IFormatProvider �ӿ�ʵ�֣��ṩ�������ض��ĸ�ʽ������Ϣ��
        //
        // ���ؽ��:
        //     ���ʵ����ֵ��Ч�� System.String ʵ����
        public string ToString(IFormatProvider provider) { return _text; }
        //
        // ժҪ:
        //     ʹ��ָ�����������ض���ʽ������Ϣ����ʵ����ֵת��Ϊ���е�Чֵ��ָ�� System.Type �� System.Object��
        //
        // ����:
        //   provider:
        //     System.IFormatProvider �ӿ�ʵ�֣��ṩ�������ض��ĸ�ʽ������Ϣ��
        //
        //   conversionType:
        //     Ҫ����ʵ����ֵת��Ϊ�� System.Type��
        //
        // ���ؽ��:
        //     ��ֵ���ʵ��ֵ��Ч�� conversionType ���͵� System.Object ʵ����
        public object ToType(Type conversionType, IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // ժҪ:
        //     ʹ��ָ�����������ض���ʽ������Ϣ����ʵ����ֵת��Ϊ��Ч�� 16 λ�޷���������
        //
        // ����:
        //   provider:
        //     System.IFormatProvider �ӿ�ʵ�֣��ṩ�������ض��ĸ�ʽ������Ϣ��
        //
        // ���ؽ��:
        //     ���ʵ����ֵ��Ч�� 16 λ�޷���������
        public ushort ToUInt16(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // ժҪ:
        //     ʹ��ָ�����������ض���ʽ������Ϣ����ʵ����ֵת��Ϊ��Ч�� 32 λ�޷���������
        //
        // ����:
        //   provider:
        //     System.IFormatProvider �ӿ�ʵ�֣��ṩ�������ض��ĸ�ʽ������Ϣ��
        //
        // ���ؽ��:
        //     ���ʵ����ֵ��Ч�� 32 λ�޷���������
        public uint ToUInt32(IFormatProvider provider) { throw new NotSupportedException(); }
        //
        // ժҪ:
        //     ʹ��ָ�����������ض���ʽ������Ϣ����ʵ����ֵת��Ϊ��Ч�� 64 λ�޷���������
        //
        // ����:
        //   provider:
        //     System.IFormatProvider �ӿ�ʵ�֣��ṩ�������ض��ĸ�ʽ������Ϣ��
        //
        // ���ؽ��:
        //     ���ʵ����ֵ��Ч�� 64 λ�޷���������
        public ulong ToUInt64(IFormatProvider provider) { throw new NotSupportedException(); }
        #endregion

        #region ��д�麯��
        public override string ToString()
        {
            return _text;
        }
        #endregion
    }
}
