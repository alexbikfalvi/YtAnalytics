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
	/// <para>AFS data base location</para> <para>Defined in <see cref="http://tools.ietf.org/html/rfc1183">RFC 1183</see> and <see cref="http://tools.ietf.org/html/rfc5864">RFC 5864</see></para>
	/// </summary>
	public class AfsdbRecord : DnsRecordBase
	{
		/// <summary>
		/// The AFS database subtype.
		/// </summary>
		public enum AfsSubType : ushort
		{
			/// <summary>
			/// <para>Andrews File Service v3.0 Location service</para> <para>Defined in <see cref="http://tools.ietf.org/html/rfc1183">RFC 1183</see></para>
			/// </summary>
			Afs = 1,
			/// <summary>
			/// <para>DCE/NCA root cell directory node</para> <para>Defined in <see cref="http://tools.ietf.org/html/rfc1183">RFC 1183</see></para>
			/// </summary>
			Dce = 2,
		}

		/// <summary>
		/// Internal constructor.
		/// </summary>
		internal AfsdbRecord() { }

		/// <summary>
		/// Creates a new instance of the AfsdbRecord class.
		/// </summary>
		/// <param name="name">Name of the record.</param>
		/// <param name="timeToLive">Seconds the record should be cached at most.</param>
		/// <param name="subType">Subtype of the record.</param>
		/// <param name="hostname">Hostname of the AFS database.</param>
		public AfsdbRecord(string name, int timeToLive, AfsSubType subType, string hostname)
			: base(name, RecordType.Afsdb, RecordClass.INet, timeToLive)
		{
			this.SubType = subType;
			this.Hostname = hostname ?? String.Empty;
		}

		// Public properties.

		/// <summary>
		/// Subtype of the record.
		/// </summary>
		public AfsSubType SubType { get; private set; }
		/// <summary>
		/// Hostname of the AFS database.
		/// </summary>
		public string Hostname { get; private set; }

		// Protected properties.

		/// <summary>
		/// Gets the maximum record data length.
		/// </summary>
		protected internal override int MaximumRecordDataLength
		{
			get { return Hostname.Length + 4; }
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
			this.SubType = (AfsSubType) DnsMessageBase.ParseUShort(resultData, ref startPosition);
			this.Hostname = DnsMessageBase.ParseDomainName(resultData, ref startPosition);
		}

		/// <summary>
		/// Converts the record data to a string.
		/// </summary>
		/// <returns>The record data string.</returns>
		internal override string RecordDataToString()
		{
			return (byte) this.SubType
				+ " " + this.Hostname;
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
			DnsMessageBase.EncodeUShort(messageData, ref currentPosition, (ushort) SubType);
			DnsMessageBase.EncodeDomainName(messageData, offset, ref currentPosition, this.Hostname, false, domainNames);
		}
	}
}