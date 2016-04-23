/**
 * Copyright (c) 2012-2013 Sakura-Zen soft All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in the
 *    documentation and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE AUTHOR(S) ``AS IS'' AND ANY EXPRESS OR
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 * IN NO EVENT SHALL THE AUTHOR(S) BE LIABLE FOR ANY DIRECT, INDIRECT,
 * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
using System;
using System.IO;
using PcmLib;

namespace PcmLib
{
	/// <summary>
	/// このクラスのインスタンスはスレッドセーフではない。外部で排他して使用すること。
	/// </summary>
	class RiffWriter : IDisposable
	{
		/// <summary>出力ファイルパス</summary>
		private readonly string filePath;
		/// <summary>ストリーム書き出し用のファイルストリーム</summary>
		private FileStream streamFs;
		/// <summary>ストリーム書き出し用のBinaryWriter</summary>
		private BinaryWriter streamBw;
		/// <summary>書き出したストリームのバイトサイズ</summary>
		private UInt32 streamByteLength;

		/// <summary>解析対象ファイルパス</summary>
		public string FilePath
		{
			get { return filePath; }
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="path">ファイルを書き出すパス</param>
		/// <exception cref="System.ArgumentNullException">引数がnull</exception>
		/// <exception cref="System.IO.IOException">入出力エラー</exception>
		/// <exception cref="System.Security.SecurityException">セキュリティエラー</exception>
		/// <exception cref="System.IO.DirectoryNotFoundException">ディレクトリが存在しない</exception>
		/// <exception cref="System.IO.PathTooLongException">パスが長すぎる</exception>
		/// <exception cref="System.System.UnauthorizedAccessException">アクセス制限エラー</exception>
		public RiffWriter(string path)
		{
			if (path == null) {
				throw new ArgumentNullException();
			}
			filePath = path;
			streamFs = null;
			streamBw = null;
			streamByteLength = 0;

			try {
				streamFs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read);
				streamBw = new BinaryWriter(streamFs);
			} catch {
				Close();
				throw;
			}
		}
		/// <summary>
		/// ファイルをクローズします。このメソッドはDisposeと等価です。
		/// </summary>
		public void Close()
		{
			Dispose();
		}
		/// <summary>
        /// RIFF-WAVヘッダーを書き込みます。
        /// </summary>
        /// <param name="waveFormat">出力するWAVEFORMATヘッダー</param>
        /// <returns>書き出しに成功すれば真</returns>
		public bool WriteHeader(WaveFormatEx waveFormat)
		{
			ThrowExceptionIfDisposed();

			try {
				streamBw.Write(new byte[] { (byte)'R', (byte)'I', (byte)'F', (byte)'F' });
				streamBw.Write((uint)0);
				streamBw.Write(new byte[] { (byte)'W', (byte)'A', (byte)'V', (byte)'E', (byte)'f', (byte)'m', (byte)'t', (byte)' ' });
				streamBw.Write((uint)0x10);
				streamBw.Write((ushort)waveFormat.FormatTag);
				streamBw.Write(waveFormat.Channels);
				streamBw.Write((uint)waveFormat.SamplesPerSecond);
				streamBw.Write((uint)waveFormat.AverageBytesPerSecond);
				streamBw.Write((ushort)waveFormat.BlockAlign);
				streamBw.Write((ushort)waveFormat.BitsPerSample);
				streamBw.Write(new byte[] { (byte)'d', (byte)'a', (byte)'t', (byte)'a' });
				streamBw.Write((uint)0);
			} catch (IOException) {
				return false;
			}
			return true;
		}
		/// <summary>
		/// PCMのストリームを書き出します。
		/// </summary>
		/// <param name="b">書き出すPCMストリームのバイト列</param>
		/// <returns>書き出しに成功すれば真</returns>
		public bool WriteStream8(byte[] b)
		{
			ThrowExceptionIfDisposed();

			if (b == null) {
				return false;
			}

			try {
				streamBw.Write(b);
			} catch (IOException) {
				return false;
			}
			streamByteLength += (UInt32)b.Length;
			return true;
		}
		/// <summary>
		/// PCMのストリームを書き出します。
		/// </summary>
		/// <param name="arr">書き出すPCMストリームのバイト列</param>
		/// <returns>書き出しに成功すれば真</returns>
		public bool WriteStream16(short[] arr)
		{
			ThrowExceptionIfDisposed();

			if (arr == null) {
				return false;
			}
			return WriteStream8(ToByteArray(arr));
		}
		/// <summary>
		/// PCMのストリームを書き出します。
		/// </summary>
		/// <param name="arr">書き出すPCMストリームのバイト列</param>
		/// <returns>書き出しに成功すれば真</returns>
		public bool WriteStream24(int[] arr)
		{
			return false;
		}
		/// <summary>
		/// PCMのストリームを書き出します。
		/// </summary>
		/// <param name="arr">書き出すPCMストリームのバイト列</param>
		/// <returns>書き出しに成功すれば真</returns>
		public bool WriteStream32(int[] arr)
		{
			ThrowExceptionIfDisposed();

			if (arr == null) {
				return false;
			}
			return WriteStream8(ToByteArray(arr));
		}
		/// <summary>
		/// PCMのストリームを書き出します。
		/// </summary>
		/// <param name="arr">書き出すPCMストリームのバイト列</param>
		/// <returns>書き出しに成功すれば真</returns>
		public bool WriteStream32F(float[] arr)
		{
			ThrowExceptionIfDisposed();

			if (arr == null) {
				return false;
			}
			return WriteStream8(ToByteArray(arr));
		}
		/// <summary>
		/// ストリームの書き出しをこれ以上行わないことを通知します。
		/// このメソッドによりヘッダーに書き出すストリームサイズが確定し、実際にヘッダーへ書き出します。
		/// </summary>
		/// <returns>書き出しが正常終了すれば真</returns>
		public bool WriteFinalize()
		{
			ThrowExceptionIfDisposed();

			try {
				streamFs.Seek(4, SeekOrigin.Begin);
				streamBw.Write((UInt32)(streamByteLength + 0x24));
				streamFs.Seek(0x28, SeekOrigin.Begin);
				streamBw.Write((UInt32)streamByteLength);
			} catch (IOException) {
				return false;
			}
			return true;
		}

		#region Dispose Finalize Pattern

		/// <summary>
		/// 既にDisposeメソッドが呼び出されているかどうかを表します。
		/// </summary>
		private bool disposed;

		/// <summary>
		/// このクラスのインスタンスによって使用されているすべてのリソースを解放します。
		/// </summary>
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}

		/// <summary>
		/// このクラスのインスタンスがGCに回収される時に呼び出されます。
		/// </summary>
		~RiffWriter()
		{
			this.Dispose(false);
		}

		/// <summary>
		/// RiffWriter によって使用されているアンマネージ リソースを解放し、オプションでマネージ リソースも解放します。
		/// </summary>
		/// <param name="disposing">マネージ リソースとアンマネージ リソースの両方を解放する場合は true。アンマネージ リソースだけを解放する場合は false。 </param>
		protected virtual void Dispose(bool disposing)
		{
			if (this.disposed) {
				return;
			}
			this.disposed = true;

			if (disposing) {
				// マネージ リソースの解放処理をこの位置に記述します。
				if (streamFs != null) {
					streamFs.Close();
					streamFs = null;
				}
				if (streamBw != null) {
					streamBw.Close();
					streamBw = null;
				}
			}
			// アンマネージ リソースの解放処理をこの位置に記述します。
		}

		/// <summary>
		/// 既にDisposeメソッドが呼び出されている場合、例外をスローします。
		/// </summary>
		/// <exception cref="System.ObjectDisposedException">既にDisposeメソッドが呼び出されています。</exception>
		protected void ThrowExceptionIfDisposed()
		{
			if (this.disposed) {
				throw new ObjectDisposedException(this.GetType().FullName);
			}
		}

		/// <summary>
		/// Dispose Finalize パターンに必要な初期化処理を行います。
		/// </summary>
		private void InitializeDisposeFinalizePattern()
		{
			this.disposed = false;
		}

		#endregion

		#region Object派生

		/// <summary>
		/// ハッシュコードを取得します。オープンしているファイルに基づくハッシュコードです。
		/// </summary>
		/// <returns>ハッシュ値</returns>
		public override int GetHashCode()
		{
			return filePath.GetHashCode();
		}
		/// <summary>
		/// 文字列表現を取得します。
		/// </summary>
		/// <returns>文字列表現</returns>
		public override string ToString()
		{
			return filePath;
		}

		#endregion

		#region unsafeコード

		/// <summary>
		/// 16bit整数配列をバイト配列に変換します。
		/// </summary>
		/// <param name="src">変換するshortの配列</param>
		/// <returns>変換したバイト配列</returns>
		unsafe private byte[] ToByteArray(short[] src)
		{
			byte[] dest = new byte[src.Length * 2];

			fixed (byte* pDest = &dest[0]) {
				fixed (short* pSrc = &src[0]) {
					CopyMemory(pDest, pSrc, (uint)src.Length * 2);
				}
			}
			return dest;
		}
		/// <summary>
		/// 32bit整数配列をバイト配列に変換します。
		/// </summary>
		/// <param name="src">変換するintの配列</param>
		/// <returns>変換したバイト配列</returns>
		unsafe private byte[] ToByteArray(int[] src)
		{
			byte[] dest = new byte[src.Length * 4];

			fixed (byte* pDest = &dest[0]) {
				fixed (int* pSrc = &src[0]) {
					CopyMemory(pDest, pSrc, (uint)src.Length * 4);
				}
			}
			return dest;
		}
		/// <summary>
		/// float配列をバイト配列に変換します。
		/// </summary>
		/// <param name="src">変換するfloatの配列</param>
		/// <returns>変換したバイト配列</returns>
		unsafe private byte[] ToByteArray(float[] src)
		{
			byte[] dest = new byte[src.Length * 4];

			fixed (byte* pDest = &dest[0]) {
				fixed (float* pSrc = &src[0]) {
					CopyMemory(pDest, pSrc, (uint)src.Length*4);
				}
			}
			return dest;
		}
		/// <summary>
		/// 配列間のデータの直接コピー。Cのmemcpy相当。
		/// </summary>
		/// <param name="outDest">出力配列のポインタ</param>
		/// <param name="inSrc">入力配列のポインタ</param>
		/// <param name="inNumOfBytes">コピーするバイトサイズ</param>
		unsafe private void CopyMemory(void* outDest, void* inSrc, uint inNumOfBytes)
		{
			// 転送先をuint幅にalignする
			const uint align = sizeof(uint) - 1;
			uint offset = (uint)outDest & align;
			// ↑ポインタは32bitとは限らないので本来このキャストはuintではダメだが、
			// 今は下位2bitだけあればいいのでこれでOK。
			if (offset != 0) {
				offset = align - offset;
			}
			offset = System.Math.Min(offset, inNumOfBytes);

			// 先頭の余り部分をbyteでちまちまコピー
			byte* srcBytes = (byte*)inSrc;
			byte* dstBytes = (byte*)outDest;
			for (uint i = 0; i < offset; i++) {
				dstBytes[i] = srcBytes[i];
			}

			// uintで一気に転送
			uint* dst = (uint*)((byte*)outDest + offset);
			uint* src = (uint*)((byte*)inSrc + offset);
			uint numOfUInt = (inNumOfBytes - offset) / sizeof(uint);
			for (uint i = 0; i < numOfUInt; i++) {
				dst[i] = src[i];
			}

			// 末尾の余り部分をbyteでちまちまコピー
			for (uint i = offset + numOfUInt * sizeof(uint); i < inNumOfBytes; i++) {
				dstBytes[i] = srcBytes[i];
			}
		}

		#endregion
	}
}
