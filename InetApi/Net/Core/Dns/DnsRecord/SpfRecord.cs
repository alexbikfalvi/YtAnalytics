﻿/* 
 * Copyright (C) 2010-2012 Alexander Reinert
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *   http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;

namespace InetApi.Net.Core.Dns
{
	/// <summary>
	/// <para>Sender Policy Framework</para> <para>Defined in <see cref="http://tools.ietf.org/html/rfc4408">RFC 4408</see>.</para>
	/// </summary>
	public class SpfRecord : DnsRecordBase, ITextRecord
	{
		/// <summary>
		/// Internal constructor.
		/// </summary>
		internal SpfRecord() { }

		/// <summary>
		/// Creates a new instance of the SpfRecord class.
		/// </summary>
		/// <param name="name">Name of the record.</param>
		/// <param name="timeToLive">Seconds the record should be cached at most.</param>
		/// <param name="textData">Text data of the record.</param>
		public SpfRecord(string name, int timeToLive, string textData)
			: base(name, RecordType.Spf, RecordClass.INet, timeToLive)
		{
			this.TextData = textData ?? String.Empty;
		}

		// Public properties.

		/// <summary>
		/// Text data of the record
		/// </summary>
		public string TextData { get; protected set; }

		// Protected properties.

		/// <summary>
		/// Gets the maximum record data length.
		/// </summary>
		protected internal override int MaximumRecordDataLength
		{
			get { return this.TextData.Length + (this.TextData.Length / 255) + (this.TextData.Length % 255 == 0 ? 0 : 1); }
		}

		// Internal methods.

		/// <summary>
		/// Parses the record data.
		/// </summary>
		/// <param name="resultData">The result data.</param>
		/// <param name="startPosition">The start position.</param>
		/// <param name="length">The length.</param>
		internal override void ParseRecordData(byte[] resultData, int startPosition, int length)
		{
			int endPosition = startPosition + length;

			TextData = String.Empty;
			while (startPosition < endPosition)
			{
				TextData += DnsMessageBase.ParseText(resultData, ref startPosition);
			}
		}

		/// <summary>
		/// Converts the record data to a string.
		/// </summary>
		/// <returns>The record data string.</returns>
		internal override string RecordDataToString()
		{
			return " \"" + this.TextData + "\"";
		}

		// Protected methods.

		/// <summary>
		/// Encodes the data for this record.
		/// </summary>
		/// <param name="messageData">The message data.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="currentPosition">The current position.</param>
		/// <param name="domainNames">The domain names.</param>
		protected internal override void EncodeRecordData(byte[] messageData, int offset, ref int currentPosition, Dictionary<string, ushort> domainNames)
		{
			DnsMessageBase.EncodeText(messageData, ref currentPosition, this.TextData);
		}
	}
}