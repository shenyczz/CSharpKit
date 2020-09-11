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
using System.Runtime.InteropServices;
using System.Text;

namespace CSharpKit.Utils
{
    /// <summary>
    /// KConverter - 工具包转换器
    /// </summary>
    public sealed class KConverter
    {
        KConverter() { }
        //public static readonly KConverter Instance = new KConverter();


        #region ArrayConverter

        public static ArrayConverter ArrayConverter => ArrayConverter.Instance;

        #endregion


        #region Data2ImageConverter

        public static Data2ImageConverter Data2ImageConverter => Data2ImageConverter.Instance;

        #endregion

        // StructConverter


        /// <summary>
        /// 缓冲区大小
        /// </summary>
        private const int BUFFER_SIZE = 1024;



        /// <summary>
        /// 把经过的秒数转换位UTC日期
        /// </summary>
        /// <param name="totalSeconds">UTC计数的秒数，从1970-01-01 00:00:00 开始计数</param>
        /// <returns></returns>
        public static DateTime UTCFromSeconds(Int32 totalSeconds)
        {
            return DateTime.Parse("1970-01-01 00:00:00").AddSeconds(totalSeconds);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static Int32 UTCToSeconds(DateTime dateTime)
        {
            TimeSpan timeSpan = DateTime.UtcNow.Subtract(DateTime.Parse("1970-01-01 00:00:00"));
            Int32 seconds = Convert.ToInt32(timeSpan.TotalSeconds);
            return seconds;
        }


        /// <summary>
        /// 字节转换到字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static String Bytes2String(Byte[] bytes)
        {
            var result = string.Empty;

            try
            {
                for (int i = 0; i < bytes?.Length; i++)
                {
                    if (bytes[i] == 0)
                    {
                        for (int pos = i; pos < bytes.Length; pos++)
                        {
                            bytes[pos] = 0;
                        }
                        break;
                    }
                }

#if NETCORE
                // todo: NetCore 出错
                // 'GB18030' is not a supported encoding name.
                // For information on defining a custom encoding, 
                // see the documentation for the Encoding.RegisterProvider method. (Parameter 'name')
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
#endif
                result = Encoding.GetEncoding("GB18030").GetString(bytes).Trim(new char[] { '\0' });
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
            }


            return result;
        }

        /// <summary>
        /// 字符串转换到字节
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Byte[] String2Bytes(String str)
        {
#if NETCORE
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
#endif
            return Encoding.GetEncoding("GB18030").GetBytes(str);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rawdatas"></param>
        /// <returns></returns>
        public static T Bytes2Struct<T>(byte[] rawdatas)
        {
            Type anytype = typeof(T);
            int rawsize = Marshal.SizeOf(anytype);
            if (rawsize > rawdatas.Length)
                return default(T);

            IntPtr buffer = Marshal.AllocHGlobal(rawsize);
            Marshal.Copy(rawdatas, 0, buffer, rawsize);
            object retobj = Marshal.PtrToStructure(buffer, anytype);
            Marshal.FreeHGlobal(buffer);

            return (T)retobj;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] Struct2Bytes(Object obj)
        {
            int rawsize = Marshal.SizeOf(obj);
            IntPtr buffer = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(obj, buffer, false);
            byte[] rawdatas = new byte[rawsize];
            Marshal.Copy(buffer, rawdatas, 0, rawsize);
            Marshal.FreeHGlobal(buffer);
            return rawdatas;
        }


        /// <summary>
        /// 对象转换到指针
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IntPtr ToIntPtr(Object obj)
        {
            IntPtr pObject = IntPtr.Zero;

            try
            {
                GCHandle hObject = GCHandle.Alloc(obj, GCHandleType.Pinned);
                pObject = hObject.AddrOfPinnedObject();
                if (hObject.IsAllocated) hObject.Free();
            }
            catch (Exception)
            {
                // no body
            }

            return pObject;
        }

        public static T ToStruct<T>(Object obj)
        {
            return (T)Marshal.PtrToStructure(ToIntPtr(obj), typeof(T));
        }

        public static T IntPtr2Struct<T>(IntPtr ptr)
        {
            return (T)Marshal.PtrToStructure(ptr, typeof(T));
        }





        /// <summary>
        /// 指针转换到字符串
        /// </summary>
        /// <param name="p"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string IntPtr2String(IntPtr p, int len = 0)
        {
            string str_value = "";

            try
            {
                int actual_len = len == 0 ? BUFFER_SIZE : len;
                byte[] uc_temp = new byte[actual_len];
                Marshal.Copy(p, uc_temp, 0, actual_len);
                str_value = Bytes2String(uc_temp);
            }
            catch (Exception)
            {
                str_value = "";
            }

            return str_value;
        }






        /*

        /// <summary>
        /// 指针转换到字节
        /// </summary>
        /// <param name="p"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static Byte[] IntPtr2Bytes(IntPtr p, int len = 0)
        {
            Byte[] uc_value = null;

            try
            {
                int actual_len = (len == 0) ? BUFFER_SIZE : len;
                uc_value = new byte[actual_len];
                Marshal.Copy(p, uc_value, 0, actual_len);
            }
            catch (Exception)
            {
                uc_value = new byte[0];
            }

            return uc_value;
        }
        public static Int16[] Bytes2ArrayInt16(Byte[] bytes)
        {
            Int16[] result = null;

            try
            {
                GCHandle hObject = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                IntPtr pObject = hObject.AddrOfPinnedObject();
                if (hObject.IsAllocated) hObject.Free();

                int size = Marshal.SizeOf(typeof(Int16));
                int len = bytes.Length / size;

                result = new Int16[len];
                Marshal.Copy(pObject, result, 0, len);
                pObject = IntPtr.Zero;
            }
            catch (Exception)
            {
            }

            return result;
        }
        public static Int32[] Bytes2ArrayInt32(Byte[] bytes)
        {
            Int32[] result = null;

            try
            {
                GCHandle hObject = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                IntPtr pObject = hObject.AddrOfPinnedObject();
                if (hObject.IsAllocated) hObject.Free();

                int size = Marshal.SizeOf(typeof(Int32));
                int len = bytes.Length / size;

                result = new Int32[len];
                Marshal.Copy(pObject, result, 0, len);
                pObject = IntPtr.Zero;
            }
            catch (Exception)
            {
            }

            return result;
        }
        public static Single[] Bytes2ArrayFloat(Byte[] bytes)
        {
            float[] result = null;

            try
            {
                GCHandle hObject = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                IntPtr pObject = hObject.AddrOfPinnedObject();
                if (hObject.IsAllocated) hObject.Free();

                int size = Marshal.SizeOf(typeof(float));
                int len = bytes.Length / size;

                result = new float[len];
                Marshal.Copy(pObject, result, 0, len);
                pObject = IntPtr.Zero;
            }
            catch (Exception)
            {
            }

            return result;
        }
        public static Single[] Bytes2ArraySingle(Byte[] bytes)
        {
            Single[] result = null;

            try
            {
                GCHandle hObject = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                IntPtr pObject = hObject.AddrOfPinnedObject();
                if (hObject.IsAllocated) hObject.Free();

                int size = Marshal.SizeOf(typeof(Single));
                int len = bytes.Length / size;

                result = new Single[len];
                Marshal.Copy(pObject, result, 0, len);
                pObject = IntPtr.Zero;
            }
            catch (Exception)
            {
            }

            return result;
        }
        public static Double[] Bytes2ArrayDouble(Byte[] bytes)
        {
            Double[] result = null;

            try
            {
                GCHandle hObject = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                IntPtr pObject = hObject.AddrOfPinnedObject();
                if (hObject.IsAllocated) hObject.Free();

                int size = Marshal.SizeOf(typeof(Double));
                int len = bytes.Length / size;

                result = new Double[len];
                Marshal.Copy(pObject, result, 0, len);
                pObject = IntPtr.Zero;
            }
            catch (Exception)
            {
            }

            return result;
        }

        public static T[] Bytes2Array<T>(Byte[] bytes)
        {
            Array result = null;

            try
            {
                GCHandle hObject = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                IntPtr pObject = hObject.AddrOfPinnedObject();
                if (hObject.IsAllocated)
                    hObject.Free();

                Type type = typeof(T);
                int size = Marshal.SizeOf(type);
                int len = bytes.Length / size;

                switch (type.Name)
                {
                    case "Int16":
                    case "UInt16":
                        result = new Int16[len];
                        Marshal.Copy(pObject, (Int16[])result, 0, len);
                        break;

                    case "Int32":
                    case "UInt32":
                        result = new Int32[len];
                        Marshal.Copy(pObject, (Int32[])result, 0, len);
                        break;

                    case "Single":
                        result = new Single[len];
                        Marshal.Copy(pObject, (Single[])result, 0, len);
                        break;

                    case "Double":
                        result = new Double[len];
                        Marshal.Copy(pObject, (Double[])result, 0, len);
                        break;
                }

                // 垃圾搜集
                pObject = IntPtr.Zero;
            }
            catch (Exception)
            {
            }

            return (T[])result;
        }
        */





        /*
         
        // 儒略日= K - 32075 + 1461 * (I + 4800 + (J-14)/12)/4+367*(J-2-(J-14)/12*12)/12-3*((I+4900+(J-14)/12)/100)/4

        void DateToJulian(unsigned short year, unsigned char month, unsigned char day, unsigned short& Julian)
        {

    struct tm cuttm;
	time_t ct, nt;
        cuttm.tm_year = year - 1900;
	cuttm.tm_mon = month - 1;
	cuttm.tm_mday = day;
	cuttm.tm_hour = 8;
	cuttm.tm_min = 0;
	cuttm.tm_sec = 0;

	ct = mktime(&cuttm);

        Julian = 1 + ct / 86400;
	return; //这一句应该不会执行到
}

  struct tm {
        int tm_sec;     //* seconds after the minute - [0,59] 
        int tm_min;     //* minutes after the hour - [0,59] 
        int tm_hour;    //* hours since midnight - [0,23] 
        int tm_mday;    //* day of the month - [1,31] 
        int tm_mon;     //* months since January - [0,11] 
        int tm_year;    //* years since 1900 
        int tm_wday;    //* days since Sunday - [0,6] 
        int tm_yday;    //* days since January 1 - [0,365] 
        int tm_isdst;   //* daylight savings time flag 
    };

void CCoreClass::GetSeconds(int& outseconds, unsigned char hour, unsigned char mins, unsigned char seconds)
{
	outseconds = hour * 3600 + mins * 60 + seconds;
	outseconds = outseconds * 1000;
	return;
}

         */

        //}}@@@
    }




}

