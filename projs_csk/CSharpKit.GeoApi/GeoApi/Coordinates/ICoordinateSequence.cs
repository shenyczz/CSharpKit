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
    public interface ICoordinateSequence : ICloneable<ICoordinateSequence>
    {
        /// <summary>
        /// Returns the dimension (number of ordinates in each coordinate) for this sequence.
        /// </summary>
        int Dimension { get; }

        /// <summary>
        /// Returns the kind of ordinates this sequence supplys. .
        /// </summary>
        Ordinates Ordinates { get; }

        /// <summary>
        /// Returns the number of coordinates in this sequence.
        /// </summary>        
        int Count { get; }

        ICoordinate this[int index] { get; }

        ICoordinate[] ToArray();

        /// <summary>
        /// Creates a reversed version of this coordinate sequence with cloned <see cref="ICoordinate"/>s
        /// </summary>
        /// <returns>A reversed version of this sequence</returns>
        ICoordinateSequence Reversed();


        /*
    public interface ICoordinateSequence : ICloneable
    {

        /// <summary>
        /// Copies the i'th coordinate in the sequence to the supplied Coordinate.  
        /// At least the first two dimensions <b>must</b> be copied.        
        /// </summary>
        /// <param name="index">The index of the coordinate to copy.</param>
        /// <param name="coord">A Coordinate to receive the value.</param>
        void GetCoordinate(int index, Coordinate coord);

        /// <summary>
        /// Returns ordinate X (0) of the specified coordinate.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The value of the X ordinate in the index'th coordinate.</returns>
        double GetX(int index);

        /// <summary>
        /// Returns ordinate Y (1) of the specified coordinate.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The value of the Y ordinate in the index'th coordinate.</returns>
        double GetY(int index);

        /// <summary>
        /// Returns the ordinate of a coordinate in this sequence.
        /// Ordinate indices 0 and 1 are assumed to be X and Y.
        /// Ordinate indices greater than 1 have user-defined semantics
        /// (for instance, they may contain other dimensions or measure values).         
        /// </summary>
        /// <remarks>
        /// If the sequence does not provide value for the required ordinate, the implementation <b>must not</b> throw an exception, it should return <see cref="Coordinate.NullOrdinate"/>.
        /// </remarks>
        /// <param name="index">The coordinate index in the sequence.</param>
        /// <param name="ordinate">The ordinate index in the coordinate (in range [0, dimension-1]).</param>
        /// <returns>The ordinate value, or <see cref="Coordinate.NullOrdinate"/> if the sequence does not provide values for <paramref name="ordinate"/>"/></returns>       
        double GetOrdinate(int index, Ordinate ordinate);

        /// <summary>
        /// Sets the value for a given ordinate of a coordinate in this sequence.       
        /// </summary>
        /// <remarks>
        /// If the sequence can't store the ordinate value, the implementation <b>must not</b> throw an exception, it should simply ignore the call.
        /// </remarks>
        /// <param name="index">The coordinate index in the sequence.</param>
        /// <param name="ordinate">The ordinate index in the coordinate (in range [0, dimension-1]).</param>
        /// <param name="value">The new ordinate value.</param>       
        void SetOrdinate(int index, Ordinate ordinate, double value);

        /// <summary>
        /// Returns (possibly copies of) the Coordinates in this collection.
        /// Whether or not the Coordinates returned are the actual underlying
        /// Coordinates or merely copies depends on the implementation. Note that
        /// if this implementation does not store its data as an array of Coordinates,
        /// this method will incur a performance penalty because the array needs to
        /// be built from scratch.
        /// </summary>
        /// <returns></returns>

        /// <summary>
        /// Expands the given Envelope to include the coordinates in the sequence.
        /// Allows implementing classes to optimize access to coordinate values.      
        /// </summary>
        /// <param name="env">The envelope to expand.</param>
        /// <returns>A reference to the expanded envelope.</returns>       
        Envelope ExpandEnvelope(Envelope env);


        /// <summary>
        /// Returns a deep copy of this collection.
        /// </summary>
        /// <returns>A copy of the coordinate sequence containing copies of all points</returns>
        ICoordinateSequence Copy();
    }
         */

    }



}
