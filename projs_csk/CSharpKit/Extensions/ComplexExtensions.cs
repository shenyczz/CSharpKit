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
using System.Numerics;
using System.Runtime;

namespace CSharpKit
{
    /// <summary>
    /// Extension methods for the Complex type provided by System.Numerics
    /// </summary>
    public static class ComplexExtensions
    {
        /// <summary>
        /// Gets the squared magnitude of the <c>Complex</c> number.
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perform this operation on.</param>
        /// <returns>The squared magnitude of the <c>Complex</c> number.</returns>
        public static double MagnitudeSquared(this Complex complex)
        {
            return (complex.Real * complex.Real) + (complex.Imaginary * complex.Imaginary);
        }

        /// <summary>
        /// Gets the unity of this complex (same argument, but on the unit circle; exp(I*arg))
        /// </summary>
        /// <returns>The unity of this <c>Complex</c>.</returns>
        public static Complex Sign(this Complex complex)
        {
            if (double.IsPositiveInfinity(complex.Real) && double.IsPositiveInfinity(complex.Imaginary))
            {
                
                return new Complex(Constants.Mathematical.Sqrt1Over2, Constants.Mathematical.Sqrt1Over2);
            }

            if (double.IsPositiveInfinity(complex.Real) && double.IsNegativeInfinity(complex.Imaginary))
            {
                return new Complex(Constants.Mathematical.Sqrt1Over2, -Constants.Mathematical.Sqrt1Over2);
            }

            if (double.IsNegativeInfinity(complex.Real) && double.IsPositiveInfinity(complex.Imaginary))
            {
                return new Complex(-Constants.Mathematical.Sqrt1Over2, -Constants.Mathematical.Sqrt1Over2);
            }

            if (double.IsNegativeInfinity(complex.Real) && double.IsNegativeInfinity(complex.Imaginary))
            {
                return new Complex(-Constants.Mathematical.Sqrt1Over2, Constants.Mathematical.Sqrt1Over2);
            }

            var mod = Hypotenuse(complex.Real, complex.Imaginary);
            if (mod == 0.0d)
            {
                return Complex.Zero;
            }

            return new Complex(complex.Real / mod, complex.Imaginary / mod);
        }

        /// <summary>
        /// Gets the conjugate of the <c>Complex</c> number.
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perform this operation on.</param>
        /// <remarks>
        /// The semantic of <i>setting the conjugate</i> is such that
        /// <code>
        /// // a, b of type Complex32
        /// a.Conjugate = b;
        /// </code>
        /// is equivalent to
        /// <code>
        /// // a, b of type Complex32
        /// a = b.Conjugate
        /// </code>
        /// </remarks>
        /// <returns>The conjugate of the <see cref="Complex"/> number.</returns>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static Complex Conjugate(this Complex complex)
        {
            return Complex.Conjugate(complex);
        }

        /// <summary>
        /// Returns the multiplicative inverse of a complex number.
        /// </summary>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static Complex Reciprocal(this Complex complex)
        {
            return Complex.Reciprocal(complex);
        }

        /// <summary>
        /// Exponential of this <c>Complex</c> (exp(x), E^x).
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perform this operation on.</param>
        /// <returns>
        /// The exponential of this complex number.
        /// </returns>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static Complex Exp(this Complex complex)
        {
            return Complex.Exp(complex);
        }

        /// <summary>
        /// Natural Logarithm of this <c>Complex</c> (Base E).
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perform this operation on.</param>
        /// <returns>
        /// The natural logarithm of this complex number.
        /// </returns>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static Complex Ln(this Complex complex)
        {
            return Complex.Log(complex);
        }

        /// <summary>
        /// Common Logarithm of this <c>Complex</c> (Base 10).
        /// </summary>
        /// <returns>The common logarithm of this complex number.</returns>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static Complex Log10(this Complex complex)
        {
            return Complex.Log10(complex);
        }

        /// <summary>
        /// Logarithm of this <c>Complex</c> with custom base.
        /// </summary>
        /// <returns>The logarithm of this complex number.</returns>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static Complex Log(this Complex complex, double baseValue)
        {
            return Complex.Log(complex, baseValue);
        }

        /// <summary>
        /// Raise this <c>Complex</c> to the given value.
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perform this operation on.</param>
        /// <param name="exponent">
        /// The exponent.
        /// </param>
        /// <returns>
        /// The complex number raised to the given exponent.
        /// </returns>
        public static Complex Power(this Complex complex, Complex exponent)
        {
            if (complex.IsZero())
            {
                if (exponent.IsZero())
                {
                    return Complex.One;
                }

                if (exponent.Real > 0d)
                {
                    return Complex.Zero;
                }

                if (exponent.Real < 0d)
                {
                    return exponent.Imaginary == 0d
                        ? new Complex(double.PositiveInfinity, 0d)
                        : new Complex(double.PositiveInfinity, double.PositiveInfinity);
                }

                return new Complex(double.NaN, double.NaN);
            }

            return Complex.Pow(complex, exponent);
        }

        /// <summary>
        /// Raise this <c>Complex</c> to the inverse of the given value.
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perform this operation on.</param>
        /// <param name="rootExponent">
        /// The root exponent.
        /// </param>
        /// <returns>
        /// The complex raised to the inverse of the given exponent.
        /// </returns>
        public static Complex Root(this Complex complex, Complex rootExponent)
        {
            return Complex.Pow(complex, 1 / rootExponent);
        }

        /// <summary>
        /// The Square (power 2) of this <c>Complex</c>
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perform this operation on.</param>
        /// <returns>
        /// The square of this complex number.
        /// </returns>
        public static Complex Square(this Complex complex)
        {
            if (complex.IsReal())
            {
                return new Complex(complex.Real * complex.Real, 0.0);
            }

            return new Complex((complex.Real * complex.Real) - (complex.Imaginary * complex.Imaginary), 2 * complex.Real * complex.Imaginary);
        }

        /// <summary>
        /// The Square Root (power 1/2) of this <c>Complex</c>
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perform this operation on.</param>
        /// <returns>
        /// The square root of this complex number.
        /// </returns>
        public static Complex SquareRoot(this Complex complex)
        {
            // Note: the following code should be equivalent to Complex.Sqrt(complex),
            // but it turns out that is implemented poorly in System.Numerics,
            // hence we provide our own implementation here. Do not replace.

            if (complex.IsRealNonNegative())
            {
                return new Complex(Math.Sqrt(complex.Real), 0.0);
            }

            Complex result;

            var absReal = Math.Abs(complex.Real);
            var absImag = Math.Abs(complex.Imaginary);
            double w;
            if (absReal >= absImag)
            {
                var ratio = complex.Imaginary / complex.Real;
                w = Math.Sqrt(absReal) * Math.Sqrt(0.5 * (1.0 + Math.Sqrt(1.0 + (ratio * ratio))));
            }
            else
            {
                var ratio = complex.Real / complex.Imaginary;
                w = Math.Sqrt(absImag) * Math.Sqrt(0.5 * (Math.Abs(ratio) + Math.Sqrt(1.0 + (ratio * ratio))));
            }

            if (complex.Real >= 0.0)
            {
                result = new Complex(w, complex.Imaginary / (2.0 * w));
            }
            else if (complex.Imaginary >= 0.0)
            {
                result = new Complex(absImag / (2.0 * w), w);
            }
            else
            {
                result = new Complex(absImag / (2.0 * w), -w);
            }

            return result;
        }

        /// <summary>
        /// Evaluate all square roots of this <c>Complex</c>.
        /// </summary>
        public static Tuple<Complex, Complex> SquareRoots(this Complex complex)
        {
            var principal = SquareRoot(complex);
            return new Tuple<Complex, Complex>(principal, -principal);
        }

        /// <summary>
        /// Evaluate all cubic roots of this <c>Complex</c>.
        /// </summary>
        public static Tuple<Complex, Complex, Complex> CubicRoots(this Complex complex)
        {
            var r = Math.Pow(complex.Magnitude, 1d/3d);
            var theta = complex.Phase/3;
            const double shift = Math.PI * 2 / 3;
            return new Tuple<Complex, Complex, Complex>(
                Complex.FromPolarCoordinates(r, theta),
                Complex.FromPolarCoordinates(r, theta + shift),
                Complex.FromPolarCoordinates(r, theta - shift));
        }

        /// <summary>
        /// Gets a value indicating whether the <c>Complex32</c> is zero.
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perform this operation on.</param>
        /// <returns><c>true</c> if this instance is zero; otherwise, <c>false</c>.</returns>
        public static bool IsZero(this Complex complex)
        {
            return complex.Real == 0.0 && complex.Imaginary == 0.0;
        }

        /// <summary>
        /// Gets a value indicating whether the <c>Complex32</c> is one.
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perform this operation on.</param>
        /// <returns><c>true</c> if this instance is one; otherwise, <c>false</c>.</returns>
        public static bool IsOne(this Complex complex)
        {
            return complex.Real == 1.0 && complex.Imaginary == 0.0;
        }

        /// <summary>
        /// Gets a value indicating whether the <c>Complex32</c> is the imaginary unit.
        /// </summary>
        /// <returns><c>true</c> if this instance is ImaginaryOne; otherwise, <c>false</c>.</returns>
        /// <param name="complex">The <see cref="Complex"/> number to perform this operation on.</param>
        public static bool IsImaginaryOne(this Complex complex)
        {
            return complex.Real == 0.0 && complex.Imaginary == 1.0;
        }

        /// <summary>
        /// Gets a value indicating whether the provided <c>Complex32</c>evaluates
        /// to a value that is not a number.
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perform this operation on.</param>
        /// <returns>
        /// <c>true</c> if this instance is <c>NaN</c>; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static bool IsNaN(this Complex complex)
        {
            return double.IsNaN(complex.Real) || double.IsNaN(complex.Imaginary);
        }

        /// <summary>
        /// Gets a value indicating whether the provided <c>Complex32</c> evaluates to an
        /// infinite value.
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perform this operation on.</param>
        /// <returns>
        ///     <c>true</c> if this instance is infinite; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// True if it either evaluates to a complex infinity
        /// or to a directed infinity.
        /// </remarks>
        public static bool IsInfinity(this Complex complex)
        {
            return double.IsInfinity(complex.Real) || double.IsInfinity(complex.Imaginary);
        }

        /// <summary>
        /// Gets a value indicating whether the provided <c>Complex32</c> is real.
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perform this operation on.</param>
        /// <returns><c>true</c> if this instance is a real number; otherwise, <c>false</c>.</returns>
        public static bool IsReal(this Complex complex)
        {
            return complex.Imaginary == 0.0;
        }

        /// <summary>
        /// Gets a value indicating whether the provided <c>Complex32</c> is real and not negative, that is &gt;= 0.
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perform this operation on.</param>
        /// <returns>
        ///     <c>true</c> if this instance is real nonnegative number; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRealNonNegative(this Complex complex)
        {
            return complex.Imaginary == 0.0f && complex.Real >= 0;
        }

        /// <summary>
        /// Returns a Norm of a value of this type, which is appropriate for measuring how
        /// close this value is to zero.
        /// </summary>
        public static double Norm(this Complex complex)
        {
            return complex.MagnitudeSquared();
        }

        /// <summary>
        /// Returns a Norm of a value of this type, which is appropriate for measuring how
        /// close this value is to zero.
        /// </summary>
        // public static double Norm(this Complex32 complex)
        // {
        //     return complex.MagnitudeSquared;
        // }

        /// <summary>
        /// Returns a Norm of the difference of two values of this type, which is
        /// appropriate for measuring how close together these two values are.
        /// </summary>
        public static double NormOfDifference(this Complex complex, Complex otherValue)
        {
            return (complex - otherValue).MagnitudeSquared();
        }

        /// <summary>
        /// Trigonometric Sine of a <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The sine of the complex number.</returns>
        // public static Complex Sin(this Complex value)
        // {
        //     if (value.IsReal())
        //     {
        //         return new Complex(TriUtils.Sin(value.Real), 0.0);
        //     }

        //     return new Complex(
        //         TriUtils.Sin(value.Real) * TriUtils.Cosh(value.Imaginary),
        //         TriUtils.Cos(value.Real) * TriUtils.Sinh(value.Imaginary));
        // }

        /// <summary>
        /// Trigonometric Cosine of a <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The cosine of a complex number.</returns>
        // public static Complex Cos(this Complex value)
        // {
        //     if (value.IsReal())
        //     {
        //         return new Complex(TriUtils.Cos(value.Real), 0.0);
        //     }

        //     return new Complex(
        //         TriUtils.Cos(value.Real) * TriUtils.Cosh(value.Imaginary),
        //         -TriUtils.Sin(value.Real) * TriUtils.Sinh(value.Imaginary));
        // }

        /// <summary>
        /// Trigonometric Tangent of a <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The tangent of the complex number.</returns>
        // public static Complex Tan(this Complex value)
        // {
        //     if (value.IsReal())
        //     {
        //         return new Complex(TriUtils.Tan(value.Real), 0.0);
        //     }

        //     // tan(z) = - j*tanh(j*z)

        //     Complex z = Tanh(new Complex(-value.Imaginary, value.Real));
        //     return new Complex(z.Imaginary, -z.Real);
        // }

        /// <summary>
        /// Trigonometric Cotangent of a <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The cotangent of the complex number.</returns>
        // public static Complex Cot(this Complex value)
        // {
        //     if (value.IsReal())
        //     {
        //         return new Complex(TriUtils.Cot(value.Real), 0d);
        //     }

        //     // cot(z) = - j*coth(-j*z)

        //     Complex z = Coth(new Complex(value.Imaginary, -value.Real));
        //     return new Complex(z.Imaginary, -z.Real);
        // }

        /// <summary>
        /// Trigonometric Secant of a <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The secant of the complex number.</returns>
        // public static Complex Sec(this Complex value)
        // {
        //     if (value.IsReal())
        //     {
        //         return new Complex(TriUtils.Sec(value.Real), 0d);
        //     }

        //     var cosr = TriUtils.Cos(value.Real);
        //     var sinhi = TriUtils.Sinh(value.Imaginary);
        //     var denom = (cosr * cosr) + (sinhi * sinhi);

        //     return new Complex(cosr * TriUtils.Cosh(value.Imaginary) / denom, TriUtils.Sin(value.Real) * sinhi / denom);
        // }

        /// <summary>
        /// Trigonometric Cosecant of a <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The cosecant of a complex number.</returns>
        // public static Complex Csc(this Complex value)
        // {
        //     if (value.IsReal())
        //     {
        //         return new Complex(TriUtils.Csc(value.Real), 0d);
        //     }

        //     var sinr = TriUtils.Sin(value.Real);
        //     var sinhi = TriUtils.Sinh(value.Imaginary);
        //     var denom = (sinr * sinr) + (sinhi * sinhi);

        //     return new Complex(sinr * TriUtils.Cosh(value.Imaginary) / denom, -TriUtils.Cos(value.Real) * sinhi / denom);
        // }

        /// <summary>
        /// Trigonometric principal Arc Sine of this <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The arc sine of a complex number.</returns>
        public static Complex Asin(this Complex value)
        {
            if (value.Imaginary > 0 || value.Imaginary == 0d && value.Real < 0)
            {
                return -Asin(-value);
            }

            return -Complex.ImaginaryOne * ((1 - value.Square()).SquareRoot() + (Complex.ImaginaryOne * value)).Ln();
        }

        /// <summary>
        /// Trigonometric principal Arc Cosine of this <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The arc cosine of a complex number.</returns>
        public static Complex Acos(this Complex value)
        {
            if (value.Imaginary < 0 || value.Imaginary == 0d && value.Real > 0)
            {
                return Math.PI - Acos(-value);
            }

            return -Complex.ImaginaryOne * (value + (Complex.ImaginaryOne * (1 - value.Square()).SquareRoot())).Ln();
        }

        /// <summary>
        /// Trigonometric principal Arc Tangent of this <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The arc tangent of a complex number.</returns>
        public static Complex Atan(this Complex value)
        {
            var iz = new Complex(-value.Imaginary, value.Real); // I*this
            return new Complex(0, 0.5) * ((1 - iz).Ln() - (1 + iz).Ln());
        }

        /// <summary>
        /// Trigonometric principal Arc Cotangent of this <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The arc cotangent of a complex number.</returns>
        public static Complex Acot(this Complex value)
        {
            if (value.IsZero())
            {
                return Math.PI / 2;
            }

            var inv = Complex.ImaginaryOne / value;
            return (Complex.ImaginaryOne * 0.5) * ((1.0 - inv).Ln() - (1.0 + inv).Ln());
        }

        /// <summary>
        /// Trigonometric principal Arc Secant of this <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The arc secant of a complex number.</returns>
        public static Complex Asec(this Complex value)
        {
            var inv = 1 / value;
            return -Complex.ImaginaryOne * (inv + (Complex.ImaginaryOne * (1 - inv.Square()).SquareRoot())).Ln();
        }

        /// <summary>
        /// Trigonometric principal Arc Cosecant of this <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The arc cosecant of a complex number.</returns>
        public static Complex Acsc(this Complex value)
        {
            var inv = 1 / value;
            return -Complex.ImaginaryOne * ((Complex.ImaginaryOne * inv) + (1 - inv.Square()).SquareRoot()).Ln();
        }

        /// <summary>
        /// Hyperbolic Sine of a <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The hyperbolic sine of a complex number.</returns>
        // public static Complex Sinh(this Complex value)
        // {
        //     if (value.IsReal())
        //     {
        //         return new Complex(TriUtils.Sinh(value.Real), 0.0);
        //     }

        //     // sinh(x + j y) = sinh(x)*cos(y) + j*cosh(x)*sin(y)
        //     // if x > huge, sinh(x + jy) = sign(x)*exp(|x|)/2*cos(y) + j*exp(|x|)/2*sin(y)

        //     if (Math.Abs(value.Real) >= 22.0) // Taken from the msun library in FreeBSD
        //     {
        //         double h = Math.Exp(Math.Abs(value.Real)) * 0.5;
        //         return new Complex(
        //             Math.Sign(value.Real) * h * TriUtils.Cos(value.Imaginary),
        //             h * TriUtils.Sin(value.Imaginary));
        //     }

        //     return new Complex(
        //         TriUtils.Sinh(value.Real) * TriUtils.Cos(value.Imaginary),
        //         TriUtils.Cosh(value.Real) * TriUtils.Sin(value.Imaginary));
        // }

        /// <summary>
        /// Hyperbolic Cosine of a <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The hyperbolic cosine of a complex number.</returns>
        // public static Complex Cosh(this Complex value)
        // {
        //     if (value.IsReal())
        //     {
        //         return new Complex(TriUtils.Cosh(value.Real), 0.0);
        //     }

        //     // cosh(x + j*y) = cosh(x)*cos(y) + j*sinh(x)*sin(y)
        //     // if x > huge, cosh(x + j*y) = exp(|x|)/2*cos(y) + j*sign(x)*exp(|x|)/2*sin(y)

        //     if (Math.Abs(value.Real) >= 22.0) // Taken from the msun library in FreeBSD
        //     {
        //         double h = Math.Exp(Math.Abs(value.Real)) * 0.5;
        //         return new Complex(
        //             h * TriUtils.Cos(value.Imaginary),
        //             Math.Sign(value.Real) * h * TriUtils.Sin(value.Imaginary));
        //     }

        //     return new Complex(
        //         TriUtils.Cosh(value.Real) * TriUtils.Cos(value.Imaginary),
        //         TriUtils.Sinh(value.Real) * TriUtils.Sin(value.Imaginary));
        // }

        /// <summary>
        /// Hyperbolic Tangent of a <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The hyperbolic tangent of a complex number.</returns>
        // public static Complex Tanh(this Complex value)
        // {
        //     if (value.IsReal())
        //     {
        //         return new Complex(TriUtils.Tanh(value.Real), 0.0);
        //     }

        //     // tanh(x + j*y) = (cosh(x)*sinh(x)/cos^2(y) + j*tan(y))/(1 + sinh^2(x)/cos^2(y))
        //     // if |x| > huge, tanh(z) = sign(x) + j*4*cos(y)*sin(y)*exp(-2*|x|)
        //     // if exp(-|x|) = 0, tanh(z) = sign(x)
        //     // if tan(y) = +/- oo or 1/cos^2(y) = 1 + tan^2(y) = oo, tanh(z) = cosh(x)/sinh(x)
        //     //
        //     // The algorithm is based on Kahan.

        //     if (Math.Abs(value.Real) >= 22.0) // Taken from the msun library in FreeBSD
        //     {
        //         double e = Math.Exp(-Math.Abs(value.Real));
        //         return e == 0.0
        //             ? new Complex(Math.Sign(value.Real), 0.0)
        //             : new Complex(Math.Sign(value.Real), 4.0 * Math.Cos(value.Imaginary) * Math.Sin(value.Imaginary) * e * e);
        //     }

        //     double tani = TriUtils.Tan(value.Imaginary);
        //     double beta = 1 + tani * tani; // beta = 1/cos^2(y) = 1 + t^2
        //     double sinhr = TriUtils.Sinh(value.Real);
        //     double coshr = TriUtils.Cosh(value.Real);

        //     if (double.IsInfinity(tani))
        //         return new Complex(coshr / sinhr, 0.0);

        //     double denom = 1.0 + beta * sinhr * sinhr;
        //     return new Complex(beta * coshr * sinhr / denom, tani / denom);
        // }

        /// <summary>
        /// Hyperbolic Cotangent of a <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The hyperbolic cotangent of a complex number.</returns>
        // public static Complex Coth(this Complex value)
        // {
        //     if (value.IsReal())
        //     {
        //         return new Complex(TriUtils.Coth(value.Real), 0.0);
        //     }

        //     // Coth(z) = 1/tanh(z)

        //     return Complex.One / Tanh(value);
        // }

        /// <summary>
        /// Hyperbolic Secant of a <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The hyperbolic secant of a complex number.</returns>
        // public static Complex Sech(this Complex value)
        // {
        //     if (value.IsReal())
        //     {
        //         return new Complex(TriUtils.Sech(value.Real), 0.0);
        //     }

        //     // sech(x + j*y) = (cosh(x)/cos(y) - j*sinh(x)*tan(y)/cos(y))/(1 + sinh^2(x)/cos^2(y))
        //     // if |x| > huge, sech(z) = 4*cosh(x)*cos(y)*exp(-2*|x|) - j*4*sinh(x)*tan(y)*cos(y)*exp(-2*|x|)
        //     // if exp(-|x|) = 0, sech(z) = 0
        //     // if tan(y) = +/- oo or 1/cos^2(y) = 1 + tan^2(y) = oo, sech(z) = -j*sign(tan(y))/sinh(x)
        //     //
        //     // The algorithm is based on Kahan.

        //     double tani = TriUtils.Tan(value.Imaginary);
        //     double cosi = TriUtils.Cos(value.Imaginary);
        //     double beta = 1.0 + tani * tani;
        //     double sinhr = Math.Sinh(value.Real);
        //     double coshr = Math.Cosh(value.Real);

        //     if (Math.Abs(value.Real) >= 22.0) // Taken from the msun library in FreeBSD
        //     {
        //         double e = Math.Exp(-Math.Abs(value.Real));
        //         return e == 0.0
        //             ? new Complex(0, 0)
        //             : new Complex(4.0 * coshr * cosi * e * e, -4.0 * sinhr * tani * cosi * e * e);
        //     }

        //     if (double.IsInfinity(tani))
        //     {
        //         return new Complex(0.0, -Math.Sign(tani) / sinhr);
        //     }

        //     double denom = 1.0 + beta * sinhr * sinhr;
        //     return new Complex(coshr / cosi / denom, -sinhr * tani / cosi / denom);
        // }

        /// <summary>
        /// Hyperbolic Cosecant of a <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The hyperbolic cosecant of a complex number.</returns>
        // public static Complex Csch(this Complex value)
        // {
        //     if (value.IsReal())
        //     {
        //         return new Complex(TriUtils.Csch(value.Real), 0.0);
        //     }

        //     // csch(x + j*y) = (sinh(x)*cot(y)/sin(y) - j*cosh(x)/sin(y))/(1 + sinh^2(x)/sin^2(y))
        //     // if |x| > huge, csch(z) = 4*sinh(x)*cot(y)*sin(y)*exp(-2*|x|) - j*4*cosh(x)*sin(y)*exp(-2*|x|)
        //     // if exp(-|x|) = 0, csch(z) = 0
        //     // if cot(y) = +/- oo or 1/sin^2(x) = 1 + cot^2(x) = oo, csch(z) = sign(cot(y))/sinh(x)
        //     //
        //     // The algorithm is based on Kahan.

        //     double coti = TriUtils.Cot(value.Imaginary);
        //     double sini = TriUtils.Sin(value.Imaginary);
        //     double beta = 1 + coti * coti;
        //     double sinhr = TriUtils.Sinh(value.Real);
        //     double coshr = TriUtils.Cosh(value.Real);

        //     if (Math.Abs(value.Real) >= 22.0) // Taken from the msun library in FreeBSD
        //     {
        //         double e = Math.Exp(-Math.Abs(value.Real));
        //         return e == 0.0
        //             ? new Complex(0, 0)
        //             : new Complex(4.0 * sinhr * coti * sini * e * e, -4.0 * coshr * sini * e * e);
        //     }

        //     if (double.IsInfinity(coti))
        //     {
        //         return new Complex(Math.Sign(coti) / sinhr, 0.0);
        //     }

        //     double denom = 1.0 + beta * sinhr * sinhr;
        //     return new Complex(sinhr * coti / sini / denom, -coshr / sini / denom);
        // }

        /// <summary>
        /// Hyperbolic Area Sine of this <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The hyperbolic arc sine of a complex number.</returns>
        public static Complex Asinh(this Complex value)
        {
            return (value + (value.Square() + 1).SquareRoot()).Ln();
        }

        /// <summary>
        /// Hyperbolic Area Cosine of this <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The hyperbolic arc cosine of a complex number.</returns>
        public static Complex Acosh(this Complex value)
        {
            return (value + ((value - 1).SquareRoot() * (value + 1).SquareRoot())).Ln();
        }

        /// <summary>
        /// Hyperbolic Area Tangent of this <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The hyperbolic arc tangent of a complex number.</returns>
        public static Complex Atanh(this Complex value)
        {
            return 0.5 * ((1 + value).Ln() - (1 - value).Ln());
        }

        /// <summary>
        /// Hyperbolic Area Cotangent of this <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The hyperbolic arc cotangent of a complex number.</returns>
        public static Complex Acoth(this Complex value)
        {
            var inv = 1.0 / value;
            return 0.5 * ((1.0 + inv).Ln() - (1.0 - inv).Ln());
        }

        /// <summary>
        /// Hyperbolic Area Secant of this <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The hyperbolic arc secant of a complex number.</returns>
        public static Complex Asech(this Complex value)
        {
            var inv = 1 / value;
            return (inv + ((inv - 1).SquareRoot() * (inv + 1).SquareRoot())).Ln();
        }

        /// <summary>
        /// Hyperbolic Area Cosecant of this <c>Complex</c> number.
        /// </summary>
        /// <param name="value">The complex value.</param>
        /// <returns>The hyperbolic arc cosecant of a complex number.</returns>
        public static Complex Acsch(this Complex value)
        {
            var inv = 1 / value;
            return (inv + (inv.Square() + 1).SquareRoot()).Ln();
        }



        /// <summary>
        /// 求斜边（弦）
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static double Hypotenuse(double a, double b)
        {
            if (Math.Abs(a) > Math.Abs(b))
            {
                double r = b / a;
                return Math.Abs(a) * Math.Sqrt(1 + (r * r));
            }

            if (b != 0.0)
            {
                // NOTE (ruegg): not "!b.AlmostZero()" to avoid convergence issues (e.g. in SVD algorithm)
                double r = a / b;
                return Math.Abs(b) * Math.Sqrt(1 + (r * r));
            }

            return 0d;
        }



        //}}@@@
    }

}
