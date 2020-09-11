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
    public abstract class XFileProcessor : Processor
    {
        #region Constructors

        protected XFileProcessor(IDataInstance owner)
            : base(owner) { }

        #endregion



        #region Public Functions


        /// <summary>
        /// 装载
        /// </summary>
        /// <returns></returns>
        public override Boolean Load()
        {
            IDataInstance fileDataInstance = this.Owner as IDataInstance;
            if (fileDataInstance == null)
                return false;

            String fileName = (this.Owner as IDataInstance).ConnectionString;
            if (!File.Exists(fileName))
                return false;

            bool isLoaded = false;

            if (XFile.IsBinaryFile(fileName))
                isLoaded = this.Load_bin(fileName);
            else
                isLoaded = Load_txt(fileName);

            return isLoaded;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public override Boolean Save()
        {
            IDataInstance fileDataInstance = this.Owner as IDataInstance;
            if (fileDataInstance == null)
                return false;

            String fileName = (this.Owner as IDataInstance).ConnectionString;
            if (String.IsNullOrEmpty(fileName))
                return false;

            return SaveAs(fileName);
        }
        public override Boolean SaveAs(String fileName)
        {
            bool isSave = false;

            //if (FileDataInstance.IsBinaryFile(fileName))
            //    isSave = Save_bin(fileName);
            //else
            isSave = Save_txt(fileName);

            return isSave;
        }

        #endregion

        #region Protected Functions

        protected override void Initialize()
        {
            // no body
        }

        protected virtual Boolean Load_bin(String fileName)
        {
            return false;
        }

        protected virtual Boolean Load_txt(String fileName)
        {
            return false;
        }

        protected virtual Boolean Save_bin(String fileName)
        {
            return false;
        }

        protected virtual Boolean Save_txt(String fileName)
        {
            return false;
        }

        /// <summary>
        /// 计算包围盒
        /// </summary>
        protected override void ComputeBoundingBox()
        {
            // no body
        }

        /// <summary>
        /// 查找极小值和极大值(顺便计算平均值)
        /// </summary>
        protected override void LookupExtremum()
        {
            // no body
        }

        #endregion





        #region FillDataInfo - 填充数据信息

        /// <summary>
        /// 填充数据信息
        /// </summary>
        protected virtual void FillDataInfo()
        {
            //throw new NotImplementedException("Please override this function!");
        }

        #endregion

        //}}@@@
    }



}
