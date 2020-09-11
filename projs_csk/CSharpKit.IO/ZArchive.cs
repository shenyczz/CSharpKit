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
using System.CodeDom;
using System.IO;

namespace CSharpKit.IO
{
    public sealed class ZArchive
    {
        ZArchive() { }
        static readonly ZArchive Instance = new ZArchive();


        public static ZipFormat ZipFormatCode(Stream ins)
        {
            var result = ZipFormat.NONE;

            try
            {
                var vtemp = result;
                {
                    var br = new BinaryReader(ins);
                    var bs = br.ReadBytes(1024);
                    //br.Close();   // 不要关闭 ins
                    ins.Seek(0, SeekOrigin.Begin);

                    vtemp = Instance.ZipFormatCode(bs);
                }
                result = vtemp;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }




        public ZipFormat ZipFormatCode(byte[] bs)
        {
            var bz2code = bz2Code(bs);
            var rarcode = rarCode(bs);
            var tarcode = tarCode(bs);
            var zipcode = tarCode(bs);

            return false ? ZipFormat.NONE
                : bz2code != ZipFormat.NONE ? bz2code
                : rarcode != ZipFormat.NONE ? rarcode
                : tarcode != ZipFormat.NONE ? tarcode
                : zipcode != ZipFormat.NONE ? zipcode
                : ZipFormat.NONE;
        }

        private ZipFormat bz2Code(byte[] bs)
        {
            bool isbzip2 = true
                && bs[0] == 0x42 //'B'
                && bs[1] == 0x5A //'Z'
                && bs[2] == 0x68 //'h'
                && bs[3] == 0x39 //'9'
                && bs[4] == 0x31 //'1'
                ;

            return isbzip2 ? ZipFormat.BZ2 : ZipFormat.NONE;
        }
        private ZipFormat rarCode(byte[] bs) => ZipFormat.NONE;
        private ZipFormat tarCode(byte[] bs) => ZipFormat.NONE;
        private ZipFormat zipCode(byte[] bs) => ZipFormat.NONE;



        //}}@@@
    }







}
