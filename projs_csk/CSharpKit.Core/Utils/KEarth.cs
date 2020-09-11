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
using System.Diagnostics;

namespace CSharpKit
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Earth
    {
        public Earth() { }

        #region Constants

        /// <summary>
        /// 名称：地球的长半轴<br/>
        /// 单位：km<br/>
        /// 备注：赤道半径 REQ<br/>
        /// </summary>
        public const double EA = 6378.1370;
        /// <summary>
        /// 名称：地球的短半轴<br/>
        /// 单位：km<br/>
        /// 备注：极半径 RPOL<br/>
        /// </summary>
        public const double EB = 6356.7523;

        /// <summary>
        /// 名称：地球扁率<br/>
        /// 单位：N/A<br/>
        /// 备注：f = (EA- EB) / EA;
        /// 长半轴a和短半轴b的差与长半轴的比值<br/>
        /// </summary>
        public static readonly double Flattening = (EA - EB) / EA;

        #endregion


        /// <summary>
        /// 地理经度转化成地心经度<br/>
        /// λe = lon;<br/>
        /// </summary>
        /// <param name="lon">地理经度(DEG)</param>
        /// <returns>地理经度(RAD)</returns>
        public static double LMD(double lon) => ToRadians(lon);
        /// <summary>
        /// 地理纬度转化成地心纬度<br/>
        /// φe = Math.Atan(eb2/ea2 * Math.Tan(lat))
        /// </summary>
        /// <param name="lat">地理纬度(DEG)</param>
        /// <returns>地心纬度(RAD)</returns>
        public static double PHI(double lat) => Math.Atan(Math.Tan(ToRadians(lat)) * (EB * EB) / (EA * EA));


        /// <summary>
        /// 地心指向地球表面任意一点的距离<br/>
        /// tmp = ((ea2-eb2)/ea2)*cos(φe)2
        /// re= eb/sqrt(1-tmp)
        /// </summary>
        /// <param name="lat">地理纬度(Deg)</param>
        /// <returns></returns>
        public static double Re(double lat)
        {
            double result = EB;

            try
            {
                var vtemp = result;
                {
                    double φe = PHI(lat);

                    double ea2 = EA * EA;
                    double eb2 = EB * EB;

                    double cosφe = Math.Cos(φe);
                    double cosφe2 = cosφe * cosφe;
                    double tmp = ((ea2 - eb2) / ea2) * cosφe2;

                    vtemp = EB / Math.Sqrt(1 - tmp);
                }
                result = vtemp;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }

            return result;
        }


        /// <summary>
        /// 角度转弧度
        /// </summary>
        /// <param name="deg"></param>
        /// <returns></returns>
        private static double ToRadians(double deg) => deg * Math.PI / 180.0;




        //@@@
    }

}
