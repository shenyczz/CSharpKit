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
using System.Text;

namespace CSharpKit.Platform.Windows
{
    /// <summary>
    /// 
    /// </summary>
    public class IniFile
    {
        public IniFile(String fileName)
        {
            FileName = fileName;
        }

        public String FileName { get; set; }

        public Int32 GetInt(String sectionName, String keyName)
        {
            return Win32Api.GetPrivateProfileInt(sectionName, keyName, -1, this.FileName);
        }

        public String GetString(String sectionName, String keyName)
        {
            StringBuilder strRet = new StringBuilder(256);
            Win32Api.GetPrivateProfileString(sectionName, keyName, "", strRet, -1, this.FileName);
            return strRet.ToString();
        }

        public Boolean GetBoolean(String sectionName, String keyName)
        {
            Boolean bRet = false;
            string sRet = GetString(sectionName, keyName);
            try
            {
                bRet = Boolean.Parse(sRet);
            }
            catch
            {
                return false;
            }

            return bRet;
        }

        public Double GetDouble(String sectionName, String keyName)
        {
            Double dValue = Double.NaN;
            //---------------------------------------------
            String sv = GetString(sectionName, keyName);
            if (!Double.TryParse(sv, out dValue))
                return Double.MaxValue;
            //---------------------------------------------
            return dValue;
        }

        public DateTime GetDateTime(String sectionName, String keyName)
        {
            DateTime dtValue;
            //---------------------------------------------
            String sv = GetString(sectionName, keyName);
            if (!DateTime.TryParse(sv, out dtValue))
                return DateTime.MaxValue;
            //---------------------------------------------
            return dtValue;
        }

        public Boolean WriteInt(String sectionName, String keyName, int value)
        {
            return WriteObject(sectionName, keyName, value);
        }

        public Boolean WriteString(String sectionName, String keyName, String strValue)
        {
            return Win32Api.WritePrivateProfileString(sectionName, keyName, strValue, this.FileName);
        }

        public Boolean WriteBoolean(String sectionName, String keyName, Boolean value)
        {
            return WriteObject(sectionName, keyName, value);
        }

        private Boolean WriteObject(String sectionName, String keyName, Object obj)
        {
            return WriteString(sectionName, keyName, obj.ToString());
        }

        //}}@@@
    }
}
