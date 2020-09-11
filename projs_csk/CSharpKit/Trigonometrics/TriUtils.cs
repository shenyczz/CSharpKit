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

namespace CSharpKit.Trigonometrics
{
    // Chemistry    Chemistries
    // Geometry     Geometrics
    // Meteorology  Meteorological
    // Physics      Physical
    // Trigonometry Trigonometrics

    /// <summary>
    /// Trigonometry - 三角学
    /// </summary>
    public static class TriUtils
    {
        /// <summary>
        /// The number (pi)/180 - factor to convert from Degree (deg) to Radians (rad).
        /// 每角度的弧度
        /// </summary>
        public const double DegreeConstant = 0.017453292519943295769236907684886127134428718885417d;

        /// <summary>
        /// The number (pi)/200 - factor to convert from NewGrad (grad) to Radians (rad).
        /// </summary>
        public const double GradConstant = 0.015707963267948966192313216916397514420985846996876d;

        /// <summary>
        /// degree to grad. => 400.0 / 360.0
        /// </summary>
        public const double DegreeToGradConstant = 400.0 / 360.0;

        /// <summary>
        /// Converts a degree (360-periodic) angle to a grad (400-periodic) angle.
        /// </summary>
        /// <param name="degree">The degree to convert.</param>
        /// <returns>The converted grad angle.</returns>
        public static double DegreeToGrad(double degree)
        {
            return degree * DegreeToGradConstant;
        }

        /// <summary>
        /// Converts a degree (360-periodic) angle to a radian (2*Pi-periodic) angle.
        /// </summary>
        /// <param name="degree">The degree to convert.</param>
        /// <returns>The converted radian angle.</returns>
        public static double DegreeToRadian(double degree)
        {
            return degree * TriUtils.DegreeConstant;
        }

        /// <summary>
        /// Converts a grad (400-periodic) angle to a degree (360-periodic) angle.
        /// </summary>
        /// <param name="grad">The grad to convert.</param>
        /// <returns>The converted degree.</returns>
        public static double GradToDegree(double grad)
        {
            return grad * 0.9;
        }

        /// <summary>
        /// Converts a grad (400-periodic) angle to a radian (2*Pi-periodic) angle.
        /// </summary>
        /// <param name="grad">The grad to convert.</param>
        /// <returns>The converted radian.</returns>
        public static double GradToRadian(double grad)
        {
            return grad * TriUtils.GradConstant;
        }

        /// <summary>
        /// Converts a radian (2*Pi-periodic) angle to a degree (360-periodic) angle.
        /// </summary>
        /// <param name="radian">The radian to convert.</param>
        /// <returns>The converted degree.</returns>
        public static double RadianToDegree(double radian)
        {
            return radian / TriUtils.DegreeConstant;
        }

        /// <summary>
        /// Converts a radian (2*Pi-periodic) angle to a grad (400-periodic) angle.
        /// </summary>
        /// <param name="radian">The radian to convert.</param>
        /// <returns>The converted grad.</returns>
        public static double RadianToGrad(double radian)
        {
            return radian / TriUtils.GradConstant;
        }


        /// <summary>
        /// Normalized Sinc function. sinc(x) = sin(pi*x)/(pi*x).
        /// </summary>
        public static double Sinc(double x)
        {
            double z = Math.PI * x;
            return z.AlmostEqual(0.0, 15) ? 1.0 : Math.Sin(z) / z;
        }

        /// <summary>
        /// Trigonometric Sine of an angle in radian, or opposite / hypotenuse.
        /// </summary>
        /// <param name="radian">The angle in radian.</param>
        /// <returns>The sine of the radian angle.</returns>
        public static double Sin(double radian)
        {
            return Math.Sin(radian);
        }

        /// <summary>
        /// Hyperbolic Cosine
        /// </summary>
        /// <param name="angle">The hyperbolic angle, i.e. the area of the hyperbolic sector.</param>
        /// <returns>The hyperbolic Cosine of the angle.</returns>
        public static double Cosh(double angle)
        {
            return (Math.Exp(angle) + Math.Exp(-angle)) / 2;
        }

        /// <summary>
        /// Trigonometric Cosine of an angle in radian, or adjacent / hypotenuse.
        /// </summary>
        /// <param name="radian">The angle in radian.</param>
        /// <returns>The cosine of an angle in radian.</returns>
        public static double Cos(double radian)
        {
            return Math.Cos(radian);
        }

        /// <summary>
        /// Trigonometric Tangent of an angle in radian, or opposite / adjacent.
        /// </summary>
        /// <param name="radian">The angle in radian.</param>
        /// <returns>The tangent of the radian angle.</returns>
        public static double Tan(double radian)
        {
            return Math.Tan(radian);
        }

        /// <summary>
        /// Trigonometric Cotangent of an angle in radian, or adjacent / opposite. Reciprocal of the tangent.
        /// </summary>
        /// <param name="radian">The angle in radian.</param>
        /// <returns>The cotangent of an angle in radian.</returns>
        public static double Cot(double radian)
        {
            return 1 / Math.Tan(radian);
        }

        /// <summary>
        /// Trigonometric Secant of an angle in radian, or hypotenuse / adjacent. Reciprocal of the cosine.
        /// </summary>
        /// <param name="radian">The angle in radian.</param>
        /// <returns>The secant of the radian angle.</returns>
        public static double Sec(double radian)
        {
            return 1 / Math.Cos(radian);
        }

        /// <summary>
        /// Trigonometric Cosecant of an angle in radian, or hypotenuse / opposite. Reciprocal of the sine.
        /// </summary>
        /// <param name="radian">The angle in radian.</param>
        /// <returns>Cosecant of an angle in radian.</returns>
        public static double Csc(double radian)
        {
            return 1 / Math.Sin(radian);
        }

        /// <summary>
        /// Trigonometric principal Arc Sine in radian
        /// </summary>
        /// <param name="opposite">The opposite for a unit hypotenuse (i.e. opposite / hypotenuse).</param>
        /// <returns>The angle in radian.</returns>
        public static double Asin(double opposite)
        {
            return Math.Asin(opposite);
        }

        /// <summary>
        /// Trigonometric principal Arc Cosine in radian
        /// </summary>
        /// <param name="adjacent">The adjacent for a unit hypotenuse (i.e. adjacent / hypotenuse).</param>
        /// <returns>The angle in radian.</returns>
        public static double Acos(double adjacent)
        {
            return Math.Acos(adjacent);
        }

        /// <summary>
        /// Trigonometric principal Arc Tangent  in radian
        /// </summary>
        /// <param name="opposite">The opposite for a unit adjacent (i.e. opposite / adjacent).</param>
        /// <returns>The angle in radian.</returns>
        public static double Atan(double opposite)
        {
            return Math.Atan(opposite);
        }

        /// <summary>
        /// Trigonometric principal Arc Secant in radian
        /// </summary>
        /// <param name="hypotenuse">The hypotenuse for a unit adjacent (i.e. hypotenuse / adjacent).</param>
        /// <returns>The angle in radian.</returns>
        public static double Asec(double hypotenuse)
        {
            return Math.Acos(1 / hypotenuse);
        }

        /// <summary>
        /// Trigonometric principal Arc Cotangent in radian
        /// </summary>
        /// <param name="adjacent">The adjacent for a unit opposite (i.e. adjacent / opposite).</param>
        /// <returns>The angle in radian.</returns>
        public static double Acot(double adjacent)
        {
            return Math.Atan(1 / adjacent);
        }

        /// <summary>
        /// Trigonometric principal Arc Cosecant in radian
        /// </summary>
        /// <param name="hypotenuse">The hypotenuse for a unit opposite (i.e. hypotenuse / opposite).</param>
        /// <returns>The angle in radian.</returns>
        public static double Acsc(double hypotenuse)
        {
            return Math.Asin(1 / hypotenuse);
        }


        /// <summary>
        /// Hyperbolic Sine
        /// </summary>
        /// <param name="angle">The hyperbolic angle, i.e. the area of the hyperbolic sector.</param>
        /// <returns>The hyperbolic sine of the angle.</returns>
        public static double Sinh(double angle)
        {
            return (Math.Exp(angle) - Math.Exp(-angle)) / 2;
        }

        /// <summary>
        /// Hyperbolic Tangent in radian
        /// </summary>
        /// <param name="angle">The hyperbolic angle, i.e. the area of the hyperbolic sector.</param>
        /// <returns>The hyperbolic tangent of the angle.</returns>
        public static double Tanh(double angle)
        {
            if (angle > 19.1)
            {
                return 1.0;
            }

            if (angle < -19.1)
            {
                return -1;
            }

            var e1 = Math.Exp(angle);
            var e2 = Math.Exp(-angle);
            return (e1 - e2) / (e1 + e2);
        }

        /// <summary>
        /// Hyperbolic Cotangent（双曲余切（函数））
        /// </summary>
        /// <param name="angle">The hyperbolic angle, i.e. the area of the hyperbolic sector.</param>
        /// <returns>The hyperbolic cotangent of the angle.</returns>
        public static double Coth(double angle)
        {
            if (angle > 19.115)
            {
                return 1.0;
            }

            if (angle < -19.115)
            {
                return -1;
            }

            var e1 = Math.Exp(angle);
            var e2 = Math.Exp(-angle);
            return (e1 + e2) / (e1 - e2);
        }

        /// <summary>
        /// Hyperbolic Cosecant
        /// </summary>
        /// <param name="angle">The hyperbolic angle, i.e. the area of the hyperbolic sector.</param>
        /// <returns>The hyperbolic cosecant of the angle.</returns>
        public static double Csch(double angle)
        {
            return 1 / Sinh(angle);
        }

        /// <summary>
        /// Hyperbolic Secant
        /// </summary>
        /// <param name="angle">The hyperbolic angle, i.e. the area of the hyperbolic sector.</param>
        /// <returns>The hyperbolic secant of the angle.</returns>
        public static double Sech(double angle)
        {
            return 1 / Cosh(angle);
        }

        /// <summary>
        /// Hyperbolic Area Cosine
        /// </summary>
        /// <param name="value">The real value.</param>
        /// <returns>The hyperbolic angle, i.e. the area of its hyperbolic sector.</returns>
        public static double Acosh(double value)
        {
            // acosh(x) = ln(x + sqrt(x*x - 1))
            // if |x| >= 2^28, acosh(x) ~ ln(x) + ln(2)

            if (Math.Abs(value) >= 268435456.0) // 2^28, taken from freeBSD
                return Math.Log(value) + Math.Log(2.0);

            return Math.Log(value + (Math.Sqrt(value - 1) * Math.Sqrt(value + 1)), Math.E);
        }

        /// <summary>
        /// Hyperbolic Area Tangent
        /// </summary>
        /// <param name="value">The real value.</param>
        /// <returns>The hyperbolic angle, i.e. the area of its hyperbolic sector.</returns>
        public static double Atanh(double value)
        {
            return 0.5 * Math.Log((1 + value) / (1 - value), Math.E);
        }

        /// <summary>
        /// Hyperbolic Area Sine
        /// </summary>
        /// <param name="value">The real value.</param>
        /// <returns>The hyperbolic angle, i.e. the area of its hyperbolic sector.</returns>
        public static double Asinh(double value)
        {
            // asinh(x) = Sign(x) * ln(|x| + sqrt(x*x + 1))
            // if |x| > huge, asinh(x) ~= Sign(x) * ln(2|x|)

            if (Math.Abs(value) >= 268435456.0) // 2^28, taken from freeBSD
                return Math.Sign(value) * (Math.Log(Math.Abs(value)) + Math.Log(2.0));

            return Math.Sign(value) * Math.Log(Math.Abs(value) + Math.Sqrt((value * value) + 1));
        }


        /// <summary>
        /// Hyperbolic Area Cotangent
        /// </summary>
        /// <param name="value">The real value.</param>
        /// <returns>The hyperbolic angle, i.e. the area of its hyperbolic sector.</returns>
        public static double Acoth(double value)
        {
            return 0.5 * Math.Log((value + 1) / (value - 1), Math.E);
        }

        /// <summary>
        /// Hyperbolic Area Secant
        /// </summary>
        /// <param name="value">The real value.</param>
        /// <returns>The hyperbolic angle, i.e. the area of its hyperbolic sector.</returns>
        public static double Asech(double value)
        {
            return Acosh(1 / value);
        }

        /// <summary>
        /// Hyperbolic Area Cosecant
        /// </summary>
        /// <param name="value">The real value.</param>
        /// <returns>The hyperbolic angle, i.e. the area of its hyperbolic sector.</returns>
        public static double Acsch(double value)
        {
            return Asinh(1 / value);
        }

        //}}@@@
    }










}
