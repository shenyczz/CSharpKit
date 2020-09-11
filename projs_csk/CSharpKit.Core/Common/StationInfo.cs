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
    /// StationInfo
    /// </summary>
    public class StationInfo : Station, ICloneable<StationInfo>
    {
        public StationInfo()
        {
            this.ElementValues = new Object[100];
        }

        protected StationInfo(StationInfo rhs)
            : base(rhs)
        {
            this.ElementCount = rhs.ElementCount;
            this.ElementValues = new Object[100];
            for (int i = 0; i < 100; i++)
            {
                this.ElementValues[i] = rhs.ElementValues[i];
            }
        }

        /// <summary>
        /// 要素数量
        /// </summary>
        public Int32 ElementCount { get; set; }

        /// <summary>
        /// 要素值
        /// </summary>
        public Object[] ElementValues { get; set; }

        public Int32 CurrentElementIndex { get; set; }

        public Object CurrentElementValue
        {
            get { return ElementValues[CurrentElementIndex]; }
            set { ElementValues[CurrentElementIndex] = value; }
        }


        public override string ToString()
        {
            return String.Format("{0}:{1}", Id, Name);
        }

        public new StationInfo Clone()
        {
            return new StationInfo(this);
        }

        //@@@
    }












}
