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
using System.Collections.Generic;
using System.IO;

namespace CSharpKit.Data.Esri
{
    /// <summary>
    /// ShapeFileIndex - Shape 文件索引(*.shx)
    /// </summary>
    public class ShapeFileIndex : Dictionary<UInt32, ShapeRecordEntry>
    {
        #region Constructors

        internal ShapeFileIndex(IShapeFile shapeFile)
        {
            try
            {
                ShapeFileHeader = new ShapeFileHeader(shapeFile);
                this.ReadRecordEntries(shapeFile.IndexFileName);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Shape 文件头
        /// </summary>
        public ShapeFileHeader ShapeFileHeader { get; private set; }

        #endregion

        #region Private Functions

        /// <summary>
        /// 读取记录入口
        /// </summary>
        /// <param name="fileName"></param>
        private void ReadRecordEntries(String fileName)
        {
            try
            {
                FileStream fs = File.OpenRead(fileName);
                BinaryReader br = new BinaryReader(File.OpenRead(fileName));

                // 跳过文件头
                br.BaseStream.Seek(ShapeFileHeader.HeaderSize, SeekOrigin.Begin);

                // 记录数量
                int recordCount = (ShapeFileHeader.FileLength - ShapeFileHeader.Length) / 8;
                for (uint i = 0; i < recordCount; i++)
                {
                    Int32 offset = BytesEncoder.GetBigEndian(br.ReadInt32());
                    Int32 length = BytesEncoder.GetBigEndian(br.ReadInt32());

                    ShapeRecordEntry entry = new ShapeRecordEntry(offset, length);
                    this.Add(i + 1, entry);
                }

                br.Close();
                fs.Close();
            }
            catch (System.Exception)
            {
                throw new Exception("读取记录入口错误 - in ShapeFileIndex::ReadRecordEntries(...) ");
            }
        }

        #endregion


        //@EndOf(ShapeFileIndex)
    }



}
