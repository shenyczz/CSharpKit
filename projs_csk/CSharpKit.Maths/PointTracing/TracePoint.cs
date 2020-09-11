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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpKit.Maths.PointTracing
{
    /// <summary>
    /// TracePoint - 追踪点
    /// </summary>
    public class TracePoint
    {
        public TracePoint()
        {
            Flag = NotTraced;
            GroupID = InvalidGroupID;
            EdgeIndex = -1;
        }

        public const int NotTraced = 0;         // 没有追踪过
        public const int InvalidGroupID = 999;  // 无效编组ID 

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        public int Row;       //行
        public int Col;       //列

        public float Lon;       //经度
        public float Lat;       //纬度

        public int Flag;      //标记
        public int GroupID;   //组ID
        public int EdgeIndex;      //边界索引

        public float Value;    //值


        public override string ToString()
        {
            return String.Format("ID = {0} Group = {1} Row = {2} Col = {3} Value = {4,4:F1} Flag = {5} EdgeIndex = {6}"
                , ID, GroupID, Row, Col, Value, Flag, EdgeIndex);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode() ^ Row.GetHashCode() ^ Col.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is TracePoint
                && Row == (obj as TracePoint).Row
                && Col == (obj as TracePoint).Col;
        }


    }





















}
