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

namespace InetApi.Net.Core.Dns.DynamicUpdate
{
	/// <summary>
	/// Delete all records action.
	/// </summary>
	public class DeleteAllRecordsUpdate : DeleteRecordUpdate
	{
		/// <summary>
		/// Internal constructor.
		/// </summary>
		internal DeleteAllRecordsUpdate() {}

		/// <summary>
		/// Creates a new instance of the DeleteAllRecordsUpdate class.
		/// </summary>
		/// <param name="name">Name of records that should be deleted.</param>
		public DeleteAllRecordsUpdate(string name)
			: base(name, RecordType.Any) {}
	}
}