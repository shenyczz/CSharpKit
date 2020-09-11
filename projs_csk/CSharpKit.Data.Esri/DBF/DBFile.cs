/******************************************************************************
 * 
 * Announce: Meteorological Toolkit（MTK）.
 *           Copyright (C) ShenYongchen.
 *           All rights reserved.
 *   Author: 申永辰.郑州 (shenyczz@163.com)
 *  WebSite: http://github.com/shenyczz/meteoToolkit
 *
 * THIS CODE IS LICENSED UNDER THE MIT LICENSE (MIT).
 * THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF 
 * ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
 * IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
 * PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
 * 
******************************************************************************/
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using CSharpKit.Data;

namespace CSharpKit.Data.Esri
{
    /// <summary>
    /// XBaseFile - ESRI 空间数据的属性数据(*.dbf)
    /// </summary>
    public class DBFile : FeatureDataTable, IDBFile, IDisposable
    {
        #region Constructors

        public DBFile(String tableName)
            : base(tableName)
        {
            // 数据处理器
            this.Processor = new DBFileProcessor(this);

            // 打开文件
            Open();

            if (IsOpen)
            {
                // 构造文件头
                _xBaseFileHeader = new DBFileHeader(this);
                // 创建基本表
                createTable();
                // 填充表
                fillTable();

                // 关闭
                Close();
            }
        }

        #endregion

        #region Fields

        private FileStream _fileStream;
        private DBFileHeader _xBaseFileHeader;

        #endregion

        #region Properties

        /// <summary>
        /// 调色板
        /// </summary>
        //public IPalette Palette { get; set; }

        ///// <summary>
        ///// 文件名称
        ///// </summary>
        public String FileName
        {
            get { return this.TableName; }
            set
            {
                this.TableName = value;
                this.ConnectionString = value;
            }
        }

        /// <summary>
        /// 是否打开
        /// </summary>
        public Boolean IsOpen { get; set; }

        private BinaryReader _BinaryReader;
        /// <summary>
        /// 二进制读取器
        /// </summary>
        public BinaryReader Reader
        {
            get { return _BinaryReader; }
        }

        /// <summary>
        /// 字段集合
        /// </summary>
        public DBFields XBaseFields
        {
            get { return _xBaseFileHeader.XBaseFields; }
        }

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime LastUpdate
        {
            get { return _xBaseFileHeader.LastUpdate; }
        }

        /// <summary>
        /// 文件头长度
        /// </summary>
        public Int16 HeaderLength
        {
            get { return _xBaseFileHeader.HeaderLength; }
        }

        /// <summary>
        /// 记录数量
        /// </summary>
        public Int32 RecordCount
        {
            get { return _xBaseFileHeader.RecordCount; }
        }

        /// <summary>
        /// 记录长度
        /// </summary>
        public Int16 RecordLength
        {
            get { return _xBaseFileHeader.RecordLength; }
        }

        /// <summary>
        /// 语言驱动编码 LDC
        /// </summary>
        public Encoding LanguageDriverEncoding
        {
            get { return _xBaseFileHeader.LanguageDriverEncoding; }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// 打开
        /// </summary>
        public void Open()
        {
            String fileName = this.TableName;
            if (!File.Exists(fileName))
                throw new FileNotFoundException("文件不存在!");

            _fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
            _BinaryReader = new BinaryReader(_fileStream);
            IsOpen = true;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            if (_BinaryReader != null)
            {
                _BinaryReader.Close();
                _BinaryReader = null;
            }
            if (_fileStream != null)
            {
                _fileStream.Close();
                _fileStream = null;
            }

            IsOpen = false;
        }

        //public BinaryTree<T, UInt32> CreateDbfIndex<T>(int ColumnId) where T : IComparable<T>
        //{
        //    BinaryTree<T, UInt32> tree = new BinaryTree<T, uint>();

        //    //for (uint i = 0; i < ((_NumberOfRecords > 10000) ? 10000 : _NumberOfRecords); i++)
        //    //    tree.Add(new BinaryTree<T, uint>.ItemValue((T)this.GetValue(i, ColumnId), i));

        //    return tree;
        //}

        /// <summary>
        /// 语言驱动
        /// </summary>
        /// <param name="dbaseCode"></param>
        /// <returns></returns>
        public Encoding GetXBaseLanguageDriver(byte dbaseCode)
        {
            switch (dbaseCode)
            {
                case 0x01:
                    return Encoding.GetEncoding(437); //DOS USA code page 437 
                case 0x02:
                    return Encoding.GetEncoding(850); // DOS Multilingual code page 850 
                case 0x03:
                    return Encoding.GetEncoding(1252); // Windows ANSI code page 1252 
                case 0x04:
                    return Encoding.GetEncoding(10000); // Standard Macintosh 
                case 0x08:
                    return Encoding.GetEncoding(865); // Danish OEM
                case 0x09:
                    return Encoding.GetEncoding(437); // Dutch OEM
                case 0x0A:
                    return Encoding.GetEncoding(850); // Dutch OEM Secondary codepage
                case 0x0B:
                    return Encoding.GetEncoding(437); // Finnish OEM
                case 0x0D:
                    return Encoding.GetEncoding(437); // French OEM
                case 0x0E:
                    return Encoding.GetEncoding(850); // French OEM Secondary codepage
                case 0x0F:
                    return Encoding.GetEncoding(437); // German OEM
                case 0x10:
                    return Encoding.GetEncoding(850); // German OEM Secondary codepage
                case 0x11:
                    return Encoding.GetEncoding(437); // Italian OEM
                case 0x12:
                    return Encoding.GetEncoding(850); // Italian OEM Secondary codepage
                case 0x13:
                    return Encoding.GetEncoding(932); // Japanese Shift-JIS
                case 0x14:
                    return Encoding.GetEncoding(850); // Spanish OEM secondary codepage
                case 0x15:
                    return Encoding.GetEncoding(437); // Swedish OEM
                case 0x16:
                    return Encoding.GetEncoding(850); // Swedish OEM secondary codepage
                case 0x17:
                    return Encoding.GetEncoding(865); // Norwegian OEM
                case 0x18:
                    return Encoding.GetEncoding(437); // Spanish OEM
                case 0x19:
                    return Encoding.GetEncoding(437); // English OEM (Britain)
                case 0x1A:
                    return Encoding.GetEncoding(850); // English OEM (Britain) secondary codepage
                case 0x1B:
                    return Encoding.GetEncoding(437); // English OEM (U.S.)
                case 0x1C:
                    return Encoding.GetEncoding(863); // French OEM (Canada)
                case 0x1D:
                    return Encoding.GetEncoding(850); // French OEM secondary codepage
                case 0x1F:
                    return Encoding.GetEncoding(852); // Czech OEM
                case 0x22:
                    return Encoding.GetEncoding(852); // Hungarian OEM
                case 0x23:
                    return Encoding.GetEncoding(852); // Polish OEM
                case 0x24:
                    return Encoding.GetEncoding(860); // Portuguese OEM
                case 0x25:
                    return Encoding.GetEncoding(850); // Portuguese OEM secondary codepage
                case 0x26:
                    return Encoding.GetEncoding(866); // Russian OEM
                case 0x37:
                    return Encoding.GetEncoding(850); // English OEM (U.S.) secondary codepage
                case 0x40:
                    return Encoding.GetEncoding(852); // Romanian OEM
                case 0x4D:
                    return Encoding.GetEncoding(936); // Chinese GBK (PRC)
                case 0x4E:
                    return Encoding.GetEncoding(949); // Korean (ANSI/OEM)
                case 0x4F:
                    return Encoding.GetEncoding(950); // Chinese Big5 (Taiwan)
                case 0x50:
                    return Encoding.GetEncoding(874); // Thai (ANSI/OEM)
                case 0x57:
                    return Encoding.GetEncoding(1252); // ANSI
                case 0x58:
                    return Encoding.GetEncoding(1252); // Western European ANSI
                case 0x59:
                    return Encoding.GetEncoding(1252); // Spanish ANSI
                case 0x64:
                    return Encoding.GetEncoding(852); // Eastern European MSDOS
                case 0x65:
                    return Encoding.GetEncoding(866); // Russian MSDOS
                case 0x66:
                    return Encoding.GetEncoding(865); // Nordic MSDOS
                case 0x67:
                    return Encoding.GetEncoding(861); // Icelandic MSDOS
                case 0x68:
                    return Encoding.GetEncoding(895); // Kamenicky (Czech) MS-DOS 
                case 0x69:
                    return Encoding.GetEncoding(620); // Mazovia (Polish) MS-DOS 
                case 0x6A:
                    return Encoding.GetEncoding(737); // Greek MSDOS (437G)
                case 0x6B:
                    return Encoding.GetEncoding(857); // Turkish MSDOS
                case 0x6C:
                    return Encoding.GetEncoding(863); // FrenchCanadian MSDOS
                case 0x78:
                    return Encoding.GetEncoding(950); // Taiwan Big 5
                case 0x79:
                    return Encoding.GetEncoding(949); // Hangul (Wansung)
                case 0x7A:
                    return Encoding.GetEncoding(936); // PRC GBK
                case 0x7B:
                    return Encoding.GetEncoding(932); // Japanese Shift-JIS
                case 0x7C:
                    return Encoding.GetEncoding(874); // Thai Windows/MSDOS
                case 0x7D:
                    return Encoding.GetEncoding(1255); // Hebrew Windows 
                case 0x7E:
                    return Encoding.GetEncoding(1256); // Arabic Windows 
                case 0x86:
                    return Encoding.GetEncoding(737); // Greek OEM
                case 0x87:
                    return Encoding.GetEncoding(852); // Slovenian OEM
                case 0x88:
                    return Encoding.GetEncoding(857); // Turkish OEM
                case 0x96:
                    return Encoding.GetEncoding(10007); // Russian Macintosh 
                case 0x97:
                    return Encoding.GetEncoding(10029); // Eastern European Macintosh 
                case 0x98:
                    return Encoding.GetEncoding(10006); // Greek Macintosh 
                case 0xC8:
                    return Encoding.GetEncoding(1250); // Eastern European Windows
                case 0xC9:
                    return Encoding.GetEncoding(1251); // Russian Windows
                case 0xCA:
                    return Encoding.GetEncoding(1254); // Turkish Windows
                case 0xCB:
                    return Encoding.GetEncoding(1253); // Greek Windows
                case 0xCC:
                    return Encoding.GetEncoding(1257); // Baltic Windows
                default:
                    return Encoding.UTF7;
            }
        }

        /// <summary>
        /// 取得数据表模板
        /// </summary>
        /// <returns>A DataTable that describes the column metadata.</returns>
        public DataTable GetSchemaTable()
        {
            DataTable tab = new DataTable();

            tab.Columns.Add("ColumnName", typeof(String));          // 列名称
            tab.Columns.Add("ColumnSize", typeof(Int32));           // 列大小
            tab.Columns.Add("ColumnOrdinal", typeof(Int32));        // Ordinal - 序数
            tab.Columns.Add("NumericPrecision", typeof(Int16));     // Precision - 精度
            tab.Columns.Add("NumericScale", typeof(Int16));         // ??
            tab.Columns.Add("DataType", typeof(Type));              // 数据类型
            tab.Columns.Add("AllowDBNull", typeof(bool));           // 是否允许 DBNull
            tab.Columns.Add("IsReadOnly", typeof(bool));            // 是否只读
            tab.Columns.Add("IsUnique", typeof(bool));              // 是否唯一(索引)
            tab.Columns.Add("IsRowVersion", typeof(bool));          // 行版本
            tab.Columns.Add("IsKey", typeof(bool));                 // 是否键
            tab.Columns.Add("IsAutoIncrement", typeof(bool));       // 是否允许自动递增
            tab.Columns.Add("IsLong", typeof(bool));                // 是否 Long ?

            foreach (DBField field in XBaseFields)
            {
                tab.Columns.Add(field.FieldName, field.FieldType);
            }

            for (int i = 0; i < XBaseFields.Count; i++)
            {
                DataRow row = tab.NewRow();
                DBField field = XBaseFields[i];
                row["ColumnName"] = field.FieldName;
                row["ColumnSize"] = field.FieldLength;
                row["ColumnOrdinal"] = i;
                row["NumericPrecision"] = field.FieldDecimal;
                row["NumericScale"] = 0;
                row["DataType"] = field.FieldType;
                row["AllowDBNull"] = true;
                row["IsReadOnly"] = false;
                row["IsUnique"] = false;
                row["IsRowVersion"] = false;
                row["IsKey"] = false;
                row["IsAutoIncrement"] = false;
                row["IsLong"] = false;

                tab.Rows.Add(row);
            }

            return tab;
        }

        public void ReLoad(String fileName) { }

        #endregion

        #region internal Functions

        internal DataTable NewTable
        {
            get { return this.Clone(); }
        }

        /*

    /// <summary>
    /// Gets the feature at the specified Object ID
    /// </summary>
    /// <param name="oid">Object ID</param>
    /// <param name="table"></param>
    /// <returns></returns>
    internal IFeature GetFeature(uint oid, IFeatureCollection table)
    {
        if (!IsOpen)
            throw (new ApplicationException("An attempt was made to read from a closed DBF file"));
        if (oid >= _NumberOfRecords)
            throw (new ArgumentException("Invalid DataRow requested at index " + oid.ToString()));
        fs.Seek(_HeaderLength + oid * _RecordLength, 0);

        IFeature dr = table.New();

        if (br.ReadChar() == '*') //is record marked deleted
            return null;

        for (int i = 0; i < XBaseFileHeader.FieldCount; i++)
        {
            XBaseField field = XBaseFileHeader.XBaseFields[i];
            dr[field.FieldName] = readDbfValue(field);
        }

        return dr;
        return null;
    }
         * */

        #endregion

        #region Private Function

        /// <summary>
        /// 根据文件头信息创建数据表
        /// </summary>
        private void createTable()
        {
            foreach (DBField field in XBaseFields)
            {
                DataColumn dataColumn = new DataColumn(field.FieldName, field.FieldType);
                this.Columns.Add(dataColumn);
            }
        }

        /// <summary>
        /// 填充表
        /// </summary>
        private void fillTable()
        {
            int recCount = this.RecordCount;
            for (int i = 0; i < recCount; i++)
            {
                ArrayList objs = getRow((uint)i);     // 记录id从0开始
                if (objs == null)
                    continue;

                this.Rows.Add(objs.ToArray());
            }

            return;
        }

        /// <summary>
        /// 取得记录行
        /// </summary>
        /// <param name="indexRow">row index</param>
        /// <returns></returns>
        private ArrayList getRow(uint indexRow)
        {
            ArrayList objs = new ArrayList();
            int fldCount = this.XBaseFields.Count;
            for (int i = 0; i < fldCount; i++)
            {
                Object obj = getFieldValue(indexRow, i);
                if (obj == null)
                    continue;

                objs.Add(obj);
            }

            return objs.Count != fldCount ? null : objs;
        }

        /// <summary>
        /// 取得指定记录的指定字段值
        /// </summary>
        /// <param name="indexRow">object index</param>
        /// <param name="indexColumn">object column id</param>
        /// <returns></returns>
        private Object getFieldValue(uint indexRow, int indexColumn)
        {
            if (!IsOpen)
                return null;

            // 对象id(从1开始)大于记录数量
            if (indexRow > this.RecordCount)
                return null;

            if (indexColumn < 0 || indexColumn >= this.XBaseFields.Count)
                return null;

            // 定位行
            _fileStream.Seek(this.HeaderLength + indexRow * this.RecordLength, 0);

            // 定位列
            for (int i = 0; i < indexColumn; i++)
            {
                Reader.BaseStream.Seek(this.XBaseFields[i].FieldLength, SeekOrigin.Current);
            }

            // 必须再移动1个字节
            Reader.BaseStream.Seek(1, SeekOrigin.Current);

            // 读取字段值
            object obj = readFieldValue(this.XBaseFields[indexColumn]);
            return obj;
        }

        /// <summary>
        /// 读取 XBase 字段数据
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private Object readFieldValue(DBField field)
        {
            Object obj = null;
            String temp = null;

            Int16 i16;
            UInt16 ui16;
            Int32 i32;
            UInt32 ui32;
            Int64 i64;
            UInt64 ui64;
            Double dbl;
            Single flt;
            DateTime datm;
            switch (field.FieldType.Name)
            {
                case "String":
                    obj = LanguageDriverEncoding.GetString(_BinaryReader.ReadBytes(field.FieldLength)).Replace("\0", "").Trim();
                    break;

                case "Int16":
                    temp = Encoding.UTF7.GetString(_BinaryReader.ReadBytes(field.FieldLength)).Replace("\0", "").Trim();
                    if (Int16.TryParse(temp, out i16))
                        obj = i16;
                    else
                        obj = DBNull.Value;
                    break;

                case "UInt16":
                    temp = Encoding.UTF7.GetString(_BinaryReader.ReadBytes(field.FieldLength)).Replace("\0", "").Trim();
                    if (UInt16.TryParse(temp, out ui16))
                        obj = ui16;
                    else
                        obj = DBNull.Value;
                    break;

                case "Int32":
                    temp = Encoding.UTF7.GetString(_BinaryReader.ReadBytes(field.FieldLength)).Replace("\0", "").Trim();
                    if (Int32.TryParse(temp, out i32))
                        obj = i32;
                    else
                        obj = DBNull.Value;
                    break;

                case "UInt32":
                    temp = Encoding.UTF7.GetString(_BinaryReader.ReadBytes(field.FieldLength)).Replace("\0", "").Trim();
                    if (UInt32.TryParse(temp, out ui32))
                        obj = ui32;
                    else
                        obj = DBNull.Value;
                    break;

                case "Int64":
                    temp = Encoding.UTF7.GetString(_BinaryReader.ReadBytes(field.FieldLength)).Replace("\0", "").Trim();
                    if (Int64.TryParse(temp, out i64))
                        obj = i64;
                    else
                        obj = DBNull.Value;
                    break;

                case "UInt64":
                    temp = Encoding.UTF7.GetString(_BinaryReader.ReadBytes(field.FieldLength)).Replace("\0", "").Trim();
                    if (UInt64.TryParse(temp, out ui64))
                        obj = ui64;
                    else
                        obj = DBNull.Value;
                    break;

                case "Single":
                    temp = Encoding.UTF8.GetString(_BinaryReader.ReadBytes(field.FieldLength)).Replace("\0", "").Trim();
                    if (Single.TryParse(temp, out flt))
                        obj = flt;
                    else
                        obj = DBNull.Value;
                    break;

                case "Double":
                    temp = LanguageDriverEncoding.GetString(_BinaryReader.ReadBytes(field.FieldLength)).Replace("\0", "").Trim();
                    if (Double.TryParse(temp, out dbl))
                        obj = dbl;
                    else
                        obj = DBNull.Value;
                    break;

                case "Boolean":
                    char ctemp = _BinaryReader.ReadChar();
                    obj = (ctemp == 'T' || ctemp == 't' || ctemp == 'Y' || ctemp == 'y');
                    break;

                case "DateTime":
                    temp = Encoding.UTF7.GetString(_BinaryReader.ReadBytes(field.FieldLength)).Replace("\0", "").Trim();
                    if (DateTime.TryParseExact(temp, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out datm))
                        obj = datm;
                    else
                        obj = DBNull.Value;
                    break;

                default:
                    obj = null;
                    break;
            }

            return obj;
        }

        #endregion

        #region IDisposable 成员

        /// <summary>
        /// 释放资源
        /// </summary>
        public new void Dispose()
        {
            if (IsOpen)
                Close();

            base.Dispose(true);
        }

        #endregion
    
        //}}@@@
    }

}
