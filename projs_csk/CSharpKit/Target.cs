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
using System.Runtime.InteropServices;

namespace CSharpKit
{
    /// <summary>
    /// 目标
    /// </summary>
    public class Target : ITarget, ITag, IDisposable, IEquatable<ITarget>
    {
        #region Constructors

        protected Target()
            : this("", "", null, null) { }

        protected Target(String name)
            : this("", name, null, null) { }

        protected Target(Object owner)
            : this("", "", owner, null) { }

        protected Target(String name, Object owner)
            : this("", name, owner, null) { }

        protected Target(Target rhs)
            : this(rhs.Id, rhs.Name, rhs.Owner, rhs.Tag) { }

        private Target(String id, String name, Object owner, Object tag)
        {
            _uid = Guid.NewGuid().ToString("B").ToUpper();
            //_uid = Guid.NewGuid().ToString("N").ToUpper();

            Id = id;
            Name = name;
            Owner = owner;
            Tag = tag;
        }

        #endregion

        #region Properties

        private readonly String _uid;
        /// <summary>
        /// 唯一标识 Guid
        /// </summary>
        public String Uid => _uid;


        /// <summary>
        /// 标识
        /// </summary>
        public String Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual String Name { get; set; }

        /// <summary>
        /// 拥有者
        /// </summary>
        public Object Owner { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public Object Tag { get; set; }

        #endregion

        #region IEquatable<ITarget> Members

        public override bool Equals(object obj)
        {
            return Equals(obj as ITarget);
        }
        public virtual bool Equals(ITarget other)
        {
            return true
                && other != null
                && Uid == other.Uid
                ;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode()
                ^ Uid.GetHashCode()
                //^ Id.GetHashCode()
                //^ Name.GetHashCode()
                ;
        }

        #endregion

        #region IDisposable Members

        public virtual void Dispose()
        {
            // throw new NotImplementedException();
        }


        #endregion


        int TargetHashCode()
        {
            int lenId = Id == null ? 0 : Id.Length;
            int lenUid = Uid == null ? 0 : Uid.Length;
            int lenName = Name == null ? 0 : Name.Length;

            int length = lenId + lenUid + lenName;
            var hashNum = Math.Min(length, 25);
            int hash = 17;

            //unchecked
            //{
            //    for (var i = 0; i < hashNum; i++)
            //    {
            //        hash = hash * 31 + At(i).GetHashCode();
            //    }
            //}

            return hash;
        }

        //}}@@@
    }


}
