/******************************************************************************
 * 
 * Announce: CSharpKit, Basic algorithms, components and definitions.
 *           Copyright (C) ShenYongchen.
 *           All rights reserved.
 *   Author: 申永辰.郑州 (shenyczz@163.com)
 *  WebSite: http://github.com/shenyczz/CSharpKit
 *
 * THIS CODE IS LICENSED UNDER THE MIT LICENSE (MIT).
 * THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF 
 * ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
 * IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
 * PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
 * 
******************************************************************************/
using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace CSharpKit.Platform.Windows
{
    /// <summary>
    /// Variant COM.
    /// </summary>
    /// <unmanaged>PROPVARIANT</unmanaged>
    [StructLayout(LayoutKind.Sequential)]
    public struct Variant
    {
        private ushort _vt;
        private ushort reserved1;
        private ushort reserved2;
        private ushort reserved3;
        private VariantValue _variantValue;


        #region VariantElementType

        /// <summary>
        /// Gets the type of the element.
        /// </summary>
        /// <value>
        /// The type of the element.
        /// </value>
        public VariantElementType ElementType
        {
            get
            {
                return (VariantElementType)(_vt & 0x0fff);
            }
            set
            {
                _vt = (ushort)((_vt & 0xf000) | (ushort)value);
            }
        }

        #endregion


        #region VariantType

        /// <summary>
        /// Gets the type.
        /// </summary>
        public VariantType Type
        {
            get
            {
                return (VariantType)(_vt & 0xf000);
            }
            set
            {
                _vt = (ushort)((_vt & 0x0fff) | (ushort)value);
            }
        }

        #endregion


        #region Value

        /*
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public unsafe object Value
        {
            get
            {
                switch (Type)
                {
                    case VariantType.Default:
                        switch (ElementType)
                        {
                            case VariantElementType.Empty:
                            case VariantElementType.Null:
                                return null;
                            case VariantElementType.Blob:
                            {
                                var buffer = new byte[(int)_variantValue.recordValue.RecordInfo];
                                if (buffer.Length > 0)
                                {
                                    Utilities.Read(_variantValue.recordValue.RecordPointer, buffer, 0, buffer.Length);
                                }
                                return buffer;
                            }
                            case VariantElementType.Bool:
                                return _variantValue.intValue != 0;
                            case VariantElementType.Byte:
                                return _variantValue.signedByteValue;
                            case VariantElementType.UByte:
                                return _variantValue.byteValue;
                            case VariantElementType.UShort:
                                return _variantValue.ushortValue;
                            case VariantElementType.Short:
                                return _variantValue.shortValue;
                            case VariantElementType.UInt:
                            case VariantElementType.UInt1:
                                return _variantValue.uintValue;
                            case VariantElementType.Int:
                            case VariantElementType.Int1:
                                return _variantValue.intValue;
                            case VariantElementType.ULong:
                                return _variantValue.ulongValue;
                            case VariantElementType.Long:
                                return _variantValue.longValue;
                            case VariantElementType.Float:
                                return _variantValue.floatValue;
                            case VariantElementType.Double:
                                return _variantValue.doubleValue;
                            case VariantElementType.BinaryString:
                                throw new NotSupportedException();
                            //return Marshal.PtrToStringBSTR(variantValue.pointerValue);
                            case VariantElementType.StringPointer:
                                return Marshal.PtrToStringAnsi(_variantValue.pointerValue);
                            case VariantElementType.WStringPointer:
                                return Marshal.PtrToStringUni(_variantValue.pointerValue);
                            case VariantElementType.ComUnknown:
                            case VariantElementType.Dispatch:
                                return new ComObject(_variantValue.pointerValue);
                            case VariantElementType.IntPointer:
                            case VariantElementType.Pointer:
                                return _variantValue.pointerValue;
                            case VariantElementType.FileTime:
                                return DateTime.FromFileTime(_variantValue.longValue);
                            default:
                                return null;
                        }
                    case VariantType.Vector:
                        int size = (int)_variantValue.recordValue.RecordInfo;
                        switch (ElementType)
                        {
                            case VariantElementType.Bool:
                            {
                                var array = new RawBool[size];
                                Utilities.Read(_variantValue.recordValue.RecordPointer, array, 0, size);
                                return Utilities.ConvertToBoolArray(array);
                            }
                            case VariantElementType.Byte:
                            {
                                var array = new sbyte[size];
                                Utilities.Read(_variantValue.recordValue.RecordPointer, array, 0, size);
                                return array;
                            }
                            case VariantElementType.UByte:
                            {
                                var array = new byte[size];
                                Utilities.Read(_variantValue.recordValue.RecordPointer, array, 0, size);
                                return array;
                            }
                            case VariantElementType.UShort:
                            {
                                var array = new ushort[size];
                                Utilities.Read(_variantValue.recordValue.RecordPointer, array, 0, size);
                                return array;
                            }
                            case VariantElementType.Short:
                            {
                                var array = new short[size];
                                Utilities.Read(_variantValue.recordValue.RecordPointer, array, 0, size);
                                return array;
                            }
                            case VariantElementType.UInt:
                            case VariantElementType.UInt1:
                            {
                                var array = new uint[size];
                                Utilities.Read(_variantValue.recordValue.RecordPointer, array, 0, size);
                                return array;
                            }
                            case VariantElementType.Int:
                            case VariantElementType.Int1:
                            {
                                var array = new int[size];
                                Utilities.Read(_variantValue.recordValue.RecordPointer, array, 0, size);
                                return array;
                            }
                            case VariantElementType.ULong:
                            {
                                var array = new ulong[size];
                                Utilities.Read(_variantValue.recordValue.RecordPointer, array, 0, size);
                                return array;
                            }
                            case VariantElementType.Long:
                            {
                                var array = new long[size];
                                Utilities.Read(_variantValue.recordValue.RecordPointer, array, 0, size);
                                return array;
                            }
                            case VariantElementType.Float:
                            {
                                var array = new float[size];
                                Utilities.Read(_variantValue.recordValue.RecordPointer, array, 0, size);
                                return array;
                            }
                            case VariantElementType.Double:
                            {
                                var array = new double[size];
                                Utilities.Read(_variantValue.recordValue.RecordPointer, array, 0, size);
                                return array;
                            }
                            case VariantElementType.BinaryString:
                            {
                                throw new NotSupportedException();
                                //var array = new string[size];
                                //for (int i = 0; i < size; i++)
                                //    array[i] = Marshal.PtrToStringBSTR(((IntPtr*)variantValue.recordValue.RecordPointer)[i]);
                                //return array;
                            }
                            case VariantElementType.StringPointer:
                            {
                                var array = new string[size];
                                for (int i = 0; i < size; i++)
                                    array[i] = Marshal.PtrToStringAnsi(((IntPtr*)_variantValue.recordValue.RecordPointer)[i]);
                                return array;
                            }
                            case VariantElementType.WStringPointer:
                            {
                                var array = new string[size];
                                for (int i = 0; i < size; i++)
                                    array[i] = Marshal.PtrToStringUni(((IntPtr*)_variantValue.recordValue.RecordPointer)[i]);
                                return array;
                            }
                            case VariantElementType.ComUnknown:
                            case VariantElementType.Dispatch:
                            {
                                var comArray = new ComObject[size];
                                for (int i = 0; i < size; i++)
                                    comArray[i] = new ComObject(((IntPtr*)_variantValue.recordValue.RecordPointer)[i]);
                                return comArray;
                            }
                            case VariantElementType.IntPointer:
                            case VariantElementType.Pointer:
                            {
                                var array = new IntPtr[size];
                                Utilities.Read(_variantValue.recordValue.RecordPointer, array, 0, size);
                                return array;
                            }
                            case VariantElementType.FileTime:
                            {
                                var fileTimeArray = new DateTime[size];
                                for (int i = 0; i < size; i++)
                                    fileTimeArray[i] = DateTime.FromFileTime(((long*)_variantValue.recordValue.RecordPointer)[i]);
                                return fileTimeArray;
                            }
                            default:
                                return null;
                        }
                }
                return null;
            }
            set
            {
                if (value == null)
                {
                    Type = VariantType.Default;
                    ElementType = VariantElementType.Null;
                    return;
                }
                var type = value.GetType();

                Type = VariantType.Default;
                if (type.GetTypeInfo().IsPrimitive)
                {
                    if (type == typeof(byte))
                    {
                        ElementType = VariantElementType.UByte;
                        _variantValue.byteValue = (byte)value;
                        return;
                    }

                    if (type == typeof(sbyte))
                    {
                        ElementType = VariantElementType.Byte;
                        _variantValue.signedByteValue = (sbyte)value;
                        return;
                    }

                    if (type == typeof(int))
                    {
                        ElementType = VariantElementType.Int;
                        _variantValue.intValue = (int)value;
                        return;
                    }

                    if (type == typeof(uint))
                    {
                        ElementType = VariantElementType.UInt;
                        _variantValue.uintValue = (uint)value;
                        return;
                    }

                    if (type == typeof(long))
                    {
                        ElementType = VariantElementType.Long;
                        _variantValue.longValue = (long)value;
                        return;
                    }

                    if (type == typeof(ulong))
                    {
                        ElementType = VariantElementType.ULong;
                        _variantValue.ulongValue = (ulong)value;
                        return;
                    }

                    if (type == typeof(short))
                    {
                        ElementType = VariantElementType.Short;
                        _variantValue.shortValue = (short)value;
                        return;
                    }

                    if (type == typeof(ushort))
                    {
                        ElementType = VariantElementType.UShort;
                        _variantValue.ushortValue = (ushort)value;
                        return;
                    }

                    if (type == typeof(float))
                    {
                        ElementType = VariantElementType.Float;
                        _variantValue.floatValue = (float)value;
                        return;
                    }

                    if (type == typeof(double))
                    {
                        ElementType = VariantElementType.Double;
                        _variantValue.doubleValue = (double)value;
                        return;
                    }
                }
                else if (value is ComObject)
                {
                    ElementType = VariantElementType.ComUnknown;
                    _variantValue.pointerValue = ((ComObject)value).NativePointer;
                    return;
                }
                else if (value is DateTime)
                {
                    ElementType = VariantElementType.FileTime;
                    _variantValue.longValue = ((DateTime)value).ToFileTime();
                    return;
                }
                else if (value is string)
                {
                    ElementType = VariantElementType.WStringPointer;
                    _variantValue.pointerValue = Marshal.StringToCoTaskMemUni((string)value);
                    return;
                }
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Type [{0}] is not handled", type.Name));
            }
        }

        */

        #endregion


        #region VariantValue

        [StructLayout(LayoutKind.Explicit)]
        private struct VariantValue
        {
            [FieldOffset(0)]
            public byte byteValue;
            [FieldOffset(0)]
            public sbyte signedByteValue;
            [FieldOffset(0)]
            public ushort ushortValue;
            [FieldOffset(0)]
            public short shortValue;
            [FieldOffset(0)]
            public uint uintValue;
            [FieldOffset(0)]
            public int intValue;
            [FieldOffset(0)]
            public ulong ulongValue;
            [FieldOffset(0)]
            public long longValue;
            [FieldOffset(0)]
            public float floatValue;
            [FieldOffset(0)]
            public double doubleValue;
            [FieldOffset(0)]
            public IntPtr pointerValue;
            [FieldOffset(0)]
            public CurrencyValue currencyValue;
            [FieldOffset(0)]
            public RecordValue recordValue;

            [StructLayout(LayoutKind.Sequential)]
            public struct CurrencyLowHigh
            {
                public uint LowValue;
                public int HighValue;
            }

            [StructLayout(LayoutKind.Explicit)]
            public struct CurrencyValue
            {
                [FieldOffset(0)]
                public CurrencyLowHigh LowHigh;
                [FieldOffset(0)]
                public long longValue;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct RecordValue
            {
                public IntPtr RecordInfo;
                public IntPtr RecordPointer;
            }
        }

        #endregion



        //@EndOf(Variant)
    }


}
