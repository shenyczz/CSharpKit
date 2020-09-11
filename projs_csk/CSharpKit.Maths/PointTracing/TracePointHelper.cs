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
/******************************************************************************

    TracePointGroup points = new TracePointGroup();
    for (int i = 0; i < col * row; i++)
    {
        int r = i / col;
        int c = i % col;

        double rv = dbz[r, c];
        
        // 下限值
        if (rv > mindbz)
        {
            TracePoint xpoint = new TracePoint
            {
                ID = i,
                Row = (short)(i / col),
                Col = (short)(i % col),
                Flag = TracePoint.NotTraced,
                GroupID = TracePoint.InvalidGroupID,
                Value = (float)rv,
            };

            double lon = 0, lat = 0;
            iso.ToCoordinate(xpoint.Col, xpoint.Row, ref lon, ref lat); // 转换为经纬度坐标
                        
            xpoint.Lon = (float)lon;
            xpoint.Lat = (float)lat;

            points.Add(xpoint);
        }
    }

    // 边 edges
    List<List<TracePoint>> pointEdge = new List<List<TracePoint>>();
    if (points.Count > 0)
    {
        // 编组
        TracePointHelper.Grouping(points);

        // 点组集合
        List<TracePointGroup> pointGroups = TracePointHelper.PointGroups.FindAll(p => p.Count > 40);

        // 边界追踪
        foreach (TracePointGroup ptg in pointGroups)
        {
            ptg.Edge = TracePointHelper.EdgeTracing_8(ptg);
            pointEdge.Add(ptg.Edge);
            //ptg.Edge.Sort((x, y) => x.EdgeIndex < y.EdgeIndex ? -1 : x.EdgeIndex > y.EdgeIndex ? 1 : 0);
        }
    }
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpKit.Maths.PointTracing
{
    public static class TracePointHelper
    {
        #region Properties

        #region 编组集合

        private static List<TracePointGroup> _PointGroups = new List<TracePointGroup>();
        public static List<TracePointGroup> PointGroups { get => _PointGroups; set => _PointGroups = value; }

        #endregion

        #endregion


        #region Functions - 编组

        public static List<TracePointGroup> Grouping(IEnumerable<TracePoint> points)
        {
            return Grouping(points.ToList());
        }


        private static List<TracePointGroup> Grouping(List<TracePoint> points)
        {
            // 按 ID 升序排列，可以提高分组速度
            points.Sort((x, y) => x.ID < y.ID ? -1 : x.ID > y.ID ? 1 : 0);

            SampleGrouping(points);

            while (HasNeighborGroup(points))
            {

                List<TracePointGroup> xpgrps = ReGroups(points);

                // 连接相邻组
                foreach (TracePointGroup xpg1 in xpgrps)
                {
                    foreach (TracePointGroup xpg2 in xpgrps)
                    {
                        if (IsNeighbor(xpg1, xpg2))
                        {
                            foreach (TracePoint xp in xpg2)
                            {
                                xp.GroupID = xpg1[0].GroupID;
                            }
                        }
                    }
                }

            }

            return _PointGroups;
        }



        /// <summary>
        /// 简单编组
        /// </summary>
        /// <param name="points">点集</param>
        private static void SampleGrouping(List<TracePoint> points)
        {
            int iGroup = 0;

            if (points.FindAll(p => p.Flag != TracePoint.NotTraced).Count == 0)
            {
                // 追踪过的格点数量为0，表示编组开始
                // 设置第一个点标记和编组值
                points[0].Flag = 1;
                points[0].GroupID = iGroup;
            }

            foreach (TracePoint pt in points)
            {
                // 过滤掉追踪过的
                if (pt.Flag != TracePoint.NotTraced)
                    continue;

                // 编组格点
                bool bFind = GroupingPoint(pt, points.FindAll(p => p.GroupID != TracePoint.InvalidGroupID));

                if (!bFind)
                {
                    // 没有发现同组格点则进行下一编组
                    pt.Flag = 1;
                    pt.GroupID = ++iGroup;
                    // 这里没有 break
                }

            }//foreach(TracePoint pt in points)

            return;
        }


        /// <summary>
        /// 格点编组
        /// </summary>
        /// <param name="point">没有追踪过的格点</param>
        /// <param name="points">编过组的格点集合或者所有格点集合</param>
        /// <returns>
        /// true - 编组称功
        /// </returns>
        /// <remarks>
        /// 遍历编过组的格点集合 xPoints，
        /// 给没有追踪过的格点 xpoint 编组
        /// </remarks>
        private static bool GroupingPoint(TracePoint point, List<TracePoint> points)
        {
            bool isFind = false;

            int row0 = point.Row;
            int col0 = point.Col;

            foreach (TracePoint pted in points)
            {
                //查找已经编组的点
                if (pted.Flag == TracePoint.NotTraced)
                    continue;

                bool b1 = IsNeighbor(point, pted);

                if (b1)
                {
                    isFind = true;    // 至少找到一个点

                    point.Flag = 1;
                    point.GroupID = pted.GroupID;
                    break;
                }

            }

            return isFind;
        }

        /// <summary>
        /// 两点是否相同位置（行列相同）
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        private static bool IsSamePosition(TracePoint pt1, TracePoint pt2)
        {
            return (pt1.Row - pt2.Row == 0) && (pt1.Col - pt2.Col == 0);
        }

        /*
        * 八度空间
        * 查看point的八度邻域（正东为0，逆时针1，2，3，4，5，6，7）
        * 
        *  3              2                  1
        *      -----------------------
        *      |-1, -1 |-1, 0 |-1, 1 | 
        *      -----------------------
        *  4   | 0, -1 | 0, 0 | 0, 1 |       0
        *      -----------------------
        *      | 1, -1 | 1, 0 | 1, 1 |
        *      -----------------------
        *  5              6                  7    
        *  
        */
        /// <summary>
        /// 两点是否相邻
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        private static bool IsNeighbor(TracePoint pt1, TracePoint pt2)
        {
            if (IsSamePosition(pt1, pt2))
                return false;

            int row1 = pt1.Row;
            int col1 = pt1.Col;

            int row2 = pt2.Row;
            int col2 = pt2.Col;

            int def_row = row1 - row2;
            int def_col = col1 - col2;

            ////////////////////////////////////////
            //  3              2               1  //
            //      -----------------------       //
            //      |-1, -1 |-1, 0 |-1, 1 |       //
            //      -----------------------       //
            //  4   | 0, -1 | 0, 0 | 0, 1 |    0  //
            //      -----------------------       //
            //      | 1, -1 | 1, 0 | 1, 1 |       //
            //      -----------------------       //
            //  5              6               7  //
            ////////////////////////////////////////

            bool c00 = def_row == -1 && def_col == -1;
            bool c01 = def_row == -1 && def_col == 0;
            bool c02 = def_row == -1 && def_col == 1;
            bool c10 = def_row == 0 && def_col == -1;
            //bool c11 = def_row == 0 && def_col == 0;        // 相同点
            bool c12 = def_row == 0 && def_col == 1;
            bool c20 = def_row == 1 && def_col == -1;
            bool c21 = def_row == 1 && def_col == 0;
            bool c22 = def_row == 1 && def_col == 1;

            return (c00 || c01 || c02 || c10 || c12 || c20 || c21 || c22);
        }

        /// <summary>
        /// 是否相邻
        /// </summary>
        /// <param name="point"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        private static bool IsNeighbor(TracePoint point, IEnumerable<TracePoint> points)
        {
            bool bNeighbor = false;

            foreach (TracePoint pt in points)
            {
                if (IsNeighbor(point, pt))
                {
                    bNeighbor = true;
                    break;
                }
            }

            return bNeighbor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ptGrp1"></param>
        /// <param name="ptGrp2"></param>
        /// <returns></returns>
        private static bool IsNeighbor(TracePointGroup ptGrp1, TracePointGroup ptGrp2)
        {
            bool bNeighbor = false;

            if (ptGrp1.GroupID != ptGrp2.GroupID)
            {
                foreach (TracePoint xp0 in ptGrp1)
                {
                    if (IsNeighbor(xp0, ptGrp2))
                    {
                        bNeighbor = true;
                        break;
                    }
                }
            }

            return bNeighbor;

        }

        /// <summary>
        /// 是否有相邻组
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private static bool HasNeighborGroup(List<TracePoint> points)
        {
            bool bHasNeighborGroup = false;

            List<TracePointGroup> xPointGroups = ReGroups(points);

            foreach (TracePointGroup xpg1 in xPointGroups)
            {
                foreach (TracePointGroup xpg2 in xPointGroups)
                {
                    if (IsNeighbor(xpg1, xpg2))
                    {
                        bHasNeighborGroup = true;
                        break;
                    }
                }

                if (bHasNeighborGroup)
                    break;
            }


            return bHasNeighborGroup;
        }

        // 重新编组
        private static List<TracePointGroup> ReGroups(List<TracePoint> points)
        {
            _PointGroups.Clear();


            // 按 GroupID 升序排列
            points.Sort((x, y) => x.GroupID < y.GroupID ? -1 : x.GroupID > y.GroupID ? 1 : 0);
            int iGroupCount = points[points.Count - 1].GroupID;   // 

            // 按 ID 升序排列
            points.Sort((x, y) => x.ID < y.ID ? -1 : x.ID > y.ID ? 1 : 0);

            int index = 0;
            for (int i = 0; i < iGroupCount; i++)
            {
                List<TracePoint> xps = points.FindAll(p => p.GroupID == i);
                if (xps.Count > 0)
                {
                    _PointGroups.Add(new TracePointGroup(xps) { GroupID = index++ });
                }
            }

            return _PointGroups;
        }

        #endregion


        #region Functions - 边界追踪

        ////////////////////////////////////////
        //                 1                  //
        //      -----------------------       //
        //              |-1, 0 |              //
        //      -----------------------       //
        //  2   | 0, -1 | 0, 0 | 0, 1 |    0  //
        //      -----------------------       //
        //              | 1, 0 |              //
        //      -----------------------       //
        //                3                   //
        ////////////////////////////////////////
        public static List<TracePoint> EdgeTracing_4(IEnumerable<TracePoint> points)
        {
            List<TracePoint> xplst = new List<TracePoint>();
            xplst.AddRange(points);
            xplst.Sort((x, y) => x.ID < y.ID ? -1 : x.ID > y.ID ? 1 : 0);

            int edgeIndex = 0;

            int dir = 3;
            TracePoint p0 = xplst[0]; p0.EdgeIndex = edgeIndex++;
            List<TracePoint> edge = new List<TracePoint> { p0 };    // 边界

            int row0 = p0.Row;
            int col0 = p0.Col;
            int[] adjdir = new int[8] { 0, 1, -1, 0, 0, -1, 1, 0 };

            bool bEnd = false;
            while (!bEnd)
            {
                int new_row, new_col;
                int searchdir = (dir + 3) % 4;

                for (int i = 0; i < 4; i++)
                {
                    int newdir = (searchdir + i) % 4;

                    new_row = row0 + adjdir[2 * newdir + 0];
                    new_col = col0 + adjdir[2 * newdir + 1];
                    TracePoint tp_tmp = xplst.Find(p => p.Row == new_row && p.Col == new_col);
                    if (tp_tmp == null)
                        continue;

                    // 找到p
                    edge.Add(tp_tmp);
                    tp_tmp.EdgeIndex = edgeIndex++; // 设置边界索引

                    // 更行行列值
                    row0 = tp_tmp.Row;
                    col0 = tp_tmp.Col;

                    if (edge.Count <= 2)
                    {
                        // 更新
                        dir = newdir;
                        break;
                    }

                    // 找到p,如果当前点Pn等于第二个点P1,并且Pn-1等于P0则追踪完毕
                    //if (xptemp.Equals(edge[1]) && p0.Equals(edge[edge.Count - 1]))
                    if (tp_tmp.Equals(edge[0]))
                    {
                        //bEnd = true;
                        dir = newdir;
                        break;
                    }

                    if (tp_tmp.Equals(edge[1]) && p0.Equals(edge[edge.Count - 2]))
                    {
                        bEnd = true;
                        break;
                    }


                    // 
                    dir = newdir;
                    break;

                }


                if (bEnd)
                    break;
            }

            return edge;

        }


        ////////////////////////////////////////
        //  3              2               1  //
        //      -----------------------       //
        //      |-1, -1 |-1, 0 |-1, 1 |       //
        //      -----------------------       //
        //  4   | 0, -1 | 0, 0 | 0, 1 |    0  //
        //      -----------------------       //
        //      | 1, -1 | 1, 0 | 1, 1 |       //
        //      -----------------------       //
        //  5              6               7  //
        ////////////////////////////////////////
        public static List<TracePoint> EdgeTracing_8(IEnumerable<TracePoint> points)
        {
            List<TracePoint> xplst = new List<TracePoint>();
            xplst.AddRange(points);
            xplst.Sort((x, y) => x.ID < y.ID ? -1 : x.ID > y.ID ? 1 : 0);

            int edgeIndex = 0;

            int dir = 7;
            TracePoint p0 = xplst[0]; p0.EdgeIndex = edgeIndex++;
            List<TracePoint> edge = new List<TracePoint> { p0 };

            int row0 = p0.Row;
            int col0 = p0.Col;
            int[] adjdir = new int[16] { 0, 1, -1, 1, -1, 0, -1, -1, 0, -1, 1, -1, 1, 0, 1, 1 };

            bool bEnd = false;
            //int searchdir = 0;
            while (!bEnd)
            {
                int new_row, new_col;
                int searchdir = dir % 2 != 0 ? (dir + 6) % 8 : (dir + 7) % 8;

                for (int i = 0; i < 8; i++)
                {
                    int newdir = (searchdir + i) % 8;
                    ////////////////////////////////////////
                    //  3              2               1  //
                    //      -----------------------       //
                    //      |-1, -1 |-1, 0 |-1, 1 |       //
                    //      -----------------------       //
                    //  4   | 0, -1 | 0, 0 | 0, 1 |    0  //
                    //      -----------------------       //
                    //      | 1, -1 | 1, 0 | 1, 1 |       //
                    //      -----------------------       //
                    //  5              6               7  //
                    ////////////////////////////////////////
                    new_row = row0 + adjdir[2 * newdir + 0];
                    new_col = col0 + adjdir[2 * newdir + 1];
                    TracePoint tp_temp = xplst.Find(p => p.Row == new_row && p.Col == new_col);

                    // 没有找到下一个边界点
                    if (tp_temp == null)
                    {
                        if (i == 7)
                        {
                            // 8个方向搜索完还没有找到
                            // 表示该点是个孤立点
                            bEnd = true;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    // 找到下一个边界点
                    //
                    edge.Add(tp_temp);   // 添加到边界
                    tp_temp.EdgeIndex = edgeIndex++; // 设置边界索引

                    // 更行行列值
                    row0 = tp_temp.Row;
                    col0 = tp_temp.Col;

                    if (edge.Count <= 2)
                    {
                        // 更新
                        dir = newdir;
                        break;
                    }

                    // 找到Pn,如果当前点Pn等于第二个点P1,并且Pn-1等于P0则追踪完毕
                    if (tp_temp.Equals(edge[1]) && p0.Equals(edge[edge.Count - 2]))
                    {
                        bEnd = true;
                        edge.RemoveAt(edge.Count - 1);

                        edge[0].EdgeIndex = 0;
                        edge[1].EdgeIndex = 1;

                        break;
                    }

                    // 更新dir
                    dir = newdir;
                    break;

                }


                if (bEnd)
                    break;
            }

            return edge;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="centLon"></param>
        /// <param name="cengLat"></param>
        /// <param name="points"></param>
        public static void RowCol2LonLat(double centLon, double cengLat, IEnumerable<TracePoint> points)
        {
            // KPolar(double centLon, double centLat, double ppkm)
            KPolar polar = new KPolar(centLon, cengLat); 
            foreach(TracePoint pt in points)
            {
                polar.XY2LonLat(pt.Col, pt.Row, out double lon, out double lat);
                pt.Lon = (float)lon;
                pt.Lat = (float)lat;
            }
        }

        #endregion


        // 匹配
        public static TracePointGroup Matching(TracePointGroup tpg, List<TracePointGroup> grps)
        {
            Dictionary<int, double> keys = new Dictionary<int, double>(); 

            for (int i = 0; i < grps.Count; i++)
            {
                double d = Distance(tpg.Edge.Centroid, grps[i].Edge.Centroid);
                keys.Add(i, d);
            }

            Dictionary<int, double> dict = keys.OrderBy(p => p.Value).ToDictionary(p => p.Key, o => o.Value);
            int m = dict.Keys.ElementAt(0);

            return grps[m];
        }

        // 根据距离匹配
        public static Dictionary<int, double> MatchingByDistance(TracePointGroup tpg, List<TracePointGroup> grps)
        {
            Dictionary<int, double> keys = new Dictionary<int, double>();

            for (int i = 0; i < grps.Count; i++)
            {
                double d = Distance(tpg.Edge.Centroid, grps[i].Edge.Centroid);
                keys.Add(i, d);
            }

            Dictionary<int, double> dict = keys.OrderBy(p => p.Value).ToDictionary(p => p.Key, o => o.Value);

            return dict;
        }


        /// <summary>
        /// 相关系数 = ∑(Xi-Xavg)(Yi-Yavg)/ Sqrt(∑(Xi-Xavg)^2)Sqrt(∑(Yi-Yavg)^2)
        /// </summary>
        /// <param name="points1"></param>
        /// <param name="points2"></param>
        /// <returns></returns>
        public static double Correlation(IEnumerable<TracePoint> points1, IEnumerable<TracePoint> points2)
        {
            double r = 0;

            List<TracePoint> psx = points1.ToList();
            List<TracePoint> psy = points2.ToList();

            // 点数量不够
            if (psx.Count <= 0 || psy.Count <= 0)
                return 0;

            // 0.618 => 0.382
            // 两组点数差绝对值大于点数平均值的10%则不相关
            double sc = 0.382;
            double count_avg = 0.5 * (psx.Count + psy.Count);   // 编组格点数量平均值
            double count_dif = Math.Abs(psx.Count - psy.Count); // 编组格点数量差值绝对值
            if (count_dif > count_avg * sc)
            {
                return 0;
            }

            //psx.Sort((a, b) => a.ID < b.ID ? -1 : a.ID > b.ID ? 1 : 0);
            //psy.Sort((a, b) => a.ID < b.ID ? -1 : a.ID > b.ID ? 1 : 0);

            // 计算XGroup平均值
            double xavg = 0;
            foreach (TracePoint pt in psx)
            {
                //xavg += pt.Value;
                xavg += pt.Value * pt.ID;
            }
            xavg /= psx.Count;

            double yavg = 0;
            foreach (TracePoint pt in psy)
            {
                //yavg += pt.Value;
                yavg += pt.Value * pt.ID;
            }
            yavg /= psy.Count;

            // 使用较小的元素数量
            int count_use = psx.Count < psy.Count ? psx.Count : psy.Count;

            // 相关系数 r = ∑(Xi-Xavg)(Yi-Yavg)/ Sqrt[(∑(Xi-Xavg)^2)(∑(Yi-Yavg)^2)]
            double sum_dxdy = 0;    // ∑(Xi-Xavg)(Yi-Yavg)
            double sum_dx_2 = 0;    // ∑(Xi-Xavg)^2
            double sum_dy_2 = 0;    // ∑(Yi-Yavg)^2
            for (int i = 0; i < count_use; i++)
            {
                //double dx = psx[i].Value - xavg;
                //double dy = psy[i].Value - yavg;
                double dx = psx[i].Value * psx[i].ID - xavg;
                double dy = psy[i].Value * psy[i].ID - yavg;

                sum_dxdy += dx * dy;
                sum_dx_2 += dx * dx;
                sum_dy_2 += dy * dy;
            }

            r = sum_dxdy / Math.Sqrt(sum_dx_2 * sum_dy_2);

            return r;
        }
















        private static double Sum_dbz(IEnumerable<TracePoint> points)
        {
            double sum_dbz = 0;
            points.ToList().ForEach(p => sum_dbz += p.Value);
            return sum_dbz;
        }

        private static double Sum_dbz_id(IEnumerable<TracePoint> points)
        {
            double sum_dbz_id = 0;
            points.ToList().ForEach(p => sum_dbz_id += p.Value * p.ID);
            return sum_dbz_id;
        }

        private static double Sum_dbz_2(IEnumerable<TracePoint> points)
        {
            double sum_dbz_2 = 0;
            points.ToList().ForEach(p => sum_dbz_2 += p.Value * p.Value);
            return sum_dbz_2;
        }














        /// <summary>
        /// 查找相关系数最大的
        /// </summary>
        /// <param name="points"></param>
        /// <param name="grps"></param>
        /// <returns></returns>
        public static Dictionary<int, double> Matching_0(IEnumerable<TracePoint> points, List<TracePointGroup> grps)
        {
            Dictionary<int, double> keys = new Dictionary<int, double>();

            for (int i = 0; i < grps.Count; i++) 
            {
                //double r = GCorrelation(points, grps[i]);
                double r = GCorrelation2(points, grps[i]);
                if (r > 0)
                {
                    keys.Add(i, r);
                }
            }

            //KeyValuePair<int, double>[] ka = keys.OrderByDescending(p => p.Value).ToArray();
            Dictionary<int, double> dict = keys.OrderByDescending(p => p.Value).ToDictionary(p => p.Key, o => o.Value);
            return dict;
        }

        public static Dictionary<int, double> MatchingByDistance_0(TracePointGroup tpg, List<TracePointGroup> grps)
        {
            Dictionary<int, double> keys = new Dictionary<int, double>();

            for (int i = 0; i < grps.Count; i++)
            {
                double d = Distance(tpg.Edge.Centroid, grps[i].Edge.Centroid);
                keys.Add(i, d);
            }

            //KeyValuePair<int, double>[] ka = keys.OrderByDescending(p => p.Value).ToArray();
            Dictionary<int, double> dict = keys.OrderBy(p => p.Value).ToDictionary(p => p.Key, o => o.Value);
            //Dictionary<int, double> dict = keys.OrderByDescending(p => p.Value).ToDictionary(p => p.Key, o => o.Value);

            return dict;
        }








        /// <summary>
        /// 相关系数 = ∑(Xi-Xavg)(Yi-Yavg)/ Sqrt(∑(Xi-Xavg)^2)Sqrt(∑(Yi-Yavg)^2)
        /// </summary>
        /// <param name="points1"></param>
        /// <param name="points2"></param>
        /// <returns></returns>
        private static double GCorrelation(IEnumerable<TracePoint> points1, IEnumerable<TracePoint> points2)
        {
            double r = 0;

            List<TracePoint> psx = points1.ToList();
            List<TracePoint> psy = points2.ToList();

            double sc = 0.1;
            double count_avg = 0.5 * (psx.Count + psy.Count);   // 编组格点数量平均值
            double count_def = Math.Abs(psx.Count - psy.Count); // 编组格点数量差值绝对值

            // 两组点数差绝对值大于点数平均值的10%则不相关
            if (count_def > count_avg * sc)
                return 0;

            // 计算XGroup平均值
            double xavg = 0;
            foreach (TracePoint pt in psx)
            {
                xavg += pt.Value * pt.ID;
            }
            xavg /= psx.Count;

            // 计算YGroup平均值
            double yavg = 0;
            foreach (TracePoint pt in psy)
            {
                yavg += pt.Value * pt.ID;
            }
            yavg /= psy.Count;

            // 使用较小的元素数量
            int count_use = psx.Count < psy.Count ? psx.Count : psy.Count;

            // 相关系数 = ∑(Xi-Xavg)(Yi-Yavg)/ Sqrt[(∑(Xi-Xavg)^2)(∑(Yi-Yavg)^2)]
            double sum_dxdy = 0;    // ∑(Xi-Xavg)(Yi-Yavg)
            double sum_dx_2 = 0;    // ∑(Xi-Xavg)^2
            double sum_dy_2 = 0;    // ∑(Yi-Yavg)^2

            for (int i = 0; i < count_use; i++)
            {
                double dx = psx[i].Value * psx[i].ID - xavg;
                double dy = psy[i].Value * psy[i].ID - yavg;

                //sum_dxdy += dx * dy;
                sum_dxdy += Math.Abs(dx) * Math.Abs(dy);
                sum_dx_2 += dx * dx;
                sum_dy_2 += dy * dy;
            }

            r = sum_dxdy / Math.Sqrt(sum_dx_2 * sum_dy_2);

            return r;
        }

        private static double GCorrelation2(IEnumerable<TracePoint> points1, IEnumerable<TracePoint> points2)
        {
            double r = 0;

            int count1 = points1.Count();
            int count2 = points2.Count();
            int count_min = Math.Min(count1, count2);
            int count_max = Math.Max(count1, count2);

            List<TracePoint> psa_min, psa_max;

            if (count1 < count2)
            {
                psa_min = points1.ToList();
                psa_max = points2.ToList();
            }
            else
            {
                psa_min = points2.ToList();
                psa_max = points1.ToList();
            }

            List<TracePoint> psx = psa_max;
            List<TracePoint> psy = psa_min;

            double count_avg = 0.5 * (psx.Count + psy.Count);   // 编组格点数量平均值
            double count_def = Math.Abs(psx.Count - psy.Count); // 编组格点数量差值绝对值

            // 两组点数差绝对值大于点数平均值的10%则不相关
            if (count_def > count_avg * 0.1)
                return 0;

            double dbz_avg_max = Sum_dbz(psa_max) / count_max;
            double dbz_avg_min = Sum_dbz(psa_min) / count_min;

            double sum_vx = 0;
            double sum_vy = 0;
            double sum_vxvy = 0;
            double sum_vx_2 = 0;
            double sum_vy_2 = 0;
            for (int i = 0; i < count_max; i++)
            {
                double vx = psa_max[i].Value;
                double vx_2 = vx * vx;
                double vy = i <= (count_min - 1) ? psa_min[i].Value : dbz_avg_min;
                double vy_2 = vy * vy;

                sum_vx += vx;
                sum_vy += vy;
                sum_vxvy += vx * vy;

                sum_vx_2 += vx_2;
                sum_vy_2 += vy_2;
            }

            // sum_vxvy - (sum_vx*sum_vy)/N
            // (sum_vx_2 - avg_vx_2*N) * (sum_vy_2 - avg_vy_2*N)

            double N = count_max;
            double avg_vx_2 = dbz_avg_max * dbz_avg_max;
            double avg_vy_2 = dbz_avg_min * dbz_avg_min;


            double ra = sum_vxvy - (sum_vx * sum_vy) / N;
            double rb = (sum_vx_2 - avg_vx_2 * N) * (sum_vy_2 - avg_vy_2 * N);

            r = ra / rb;

            return r;
        }



        // Shape Context
        // https://en.wikipedia.org/wiki/Shape_context


        private static TracePoint CalcCentroid(IList<TracePoint> points)
        {

            double sum_x = 0;
            double sum_y = 0;
            double sum_area = 0;

            TracePoint p0 = points[0];
            TracePoint p1 = points[1];
            for (int i = 0; i < points.Count; i++)
            {
                TracePoint p2 = points[i];
                double area = Area(p0, p1, p2);
                sum_area += area;
                sum_x += (points[0].Lon + p1.Lon + p2.Lon) * area;
                sum_y += (points[0].Lat + p1.Lat + p2.Lat) * area;
                p1 = p2;
            }

            double xx = sum_x / sum_area / 3;
            double yy = sum_y / sum_area / 3;

            TracePoint centroid = new TracePoint
            {
                Lon = (float)xx,
                Lat = (float)yy
            };

            return centroid;
        }

        private static double Area(TracePoint p0, TracePoint p1, TracePoint p2)
        {
            double area = 0;
            area = p0.Lon * p1.Lat + p1.Lon * p2.Lat + p2.Lon * p0.Lat - p1.Lon * p0.Lat - p2.Lon * p1.Lat - p0.Lon * p2.Lat;
            return area / 2;
        }

        private static double Distance(TracePoint p1, TracePoint p2)
        {
            double d = 0;

            double dx = p1.Lon - p2.Lon;
            double dy = p1.Lat - p2.Lat;

            d = Math.Sqrt(dx * dx + dy * dy);

            return d;
        }

        //}}@@@
    }
}
