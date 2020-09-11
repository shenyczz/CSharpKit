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
using System.Data;
using System.IO;
using CSharpKit.GeoApi;
using CSharpKit.GeoApi.Geometries;
using GeoPoint = CSharpKit.GeoApi.Geometries.Point;

namespace CSharpKit.Data.Esri
{
    public class ShapeFileProcessor : EsriFileProcessor
    {
        #region Constructors

        public ShapeFileProcessor(IDataInstance owner)
            : base(owner) { }

        #endregion

        #region Protected Functions

        /// <summary>
        /// 装载二进制文件数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected override Boolean Load_bin(String fileName)
        {
            FileStream fs = null;
            BinaryReader br = null;

            try
            {
                var shapeFile = this.Owner as ShapeFile;

                fs = File.OpenRead(fileName);
                br = new BinaryReader(fs);

                // 索引文件
                if (shapeFile.ShapeFileIndex == null)
                {
                    shapeFile.ShapeFileIndex = new ShapeFileIndex(shapeFile);
                    shapeFile.DataInfo.Extent = shapeFile.ShapeFileIndex.ShapeFileHeader.Extent;
                }


                // 读取 Feature
                int shapeCount = shapeFile.ShapeFileIndex.Count;
                for (int i = 0; i < shapeCount; i++)
                {
                    IFeature feature = this.ReadFeature(br, (uint)(i + 1));
                    if (feature != null)
                    {
                        shapeFile.Features.Add(feature);
                    }
                }

                br.Close();
                fs.Close();
            }
            catch (System.Exception ex)
            {
                if (fs != null) fs.Close();
                if (br != null) br.Close();
                string errorMessage = ex.Message;
                return false;
            }

            return true;
        }

        #endregion

        private IFeature ReadFeature(BinaryReader br, UInt32 oid)
        {
            IFeature feature = null;
            //---------------------------------------------
            try
            {
                ShapeFile shapeFile = this.Owner as ShapeFile;

                DBFile xBaseFile = shapeFile.XBaseFile;
                String displayFieldName = xBaseFile.XBaseFields.DisplayFieldName;

                // 数据行
                int index = (int)oid - 1;
                DataRow dataRow = xBaseFile.Rows[index];

                feature = new Feature();

                // 专题.属性数据
                Object obj = null;
                DataColumn column = null;

                // 设置 Feature ID
                column = xBaseFile.Columns["Id"];
                if (column == null)
                    column = xBaseFile.Columns["StationId"];

                if (column != null)
                {
                    obj = dataRow[column];
                    if (obj != null)
                    {
                        feature.Id = obj.ToString();
                    }
                }

                // 设置 Feature Name
                column = xBaseFile.Columns["Name"];
                if (column == null)
                    column = xBaseFile.Columns[displayFieldName];

                if (column != null)
                {
                    obj = dataRow[column];
                    if (obj != null)
                    {
                        feature.Name = obj.ToString();
                    }
                }

                // 专题.地理信息矢量数据
                ShapeRecordEntry shapeRecordEntry = shapeFile.ShapeFileIndex[oid];
                feature.Geometry = readOneShape(br, shapeRecordEntry.Offset);           // 矢量数据
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
                feature = null;
            }
            //---------------------------------------------
            return feature;
        }
        private IGeometry readOneShape(BinaryReader reader, Int32 offset)
        {
            IGeometry geometry = null;
            ShapeFile shapeFile = this.Owner as ShapeFile;

            switch (shapeFile.ShapeType)
            {
                case ShapeType.Point:
                    geometry = readOneShape_point(reader, offset);
                    break;

                case ShapeType.Polyline:
                    geometry = readOneShape_polyline(reader, offset);
                    break;

                case ShapeType.Polygon:
                    geometry = readOneShape_polygon(reader, offset);
                    break;
            }

            return geometry;
        }
        private IGeometry readOneShape_point(BinaryReader reader, Int32 offset)
        {
            // 寻址
            reader.BaseStream.Seek(offset, SeekOrigin.Begin);

            // 读取记录头
            Int32 recordNo = BytesEncoder.GetBigEndian(reader.ReadInt32());
            Int32 recordLength = BytesEncoder.GetBigEndian(reader.ReadInt32());

            // 记录号 lRecordNo 从 1 开始
            // 记录号等于0表示 Null Shape
            if (recordNo == 0)
                return null;

            // 图形类型
            ShapeType shapeType = (ShapeType)reader.ReadInt32();
            if (shapeType != ShapeType.Point)
                return null;

            GeometryCollection geometrys = new GeometryCollection();
            geometrys.Extent = new Extent();

            // 处理单个目标有多个点的问题 - beg
            {
                // 读取点坐标
                double x = reader.ReadDouble();
                double y = reader.ReadDouble();

                // 点对象(Point)
                GeoPoint point = new GeoPoint(x, y);
                point.Extent = new Extent(x, y, x, y);

                geometrys.Add(point);
                geometrys.Extent = point.Extent;
            }
            // 处理单个目标有多个点的问题 - end

            return geometrys;
        }
        private IGeometry readOneShape_polyline(BinaryReader reader, Int32 offset)
        {
            int i;

            // 寻址
            reader.BaseStream.Seek(offset, SeekOrigin.Begin);

            // 读取记录头
            Int32 recordNo = BytesEncoder.GetBigEndian(reader.ReadInt32());
            Int32 recordLength = BytesEncoder.GetBigEndian(reader.ReadInt32());

            // 记录号 lRecordNo 从 1 开始
            // 记录号等于0表示 Null Shape
            if (recordNo == 0)
                return null;

            // 图形类型
            ShapeType shapeType = (ShapeType)reader.ReadInt32();
            if (shapeType != ShapeType.Polyline)
                return null;

            // 读取Shape对象包围盒数据
            double xmin = reader.ReadDouble();
            double ymin = reader.ReadDouble();
            double xmax = reader.ReadDouble();
            double ymax = reader.ReadDouble();

            GeometryCollection geometrys = new GeometryCollection();
            geometrys.Extent = new Extent(xmin, ymin, xmax, ymax);

            // 读取折线数量
            Int32 numParts = reader.ReadInt32();
            // 读取总点数
            Int32 numPoints = reader.ReadInt32();

            // 读取折线起点索引数组
            Int32[] Parts = new Int32[numParts];
            for (i = 0; i < numParts; i++)
            {
                Parts[i] = reader.ReadInt32();
            }

            // 处理单个目标有多条线的问题 - beg
            for (i = 0; i < numParts; i++)
            {
                // 折线
                Polyline polyline = new Polyline();

                int curPosIndex = Parts[i];     // 当前位置
                int nxtPosIndex = 0;            // 下一位置
                int curPointCount = 0;          // 当前点数量

                if (i == numParts - 1)
                {
                    curPointCount = numPoints - curPosIndex;
                }
                else
                {
                    nxtPosIndex = Parts[i + 1];
                    curPointCount = nxtPosIndex - curPosIndex;
                }

                // 读取一条折线坐标
                for (int np = 0; np < curPointCount; np++)
                {
                    double x = reader.ReadDouble();
                    double y = reader.ReadDouble();
                    polyline.Add(new GeoPoint(x, y));
                }

                // 添加一条折线
                geometrys.Add(polyline);

            }// next i
            // 处理单个目标有多条线的问题 - end

            return geometrys;
        }
        private IGeometry readOneShape_polygon(BinaryReader reader, Int32 offset)
        {
            int i;

            // 寻址
            reader.BaseStream.Seek(offset, SeekOrigin.Begin);

            // 读取记录头
            Int32 recordNo = BytesEncoder.GetBigEndian(reader.ReadInt32());
            Int32 recordLength = BytesEncoder.GetBigEndian(reader.ReadInt32());

            // 记录号 lRecordNo 从 1 开始
            // 记录号等于0表示 Null Shape
            if (recordNo == 0)
                return null;

            // 图形类型
            ShapeType shapeType = (ShapeType)reader.ReadInt32();
            if (shapeType != ShapeType.Polygon)
                return null;

            // 读取Shape对象包围盒数据
            double xmin = reader.ReadDouble();
            double ymin = reader.ReadDouble();
            double xmax = reader.ReadDouble();
            double ymax = reader.ReadDouble();

            GeometryCollection geometrys = new GeometryCollection();
            geometrys.Extent = new Extent(xmin, ymin, xmax, ymax);

            // 读取折线数量
            Int32 numParts = reader.ReadInt32();
            // 读取总点数
            Int32 numPoints = reader.ReadInt32();

            // 读取折线起点索引数组
            Int32[] Parts = new Int32[numParts];
            for (i = 0; i < numParts; i++)
            {
                Parts[i] = reader.ReadInt32();
            }

            // 处理单个目标有多条线的问题 - beg
            for (i = 0; i < numParts; i++)
            {
                // 多边形
                Polygon polygon = new Polygon();

                int curPosIndex = Parts[i];     // 当前位置
                int nxtPosIndex = 0;            // 下一位置
                int curPointCount = 0;          // 当前点数量

                if (i == numParts - 1)
                {
                    curPointCount = numPoints - curPosIndex;
                }
                else
                {
                    nxtPosIndex = Parts[i + 1];
                    curPointCount = nxtPosIndex - curPosIndex;
                }

                // 读取一个多边形
                for (int np = 0; np < curPointCount; np++)
                {
                    double x = reader.ReadDouble();
                    double y = reader.ReadDouble();
                    polygon.Add(new GeoPoint(x, y));
                }

                // 添加一个多边形
                geometrys.Add(polygon);

            }// next i
            // 处理单个目标有多条线的问题 - end

            return geometrys;
        }




        //}}@@@
    }


}
