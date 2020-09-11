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

namespace CSharpKit.ComponentModel
{
    /// <summary>
    /// 自定义属性集合
    /// </summary>
    public class CustomPropertyCollection : IList<CustomProperty>, ICustomTypeDescriptor
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomPropertyCollection()
        {
            _PropertyItemCollection = new List<CustomProperty>();
        }

        #endregion

        #region Fields

        private List<CustomProperty> _PropertyItemCollection;

        #endregion

        #region --成员函数--

        /// <summary>
        /// 名称索引器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CustomProperty this[String name]
        {
            get
            {
                CustomProperty propertyItem = null;
                foreach (CustomProperty item in this)
                {
                    if (item.Name.Equals(name))
                    {
                        propertyItem = item;
                        break;
                    }
                }
                return propertyItem;
            }
            set
            {
                if (this[value.Name] == null) return;
                else this[value.Name].ValueChanged -= item_ValueChanged;

                foreach (CustomProperty propItem in _PropertyItemCollection)
                {
                    if (propItem.Name == name)
                    {
                        propItem.ValueChanged += item_ValueChanged;
                        propItem.Value = value;
                        break;
                    }
                }
            }

        }

        /// <summary>
        /// 包含指定名称的属性项
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Boolean Contains(String name)
        {
            return this[name] != null;
        }
        
        #endregion

        #region --事件处理--

        /// <summary>
        /// 事件 - 属性项改变
        /// </summary>
        public event EventHandler<CustomPropertyChangedEventArgs> CustomPropertyChanged;
        /// <summary>
        /// 点燃属性改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FireEvent_PropertyItemChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.CustomPropertyChanged != null)
            {
                this.CustomPropertyChanged(sender, new CustomPropertyChangedEventArgs(e.PropertyName));
            }
        }

        /// <summary>
        /// 属性条目事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void item_ValueChanged(object sender, PropertyChangedEventArgs e)
        {
            // 点燃 CustomPropertyChanged 事件
            FireEvent_PropertyItemChanged(sender, e);
        }

        #endregion


        public int RemoveAll(Predicate<CustomProperty> match)
        {
            return _PropertyItemCollection.RemoveAll(match);
        }



        #region IList<CustomProperty> 成员

        public int IndexOf(CustomProperty item)
        {
            return _PropertyItemCollection.IndexOf(item);
        }

        public void Insert(int index, CustomProperty item)
        {
            if (this.Contains(item.Name))
                return;

            item.ValueChanged += item_ValueChanged;
            _PropertyItemCollection.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            CustomProperty item = _PropertyItemCollection[index];
            if (item == null)
                return;

            item.ValueChanged -= item_ValueChanged;
            _PropertyItemCollection.RemoveAt(index);
        }

        public CustomProperty this[int index]
        {
            get
            {
                return _PropertyItemCollection[index];
            }
            set
            {
                if (value == null) return;

                CustomProperty item = _PropertyItemCollection[index];
                if (item != null) item.ValueChanged -= item_ValueChanged;

                value.ValueChanged += item_ValueChanged;
                _PropertyItemCollection[index] = value;
            }
        }

        #endregion

        #region ICollection<CustomProperty> 成员

        public void Add(CustomProperty item)
        {
            if (this.Contains(item.Name))
            {
                CustomProperty itm = this[item.Name];
                itm.DefaultValue = item.Value;
                itm.Category = item.Category;
                itm.DisplayName = item.DisplayName;
                itm.Browsable = item.Browsable;
                itm.ReadOnly = item.ReadOnly;
                itm.Converter = item.Converter;
                itm.Editor = item.Editor;
            }
            else
            {
                item.ValueChanged += item_ValueChanged;
                _PropertyItemCollection.Add(item);
            }
        }

        public void Clear()
        {
            foreach (CustomProperty item in _PropertyItemCollection)
                item.ValueChanged -= item_ValueChanged;

            _PropertyItemCollection.Clear();
        }

        public bool Contains(CustomProperty item)
        {
            return _PropertyItemCollection.Contains(item);
        }

        public void CopyTo(CustomProperty[] array, int arrayIndex)
        {
            _PropertyItemCollection.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _PropertyItemCollection.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(CustomProperty item)
        {
            if (this.Contains(item))
                item.ValueChanged -= item_ValueChanged;

            return _PropertyItemCollection.Remove(item);
        }

        public bool Remove(String itemName)
        {
            foreach (CustomProperty item in _PropertyItemCollection)
            {
                if (item.Name.Equals(itemName))
                {
                    return Remove(item);
                }
            }
            return false;
        }

        #endregion

        #region IEnumerable<CustomProperty> 成员

        public IEnumerator<CustomProperty> GetEnumerator()
        {
            return _PropertyItemCollection.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (CustomProperty item in _PropertyItemCollection)
                yield return item;
        }

        #endregion

        #region ICustomTypeDescriptor 成员

        /// <summary>
        /// 返回此组件实例的自定义特性的集合。
        /// </summary>
        /// <returns></returns>
        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        /// <summary>
        /// 返回此组件实例的类名。
        /// </summary>
        /// <returns></returns>
        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        /// <summary>
        /// 返回此组件实例的名称。
        /// </summary>
        /// <returns></returns>
        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        /// <summary>
        /// 返回此组件实例的类型转换器。
        /// </summary>
        /// <returns></returns>
        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        /// <summary>
        /// 返回此组件实例的默认事件。
        /// </summary>
        /// <returns></returns>
        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        /// <summary>
        /// 返回此组件实例的默认属性。
        /// </summary>
        /// <returns></returns>
        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        /// <summary>
        /// 返回此组件实例的指定类型的编辑器。
        /// </summary>
        /// <param name="editorBaseType"></param>
        /// <returns></returns>
        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        /// <summary>
        /// 返回此组件实例的事件。
        /// </summary>
        /// <returns></returns>
        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        /// <summary>
        /// 将指定的特性数组用作筛选器来返回此组件实例的事件。
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        /// <summary>
        /// 返回此组件实例的属性。
        /// </summary>
        /// <returns></returns>
        public PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(this, true);
        }

        /// <summary>
        /// 返回将特性数组用作筛选器的此组件实例的属性。
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptorCollection properties = new PropertyDescriptorCollection(null);

            for (int i = 0; i < this.Count; i++)
            {
                CustomProperty prop = this[i];

                List<Attribute> attrs = new List<Attribute>();
                //[Browsable(false)]
                if (!prop.Browsable)
                {
                    attrs.Add(new BrowsableAttribute(prop.Browsable));
                }
                //[ReadOnly(true)]
                if (prop.ReadOnly)
                {
                    attrs.Add(new ReadOnlyAttribute(prop.ReadOnly));
                }
                //[Editor(typeof(editor),typeof(UITypeEditor))]
                if (prop.EditorType != null)
                {
                    // waiting...
                    //attrs.Add(new EditorAttribute(prop.EditorType, typeof(System.Drawing.Design.UITypeEditor)));
                }

                properties.Add(new CustomPropertyDescriptor(ref prop, attrs.ToArray()));
            }

            return properties;
        }

        /// <summary>
        /// 返回包含指定的属性描述符所描述的属性的对象。
        /// </summary>
        /// <param name="pd"></param>
        /// <returns></returns>
        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion

        //@@@
    }


}
