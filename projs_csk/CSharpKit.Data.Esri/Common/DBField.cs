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
using System.IO;
using CSharpKit.Data;

namespace CSharpKit.Data.Esri
{
    /// <summary>
    /// XBaseField - XBase字段描述(32字节)
    /// </summary>
    /// <remarks>
    /// <para>FieldName     - 字段名称(11字节)</para>
    /// <para>FieldType     - 字段类型( 1字节)</para>
    /// <para>FieldOffset   - 字段偏移( 4字节)</para>
    /// <para>FieldLength   - 字段宽度( 1字节)</para>
    /// <para>FieldDecimal  - 字段小数( 1字节)</para>
    /// <para>Reserved[14]  - 字段保留(14字节)</para>
    /// <para></para>
    /// </remarks>
    public sealed class DBField
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="br"></param>
        public DBField(BinaryReader br)
        {
            ReadFild(br);
        }

        #endregion

        #region Fields

        Byte[] _FieldName = new Byte[11];   // 11
        Byte _FieldType;                    // 1
        UInt32 _FieldOffset;                // 4
        Byte _FieldLength;                  // 1
        Byte _FieldDecimal;                 // 1
        Byte[] _Reserved = new Byte[14];    // 14
        // TotalByte =                              // 32

        #endregion

        #region Properties

        /// <summary>
        /// 字段名称
        /// </summary>
        /// <remarks>
        /// 由字母、下划线、汉字、数字组成, 但第一个字符不能是数字,中间也不允许有空格
        /// </remarks>
        public String FieldName
        {
            get { return System.Text.Encoding.Default.GetString(_FieldName).Replace("\0", "").Trim(); }
            set
            {
                if (value != FieldName)
                {
                    _FieldName = System.Text.Encoding.Default.GetBytes(value);
                }
            }
        }

        /// <summary>
        /// 字段类型
        /// </summary>
        /// <remarks>
        /// <para>'B' - Binary      FieldLength = ?      二进制</para>
        /// <para>'C' - Character   FieldLength = 254    字符型字段最大值为254)</para>
        /// <para>'D' - Date        FieldLength = 8      日期型</para>
        /// <para>'F' - Float       FieldLength = 32     浮点型</para>
        /// <para>'G' - General     FieldLength = ??     通用型/OLE</para>
        /// <para>'I' - Interger    FieldLength = 4      整型</para>
        /// <para>'L' - Logical     FieldLength = 1      逻辑型</para>
        /// <para>'M' - Memo        FieldLength = 10     备注型存储一指针</para>
        /// <para>'N' - Numeric     FieldLength = 19     数值型</para>
        /// <para>'T' - DayeTime    FieldLength = 8      日期时间型</para>
        /// </remarks>
        public Type FieldType
        {
            get
            {
                Type type;
                switch ((Char)_FieldType)
                {
                    case 'L':
                        type = typeof(bool);
                        break;
                    case 'C':
                        type = typeof(string);
                        break;
                    case 'D':
                        type = typeof(DateTime);
                        break;
                    case 'N':
                        type = typeof(double);
                        break;
                    case 'F':
                        type = typeof(float);
                        break;
                    case 'B':
                        type = typeof(byte[]);
                        break;
                    default:
                        throw (new NotSupportedException("Invalid or unknown DBase field type '" + FieldType + "' in column '" + FieldName + "'"));
                }
                return type;
            }
            set
            {
                if (FieldType == value)
                    return;

                FieldType = value;
                switch (value.ToString())
                {
                    case "System.String":
                        _FieldType = (Byte)'C';
                        break;
                    case "System.DateTime":
                        _FieldType = (Byte)'D';
                        break;
                    case "System.Single":
                        _FieldType = (Byte)'F';
                        break;
                    case "":
                        _FieldType = (Byte)'L';
                        break;
                    case "System.Boolean":
                        _FieldType = (Byte)'L';
                        break;
                }
            }
        }

        /// <summary>
        /// 字段数据相对于该记录的偏移量
        /// </summary>
        /// <remarks>
        /// <para>第一个字段一般为00000001H</para>
        /// <para>若为 00000000H 或 大于 00001000H(4096) 则需要通过计算确定</para>
        /// </remarks>
        public UInt32 FieldOffset => _FieldOffset;

        /// <summary>
        /// 字段宽度
        /// </summary>
        /// <remarks>
        /// <para>Binary    ??</para>
        /// <para>Character 254 字节</para>
        /// <para>Date      08H</para>
        /// <para>Float     32 位</para>
        /// <para>General   ??</para>
        /// <para>Interger  4字节</para>
        /// <para>Logical   01H</para>
        /// <para>Memo:     0AH</para>
        /// <para>Numeric   19 位</para>
        /// <para>DayeTime  8字节</para>
        /// <para></para>
        /// </remarks>
        public UInt16 FieldLength
        {
            get { return _FieldLength; }
            set { _FieldLength = (Byte)value; }
        }

        /// <summary>
        /// 小数位数
        /// </summary>
        /// <remarks>
        /// <para>(Character/Date/Logical/Memo 00H)</para>
        /// </remarks>
        public UInt16 FieldDecimal
        {
            get { return _FieldDecimal; }
            set { _FieldDecimal = (Byte)value; }
        }

        /// <summary>
        /// 是否显示字段
        /// </summary>
        public Boolean IsDisplayField { get; set; }

        #endregion

        #region Private Functions

        /// <summary>
        /// 读取字段描述(32字节)
        /// </summary>
        /// <param name="br"></param>
        private void ReadFild(BinaryReader br)
        {
            _FieldName = br.ReadBytes(11);
            _FieldType = br.ReadByte();
            _FieldOffset = br.ReadUInt32();
            _FieldLength = br.ReadByte();
            _FieldDecimal = br.ReadByte();
            _Reserved = br.ReadBytes(14);

            // 字段名称是
            if (FieldName == "Name" || FieldName == "名称")
            {
                IsDisplayField = true;
            }
        }

        #endregion
    }

}
