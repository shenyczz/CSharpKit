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
using System.ComponentModel;

namespace CSharpKit.ComponentModel
{
    /// <summary>
    /// PropertyItemDescriptor - 属性条目描述器
    /// 提供类上的属性的抽象化。
    /// </summary>
    public class CustomPropertyDescriptor : PropertyDescriptor
    {
        #region Constructors

        public CustomPropertyDescriptor(ref CustomProperty propertyItem, Attribute[] attrs)
            : base(propertyItem.Name, attrs)
        {
            _propertyItem = propertyItem;
        }

        #endregion

        #region Fields

        private CustomProperty _propertyItem;

        #endregion

        #region Properties

        /// <summary>
        /// 获取该成员所属的类别的名称，如 System.ComponentModel.CategoryAttribute 中所指定的。
        /// </summary>
        public override String Category
        {
            get { return _propertyItem.Category; }
        }

        /// <summary>
        /// 获取成员的说明，如 System.ComponentModel.DescriptionAttribute 中所指定的。
        /// </summary>
        public override String Description
        {
            get { return _propertyItem.Description; }
        }

        /// <summary>
        /// 可以显示在窗口（如“属性”窗口）中的名称。
        /// </summary>
        public override String DisplayName
        {
            get { return _propertyItem.DisplayName; }
        }

        /// <summary>
        /// 是否只能在设计时设置该成员
        /// </summary>
        public override Boolean DesignTimeOnly
        {
            get { return false; }
        }

        /// <summary>
        /// 指示该成员是否可浏览
        /// </summary>
        public override Boolean IsBrowsable
        {
            get { return _propertyItem.Browsable; }
        }

        /// <summary>
        /// 获取指定类型的编辑器
        /// </summary>
        /// <param name="editorBaseType"></param>
        /// <returns></returns>
        public override Object GetEditor(Type editorBaseType)
        {
            return _propertyItem.Editor == null ? base.GetEditor(editorBaseType) : _propertyItem.Editor;
        }

        /// <summary>
        /// 获取该属性的类型转换器
        /// </summary>
        /// <returns>
        /// 一个 System.ComponentModel.TypeConverter，用于转换该属性的 System.Type
        /// </returns>
        public override TypeConverter Converter
        {
            get
            {
                return _propertyItem == null ? base.Converter : _propertyItem.Converter;
            }
        }

        #region PropertyDescriptor Abstract Properties

        /// <summary>
        /// 获取该属性绑定到的组件的类型。
        /// </summary>
        public override Type ComponentType
        {
            get { return _propertyItem.GetType(); }
        }

        /// <summary>
        /// 获取指示该属性是否为只读的值
        /// </summary>
        public override Boolean IsReadOnly
        {
            get { return _propertyItem == null ? true : _propertyItem.ReadOnly; }
        }

        /// <summary>
        /// 属性类型
        /// </summary>
        public override Type PropertyType
        {
            get { return _propertyItem == null ? base.GetType() : _propertyItem.DefaultValue.GetType(); }
        }

        #endregion

        #endregion

        #region Public Functions

        #region PropertyDescriptor Abstract Function

        /// <summary>
        /// 重置对象时是否更改其值。
        /// </summary>
        /// <param name="component">要测试重置功能的组件。</param>
        /// <returns>如果重置组件更改其值，则为 true；否则为 false</returns>
        public override bool CanResetValue(object component)
        {
            return _propertyItem == null ? false : _propertyItem.DefaultValue != null;
        }

        /// <summary>
        /// 获取组件上的属性的当前值
        /// </summary>
        /// <param name="component">具有为其检索值的属性的组件</param>
        /// <returns>给定组件的属性的值</returns>
        public override Object GetValue(Object component)
        {
            return _propertyItem == null ? null : _propertyItem.Value;
        }

        /// <summary>
        /// 重置为默认值
        /// </summary>
        /// <param name="component"></param>
        public override void ResetValue(Object component)
        {
            if (_propertyItem != null) _propertyItem.ResetValue();
        }

        /// <summary>
        /// 将组件的值设置为一个不同的值
        /// </summary>
        /// <param name="component">具有要进行设置的属性值的组件。</param>
        /// <param name="value">新值</param>
        public override void SetValue(Object component, Object value)
        {
            if (_propertyItem != null) _propertyItem.Value = value;
        }

        /// <summary>
        /// 是否需要永久保存此属性的值
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override Boolean ShouldSerializeValue(Object component)
        {
            // true 和 false 有什么不同?
            return true;
        }

        #endregion

        #endregion
    }
}
