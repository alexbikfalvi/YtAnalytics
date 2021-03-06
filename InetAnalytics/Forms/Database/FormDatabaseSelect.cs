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
using System.Windows.Forms;
using InetAnalytics.Events;
using InetCommon.Database;
using InetCommon.Database.Data;
using DotNetApi.Windows;
using DotNetApi.Windows.Forms;

namespace InetAnalytics.Forms.Database
{
	/// <summary>
	/// A form dialog allowing the selection of an object from the database.
	/// </summary>
	public partial class FormDatabaseSelect : ThreadSafeForm
	{
		private bool canClose = true;

		/// <summary>
		/// Creates a new form instance.
		/// </summary>
		public FormDatabaseSelect()
		{
			InitializeComponent();

			// Set the font.
			Window.SetFont(this);
		}

		// Public properties.

		/// <summary>
		/// The database selected result.
		/// </summary>
		public DbObject SelectedResult { get; private set; }
		/// <summary>
		/// The database all results.
		/// </summary>
		public DbDataObject AllResults { get; private set; }

		// Public methods.

		/// <summary>
		/// Opens the modal dialog to select database objects from the specified database server and table.
		/// </summary>
		/// <typeparam name="T">The database object type.</typeparam>
		/// <param name="owner">The window owner.</param>
		/// <param name="server">The database server.</param>
		/// <param name="table">The database table.</param>
		/// <param name="result">The result to display when the dialog opens.</param>
		/// <returns>The dialog result.</returns>
		public DialogResult ShowDialog(IWin32Window owner, DbServerSql server, ITable table, DbDataObject result)
		{
			// Reset the result.
			this.SelectedResult = null;
			this.AllResults = result;
			// Initialize the control.
			this.control.Select(server, table, result);
			// Show the dialog.
			return base.ShowDialog(owner);
		}

		/// <summary>
		/// Opens the modal dialog to select database objects from the specified database server and query.
		/// </summary>
		/// <typeparam name="T">The database object type.</typeparam>
		/// <param name="owner">The window owner.</param>
		/// <param name="server">The database server.</param>
		/// <param name="query">The database query.</param>
		/// <param name="result">The result to display when the dialog opens.</param>
		/// <returns>The dialog result.</returns>
		public DialogResult ShowDialog(IWin32Window owner, DbServerSql server, DbQuerySql query, DbDataObject result)
		{
			// Reset the result.
			this.SelectedResult = null;
			this.AllResults = result;
			// Initialize the control.
			this.control.Select(server, query, result);
			// Show the dialog.
			return base.ShowDialog(owner);
		}

		// Private methods.

		/// <summary>
		/// Shows the form.
		/// </summary>
		private new void Show()
		{
			base.Show();
		}

		/// <summary>
		/// Shows the form.
		/// </summary>
		/// <param name="owner">The owner.</param>
		private new void Show(IWin32Window owner)
		{
			base.Show(owner);
		}

		/// <summary>
		/// Shows the dialog.
		/// </summary>
		/// <returns>The dialog result.</returns>
		private new DialogResult ShowDialog()
		{
			return base.ShowDialog();
		}

		/// <summary>
		/// Shows the dialog.
		/// </summary>
		/// <param name="owner">The owner.</param>
		/// <returns>The dialog result.</returns>
		private new DialogResult ShowDialog(IWin32Window owner)
		{
			return base.ShowDialog(owner);
		}

		/// <summary>
		/// An event handler called when starting to refresh the list of database objects.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnRefreshStarted(object sender, EventArgs e)
		{
			this.canClose = false;
		}

		/// <summary>
		/// An event handler called when finishing to refresh the list of database objects.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnRefreshFinished(object sender, EventArgs e)
		{
			this.canClose = true;
		}

		/// <summary>
		/// An event handler called when the user selects the database object.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnSelected(object sender, DatabaseObjectSelectedEventArgs e)
		{
			// Set the result.
			this.SelectedResult = e.SelectedResult;
			this.AllResults = e.AllResults;
			// Set the dialog result.
			this.DialogResult = DialogResult.OK;
			// Close the form.
			this.Close();
		}

		/// <summary>
		/// An event handler called when the user closes the dialog.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnClosed(object sender, EventArgs e)
		{
			// Set the dialog result.
			this.DialogResult = DialogResult.Cancel;
			// Close the form.
			this.Close();
		}

		/// <summary>
		/// An event handler called when the user is closing the dialog.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			// If the form cannot be closed.
			if (!this.canClose)
			{
				// Cancel the closing.
				e.Cancel = true;
			}
		}

		/// <summary>
		/// An event handler called when a database operation has started.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnDatabaseOperationStarted(object sender, EventArgs e)
		{
			this.canClose = false;
		}

		/// <summary>
		/// An event handler called when a database operation has finished.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnDatabaseOperationFinished(object sender, EventArgs e)
		{
			this.canClose = true;
		}

		/// <summary>
		/// An event handler called when the form loads.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnLoad(object sender, EventArgs e)
		{
			// If the current result is null, refresh the results list.
			if (this.AllResults == null) this.control.RefreshList();
		}
	}
}
