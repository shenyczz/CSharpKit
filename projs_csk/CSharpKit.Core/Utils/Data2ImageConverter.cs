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
using System.Runtime.InteropServices;

namespace CSharpKit.Utils
{
    public enum PixelBits
    {
        PB08,
        PB24,
        PB32,
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class Data2ImageConverter
    {
        #region Constructors

        Data2ImageConverter() { }
        public static readonly Data2ImageConverter Instance = new Data2ImageConverter();

        #endregion


        /// <summary>
        /// RGB通道合成 24 位彩色灰度图
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="RDatas"></param>
        /// <param name="GDatas"></param>
        /// <param name="BDatas"></param>
        /// <param name="ymirror"></param>
        /// <returns></returns>
        public Image Data2Image<T>(T[,] RDatas, T[,] GDatas, T[,] BDatas, bool ymirror)
        {
            Image resoult = null;

            try
            {
                var vtemp = resoult;
                {
                    var r = ymirror ? ArrayConverter.Instance.ArrayMirrory2D(RDatas) : RDatas;
                    var g = ymirror ? ArrayConverter.Instance.ArrayMirrory2D(GDatas) : GDatas;
                    var b = ymirror ? ArrayConverter.Instance.ArrayMirrory2D(BDatas) : BDatas;
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
        /// <summary>
        /// RGB通道合成 32 位彩色灰度图
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="RDatas"></param>
        /// <param name="GDatas"></param>
        /// <param name="BDatas"></param>
        /// <param name="opacity"></param>
        /// <param name="ymirror"></param>
        /// <returns></returns>
        public Image Data2Image<T>(T[,] RDatas, T[,] GDatas, T[,] BDatas, byte opacity, bool ymirror)
        {
            Image resoult = null;

            try
            {
                var vtemp = resoult;
                {
                    var r = ymirror ? ArrayConverter.Instance.ArrayMirrory2D(RDatas) : RDatas;
                    var g = ymirror ? ArrayConverter.Instance.ArrayMirrory2D(GDatas) : GDatas;
                    var b = ymirror ? ArrayConverter.Instance.ArrayMirrory2D(BDatas) : BDatas;
                    vtemp = Data2ImageColor32(r, g, b, opacity);
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
        /// <summary>
        /// RGB通道合成彩色灰度图
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="RDatas"></param>
        /// <param name="GDatas"></param>
        /// <param name="BDatas"></param>
        /// <param name="ePixelBits"><see cref="PixelBits"/></param>
        /// <param name="ymirror"></param>
        /// <returns></returns>
        public Image Data2Image<T>(T[,] RDatas, T[,] GDatas, T[,] BDatas, PixelBits ePixelBits, bool ymirror)
        {
            Image resoult = null;

            try
            {
                var vtemp = resoult;
                {
                    var r = ymirror ? ArrayConverter.Instance.ArrayMirrory2D(RDatas) : RDatas;
                    var g = ymirror ? ArrayConverter.Instance.ArrayMirrory2D(GDatas) : GDatas;
                    var b = ymirror ? ArrayConverter.Instance.ArrayMirrory2D(BDatas) : BDatas;

                    switch (ePixelBits)
                    {
                        case PixelBits.PB08:
                            vtemp = Data2ImageColor24(r, g, b);
                            break;

                        case PixelBits.PB24:
                            vtemp = Data2ImageColor24(r, g, b);
                            break;

                        case PixelBits.PB32:
                            vtemp = Data2ImageColor32(r, g, b);
                            break;

                        default:
                            break;
                    }

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






        /// <summary>
        /// 数据转换为 8 位有颜色表的彩色灰度图
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <param name="colorTable"></param>
        /// <returns></returns>
        public Image Data2Image<T>(T[,] datas, int[] colorTable)
        {
            Image resoult = null;

            try
            {
                var vtemp = resoult;
                {
                    ArrayConverter.Instance.Array2D1D(datas, out T[] raws, out int rows, out int cols);
                    vtemp = Data2ImageGray08(raws, rows, cols);
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
        /// <summary>
        /// 数据转换为 24 位彩色灰度图
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="datas">二维数组</param>
        /// <param name="predicaters">对输入数据rawsdata格点值的有效值检测器</param>
        /// <param name="tranformer">数据颜色转换器</param>
        /// <param name="ymirror">关于Y轴镜像</param>
        /// <returns></returns>
        public Image Data2Image<T>(T[,] datas, IColorTranformer<double> tranformer, Predicate<double> predicaters, bool ymirror)
        {
            Image result = default;

            try
            {
                var vtemp = result;

                if (datas != null)
                {
                    int rows = datas.GetLength(0);
                    int cols = datas.GetLength(1);

                    byte[,] redDatas = new byte[rows, cols];
                    byte[,] grnDatas = new byte[rows, cols];
                    byte[,] bluDatas = new byte[rows, cols];

                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            T fv = datas[i, j];
                            var dv = (double)Convert.ChangeType(fv, TypeCode.Double);

                            int clr = 0;    // 可以设置背景颜色

                            if (predicaters(dv))
                            {
                                clr = tranformer.GetColor(dv);
                            }

                            Color color = Color.FromArgb(clr);
                            bluDatas[i, j] = color.B;
                            grnDatas[i, j] = color.G;
                            redDatas[i, j] = color.R;
                        }
                    }//for(i)

                    var RDatas = ymirror ? ArrayConverter.Instance.ArrayMirrory2D(redDatas) : redDatas;
                    var GDatas = ymirror ? ArrayConverter.Instance.ArrayMirrory2D(grnDatas) : grnDatas;
                    var BDatas = ymirror ? ArrayConverter.Instance.ArrayMirrory2D(bluDatas) : bluDatas;

                    vtemp = Data2ImageColor24(RDatas, GDatas, BDatas);
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
        /// 数据转换为 32 位彩色灰度图
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <param name="tranformer"></param>
        /// <param name="opacity"></param>
        /// <param name="predicaters"></param>
        /// <param name="ymirror"></param>
        /// <returns></returns>
        public Image Data2Image<T>(T[,] datas, IColorTranformer<double> tranformer, byte opacity, Predicate<double> predicaters, bool ymirror)
        {
            Image result = default;

            try
            {
                var vtemp = result;

                if (datas != null)
                {
                    int rows = datas.GetLength(0);
                    int cols = datas.GetLength(1);

                    byte[,] redDatas = new byte[rows, cols];
                    byte[,] grnDatas = new byte[rows, cols];
                    byte[,] bluDatas = new byte[rows, cols];

                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            T fv = datas[i, j];
                            var dv = (double)Convert.ChangeType(fv, TypeCode.Double);

                            int clr = 0;    // 可以设置背景颜色

                            if (predicaters(dv))
                            {
                                clr = tranformer.GetColor(dv);
                            }

                            Color color = Color.FromArgb(clr);
                            bluDatas[i, j] = color.B;
                            grnDatas[i, j] = color.G;
                            redDatas[i, j] = color.R;
                        }
                    }//for(i)

                    var RDatas = ymirror ? ArrayConverter.Instance.ArrayMirrory2D(redDatas) : redDatas;
                    var GDatas = ymirror ? ArrayConverter.Instance.ArrayMirrory2D(grnDatas) : grnDatas;
                    var BDatas = ymirror ? ArrayConverter.Instance.ArrayMirrory2D(bluDatas) : bluDatas;

                    vtemp = Data2ImageColor32(RDatas, GDatas, BDatas, opacity);
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
        /// 生成RGB同值的8位灰度图（可以设置调色板）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <param name="ymirror"></param>
        /// <returns></returns>
        public Image Data2ImageGray<T>(T[,] datas, bool ymirror)
        {
            var result = default(Image);

            try
            {
                var vtemp = result;
                {
                    var dsmirror = (ymirror) ? ArrayConverter.Instance.ArrayMirrory2D(datas) : datas;
                    ArrayConverter.Instance.Array2D1D(dsmirror, out T[] raws, out int rows, out int cols);
                    vtemp = Data2ImageGray08(raws, cols, rows);
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
        /// 生成灰度图（RGB通道值相等）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <param name="ePixelBits"></param>
        /// <param name="ymirror"></param>
        /// <returns></returns>
        public Image Data2ImageGray<T>(T[,] datas, PixelBits ePixelBits, bool ymirror)
        {
            var result = default(Image);

            try
            {
                var vtemp = result;
                {
                    var dsmirror = (ymirror) ? ArrayConverter.Instance.ArrayMirrory2D(datas) : datas;
                    ArrayConverter.Instance.Array2D1D(dsmirror, out T[] raws, out int rows, out int cols);

                    switch (ePixelBits)
                    {
                        case PixelBits.PB08:
                            vtemp = Data2ImageGray08(raws, cols, rows);
                            break;

                        case PixelBits.PB24:
                            vtemp = Data2ImageGray24(raws, cols, rows);
                            break;

                        case PixelBits.PB32:
                            vtemp = Data2ImageGray32(raws, cols, rows);
                            break;

                        default:
                            break;
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

        /// <summary>
        /// 生成灰度图（RGB通道值相等）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <param name="ePixelBits"></param>
        /// <param name="gac"></param>
        /// <param name="ymirror"></param>
        /// <returns></returns>
        public Image Data2ImageGray<T>(T[,] datas, PixelBits ePixelBits, GammaCorrect gac, bool ymirror)
        {
            var result = default(Image);

            try
            {
                var vtemp = result;
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
        /// 针对256色位图设置调色板
        /// </summary>
        /// <param name="image256"></param>
        /// <param name="colorTable"></param>
        /// <returns></returns>
        public bool SetPalette(Image image256, Int32[] colorTable)
        {
            var result = false;

            try
            {
                var vtemp = result;
                var bitmap = image256 as Bitmap;
                var canOp = true
                    && bitmap != null
                    && colorTable != null
                    && bitmap.PixelFormat == PixelFormat.Format8bppIndexed;

                if (canOp)
                {
                    int len = colorTable.GetLength(0);
                    len = len > 255 ? 255 : len;

                    ColorPalette palette = bitmap.Palette;
                    for (int i = 0; i < len; i++)
                    {
                        palette.Entries[i] = Color.FromArgb(colorTable[i]);
                    }
                    bitmap.Palette = palette;

                    vtemp = true;
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







        #region Color08

        /// <summary>
        /// 256色位图
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <param name="colorTable"></param>
        /// <returns></returns>
        private Image Data2ImageColor08<T>(T[,] datas, int[] colorTable)
        {
            Image resoult = null;

            try
            {
                var vtemp = resoult;
                {
                    ArrayConverter.Instance.Array2D1D(datas, out T[] raws, out int rows, out int cols);
                    vtemp = Data2ImageGray08(raws, rows, cols);
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

        #endregion

        #region Color24

        private Image Data2ImageColor24<T>(T[,] RDatas, T[,] GDatas, T[,] BDatas)
        {
            Image resoult = null;

            try
            {
                // 红色通道的高度、宽度
                int rh = RDatas.GetLength(0);
                int rw = RDatas.GetLength(1);

                // 绿色通道的高度、宽度
                int gh = GDatas.GetLength(0);
                int gw = GDatas.GetLength(1);

                // 蓝色通道的高度、宽度
                int bh = BDatas.GetLength(0);
                int bw = BDatas.GetLength(1);

                // 检查尺寸一致性
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

        #endregion

        #region Color32


        private Image Data2ImageColor32<T>(T[,] RDatas, T[,] GDatas, T[,] BDatas)
        {
            return Data2ImageColor32(RDatas, GDatas, BDatas, 0xFF);
        }
        private Image Data2ImageColor32<T>(T[,] RDatas, T[,] GDatas, T[,] BDatas, byte opacity)
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

        // 可以正对每个像素设置不透明掩码
        private Image Data2ImageColor32<T>(T[,] RDatas, T[,] GDatas, T[,] BDatas, T[,] ADatas)
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

                int ah = ADatas.GetLength(0);
                int aw = ADatas.GetLength(1);

                bool okw = rw == gw && gw == bw && bw == aw;
                bool okh = rh == gh && gh == bh && bh == ah;

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
                            var A = (byte)Convert.ChangeType(ADatas[i, j], TypeCode.Byte); ;

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

        #endregion


        #region Gray08

        private Image Data2ImageGray08<T>(T[,] datas)
        {
            Image result = null;

            try
            {
                var vtemp = result;
                {
                    ArrayConverter.Instance.Array2D1D(datas, out T[] raws, out int rows, out int cols);
                    vtemp = Data2ImageGray08(raws, rows, cols);
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
        private Image Data2ImageGray08<T>(T[] raws, int height, int width)
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

        #endregion

        #region Gray24

        private Image Data2ImageGray24<T>(T[,] datas)
        {
            Image result = null;

            try
            {
                var vtemp = result;
                {
                    ArrayConverter.Instance.Array2D1D(datas, out T[] raws, out int rows, out int cols);
                    vtemp = Data2ImageGray24(raws, rows, cols);
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
        private Image Data2ImageGray24<T>(T[] raws, int height, int width)
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

        #endregion

        #region Gray32

        private Image Data2ImageGray32<T>(T[,] datas)
        {
            Image result = null;

            try
            {
                var vtemp = result;
                {
                    ArrayConverter.Instance.Array2D1D(datas, out T[] raws, out int rows, out int cols);
                    vtemp = Data2ImageGray32(raws, rows, cols);
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
        private Image Data2ImageGray32<T>(T[,] datas, byte opacity)
        {
            Image result = null;

            try
            {
                var vtemp = result;
                {
                    ArrayConverter.Instance.Array2D1D(datas, out T[] raws, out int rows, out int cols);
                    vtemp = Data2ImageGray32(raws, rows, cols, opacity);
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

        private Image Data2ImageGray32<T>(T[] raws, int height, int width)
        {
            return Data2ImageGray32(raws, height, width, 0xFF);
        }
        private Image Data2ImageGray32<T>(T[] raws, int height, int width, byte opacity)
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
                        var A = opacity;

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




        #region Obsolete Functions - 具有保存价值

        [Obsolete("具有保存价值", true)]
        Image Data2Image24_bak<T>(T[,] datas, Predicate<double> predicater, IColorTranformer<double> tranformer, bool ymirror)
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

            return bitmap;
        }

        #endregion



        //@@@
    }


}
