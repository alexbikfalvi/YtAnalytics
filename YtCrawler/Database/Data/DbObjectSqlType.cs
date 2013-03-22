﻿/* 
 * Copyright (C) 2012-2013 Alex Bikfalvi
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
using System.ComponentModel;
using System.Data;
using YtCrawler.Database;

namespace YtCrawler.Database.Data
{
	/// <summary>
	/// A type representing a data type in an SQL Server.
	/// </summary>
	[Serializable]
	public sealed class DbObjectSqlType : DbObject
	{
		// Properties.

		[Browsable(true), DisplayName("Name"), ReadOnly(true), Db(DbType.String, false, "name"), Description("The data type name.")]
		public string Name { get; set; }
		
		[Browsable(true), DisplayName("System type ID"), ReadOnly(true), Db(DbType.Int32, false, "system_type_id"), Description("The system type ID.")]
		public int SystemTypeId { get; set; }
		
		[Browsable(true), DisplayName("User type ID"), ReadOnly(true), Db(DbType.Int32, false, "user_type_id"), Description("The user type ID.")]
		public int UserTypeId { get; set; }

		[Browsable(true), DisplayName("Schema ID"), ReadOnly(true), Db(DbType.Int32, false, "schema_id"), Description("The schema ID.")]
		public int SchemaId { get; set; }

		[Browsable(true), DisplayName("Principal ID"), ReadOnly(true), Db(DbType.Int32, true, "principal_id"), Description("The principal ID.")]
		public int? PrincipalId { get; set; }

		[Browsable(true), DisplayName("Maximum length"), ReadOnly(true), Db(DbType.Int32, false, "max_length"), Description("The maximum length for this type.")]
		public int MaximumLength { get; set; }
		
		[Browsable(true), DisplayName("Precision"), ReadOnly(true), Db(DbType.Int32, false, "precision"), Description("The precision for this type.")]
		public int Precision { get; set; }
		
		[Browsable(true), DisplayName("Scale"), ReadOnly(true), Db(DbType.Int32, false, "scale"), Description("The scale for this type.")]
		public int Scale { get; set; }
		
		[Browsable(true), DisplayName("Is nullable"), ReadOnly(true), Db(DbType.Boolean, true, "is_nullable"), Description("Indicates if the type is nullable.")]
		public bool? IsNullable { get; set; }

		[Browsable(true), DisplayName("Is user-defined"), ReadOnly(true), Db(DbType.Boolean, false, "is_user_defined"), Description("Indicates if the type was defined by the user.")]
		public bool IsUserDefined { get; set; }

		[Browsable(true), DisplayName("Is assembly type"), ReadOnly(true), Db(DbType.Boolean, false, "is_assembly_type"), Description("Indicates if this is an assembly type.")]
		public bool IsAssemblyType { get; set; }

		[Browsable(true), DisplayName("Is table type"), ReadOnly(true), Db(DbType.Boolean, false, "is_table_type"), Description("Indicates if this is a table type.")]
		public bool IsTableType { get; set; }

		// Methods.

		/// <summary>
		/// The name of the current object.
		/// </summary>
		/// <returns>The name.</returns>
		public override string GetName()
		{
			return this.Name;
		}

		/// <summary>
		/// Compares two database objects.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns><b>True</b> if the two objects are equal, <b>false</b> otherwise.</returns>
		public bool Equals(DbObjectSqlType obj)
		{
			return (this.Name == obj.Name) && (this.SystemTypeId == obj.SystemTypeId) && (this.UserTypeId == obj.UserTypeId);
		}
	}
}
