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
    /// <summary>
    /// Class containing static methods for conversions
    /// between dimension values and characters.
    /// </summary>
    public sealed class DimensionUtility
    {
        /// <summary>
        /// Symbol for the FALSE pattern matrix entry
        /// </summary>
        public const char SymFalse = 'F';

        /// <summary>
        /// Symbol for the TRUE pattern matrix entry
        /// </summary>
        public const char SymTrue = 'T';

        /// <summary>
        /// Symbol for the DONTCARE pattern matrix entry
        /// </summary>
        public const char SymDontcare = '*';

        /// <summary>
        /// Symbol for the P (dimension 0) pattern matrix entry
        /// </summary>
        public const char SymP = '0';

        /// <summary>
        /// Symbol for the L (dimension 1) pattern matrix entry
        /// </summary>
        public const char SymL = '1';

        /// <summary>
        /// Symbol for the A (dimension 2) pattern matrix entry
        /// </summary>
        public const char SymA = '2';



        /// <summary>
        /// Converts the dimension value to a dimension symbol,
        /// for example, <c>True => 'T'</c>
        /// </summary>
        /// <param name="dimensionValue">Number that can be stored in the <c>IntersectionMatrix</c>.
        /// Possible values are <c>True, False, Dontcare, 0, 1, 2</c>.</param>
        /// <returns>Character for use in the string representation of an <c>IntersectionMatrix</c>.
        /// Possible values are <c>T, F, * , 0, 1, 2</c>.</returns>
        public static char ToDimensionSymbol(Dimension dimensionValue)
        {
            switch (dimensionValue)
            {
                case Dimension.False:
                    return SymFalse;
                case Dimension.True:
                    return SymTrue;
                case Dimension.Dontcare:
                    return SymDontcare;
                case Dimension.Point:
                    return SymP;
                case Dimension.Curve:
                    return SymL;
                case Dimension.Surface:
                    return SymA;
                default:
                    throw new ArgumentOutOfRangeException
                        ("Unknown dimension value: " + dimensionValue);
            }
        }

        /// <summary>
        /// Converts the dimension symbol to a dimension value,
        /// for example, <c>'*' => Dontcare</c>
        /// </summary>
        /// <param name="dimensionSymbol">Character for use in the string representation of an <c>IntersectionMatrix</c>.
        /// Possible values are <c>T, F, * , 0, 1, 2</c>.</param>
        /// <returns>Number that can be stored in the <c>IntersectionMatrix</c>.
        /// Possible values are <c>True, False, Dontcare, 0, 1, 2</c>.</returns>
        public static Dimension ToDimensionValue(char dimensionSymbol)
        {
            switch (Char.ToUpper(dimensionSymbol))
            {
                case SymFalse:
                    return Dimension.False;
                case SymTrue:
                    return Dimension.True;
                case SymDontcare:
                    return Dimension.Dontcare;
                case SymP:
                    return Dimension.Point;
                case SymL:
                    return Dimension.Curve;
                case SymA:
                    return Dimension.Surface;
                default:
                    throw new ArgumentOutOfRangeException
                        ("Unknown dimension symbol: " + dimensionSymbol);
            }
        }
    }










}
