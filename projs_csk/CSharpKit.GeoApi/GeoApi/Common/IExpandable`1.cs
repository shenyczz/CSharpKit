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

namespace CSharpKit.GeoApi
{
    /// <summary>
    /// Function to expand compute a new object that is this object by expanded by other.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IExpandable<T>
    {
        /// <summary>
        /// Function to expand compute a new object that is this object by expanded by other
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        T ExpandedBy(T other);

        /// <summary>
        /// Method to expand this object by other
        /// </summary>
        /// <param name="other"></param>
        void ExpandToInclude(T other);

        //@EndOf(IExpandable<T>)
    }

}
