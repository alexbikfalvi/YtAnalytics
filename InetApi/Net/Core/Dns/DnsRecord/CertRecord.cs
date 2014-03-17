﻿#region Copyright and License
// Copyright 2010..2012 Alexander Reinert
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARSoft.Tools.Net.Dns
{
	/// <summary>
	/// <para>Certificate storage record</para> <para>Defined in <see cref="http://tools.ietf.org/html/rfc4398">RFC 4398</see>.</para>
	/// </summary>
	public class CertRecord : DnsRecordBase
	{
		/// <summary>
		/// Type of cert
		/// </summary>
		public enum CertType : ushort
		{
			/// <summary>
			/// None.
			/// </summary>
			None = 0,
			/// <summary>
			/// <para>X.509 as per PKIX</para> <para>Defined in <see cref="http://tools.ietf.org/html/rfc4398">RFC 4398</see>.</para>
			/// </summary>
			Pkix = 1,
			/// <summary>
			/// <para>SPKI certificate</para> <para>Defined in <see cref="http://tools.ietf.org/html/rfc4398">RFC 4398</see>.</para>
			/// </summary>
			Spki = 2,
			/// <summary>
			/// <para>OpenPGP packet</para> <para>Defined in <see cref="http://tools.ietf.org/html/rfc4398">RFC 4398</see>.</para>
			/// </summary>
			Pgp = 3,
			/// <summary>
			/// <para>The URL of an X.509 data object</para> <para>Defined in <see cref="http://tools.ietf.org/html/rfc4398">RFC 4398</see>.</para>
			/// </summary>
			IPkix = 4,
			/// <summary>
			/// <para>The URL of an SPKI certificate</para> <para>Defined in <see cref="http://tools.ietf.org/html/rfc4398">RFC 4398</see>.</para>
			/// </summary>
			ISpki = 5,
			/// <summary>
			/// <para>The fingerprint and URL of an OpenPGP packet</para> <para>Defined in <see cref="http://tools.ietf.org/html/rfc4398">RFC 4398</see>.</para>
			/// </summary>
			IPgp = 6,
			/// <summary>
			/// <para>Attribute Certificate</para> <para>Defined in <see cref="http://tools.ietf.org/html/rfc4398">RFC 4398</see>.</para>
			/// </summary>
			Acpkix = 7,
			/// <summary>
			/// <para>The URL of an Attribute Certificate</para> <para>Defined in <see cref="http://tools.ietf.org/html/rfc4398">RFC 4398</see>.</para>
			/// </summary>
			IAcpkix = 8,
			/// <summary>
			/// <para>URI private</para> <para>Defined in <see cref="http://tools.ietf.org/html/rfc4398">RFC 4398</see>.</para>
			/// </summary>
			Uri = 253,
			/// <summary>
			/// <para>OID private</para> <para>Defined in <see cref="http://tools.ietf.org/html/rfc4398">RFC 4398</see>.</para>
			/// </summary>
			Oid = 254
		}

		/// <summary>
		/// Internal constructor.
		/// </summary>
		internal CertRecord() { }

		/// <summary>
		/// Creates a new instace of the CertRecord class
		/// </summary>
		/// <param name="name"> Name of the record </param>
		/// <param name="timeToLive"> Seconds the record should be cached at most </param>
		/// <param name="type"> Type of the certificate data </param>
		/// <param name="keyTag"> Key tag </param>
		/// <param name="algorithm"> Algorithm of the certificate </param>
		/// <param name="certificate"> Binary data of the certificate </param>
		public CertRecord(string name, int timeToLive, CertType type, ushort keyTag, DnsSecAlgorithm algorithm, byte[] certificate)
			: base(name, RecordType.Cert, RecordClass.INet, timeToLive)
		{
			this.Type = type;
			this.KeyTag = keyTag;
			this.Algorithm = algorithm;
			this.Certificate = certificate ?? new byte[] { };
		}

		// Public properties.

		/// <summary>
		/// Type of the certificate data.
		/// </summary>
		public CertType Type { get; private set; }
		/// <summary>
		/// Key tag.
		/// </summary>
		public ushort KeyTag { get; private set; }
		/// <summary>
		/// Algorithm of the certificate.
		/// </summary>
		public DnsSecAlgorithm Algorithm { get; private set; }
		/// <summary>
		/// Binary data of the certificate
		/// </summary>
		public byte[] Certificate { get; private set; }

		// Protected properties.

		/// <summary>
		/// Gets the maximum record data length.
		/// </summary>
		protected internal override int MaximumRecordDataLength
		{
			get { return 5 + Certificate.Length; }
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
			Type = (CertType) DnsMessageBase.ParseUShort(resultData, ref startPosition);
			KeyTag = DnsMessageBase.ParseUShort(resultData, ref startPosition);
			Algorithm = (DnsSecAlgorithm) resultData[startPosition++];
			Certificate = DnsMessageBase.ParseByteData(resultData, ref startPosition, length - 5);
		}

		/// <summary>
		/// Converts the record data to a string.
		/// </summary>
		/// <returns>The record data string.</returns>
		internal override string RecordDataToString()
		{
			return (ushort) this.Type
				+ " " + this.KeyTag
				+ " " + (byte) this.Algorithm
				+ " " + this.Certificate.ToBase64String();
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
			DnsMessageBase.EncodeUShort(messageData, ref currentPosition, (ushort) Type);
			DnsMessageBase.EncodeUShort(messageData, ref currentPosition, KeyTag);
			messageData[currentPosition++] = (byte) Algorithm;
			DnsMessageBase.EncodeByteArray(messageData, ref currentPosition, Certificate);
		}
	}
}