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
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace CSharpKit.ComponentModel
{
    /// <summary>
    /// CustomProperty - 自定义属性项
    /// </summary>
    public class CustomProperty
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="defaultValue">属性值</param>
        public CustomProperty(String name, Object defaultValue)
        {
            this.ValueChanged = null;

            this.Name = name;
            this.DefaultValue = defaultValue;

            this.Category = "";
            this.Description = Name;
            this.DisplayName = Name;

            this.Browsable = true;
            this.ReadOnly = false;

            this.Editor = null;
            this.Converter = null;
        }

        public CustomProperty(
            String name,
            String[] propertyNames,
            Type valueType,
            Object defaultValue,
            Object value,
            Boolean isReadOnly,
            Boolean isBrowsable,
            String category,
            String description,
            Object objectSource,
            Type editorType)
        {
            // no body
        }

        #endregion

        #region Constants

        //-------------------------------------------------
        public const String Category_Misce = "1.杂项";
        public const String Comment = "Comment";                    // 注释
        //-------------------------------------------------
        public const String Category_Appearance = "Appearance - 外观";
        public const String Background = "Background";              // 背景颜色
        public const String Foreground = "Foreground";              // 前景颜色
        public const String Opacity = "Opacity";                    // 不透明度
        public const String Transparency = "Transparency";          // 透明度
        public const String LineWidth = "LineWidth";                // 线宽
        public const String LineStyle = "LineStyle";                // 线型
        public const String IsVisible = "IsVisible";                // 可见
        public const String IsSelected = "IsSelected";              // 选中
        public const String Thickness = "Thickness";                // 大气层厚度
        
        //-------------------------------------------------
        public const String Category_Behaviour = "Behaviour - 行为";
        public const String IsClip = "IsClip";                      // 剪切
        //-------------------------------------------------
        public const String Category_Contour = "Contour - 等值线";
        public const String IsDrawContour = "IsDrawContour";            // 绘制等值线
        public const String IsFillContour = "IsFillContour";            // 填充等值线
        public const String IsLabelContour = "IsLabelContour";          // 标注等值线
        public const String IsColorContour = "IsColorContour";          // 彩色等值线
        //-------------------------------------------------
        public const String Category_Data = "Data - 数据";
        public const String Text = "Text";                          // 文本
        public const String Font = "Font";                          // 字体
        public const String Decimals = "Decimals";                  // 小数位数
        public const String ShowData = "ShowData";                  // 显示数据
        public const String ShowStationName = "ShowStationName";    // 显示站点名称
        //-------------------------------------------------
        //-------------------------------------------------
        public const String Category_DataInfo = "DataInfo - 数据信息";
        public const String StationID = "StationID";                // 站点ID
        public const String StationName = "StationName";            // 站点名称
        public const String DateTime = "DateTime";                  // 日期时间
        public const String DateTime2 = "DateTime2";                // 日期时间
        public const String DataCode = "DataCode";                  // 数据代码
        public const String Cut = "Cut";                            // 雷达扫描层
        public const String Elevation = "Elevation";                // 雷达仰角
        
        //-------------------------------------------------
        public const String Category_Layout = "布局";
        //-------------------------------------------------
        public const String Category_Legend = "图例";
        //-------------------------------------------------

        #endregion

        #region Properties

        private String _Name = string.Empty;
        /// <summary>
        /// 名称
        /// </summary>
        [Description("名称")]
        public String Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                if (PropertyNames == null)
                {
                    PropertyNames = new String[] { _Name };
                }
            }
        }

        private Object _Value;
        /// <summary>
        /// 值
        /// </summary>
        public Object Value
        {
            get { return _Value; }
            set
            {
                if (_Value != value && value != null)
                {
                    _Value = value;
                    FireEvent_ValueChanged(this, new PropertyChangedEventArgs(this.Name));
                }
            }
        }

        /// <summary>
        /// 标签
        /// </summary>
        public Object Tag { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public String Category { get; set; }

        /// <summary>
        /// 显示的名称。
        /// </summary>
        public String DisplayName { get; set; }

        /// <summary>
        /// 指示该成员是否可浏览的值，
        /// 如 System.ComponentModel.BrowsableAttribute 中所指定的
        /// </summary>
        public Boolean Browsable { get; set; }

        /// <summary>
        /// 只读
        /// </summary>
        public Boolean ReadOnly { get; set; }

        /// <summary>
        /// 编辑器
        /// </summary>
        public Object Editor { get; set; }

        /// <summary>
        /// 编辑器类型
        /// </summary>
        public Type EditorType { get; set; }

        /// <summary>
        /// 类型转换器
        /// </summary>
        public TypeConverter Converter { get; set; }

        private Object _DefaultValue = null;
        /// <summary>
        /// 默认属性
        /// </summary>
        public Object DefaultValue
        {
            get { return _DefaultValue; }
            set
            {
                _DefaultValue = value;
                if (_DefaultValue != null)
                {
                    if (_Value == null) _Value = _DefaultValue;
                    if (_ValueType == null && DefaultValue!=null)
                    {
                        _ValueType = _DefaultValue.GetType();
                    }
                }
            }
        }

        private Type _ValueType = null;
        public Type ValueType
        {
            get { return _ValueType; }
        }

        private Object _ObjectSource = null;
        /// <summary>
        /// 源对象
        /// </summary>
        public Object ObjectSource
        {
            get { return _ObjectSource; }
            set
            {
                _ObjectSource = value;
                OnObjectSourceChanged();
            }
        }

        /// <summary>
        /// 属性名称
        /// </summary>
        public String[] PropertyNames { get; set; }

        private PropertyInfo[] _PropertyInfos = null;
        /// <summary>
        /// 属性信息
        /// </summary>
        protected PropertyInfo[] PropertyInfos
        {
            get
            {
                if (PropertyNames == null)
                    return null;

                if (_PropertyInfos == null)
                {
                    Type type = ObjectSource.GetType();
                    _PropertyInfos = new PropertyInfo[PropertyNames.Length];

                    for (int i = 0; i < PropertyNames.Length; i++)
                    {
                        _PropertyInfos[i] = type.GetProperty(PropertyNames[i]);
                    }
                }

                return _PropertyInfos;
            }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// 重置
        /// </summary>
        public void ResetValue()
        {
            Value = DefaultValue;
        }
        /// <summary>
        /// 返回哈希码
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Value.GetHashCode();
        }
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            //---------------------------------------------
            sb.Append("属性名称：" + Name + "\n");
            sb.AppendLine();
            sb.Append("属性数值：" + Value.ToString());
            //---------------------------------------------
            return sb.ToString();
        }

        #endregion

        #region Protected Functions

        protected void OnObjectSourceChanged()
        {
            if (PropertyInfos == null)
                return;

            if (PropertyInfos.Length == 0)
                return;

            Object value = PropertyInfos[0].GetValue(_ObjectSource, null);
            if (_DefaultValue == null)
                DefaultValue = value;

            _Value = value;
        }

        protected void OnValueChanged()
        {
            if (_ObjectSource == null)
                return;

            foreach (PropertyInfo propertyInfo in PropertyInfos)
            {
                //propertyInfo.SetValue(_ObjectSource, _Value, null);
            }
        }

        #endregion

        #region --事件处理--

        /// <summary>
        /// 属性改变事件
        /// </summary>
        public event EventHandler<PropertyChangedEventArgs> ValueChanged;
        /// <summary>
        /// 点燃属性值改变事件
        /// </summary>
        private void FireEvent_ValueChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(sender, e);
            }
        }

        #endregion

        //@@@
    }
}
