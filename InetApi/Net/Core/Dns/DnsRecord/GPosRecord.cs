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
using System.Globalization;

namespace InetApi.Net.Core.Dns
{
	/// <summary>
	/// <para>Geographical position</para> <para>Defined in <see cref="http://tools.ietf.org/html/rfc1712">RFC 1712</see>.</para>
	/// </summary>
	public class GPosRecord : DnsRecordBase
	{
		/// <summary>
		/// Internal constructor.
		/// </summary>
		internal GPosRecord() { }

		/// <summary>
		/// Creates a new instance of the GPosRecord class.
		/// </summary>
		/// <param name="name">Name of the record.</param>
		/// <param name="timeToLive">Seconds the record should be cached at most.</param>
		/// <param name="longitude">Longitude of the geographical position.</param>
		/// <param name="latitude">Latitude of the geographical position.</param>
		/// <param name="altitude">Altitude of the geographical position.</param>
		public GPosRecord(string name, int timeToLive, double longitude, double latitude, double altitude)
			: base(name, RecordType.GPos, RecordClass.INet, timeToLive)
		{
			this.Longitude = longitude;
			this.Latitude = latitude;
			this.Altitude = altitude;
		}

		// Public properties.

		/// <summary>
		/// Longitude of the geographical position.
		/// </summary>
		public double Longitude { get; private set; }

		/// <summary>
		/// Latitude of the geographical position.
		/// </summary>
		public double Latitude { get; private set; }

		/// <summary>
		/// Altitude of the geographical position.
		/// </summary>
		public double Altitude { get; private set; }

		// Protected properties.

		/// <summary>
		/// Gets the maximum record data length.
		/// </summary>
		protected internal override int MaximumRecordDataLength
		{
			get { return 3 + this.Longitude.ToString().Length + this.Latitude.ToString().Length + this.Altitude.ToString().Length; }
		}

		// Internal methods.

		/// <summary>
		/// Parses the record data.
		/// </summary>
		/// <param name="resultData">The result data.</param>
		/// <param name="startPosition">The start position.</param>
		/// <param name="length">The length.</param>
		internal override void ParseRecordData(byte[] resultData, int currentPosition, int length)
		{
			this.Longitude = Double.Parse(DnsMessageBase.ParseText(resultData, ref currentPosition), CultureInfo.InvariantCulture);
			this.Latitude = Double.Parse(DnsMessageBase.ParseText(resultData, ref currentPosition), CultureInfo.InvariantCulture);
			this.Altitude = Double.Parse(DnsMessageBase.ParseText(resultData, ref currentPosition), CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Converts the record data to a string.
		/// </summary>
		/// <returns>The record data string.</returns>
		internal override string RecordDataToString()
		{
			return this.Longitude.ToString(CultureInfo.InvariantCulture)
				+ " " + this.Latitude.ToString(CultureInfo.InvariantCulture)
				+ " " + this.Altitude.ToString(CultureInfo.InvariantCulture);
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
			DnsMessageBase.EncodeText(messageData, ref currentPosition, this.Longitude.ToString(CultureInfo.InvariantCulture));
			DnsMessageBase.EncodeText(messageData, ref currentPosition, this.Latitude.ToString(CultureInfo.InvariantCulture));
			DnsMessageBase.EncodeText(messageData, ref currentPosition, this.Altitude.ToString(CultureInfo.InvariantCulture));
		}
	}
}