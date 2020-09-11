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

namespace CSharpKit.GeoApi.Geometries
{

    public class ContourPlex : GeometryCollection<Contour>
    {
        /// <summary>
        /// 等值线值
        /// </summary>
        public Double Value { get; set; }


        Extent _Extent;
        public new Extent Extent
        {
            get
            {
                _Extent = new Extent(double.MaxValue, double.MaxValue, double.MinValue, double.MinValue);
                foreach (Contour contour in this)
                {
                    _Extent.Union(contour.Extent);
                }
                return _Extent;
            }
        }

    }


    /*
    /// <summary>
    /// ContourGroup - 等高线簇
    /// </summary>
    public class ContourPlex : GeometryCollection, IList<Contour>
    {
        #region Properties

        /// <summary>
        /// 等值线值
        /// </summary>
        public Double Value { get; set; }

        Extent _Extent;
        public new Extent Extent
        {
            get
            {
                _Extent = new Extent(double.MaxValue, double.MaxValue, double.MinValue, double.MinValue);
                foreach (Contour contour in this)
                {
                    _Extent.Union(contour.Extent);
                }
                return _Extent;
            }
        }

        #endregion

        /// <summary>
        /// 使图形偏移指定的量
        /// </summary>
        /// <param name="dx">X坐标的偏移量</param>
        /// <param name="dy">Y坐标的偏移量</param>
        public override void Offset(Double dx, Double dy)
        {
            base.Offset(dx, dy);
            //...
        }

        #region IList<Contour> 成员

        public int IndexOf(Contour item)
        {
            return _Geometries.IndexOf(item);
        }

        public void Insert(int index, Contour item)
        {
            _Geometries.Insert(index, item);
        }

        public new void RemoveAt(int index)
        {
            _Geometries.RemoveAt(index);
        }

        public new Contour this[int index]
        {
            get { return _Geometries[index] as Contour; }
            set { _Geometries[index] = value; }
        }

        #endregion

        #region ICollection<Contour> 成员

        public void Add(Contour item)
        {
            _Geometries.Add(item);
        }

        public new void Clear()
        {
            _Geometries.Clear();
        }

        public bool Contains(Contour item)
        {
            return _Geometries.Contains(item);
        }

        public void CopyTo(Contour[] array, int arrayIndex)
        {
            _Geometries.CopyTo(array, arrayIndex);
        }

        public new int Count
        {
            get { return _Geometries.Count; }
        }

        public new bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Contour item)
        {
            return _Geometries.Remove(item);
        }

        #endregion

        #region IEnumerable<Contour> 成员

        public new IEnumerator<Contour> GetEnumerator()
        {
            foreach (Geometry geom in _Geometries)
            {
                yield return geom as Contour;
            }
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException("private function!");
        }

        #endregion
    }

    */































}
