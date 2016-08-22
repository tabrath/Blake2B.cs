﻿/*	BLAKE2 reference source code package - C# implementation

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

namespace Crypto
{
#if !INLINE && !SIMPLE
	public partial class Blake2B
	{
		partial void Compress(byte[] block, int start)
		{
			if (BitConverter.IsLittleEndian)
			{
				Buffer.BlockCopy(block, start, m, 0, BLAKE2B_BLOCKBYTES);
			}
			else
			{
				for (int i = 0; i < (BLAKE2B_BLOCKBYTES / 8); ++i)
					m[i] = BytesToUInt64(block, start + (i << 3));
			}

			ulong m0 = m[0];
			ulong m1 = m[1];
			ulong m2 = m[2];
			ulong m3 = m[3];
			ulong m4 = m[4];
			ulong m5 = m[5];
			ulong m6 = m[6];
			ulong m7 = m[7];
			ulong m8 = m[8];
			ulong m9 = m[9];
			ulong m10 = m[10];
			ulong m11 = m[11];
			ulong m12 = m[12];
			ulong m13 = m[13];
			ulong m14 = m[14];
			ulong m15 = m[15];

			ulong v0 = state[0];
			ulong v1 = state[1];
			ulong v2 = state[2];
			ulong v3 = state[3];
			ulong v4 = state[4];
			ulong v5 = state[5];
			ulong v6 = state[6];
			ulong v7 = state[7];
			ulong v8 = IV0;
			ulong v9 = IV1;
			ulong v10 = IV2;
			ulong v11 = IV3;
			ulong v12 = IV4 ^ counter0;
			ulong v13 = IV5 ^ counter1;
			ulong v14 = IV6 ^ f0;
			ulong v15 = IV7 ^ f1;

			// Rounds

			// ##### Round(0) #####
			// G(0, 0, v0, v4, v8, v12)
			v0 = v0 + v4 + m0;
			v12 ^= v0;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v0 = v0 + v4 + m1;
			v12 ^= v0;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));

			// G(0, 1, v1, v5, v9, v13)
			v1 = v1 + v5 + m2;
			v13 ^= v1;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v1 = v1 + v5 + m3;
			v13 ^= v1;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(0, 2, v2, v6, v10, v14)
			v2 = v2 + v6 + m4;
			v14 ^= v2;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v2 = v2 + v6 + m5;
			v14 ^= v2;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(0, 3, v3, v7, v11, v15)
			v3 = v3 + v7 + m6;
			v15 ^= v3;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v3 = v3 + v7 + m7;
			v15 ^= v3;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(0, 4, v0, v5, v10, v15)
			v0 = v0 + v5 + m8;
			v15 ^= v0;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v0 = v0 + v5 + m9;
			v15 ^= v0;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(0, 5, v1, v6, v11, v12)
			v1 = v1 + v6 + m10;
			v12 ^= v1;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v1 = v1 + v6 + m11;
			v12 ^= v1;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(0, 6, v2, v7, v8, v13)
			v2 = v2 + v7 + m12;
			v13 ^= v2;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v2 = v2 + v7 + m13;
			v13 ^= v2;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(0, 7, v3, v4, v9, v14)
			v3 = v3 + v4 + m14;
			v14 ^= v3;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v3 = v3 + v4 + m15;
			v14 ^= v3;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));


			// ##### Round(1) #####
			// G(1, 0, v0, v4, v8, v12)
			v0 = v0 + v4 + m14;
			v12 ^= v0;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v0 = v0 + v4 + m10;
			v12 ^= v0;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));

			// G(1, 1, v1, v5, v9, v13)
			v1 = v1 + v5 + m4;
			v13 ^= v1;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v1 = v1 + v5 + m8;
			v13 ^= v1;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(1, 2, v2, v6, v10, v14)
			v2 = v2 + v6 + m9;
			v14 ^= v2;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v2 = v2 + v6 + m15;
			v14 ^= v2;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(1, 3, v3, v7, v11, v15)
			v3 = v3 + v7 + m13;
			v15 ^= v3;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v3 = v3 + v7 + m6;
			v15 ^= v3;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(1, 4, v0, v5, v10, v15)
			v0 = v0 + v5 + m1;
			v15 ^= v0;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v0 = v0 + v5 + m12;
			v15 ^= v0;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(1, 5, v1, v6, v11, v12)
			v1 = v1 + v6 + m0;
			v12 ^= v1;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v1 = v1 + v6 + m2;
			v12 ^= v1;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(1, 6, v2, v7, v8, v13)
			v2 = v2 + v7 + m11;
			v13 ^= v2;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v2 = v2 + v7 + m7;
			v13 ^= v2;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(1, 7, v3, v4, v9, v14)
			v3 = v3 + v4 + m5;
			v14 ^= v3;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v3 = v3 + v4 + m3;
			v14 ^= v3;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));


			// ##### Round(2) #####
			// G(2, 0, v0, v4, v8, v12)
			v0 = v0 + v4 + m11;
			v12 ^= v0;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v0 = v0 + v4 + m8;
			v12 ^= v0;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));

			// G(2, 1, v1, v5, v9, v13)
			v1 = v1 + v5 + m12;
			v13 ^= v1;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v1 = v1 + v5 + m0;
			v13 ^= v1;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(2, 2, v2, v6, v10, v14)
			v2 = v2 + v6 + m5;
			v14 ^= v2;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v2 = v2 + v6 + m2;
			v14 ^= v2;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(2, 3, v3, v7, v11, v15)
			v3 = v3 + v7 + m15;
			v15 ^= v3;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v3 = v3 + v7 + m13;
			v15 ^= v3;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(2, 4, v0, v5, v10, v15)
			v0 = v0 + v5 + m10;
			v15 ^= v0;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v0 = v0 + v5 + m14;
			v15 ^= v0;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(2, 5, v1, v6, v11, v12)
			v1 = v1 + v6 + m3;
			v12 ^= v1;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v1 = v1 + v6 + m6;
			v12 ^= v1;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(2, 6, v2, v7, v8, v13)
			v2 = v2 + v7 + m7;
			v13 ^= v2;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v2 = v2 + v7 + m1;
			v13 ^= v2;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(2, 7, v3, v4, v9, v14)
			v3 = v3 + v4 + m9;
			v14 ^= v3;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v3 = v3 + v4 + m4;
			v14 ^= v3;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));


			// ##### Round(3) #####
			// G(3, 0, v0, v4, v8, v12)
			v0 = v0 + v4 + m7;
			v12 ^= v0;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v0 = v0 + v4 + m9;
			v12 ^= v0;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));

			// G(3, 1, v1, v5, v9, v13)
			v1 = v1 + v5 + m3;
			v13 ^= v1;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v1 = v1 + v5 + m1;
			v13 ^= v1;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(3, 2, v2, v6, v10, v14)
			v2 = v2 + v6 + m13;
			v14 ^= v2;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v2 = v2 + v6 + m12;
			v14 ^= v2;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(3, 3, v3, v7, v11, v15)
			v3 = v3 + v7 + m11;
			v15 ^= v3;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v3 = v3 + v7 + m14;
			v15 ^= v3;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(3, 4, v0, v5, v10, v15)
			v0 = v0 + v5 + m2;
			v15 ^= v0;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v0 = v0 + v5 + m6;
			v15 ^= v0;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(3, 5, v1, v6, v11, v12)
			v1 = v1 + v6 + m5;
			v12 ^= v1;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v1 = v1 + v6 + m10;
			v12 ^= v1;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(3, 6, v2, v7, v8, v13)
			v2 = v2 + v7 + m4;
			v13 ^= v2;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v2 = v2 + v7 + m0;
			v13 ^= v2;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(3, 7, v3, v4, v9, v14)
			v3 = v3 + v4 + m15;
			v14 ^= v3;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v3 = v3 + v4 + m8;
			v14 ^= v3;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));


			// ##### Round(4) #####
			// G(4, 0, v0, v4, v8, v12)
			v0 = v0 + v4 + m9;
			v12 ^= v0;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v0 = v0 + v4 + m0;
			v12 ^= v0;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));

			// G(4, 1, v1, v5, v9, v13)
			v1 = v1 + v5 + m5;
			v13 ^= v1;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v1 = v1 + v5 + m7;
			v13 ^= v1;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(4, 2, v2, v6, v10, v14)
			v2 = v2 + v6 + m2;
			v14 ^= v2;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v2 = v2 + v6 + m4;
			v14 ^= v2;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(4, 3, v3, v7, v11, v15)
			v3 = v3 + v7 + m10;
			v15 ^= v3;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v3 = v3 + v7 + m15;
			v15 ^= v3;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(4, 4, v0, v5, v10, v15)
			v0 = v0 + v5 + m14;
			v15 ^= v0;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v0 = v0 + v5 + m1;
			v15 ^= v0;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(4, 5, v1, v6, v11, v12)
			v1 = v1 + v6 + m11;
			v12 ^= v1;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v1 = v1 + v6 + m12;
			v12 ^= v1;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(4, 6, v2, v7, v8, v13)
			v2 = v2 + v7 + m6;
			v13 ^= v2;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v2 = v2 + v7 + m8;
			v13 ^= v2;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(4, 7, v3, v4, v9, v14)
			v3 = v3 + v4 + m3;
			v14 ^= v3;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v3 = v3 + v4 + m13;
			v14 ^= v3;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));


			// ##### Round(5) #####
			// G(5, 0, v0, v4, v8, v12)
			v0 = v0 + v4 + m2;
			v12 ^= v0;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v0 = v0 + v4 + m12;
			v12 ^= v0;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));

			// G(5, 1, v1, v5, v9, v13)
			v1 = v1 + v5 + m6;
			v13 ^= v1;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v1 = v1 + v5 + m10;
			v13 ^= v1;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(5, 2, v2, v6, v10, v14)
			v2 = v2 + v6 + m0;
			v14 ^= v2;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v2 = v2 + v6 + m11;
			v14 ^= v2;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(5, 3, v3, v7, v11, v15)
			v3 = v3 + v7 + m8;
			v15 ^= v3;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v3 = v3 + v7 + m3;
			v15 ^= v3;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(5, 4, v0, v5, v10, v15)
			v0 = v0 + v5 + m4;
			v15 ^= v0;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v0 = v0 + v5 + m13;
			v15 ^= v0;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(5, 5, v1, v6, v11, v12)
			v1 = v1 + v6 + m7;
			v12 ^= v1;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v1 = v1 + v6 + m5;
			v12 ^= v1;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(5, 6, v2, v7, v8, v13)
			v2 = v2 + v7 + m15;
			v13 ^= v2;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v2 = v2 + v7 + m14;
			v13 ^= v2;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(5, 7, v3, v4, v9, v14)
			v3 = v3 + v4 + m1;
			v14 ^= v3;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v3 = v3 + v4 + m9;
			v14 ^= v3;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));


			// ##### Round(6) #####
			// G(6, 0, v0, v4, v8, v12)
			v0 = v0 + v4 + m12;
			v12 ^= v0;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v0 = v0 + v4 + m5;
			v12 ^= v0;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));

			// G(6, 1, v1, v5, v9, v13)
			v1 = v1 + v5 + m1;
			v13 ^= v1;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v1 = v1 + v5 + m15;
			v13 ^= v1;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(6, 2, v2, v6, v10, v14)
			v2 = v2 + v6 + m14;
			v14 ^= v2;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v2 = v2 + v6 + m13;
			v14 ^= v2;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(6, 3, v3, v7, v11, v15)
			v3 = v3 + v7 + m4;
			v15 ^= v3;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v3 = v3 + v7 + m10;
			v15 ^= v3;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(6, 4, v0, v5, v10, v15)
			v0 = v0 + v5 + m0;
			v15 ^= v0;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v0 = v0 + v5 + m7;
			v15 ^= v0;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(6, 5, v1, v6, v11, v12)
			v1 = v1 + v6 + m6;
			v12 ^= v1;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v1 = v1 + v6 + m3;
			v12 ^= v1;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(6, 6, v2, v7, v8, v13)
			v2 = v2 + v7 + m9;
			v13 ^= v2;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v2 = v2 + v7 + m2;
			v13 ^= v2;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(6, 7, v3, v4, v9, v14)
			v3 = v3 + v4 + m8;
			v14 ^= v3;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v3 = v3 + v4 + m11;
			v14 ^= v3;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));


			// ##### Round(7) #####
			// G(7, 0, v0, v4, v8, v12)
			v0 = v0 + v4 + m13;
			v12 ^= v0;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v0 = v0 + v4 + m11;
			v12 ^= v0;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));

			// G(7, 1, v1, v5, v9, v13)
			v1 = v1 + v5 + m7;
			v13 ^= v1;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v1 = v1 + v5 + m14;
			v13 ^= v1;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(7, 2, v2, v6, v10, v14)
			v2 = v2 + v6 + m12;
			v14 ^= v2;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v2 = v2 + v6 + m1;
			v14 ^= v2;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(7, 3, v3, v7, v11, v15)
			v3 = v3 + v7 + m3;
			v15 ^= v3;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v3 = v3 + v7 + m9;
			v15 ^= v3;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(7, 4, v0, v5, v10, v15)
			v0 = v0 + v5 + m5;
			v15 ^= v0;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v0 = v0 + v5 + m0;
			v15 ^= v0;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(7, 5, v1, v6, v11, v12)
			v1 = v1 + v6 + m15;
			v12 ^= v1;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v1 = v1 + v6 + m4;
			v12 ^= v1;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(7, 6, v2, v7, v8, v13)
			v2 = v2 + v7 + m8;
			v13 ^= v2;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v2 = v2 + v7 + m6;
			v13 ^= v2;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(7, 7, v3, v4, v9, v14)
			v3 = v3 + v4 + m2;
			v14 ^= v3;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v3 = v3 + v4 + m10;
			v14 ^= v3;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));


			// ##### Round(8) #####
			// G(8, 0, v0, v4, v8, v12)
			v0 = v0 + v4 + m6;
			v12 ^= v0;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v0 = v0 + v4 + m15;
			v12 ^= v0;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));

			// G(8, 1, v1, v5, v9, v13)
			v1 = v1 + v5 + m14;
			v13 ^= v1;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v1 = v1 + v5 + m9;
			v13 ^= v1;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(8, 2, v2, v6, v10, v14)
			v2 = v2 + v6 + m11;
			v14 ^= v2;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v2 = v2 + v6 + m3;
			v14 ^= v2;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(8, 3, v3, v7, v11, v15)
			v3 = v3 + v7 + m0;
			v15 ^= v3;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v3 = v3 + v7 + m8;
			v15 ^= v3;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(8, 4, v0, v5, v10, v15)
			v0 = v0 + v5 + m12;
			v15 ^= v0;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v0 = v0 + v5 + m2;
			v15 ^= v0;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(8, 5, v1, v6, v11, v12)
			v1 = v1 + v6 + m13;
			v12 ^= v1;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v1 = v1 + v6 + m7;
			v12 ^= v1;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(8, 6, v2, v7, v8, v13)
			v2 = v2 + v7 + m1;
			v13 ^= v2;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v2 = v2 + v7 + m4;
			v13 ^= v2;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(8, 7, v3, v4, v9, v14)
			v3 = v3 + v4 + m10;
			v14 ^= v3;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v3 = v3 + v4 + m5;
			v14 ^= v3;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));


			// ##### Round(9) #####
			// G(9, 0, v0, v4, v8, v12)
			v0 = v0 + v4 + m10;
			v12 ^= v0;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v0 = v0 + v4 + m2;
			v12 ^= v0;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));

			// G(9, 1, v1, v5, v9, v13)
			v1 = v1 + v5 + m8;
			v13 ^= v1;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v1 = v1 + v5 + m4;
			v13 ^= v1;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(9, 2, v2, v6, v10, v14)
			v2 = v2 + v6 + m7;
			v14 ^= v2;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v2 = v2 + v6 + m6;
			v14 ^= v2;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(9, 3, v3, v7, v11, v15)
			v3 = v3 + v7 + m1;
			v15 ^= v3;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v3 = v3 + v7 + m5;
			v15 ^= v3;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(9, 4, v0, v5, v10, v15)
			v0 = v0 + v5 + m15;
			v15 ^= v0;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v0 = v0 + v5 + m11;
			v15 ^= v0;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(9, 5, v1, v6, v11, v12)
			v1 = v1 + v6 + m9;
			v12 ^= v1;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v1 = v1 + v6 + m14;
			v12 ^= v1;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(9, 6, v2, v7, v8, v13)
			v2 = v2 + v7 + m3;
			v13 ^= v2;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v2 = v2 + v7 + m12;
			v13 ^= v2;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(9, 7, v3, v4, v9, v14)
			v3 = v3 + v4 + m13;
			v14 ^= v3;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v3 = v3 + v4 + m0;
			v14 ^= v3;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));


			// ##### Round(10) #####
			// G(10, 0, v0, v4, v8, v12)
			v0 = v0 + v4 + m0;
			v12 ^= v0;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v0 = v0 + v4 + m1;
			v12 ^= v0;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));

			// G(10, 1, v1, v5, v9, v13)
			v1 = v1 + v5 + m2;
			v13 ^= v1;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v1 = v1 + v5 + m3;
			v13 ^= v1;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(10, 2, v2, v6, v10, v14)
			v2 = v2 + v6 + m4;
			v14 ^= v2;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v2 = v2 + v6 + m5;
			v14 ^= v2;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(10, 3, v3, v7, v11, v15)
			v3 = v3 + v7 + m6;
			v15 ^= v3;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v3 = v3 + v7 + m7;
			v15 ^= v3;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(10, 4, v0, v5, v10, v15)
			v0 = v0 + v5 + m8;
			v15 ^= v0;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v0 = v0 + v5 + m9;
			v15 ^= v0;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(10, 5, v1, v6, v11, v12)
			v1 = v1 + v6 + m10;
			v12 ^= v1;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v1 = v1 + v6 + m11;
			v12 ^= v1;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(10, 6, v2, v7, v8, v13)
			v2 = v2 + v7 + m12;
			v13 ^= v2;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v2 = v2 + v7 + m13;
			v13 ^= v2;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(10, 7, v3, v4, v9, v14)
			v3 = v3 + v4 + m14;
			v14 ^= v3;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v3 = v3 + v4 + m15;
			v14 ^= v3;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));


			// ##### Round(11) #####
			// G(11, 0, v0, v4, v8, v12)
			v0 = v0 + v4 + m14;
			v12 ^= v0;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v0 = v0 + v4 + m10;
			v12 ^= v0;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v8 = v8 + v12;
			v4 ^= v8;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));

			// G(11, 1, v1, v5, v9, v13)
			v1 = v1 + v5 + m4;
			v13 ^= v1;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v1 = v1 + v5 + m8;
			v13 ^= v1;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v9 = v9 + v13;
			v5 ^= v9;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(11, 2, v2, v6, v10, v14)
			v2 = v2 + v6 + m9;
			v14 ^= v2;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v2 = v2 + v6 + m15;
			v14 ^= v2;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v10 = v10 + v14;
			v6 ^= v10;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(11, 3, v3, v7, v11, v15)
			v3 = v3 + v7 + m13;
			v15 ^= v3;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v3 = v3 + v7 + m6;
			v15 ^= v3;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v11 = v11 + v15;
			v7 ^= v11;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(11, 4, v0, v5, v10, v15)
			v0 = v0 + v5 + m1;
			v15 ^= v0;
			v15 = ((v15 >> 32) | (v15 << (64 - 32)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 24) | (v5 << (64 - 24)));
			v0 = v0 + v5 + m12;
			v15 ^= v0;
			v15 = ((v15 >> 16) | (v15 << (64 - 16)));
			v10 = v10 + v15;
			v5 ^= v10;
			v5 = ((v5 >> 63) | (v5 << (64 - 63)));

			// G(11, 5, v1, v6, v11, v12)
			v1 = v1 + v6 + m0;
			v12 ^= v1;
			v12 = ((v12 >> 32) | (v12 << (64 - 32)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 24) | (v6 << (64 - 24)));
			v1 = v1 + v6 + m2;
			v12 ^= v1;
			v12 = ((v12 >> 16) | (v12 << (64 - 16)));
			v11 = v11 + v12;
			v6 ^= v11;
			v6 = ((v6 >> 63) | (v6 << (64 - 63)));

			// G(11, 6, v2, v7, v8, v13)
			v2 = v2 + v7 + m11;
			v13 ^= v2;
			v13 = ((v13 >> 32) | (v13 << (64 - 32)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 24) | (v7 << (64 - 24)));
			v2 = v2 + v7 + m7;
			v13 ^= v2;
			v13 = ((v13 >> 16) | (v13 << (64 - 16)));
			v8 = v8 + v13;
			v7 ^= v8;
			v7 = ((v7 >> 63) | (v7 << (64 - 63)));

			// G(11, 7, v3, v4, v9, v14)
			v3 = v3 + v4 + m5;
			v14 ^= v3;
			v14 = ((v14 >> 32) | (v14 << (64 - 32)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 24) | (v4 << (64 - 24)));
			v3 = v3 + v4 + m3;
			v14 ^= v3;
			v14 = ((v14 >> 16) | (v14 << (64 - 16)));
			v9 = v9 + v14;
			v4 ^= v9;
			v4 = ((v4 >> 63) | (v4 << (64 - 63)));

			// Finalization
			state[0] ^= v0 ^ v8;
			state[1] ^= v1 ^ v9;
			state[2] ^= v2 ^ v10;
			state[3] ^= v3 ^ v11;
			state[4] ^= v4 ^ v12;
			state[5] ^= v5 ^ v13;
			state[6] ^= v6 ^ v14;
			state[7] ^= v7 ^ v15;
		}
	}
#endif
}
