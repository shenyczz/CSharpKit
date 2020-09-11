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
/* 扫描平面
 *
 *         扫描平面
 *            /
 *           /
 *          /
 *         /
 *        /
 *       /  仰角
 *      -------------------- 0度平面
 *
 * 如图所示：
 *          扫描平面=>0度平面需要乘以cos(仰角)
 *          0度平面=>扫描平面需要除以cos(仰角)
 *
 * 注意，日常显示的雷达图是扫描平面上的图。本类所说的屏幕指扫描平面。
 * 
 */
/*雷达扫描示意图
 * 
 *
 *                        0(360)
 *                        |     radius
 *                        |       /
 *                        |      /
 *                        |angle/
 *                        |    /
 *                        | ^ /
 *                        |  /
 *                        | /
 *                        |/
 * 270 -----------------中心------------------ 90
 *                        |
 *                        |
 *                        |
 *                        |
 *                        |
 *                        |
 *                        |
 *                        |
 *                        |
 *                       180
 *                       
 *********************************************************************/

using System;

namespace CSharpKit.Maths
{
    /// <summary>
    /// 极坐标
    /// </summary>
    public sealed class KPolar
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="centLon"></param>
        /// <param name="centLat"></param>
        public KPolar(double centLon, double centLat)
            : this(centLon, centLat, 0.998) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="centLon">中心经度</param>
        /// <param name="centLat">中心纬度</param>
        /// <param name="ppkm">每公里像素数</param>
        public KPolar(double centLon, double centLat, double ppkm)
        {
            CenterLongitude = centLon;
            CenterLatitude = centLat;

            PixelPerKm = ppkm;

            CenterX = 255;
            CenterY = 255;

            Elevation = 0;
        }


        #endregion

        #region Constants

        public const double EarthRadius = 6371.004;         // 地球平均半径，单位：公里(km)
        public const double EarthRadius_Polar = 6356.755;   // EB，地球两极半径，单位：公里(km)
        public const double EarthRadius_Equator = 6373.140; // EA，地球赤道半径，单位：公里(km)

        #endregion


        #region 中心经纬坐标

        /// <summary>
        /// 中心经度
        /// </summary>
        public Double CenterLongitude { get; set; }
        /// <summary>
        /// 中心纬度
        /// </summary>
        public Double CenterLatitude { get; set; }

        #endregion

        #region 中心屏幕坐标（图像固定为 512 x 512 点阵）

        /// <summary>
        /// 屏幕中心X坐标(列)
        /// </summary>
        public int CenterX { get; set; }
        /// <summary>
        /// 屏幕中心X坐标(行)
        /// </summary>
        public int CenterY { get; set; }

        #endregion

        #region 扫描仰角

        public Double Elevation { get; set; }

        #endregion

        #region 每公里像素 - PixelPerKm

        /// <summary>
        /// 每公里像素（PPKM）
        /// </summary>
        /// <remarks>
        /// value = 1000 / 距离库长度?
        /// </remarks>
        public Double PixelPerKm { get; set; }

        #endregion

        #region 每经度公里

        public Double KmPerLongitude
        {
            get
            {
                return this.DistanceOfSphere(CenterLongitude, CenterLatitude,
                    CenterLongitude + 1.0, CenterLatitude) / Math.Cos(ToRadian(Elevation));
            }
        }

        #endregion

        #region 每纬度公里

        public Double KmPerLatitude
        {
            get
            {
                return this.DistanceOfSphere(CenterLongitude, CenterLatitude,
                    CenterLongitude, CenterLatitude + 1.0) / Math.Cos(ToRadian(Elevation));
            }
        }

        #endregion




        /// <summary>
        /// 计算球面距离（km）
        /// </summary>
        /// <param name="dLon0"></param>
        /// <param name="dLat0"></param>
        /// <param name="dLon1"></param>
        /// <param name="dLat1"></param>
        /// <returns></returns>
        public double DistanceOfSphere(double dLon0, double dLat0, double dLon1, double dLat1)
        {
            double dValue = 0;

            // Google
            // A(a1,b1), B(a2,b2)
            // AB球面距离 = R*arccos[cos(b1)*cos(b2)*cos(a1-a2)+sin(b1)sin(b2)] 
            double rlon0 = ToRadian(dLon0);
            double rlat0 = ToRadian(dLat0);
            double rlon1 = ToRadian(dLon1);
            double rlat1 = ToRadian(dLat1);
            double A = Math.Cos(rlat0) * Math.Cos(rlat1) * Math.Cos(rlon0 - rlon1);
            double B = Math.Sin(rlat0) * Math.Sin(rlat1);
            double AB = A + B;
            double acosAB = AB >= 1 ? 0 : Math.Acos(AB);
            double val = KPolar.EarthRadius * acosAB;

            dValue = val;

            return dValue;
        }

        /// <summary>
        /// 取得方位角（正北方为0）
        /// </summary>
        /// <param name="dLon"></param>
        /// <param name="dLat"></param>
        /// <returns>返回角度值(DEG)</returns>
        public double GetAzimuth(double dLon, double dLat)
        {
            double dValue = 0;

            if (Double.Equals(dLon, CenterLongitude) && Double.Equals(dLat, CenterLatitude))
            {
                // 经纬度相同，方位角为0
                dValue = 0;
            }
            else if (Double.Equals(dLon, CenterLongitude))
            {
                // 经度相同，在垂直方向
                dValue = dLat > CenterLatitude ? 0.0 : 180.0;
            }
            else if (Double.Equals(dLat, CenterLatitude))
            {
                // 纬度相同，在水平方向
                dValue = dLon > CenterLongitude ? 90.0 : 270.0;
            }
            else
            {
                // 注：由于经向和纬向的球面距离不等(华南，经向>纬向)，
                // 故点(1,1)与中心点(0,0)的极角不等45度，而应是略大于45度
                double xx = Math.Abs(dLon - CenterLongitude) * KmPerLongitude;
                double yy = Math.Abs(dLat - CenterLatitude) * KmPerLatitude;
                dValue = this.ToDegree(Math.Atan2(xx, yy));

            }

            // 根据象限确定方位角
            dValue =
                dLon > CenterLongitude && dLat > CenterLatitude ? dValue :          //第一象限
                dLon < CenterLongitude && dLat > CenterLatitude ? 360.0 - dValue :  //第二象限
                dLon < CenterLongitude && dLat < CenterLatitude ? 180.0 + dValue :  //第三象限
                dLon > CenterLongitude && dLat < CenterLatitude ? 180.0 - dValue :  //第四象限
                dValue;

            return dValue;
        }

        /// <summary>
        /// 取得极半径（km）
        /// </summary>
        /// <param name="dLon"></param>
        /// <param name="dLat"></param>
        /// <returns></returns>
        public double GetRadius(double dLon, double dLat)
        {
            LonLat2XY(dLon, dLat, out int x, out int y);

            double dx = (x - CenterX);
            double dy = (y - CenterY);
            double dValue = Math.Sqrt(dx * dx + dy * dy) / this.PixelPerKm;

            return dValue;
        }


        /// <summary>
        /// 经纬度转换为像素坐标
        /// </summary>
        /// <param name="dLon">经度</param>
        /// <param name="dLat">纬度</param>
        /// <param name="X">X坐标（列号）</param>
        /// <param name="Y">Y坐标（行号）</param>
        public void LonLat2XY(double dLon, double dLat, out int X, out int Y)
        {
            double cosineElevation = Math.Cos(Elevation);

            // 相对于中心经度的纬向（X向）距离，km
            double dx = DistanceOfSphere(dLon, CenterLatitude, CenterLongitude, CenterLatitude) / cosineElevation;

            // 相对于中心纬度的径向（Y向）距离，km
            double dy = DistanceOfSphere(CenterLongitude, dLat, CenterLongitude, CenterLatitude) / cosineElevation;

            X = CenterX + (int)(dx * PixelPerKm) * (dLon > CenterLongitude ? 1 : -1);
            Y = CenterY - (int)(dy * PixelPerKm) * (dLat > CenterLatitude ? 1 : -1);
        }

        /// <summary>
        /// 像素坐标转换为经纬度
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="dLon"></param>
        /// <param name="dLat"></param>
        public void XY2LonLat(int X, int Y, out double dLon, out double dLat)
        {
            int x0 = this.CenterX;
            int y0 = this.CenterY;

            int dx = X - x0;    // X向像素数量
            int dy = Y - y0;    // Y向像素数量

            double dXkm = dx / this.PixelPerKm; // X向公里数
            double dYkm = dy / this.PixelPerKm; // Y向公里数

            double lonOffset = dXkm / this.KmPerLongitude;
            double latOffset = dYkm / this.KmPerLatitude;

            double lon0 = this.CenterLongitude;
            double lat0 = this.CenterLatitude;

            dLon = lon0 + lonOffset;
            dLat = lat0 - latOffset;
        }

        /// <summary>
        /// 取得像素坐标
        /// </summary>
        /// <param name="dRadius">极半径(km)</param>
        /// <param name="dAzimuth">方位角(deg)</param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public void GetXY(double dRadius, double dAzimuth, out int X, out int Y)
        {
            int xtmp = (int)(dRadius * Math.Sin(ToRadian(dAzimuth)));
            int ytmp = (int)(dRadius * Math.Cos(ToRadian(dAzimuth)));

            X = CenterX + xtmp;
            Y = CenterY - ytmp;
        }

        /// <summary>
        /// 半径+方位角转换为格点坐标
        /// </summary>
        /// <param name="dRadius"></param>
        /// <param name="dAzimuth"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public void RA2XY(double dRadius, double dAzimuth, out int X, out int Y)
        {
            GetXY(dRadius, dAzimuth, out X, out Y);
        }

        /// <summary>
        /// 取得经纬度
        /// </summary>
        /// <param name="dRadius">极半径</param>
        /// <param name="dAzimuth">方位角(DEG)</param>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        public void GetLonLat(double dRadius, double dAzimuth, out double lon, out double lat)
        {
            this.GetXY(dRadius, dAzimuth, out int x, out int y);

            // 扫描平面上1经度的对应的像素点数
            double dPixelPerDegreeX = this.PixelPerKm * this.KmPerLongitude;
            lon = this.CenterLongitude + (x - this.CenterX) / dPixelPerDegreeX;

            double earthRadius = KPolar.EarthRadius;
            double cosElevation = Math.Cos(ToRadian(this.Elevation));
            double deltLat = (y - this.CenterY) * cosElevation / this.PixelPerKm / earthRadius;
            double rlat = ToRadian(this.CenterLatitude) - deltLat;
            lat = ToDegree(rlat);
        }
        public void RA2LonLat(double dRadius, double dAzimuth, out double lon, out double lat)
        {
            GetLonLat(dRadius, dAzimuth, out lon, out lat);
        }


        public double ToRadian(double deg)
        {
            return (deg * Math.PI / 180.0);
        }
        public double ToDegree(double rad)
        {
            return (rad * 180.0 / Math.PI);
        }


        #region GridInfo

        public IGridInfo GridInfo => getGridInfo();
        private IGridInfo getGridInfo()
        {
            int skip = 0;

            // 中心经纬度
            double dCenterLon = CenterLongitude;
            double dCenterLat = CenterLatitude;

            // 每公里1.1公里=0.008979经纬度 1.1/111.37
            double dDegreePerKm = 0.009876986621;   // KmPerLatitude

            // 每个像素表示的公里数
            double dKmPerPixel = 1.0 / this.PixelPerKm;

            // 每个象素代表的经纬度
            double dDegreePerPixel = dDegreePerKm * dKmPerPixel;

            // 格点数
            int xnum = (this.CenterX + 1) * 2;
            int ynum = (this.CenterY + 1) * 2;

            //求出左下角经纬度
            double xmin = dCenterLon - dDegreePerPixel * CenterX;
            double ymin = dCenterLat - dDegreePerPixel * CenterY;

            //求出右上角下角经纬度
            double xmax = dCenterLon + dDegreePerPixel * CenterX;
            double ymax = dCenterLat + dDegreePerPixel * CenterY;

            //经纬度间隔
            double xinterval = dDegreePerPixel + dDegreePerPixel * skip;
            double yinterval = dDegreePerPixel + dDegreePerPixel * skip;

            IGridInfo gi1 = new GridInfo(xnum, ynum, xmin, ymin, xinterval, yinterval);

            return gi1;
        }

        #endregion


        public override string ToString()
        {
            return string.Format("CLON = {0,7:F3} CLAT = {1,7:F3} PPKM = {2,3:F1}"
                , CenterLongitude
                , CenterLatitude
                , PixelPerKm);
        }


        //}}@@@
    }


}
