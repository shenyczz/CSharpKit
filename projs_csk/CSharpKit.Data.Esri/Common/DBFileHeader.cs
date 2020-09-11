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
using System.Text;

namespace CSharpKit.Data.Esri
{
    public class DBFileHeader
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xBaseFile"></param>
        public DBFileHeader(IDBFile xBaseFile)
        {
            ParseFileHeader(xBaseFile.Reader);
        }

        #endregion

        #region Fields

        /// <summary>
        /// 版本
        /// </summary>
        /// <remarks>
        /// <para>02H - FoxBase</para>
        /// <para>03H - DBASE III (FoxBase/CLIPPER/FoxPro 与之兼容) foxbase+, foxpro, dbase III+, dbaseIV, no memo - 0x03</para>
        /// <para>04H - dBase IV w/o memo file</para>
        /// <para>05H - dBase V w/o memo file</para>
        /// <para>30H - Visual FoxPro / Visual FoxPro w. DBC</para>
        /// <para>31H - Visual FoxPro w. AutoIncrement field</para>
        /// <para>43H - .dbv memo var size (Flagship)</para>
        /// <para>7BH - dBASE IV with memo</para>
        /// <para>83H - FoxBase+  2.0/2.1  foxbase+, dbase III+ with memo - File with DBT</para>
        /// <para>8BH - dBase IV with memo</para>
        /// <para>CBH - dBase IV with SQL table - 0x8e</para>
        /// <para>E5H - Clipper SIX driver w. SMT memo file.</para>
        /// <para>F5H - FoxPro 2.0/2.5  foxpro with memo - 0xf5</para>
        /// </remarks>
        Byte _Version;

        /// <summary>
        /// 文件更新日期(yymmdd) - 年
        /// </summary>
        /// <remarks>
        /// Base on 1900. The max year = 1900 + 255 = 2155
        /// </remarks>
        Byte _Year;

        /// <summary>
        /// 文件更新日期(yymmdd) - 月
        /// </summary>
        Byte _Month;

        /// <summary>
        /// 文件更新日期(yymmdd) - 日
        /// </summary>
        Byte _Day;

        /// <summary>
        /// 记录数量
        /// </summary>
        Int32 _RecordCount;

        /// <summary>
        /// 文件头 + 字段结构描述部分的长度 + 1(0x0d)
        /// </summary>
        Int16 _HeaderLength;

        /// <summary>
        /// 每条记录的长度
        /// </summary>
        Int16 _RecordLength;

        /// <summary>
        /// 保留
        /// </summary>
        Byte[] _Reserved = new Byte[17];

        /// <summary>
        /// 语言驱动 ID
        /// </summary>
        Byte _LanguageDriverId;

        /// <summary>
        /// 标记
        /// </summary>
        Byte[] _Flag = new Byte[2];

        #endregion

        #region Properties

        /// <summary>
        /// 上次修改日期(最大到2155年)
        /// </summary>
        public DateTime LastUpdate
        {
            get { return new DateTime(1900 + _Year, _Month, _Day); }
        }

        /// <summary>
        /// 文件头长度
        /// </summary>
        /// <remarks>
        /// 文件头 + 字段结构描述部分的长度 + 1(0x0d)
        /// </remarks>
        public Int16 HeaderLength
        {
            get { return _HeaderLength; }
        }

        /// <summary>
        /// 记录数量
        /// </summary>
        public Int32 RecordCount
        {
            get { return _RecordCount; }
        }

        /// <summary>
        /// 记录长度
        /// </summary>
        public Int16 RecordLength
        {
            get { return _RecordLength; }
        }

        /// <summary>
        /// 语言驱动编码
        /// </summary>
        public Encoding LanguageDriverEncoding
        {
            get
            {
                Encoding encoding = Encoding.UTF7;
                switch (_LanguageDriverId)
                {
                    case 0x01:
                        encoding = Encoding.GetEncoding(437);   //DOS USA code page 437 
                        break;
                    case 0x02:
                        encoding = Encoding.GetEncoding(850);   // DOS Multilingual code page 850 
                        break;
                    case 0x03:
                        encoding = Encoding.GetEncoding(1252);  // Windows ANSI code page 1252 
                        break;
                    case 0x04:
                        encoding = Encoding.GetEncoding(10000); // Standard Macintosh 
                        break;
                    case 0x08:
                        encoding = Encoding.GetEncoding(865);   // Danish OEM
                        break;
                    case 0x09:
                        encoding = Encoding.GetEncoding(437);   // Dutch OEM
                        break;
                    case 0x0A:
                        encoding = Encoding.GetEncoding(850);   // Dutch OEM Secondary codepage
                        break;
                    case 0x0B:
                        encoding = Encoding.GetEncoding(437);   // Finnish OEM
                        break;
                    case 0x0D:
                        encoding = Encoding.GetEncoding(437);   // French OEM
                        break;
                    case 0x0E:
                        encoding = Encoding.GetEncoding(850);   // French OEM Secondary codepage
                        break;
                    case 0x0F:
                        encoding = Encoding.GetEncoding(437);   // German OEM
                        break;
                    case 0x10:
                        encoding = Encoding.GetEncoding(850);   // German OEM Secondary codepage
                        break;
                    case 0x11:
                        encoding = Encoding.GetEncoding(437);   // Italian OEM
                        break;
                    case 0x12:
                        encoding = Encoding.GetEncoding(850);   // Italian OEM Secondary codepage
                        break;
                    case 0x13:
                        encoding = Encoding.GetEncoding(932);   // Japanese Shift-JIS
                        break;
                    case 0x14:
                        encoding = Encoding.GetEncoding(850);   // Spanish OEM secondary codepage
                        break;
                    case 0x15:
                        encoding = Encoding.GetEncoding(437);   // Swedish OEM
                        break;
                    case 0x16:
                        encoding = Encoding.GetEncoding(850);   // Swedish OEM secondary codepage
                        break;
                    case 0x17:
                        encoding = Encoding.GetEncoding(865);   // Norwegian OEM
                        break;
                    case 0x18:
                        encoding = Encoding.GetEncoding(437);   // Spanish OEM
                        break;
                    case 0x19:
                        encoding = Encoding.GetEncoding(437);   // English OEM (Britain)
                        break;
                    case 0x1A:
                        encoding = Encoding.GetEncoding(850);   // English OEM (Britain) secondary codepage
                        break;
                    case 0x1B:
                        encoding = Encoding.GetEncoding(437);   // English OEM (U.S.)
                        break;
                    case 0x1C:
                        encoding = Encoding.GetEncoding(863);   // French OEM (Canada)
                        break;
                    case 0x1D:
                        encoding = Encoding.GetEncoding(850);   // French OEM secondary codepage
                        break;
                    case 0x1F:
                        encoding = Encoding.GetEncoding(852);   // Czech OEM
                        break;
                    case 0x22:
                        encoding = Encoding.GetEncoding(852);   // Hungarian OEM
                        break;
                    case 0x23:
                        encoding = Encoding.GetEncoding(852);   // Polish OEM
                        break;
                    case 0x24:
                        encoding = Encoding.GetEncoding(860);   // Portuguese OEM
                        break;
                    case 0x25:
                        encoding = Encoding.GetEncoding(850);   // Portuguese OEM secondary codepage
                        break;
                    case 0x26:
                        encoding = Encoding.GetEncoding(866);   // Russian OEM
                        break;
                    case 0x37:
                        encoding = Encoding.GetEncoding(850);   // English OEM (U.S.) secondary codepage
                        break;
                    case 0x40:
                        encoding = Encoding.GetEncoding(852);   // Romanian OEM
                        break;
                    case 0x4D:
                        encoding = Encoding.GetEncoding(936);   // Chinese GBK (PRC)
                        break;
                    case 0x4E:
                        encoding = Encoding.GetEncoding(949);   // Korean (ANSI/OEM)
                        break;
                    case 0x4F:
                        encoding = Encoding.GetEncoding(950);   // Chinese Big5 (Taiwan)
                        break;
                    case 0x50:
                        encoding = Encoding.GetEncoding(874);   // Thai (ANSI/OEM)
                        break;
                    case 0x57:
                        encoding = Encoding.GetEncoding(1252);  // ANSI
                        break;
                    case 0x58:
                        encoding = Encoding.GetEncoding(1252);  // Western European ANSI
                        break;
                    case 0x59:
                        encoding = Encoding.GetEncoding(1252);  // Spanish ANSI
                        break;
                    case 0x64:
                        encoding = Encoding.GetEncoding(852);   // Eastern European MSDOS
                        break;
                    case 0x65:
                        encoding = Encoding.GetEncoding(866);   // Russian MSDOS
                        break;
                    case 0x66:
                        encoding = Encoding.GetEncoding(865);   // Nordic MSDOS
                        break;
                    case 0x67:
                        encoding = Encoding.GetEncoding(861);   // Icelandic MSDOS
                        break;
                    case 0x68:
                        encoding = Encoding.GetEncoding(895);   // Kamenicky (Czech) MS-DOS 
                        break;
                    case 0x69:
                        encoding = Encoding.GetEncoding(620);   // Mazovia (Polish) MS-DOS 
                        break;
                    case 0x6A:
                        encoding = Encoding.GetEncoding(737);   // Greek MSDOS (437G)
                        break;
                    case 0x6B:
                        encoding = Encoding.GetEncoding(857);   // Turkish MSDOS
                        break;
                    case 0x6C:
                        encoding = Encoding.GetEncoding(863);   // FrenchCanadian MSDOS
                        break;
                    case 0x78:
                        encoding = Encoding.GetEncoding(950);   // Taiwan Big 5
                        break;
                    case 0x79:
                        encoding = Encoding.GetEncoding(949);   // Hangul (Wansung)
                        break;
                    case 0x7A:
                        encoding = Encoding.GetEncoding(936);   // PRC GBK
                        break;
                    case 0x7B:
                        encoding = Encoding.GetEncoding(932);   // Japanese Shift-JIS
                        break;
                    case 0x7C:
                        encoding = Encoding.GetEncoding(874);   // Thai Windows/MSDOS
                        break;
                    case 0x7D:
                        encoding = Encoding.GetEncoding(1255);  // Hebrew Windows 
                        break;
                    case 0x7E:
                        encoding = Encoding.GetEncoding(1256);  // Arabic Windows 
                        break;
                    case 0x86:
                        encoding = Encoding.GetEncoding(737);   // Greek OEM
                        break;
                    case 0x87:
                        encoding = Encoding.GetEncoding(852);   // Slovenian OEM
                        break;
                    case 0x88:
                        encoding = Encoding.GetEncoding(857);   // Turkish OEM
                        break;
                    case 0x96:
                        encoding = Encoding.GetEncoding(10007); // Russian Macintosh 
                        break;
                    case 0x97:
                        encoding = Encoding.GetEncoding(10029); // Eastern European Macintosh 
                        break;
                    case 0x98:
                        encoding = Encoding.GetEncoding(10006); // Greek Macintosh 
                        break;
                    case 0xC8:
                        encoding = Encoding.GetEncoding(1250);  // Eastern European Windows
                        break;
                    case 0xC9:
                        encoding = Encoding.GetEncoding(1251);  // Russian Windows
                        break;
                    case 0xCA:
                        encoding = Encoding.GetEncoding(1254);  // Turkish Windows
                        break;
                    case 0xCB:
                        encoding = Encoding.GetEncoding(1253);  // Greek Windows
                        break;
                    case 0xCC:
                        encoding = Encoding.GetEncoding(1257);  // Baltic Windows
                        break;
                    default:
                        encoding = Encoding.UTF7;
                        break;
                }
                return encoding;
            }
        }

        /// <summary>
        /// 字段数量
        /// </summary>
        /// <remarks>（文件头长度 - 32 字节）/ 32 </remarks>
        public int FieldCount
        {
            get { return (_HeaderLength - 31) / 32; }
        }

        /// <summary>
        /// 字段集合
        /// </summary>
        internal DBFields XBaseFields { get; private set; }

        #endregion

        #region Private Functions

        /// <summary>
        /// 解析文件头
        /// </summary>
        /// <param name="br"></param>
        void ParseFileHeader(BinaryReader br)
        {
            //---------------------------------------------
            // 1.XBase 文件头 - 32字节
            _Version = br.ReadByte();           // 版本      1 byte
            _Year = br.ReadByte();              // 年        1 byte
            _Month = br.ReadByte();             // 月        1 byte
            _Day = br.ReadByte();               // 日        1 byte

            _RecordCount = br.ReadInt32();   // 记录数量   4 bytes
            _HeaderLength = br.ReadInt16();    // 文件头长度 2 bytes
            _RecordLength = br.ReadInt16();    // 记录长度   2 bytes

            _Reserved = br.ReadBytes(17);       // 保留       17 bytes

            _LanguageDriverId = br.ReadByte();  // 语言驱动id 1 byte

            _Flag = br.ReadBytes(2);            // 保留       2 bytes
            //---------------------------------------------
            if (_Version != 0x03)
                throw new NotSupportedException("Unsupported DBF Type");
            //---------------------------------------------
            // 2.读取字段信息
            int fldCount = FieldCount;
            XBaseFields = new DBFields();
            for (int i = 0; i < fldCount; i++)
            {
                XBaseFields.Add(new DBField(br));
            }
            //---------------------------------------------
            // 3.文件头终止符 = 0x0d
            Byte temp = br.ReadByte();
            //---------------------------------------------
            // 设置显示字段
            // 默认是 Name, 如果没有 Name 字段，选择第一个字段
            if (!XBaseFields.HasDisplayField)
            {
                if (XBaseFields.Count > 0)
                    XBaseFields.EnableDisplayField(0);
            }
            //---------------------------------------------
            return;
        }

        #endregion
    }


}
