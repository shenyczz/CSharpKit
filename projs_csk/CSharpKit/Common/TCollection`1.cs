/******************************************************************************
 * 
 * Announce: Meteorological Toolkit（MTK）.
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
using System.Collections;
using System.Collections.Generic;

namespace CSharpKit
{
    /// <summary>
    /// 基类约束
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// dotnet 支持的类型参数约束有以下五种：
    /// where T : class                 ----  T 必须是一个引用类型
    /// where T : struct                ----  T 必须是一个值类型
    /// where T : new()                 ----  T 必须要有一个无参构造函数, (即他要求类型参数必须提供一个无参数的构造函数)
    /// where T : NameOfBaseClass       ----  T 必须继承名为NameOfBaseClass的类
    /// where T : NameOfInterface       ----  T 必须实现名为NameOfInterface的接口, ITag
    /// 
    /// C# 7.3
    /// where T : Enum                  ----  T 必须是一个枚举类型
    /// where T : Delegate              ----  T 必须是一个委托类型
    /// where T : MulticastDelegate     ----  T 必须是一个多路广播委托类型
    /// where T : unmanaged             ----  T 必须是一个非托管类型类型
    ///                                       sbyte、byte、short、ushort、int、uint、long、ulong、char、float、double、decimal 或 bool
    ///                                       任何枚举类型
    ///                                       任何指针类型
    ///                                       任何用户定义的 struct 类型
    /// 
    /// C# 8.0
    /// where T : notnull               ----  T 必须实现名为NameOfInterface的接口, ITag
    /// 
    /// 
    /// Disable
    /// Object、Array 和 ValueType
    /// 
    /// </remarks>
    public class TCollection<T> : Target, IList<T>, IList
    {
        /// <summary>
        /// 
        /// </summary>
        public TCollection()
        {
            _tSet = new List<T>();
        }


        #region Fields

        protected List<T> _tSet;

        #endregion

        public void AddRange(IEnumerable<T> items)
        {
            _tSet.AddRange(items);
        }


        #region Find

        public T Find(Predicate<T> match)
        {
            return _tSet.Find(match);
        }
        public List<T> FindAll(Predicate<T> match)
        {
            return _tSet.FindAll(match);
        }
        public int FindIndex(Predicate<T> match)
        {
            return _tSet.FindIndex(match);
        }
        public int FindIndex(int startIndex, Predicate<T> match)
        {
            return _tSet.FindIndex(startIndex, match);
        }
        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            return _tSet.FindIndex(startIndex, count, match);
        }

        public T FindLast(Predicate<T> match)
        {
            return _tSet.FindLast(match);
        }

        public int FindLastIndex(Predicate<T> match)
        {
            return _tSet.FindLastIndex(match);
        }

        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            return _tSet.FindLastIndex(startIndex, match);
        }

        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            return _tSet.FindLastIndex(startIndex, count, match);
        }

        public void ForEach(Action<T> action)
        {
            _tSet.ForEach(action);
        }

        #endregion

        #region Sort

        public void Sort()
        {
            _tSet.Sort();
        }
        public void Sort(IComparer<T> comparer)
        {
            _tSet.Sort(comparer);
        }
        public void Sort(Comparison<T> comparison)
        {
            _tSet.Sort(comparison);
        }
        public void Sort(int index, int count, IComparer<T> comparer)
        {
            _tSet.Sort(index, count, comparer);
        }

        #endregion


        #region IList<T> Members

        public int IndexOf(T item)
        {
            return ((IList<T>)_tSet).IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            ((IList<T>)_tSet).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<T>)_tSet).RemoveAt(index);
        }

        public T this[int index]
        {
            get => ((IList<T>)_tSet)[index];
            set => ((IList<T>)_tSet)[index] = value;
        }

        public void Add(T item)
        {
            ((IList<T>)_tSet).Add(item);
        }

        public void Clear()
        {
            ((IList<T>)_tSet).Clear();
        }

        public bool Contains(T item)
        {
            return ((IList<T>)_tSet).Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((IList<T>)_tSet).CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return ((IList<T>)_tSet).Remove(item);
        }

        public int Count => ((IList<T>)_tSet).Count;

        public bool IsReadOnly => ((IList<T>)_tSet).IsReadOnly;

        public IEnumerator<T> GetEnumerator()
        {
            return ((IList<T>)_tSet).GetEnumerator();
        }

        public int Add(object value)
        {
            return ((IList)_tSet).Add(value);
        }

        public bool Contains(object value)
        {
            return ((IList)_tSet).Contains(value);
        }

        public int IndexOf(object value)
        {
            return ((IList)_tSet).IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            ((IList)_tSet).Insert(index, value);
        }

        public void Remove(object value)
        {
            ((IList)_tSet).Remove(value);
        }

        public bool IsFixedSize => ((IList)_tSet).IsFixedSize;

        public void CopyTo(Array array, int index)
        {
            ((IList)_tSet).CopyTo(array, index);
        }

        public object SyncRoot => ((IList)_tSet).SyncRoot;

        public bool IsSynchronized => ((IList)_tSet).IsSynchronized;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<T>)_tSet).GetEnumerator();
        }

        object IList.this[int index]
        {
            get => ((IList)_tSet)[index];
            set => ((IList)_tSet)[index] = value;
        }

        #endregion


        //@@@
    }

}
