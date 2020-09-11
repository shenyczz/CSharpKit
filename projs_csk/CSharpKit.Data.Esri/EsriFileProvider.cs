/******************************************************************************
 * 
 * Announce: CSharpKit, used to meteorological data visualization.
 *           Copyright (C) ShenYongchen.
 *           All rights reserved.
 *   Author: 申永辰.郑州 (shenyczz@163.com)
 *  WebSite: http://github.com/shenyczz/meteoToolkit
 *
 * THIS CODE IS LICENSED UNDER THE MIT LICENSE (MIT).
 * THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF 
 * ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
 * IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
 * PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
 * 
******************************************************************************/

using CSharpKit.Data;

namespace CSharpKit.Data.Esri
{
    public class EsriFileProvider : Provider
    {
        public EsriFileProvider(string connectionString)
            : base(connectionString, null) { }



        protected override IDataInstance GetDataInstance()
        {
            // return null;
            return new ShapeFile(this);
        }

        //}}}@@@
    }




}
