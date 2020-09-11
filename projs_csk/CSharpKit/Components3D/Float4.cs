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
using System.Runtime.InteropServices;

namespace CSharpKit
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
    public struct Float4
    {
        public Float4(float x, float y, float z)
            : this(x, y, z, 1.0f) { }

        public Float4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public float X;
        public float Y;
        public float Z;
        public float W;


        public static Float4 FromArgb(Argb argb)
        {
            float[] fva = new float[4] { argb.R / 255.0f, argb.G / 255.0f, argb.B / 255.0f, argb.A / 255.0f };
            return new Float4(fva[0], fva[1], fva[2], fva[3]);
        }

        //}}@@@
    }




}
