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


namespace CSharpKit.Maths
{
    /// <summary>
    /// WGS-84，GCJ-02，BD-09
    /// </summary>
    public class KGps
    {
        public KGps()
        {
        }

        /// <summary>
        /// 经度
        /// </summary>
        public double Lon { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Lat { get; set; }


        //@@@
    }

    public enum GpsType
    {
        Wgs84 = 0,
        Gcj02 = 1,
        Bd09 = 2
    };


    /*







/// <summary>

/// GPSConvert 的摘要说明

/// </summary>

public class GPSConvert : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {



        //DFMGps g = new DFMGps() { lng = "104.03.40.95", lat = "30.39.46.04" };

        //Gps g = new Gps() { lng = 104.0613755710, lat = 30.6627876858 };//谷歌地球

        //Gps g = new Gps() { lng = 104.0638732910, lat = 30.660359565 };//腾讯高德

        //Gps g = new Gps() { lng = 104.0703438917, lat = 30.6664848935 };//百度

        Gps g = new Gps();

        GpsConvert gs;

        int _gt = 0;

        //0:谷歌地球,1:腾讯高德,2:百度

        if (int.TryParse(context.Request["t"], out _gt))

            ;

        try

        {

            string[] _g = context.Request["g"].Split(',');              

            if (double.TryParse(_g[0], out g.lng) && double.TryParse(_g[1], out g.lat))

                gs = new GpsConvert(g, (GpsType)_gt);

            else

                gs = new GpsConvert(new DFMGps() { lng = _g[0], lat = _g[1] });

            context.Response.ContentType = "text/plain";

            context.Response.Write(JsonConvert.SerializeObject(gs));

            context.Response.End();

        }

        catch (Exception)

        {



            throw new Exception("invaild parameter");

        }





    }



    public bool IsReusable

    {

        get

        {

            return false;

        }

    }







    //度分秒格式坐标

    public class DFMGps
    {
        public string lng;//经度
        public string lat;//纬度
    }


    public class GpsConvert

    {

        public DFMGps _dfm = new DFMGps(); //度分秒坐标 

        public Gps Wgs84 = new Gps(); //WGS-84 

        public Gps Gcj02 = new Gps();//GCJ-02 中国坐标偏移标准 Google Map、高德、腾讯使用

        public Gps Bd09 = new Gps();//BD-09 百度坐标偏移标准，Baidu Map使用  

        private double PI = Math.PI;

        private double xPI = Math.PI * 3000.0 / 180.0;







        public GpsConvert(DFMGps _v)

        {

            _dfm = _v;

            DfmToWgs84();

            Wgs84ToGcj02();

            Gcj02ToBd09();

        }

        public GpsConvert(Gps _v, GpsType type)

        {

            if (type == GpsType.Wgs84)

            {

                Wgs84 = _v;

                Wgs84ToDfm();

                Wgs84ToGcj02();

                Gcj02ToBd09();

            }

            else if (type == GpsType.Gcj02)

            {

                Gcj02 = _v;

                Gcj02ToWgs84();

                Wgs84ToDfm();

                Gcj02ToBd09();

            }

            else if (type == GpsType.Bd09)

            {

                Bd09 = _v;

                Bd09ToGcj02();

                Gcj02ToWgs84();

                Wgs84ToDfm();

            }

        }



        private Gps delta(Gps t)

        {

            var a = 6378245.0; //  a: 卫星椭球坐标投影到平面地图坐标系的投影因子。

            var ee = 0.00669342162296594323; //  ee: 椭球的偏心率。

            var dLat = this.transformLat(t.lng - 105.0, t.lat - 35.0);

            var dLng = this.transformLng(t.lng - 105.0, t.lat - 35.0);

            var radLat = t.lat / 180.0 * PI;

            var magic = Math.Sin(radLat);

            magic = 1 - ee * magic * magic;

            var sqrtMagic = Math.Sqrt(magic);

            return new Gps() { lat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * PI), lng = (dLng * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * PI) };

        }





        //WGS-84 to GCJ-02

        private void Wgs84ToGcj02()

        {

            if (Wgs84 == null || this.outOfChina(Wgs84))

                Gcj02 = Wgs84;

            var t = this.delta(Wgs84);

            Gcj02.lng = t.lng + Wgs84.lng;

            Gcj02.lat = t.lat + Wgs84.lat;

        }



        //GCJ-02 to WGS-84

        private void Gcj02ToWgs84()

        {

            if (Gcj02 == null || this.outOfChina(Gcj02))

                Wgs84 = Gcj02;

            var t = this.delta(Gcj02);

            Wgs84.lng = Gcj02.lng - t.lng;

            Wgs84.lat = Gcj02.lat - t.lat;

        }



        //GCJ-02 to BD-09

        private void Gcj02ToBd09()

        {

            double x = Gcj02.lng;

            double y = Gcj02.lat;

            double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * xPI);

            double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * xPI);

            Bd09.lng = z * Math.Cos(theta) + 0.0065;

            Bd09.lat = z * Math.Sin(theta) + 0.006;

        }

        //BD-09 to GCJ-02

        private void Bd09ToGcj02()

        {

            double x = Bd09.lng - 0.0065;

            double y = Bd09.lat - 0.006;

            double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * xPI);

            double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * xPI);

            Gcj02.lng = z * Math.Cos(theta);

            Gcj02.lat = z * Math.Sin(theta);

        }



        //WGS-84 to 度分秒坐标  

        private void Wgs84ToDfm()

        {

            _dfm.lng = TranDegreeToDMs(Wgs84.lng);

            _dfm.lat = TranDegreeToDMs(Wgs84.lat);

        }





        //度分秒坐标 to WGS-84

        private void DfmToWgs84()

        {

            Wgs84.lng = TranDMsToDegree(_dfm.lng);

            Wgs84.lat = TranDMsToDegree(_dfm.lat);

        }





        private double TranDMsToDegree(string _dms)

        {

            string[] dms = _dms.Split('.');

            if (dms.Length == 4)

                return double.Parse(dms[0]) + double.Parse(dms[1]) / 60 + double.Parse(dms[2] + "." + dms[3] ?? "0") / 3600;

            else if (dms.Length == 3)

                return double.Parse(dms[0]) + double.Parse(dms[1]) / 60 + double.Parse(dms[2]) / 3600;

            else if (dms.Length == 2)

                return double.Parse(_dms);

            else

                return 0d;



        }





        private static string TranDegreeToDMs(double d)

        {

            int Degree = Convert.ToInt16(Math.Truncate(d));//度

            d = d - Degree;

            int M = Convert.ToInt16(Math.Truncate((d) * 60));//分

            int S = Convert.ToInt16(Math.Round((d * 60 - M) * 60));

            if (S == 60)

            {

                M = M + 1;

                S = 0;

            }

            if (M == 60)

            {

                M = 0;

                Degree = Degree + 1;

            }

            string rstr = Degree.ToString() + ".";

            if (M < 10)

                rstr = rstr + "0" + M.ToString();

            else

                rstr = rstr + M.ToString();

            if (S < 10)

                rstr = rstr + "0" + S.ToString();

            else

                rstr = rstr + S.ToString();

            return rstr;

        }



        private bool outOfChina(Gps _t)

        {

            if (_t.lng < 72.004 || _t.lng > 137.8347)

                return true;

            if (_t.lat < 0.8293 || _t.lat > 55.8271)

                return true;

            return false;

        }



        private double transformLat(double x, double y)

        {

            double ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * Math.Sqrt(Math.Abs(x));

            ret += (20.0 * Math.Sin(6.0 * x * PI) + 20.0 * Math.Sin(2.0 * x * PI)) * 2.0 / 3.0;

            ret += (20.0 * Math.Sin(y * PI) + 40.0 * Math.Sin(y / 3.0 * PI)) * 2.0 / 3.0;

            ret += (160.0 * Math.Sin(y / 12.0 * PI) + 320 * Math.Sin(y * PI / 30.0)) * 2.0 / 3.0;

            return ret;

        }



        private double transformLng(double x, double y)

        {

            double ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * Math.Sqrt(Math.Abs(x));

            ret += (20.0 * Math.Sin(6.0 * x * PI) + 20.0 * Math.Sin(2.0 * x * PI)) * 2.0 / 3.0;

            ret += (20.0 * Math.Sin(x * PI) + 40.0 * Math.Sin(x / 3.0 * PI)) * 2.0 / 3.0;

            ret += (150.0 * Math.Sin(x / 12.0 * PI) + 300.0 * Math.Sin(x / 30.0 * PI)) * 2.0 / 3.0;

            return ret;

        }

    }

}


 * */

    /*
    public class TempGps
    {
        public double Tlng;
        public double Tlat;
    }

    public class GPS
    {
        public string oLng;//经度 度分秒坐标
        public string oLat;//纬度 度分秒坐标

        public double lng;//经度 WGS-84
        public double lat;//纬度 WGS-84

        public double gLng;//经度 GCJ-02 中国坐标偏移标准 Google Map、高德、腾讯使用
        public double gLat;//纬度 GCJ-02 中国坐标偏移标准 Google Map、高德、腾讯使用

        public double bLng;//经度 BD-09 百度坐标偏移标准，Baidu Map使用
        public double bLat;//纬度 BD-09 百度坐标偏移标准，Baidu Map使用


        public double PI = Math.PI;
        double xPI = Math.PI * 3000.0 / 180.0;

        public TempGps delta(TempGps t)
        {
            var a = 6378245.0; //  a: 卫星椭球坐标投影到平面地图坐标系的投影因子。
            var ee = 0.00669342162296594323; //  ee: 椭球的偏心率。
            var dLat = this.transformLat(t.Tlng - 105.0, t.Tlat - 35.0);
            var dLng = this.transformLng(t.Tlng - 105.0, t.Tlat - 35.0);
            var radLat = t.Tlat / 180.0 * PI;
            var magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            var sqrtMagic = Math.Sqrt(magic);
            return new TempGps() { Tlat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * PI),  Tlng= (dLng * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * PI) };
        }
        //WGS-84 to GCJ-02
        public void gcj_encrypt()
        {
            if (this.outOfChina(lng, lat))
            {
                gLng = lng;
                gLat = lat;
            }
            var t = this.delta(new TempGps() { Tlng = lng, Tlat = lat });
            gLng = t.Tlng+lng;
            gLat = t.Tlat+lat;
        }

        //GCJ-02 to WGS-84
        public void gcj_decrypt()
        {


            if (this.outOfChina(gLng, gLat))
            {
                lng = gLng;
                lat = gLat;

            }
            var t = this.delta(new TempGps() { Tlng = gLng, Tlat = gLat });
            lng = gLng-t.Tlng;
            lat = gLat-t.Tlat;
        }

        //GCJ-02 to BD-09
        public void bd_encrypt()
        {
            double x = gLng;
            double y = gLat;
            double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * xPI);
            double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * xPI);
            bLng = z * Math.Cos(theta) + 0.0065;
            bLat = z * Math.Sin(theta) + 0.006;
        }
        //BD-09 to GCJ-02
        public void bd_decrypt()
        {
            double x = bLng - 0.0065;
            double y = bLat - 0.006;
            double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * xPI);
            double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * xPI);
            gLng = z * Math.Cos(theta);
            gLat = z * Math.Sin(theta);
        }

        //WGS-84 to 度分秒坐标  
        public void wgs_decrypt()
        {
            oLng = TranDegreeToDMs(lng);
            oLat = TranDegreeToDMs(lat);
        }


        //度分秒坐标 to WGS-84
        public void wgs_encrypt()
        {
            lng = TranDMsToDegree(oLng);
            lat = TranDMsToDegree(oLat);
        }


        public double TranDMsToDegree(string _dms)
        {
            string[] dms = _dms.Split('.');
            if (dms.Length > 2)
                return double.Parse(dms[0]) + double.Parse(dms[1]) / 60 + double.Parse(dms[2] + "." + dms[3] ?? "0") / 3600;
            else
                return 0d;

        }


        private static string TranDegreeToDMs(double d)
        {
            int Degree = Convert.ToInt16(Math.Truncate(d));//度
            d = d - Degree;
            int M = Convert.ToInt16(Math.Truncate((d) * 60));//分
            int S = Convert.ToInt16(Math.Round((d * 60 - M) * 60));
            if (S == 60)
            {
                M = M + 1;
                S = 0;
            }
            if (M == 60)
            {
                M = 0;
                Degree = Degree + 1;
            }
            string rstr = Degree.ToString() + ".";
            if (M < 10)
                rstr = rstr + "0" + M.ToString();
            else
                rstr = rstr + M.ToString();
            if (S < 10)
                rstr = rstr + "0" + S.ToString();
            else
                rstr = rstr + S.ToString();
            return rstr;
        }

        private bool outOfChina(double _lng, double _lat)
        {
            if (lng < 72.004 || lng > 137.8347)
                return true;
            if (lat < 0.8293 || lat > 55.8271)
                return true;
            return false;
        }

        private double transformLat(double x, double y)
        {
            double ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * PI) + 20.0 * Math.Sin(2.0 * x * PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(y * PI) + 40.0 * Math.Sin(y / 3.0 * PI)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(y / 12.0 * PI) + 320 * Math.Sin(y * PI / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        private double transformLng(double x, double y)
        {
            double ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * PI) + 20.0 * Math.Sin(2.0 * x * PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(x * PI) + 40.0 * Math.Sin(x / 3.0 * PI)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(x / 12.0 * PI) + 300.0 * Math.Sin(x / 30.0 * PI)) * 2.0 / 3.0;
            return ret;
        }
    }  
    
                    GPS t = new GPS();
                    t.oLng = dt.Rows[i][1].ToString();
                    t.oLat = dt.Rows[i][2].ToString();
                    t.wgs_encrypt();
                    t.gcj_encrypt();
                    t.bd_encrypt();
                    cells[i+1, 3].PutValue(t.bLng);
                    cells[i+1, 4].PutValue(t.bLat);   
     */






}
