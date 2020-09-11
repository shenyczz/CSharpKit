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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpKit.Maths.PointTracing
{
    /// <summary>
    /// TracePointGroup - 编组
    /// </summary>
    public class TracePointGroup : TracePointCollectionBase
    {
        public TracePointGroup()
        {
            _GroupID = TracePoint.InvalidGroupID;
        }
        public TracePointGroup(List<TracePoint> points) : this()
        {
            this.AddRange(points);
        }

        #region GroupID

        private int _GroupID;
        public int GroupID
        {
            get { return _GroupID; }
            set
            {
                _GroupID = value;
                foreach (TracePoint pt in this)
                {
                    pt.GroupID = _GroupID;
                }
            }
        }

        #endregion

        #region Edge

        public TracePointEdge Edge { get; set; }

        #endregion

        #region Centroid

        public TracePoint Centroid
        {
            get { return Edge.Centroid; }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("[TracePointGroup] Count = {0,4} GroupID = {1,4}"
                , Count, GroupID);
        }

    }



}
