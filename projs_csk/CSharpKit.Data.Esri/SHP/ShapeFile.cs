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

using CSharpKit.GeoApi;
using CSharpKit.GeoApi.Geometries;
using System;
using System.Collections.Generic;
using System.IO;

namespace CSharpKit.Data.Esri
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ShapeFile : EsriFile, IShapeFile
    {
        public ShapeFile(IProvider owner)
            : base(owner) { }


        #region Properties

        public string FileName => ConnectionString;

        /// <summary>
        /// 形状类型
        /// </summary>
        public ShapeType ShapeType
        {
            get
            {
                return ShapeFileIndex == null || ShapeFileIndex.ShapeFileHeader == null
                    ? ShapeType.Null
                    : ShapeFileIndex.ShapeFileHeader.ShapeType;
            }
        }

        /// <summary>
        /// 取得数据对象范围
        /// </summary>
        /// <returns></returns>
        public IExtent Extent
        {
            get { return ShapeFileIndex.ShapeFileHeader.Extent; }
        }
        /// <summary>
        /// 地貌集
        /// </summary>
        public List<IFeature> Features { get; private set; }

        /// <summary>
        /// 索引文件名
        /// </summary>
        public String IndexFileName
        {
            get { return Path.Combine(Path.GetDirectoryName(this.FileName), Path.GetFileNameWithoutExtension(this.FileName)) + ".shx"; }
        }

        /// <summary>
        /// 属性数据库文件名
        /// </summary>
        public String XBaseFileName
        {
            get { return Path.Combine(Path.GetDirectoryName(this.FileName), Path.GetFileNameWithoutExtension(this.FileName)) + ".dbf"; }
        }

        /// <summary>
        /// ESRI 空间数据的属性数据(*.dbf)
        /// </summary>
        public DBFile XBaseFile { get; private set; }

        /// <summary>
        /// Shape 文件索引(*.shx)
        /// </summary>
        public ShapeFileIndex ShapeFileIndex { get; set; }

        #endregion

        #region Protected Functions

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Initialize()
        {
            Features = new List<IFeature>();

            try
            {
                // 属性数据
                this.XBaseFile = new DBFile(this.XBaseFileName);

                // 设置显示字段
                if (!this.XBaseFile.XBaseFields.EnableDisplayField("NAME"))
                {
                    this.XBaseFile.XBaseFields.EnableDisplayField("FIRST_NAME");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                // 数据信息
                this.DataInfo = new ShapeFileDataInfo(this);
                // 数据算法
                this.Algorithm = new ShapeFileAlgorithm(this);
                // 数据处理器
                this.Processor = new ShapeFileProcessor(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public override void Dispose()
        {

        }

        #region Public Functions - Static

        /// <summary>
        /// 是否是Shape文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Boolean IsShapeFile(String fileName)
        {
            Int32 fileCode = 0;

            FileStream fs = null;
            BinaryReader br = null;

            try
            {
                fs = new FileStream(fileName, FileMode.Open);
                br = new BinaryReader(fs);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                // 文件代码
                fileCode = BytesEncoder.GetBigEndian(br.ReadInt32());
                br.Close();
                fs.Close();
            }

            // 文件格式
            return (ShapeFileHeader.FileCode == fileCode);
        }

        public override void BuildLegend(int iDataCode)
        {
            throw new NotImplementedException();
        }

        #endregion

        //}}@@@
    }


}
