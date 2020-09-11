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
        /// 世界常数
        /// </summary>
        public static class Universal
        {
            /// <summary>
            /// Speed of Light in Vacuum（真空）<br/>
            /// c0 = 2.99792458e8 [m s^-1]<br/>
            /// </summary>
            public const double SpeedOfLight = 2.99792458e8;

            /// <summary>
            /// Magnetic Permeability(导磁率) in Vacuum<br/>
            /// mu0 = 4*Pi * 10^-7 [N A^-2 = kg m A^-2 s^-2]
            /// </summary>
            public const double MagneticPermeability = 1.2566370614359172953850573533118011536788677597500e-6;

            /// <summary>Electric Permittivity in Vacuum: epsilon_0 = 1/(mu_0*c_0^2) [F m^-1 = A^2 s^4 kg^-1 m^-3] (defined, exact; 2007 CODATA)</summary>
            public const double ElectricPermittivity = 8.8541878171937079244693661186959426889222899381429e-12;

            /// <summary>Characteristic Impedance of Vacuum: Z_0 = mu_0*c_0 [Ohm = m^2 kg s^-3 A^-2] (defined, exact; 2007 CODATA)</summary>
            public const double CharacteristicImpedanceVacuum = 376.73031346177065546819840042031930826862350835242;

            /// <summary>Newtonian Constant of Gravitation: G = 6.67429e-11 [m^3 kg^-1 s^-2] (2007 CODATA)</summary>
            public const double GravitationalConstant = 6.67429e-11;

            /// <summary>Planck's constant: h = 6.62606896e-34 [J s = m^2 kg s^-1] (2007 CODATA)</summary>
            public const double PlancksConstant = 6.62606896e-34;

            /// <summary>Reduced Planck's constant: h_bar = h / (2*Pi) [J s = m^2 kg s^-1] (2007 CODATA)</summary>
            public const double DiracsConstant = 1.054571629e-34;

            /// <summary>Planck mass: m_p = (h_bar*c_0/G)^(1/2) [kg] (2007 CODATA)</summary>
            public const double PlancksMass = 2.17644e-8;

            /// <summary>Planck temperature: T_p = (h_bar*c_0^5/G)^(1/2)/k [K] (2007 CODATA)</summary>
            public const double PlancksTemperature = 1.416786e32;

            /// <summary>Planck length: l_p = h_bar/(m_p*c_0) [m] (2007 CODATA)</summary>
            public const double PlancksLength = 1.616253e-35;

            /// <summary>Planck time: t_p = l_p/c_0 [s] (2007 CODATA)</summary>
            public const double PlancksTime = 5.39124e-44;

            //}}@@@
        }
    }


}
