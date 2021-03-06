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
using System.Collections.Generic;
using System.Text;
using DotNetApi;
using InetCommon.Database.Data;

namespace InetCommon.Database
{
	/// <summary>
	/// A class representing a generic SQL database query.
	/// </summary>
	public sealed class DbQuerySql : DbQuery
	{
		private string query = null;
		private ITable table = null;

		private string messageStart = string.Empty;
		private string messageFinishSuccess = string.Empty;
		private string messageFinishFail = string.Empty;

		private readonly List<object> parameters = new List<object>();

		private object userState;

		/// <summary>
		/// Private constructor.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <param name="table">The table corresponding to this query.</param>
		/// <param name="userState">The user state.</param>
		private DbQuerySql(
			string query,
			ITable table,
			object userState
			)
		{
			this.query = query;
			this.table = table;
			this.userState = userState;
		}
		
		// Public properties.

		/// <summary>
		/// Gets the query string.
		/// </summary>
		public string Query { get { return this.query; } }
		/// <summary>
		/// Gets the database table corresponding to this query.
		/// </summary>
		public ITable Table { get { return this.table; } }
		/// <summary>
		/// Gets the parameters used for this query.
		/// </summary>
		public List<object> Parameters { get { return this.parameters; } }
		/// <summary>
		/// Gets the user state for this query.
		/// </summary>
		public object State { get { return this.userState; } }
		/// <summary>
		/// Gets the message to display when starting this query.
		/// </summary>
		public string MessageStart
		{
			get { return this.messageStart; }
			set { this.messageStart = value; }
		}
		/// <summary>
		/// Gets the message to display when successfully finishing this query.
		/// </summary>
		public string MessageFinishSuccess
		{
			get { return this.messageFinishSuccess; }
			set { this.messageFinishSuccess = value; }
		}
		/// <summary>
		/// Gets the message to display when the query fails.
		/// </summary>
		public string MessageFinishFail
		{
			get { return this.messageFinishFail; }
			set { this.messageFinishFail = value; }
		}

		// Public methods.

		/// <summary>
		/// Converts the current query to a string.
		/// </summary>
		/// <returns>The string representation of the query.</returns>
		public override string ToString()
		{
			return this.query;
		}

		/// <summary>
		/// Creates a database query for the specified query text.
		/// </summary>
		/// <param name="text">The query text.</param>
		/// <param name="userState">The user state.</param>
		/// <returns>The query.</returns>
		public static DbQuerySql Create(string text, object userState = null)
		{
			return new DbQuerySql(text, null, userState);
		}

		/// <summary>
		/// Creates a database query that returns all records for the specified table.
		/// </summary>
		/// <param name="table">The database table.</param>
		/// <param name="database">The datase, if needed.</param>
		/// <param name="userState">The user state.</param>
		/// <returns>The query.</returns>
		public static DbQuerySql CreateSelectAll(ITable table, DbObjectDatabase database, object userState = null)
		{
			// If the table is not configured, throw an exception.
			if (!table.IsConfigured) throw new DbException("Cannot create a database query for the table \'{0}\'. The table is not configured.".FormatWith(table.LocalName));
			// If the table  requires a database, check that a database is configured for this server.
			if (table.DefaultDatabase && (database == null)) throw new DbException("Cannot create a database query for the table \'{0}\'. The table requires a database but the server does not have a database configured.".FormatWith(table.LocalName));
			// Get the table name.
			string tableName = DbQuerySql.GetTableName(table, database);
			// Create and return the query.
			return new DbQuerySql("SELECT {0} FROM {1}".FormatWith(
				DbQuerySql.GetFieldNames(table, tableName),
				tableName), table, userState);
		}

		/// <summary>
		/// Creates a database query that returns the field for the specified table.
		/// </summary>
		/// <param name="table">The database table.</param>
		/// <param name="nameSelect">The field to select.</param>
		/// <param name="database">The datase, if needed.</param>
		/// <param name="userState">The user state.</param>
		/// <returns>The query.</returns>
		public static DbQuerySql CreateSelectField(ITable table, string nameSelect, DbObjectDatabase database, object userState = null)
		{
			// If the table is not configured, throw an exception.
			if (!table.IsConfigured) throw new DbException("Cannot create a database query for the table \'{0}\'. The table is not configured.".FormatWith(table.LocalName));
			// If the table  requires a database, check that a database is configured for this server.
			if (table.DefaultDatabase && (database == null)) throw new DbException("Cannot create a database query for the table \'{0}\'. The table requires a database but the server does not have a database configured.".FormatWith(table.LocalName));
			// Get the field.
			DbField field = table[nameSelect];
			// Create and return the query.
			return new DbQuerySql("SELECT {0} FROM {1}".FormatWith(
				field.GetDatabaseName(),
				DbQuerySql.GetTableName(table, database)), table, userState);
		}

		/// <summary>
		/// Creates a database query that returns the field for the specified table for a given field value.
		/// </summary>
		/// <param name="table">The database table.</param>
		/// <param name="fieldOn">The field to check.</param>
		/// <param name="valueOn">The field to check value.</param>
		/// <param name="database">The datase, if needed.</param>
		/// <param name="userState">The user state.</param>
		/// <returns>The query.</returns>
		public static DbQuerySql CreateSelectAllOn(ITable table, string fieldOn, object valueOn, DbObjectDatabase database, object userState = null)
		{
			// If the table is not configured, throw an exception.
			if (!table.IsConfigured) throw new DbException("Cannot create a database query for the table \'{0}\'. The table is not configured.".FormatWith(table.LocalName));
			// If the table  requires a database, check that a database is configured for this server.
			if (table.DefaultDatabase && (database == null)) throw new DbException("Cannot create a database query for the table \'{0}\'. The table requires a database but the server does not have a database configured.".FormatWith(table.LocalName));
			// Get the field.
			DbField field = table[fieldOn];
			// Get the table name.
			string tableName = DbQuerySql.GetTableName(table, database);
			// Create the query.
			DbQuerySql query = new DbQuerySql("SELECT {0} FROM {1} WHERE {2} = {3}".FormatWith(
				DbQuerySql.GetFieldNames(table, tableName),
				tableName,
				field.GetDatabaseName(),
				"{0}"), table, userState);
			// Add the parameters.
			query.parameters.Add(valueOn);
			// Return the query.
			return query;
		}

		/// <summary>
		/// Creates a database query that returns the field for the specified table for a given field value of
		/// a related table.
		/// </summary>
		/// <param name="table">The database table.</param>
		/// <param name="tableOn">The related table.</param>
		/// <param name="fieldOn">The related table field name.</param>
		/// <param name="valueOn">The related table field value.</param>
		/// <param name="database">The datase, if needed.</param>
		/// <param name="userState">The user state.</param>
		/// <returns>The query.</returns>
		public static DbQuerySql CreateSelectAllOn(ITable table, ITable tableOn, string fieldOn, object valueOn, DbObjectDatabase database, object userState = null)
		{
			// If the table is not configured, throw an exception.
			if (!table.IsConfigured) throw new DbException("Cannot create a database query for the table \'{0}\'. The table is not configured.".FormatWith(table.LocalName));
			// If the table  requires a database, check that a database is configured for this server.
			if (table.DefaultDatabase && (database == null)) throw new DbException("Cannot create a database query for the table \'{0}\'. The table requires a database but the server does not have a database configured.".FormatWith(table.LocalName));
			
			// Get the relationships between the current table and the right table.
			List<IRelationship> relationships = new List<IRelationship>();
			foreach (IRelationship relationship in table.Relationships)
			{
				if (relationship.RightTable == tableOn)
				{
					relationships.Add(relationship);
				}
			}
			// If there are no relationships between tables, throw an exception.
			if (relationships.Count == 0) throw new DbException("Cannot create a database query for table \'{0}\' based on a match in the table \'{1}\', because there is no relationship between the two tables.".FormatWith(table.LocalName, tableOn.LocalName));

			// Get the on field.
			DbField fieldRight = tableOn[fieldOn];
			// Check the on field exists.
			if(null == fieldRight) throw new DbException("Cannot create a database query on matching field \'{0}\' for the table \'{1}\', because the field does not exist.".FormatWith(fieldOn, tableOn.LocalName));
			// Get the table names.
			string tableNameLeft = DbQuerySql.GetTableName(table, database);
			string tableNameRight = DbQuerySql.GetTableName(tableOn, database);
			// Create the query.
			DbQuerySql query = new DbQuerySql(
				"SELECT {0} FROM {1} INNER JOIN {2} ON ({3} = {4}){5}".FormatWith(
					DbQuerySql.GetFieldNames(table, tableNameLeft),
					tableNameLeft,
					tableNameRight,
					DbQuerySql.GetFieldName(fieldRight, tableNameRight),
					"{0}",
					DbQuerySql.GetRelationshipsMatch(relationships, database)
				),
				table, userState);
			// Add the parameters.
			query.parameters.Add(valueOn);
			// Return the query.
			return query;
		}

		/// <summary>
		/// Creates a database query that returns the field for the specified table for a given field value of
		/// a related table.
		/// </summary>
		/// <param name="table">The database table.</param>
		/// <param name="tableOn">The related table.</param>
		/// <param name="fieldsOn">The related table field names.</param>
		/// <param name="valuesOn">The related table field values.</param>
		/// <param name="database">The datase, if needed.</param>
		/// <param name="userState">The user state.</param>
		/// <returns>The query.</returns>
		public static DbQuerySql CreateSelectAllOn(ITable table, ITable tableOn, string[] fieldsOn, object[] valuesOn, DbObjectDatabase database, object userState = null)
		{
			// If the table is not configured, throw an exception.
			if (!table.IsConfigured) throw new DbException("Cannot create a database query for the table \'{0}\'. The table is not configured.".FormatWith(table.LocalName));
			// If the table  requires a database, check that a database is configured for this server.
			if (table.DefaultDatabase && (database == null)) throw new DbException("Cannot create a database query for the table \'{0}\'. The table requires a database but the server does not have a database configured.".FormatWith(table.LocalName));
			// Check the number of field names and values matches.
			if (fieldsOn.Length != valuesOn.Length) throw new DbException("Cannot create a database query for the table \'{0}\'. The number of field names (\'{1}\') and field values (\'{2}\') must be equal.".FormatWith(table.LocalName, fieldsOn.Length, valuesOn.Length));

			// Get the relationships between the current table and the right table.
			List<IRelationship> relationships = new List<IRelationship>();
			foreach (IRelationship relationship in table.Relationships)
			{
				if (relationship.RightTable == tableOn)
				{
					relationships.Add(relationship);
				}
			}
			// If there are no relationships between tables, throw an exception.
			if (relationships.Count == 0) throw new DbException("Cannot create a database query for table \'{0}\' based on a match in the table \'{1}\', because there is no relationship between the two tables.".FormatWith(table.LocalName, tableOn.LocalName));

			// The field index.
			int fieldIndex = 0;
			// Get the table names.
			string tableNameLeft = DbQuerySql.GetTableName(table, database);
			string tableNameRight = DbQuerySql.GetTableName(tableOn, database);
			// Create the query.
			DbQuerySql query = new DbQuerySql(
				"SELECT {0} FROM {1} INNER JOIN {2} ON {3}{4}".FormatWith(
					DbQuerySql.GetFieldNames(table, tableNameLeft),
					tableNameLeft,
					tableNameRight,
					DbQuerySql.GetFieldsMatch(tableOn, fieldsOn, database, ref fieldIndex),
					DbQuerySql.GetRelationshipsMatch(relationships, database)
				),
				table, userState);
			// Add the parameters.
			foreach (object valueOn in valuesOn)
			{
				query.parameters.Add(valueOn);
			}
			// Return the query.
			return query;
		}

		public static DbQuerySql CreateInsertAll(ITable table, DbObject value, DbObjectDatabase database, object userState = null)
		{
			// If the table is not configured, throw an exception.
			if (!table.IsConfigured) throw new DbException("Cannot create a database query for the table \'{0}\'. The table is not configured.".FormatWith(table.LocalName));
			// If the table  requires a database, check that a database is configured for this server.
			if (table.DefaultDatabase && (database == null)) throw new DbException("Cannot create a database query for the table \'{0}\'. The table requires a database but the server does not have a database configured.".FormatWith(table.LocalName));
			// Check the table type matches the database object type.
			if (table.Type != value.GetType()) throw new DbException("Cannot create a database insert query for table \'{0}\', because object type \'{1}\' does not match the table type \'{2}\'.".FormatWith(table.LocalName, table.Type.Name, value.GetType().Name));
			// Get the table name.
			string tableName = DbQuerySql.GetTableName(table, database);
			// Create the query.
			DbQuerySql query = new DbQuerySql(
				"INSERT INTO {0} ({1}) VALUES ({2})".FormatWith(
					DbQuerySql.GetFieldNames(table, tableName),
					DbQuerySql.GetFieldIndices(table, 0)
				),
				table, userState);
			// Add the parameters.
			foreach (DbField field in table.Fields)
			{
				query.parameters.Add(value.GetValue(field.LocalName));
			}
			// Return the query.
			return query;
		}

		public static DbQuerySql CreateUpdateAllOn(ITable table, DbObject value, string fieldOn, object valueOn, DbObjectDatabase database, object userState = null)
		{
			// If the table is not configured, throw an exception.
			if (!table.IsConfigured) throw new DbException("Cannot create a database query for the table \'{0}\'. The table is not configured.".FormatWith(table.LocalName));
			// If the table  requires a database, check that a database is configured for this server.
			if (table.DefaultDatabase && (database == null)) throw new DbException("Cannot create a database query for the table \'{0}\'. The table requires a database but the server does not have a database configured.".FormatWith(table.LocalName));
			// Check the table type matches the database object type.
			if (table.Type != value.GetType()) throw new DbException("Cannot create a database update query for table \'{0}\', because object type \'{1}\' does not match the table type \'{2}\'.".FormatWith(table.LocalName, table.Type.Name, value.GetType().Name));

			// Get the table name.
			string tableName = DbQuerySql.GetTableName(table, database);

			// Get the on field.
			DbField field = table[fieldOn];
			// Check the on field exists.
			if(null == field) throw new DbException("Cannot create a database query on matching field \'{0}\' for the table \'{1}\', because the field does not exist.".FormatWith(fieldOn, table.LocalName));

			// Get the fields to update, excluding the on field.
			int fieldIndex = 0;
			string fields = DbQuerySql.GetFieldsSetExcluding(table, fieldOn, database, ref fieldIndex);

			// Create the query.
			DbQuerySql query = new DbQuerySql(
				"UPDATE {0} SET {1} WHERE {2}={{{3}}}".FormatWith(
					tableName,
					fields,
					field.GetDatabaseName(),
					fieldIndex++
				),
				table,
				userState);
			// Return the query.
			return query;
		}

		// Private methods.

		/// <summary>
		/// Returns full the table name, including database and schema if required.
		/// </summary>
		/// <param name="table">The database table.</param>
		/// <param name="database">The database.</param>
		/// <returns>The table name as a string.</returns>
		private static string GetTableName(ITable table, DbObjectDatabase database)
		{
			// Return the table name made of the database name, schema name, and table name.
			return "[{0}].[{1}].[{2}]".FormatWith(
				table.DefaultDatabase ? database.Name : table.Database,
				table.Schema,
				table.DatabaseName
				);
		}

		/// <summary>
		/// Returns the full database field name for the given table, local field name and default database.
		/// </summary>
		/// <param name="field">The field.</param>
		/// <param name="tableName">The table name.</param>
		/// <returns>The field name as a string.</returns>
		private static string GetFieldName(DbField field, string tableName)
		{
			// Return the field name made of the database name, schema name, table name, and field name.
			return "{0}.[{1}]".FormatWith(tableName, field.GetDatabaseName());
		}

		/// <summary>
		/// Returns the field names, separated by comma for the specified table.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <param name="tableName">The table name.</param>
		/// <returns>The field names as a string.</returns>
		private static string GetFieldNames(ITable table, string tableName)
		{
			// Create the fields.
			StringBuilder builderFields = new StringBuilder();
			using (IEnumerator<DbField> enumerator = table.Fields.GetEnumerator())
			{
				//  If the fields list is not empty, advance to the first element.
				if (enumerator.MoveNext())
				{
					DbField field;
					bool notLast;
					// Continue reading fields.
					do
					{
						field = enumerator.Current;
						notLast = enumerator.MoveNext();

						// If last element, append its name and a comma.
						if (notLast) builderFields.AppendFormat("{0}.[{1}],", tableName, field.GetDatabaseName());
						// Otherwise, append its name.
						else builderFields.AppendFormat("{0}.[{1}]", tableName, field.GetDatabaseName());
					}
					// Until the last field is reached.
					while (notLast);
				}
			}
			// Return the field names.
			return builderFields.ToString();
		}

		/// <summary>
		/// Returns the field indices, separated by comma for the specified table.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <param name="startAt">The index at which to start.</param>
		/// <returns>The field indices as a string.</returns>
		private static string GetFieldIndices(ITable table, int startAt)
		{
			// Set the index.
			int index = startAt;
			// Create the fields.
			StringBuilder builderFields = new StringBuilder();
			using (IEnumerator<DbField> enumerator = table.Fields.GetEnumerator())
			{
				//  If the fields list is not empty, advance to the first element.
				if (enumerator.MoveNext())
				{
					DbField field;
					bool notLast;
					// Continue reading fields.
					do
					{
						field = enumerator.Current;
						notLast = enumerator.MoveNext();

						// If last element, append its name and a comma.
						if (notLast) builderFields.AppendFormat("{{{0}}},", index++);
						// Otherwise, append its name.
						else builderFields.AppendFormat("{{{0}}}", index++);
					}
					// Until the last field is reached.
					while (notLast);
				}
			}
			// Return the field names.
			return builderFields.ToString();
		}

		/// <summary>
		/// Returns the string of conditions corresponding to matching the relationships list.
		/// </summary>
		/// <param name="relationships">The list of relationships.</param>
		/// <param name="database">The default database.</param>
		/// <returns>The conditions string.</returns>
		private static string GetRelationshipsMatch(IEnumerable<IRelationship> relationships, DbObjectDatabase database)
		{
			// Create the string builder.
			StringBuilder builderConditions = new StringBuilder();
			// Add a condition for each relationship.
			foreach (IRelationship relationship in relationships)
			{
				ITable tableLeft = relationship.LeftTable;
				ITable tableRight = relationship.RightTable;
				DbField fieldLeft = tableLeft[relationship.LeftField];
				DbField fieldRight = tableRight[relationship.RightField];

				builderConditions.AppendFormat(" AND ({0}.[{1}] = {2}.[{3}])",
					DbQuerySql.GetTableName(tableLeft, database),
					fieldLeft.GetDatabaseName(),
					DbQuerySql.GetTableName(tableRight, database),
					fieldRight.GetDatabaseName()
					);
			}
			// Returns the conditions string.
			return builderConditions.ToString();
		}

		/// <summary>
		/// Returns the string of conditions that matches the fields of a table with a set of parameter indices,
		/// separated by an AND condition.
		/// </summary>
		/// <param name="table">The database table.</param>
		/// <param name="fields">The field names.</param>
		/// <param name="database">The database.</param>
		/// <param name="fieldIndex">The field index.</param>
		/// <returns>The string of conditions.</returns>
		private static string GetFieldsMatch(ITable table, string[] fields, DbObjectDatabase database, ref int fieldIndex)
		{
			// Get the table name.
			string tableName = DbQuerySql.GetTableName(table, database);
			// Create the string builder.
			StringBuilder stringBuilder = new StringBuilder();
			// Set the start index.
			int startIndex = fieldIndex;
			// For all the fields.
			foreach (string fieldName in fields)
			{
				// Get the on field.
				DbField field = table[fieldName];
				// Check the on field exists.
				if (null == field) throw new DbException("Cannot create a database query on matching field \'{0}\' for the table \'{1}\', because the field does not exist.".FormatWith(fieldName, table.LocalName));
				// Add the field match to the query.
				stringBuilder.AppendFormat(" {0} ({1} = {{{2}}})",
					fieldIndex != startIndex ? "AND" : string.Empty,
					DbQuerySql.GetFieldName(field, tableName),
					fieldIndex);
				// Increment the field index.
				fieldIndex++;
			}
			// Return the string.
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Returns the string of statements that matches all the fields of a table with a set of parameter indices,
		/// separated by a comma.
		/// </summary>
		/// <param name="table">The database table.</param>
		/// <param name="database">The database.</param>
		/// <param name="fieldIndex">The field index.</param>
		/// <returns>The string of conditions.</returns>
		private static string GetFieldsSet(ITable table, DbObjectDatabase database, ref int fieldIndex)
		{
			// Get the table name.
			string tableName = DbQuerySql.GetTableName(table, database);
			// Create the string builder.
			StringBuilder stringBuilder = new StringBuilder();
			// Set the start index.
			int startIndex = fieldIndex;
			// For all the fields.
			foreach (DbField field in table.Fields)
			{
				// Add the field match to the query.
				stringBuilder.AppendFormat("{0} {1} = {{{2}}}",
					fieldIndex != startIndex ? "," : string.Empty,
					DbQuerySql.GetFieldName(field, tableName),
					fieldIndex);
				// Increment the field index.
				fieldIndex++;
			}
			// Return the string.
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Returns the string of statements that matches all the fields of a table, exclusing a specified field,
		/// with a set of parameter indices, separated by a comma.
		/// </summary>
		/// <param name="table">The database table.</param>
		/// <param name="fieldExclude">The field name to exclude.</param>
		/// <param name="database">The database.</param>
		/// <param name="fieldIndex">The field index.</param>
		/// <returns>The string of conditions.</returns>
		private static string GetFieldsSetExcluding(ITable table, string fieldExclude, DbObjectDatabase database, ref int fieldIndex)
		{
			// Get the table name.
			string tableName = DbQuerySql.GetTableName(table, database);
			// Create the string builder.
			StringBuilder stringBuilder = new StringBuilder();
			// Set the start index.
			int startIndex = fieldIndex;
			// For all the fields.
			foreach (DbField field in table.Fields)
			{
				// If the field matches the field to exclude, ignore the field.
				if (field.LocalName == fieldExclude) continue;
				// Add the field match to the query.
				stringBuilder.AppendFormat("{0} {1} = {{{2}}}",
					fieldIndex != startIndex ? "," : string.Empty,
					DbQuerySql.GetFieldName(field, tableName),
					fieldIndex);
				// Increment the field index.
				fieldIndex++;
			}
			// Return the string.
			return stringBuilder.ToString();
		}
	}
}
