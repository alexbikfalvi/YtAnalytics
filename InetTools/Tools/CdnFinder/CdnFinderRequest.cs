﻿/* 
 * Copyright (C) 2013 Alex Bikfalvi
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or (at
 * your option) any later version.
 *
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.
 */

using System;
using System.Collections.Generic;
using System.Text;
using DotNetApi;
using DotNetApi.Web;

namespace InetTools.Tools.CdnFinder
{
	/// <summary>
	/// A class representing a CDN Finder request.
	/// </summary>
	public sealed class CdnFinderRequest : AsyncWebRequest
	{
		private readonly CdnFinderConfig config;

		/// <summary>
		/// Creates a new CDN Finder request instance.
		/// </summary>
		/// <param name="config">The configuration.</param>
		public CdnFinderRequest(CdnFinderConfig config)
		{
			this.config = config;
		}

		/// <summary>
		/// Begins a new request to the specified CDN Finder server.
		/// </summary>
		/// <param name="uri">The CDN Finder URI.</param>
		/// <param name="sites">The list of web sites.</param>
		/// <param name="callback">The callback method.</param>
		/// <param name="userState">The user state.</param>
		/// <returns>The result of the asynchronous web operation.</returns>
		public IAsyncResult Begin(Uri uri, IEnumerable<object> sites, AsyncWebRequestCallback callback, object userState = null)
		{
			// Create the request state.
			AsyncWebResult asyncState = new AsyncWebResult(uri, callback, userState);

			// Generate the request boundary.
			string boundary = "----InetAnalytics{0}".FormatWith(AsyncWeb.GenerateNonce());

			// Set the request headers.
			asyncState.Request.Method = "POST";
			asyncState.Request.Accept = "text/html,application/xhtml+xml,application/xml";
			asyncState.Request.ContentType = "multipart/form-data; boundary={0}".FormatWith(boundary);
			asyncState.Request.Timeout = this.config.Timeout;
			asyncState.Request.AllowAutoRedirect = this.config.AutoRedirect;

			// Compute the send data.
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat("--{0}{1}", boundary, Environment.NewLine);
			builder.AppendLine("Content-Disposition: form-data; name=\"file\"; filename=\"sites\"");
			builder.AppendLine("Content-Type: text/plain");
			builder.AppendLine();
			foreach (object site in sites)
			{
				if (!string.IsNullOrWhiteSpace(site.ToString()))
				{
					builder.AppendLine(site.ToString());
				}
			}
			builder.AppendFormat("--{0}{1}", boundary, Environment.NewLine);
			builder.AppendLine("Content-Disposition: form-data; name=\"format\"");
			builder.AppendLine();
			builder.AppendLine("xml");
			builder.AppendFormat("--{0}--{1}", boundary, Environment.NewLine);

			// Append the send data.
			asyncState.SendData.Append(builder.ToString(), Encoding.UTF8);

			// Begin the request.
			return base.Begin(asyncState);
		}

		/// <summary>
		/// Completes the web request and returns the result.
		/// </summary>
		/// <param name="result">The result of the asynchronous web operation.</param>
		/// <returns>The list of CDN Finder sites.</returns>
		public new CdnFinderSites End(IAsyncResult result)
		{
			// The data.
			string data;

			// Call the base class end method.
			base.End(result, out data);

			// Parse the data into the list of CDN Finder sites.
			return CdnFinderSites.Parse(data);
		}
	}
}
