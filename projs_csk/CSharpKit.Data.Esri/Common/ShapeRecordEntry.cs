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

namespace CSharpKit.Data.Esri
{
    /// <summary>
    /// ShapeRecordEntry - Shape 记录入口
    /// </summary>
    public sealed class ShapeRecordEntry
    {
        private readonly Int32 _Offset;
        private readonly Int32 _Length;

        public ShapeRecordEntry(Int32 offset, Int32 length)
        {
            _Offset = offset;
            _Length = length;
        }

        /// <summary>
        /// Offset of the record in 16-bit words from the 
        /// beginning of the shapefile.
        /// </summary>
        public Int32 Offset
        {
            get { return _Offset * 2; }
        }

        /// <summary>
        /// Number of 16-bit words taken up by the record.
        /// </summary>
        public Int32 Length
        {
            get { return _Length * 2; }
        }

        //public override String ToString()
        //{
        //    return String.Format("[IndexEntry] Offset: {0}; Length: {1}; Stream Position: {2}",
        //        Offset, Length, AbsoluteByteOffset);
        //}
    }



}
