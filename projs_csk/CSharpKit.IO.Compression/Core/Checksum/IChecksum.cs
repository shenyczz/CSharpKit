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

namespace CSharpKit.IO.Compression.Core
{
	/// <summary>
	/// 检验和(checksum)<br/>
	/// 在数据处理和数据通信领域中，用于校验目的地一组数据项的和。
	/// 它通常是以十六进制为数制表示的形式。如果校验和的数值超过十六进制的FF，也就是255. 
	/// 就要求其补码作为校验和。通常用来在通信中，尤其是远距离通信中保证数据的完整性和准确性。
	/// </summary>
	public interface IChecksum
	{
		/// <summary>
		/// Returns the data checksum computed so far.
		/// </summary>
		long Value { get; }

		/// <summary>
		/// Resets the data checksum as if no update was ever called.
		/// </summary>
		void Reset();

		/// <summary>
		/// Adds one byte to the data checksum.
		/// </summary>
		/// <param name = "bval">
		/// the data value to add. The high byte of the int is ignored.
		/// </param>
		void Update(int bval);

		/// <summary>
		/// Updates the data checksum with the bytes taken from the array.
		/// </summary>
		/// <param name="buffer">
		/// buffer an array of bytes
		/// </param>
		void Update(byte[] buffer);

		/// <summary>
		/// Adds the byte array to the data checksum.
		/// </summary>
		/// <param name = "segment">
		/// The chunk of data to add
		/// </param>
		void Update(ArraySegment<byte> segment);

		//}}@@@
	}


}
