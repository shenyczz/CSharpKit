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
    /// ClipArea - 剪切区
    /// </summary>
    public sealed class ClipArea : IClipArea
    {
        public ClipArea()
        {
            XClip = new Double[100];
            YClip = new Double[100];
        }

        /// <summary>
        /// 剪切区域标识
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 剪切区坐标点数量
        /// </summary>
        public Int32 ClipPointCount
        {
            get { return Id; }
        }

        /// <summary>
        /// 剪切区域X坐标集
        /// </summary>
        public Double[] XClip { get; set; }

        /// <summary>
        /// 剪切区域Y坐标集
        /// </summary>
        public Double[] YClip { get; set; }

        /// <summary>
        /// 剪切区X最小值
        /// </summary>
        public Double XClipMin
        {
            get { return XClip[0]; }
            set { XClip[0] = value; }
        }

        /// <summary>
        /// 剪切区Y最小值
        /// </summary>
        public Double YClipMin
        {
            get { return YClip[0]; }
            set { YClip[0] = value; }
        }

        /// <summary>
        /// 剪切区X最大值
        /// </summary>
        public Double XClipMax
        {
            get { return XClip[1]; }
            set { XClip[1] = value; }
        }

        /// <summary>
        /// 剪切区Y最大值
        /// </summary>
        public Double YClipMax
        {
            get { return YClip[1]; }
            set { YClip[1] = value; }
        }

        //@@@
    }



}
