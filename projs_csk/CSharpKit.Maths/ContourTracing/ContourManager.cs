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

namespace CSharpKit.Maths.ContourTracing
{
    /// <summary>
    /// ContourManager - 等值线管理器
    /// </summary>
    public class ContourManager
    {
        #region Constructors

        public ContourManager()
        {
            _TraceValues = new List<Double>();
        }

        public ContourManager(IEnumerable<Double> traceValues)
        {
            SetTraceValue(traceValues);
        }

        public ContourManager(Double[] traceValues)
        {
            SetTraceValue(traceValues);
        }

        public ContourManager(Double vInterval, Double vmin, Double vmax)
        {
            SetTraceValue(vInterval, vmin, vmax);
        }

        #endregion

        #region Properties

        private List<Double> _TraceValues;
        /// <summary>
        /// 追踪值集合
        /// </summary>
        public List<Double> TraceValues
        {
            get { return _TraceValues; }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// 设置要追踪的等值线值
        /// </summary>
        /// <param name="traceValues">等值线值数组</param>
        public Boolean SetTraceValue(Double[] traceValues)
        {
            return SetTraceValue(traceValues as IEnumerable<Double>);
        }

        /// <summary>
        /// 设置要追踪的等值线值
        /// </summary>
        /// <param name="vinterval">等值线值间隔</param>
        /// <param name="vmin">起始等值线值</param>
        /// <param name="vmax">终止等值线值</param>
        /// <returns></returns>
        public Boolean SetTraceValue(Double vinterval, Double vmin, Double vmax)
        {
            List<Double> _vlist = new List<Double>();

            // 等值线间隔不得等于零
            if (vinterval < 1.0e-12)
                return false;

            // 等值线间隔大于零
            // 则起始等值线值必须小于终止等值线值 --> ( (vInterval > 0) && (vmin < vmax) )
            if (vinterval > 0)
            {
                // 起始等值线值必须小于终止等值线值
                if (vmin > vmax)
                {
                    double temp = vmin;
                    vmin = vmax;
                    vmax = temp;
                }
            }

            // 等值线间隔小于零
            // 则起始等值线值必须大于终止等值线值 --> ( (vInterval < 0) && (vmin > vmax) )
            if (vinterval < 0)
            {
                // 起始等值线值必须大于终止等值线值
                if (vmin < vmax)
                {
                    double temp = vmin;
                    vmin = vmax;
                    vmax = temp;
                }
            }

            for (double value = vmin; value <= vmax; value += vinterval)
            {
                _vlist.Add(value);
            }

            return SetTraceValue(_vlist);
        }

        /// <summary>
        /// 设置要追踪的等值线值
        /// </summary>
        /// <param name="traceValues">等值线值集合</param>
        /// <returns></returns>
        public Boolean SetTraceValue(IEnumerable<Double> traceValues)
        {
            if (traceValues == null)
                return false;

            if (_TraceValues == null)
                _TraceValues = new List<Double>();

            TraceValues.Clear();
            TraceValues.AddRange(traceValues);

            return true;
        }

        #endregion
    }
}
