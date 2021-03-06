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
using Microsoft.Win32;
using DotNetApi;
using InetCommon.Database;
using InetCommon.Log;
using InetCommon.Status;

namespace InetCommon.Tools
{
	/// <summary>
	/// A class allowing the communication of a tool with the host application.
	/// </summary>
	public sealed class ToolApi : IToolApi, IToolConfig
	{
		private readonly IDbApplication application;
		private readonly ToolsetInfoAttribute toolset;
		private readonly ToolInfoAttribute tool;
		private readonly RegistryKey key;

		/// <summary>
		/// Creates a new tool API instance.
		/// </summary>
		/// <param name="application">The application.</param>
		/// <param name="toolset">The toolset information.</param>
		/// <param name="tool">The tool information.</param>
		/// <param name="key">The registry key for this tool.</param>
		public ToolApi(IDbApplication application, ToolsetInfoAttribute toolset, ToolInfoAttribute tool, RegistryKey key)
		{
			// Check the arguments.
			if (null == application) throw new ArgumentNullException("application");
			if (null == toolset) throw new ArgumentNullException("toolset");
			if (null == tool) throw new ArgumentNullException("tool");
			if (null == key) throw new ArgumentNullException("key");

			// Set the parameters.
			this.application = application;
			this.toolset = toolset;
			this.tool = tool;
			this.key = key;
		}

		// Configuration.

		/// <summary>
		/// Gets the Registry key.
		/// </summary>
		public RegistryKey Key { get { return this.key; } }
		/// <summary>
		/// Gets the tool configuration.
		/// </summary>
		public IToolConfig Config { get { return this; } }
		/// <summary>
		/// Gets the crawler status.
		/// </summary>
		public ApplicationStatus Status { get { return this.application.Status; } }
		/// <summary>
		/// Gets the notification message close delay.
		/// </summary>
		public TimeSpan MessageCloseDelay { get { return this.application.Config.MessageCloseDelay; } }

		// Log.

		/// <summary>
		/// Logs an event for the specified toolset.
		/// </summary>
		/// <param name="level">The log event level.</param>
		/// <param name="type">The log event type.</param>
		/// <param name="message">The event message.</param>
		/// <param name="parameters">The event parameters.</param>
		/// <param name="exception">The event exception.</param>
		public LogEvent Log(LogEventLevel level, LogEventType type, string message, object[] parameters = null, Exception exception = null)
		{
			return this.application.Log.Add(
				level,
				type,
				@"Toolbox\{0}\{1}".FormatWith(this.toolset.Name, this.tool.Name),
				message,
				parameters,
				exception
				);
		}

		// Database.

		/// <summary>
		/// Adds the table template to the database.
		/// </summary>
		/// <param name="table">The database table template.</param>
		public void DatabaseAddTable(DbTableTemplate table)
		{
			this.application.Database.Sql.Tables.Add(table);
		}

		/// <summary>
		/// Removes the table template to the database.
		/// </summary>
		/// <param name="table">The database table template.</param>
		public void DatabaseRemoveTable(DbTableTemplate table)
		{
			this.application.Database.Sql.Tables.Remove(table);
		}

		/// <summary>
		/// Adds the table relationship to the database.
		/// </summary>
		/// <param name="leftTable">The left table template.</param>
		/// <param name="rightTable">The right table template.</param>
		/// <param name="leftField">The left field.</param>
		/// <param name="rightField">The right field.</param>
		/// <param name="readOnly">Indicates if the relationship is read-only.</param>
		public void DatabaseAddRelationship(DbTableTemplate leftTable, DbTableTemplate rightTable, string leftField, string rightField, bool readOnly)
		{
			this.application.Database.Sql.Relationships.Add(new DbRelationshipTemplate(leftTable, rightTable, leftField, rightField, readOnly));
		}
	}
}
