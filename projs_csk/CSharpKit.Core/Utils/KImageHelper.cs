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
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CSharpKit.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class KImageHelper
    {

        
        /// <summary>
        /// 数据转换为图像
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="datas">二维数组</param>
        /// <param name="predicater">对输入数据rawsdata格点值的有效值检测器</param>
        /// <param name="tranformer">数据颜色转换器</param>
        /// <param name="ymirror">关于Y轴镜像</param>
        /// <returns></returns>
        public static Image Data2Image24<T>(T[,] datas,
            Predicate<double> predicater,
            IColorTranformer<double> tranformer,
            bool ymirror = false)
        {
            int height = datas.GetLength(0);
            int width = datas.GetLength(1);

            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            {
                int stride = bmpData.Stride;            // 扫描宽度
                IntPtr scan0 = bmpData.Scan0;           // 位图的第一个扫描行(bmpData的内存起始位置)

                int offset = stride - width * 3;        // 显示宽度与扫描线宽度的间隙  

                int scanBytes = stride * height;        // 用stride宽度，表示这是内存区域的大小  
                byte[] pixels = new byte[scanBytes];    // 像素数量

                int posScan = 0;                        // 扫描指针

                for (int i = 0; i < height; i++)
                {
                    int rw = (!ymirror) ? (i) : ((height - 1) - i);

                    for (int j = 0; j < width; j++)
                    {
                        int cl = j;

                        T fv = datas[rw, cl];
                        var dv = (double)Convert.ChangeType(fv, TypeCode.Double);

                        int clr = 0;    // 可以设置背景颜色
                        if (predicater(dv))
                        {
                            clr = tranformer.GetColor(dv);
                        }

                        Color color = Color.FromArgb(clr);
                        pixels[posScan++] = color.B;
                        pixels[posScan++] = color.G;
                        pixels[posScan++] = color.R;
                    }

                    posScan += offset;
                }

                // 从 pixels 复制到 scan0
                Marshal.Copy(pixels, 0, scan0, scanBytes);
            }
            bitmap.UnlockBits(bmpData);

            #region --unsafe code demo

            /*
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                for (i = 0; i < iHeight; i++)
                {
                    for (j = 0; j < iWidth; j++)
                    {
                        UInt16 wRData = rsData[rDataIndex, i, j];
                        UInt16 wGData = rsData[gDataIndex, i, j];
                        UInt16 wBData = rsData[bDataIndex, i, j];

                        byte byRGray = rsDataGray[rDataIndex, wRData];
                        byte byGGray = rsDataGray[gDataIndex, wGData];
                        byte byBGray = rsDataGray[bDataIndex, wBData];

                        //升降轨标记, 1: 升轨, 0: 降轨
                        int ii = i;
                        //int ii = (iHeight - 1 - i);

                        p[ii * stride + j * 3 + 0] = byBGray; // B
                        p[ii * stride + j * 3 + 1] = byGGray;  // G
                        p[ii * stride + j * 3 + 2] = byRGray;  // R
                    }
                }
            }//unsafe
            */

            #endregion

            return bitmap;
        }



        /// <summary>
        /// 像素位数（8、24、32）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <param name="ePixelBits">像素位数（8、24、32）</param>
        /// <param name="ymirror"></param>
        /// <returns></returns>
        public static Image Data2ImageGray<T>(T[,] datas, PixelBits ePixelBits, bool ymirror)
        {
            Image result = null;

            try
            {
                var vtemp = result;

                var ts = (ymirror) ? Mirrory(datas) : datas;
                var x = ConvertArray2D1D(ts);
                var bytes = x.raws;
                var width = x.width;
                var height = x.height;

                switch (ePixelBits)
                {
                    case PixelBits.PB08:
                        vtemp = Data2ImageGray08(bytes, width, height);
                        break;

                    case PixelBits.PB24:
                        vtemp = Data2ImageGray24(bytes, width, height);
                        break;

                    case PixelBits.PB32:
                        vtemp = Data2ImageGray32(bytes, width, height);
                        break;

                    default:
                        vtemp = Data2ImageGray08(bytes, width, height);
                        break;
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


        public static Image Data2ImageGray<T>(T[,] datas, GammaCorrect gac, PixelBits ePixelBits, bool ymirror)
        {
            Image resoult = null;

            try
            {
                var vtemp = resoult;
                {
                    int rows = datas.GetLength(0);
                    int cols = datas.GetLength(1);

                    var bys = new T[rows, cols];
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            var dv = (double)Convert.ChangeType(datas[i, j], TypeCode.Double);
                            bys[i, j] = (T)Convert.ChangeType(gac.ToGary(dv), typeof(T));
                        }
                    }

                    vtemp = Data2ImageGray(bys, ePixelBits, ymirror);
                }
                resoult = vtemp;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }

            return resoult;
        }





        #region Data2ImageColor

        public static Image Data2ImageColor08<T>(T[,] datas, int[] colorTable)
        {
            Image resoult = null;

            try
            {
                var vtemp = resoult;
                {
                    var x = ConvertArray2D1D(datas);
                    vtemp = Data2ImageGray08(x.raws, x.width, x.height);
                    SetPalette(vtemp, colorTable);
                }
                resoult = vtemp;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }

            return resoult;
        }
        public static Image Data2ImageColor24<T>(T[,] RDatas, T[,] GDatas, T[,] BDatas)
        {
            Image resoult = null;

            try
            {
                int rh = RDatas.GetLength(0);
                int rw = RDatas.GetLength(1);

                int gh = GDatas.GetLength(0);
                int gw = GDatas.GetLength(1);

                int bh = BDatas.GetLength(0);
                int bw = BDatas.GetLength(1);

                bool okw = rw == gw && rw == bw;
                bool okh = rh == gh && rh == bh;

                if (!(okw && okh))
                    return null;

                int height = rh;
                int width = rw;


                PixelFormat pixelFormat = PixelFormat.Format24bppRgb;
                Bitmap bitmap = new Bitmap(width, height, pixelFormat);
                BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadWrite, pixelFormat);
                {
                    int stride = bmpData.Stride;            // 扫描宽度
                    IntPtr scan0 = bmpData.Scan0;           // 位图的第一个扫描行(bmpData的内存起始位置)

                    int offset = stride - width * 3;        // 显示宽度与扫描线宽度的间隙  

                    int scanBytes = stride * height;        // 用stride宽度，表示这是内存区域的大小  
                    byte[] pixels = new byte[scanBytes];    // 像素数量

                    int posScan = 0;                        // 扫描指针

                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            var B = (byte)Convert.ChangeType(BDatas[i, j], TypeCode.Byte);
                            var G = (byte)Convert.ChangeType(GDatas[i, j], TypeCode.Byte);
                            var R = (byte)Convert.ChangeType(RDatas[i, j], TypeCode.Byte);

                            pixels[posScan++] = B;
                            pixels[posScan++] = G;
                            pixels[posScan++] = R;
                        }

                        posScan += offset;
                    }

                    // 从 pixels 复制到 scan0
                    Marshal.Copy(pixels, 0, scan0, scanBytes);
                }
                bitmap.UnlockBits(bmpData);

                resoult = bitmap;
            }
            catch (Exception)
            {
            }

            return resoult;
            #region --unsafe code demo

            /*
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                for (i = 0; i < iHeight; i++)
                {
                    for (j = 0; j < iWidth; j++)
                    {
                        UInt16 wRData = rsData[rDataIndex, i, j];
                        UInt16 wGData = rsData[gDataIndex, i, j];
                        UInt16 wBData = rsData[bDataIndex, i, j];

                        byte byRGray = rsDataGray[rDataIndex, wRData];
                        byte byGGray = rsDataGray[gDataIndex, wGData];
                        byte byBGray = rsDataGray[bDataIndex, wBData];

                        //升降轨标记, 1: 升轨, 0: 降轨
                        int ii = i;
                        //int ii = (iHeight - 1 - i);

                        p[ii * stride + j * 3 + 0] = byBGray; // B
                        p[ii * stride + j * 3 + 1] = byGGray;  // G
                        p[ii * stride + j * 3 + 2] = byRGray;  // R
                    }
                }
            }//unsafe
            */

            #endregion
        }
        public static Image Data2ImageColor32<T>(T[,] RDatas, T[,] GDatas, T[,] BDatas, byte opacity = 0xFF)
        {
            Image resoult = null;

            try
            {
                int rh = RDatas.GetLength(0);
                int rw = RDatas.GetLength(1);

                int gh = GDatas.GetLength(0);
                int gw = GDatas.GetLength(1);

                int bh = BDatas.GetLength(0);
                int bw = BDatas.GetLength(1);

                bool okw = rw == gw && rw == bw;
                bool okh = rh == gh && rh == bh;

                if (!(okw && okh))
                    return null;

                int height = rh;
                int width = rw;

                PixelFormat pixelFormat = PixelFormat.Format32bppArgb;
                Bitmap bitmap = new Bitmap(width, height, pixelFormat);
                BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadWrite, pixelFormat);
                {
                    int stride = bmpData.Stride;            // 扫描宽度
                    IntPtr scan0 = bmpData.Scan0;           // 位图的第一个扫描行(bmpData的内存起始位置)

                    int offset = stride - width * 4;        // 显示宽度与扫描线宽度的间隙  

                    int scanBytes = stride * height;        // 用stride宽度，表示这是内存区域的大小  
                    byte[] pixels = new byte[scanBytes];    // 像素数量

                    int posScan = 0;                        // 扫描指针

                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            var B = (byte)Convert.ChangeType(BDatas[i, j], TypeCode.Byte);
                            var G = (byte)Convert.ChangeType(GDatas[i, j], TypeCode.Byte);
                            var R = (byte)Convert.ChangeType(RDatas[i, j], TypeCode.Byte);
                            var A = opacity;

                            pixels[posScan++] = B;
                            pixels[posScan++] = G;
                            pixels[posScan++] = R;
                            pixels[posScan++] = A;
                        }

                        posScan += offset;
                    }

                    // 从 pixels 复制到 scan0
                    Marshal.Copy(pixels, 0, scan0, scanBytes);
                }
                bitmap.UnlockBits(bmpData);

                resoult = bitmap;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }

            return resoult;
        }

        /// <summary>
        /// 针对256色位图设置调色板
        /// </summary>
        /// <param name="image"></param>
        /// <param name="colorTable"></param>
        /// <returns></returns>
        public static bool SetPalette(Image image, Int32[] colorTable)
        {
            if (colorTable == null)
                return false;

            Bitmap bmp = image as Bitmap;
            if (bmp == null)
                return false;

            if (bmp.PixelFormat != PixelFormat.Format8bppIndexed)
                return false;

            int len = colorTable.GetLength(0);
            len = len > 255 ? 255 : len;

            ColorPalette palette = bmp.Palette;
            for (int i = 0; i < len; i++)
            {
                palette.Entries[i] = Color.FromArgb(colorTable[i]);
            }
            bmp.Palette = palette;

            return true;
        }

        #endregion

        #region Data2ImageGray

        public static Image Data2ImageGray08<T>(T[] raws, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            {
                int stride = bmpData.Stride;        // 扫描宽度
                IntPtr scan0 = bmpData.Scan0;       // 位图的第一个扫描行(bmpData的内存起始位置)

                int offset = stride - width;    // 显示宽度与扫描线宽度的间隙  

                int scanBytes = stride * height;        // 用stride宽度，表示这是内存区域的大小  
                byte[] pixels = new byte[scanBytes];

                // 分别设置两个位置指针，指向源数组和目标数组  
                int posScan = 0;
                int posReal = 0;

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        pixels[posScan++] = (byte)Convert.ChangeType(raws[posReal++], TypeCode.Byte);
                    }

                    posScan += offset;
                }

                Marshal.Copy(pixels, 0, scan0, scanBytes);
            }
            bitmap.UnlockBits(bmpData);

            ColorPalette palette = bitmap.Palette;
            for (int i = 0; i < 256; i++)
            {
                palette.Entries[i] = Color.FromArgb(i, i, i);
            }
            bitmap.Palette = palette;

            return bitmap;
        }
        public static Image Data2ImageGray24<T>(T[] raws, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            {
                int stride = bmpData.Stride;        // 扫描宽度
                IntPtr scan0 = bmpData.Scan0;       // 位图的第一个扫描行(bmpData的内存起始位置)

                int offset = stride - width * 3;    // 显示宽度与扫描线宽度的间隙  

                int scanBytes = stride * height;        // 用stride宽度，表示这是内存区域的大小  
                byte[] pixels = new byte[scanBytes];

                int num = width * height;
                int posScan = 0;
                int posReal = 0;// 分别设置两个位置指针，指向源数组和目标数组  

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        var by = (byte)Convert.ChangeType(raws[posReal++], TypeCode.Byte);

                        var B = by;
                        var G = by;
                        var R = by;

                        pixels[posScan++] = B;
                        pixels[posScan++] = G;
                        pixels[posScan++] = R;
                    }

                    posScan += offset;
                }

                Marshal.Copy(pixels, 0, scan0, scanBytes);
            }
            bitmap.UnlockBits(bmpData);

            return bitmap;
        }
        public static Image Data2ImageGray32<T>(T[] raws, int width, int height, double opacity = 1)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            {
                int stride = bmpData.Stride;        // 扫描宽度
                IntPtr scan0 = bmpData.Scan0;       // 位图的第一个扫描行(bmpData的内存起始位置)

                int offset = stride - width * 4;    // 显示宽度与扫描线宽度的间隙  

                int scanBytes = stride * height;        // 用stride宽度，表示这是内存区域的大小  
                byte[] pixels = new byte[scanBytes];

                // 分别设置两个位置指针，指向源数组和目标数组  
                int posScan = 0;
                int posReal = 0;

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        byte by = (byte)Convert.ChangeType(raws[posReal++], TypeCode.Byte);

                        var B = by;
                        var G = by;
                        var R = by;
                        var A = (byte)(0xFF * opacity);

                        pixels[posScan++] = B;
                        pixels[posScan++] = G;
                        pixels[posScan++] = R;
                        pixels[posScan++] = A;
                    }

                    posScan += offset;
                }

                Marshal.Copy(pixels, 0, scan0, scanBytes);
            }
            bitmap.UnlockBits(bmpData);

            return bitmap;
        }

        #endregion




        /// <summary>
        /// 镜像数据 - Y向
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <returns></returns>
        private static T[,] Mirrory<T>(T[,] datas)
        {
            T[,] result = null;

            try
            {
                var vtemp = result;
                {
                    int rows = datas.GetLength(0);
                    int cols = datas.GetLength(1);
                    vtemp = new T[rows, cols];
                    for (int i = 0; i < rows; i++)
                    {
                        int ii = (rows - 1) - i;
                        for (int j = 0; j < cols; j++)
                        {
                            vtemp[ii, j] = datas[i, j];
                        }
                    }
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

        private static (T[] raws, int width, int height) ConvertArray2D1D<T>(T[,] datas)
        {
            T[] result = null; int w = 0; int h = 0;

            try
            {
                int rows = datas.GetLength(0);
                int cols = datas.GetLength(1);
                var vtemp = new T[cols* rows];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        vtemp[i * cols + j] = datas[i, j];
                    }
                }

                result = vtemp;
                w = cols;
                h = rows;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif          
            }

            return (result, w, h);
        }





        [Obsolete("", true)]
        private static T[,] ConvertArray1D2D<T>(T[] raws, int width, int height)
        {
            T[,] result = null;

            try
            {
                int rows = height;
                int cols = width;
                var vtemp = new T[rows, cols];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        vtemp[i, j] = raws[i * cols + j];
                    }
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

        [Obsolete("",true)]
        public static Image Data2Image24_bak<T>(T[,] RDatas, T[,] GDatas, T[,] BDatas, bool ymirror = false)
        {
            Image resoult = null;

            try
            {
                int rh = RDatas.GetLength(0);
                int rw = RDatas.GetLength(1);

                int gh = GDatas.GetLength(0);
                int gw = GDatas.GetLength(1);

                int bh = BDatas.GetLength(0);
                int bw = BDatas.GetLength(1);

                bool okw = rw == gw && rw == bw;
                bool okh = rh == gh && rh == bh;

                if (!(okw && okh))
                    return null;

                int height = rh;
                int width = rw;


                PixelFormat pixelFormat = PixelFormat.Format24bppRgb;
                Bitmap bitmap = new Bitmap(width, height, pixelFormat);
                BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadWrite, pixelFormat);
                {
                    int stride = bmpData.Stride;            // 扫描宽度
                    IntPtr scan0 = bmpData.Scan0;           // 位图的第一个扫描行(bmpData的内存起始位置)

                    int offset = stride - width * 3;        // 显示宽度与扫描线宽度的间隙  

                    int scanBytes = stride * height;        // 用stride宽度，表示这是内存区域的大小  
                    byte[] pixels = new byte[scanBytes];    // 像素数量

                    int posScan = 0;                        // 扫描指针

                    for (int i = 0; i < height; i++)
                    {
                        int ii = (!ymirror) ? (i) : ((height - 1) - i);

                        for (int j = 0; j < width; j++)
                        {
                            int jj = j;

                            dynamic RGray = RDatas[ii, jj];
                            dynamic GGray = GDatas[ii, jj];
                            dynamic BGray = BDatas[ii, jj];

                            pixels[posScan++] = (byte)BGray;
                            pixels[posScan++] = (byte)GGray;
                            pixels[posScan++] = (byte)RGray;
                        }

                        posScan += offset;
                    }

                    // 从 pixels 复制到 scan0
                    Marshal.Copy(pixels, 0, scan0, scanBytes);
                }
                bitmap.UnlockBits(bmpData);

                resoult = bitmap;
            }
            catch (Exception)
            {
            }

            return resoult;
        }
        [Obsolete("", true)]
        public static Image Data2Image24_bak_02<T>(T[,] RDatas, T[,] GDatas, T[,] BDatas, bool ymirror = false)
        {
            Image resoult = null;

            try
            {
                var vtemp = resoult;
                {
                    var r = ymirror ? Mirrory(RDatas) : RDatas;
                    var g = ymirror ? Mirrory(GDatas) : GDatas;
                    var b = ymirror ? Mirrory(BDatas) : BDatas;

                    vtemp = Data2ImageColor24(r, g, b);
                }

                resoult = vtemp;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }

            return resoult;
        }


        //@@@
    }


}
