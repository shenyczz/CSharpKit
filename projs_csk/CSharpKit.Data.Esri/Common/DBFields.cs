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

namespace CSharpKit.Data.Esri
{
    /// <summary>
    /// XBaseFields - 数据库字段集合
    /// </summary>
    public sealed class DBFields : List<DBField>
    {
        #region Properties

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public DBField this[String fieldName]
        {
            get
            {
                int index = this.IndexOf(fieldName);
                if (index < 0)
                    return null;
                else
                    return this[index];
            }
            set
            {
                int index = this.IndexOf(fieldName);
                if (index < 0)
                    return;

                this[index] = value;
            }
        }

        /// <summary>
        /// 是否有显示字段
        /// </summary>
        public Boolean HasDisplayField
        {
            get
            {
                bool bValue = false;

                foreach (DBField field in this)
                {
                    if (field.IsDisplayField)
                    {
                        bValue = true;
                        break;
                    }
                }

                return bValue;
            }
        }

        /// <summary>
        /// 显示字段名称
        /// 如果没有则为空字符串
        /// </summary>
        public String DisplayFieldName
        {
            get
            {
                if (!HasDisplayField)
                    return String.Empty;

                foreach (DBField field in this)
                {
                    if (field.IsDisplayField)
                    {
                        return field.FieldName;
                    }
                }

                return String.Empty;
            }
        }

        #endregion

        #region Public Functions

        public int IndexOf(String fieldName)
        {
            for (int index = 0; index < this.Count; index++)
            {
                DBField field = this[index];
                if (field.FieldName.ToLower() == fieldName.ToLower())
                {
                    return index;
                }
            }

            return -1;
        }

        public Boolean Contains(String fieldName)
        {
            Boolean bContain = false;
            DBField field = this[fieldName];
            if (field != null)
            {
                bContain = this.Contains(field);
            }
            return bContain;
        }

        /// <summary>
        /// 设置显示字段
        /// </summary>
        /// <param name="index"></param>
        public Boolean EnableDisplayField(int index)
        {
            Boolean bret = false;
            DBField field = null;
            try
            {
                field = this[index];
            }
            catch (Exception)
            {
                bret = false;
            }
            finally
            {
                bret = EnableDisplayField(field);
            }
            return bret;
        }

        /// <summary>
        /// 设置显示字段
        /// </summary>
        /// <param name="fieldName"></param>
        public Boolean EnableDisplayField(String fieldName)
        {
            int index = this.GetFieldIndex(fieldName);
            if (index < 0)
                return false;

            DBField field = this[index];
            return EnableDisplayField(field);
        }

        /// <summary>
        /// 设置显示字段
        /// </summary>
        /// <param name="field"></param>
        public Boolean EnableDisplayField(DBField field)
        {
            foreach (DBField fld in this)
            {
                fld.IsDisplayField = false;
            }

            foreach (DBField fld in this)
            {
                if (fld == field)
                {
                    fld.IsDisplayField = true;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 禁止显示字段
        /// </summary>
        public void DisableDisplayField()
        {
            foreach (DBField fld in this)
            {
                fld.IsDisplayField = false;
            }
        }

        #endregion

        #region Private Functions

        private int GetFieldIndex(String fieldName)
        {
            for (int index = 0; index < this.Count; index++)
            {
                DBField field = this[index];
                if (field.FieldName.ToLower() == fieldName.ToLower())
                {
                    return index;
                }
            }

            return -1;
        }

        #endregion

        //@EndOf(XBaseFields)
    }

}
