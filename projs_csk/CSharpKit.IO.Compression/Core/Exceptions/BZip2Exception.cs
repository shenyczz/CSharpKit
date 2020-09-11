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
using System.Runtime.Serialization;

namespace CSharpKit.IO.Compression
{
    [Serializable]
	public class BZip2Exception : CompressionException
	{
		public BZip2Exception()
		{
		}

		public BZip2Exception(string message)
			: base(message) { }

		public BZip2Exception(string message, Exception innerException)
			: base(message, innerException) { }

		protected BZip2Exception(SerializationInfo info, StreamingContext context)
			: base(info, context) { }

		//}}@@@
	}


}
