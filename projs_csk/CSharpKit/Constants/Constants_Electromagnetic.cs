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

namespace CSharpKit
{
    partial class Constants
    {
        /// <summary>
        /// 电磁常数
        /// </summary>
        public static class Electromagnetic
        {
            /// <summary>
            /// 基本电子电荷:
            /// Elementary Electron Charge: e = 1.602176487e-19 [C = A s] (2007 CODATA)
            /// </summary>
            public const double ElementaryCharge = 1.602176487e-19;

            /// <summary>Magnetic Flux Quantum: theta_0 = h/(2*e) [Wb = m^2 kg s^-2 A^-1] (2007 CODATA)</summary>
            public const double MagneticFluxQuantum = 2.067833668e-15;

            /// <summary>Conductance Quantum: G_0 = 2*e^2/h [S = m^-2 kg^-1 s^3 A^2] (2007 CODATA)</summary>
            public const double ConductanceQuantum = 7.7480917005e-5;

            /// <summary>Josephson Constant: K_J = 2*e/h [Hz V^-1] (2007 CODATA)</summary>
            public const double JosephsonConstant = 483597.891e9;

            /// <summary>Von Klitzing Constant: R_K = h/e^2 [Ohm = m^2 kg s^-3 A^-2] (2007 CODATA)</summary>
            public const double VonKlitzingConstant = 25812.807557;

            /// <summary>Bohr Magneton: mu_B = e*h_bar/2*m_e [J T^-1] (2007 CODATA)</summary>
            public const double BohrMagneton = 927.400915e-26;

            /// <summary>Nuclear Magneton: mu_N = e*h_bar/2*m_p [J T^-1] (2007 CODATA)</summary>
            public const double NuclearMagneton = 5.05078324e-27;

            //}}@@@
        }
    }


}
