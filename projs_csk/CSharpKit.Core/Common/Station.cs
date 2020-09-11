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

namespace CSharpKit
{
    /// <summary>
    /// Station - 站点
    /// </summary>
    public class Station : Target, ICloneable<Station>
    {
        public Station()
            : base() { }

        protected Station(Station rhs)
            : base(rhs)
        {
            this.Lon = rhs.Lon;
            this.Lat = rhs.Lat;
            this.Alt = rhs.Alt;
            this.Level = rhs.Level;
        }

        /// <summary>
        /// 经度
        /// </summary>
        public Double Lon { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public Double Lat { get; set; }

        /// <summary>
        /// 海拔
        /// </summary>
        public Double Alt { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public Int32 Level { get; set; }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public Station Clone()
        {
            return new Station(this);
        }

        //@@@
    }










}
