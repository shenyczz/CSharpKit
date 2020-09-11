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

namespace CSharpKit.GeoApi.Geometries
{
    //
    // 摘要:
    //     Interface for classes specifying the precision model of the Coordinates in a
    //     IGeometry. In other words, specifies the grid of allowable points for all IGeometrys.
    public interface IPrecisionModel : IComparable, IComparable<IPrecisionModel>
    {
        //
        // 摘要:
        //     Gets a value indicating the precision model type
        PrecisionModels PrecisionModelType { get; }
        //
        // 摘要:
        //     Gets a value indicating if this precision model has floating precision
        bool IsFloating { get; }
        //
        // 摘要:
        //     Gets a value indicating the maximum precision digits
        int MaximumSignificantDigits { get; }
        //
        // 摘要:
        //     Gets a value indicating the scale factor of a fixed precision model
        //
        // 言论：
        //     The number of decimal places of precision is equal to the base-10 logarithm of
        //     the scale factor. Non-integral and negative scale factors are supported. Negative
        //     scale factors indicate that the places of precision is to the left of the decimal
        //     point.
        double Scale { get; }

        //
        // 摘要:
        //     Function to compute a precised value of val
        //
        // 参数:
        //   val:
        //     The value to precise
        //
        // 返回结果:
        //     The precised value
        double MakePrecise(double val);
        //
        // 摘要:
        //     Method to precise coord.
        //
        // 参数:
        //   coord:
        //     The coordinate to precise
        void MakePrecise(ICoordinate coord);
    }















}
