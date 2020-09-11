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
    /// <summary>
    /// All library exceptions are derived from this.
    /// </summary>
    [Serializable]
	public class CompressionException : Exception
	{
		public CompressionException() { }

		public CompressionException(string message)
			: base(message) { }

		public CompressionException(string message, Exception innerException)
			: base(message, innerException) { }

		protected CompressionException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }

		//}}@@@
	}

}
