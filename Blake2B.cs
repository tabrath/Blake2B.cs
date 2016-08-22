﻿/*	Blake2B.cs source code package - C# implementation

	Written in 2012 by Samuel Neves <sneves@dei.uc.pt>
	Written in 2012 by Christian Winnerlein <codesinchaos@gmail.com>
	Written in 2016 by Uli Riehm <metadings@live.de>

	To the extent possible under law, the author(s) have dedicated all copyright
	and related and neighboring rights to this software to the public domain
	worldwide. This software is distributed without any warranty.

	You should have received a copy of the CC0 Public Domain Dedication along with
	this software. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.
*/
using System;
using System.Security.Cryptography;

namespace Crypto
{
	public partial class Blake2B : HashAlgorithm
	{
		private ulong[] rawConfig;

		private byte[] _Personalization;

		public byte[] Personalization 
		{ 
			get { return _Personalization; }
			set { 
				_Personalization = value; 
				HashClear();
			}
		}

		private byte[] _Salt;

		public byte[] Salt 
		{ 
			get { return _Salt; }
			set { 
				_Salt = value; 
				HashClear();
			}
		}

		private byte[] _Key;

		public byte[] Key
		{ 
			get { return _Key; }
			set { 
				_Key = value; 
				HashClear();
			}
		}

		private uint _IntermediateHashSize;

		public uint IntermediateHashSize
		{ 
			get { return _IntermediateHashSize; }
			set { 
				_IntermediateHashSize = value; 
				HashClear();
			}
		}

		private uint _MaxHeight;

		public uint MaxHeight
		{ 
			get { return _MaxHeight; }
			set { 
				_MaxHeight = value; 
				HashClear();
			}
		}

		private ulong _LeafSize;

		public ulong LeafSize
		{ 
			get { return _LeafSize; }
			set { 
				_LeafSize = value; 
				HashClear();
			}
		}

		private uint _FanOut;

		public uint FanOut
		{ 
			get { return _FanOut; }
			set { 
				_FanOut = value; 
				HashClear();
			}
		}

		private int _hashSizeInBytes;

		public int HashSizeInBytes { get { return _hashSizeInBytes; } }

		public override int HashSize { get { return HashSizeInBytes * 8; } }

		public override byte[] Hash 
		{
			get {
				// if (m_bDisposed) throw new ObjectDisposedException(null);
				// if (State != 0) throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_HashNotYetFinalized"));
				var result = new byte[HashSizeInBytes];
				for (int i = 0; i < 8; ++i) UInt64ToBytes(state[i], result, i << 3);
				return result;
			}
		}

		public Blake2B() : this(64) { }

		public Blake2B(int hashSizeInBytes)
		{	
			if (hashSizeInBytes <= 0 || hashSizeInBytes > 64)
				throw new ArgumentOutOfRangeException("hashSizeInBytes");
			if (hashSizeInBytes % 8 != 0)
				throw new ArgumentOutOfRangeException("hashSizeInBytes", "must be a multiple of 8");
			
			_hashSizeInBytes = hashSizeInBytes;

			_FanOut = 1;
			_MaxHeight = 1;
			// _LeafSize = 0;
			// _IntermediateHashSize = 0;
		}

		/* 
		#region Consts
		private static readonly ulong[] c64init384 =
		{
			0xCBBB9D5DC1059ED8UL, 0x629A292A367CD507UL, 0x9159015A3070DD17UL, 0x152FECD8F70E5939UL,
			0x67332667FFC00B31UL, 0x8EB44A8768581511UL, 0xDB0C2E0D64F98FA7UL, 0x47B5481DBEFA4FA4UL
		};

		private static readonly ulong[] c64init512 =
		{
			0x6A09E667F3BCC908UL, 0xBB67AE8584CAA73BUL, 0x3C6EF372FE94F82BUL, 0xA54FF53A5F1D36F1UL,
			0x510E527FADE682D1UL, 0x9B05688C2B3E6C1FUL, 0x1F83D9ABFB41BD6BUL, 0x5BE0CD19137E2179UL
		};
		#endregion
		/**/

		private bool isInitialized = false;

		private int bufferFilled;
		private byte[] buffer = new byte[BLAKE2B_BLOCKBYTES];
		private ulong[] state = new ulong[8];
		private ulong[] m = new ulong[16];
		private ulong counter0;
		private ulong counter1;
		private ulong f0;
		private ulong f1;

		public const int ROUNDS = 12;

		// enum blake2b_constant's
		public const int BLAKE2B_BLOCKBYTES = 128;
		public const int BLAKE2B_OUTBYTES = 64;
		public const int BLAKE2B_KEYBYTES = 64;
		public const int BLAKE2B_SALTBYTES = 16;
		public const int BLAKE2B_PERSONALBYTES = 16;

		public const ulong IV0 = 0x6A09E667F3BCC908UL;
		public const ulong IV1 = 0xBB67AE8584CAA73BUL;
		public const ulong IV2 = 0x3C6EF372FE94F82BUL;
		public const ulong IV3 = 0xA54FF53A5F1D36F1UL;
		public const ulong IV4 = 0x510E527FADE682D1UL;
		public const ulong IV5 = 0x9B05688C2B3E6C1FUL;
		public const ulong IV6 = 0x1F83D9ABFB41BD6BUL;
		public const ulong IV7 = 0x5BE0CD19137E2179UL;

		public static readonly int[] Sigma = new int[ROUNDS * 16] {
			0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15,
			14, 10, 4, 8, 9, 15, 13, 6, 1, 12, 0, 2, 11, 7, 5, 3,
			11, 8, 12, 0, 5, 2, 15, 13, 10, 14, 3, 6, 7, 1, 9, 4,
			7, 9, 3, 1, 13, 12, 11, 14, 2, 6, 5, 10, 4, 0, 15, 8,
			9, 0, 5, 7, 2, 4, 10, 15, 14, 1, 11, 12, 6, 8, 3, 13,
			2, 12, 6, 10, 0, 11, 8, 3, 4, 13, 7, 5, 15, 14, 1, 9,
			12, 5, 1, 15, 14, 13, 4, 10, 0, 7, 6, 3, 9, 2, 8, 11,
			13, 11, 7, 14, 12, 1, 3, 9, 5, 0, 15, 4, 8, 6, 2, 10,
			6, 15, 14, 9, 11, 3, 0, 8, 12, 2, 13, 7, 1, 4, 10, 5,
			10, 2, 8, 4, 7, 6, 1, 5, 15, 11, 9, 14, 3, 12, 13, 0,
			0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15,
			14, 10, 4, 8, 9, 15, 13, 6, 1, 12, 0, 2, 11, 7, 5, 3
		};

		public static ulong BytesToUInt64(byte[] buf, int offset)
		{
			return
				((ulong)buf[offset + 7] << 7 * 8 |
				((ulong)buf[offset + 6] << 6 * 8) |
				((ulong)buf[offset + 5] << 5 * 8) |
				((ulong)buf[offset + 4] << 4 * 8) |
				((ulong)buf[offset + 3] << 3 * 8) |
				((ulong)buf[offset + 2] << 2 * 8) |
				((ulong)buf[offset + 1] << 1 * 8) |
				((ulong)buf[offset]));
		}

		public static void UInt64ToBytes(ulong value, byte[] buf, int offset)
		{
			buf[offset + 7] = (byte)(value >> 7 * 8);
			buf[offset + 6] = (byte)(value >> 6 * 8);
			buf[offset + 5] = (byte)(value >> 5 * 8);
			buf[offset + 4] = (byte)(value >> 4 * 8);
			buf[offset + 3] = (byte)(value >> 3 * 8);
			buf[offset + 2] = (byte)(value >> 2 * 8);
			buf[offset + 1] = (byte)(value >> 1 * 8);
			buf[offset] = (byte)value;
		}

		public virtual ulong[] Prepare()
		{
			var rawConfig = new ulong[8];

			// digest length
			rawConfig[0] |= (ulong)(uint)HashSizeInBytes;

			// Key length
			if (Key != null)
			{
				if (Key.Length > 64)
					throw new ArgumentException("Key", "Key too long");

				rawConfig[0] |= (ulong)((uint)Key.Length << 8);
			}

			if (IntermediateHashSize > 64)
				throw new ArgumentOutOfRangeException("IntermediateHashSize");

			// bool isSequential = TreeConfig == null;
			// FanOut
			rawConfig[0] |= FanOut << 16;
			// Depth
			rawConfig[0] |= MaxHeight << 24;
			// Leaf length
			rawConfig[0] |= LeafSize << 32;
			// Inner length
			rawConfig[2] |= IntermediateHashSize << 8;

			// Salt
			if (Salt != null)
			{
				if (Salt.Length != 16)
					throw new ArgumentException("Salt has invalid length");

				rawConfig[4] = BytesToUInt64(Salt, 0);
				rawConfig[5] = BytesToUInt64(Salt, 8);
			}
			// Personalization
			if (Personalization != null)
			{
				if (Personalization.Length != 16)
					throw new ArgumentException("Personalization has invalid length");

				rawConfig[6] = BytesToUInt64(Personalization, 0);
				rawConfig[7] = BytesToUInt64(Personalization, 8);
			}

			return rawConfig;
		}

		public override void Initialize()
		{
			if (rawConfig == null)
			{
				rawConfig = Prepare();
			}
			Initialize(rawConfig);
		}

		/* public static void ConfigBSetNode(ulong[] rawConfig, byte depth, ulong nodeOffset)
		{
			rawConfig[1] = nodeOffset;
			rawConfig[2] = (rawConfig[2] & ~0xFFul) | depth;
		} */

		public virtual void Initialize(ulong[] config)
		{
			if (config == null)
				throw new ArgumentNullException("config");
			if (config.Length != 8)
				throw new ArgumentException("config length must be 8 words");

			HashClear();

			state[0] = IV0;
			state[1] = IV1;
			state[2] = IV2;
			state[3] = IV3;
			state[4] = IV4;
			state[5] = IV5;
			state[6] = IV6;
			state[7] = IV7;

			for (int i = 0; i < 8; i++) state[i] ^= config[i];

			isInitialized = true;

			if (Key != null) HashCore(Key, 0, Key.Length);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing) HashClear();
			base.Dispose(disposing);
		}

		public virtual void HashClear()
		{
			int i;
			isInitialized = false;

			counter0 = 0UL;
			counter1 = 0UL;
			f0 = 0UL;
			f1 = 0UL;

			for (i = 0; i < buffer.Length; ++i) buffer[i] = 0x00;
			bufferFilled = 0;

			for (i = 0; i < 8; ++i) state[i] = 0UL;

			for (i = 0; i < 16; ++i) m[i] = 0UL;

			if (Personalization != null)
			{
				Array.Clear(Personalization, 0, Personalization.Length);
				Personalization = null;
			}
			if (Salt != null)
			{
				Array.Clear(Salt, 0, Salt.Length);
				Salt = null;
			}
			if (Key != null)
			{
				Array.Clear(Key, 0, Key.Length);
				Key = null;
			}
		}

		void IncrementCounter( ulong inc )
		{
			counter0 += inc;
			if (counter0 == 0) ++counter1;
		}

		partial void Compress(byte[] block, int start);

		protected override void HashCore(byte[] array, int start, int count)
		{
			Core(array, start, count);
		}

		public virtual void Core(byte[] array, int start, int count)
		{
			if (array == null)
				throw new ArgumentNullException("array");
			if (start < 0)
				throw new ArgumentOutOfRangeException("start");
			if (count < 0)
				throw new ArgumentOutOfRangeException("count");
			if (start + count > array.Length)
				throw new ArgumentOutOfRangeException("start + count");

			if (!isInitialized) Initialize();

			int bytesDone = 0, bytesToFill;
			int offset = start;
			do
			{
				bytesToFill = Math.Min(count - offset, BLAKE2B_BLOCKBYTES);
				Buffer.BlockCopy(array, offset, buffer, bufferFilled, bytesToFill);

				bytesDone += bytesToFill;
				bufferFilled += bytesToFill;
				offset += bytesToFill;

				if (bufferFilled == BLAKE2B_BLOCKBYTES)
				{
					IncrementCounter((ulong)BLAKE2B_BLOCKBYTES);
					Compress(buffer, 0);

					start += BLAKE2B_BLOCKBYTES;
					count -= BLAKE2B_BLOCKBYTES;
					bufferFilled = 0;
				}

			} while (bytesDone < count && offset < array.Length);
		}

		protected override byte[] HashFinal()
		{
			return Final();
		}

		public virtual byte[] Final()
		{
			return Final(false);
		}

		public virtual byte[] Final(bool isEndOfLayer)
		{
			var result = new byte[HashSizeInBytes];
			Final(result, isEndOfLayer);
			return result;
		}

		public virtual void Final(byte[] hash)
		{
			Final(hash, false);
		}

		public virtual void Final(byte[] hash, bool isEndOfLayer)
		{
			if (hash.Length != HashSizeInBytes)
				throw new ArgumentOutOfRangeException("_hash", "length must be HashSizeInBytes");

			if (!isInitialized) Initialize();

			// Last compression
			IncrementCounter((ulong)bufferFilled);

			f0 = ulong.MaxValue;
			if (isEndOfLayer) f1 = ulong.MaxValue;

			for (int i = bufferFilled; i < BLAKE2B_BLOCKBYTES; ++i) buffer[i] = 0x00;
			Compress(buffer, 0);

			// Output
			for (int i = 0; i < 8; ++i) UInt64ToBytes(state[i], hash, i << 3);

			isInitialized = false;
		}

		public virtual void Compute(byte[] value, byte[] sourceCode)
		{
			Core(sourceCode, 0, sourceCode.Length);
			Final(value);
		}

		public virtual byte[] Compute(byte[] sourceCode)
		{
			Core(sourceCode, 0, sourceCode.Length);
			return Final();
		}
	}
}
