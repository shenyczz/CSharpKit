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
using System.Collections;
using System.Collections.Generic;

namespace CSharpKit.Maths.PointTracing
{
    class AAABB<T> :TCollection<int>
    { }

    public class TracePointCollectionBase : IList<TracePoint>, ICollection<TracePoint>, IEnumerable<TracePoint>, IList, ICollection, IEnumerable
    {
        public TracePointCollectionBase()
        {
            _TracePoints = new List<TracePoint>();
        }

        #region Fields

        private List<TracePoint> _TracePoints;

        #endregion


        public void AddRange(IEnumerable<TracePoint> points)
        {
            _TracePoints.AddRange(points);
        }

        public void Sort()
        {
            _TracePoints.Sort();
        }
        public void Sort(IComparer<TracePoint> comparer)
        {
            _TracePoints.Sort(comparer);
        }
        public void Sort(Comparison<TracePoint> comparison)
        {
            _TracePoints.Sort(comparison);
        }
        public void Sort(int index, int count, IComparer<TracePoint> comparer)
        {
            _TracePoints.Sort(index, count, comparer);
        }


        #region 接口实现

        public int IndexOf(TracePoint item)
        {
            return ((IList<TracePoint>)_TracePoints).IndexOf(item);
        }

        public void Insert(int index, TracePoint item)
        {
            ((IList<TracePoint>)_TracePoints).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<TracePoint>)_TracePoints).RemoveAt(index);
        }

        public TracePoint this[int index]
        {
            get => ((IList<TracePoint>)_TracePoints)[index];
            set => ((IList<TracePoint>)_TracePoints)[index] = value;
        }

        public void Add(TracePoint item)
        {
            ((IList<TracePoint>)_TracePoints).Add(item);
        }

        public void Clear()
        {
            ((IList<TracePoint>)_TracePoints).Clear();
        }

        public bool Contains(TracePoint item)
        {
            return ((IList<TracePoint>)_TracePoints).Contains(item);
        }

        public void CopyTo(TracePoint[] array, int arrayIndex)
        {
            ((IList<TracePoint>)_TracePoints).CopyTo(array, arrayIndex);
        }

        public bool Remove(TracePoint item)
        {
            return ((IList<TracePoint>)_TracePoints).Remove(item);
        }

        public int Count => ((IList<TracePoint>)_TracePoints).Count;

        public bool IsReadOnly => ((IList<TracePoint>)_TracePoints).IsReadOnly;

        public IEnumerator<TracePoint> GetEnumerator()
        {
            return ((IList<TracePoint>)_TracePoints).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<TracePoint>)_TracePoints).GetEnumerator();
        }

        public override string ToString()
        {
            return string.Format("Count = {0}", _TracePoints.Count);
        }

        public override bool Equals(object obj)
        {
            return _TracePoints.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _TracePoints.GetHashCode();
        }

        public int Add(object value)
        {
            return ((IList)_TracePoints).Add(value);
        }

        public bool Contains(object value)
        {
            return ((IList)_TracePoints).Contains(value);
        }

        public int IndexOf(object value)
        {
            return ((IList)_TracePoints).IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            ((IList)_TracePoints).Insert(index, value);
        }

        public void Remove(object value)
        {
            ((IList)_TracePoints).Remove(value);
        }

        object IList.this[int index] { get => ((IList)_TracePoints)[index]; set => ((IList)_TracePoints)[index] = value; }

        public bool IsFixedSize => ((IList)_TracePoints).IsFixedSize;

        public void CopyTo(Array array, int index)
        {
            ((IList)_TracePoints).CopyTo(array, index);
        }

        public object SyncRoot => ((IList)_TracePoints).SyncRoot;

        public bool IsSynchronized => ((IList)_TracePoints).IsSynchronized;

        #endregion

    }

}
