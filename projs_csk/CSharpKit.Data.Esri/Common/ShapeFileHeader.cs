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
using CSharpKit.GeoApi;

namespace CSharpKit.Data.Esri
{
    /// <summary>
    /// ShapeFileHeader - Shape 文件头
    /// </summary>
    public sealed class ShapeFileHeader
    {
        #region Constructors

        public ShapeFileHeader(IShapeFile shapeFile)
        {
            String fileName = shapeFile.IndexFileName;
            if (!File.Exists(fileName))
            {
                throw new Exception(fileName);
            }

            using (BinaryReader reader = new BinaryReader(File.OpenRead(fileName)))
            {
                this.ParseHeader(reader);
            }
        }

        #endregion

        #region Constants

        /// <summary>
        /// 文件头大小
        /// </summary>
        public const Int32 HeaderSize = 100;

        /// <summary>
        /// 文件代码(BE)
        /// </summary>
        public const Int32 FileCode = 0x270a;

        #endregion

        #region Fields

        Int32 _FileCode;    // [BE]
        //Int32 _Unused1;     // [BE] 
        //Int32 _Unused2;     // [BE] 
        //Int32 _Unused3;     // [BE] 
        //Int32 _Unused4;     // [BE] 
        //Int32 _Unused5;     // [BE] 
        Int32 _FileLength;  // [BE]
        Int32 _Version;     // [LE]
        Int32 _ShapeType;   // [LE]
        double _Xmin;       // [LE]
        double _Ymin;       // [LE]
        double _Xmax;       // [LE]
        double _Ymax;       // [LE]
        //double _Zmin;       // [LE]
        //double _Zmax;       // [LE]
        //double _Mmin;       // [LE]
        //double _Mmax;       // [LE]
        IExtent _Extent = null;

        #endregion

        #region Properties

        /// <summary>
        /// 文件长度
        /// </summary>
        public Int32 FileLength
        {
            get { return _FileLength; }
        }

        /// <summary>
        /// 文件头长度 = 100 字节
        /// </summary>
        public Int32 Length
        {
            get { return ShapeFileHeader.HeaderSize; }
        }

        /// <summary>
        /// 图形类型
        /// </summary>
        public ShapeType ShapeType
        {
            get { return (ShapeType)_ShapeType; }
        }

        /// <summary>
        /// 范围
        /// </summary>
        public IExtent Extent
        {
            get { return _Extent; }
            set { _Extent = value; }
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// Reads and parses the header of the .shp index file(.shx)
        /// </summary>
        private void ParseHeader(BinaryReader reader)
        {
            reader.BaseStream.Seek(0, SeekOrigin.Begin);

            // 文件代码
            _FileCode = BytesEncoder.GetBigEndian(reader.ReadInt32());
            if (ShapeFileHeader.FileCode != _FileCode)
            {
                throw new Exception("Invalid ShapeFile (.shp or .shx)");
            }

            // 跳过未用的5个LONG到文件长度字段
            reader.BaseStream.Seek(20, SeekOrigin.Current);
            _FileLength = BytesEncoder.GetBigEndian(reader.ReadInt32());
            _FileLength *= 2;   // 变为字节数

            // 版本 = 1000
            _Version = BytesEncoder.GetLittleEndian(reader.ReadInt32());

            // 图形类型
            _ShapeType = BytesEncoder.GetLittleEndian(reader.ReadInt32());

            // 绑定坐标
            _Xmin = BytesEncoder.GetLittleEndian(reader.ReadDouble());
            _Ymin = BytesEncoder.GetLittleEndian(reader.ReadDouble());
            _Xmax = BytesEncoder.GetLittleEndian(reader.ReadDouble());
            _Ymax = BytesEncoder.GetLittleEndian(reader.ReadDouble());
            //_Zmin;       // [LE]
            //_Zmax;       // [LE]
            //_Mmin;       // [LE]
            //_Mmax;       // [LE]
            _Extent = new Extent(_Xmin, _Ymin, _Xmax, _Ymax);

            return;
        }

        #endregion
    
        //}}@@@
    }

}
