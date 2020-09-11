using System;
using System.IO;
using System.Text;
using CSharpKit.IO.Compression.BZIP2;

namespace CSharpKit.IO.Compression
{
    /// <summary>
    /// An example class to demonstrate compression and decompression of BZip2 streams.
    /// </summary>
    public static class BZip2
	{
		public static void Decompress(Stream inStream, out byte[] outraws)
		{
			outraws = default;

			try
			{
				var vtemp = outraws;

				using (BZip2InputStream bzipInput = new BZip2InputStream(inStream))
				{
					var ms = new MemoryStream();
					bzipInput.IsStreamOwner = false;
					Core.StreamUtils.Copy(bzipInput, ms, new byte[4096]);

					ms.Seek(0, SeekOrigin.Begin);
					vtemp = new byte[ms.Length];
					ms.Read(vtemp, 0, (int)ms.Length);
					ms.Close();
				}

				outraws = vtemp;
            }
			catch (Exception ex)
			{
				throw ex;
			}

			return;
		}

		public static void Decompress(Stream inStream, out MemoryStream outStream)
		{
			outStream = default;

            try
            {
				var vtemp = outStream;

				using (BZip2InputStream bzipInput = new BZip2InputStream(inStream))
                {
					vtemp = new MemoryStream();
					bzipInput.IsStreamOwner = false;
					Core.StreamUtils.Copy(bzipInput, vtemp, new byte[4096]);
				}

				outStream = vtemp;
			}
			catch (Exception ex)
            {
                throw ex;
            }

			return;
		}

		/// <summary>
		/// Decompress the <paramref name="inStream">input</paramref> writing
		/// uncompressed data to the <paramref name="outStream">output stream</paramref>
		/// </summary>
		/// <param name="inStream">The readable stream containing data to decompress.</param>
		/// <param name="outStream">The output stream to receive the decompressed data.</param>
		/// <param name="isStreamOwner">Both streams are closed on completion if true.</param>
		public static void Decompress(Stream inStream, Stream outStream, bool isStreamOwner)
		{
			if (inStream == null)
				throw new ArgumentNullException(nameof(inStream));

			if (outStream == null)
				throw new ArgumentNullException(nameof(outStream));

			try
			{
				using (BZip2InputStream bzipInput = new BZip2InputStream(inStream))
				{
					bzipInput.IsStreamOwner = isStreamOwner;
					Core.StreamUtils.Copy(bzipInput, outStream, new byte[4096]);
				}
			}
			finally
			{
				if (isStreamOwner)
				{
					// inStream is closed by the BZip2InputStream if stream owner
					outStream.Dispose();
				}
			}
		}

		/// <summary>
		/// Compress the <paramref name="inStream">input stream</paramref> sending
		/// result data to <paramref name="outStream">output stream</paramref>
		/// </summary>
		/// <param name="inStream">The readable stream to compress.</param>
		/// <param name="outStream">The output stream to receive the compressed data.</param>
		/// <param name="isStreamOwner">Both streams are closed on completion if true.</param>
		/// <param name="level">Block size acts as compression level (1 to 9) with 1 giving
		/// the lowest compression and 9 the highest.</param>
		public static void Compress(Stream inStream, Stream outStream, bool isStreamOwner, int level)
		{
			if (inStream == null)
				throw new ArgumentNullException(nameof(inStream));

			if (outStream == null)
				throw new ArgumentNullException(nameof(outStream));

			try
			{
				using (BZip2OutputStream bzipOutput = new BZip2OutputStream(outStream, level))
				{
					bzipOutput.IsStreamOwner = isStreamOwner;
					Core.StreamUtils.Copy(inStream, bzipOutput, new byte[4096]);
				}
			}
			finally
			{
				if (isStreamOwner)
				{
					// outStream is closed by the BZip2OutputStream if stream owner
					inStream.Dispose();
				}
			}
		}

		// TEST
		public static byte[] Zip(byte[] data)
		{
			MemoryStream mstream = new MemoryStream();
			BZip2OutputStream zipOutStream = new BZip2OutputStream(mstream);
			zipOutStream.Write(data, 0, data.Length);
			zipOutStream.Close();
			byte[] result = mstream.ToArray();
			mstream.Close();
			return result;
		}

		public static byte[] Unzip(byte[] data)
		{
			MemoryStream mstream = new MemoryStream(data);
			BZip2InputStream zipInputStream = new BZip2InputStream(mstream);
			StreamReader readstream = new StreamReader(zipInputStream, Encoding.Default);
			string unzipdata = readstream.ReadToEnd();

			zipInputStream.Close();
			mstream.Close();
			return Encoding.Default.GetBytes(unzipdata);
		}



	}

}
