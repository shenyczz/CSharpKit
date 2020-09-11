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
using System.Text;

namespace CSharpKit.Maths.Interpolation
{
    /// <summary>
    /// 离散点插值器
    /// </summary>
    public class V2VInterpolater
    {
        /// <summary>
        /// 节点结构
        /// </summary>
        private struct Node : IComparable<Node>
        {
            public double distance;
            public double value;

            public Node(double d, double v)
            {
                distance = d;
                value = v;
            }

            #region IComparable<Node> 成员

            public int CompareTo(Node other)
            {
                if (this.distance - other.distance > 0)
                    return 1;
                else if (this.distance - other.distance < 0)
                    return -1;
                else
                    return 0;
            }

            #endregion
        }

        #region ---字段、属性---

        /// <summary>
        /// 源数据 X 坐标集
        /// </summary>
        public Double[] Xsource { get; set; }
        /// <summary>
        /// 源数据 Y 坐标集
        /// </summary>
        public Double[] Ysource { get; set; }
        /// <summary>
        /// 源数据 V 坐标集
        /// </summary>
        public Double[] Vsource { get; set; }
        /// <summary>
        /// 目标数据 X 坐标集
        /// </summary>
        public Double[] Xtarget { get; set; }
        /// <summary>
        /// 目标数据 Y 坐标集
        /// </summary>
        public Double[] Ytarget { get; set; }
        /// <summary>
        /// 目标数据 V 坐标集
        /// </summary>
        public Double[] Vtarget { get; set; }
        /// <summary>
        /// 距离插值点最近的最少源数据点数
        /// </summary>
        public int NumbersOfPoint { get; set; }
        /// <summary>
        /// 节点链
        /// </summary>
        private List<Node> _NodeList = null;

        #endregion

        #region ---构造函数---

        /// <summary>
        /// 构造函数
        /// </summary>
        public V2VInterpolater()
        {
            NumbersOfPoint = 5;
            _NodeList = new List<Node>();
        }

        #endregion

        /// <summary>
        /// 插值处理
        /// </summary>
        public void Transact()
        {
            int NumberTarget = Vtarget.Length;
            for (int i = 0; i < NumberTarget; i++)
            {
                Vtarget[i] = InterpolateOnePoint(Xtarget[i], Ytarget[i]);
            }

        }// Transact - end

        #region ---私有函数---

        /// <summary>
        /// 插值一个点
        /// </summary>
        /// <param name="x">被插值点的x坐标</param>
        /// <param name="y">被插值点的y坐标</param>
        /// <returns>被插值点要素值</returns>
        private Double InterpolateOnePoint(Double x, Double y)
        {
            int i;

            _NodeList.Clear();

            int NumberSource = Vsource.Length;
            for (i = 0; i < NumberSource; i++)
            {
                double xs = Xsource[i];
                double ys = Ysource[i];
                double vs = Vsource[i];
                double d = Math.Sqrt(((x - xs) * (x - xs) + (y - ys) * (y - ys)));

                _NodeList.Add(new Node(d, vs));
            }

            // 根据距离升序排列
            _NodeList.Sort();

            // 扫描半径
            double radius = _NodeList[NumbersOfPoint].distance;
            // 权重和
            double sumw = 0;
            // 被插值点值和
            double sumv = 0;

            for (i = 0; i < NumbersOfPoint; i++)
            {
                double v = _NodeList[i].value;
                double d = _NodeList[i].distance;
                double w = GressmanWeight(radius, d);

                sumv += v * w;  // 权重和
                sumw += w;      //源数据点的加权值之和
            }

            return sumw < 1.0e-12 ? 0 : sumv / sumw;

        }// InterpolateOnePoint - end

        /// <summary>
        /// Gressman权重
        /// </summary>
        /// <param name="radius">扫描半径</param>
        /// <param name="distance">两点间距离</param>
        /// <returns>Gressman权重</returns>
        private Double GressmanWeight(Double radius, Double distance)
        {
            Double weight = 0;

            if (distance < radius)
            {
                Double rr = radius * radius;
                Double dd = distance * distance;
                weight = (rr - dd) / (rr + dd);
            }

            return weight;

        }

        /// <summary>
        /// 高斯权重
        /// </summary>
        /// <param name="radius">扫描半径</param>
        /// <param name="distance">两点间距离</param>
        /// <returns>高斯权重</returns>
        private Double GaussWeight(Double radius, Double distance)
        {
            Double weight = 0;

            if (distance < radius)
            {
                Double rr = radius * radius;
                Double dd = distance * distance;
                weight = Math.Exp(-(rr + dd) / (rr - dd) / 4.0 * 10000);
                //weight = Math.Exp(-dd / 4.0 * 10000);
            }

            return weight;

        }

        #endregion


        //}}@@@
    }
}
