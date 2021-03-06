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
using System.Net;

namespace InetApi.Net.Core.Dns
{
	/// <summary>
	/// <para>IPv6 address</para> <para>Defined in <see cref="http://tools.ietf.org/html/rfc3596">RFC 3596</see></para>
	/// </summary>
	public class AaaaRecord : DnsRecordBase, IAddressRecord
	{
		/// <summary>
		/// Internal constructor.
		/// </summary>
		internal AaaaRecord() {}

		/// <summary>
		/// Creates a new instance of the AaaaRecord class.
		/// </summary>
		/// <param name="name">Domain name of the host.</param>
		/// <param name="timeToLive">Seconds the record should be cached at most.</param>
		/// <param name="address"> IP address of the host.</param>
		public AaaaRecord(string name, int timeToLive, IPAddress address)
			: base(name, RecordType.Aaaa, RecordClass.INet, timeToLive)
		{
			this.Address = address ?? IPAddress.IPv6None;
		}

		// Public properties.

		/// <summary>
		/// IP address of the host.
		/// </summary>
		public IPAddress Address { get; private set; }

		// Protected properties.

		/// <summary>
		/// Gets the maximum record data length.
		/// </summary>
		protected internal override int MaximumRecordDataLength
		{
			get { return 16; }
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
			this.Address = new IPAddress(DnsMessageBase.ParseByteData(resultData, ref startPosition, 16));
		}

		/// <summary>
		/// Converts the record data to a string.
		/// </summary>
		/// <returns>The record data string.</returns>
		internal override string RecordDataToString()
		{
			return this.Address.ToString();
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
			DnsMessageBase.EncodeByteArray(messageData, ref currentPosition, this.Address.GetAddressBytes());
		}
	}
}