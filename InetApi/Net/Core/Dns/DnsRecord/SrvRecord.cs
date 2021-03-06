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
	/// <para>Server selector.</para> <para>Defined in <see cref="http://tools.ietf.org/html/rfc2782">RFC 2782</see>.</para>
	/// </summary>
	public class SrvRecord : DnsRecordBase
	{
		/// <summary>
		/// Internal constructor.
		/// </summary>
		internal SrvRecord() { }

		/// <summary>
		/// Creates a new instance of the SrvRecord class
		/// </summary>
		/// <param name="name"> Name of the record </param>
		/// <param name="timeToLive"> Seconds the record should be cached at most </param>
		/// <param name="priority"> Priority of the record </param>
		/// <param name="weight"> Relative weight for records with the same priority </param>
		/// <param name="port"> The port of the service on the target </param>
		/// <param name="target"> Domain name of the target host </param>
		public SrvRecord(string name, int timeToLive, ushort priority, ushort weight, ushort port, string target)
			: base(name, RecordType.Srv, RecordClass.INet, timeToLive)
		{
			Priority = priority;
			Weight = weight;
			Port = port;
			Target = target ?? String.Empty;
		}

		// Public properties.

		/// <summary>
		/// Priority of the record.
		/// </summary>
		public ushort Priority { get; private set; }
		/// <summary>
		/// Relative weight for records with the same priority.
		/// </summary>
		public ushort Weight { get; private set; }
		/// <summary>
		/// The port of the service on the target.
		/// </summary>
		public ushort Port { get; private set; }
		/// <summary>
		/// Domain name of the target host.
		/// </summary>
		public string Target { get; private set; }

		// Protected properties.

		/// <summary>
		/// Gets the maximum record data length.
		/// </summary>
		protected internal override int MaximumRecordDataLength
		{
			get { return this.Target.Length + 8; }
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
			this.Priority = DnsMessageBase.ParseUShort(resultData, ref startPosition);
			this.Weight = DnsMessageBase.ParseUShort(resultData, ref startPosition);
			this.Port = DnsMessageBase.ParseUShort(resultData, ref startPosition);
			this.Target = DnsMessageBase.ParseDomainName(resultData, ref startPosition);
		}

		/// <summary>
		/// Converts the record data to a string.
		/// </summary>
		/// <returns>The record data string.</returns>
		internal override string RecordDataToString()
		{
			return this.Priority
				+ " " + this.Weight
				+ " " + this.Port
				+ " " + this.Target;
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
			DnsMessageBase.EncodeUShort(messageData, ref currentPosition, this.Priority);
			DnsMessageBase.EncodeUShort(messageData, ref currentPosition, this.Weight);
			DnsMessageBase.EncodeUShort(messageData, ref currentPosition, this.Port);
			DnsMessageBase.EncodeDomainName(messageData, offset, ref currentPosition, this.Target, false, domainNames);
		}
	}
}