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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace CSharpKit.GeoApi.Geometries
{
    /// <summary>
    /// Defines a rectangular region of the 2D coordinate plane. It is often used to
    /// represent the bounding box of a Geometry, e.g. the minimum and maximum x and
    /// y values of the Coordinates. Note that Envelopes support infinite or half-infinite
    /// regions, by using the values of Double.PositiveInfinity and Double.NegativeInfinity.
    /// When Envelope objects are created or initialized, the supplies extent values
    /// are automatically sorted into the correct order.
    /// </summary>
    public sealed class Envelope
        : IEnvelope
        , ICloneable
        , IComparable, IComparable<Envelope>
        , IEquatable<Envelope>
        , IExpandable<Envelope>
        , IIntersectable<Envelope>
    {
        #region Constructors

        public Envelope()
        {
            SetToNull();
        }

        public Envelope(double x1, double x2, double y1, double y2)
        {
            Init(x1, x2, y1, y2);
        }

        public Envelope(ICoordinate p1, ICoordinate p2)
            : this(p1.X, p2.X, p1.Y, p2.Y) { }

        /// <summary>
        /// Initialize an <c>Envelope</c> from an existing Envelope.
        /// </summary>
        /// <param name="rhs">The Envelope to initialize from.</param>
        private Envelope(Envelope rhs)
        {
            _minx = rhs._minx;
            _maxx = rhs._maxx;
            _miny = rhs._miny;
            _maxy = rhs._maxy;
        }

        private void Init(double x1, double x2, double y1, double y2)
        {
            if (x1 < x2)
            {
                _minx = x1;
                _maxx = x2;
            }
            else
            {
                _minx = x2;
                _maxx = x1;
            }

            if (y1 < y2)
            {
                _miny = y1;
                _maxy = y2;
            }
            else
            {
                _miny = y2;
                _maxy = y1;
            }
        }


        #endregion

        #region Fields

        private double _minx;
        private double _maxx;
        private double _miny;
        private double _maxy;

        #endregion

        #region Properties

        public double MinX
        {
            get { return _minx; }
        }
        public double MaxX
        {
            get { return _maxx; }
        }
        public double MinY
        {
            get { return _miny; }
        }
        public double MaxY
        {
            get { return _maxy; }
        }

        public double Width
        {
            get { return IsNull ? 0 : (_maxx - _minx); }
        }
        public double Height
        {
            get { return IsNull ? 0 : (_maxy - _miny); }
        }

        public bool IsNull
        {
            get
            {
                return (_minx > _maxx) || (_miny > _maxy);
            }
        }

        public double Area
        {
            get
            {
                return Width * Height;
            }
        }

        public ICoordinate Centre
        {
            get
            {
                return IsNull ? null
                    : new Coordinate((MinX + MaxX) / 2.0, (MinY + MaxY) / 2.0);
            }
        }

        public double MinExtent
        {
            get
            {
                if (IsNull) return 0.0;
                double w = Width;
                double h = Height;
                if (w < h) return w;
                return h;
            }
        }

        public double MaxExtent
        {
            get
            {
                if (IsNull) return 0.0;
                double w = Width;
                double h = Height;
                if (w > h) return w;
                return h;
            }
        }

        #endregion


        /// <summary>
        /// Makes this <c>Envelope</c> a "null" envelope..
        /// </summary>
        public void SetToNull()
        {
            _minx = 0;
            _maxx = -1;
            _miny = 0;
            _maxy = -1;
        }

        /// <summary>
        /// Expands this envelope by a given distance in all directions.
        /// Both positive and negative distances are supported.
        /// </summary>
        /// <param name="deltaX">The distance to expand the envelope along the the X axis.</param>
        /// <param name="deltaY">The distance to expand the envelope along the the Y axis.</param>
        public void ExpandBy(double deltaX, double deltaY)
        {
            if (IsNull)
                return;

            _minx -= deltaX;
            _maxx += deltaX;
            _miny -= deltaY;
            _maxy += deltaY;

            // check for envelope disappearing
            if (_minx > _maxx || _miny > _maxy)
                SetToNull();
        }

        public Envelope ExpandedBy(Envelope other)
        {
            if (other.IsNull)
                return this;
            if (IsNull)
                return other;

            var minx = (other._minx < _minx) ? other._minx : _minx;
            var maxx = (other._maxx > _maxx) ? other._maxx : _maxx;
            var miny = (other._miny < _miny) ? other._miny : _miny;
            var maxy = (other._maxy > _maxy) ? other._maxy : _maxy;
            return new Envelope(minx, maxx, miny, maxy);
        }

        public void ExpandToInclude(double x, double y)
        {
            if (IsNull)
            {
                _minx = x;
                _maxx = x;
                _miny = y;
                _maxy = y;
            }
            else
            {
                if (x < _minx)
                    _minx = x;
                if (x > _maxx)
                    _maxx = x;
                if (y < _miny)
                    _miny = y;
                if (y > _maxy)
                    _maxy = y;
            }
        }

        public void ExpandToInclude(Envelope other)
        {
            if (other.IsNull)
                return;
            if (IsNull)
            {
                _minx = other.MinX;
                _maxx = other.MaxX;
                _miny = other.MinY;
                _maxy = other.MaxY;
            }
            else
            {
                if (other.MinX < _minx)
                    _minx = other.MinX;
                if (other.MaxX > _maxx)
                    _maxx = other.MaxX;
                if (other.MinY < _miny)
                    _miny = other.MinY;
                if (other.MaxY > _maxy)
                    _maxy = other.MaxY;
            }
        }

        public void Translate(double transX, double transY)
        {
            if (IsNull)
                return;
            Init(MinX + transX, MaxX + transX, MinY + transY, MaxY + transY);
        }

        /// <summary>
        /// Computes the intersection of two <see cref="Envelope"/>s.
        /// </summary>
        /// <param name="env">The envelope to intersect with</param>
        /// <returns>
        /// A new Envelope representing the intersection of the envelopes (this will be
        /// the null envelope if either argument is null, or they do not intersect
        /// </returns>
        public Envelope Intersection(Envelope env)
        {
            if (IsNull || env.IsNull || !Intersects(env))
                return new Envelope();

            return new Envelope(Math.Max(MinX, env.MinX),
                                Math.Min(MaxX, env.MaxX),
                                Math.Max(MinY, env.MinY),
                                Math.Min(MaxY, env.MaxY));
        }


        /// <summary>
        /// Check if the region defined by <c>other</c>
        /// intersects the region of this <c>Envelope</c>.
        /// </summary>
        /// <param name="other">The <c>Envelope</c> which this <c>Envelope</c> is
        /// being checked for intersecting.
        /// </param>
        /// <returns>
        /// <c>true</c> if the <c>Envelope</c>s intersect.
        /// </returns>
        public bool Intersects(Envelope other)
        {
            if (IsNull || other.IsNull)
                return false;
            return !(other.MinX > _maxx || other.MaxX < _minx || other.MinY > _maxy || other.MaxY < _miny);
        }

        /// <summary>
        /// Check if the point <c>(x, y)</c> overlaps (lies inside) the region of this <c>Envelope</c>.
        /// </summary>
        /// <param name="x"> the x-ordinate of the point.</param>
        /// <param name="y"> the y-ordinate of the point.</param>
        /// <returns><c>true</c> if the point overlaps this <c>Envelope</c>.</returns>
        public bool Intersects(double x, double y)
        {
            return !(x > _maxx || x < _minx || y > _maxy || y < _miny);
        }

        /// <summary>
        /// Check if the point <c>p</c> overlaps (lies inside) the region of this <c>Envelope</c>.
        /// </summary>
        /// <param name="p"> the <c>Coordinate</c> to be tested.</param>
        /// <returns><c>true</c> if the point overlaps this <c>Envelope</c>.</returns>
        public bool Intersects(ICoordinate p)
        {
            return Intersects(p.X, p.Y);
        }


        /// <summary>
        /// Check if the extent defined by two extremal points
        /// intersects the extent of this <code>Envelope</code>.
        /// </summary>
        /// <param name="a">A point</param>
        /// <param name="b">Another point</param>
        /// <returns><c>true</c> if the extents intersect</returns>
        public bool Intersects(ICoordinate a, ICoordinate b)
        {
            if (IsNull) return false;

            var envminx = (a.X < b.X) ? a.X : b.X;
            if (envminx > _maxx) return false;

            var envmaxx = (a.X > b.X) ? a.X : b.X;
            if (envmaxx < _minx) return false;

            var envminy = (a.Y < b.Y) ? a.Y : b.Y;
            if (envminy > _maxy) return false;

            var envmaxy = (a.Y > b.Y) ? a.Y : b.Y;
            if (envmaxy < _miny) return false;

            return true;
        }


        ///<summary>
        /// Tests if the given point lies in or on the envelope.
        ///</summary>
        /// <param name="x">the x-coordinate of the point which this <c>Envelope</c> is being checked for containing</param>
        /// <param name="y">the y-coordinate of the point which this <c>Envelope</c> is being checked for containing</param>
        /// <returns> <c>true</c> if <c>(x, y)</c> lies in the interior or on the boundary of this <c>Envelope</c>.</returns>
        public bool Covers(double x, double y)
        {
            if (IsNull) return false;
            return x >= _minx &&
                x <= _maxx &&
                y >= _miny &&
                y <= _maxy;
        }

        public bool Covers(ICoordinate p)
        {
            return Covers(p.X, p.Y);
        }

        public bool Covers(Envelope other)
        {
            if (IsNull || other.IsNull)
                return false;
            return other.MinX >= _minx &&
                other.MaxX <= _maxx &&
                other.MinY >= _miny &&
                other.MaxY <= _maxy;
        }

        public bool Contains(double x, double y)
        {
            return Covers(x, y);
        }
        public bool Contains(ICoordinate p)
        {
            return Covers(p);
        }
        public bool Contains(Envelope other)
        {
            return Covers(other);
        }


        public double Distance(Envelope env)
        {
            if (Intersects(env))
                return 0;

            double dx = 0.0;

            if (_maxx < env.MinX)
                dx = env.MinX - _maxx;
            else if (_minx > env.MaxX)
                dx = _minx - env.MaxX;

            double dy = 0.0;

            if (_maxy < env.MinY)
                dy = env.MinY - _maxy;
            else if (_miny > env.MaxY)
                dy = _miny - env.MaxY;

            // if either is zero, the envelopes overlap either vertically or horizontally
            if (dx == 0.0)
                return dy;
            if (dy == 0.0)
                return dx;

            return Math.Sqrt(dx * dx + dy * dy);
        }

        #region IClone Members

        public Envelope Clone()
        {
            return new Envelope(this);
        }

        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IComparable Members

        public int CompareTo(object obj)
        {
            return CompareTo(obj as Envelope);
        }

        public int CompareTo(Envelope env)
        {
            //if(env==null)...
            env = env ?? new Envelope();

            // compare nulls if present
            if (IsNull)
            {
                if (env.IsNull) return 0;
                return -1;
            }
            else
            {
                if (env.IsNull) return 1;
            }

            // compare based on numerical ordering of ordinates
            if (MinX < env.MinX) return -1;
            if (MinX > env.MinX) return 1;
            if (MinY < env.MinY) return -1;
            if (MinY > env.MinY) return 1;
            if (MaxX < env.MaxX) return -1;
            if (MaxX > env.MaxX) return 1;
            if (MaxY < env.MaxY) return -1;
            if (MaxY > env.MaxY) return 1;
            return 0;
        }



        #endregion

        #region IEquatable<Envelope> Members

        public bool Equals(Envelope other)
        {
            if (IsNull)
                return other.IsNull;

            return _maxx == other.MaxX && _maxy == other.MaxY &&
                   _minx == other.MinX && _miny == other.MinY;
        }

        #endregion


        #region Override functions

        public override bool Equals(object obj)
        {
            return Equals(obj as Envelope);
        }

        public override int GetHashCode()
        {
            var result = 17;
            // ReSharper disable NonReadonlyFieldInGetHashCode
            result = 37 * result + GetHashCode(_minx);
            result = 37 * result + GetHashCode(_maxx);
            result = 37 * result + GetHashCode(_miny);
            result = 37 * result + GetHashCode(_maxy);
            // ReSharper restore NonReadonlyFieldInGetHashCode
            return result;
        }

        private static int GetHashCode(double value)
        {
            var f = BitConverter.DoubleToInt64Bits(value);
            return (int)(f ^ (f >> 32));

            // This was implemented as follows, but that's actually equivalent:
            //return value.GetHashCode();
       }

        public override string ToString()
        {
            var sb = new StringBuilder("Env[");
            if (IsNull)
            {
                sb.Append("Null]");
            }
            else
            {
                sb.AppendFormat(NumberFormatInfo.InvariantInfo, "{0:R} : {1:R}, ", _minx, _maxx);
                sb.AppendFormat(NumberFormatInfo.InvariantInfo, "{0:R} : {1:R}]", _miny, _maxy);
            }
            return sb.ToString();

            //return "Env[" + _minx + " : " + _maxx + ", " + _miny + " : " + _maxy + "]";
        }

        #endregion


        /// <summary>
        /// Method to parse an envelope from its <see cref="Envelope.ToString"/> value
        /// </summary>
        /// <param name="envelope">The envelope string</param>
        /// <returns>The envelope</returns>
        public static Envelope Parse(string envelope)
        {
            if (string.IsNullOrEmpty(envelope))
                throw new ArgumentNullException("envelope");
            if (!(envelope.StartsWith("Env[") && envelope.EndsWith("]")))
                throw new ArgumentException("Not a valid envelope string", "envelope");

            // test for null
            envelope = envelope.Substring(4, envelope.Length - 5);
            if (envelope == "Null")
                return new Envelope();

            // Parse values
            var ordinatesValues = new double[4];
            var ordinateLabel = new[] { "x", "y" };
            var j = 0;

            // split into ranges
            var parts = envelope.Split(',');
            if (parts.Length != 2)
                throw new ArgumentException("Does not provide two ranges", "envelope");

            foreach (var part in parts)
            {
                // Split int min/max
                var ordinates = part.Split(':');
                if (ordinates.Length != 2)
                    throw new ArgumentException("Does not provide just min and max values", "envelope");

                if (!ValueParser.TryParse(ordinates[0].Trim(), NumberStyles.Number, NumberFormatInfo.InvariantInfo, out ordinatesValues[2 * j]))
                    throw new ArgumentException(string.Format("Could not parse min {0}-Ordinate", ordinateLabel[j]), "envelope");
                if (!ValueParser.TryParse(ordinates[1].Trim(), NumberStyles.Number, NumberFormatInfo.InvariantInfo, out ordinatesValues[2 * j + 1]))
                    throw new ArgumentException(string.Format("Could not parse max {0}-Ordinate", ordinateLabel[j]), "envelope");
                j++;
            }

            return new Envelope(ordinatesValues[0], ordinatesValues[1],
                                ordinatesValues[2], ordinatesValues[3]);
        }

        /// <summary>
        /// Test the point q to see whether it intersects the Envelope
        /// defined by p1-p2.
        /// </summary>
        /// <param name="p1">One extremal point of the envelope.</param>
        /// <param name="p2">Another extremal point of the envelope.</param>
        /// <param name="q">Point to test for intersection.</param>
        /// <returns><c>true</c> if q intersects the envelope p1-p2.</returns>
        public static bool Intersects(ICoordinate p1, ICoordinate p2, ICoordinate q)
        {
            return ((q.X >= (p1.X < p2.X ? p1.X : p2.X)) && (q.X <= (p1.X > p2.X ? p1.X : p2.X))) &&
                   ((q.Y >= (p1.Y < p2.Y ? p1.Y : p2.Y)) && (q.Y <= (p1.Y > p2.Y ? p1.Y : p2.Y)));
        }

        /// <summary>
        /// Tests whether the envelope defined by p1-p2
        /// and the envelope defined by q1-q2
        /// intersect.
        /// </summary>
        /// <param name="p1">One extremal point of the envelope Point.</param>
        /// <param name="p2">Another extremal point of the envelope Point.</param>
        /// <param name="q1">One extremal point of the envelope Q.</param>
        /// <param name="q2">Another extremal point of the envelope Q.</param>
        /// <returns><c>true</c> if Q intersects Point</returns>
        public static bool Intersects(ICoordinate p1, ICoordinate p2, ICoordinate q1, ICoordinate q2)
        {
            double minp = Math.Min(p1.X, p2.X);
            double maxq = Math.Max(q1.X, q2.X);
            if (minp > maxq)
                return false;

            double minq = Math.Min(q1.X, q2.X);
            double maxp = Math.Max(p1.X, p2.X);
            if (maxp < minq)
                return false;

            minp = Math.Min(p1.Y, p2.Y);
            maxq = Math.Max(q1.Y, q2.Y);
            if (minp > maxq)
                return false;

            minq = Math.Min(q1.Y, q2.Y);
            maxp = Math.Max(p1.Y, p2.Y);
            if (maxp < minq)
                return false;

            return true;
        }


        //@EndOf(Envelope)
    }


    internal static class ValueParser
    {
        /// <summary>
        /// Attempts to convert the string representation of a number in a
        /// specified style and culture-specific format to its double-precision
        /// floating-point number equivalent.
        /// </summary>
        /// <param name="s">The string to attempt to parse.</param>
        /// <param name="style">
        /// A bitwise combination of <see cref="System.Globalization.NumberStyles"/>
        /// values that indicates the permitted format of <paramref name="s"/>.
        /// </param>
        /// <param name="provider">
        /// A <see cref="System.IFormatProvider"/> that supplies
        /// culture-specific formatting information about <paramref name="s"/>.
        /// </param>
        /// <param name="result">The result of the parsed string, or zero if parsing failed.</param>
        /// <returns>A boolean value indicating whether or not the parse succeeded.</returns>
        /// <remarks>Returns 0 in the result parameter if the parse fails.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out double result)
        {
            bool retVal = false;

            try
            {
                result = double.Parse(s, style, provider);
                retVal = true;
            }
            catch (FormatException)
            { result = 0; }
            catch (InvalidCastException)
            { result = 0; }

            return retVal;
        }
    }


}
