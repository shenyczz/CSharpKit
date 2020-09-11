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
        /// 原子和核常数
        /// </summary>
        public static class Atomic
        {
            /// <summary>Fine Structure Constant: alpha = e^2/4*Pi*e_0*h_bar*c_0 [1] (2007 CODATA)</summary>
            public const double FineStructureConstant = 7.2973525376e-3;

            /// <summary>Rydberg Constant: R_infty = alpha^2*m_e*c_0/2*h [m^-1] (2007 CODATA)</summary>
            public const double RydbergConstant = 10973731.568528;

            /// <summary>Bor Radius: a_0 = alpha/4*Pi*R_infty [m] (2007 CODATA)</summary>
            public const double BohrRadius = 0.52917720859e-10;

            /// <summary>Hartree Energy: E_h = 2*R_infty*h*c_0 [J] (2007 CODATA)</summary>
            public const double HartreeEnergy = 4.35974394e-18;

            /// <summary>Quantum of Circulation: h/2*m_e [m^2 s^-1] (2007 CODATA)</summary>
            public const double QuantumOfCirculation = 3.6369475199e-4;

            /// <summary>Fermi Coupling Constant: G_F/(h_bar*c_0)^3 [GeV^-2] (2007 CODATA)</summary>
            public const double FermiCouplingConstant = 1.16637e-5;

            /// <summary>Weak Mixin Angle: sin^2(theta_W) [1] (2007 CODATA)</summary>
            public const double WeakMixingAngle = 0.22256;

            /// <summary>Electron Mass: [kg] (2007 CODATA)</summary>
            public const double ElectronMass = 9.10938215e-31;

            /// <summary>Electron Mass Energy Equivalent: [J] (2007 CODATA)</summary>
            public const double ElectronMassEnergyEquivalent = 8.18710438e-14;

            /// <summary>Electron Molar Mass: [kg mol^-1] (2007 CODATA)</summary>
            public const double ElectronMolarMass = 5.4857990943e-7;

            /// <summary>Electron Compton Wavelength: [m] (2007 CODATA)</summary>
            public const double ComptonWavelength = 2.4263102175e-12;

            /// <summary>Classical Electron Radius: [m] (2007 CODATA)</summary>
            public const double ClassicalElectronRadius = 2.8179402894e-15;

            /// <summary>Thomson Cross Section: [m^2] (2002 CODATA)</summary>
            public const double ThomsonCrossSection = 0.6652458558e-28;

            /// <summary>Electron Magnetic Moment: [J T^-1] (2007 CODATA)</summary>
            public const double ElectronMagneticMoment = -928.476377e-26;

            /// <summary>Electon G-Factor: [1] (2007 CODATA)</summary>
            public const double ElectronGFactor = -2.0023193043622;

            /// <summary>Muon Mass: [kg] (2007 CODATA)</summary>
            public const double MuonMass = 1.88353130e-28;

            /// <summary>Muon Mass Energy Equivalent: [J] (2007 CODATA)</summary>
            public const double MuonMassEnegryEquivalent = 1.692833511e-11;

            /// <summary>Muon Molar Mass: [kg mol^-1] (2007 CODATA)</summary>
            public const double MuonMolarMass = 0.1134289256e-3;

            /// <summary>Muon Compton Wavelength: [m] (2007 CODATA)</summary>
            public const double MuonComptonWavelength = 11.73444104e-15;

            /// <summary>Muon Magnetic Moment: [J T^-1] (2007 CODATA)</summary>
            public const double MuonMagneticMoment = -4.49044786e-26;

            /// <summary>Muon G-Factor: [1] (2007 CODATA)</summary>
            public const double MuonGFactor = -2.0023318414;

            /// <summary>Tau Mass: [kg] (2007 CODATA)</summary>
            public const double TauMass = 3.16777e-27;

            /// <summary>Tau Mass Energy Equivalent: [J] (2007 CODATA)</summary>
            public const double TauMassEnergyEquivalent = 2.84705e-10;

            /// <summary>Tau Molar Mass: [kg mol^-1] (2007 CODATA)</summary>
            public const double TauMolarMass = 1.90768e-3;

            /// <summary>Tau Compton Wavelength: [m] (2007 CODATA)</summary>
            public const double TauComptonWavelength = 0.69772e-15;

            /// <summary>Proton Mass: [kg] (2007 CODATA)</summary>
            public const double ProtonMass = 1.672621637e-27;

            /// <summary>Proton Mass Energy Equivalent: [J] (2007 CODATA)</summary>
            public const double ProtonMassEnergyEquivalent = 1.503277359e-10;

            /// <summary>Proton Molar Mass: [kg mol^-1] (2007 CODATA)</summary>
            public const double ProtonMolarMass = 1.00727646677e-3;

            /// <summary>Proton Compton Wavelength: [m] (2007 CODATA)</summary>
            public const double ProtonComptonWavelength = 1.3214098446e-15;

            /// <summary>Proton Magnetic Moment: [J T^-1] (2007 CODATA)</summary>
            public const double ProtonMagneticMoment = 1.410606662e-26;

            /// <summary>Proton G-Factor: [1] (2007 CODATA)</summary>
            public const double ProtonGFactor = 5.585694713;

            /// <summary>Proton Shielded Magnetic Moment: [J T^-1] (2007 CODATA)</summary>
            public const double ShieldedProtonMagneticMoment = 1.410570419e-26;

            /// <summary>Proton Gyro-Magnetic Ratio: [s^-1 T^-1] (2007 CODATA)</summary>
            public const double ProtonGyromagneticRatio = 2.675222099e8;

            /// <summary>Proton Shielded Gyro-Magnetic Ratio: [s^-1 T^-1] (2007 CODATA)</summary>
            public const double ShieldedProtonGyromagneticRatio = 2.675153362e8;

            /// <summary>Neutron Mass: [kg] (2007 CODATA)</summary>
            public const double NeutronMass = 1.674927212e-27;

            /// <summary>Neutron Mass Energy Equivalent: [J] (2007 CODATA)</summary>
            public const double NeutronMassEnegryEquivalent = 1.505349506e-10;

            /// <summary>Neutron Molar Mass: [kg mol^-1] (2007 CODATA)</summary>
            public const double NeutronMolarMass = 1.00866491597e-3;

            /// <summary>Neuron Compton Wavelength: [m] (2007 CODATA)</summary>
            public const double NeutronComptonWavelength = 1.3195908951e-1;

            /// <summary>Neutron Magnetic Moment: [J T^-1] (2007 CODATA)</summary>
            public const double NeutronMagneticMoment = -0.96623641e-26;

            /// <summary>Neutron G-Factor: [1] (2007 CODATA)</summary>
            public const double NeutronGFactor = -3.82608545;

            /// <summary>Neutron Gyro-Magnetic Ratio: [s^-1 T^-1] (2007 CODATA)</summary>
            public const double NeutronGyromagneticRatio = 1.83247185e8;

            /// <summary>Deuteron Mass: [kg] (2007 CODATA)</summary>
            public const double DeuteronMass = 3.34358320e-27;

            /// <summary>Deuteron Mass Energy Equivalent: [J] (2007 CODATA)</summary>
            public const double DeuteronMassEnegryEquivalent = 3.00506272e-10;

            /// <summary>Deuteron Molar Mass: [kg mol^-1] (2007 CODATA)</summary>
            public const double DeuteronMolarMass = 2.013553212725e-3;

            /// <summary>Deuteron Magnetic Moment: [J T^-1] (2007 CODATA)</summary>
            public const double DeuteronMagneticMoment = 0.433073465e-26;

            /// <summary>Helion Mass: [kg] (2007 CODATA)</summary>
            public const double HelionMass = 5.00641192e-27;

            /// <summary>Helion Mass Energy Equivalent: [J] (2007 CODATA)</summary>
            public const double HelionMassEnegryEquivalent = 4.49953864e-10;

            /// <summary>Helion Molar Mass: [kg mol^-1] (2007 CODATA)</summary>
            public const double HelionMolarMass = 3.0149322473e-3;

            /// <summary>Avogadro constant: [mol^-1] (2010 CODATA)</summary>
            public const double Avogadro = 6.0221412927e23;

            //}}@@@
        }
    }



}
