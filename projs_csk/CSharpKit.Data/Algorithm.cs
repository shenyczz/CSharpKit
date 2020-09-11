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
    public abstract class Algorithm : Target, IAlgorithm
    {
        protected Algorithm(IDataInstance owner)
            :base(Guid.NewGuid().ToString("N"), owner)
        {
            this.Tag = (owner as ITag)?.Tag;

            Initialize();
        }

        protected virtual void Initialize() => throw new NotImplementedException();


        public virtual bool IsValid(double v) => throw new NotImplementedException();


        //}}@@@
    }

}
