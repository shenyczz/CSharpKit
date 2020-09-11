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
using System.IO;

namespace CSharpKit.Data
{
    public abstract class XFile : DataInstance
    {
        protected XFile(IProvider owner)
            : base(owner) { }


        /// <summary>
        /// 是否二进制文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Boolean IsBinaryFile(String fileName)
        {
            bool bin = false;

            try
            {
                byte[] bytes = null;

                FileInfo fi = new FileInfo(fileName);
                long len = fi.Length > 1024 ? 1024 : fi.Length;

                using (FileStream fs = fi.OpenRead())
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        bytes = br.ReadBytes((int)len);
                    }
                }

                // 判断(只要有一个字节等于0就是二进制文件)
                for (int i = 0; i < bytes.Length; i++)
                {
                    if (bytes[i] == 0)
                    {
                        bin = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }

            return bin;
        }



        //}}@@@
    }


}
