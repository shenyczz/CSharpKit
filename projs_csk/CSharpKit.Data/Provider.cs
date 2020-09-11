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
    public abstract class Provider : Target, IProvider
    {
        protected Provider()
            : this("", null) { }

        protected Provider(String connectionString, Object tag)
            : base(connectionString)
        {
            this.Tag = tag;
            this.ConnectionString = connectionString;
            this.Open();
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public String ConnectionString { get; set; }

        /// <summary>
        /// 数据实例
        /// </summary>
        public IDataInstance DataInstance { get; protected set; }

        /// <summary>
        /// 是否打开
        /// </summary>
        public Boolean IsOpen { get; protected set; }

        /// <summary>
        /// 打开
        /// </summary>
        /// <returns></returns>
        public virtual void Open()
        {
            if(!IsOpen)
            {
                var tag = this.Tag;

                this.Close();
                this.DataInstance = GetDataInstance();
                this.IsOpen = (DataInstance != null);
            }
        }

        /// <summary>
        /// 带参数打开
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tag"></param>
        public void Open(String connectionString, Object tag)
        {
            Tag = tag;
            ConnectionString = connectionString;
            Open();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public virtual void Close()
        {
            IsOpen = false;
        }

        /// <summary>
        /// 垃圾回收处理
        /// </summary>
        public override void Dispose()
        {
            Close();
        }


        protected abstract IDataInstance GetDataInstance();



        public override string ToString()
        {
            return String.Format("{0}: {1}, {2}"
                , GetType().Name
                , ConnectionString
                , IsOpen ? "Open" : "Close"
                );
        }



        //}}@@@
    }

}
