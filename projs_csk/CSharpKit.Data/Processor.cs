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

namespace CSharpKit.Data
{
    /// <summary>
    /// DataProcessor - 数据处理器
    /// </summary>
    public abstract class Processor : Target, IProcessor
    {
        protected Processor(IDataInstance owner)
            : base(owner)
        {
            if (this.Owner == null)
                return;

            if (string.IsNullOrEmpty((this.Owner as IDataInstance).ConnectionString))
                return;

            this.Tag = (owner as ITag)?.Tag;


            // 1.初始化
            this.Initialize();

            // 2.加载
            IsLoad = Load();

            if (!IsLoad)
            {
                //throw new Exception("装载数据错误! - in DataProcessor::DataProcessor()");
            }

            // 计算包围盒
            ComputeBoundingBox();

            // 查找极小值和极大值(顺便计算平均值)
            LookupExtremum();

            return;
        }

        /// <summary>
        /// 是否装载
        /// </summary>
        public Boolean IsLoad { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public long ErrorCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public String ErrorMessage { get; set; }


        /// <summary>
        /// 初始化
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// 装载
        /// </summary>
        /// <returns></returns>
        public abstract Boolean Load();

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public abstract Boolean Save();

        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public abstract Boolean SaveAs(String fileName);

        /// <summary>
        /// 计算包围盒
        /// </summary>
        protected abstract void ComputeBoundingBox();

        /// <summary>
        /// 查找极小值和极大值(顺便计算平均值)
        /// </summary>
        protected abstract void LookupExtremum();

        //}}@@@
    }

}
