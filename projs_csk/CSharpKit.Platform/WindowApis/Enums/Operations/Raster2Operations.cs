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

namespace CSharpKit.Platform.Windows
{
    /// <summary>
    /// BinaryRasterOperations - 二元光栅操作
    /// </summary>
    public enum Raster2Operations
    {
        R2_BLACK = 1,   // 0    
        R2_NOTMERGEPEN = 2,   // DPon  
        R2_MASKNOTPEN = 3,   // DPna  
        R2_NOTCOPYPEN = 4,   // PN    
        R2_MASKPENNOT = 5,   // PDna  
        R2_NOT = 6,   // Dn    
        R2_XORPEN = 7,   // DPx   
        R2_NOTMASKPEN = 8,   // DPan  
        R2_MASKPEN = 9,   // DPa   
        R2_NOTXORPEN = 10,  // DPxn  
        R2_NOP = 11,  // D     
        R2_MERGENOTPEN = 12,  // DPno  
        R2_COPYPEN = 13,  // P     
        R2_MERGEPENNOT = 14,  // PDno  
        R2_MERGEPEN = 15,  // DPo   
        R2_WHITE = 16,  // 1    
        R2_LAST = 16,
    }

}
