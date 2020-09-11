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
using System.ComponentModel;

namespace CSharpKit.ComponentModel
{
    /// <summary>
    /// ListConverter
    /// </summary>
    public class ListConverter : TypeConverter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="objects"></param>
        public ListConverter(object[] objects)
        {
            m_Objects = objects;
        }

        object[] m_Objects;

        /// <summary>
        /// 使用指定的上下文返回此对象是否支持可以从列表中选取的标准值集
        /// </summary>
        /// <param name="context">提供格式上下文</param>
        /// <returns>
        /// 如果应调用 System.ComponentModel.TypeConverter.GetStandardValues()
        /// 来查找对象支持的一组公共值，则为true；否则，为 false
        /// </returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// 使用指定的上下文返回此对象是否支持可以从列表中选取的标准值集
        /// </summary>
        /// <param name="context">提供格式上下文</param>
        /// <returns>
        /// 如果应调用 System.ComponentModel.TypeConverter.GetStandardValues()
        /// 来查找对象支持的一组公共值，则为 true；否则，为 false。
        /// </returns>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// 当与格式上下文一起提供时，返回此类型转换器设计用于的数据类型的标准值集合
        /// </summary>
        /// <param name="context">
        /// 提供格式上下文的 System.ComponentModel.ITypeDescriptorContext，
        /// 可用来提取有关从中调用此转换器的环境的附加信息.
        /// 此参数或其属性(Property) 可以为 null
        /// </param>
        /// <returns></returns>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(m_Objects);
        }
    }
}
