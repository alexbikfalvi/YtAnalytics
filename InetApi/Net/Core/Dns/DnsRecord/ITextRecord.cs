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

namespace InetApi.Net.Core.Dns
{
	/// <summary>
	/// Interface for text providing <see cref="DnsRecordBase">records</see>.
	/// </summary>
	internal interface ITextRecord
	{
		/// <summary>
		/// The text data.
		/// </summary>
		string TextData { get; }
	}
}