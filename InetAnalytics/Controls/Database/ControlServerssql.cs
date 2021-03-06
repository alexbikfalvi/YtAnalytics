﻿/* 
 * Copyright (C) 2012 Alex Bikfalvi
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
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InetAnalytics.Controls.Log;
using InetAnalytics.Forms.Database;
using InetCommon.Database;
using InetCommon.Log;
using InetControls.Controls.Database;
using InetCrawler;
using DotNetApi;
using DotNetApi.Windows.Controls;

namespace InetAnalytics.Controls.Database
{
	/// <summary>
	/// A class representing the database servers control.
	/// </summary>
	public partial class ControlServersSql : ControlBaseSql
	{
		/// <summary>
		/// A class storing the UI controls associated with a database server.
		/// </summary>
		protected sealed class ServerControls : IDisposable
		{
			private ListViewItem item;
			private TreeNode node;
			private ControlServerSql controlServer = new ControlServerSql();
			private ControlLog controlLog = new ControlLog();
			private ControlServerQuery controlQuery = new ControlServerQuery();

			/// <summary>
			/// Initializes the server controls object.
			/// </summary>
			/// <param name="item">The list view item.</param>
			/// <param name="node">The tree node.</param>
			public ServerControls(ListViewItem item, TreeNode node)
			{
				this.item = item;
				this.node = node;
			}

			/// <summary>
			/// Gets the list view item.
			/// </summary>
			public ListViewItem Item { get { return this.item; } }
			/// <summary>
			/// Gets the tree node.
			/// </summary>
			public TreeNode Node { get { return this.node; } }
			/// <summary>
			/// Gets the server control.
			/// </summary>
			public ControlServerSql ControlServer { get { return this.controlServer; } }
			/// <summary>
			/// Gets the log control.
			/// </summary>
			public ControlLog ControlLog { get { return this.controlLog; } }
			/// <summary>
			/// Gets the query control.
			/// </summary>
			public ControlServerQuery ControlQuery { get { return this.controlQuery; } }

			// Public methods.

			public void Dispose()
			{
				// Dispose the fields.
				this.controlServer.Dispose();
				this.controlLog.Dispose();
				this.controlQuery.Dispose();
				// Supress the finalizer for this object.
				GC.SuppressFinalize(this);
			}
		};

		private static readonly string logSource = "Database";

		private Crawler crawler;

		private readonly FormAddServerSql formAdd = new FormAddServerSql();
		private readonly FormServerProperties formProperties = new FormServerProperties();

		private readonly Dictionary<Guid, ServerControls> items = new Dictionary<Guid, ServerControls>();

		private TreeNode treeNode = null;
		private static readonly string[] imageKeys = { "ServerDown", "ServerUp", "ServerWarning", "ServerBusy", "ServerBusy", "ServerBusy" };
		private Control.ControlCollection controls = null;


		/// <summary>
		/// Creates a new instance of the control.
		/// </summary>
		public ControlServersSql()
		{
			// Initialize component.
			InitializeComponent();

			// Set the default control properties.
			this.Visible = false;
			this.Dock = DockStyle.Fill;
		}

		/// <summary>
		/// Initializes the control with a crawler instance.
		/// </summary>
		/// <param name="crawler">The crawler instance.</param>
		/// <param name="treeNode">The root tree node for the database servers.</param>
		/// <param name="controls">The panel where to add the server control.</param>
		/// <param name="imageList">The image list.</param>
		public void Initialize(Crawler crawler, TreeNode treeNode, Control.ControlCollection controls, ImageList imageList)
		{
			// Set the crawler.
			this.crawler = crawler;
			// Set the root tree node.
			this.treeNode = treeNode;
			// Set the image list.
			this.listView.SmallImageList = imageList;
			// Set the controls collection.
			this.controls = controls;

			// Set the log event handler for the database servers.
			this.crawler.Database.Sql.EventLogged += this.OnEventLogged;

			// Add all the servers in the configuration.
			foreach (DbServerSql server in this.crawler.Database.Sql)
			{
				this.AddServer(server);
			}

			// Add the event handlers for the servers.
			this.crawler.Database.Sql.ServerAdded += this.OnServerAdded;
			this.crawler.Database.Sql.ServerChanged += this.OnServerChanged;
			this.crawler.Database.Sql.ServerStateChanged += this.OnServerStateChanged;
			this.crawler.Database.Sql.PrimaryServerChanged += this.OnPrimaryServerChanged;
			this.crawler.Database.Sql.ServerRemoved += this.OnServerRemoved;

			// Reload the server configurations.
			this.crawler.Database.Sql.Reload();
		}

		// Private methods.
		
		/// <summary>
		/// Adds a new server to the servers control.
		/// </summary>
		/// <param name="server">The server.</param>
		private void AddServer(DbServerSql server)
		{
			// Create a new server item.
			ListViewItem item = new ListViewItem(new string[] {
					server.Name,
					this.crawler.Database.Sql.IsPrimary(server) ? "Primary" : "Backup",
					server.State.ToString(),
					server.Version,
					server.Id.ToString()
				});
			item.ImageKey = ControlServersSql.imageKeys[(int)server.State];
			item.Tag = server.Id;
			this.listView.Items.Add(item);
			// Create a new tree node for the server.
			TreeNode nodeServer = new TreeNode(this.GetServerTreeName(server));
			nodeServer.ImageKey = ControlServersSql.imageKeys[(int)server.State];
			nodeServer.SelectedImageKey = ControlServersSql.imageKeys[(int)server.State];
			this.treeNode.Nodes.Add(nodeServer);
			// Create a new tree node for the server query.
			TreeNode nodeQuery = new TreeNode("Query");
			nodeQuery.ImageKey = "QueryDatabase";
			nodeQuery.SelectedImageKey = "QueryDatabase";
			// Create a new tree node for the server log.
			TreeNode nodeLog = new TreeNode("Server log");
			nodeLog.ImageKey = "Log";
			nodeLog.SelectedImageKey = "Log";
			// Add the children nodes to the server node.
			nodeServer.Nodes.AddRange(new TreeNode[] {
					nodeQuery,
					nodeLog
				});
			this.treeNode.ExpandAll();

			// Create a new controls item.
			ServerControls controls = new ServerControls(item, nodeServer);

			// Initialize the server controls.
			controls.ControlServer.Initialize(this.crawler, server, nodeServer);
			controls.ControlLog.Initialize(this.crawler.Config, server.Log);
			controls.ControlQuery.Initialize(this.crawler, server);

			// Add the controls to the panel.
			this.controls.Add(controls.ControlServer);
			this.controls.Add(controls.ControlQuery);
			this.controls.Add(controls.ControlLog);

			// Set the tree nodes tag.
			nodeServer.Tag = controls.ControlServer;
			nodeQuery.Tag = controls.ControlQuery;
			nodeLog.Tag = controls.ControlLog;

			// Add the servers controls to the dictionary.
			this.items.Add(server.Id, controls);
		}

		/// <summary>
		/// Removes the server with the specified ID.
		/// </summary>
		/// <param name="id">The server ID.</param>
		private void RemoveServer(Guid id)
		{
			// Remove the menu item for the specified database server.
			this.listView.Items.Remove(this.items[id].Item);
			this.treeNode.Nodes.Remove(this.items[id].Node);

			// Remove the controls from the panel.
			this.controls.Remove(this.items[id].ControlServer);
			this.controls.Remove(this.items[id].ControlQuery);
			this.controls.Remove(this.items[id].ControlLog);

			// Dispose the controls.
			this.items[id].ControlServer.Dispose();
			this.items[id].ControlQuery.Dispose();
			this.items[id].ControlLog.Dispose();

			// Remove the controls entry from the dictionary.
			this.items.Remove(id);

			// Call the selected item change event to update the buttons.
			this.OnServerSelectionChanged(this, EventArgs.Empty);
		}

		/// <summary>
		/// Returns the server name to display in the tree view.
		/// </summary>
		/// <param name="server">The server.</param>
		/// <returns>The server name</returns>
		private string GetServerTreeName(DbServer server)
		{
			return server.Name + (this.crawler.Database.Sql.IsPrimary(server) ? " (primary)" : string.Empty);
		}

		/// <summary>
		/// An event handler called when a new server was added.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnServerAdded(object sender, DbServerEventArgs e)
		{
			// Execute the code on the UI thread.
			this.Invoke(() =>
				{
					// Add the server.
					this.AddServer(e.Server as DbServerSql);

					// Log the change.
					this.log.Add(this.crawler.Log.Add(
						LogEventLevel.Verbose,
						LogEventType.Success,
						ControlServersSql.logSource,
						"Database server with ID \'{0}\' and name \'{1}\' added. The server is {2}.",
						new object[] { e.Server.Id, e.Server.Name, this.crawler.Database.Sql.IsPrimary(e.Server) ? "primary" : "backup" }));

					// Hide the message.
					this.HideMessage();
				});
		}

		/// <summary>
		/// An event handler called when a server was removed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnServerRemoved(object sender, DbIdEventArgs e)
		{
			// Execute the code on the UI thread.
			this.Invoke(() =>
				{
					// Remove the server.
					this.RemoveServer(e.Id);

					// Log the change.
					this.log.Add(this.crawler.Log.Add(
						LogEventLevel.Verbose,
						LogEventType.Success,
						ControlServersSql.logSource,
						"Database server with ID \'{0}\' was removed.",
						new object[] { e.Id }));
				});
		}

		/// <summary>
		/// An event handler called when a server configuration has changed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnServerChanged(object sender, DbServerEventArgs e)
		{
			// Execute the code on the UI thread.
			this.Invoke(() =>
				{
					// Update the server information.

					// Get the controls corresponding to this server.
					ServerControls controls = this.items[e.Server.Id];
					// Update the server information.
					controls.Item.SubItems[0].Text = e.Server.Name;
					controls.Item.SubItems[2].Text = e.Server.State.ToString();
					controls.Item.SubItems[3].Text = e.Server.Version;
					controls.Node.Text = this.GetServerTreeName(e.Server);

					// Call the selected item change event to update the buttons.
					this.OnServerSelectionChanged(this, e);
				});
		}

		/// <summary>
		/// An event handler called when the state of a server connection has changed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		void OnServerStateChanged(object sender, DbServerStateEventArgs e)
		{
			// Execute the code on the UI thread.
			this.Invoke(() =>
				{
					// If there are no controls for this server, ignore the event.
					if (!this.items.ContainsKey(e.Server.Id)) return;

					// Get the controls corresponding to this server.
					ServerControls controls = this.items[e.Server.Id];

					// Update the list view item.
					controls.Item.SubItems[2].Text = e.Server.State.ToString();
					controls.Item.SubItems[3].Text = e.Server.Version;
					controls.Item.ImageKey = ControlServersSql.imageKeys[(int)e.Server.State];

					// Update the tree node.
					controls.Node.ImageKey = ControlServersSql.imageKeys[(int)e.Server.State];
					controls.Node.SelectedImageKey = ControlServersSql.imageKeys[(int)e.Server.State];

					// Call the selected item change event to update the buttons.
					this.OnServerSelectionChanged(this, e);
				});
		}

		/// <summary>
		/// An event handler called when the primary server has changed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnPrimaryServerChanged(object sender, DbPrimaryServerChangedEventArgs e)
		{
			// Execute the code on the UI thread.
			this.Invoke(() =>
				{
					// Update the old primary server, if not null.
					if (null != e.OldPrimary)
					{
						if (this.items.ContainsKey(e.OldPrimary.Id))
						{
							ServerControls controls = this.items[e.OldPrimary.Id];
							controls.Item.SubItems[1].Text = "Backup";
							controls.Node.Text = this.GetServerTreeName(e.OldPrimary);
						}
					}

					// Update the new primary server, if not null.
					if (null != e.NewPrimary)
					{
						if (this.items.ContainsKey(e.NewPrimary.Id))
						{
							ServerControls controls = this.items[e.NewPrimary.Id];
							controls.Item.SubItems[1].Text = "Primary";
							controls.Node.Text = this.GetServerTreeName(e.NewPrimary);
						}
					}

					// Call the selected item change event to update the buttons.
					this.OnServerSelectionChanged(this, EventArgs.Empty);

					// Log the change.
					this.log.Add(this.crawler.Log.Add(
						LogEventLevel.Verbose,
						LogEventType.Information,
						ControlServersSql.logSource,
						"Primary database server has changed from \'{0}\' to \'{1}\'.",
						new object[] {
							e.OldPrimary != null ? e.OldPrimary.Id.ToString() : string.Empty,
							e.NewPrimary != null ? e.NewPrimary.Id.ToString() : string.Empty
						}));
				});
		}

		/// <summary>
		/// An event handler called when adding a new database server.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnAdd(object sender, EventArgs e)
		{
			// Show the server add dialog.
			if (this.formAdd.ShowDialog(
				this,
				this.crawler.Database.Sql.Count == 0 ? true : false,
				this.crawler.Database.Sql.Count == 0 ? false : true
				) == DialogResult.OK)
			{
				// Show a message.
				this.ShowMessage(Resources.ServerAdd_48, "Database", "Adding a new database server...");

				// Check if the new server changes the primary server.
				bool primary = this.formAdd.IsPrimary;
				if (primary && this.crawler.Database.Sql.HasPrimary)
				{
					// If the user does not confirm changing the primary server.
					if (DialogResult.No == MessageBox.Show(
						this,
						"The new database server is marked as primary, but a different primary server already exists. Do you want to change the primary server?",
						"Confirm Changing the Primary Server",
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Question,
						MessageBoxDefaultButton.Button2))
					{
						// Set the primary to false.
						primary = false;
					}
				}
				try
				{
					// Try add the new server.
					this.crawler.Database.Sql.Add(
						this.formAdd.Type,
						this.formAdd.ServerName,
						this.formAdd.DataSource,
						this.formAdd.Username,
						this.formAdd.Password,
						primary);
				}
				catch (Exception exception)
				{
					// Hide the message.
					this.HideMessage();
					// Catch all exceptions and show an error message.
					MessageBox.Show(this, exception.Message, "Add Database Server Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		/// <summary>
		/// An event handler called when removing an existing server.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnRemove(object sender, EventArgs e)
		{
			// If there are no selected items, do nothing.
			if (this.listView.SelectedItems.Count == 0) return;

			// Get the selected server.
			DbServerSql server = this.crawler.Database.Sql[(Guid)this.listView.SelectedItems[0].Tag];

			// If there are more than one server, and the selected server is a primary server ask the user to change the primary.
			if (this.crawler.Database.Sql.IsPrimary(server) && (this.crawler.Database.Sql.Count > 1))
			{
				MessageBox.Show(
					this,
					"Change the primary database server before removing the current server.",
					"Cannot Remove Primary Database Server",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
				return;
			}

			// Ask the user to confirm removing the server.
			if (DialogResult.Yes == MessageBox.Show(
				this,
				"You are remove the selected database server. Do you want to continue?",
				"Confirm Removing the Database Server",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2))
			{
				// Try removing the server asynchronously. The server will be removed only after the connection to the server is closed.
				try
				{
					// Show a message to the user.
					this.ShowMessage(
						Resources.ServerRemove_48,
						"Database",
						"Removing the database server with ID \'{0}\'.{1}The server will be removed only after the current connection to the server is closed.".FormatWith(server.Id, Environment.NewLine)
						);
					// Begin an asynchronous remove of the database server.
					this.crawler.Database.Sql.RemoveAsync(server, this.OnRemoveComplete);
				}
				catch (Exception exception)
				{
					// Display a message if an exception occurs.
					MessageBox.Show(
						this,
						exception.Message,
						"Database Server Removal Failed",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);
				}
			}
		}

		/// <summary>
		/// A callback function for when the removal operation completed, either successfully or unsuccessfully.
		/// </summary>
		/// <param name="state">The asynchronous state.</param>
		private void OnRemoveComplete(DbServerAsyncState state)
		{
			// Execute the code on the UI thread.
			this.Invoke(() =>
				{
					// Hide the message.
					this.HideMessage();
					// If the exception is not null, display an error message to the user.
					if (state.Exception != null)
					{
						MessageBox.Show(
							this,
							state.Exception.Message,
							"Database Server Removal Failed",
							MessageBoxButtons.OK,
							MessageBoxIcon.Error);
					}
				});
		}

		/// <summary>
		/// An event handler called when changing the primary server.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnMakePrimary(object sender, EventArgs e)
		{
			// If there are no selected items, do nothing.
			if (this.listView.SelectedItems.Count == 0) return;

			// Ask the user to confirm changing the primary server.
			if (DialogResult.Yes == MessageBox.Show(
				this,
				"You are changing the primary database server. Do you want to continue? Database information will not be copied.",
				"Confirm Changing the Primary Server",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2))
			{
				// Get the server.
				DbServerSql server = this.crawler.Database.Sql[(Guid)this.listView.SelectedItems[0].Tag];
				// Change the primary server.
				this.crawler.Database.Sql.SetPrimary(server);
			}
		}

		/// <summary>
		/// An event handler called when connecting to a database server.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnConnect(object sender, EventArgs e)
		{
			// If there are no selected items, do nothing.
			if (this.listView.SelectedItems.Count == 0) return;

			// Get the selected server.
			DbServerSql server = this.crawler.Database.Sql[(Guid)this.listView.SelectedItems[0].Tag];

			// Connect the database server.
			this.DatabaseConnect(server);
		}

		/// <summary>
		/// An event handler called when disconnecting from a database server.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnDisconnect(object sender, EventArgs e)
		{
			// If there are no selected items, do nothing.
			if (this.listView.SelectedItems.Count == 0) return;

			// Get the selected server.
			DbServerSql server = this.crawler.Database.Sql[(Guid)this.listView.SelectedItems[0].Tag];

			// Disconnect the database server.
			this.DatabaseDisconnect(server);
		}

		/// <summary>
		/// An event handler called when the user changes the password.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnChangePassword(object sender, EventArgs e)
		{
			// If there are no selected items, do nothing.
			if (this.listView.SelectedItems.Count == 0) return;

			// Get the selected server.
			DbServerSql server = this.crawler.Database.Sql[(Guid)this.listView.SelectedItems[0].Tag];

			// Change the password for the selected server.
			this.DatabaseChangePassword(server);
		}

		/// <summary>
		/// An event handler called when the server selection has changed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnServerSelectionChanged(object sender, EventArgs e)
		{
			bool remove = false;
			bool primary = false;
			bool connect = false;
			bool disconnect = false;
			bool changePassword = false;
			if (this.listView.SelectedItems.Count != 0)
			{
				// Get the server corresponding to this item.
				DbServerSql server = this.crawler.Database.Sql[(Guid)this.listView.SelectedItems[0].Tag];

				remove =
					(server.State == DbServerSql.ServerState.Disconnected) ||
					(server.State == DbServerSql.ServerState.Failed);
				primary = !this.crawler.Database.Sql.IsPrimary(server);
				connect =
					(server.State == DbServerSql.ServerState.Disconnected) ||
					(server.State == DbServerSql.ServerState.Failed);
				disconnect = server.State == DbServerSql.ServerState.Connected;
				changePassword =
					(server.State == DbServerSql.ServerState.Disconnected) ||
					(server.State == DbServerSql.ServerState.Failed);
			}
			this.buttonRemove.Enabled = remove;
			this.buttonPrimary.Enabled = primary;
			this.buttonConnect.Enabled = connect;
			this.buttonDisconnect.Enabled = disconnect;
			this.buttonChangePassword.Enabled = changePassword;
			this.menuItemPrimary.Enabled = primary;
			this.menuItemConnect.Enabled = connect;
			this.menuItemDisconnect.Enabled = disconnect;
			this.menuItemChangePassword.Enabled = changePassword;
		}

		/// <summary>
		/// An event handler called when displaying the properties of a database server.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnProperties(object sender, EventArgs e)
		{
			if (this.listView.SelectedItems.Count == 0) return;

			// Get the server.
			DbServerSql server = this.crawler.Database.Sql[(Guid)this.listView.SelectedItems[0].Tag];

			// Show the properties dialog.
			this.formProperties.ShowDialog(this, server, this.crawler.Database.Sql.IsPrimary(server));
		}

		/// <summary>
		/// An event handler called when the user clicks on a list view item.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnMouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				if (this.listView.FocusedItem != null)
				{
					if (this.listView.FocusedItem.Bounds.Contains(e.Location))
					{
						this.contextMenu.Show(this.listView, e.Location);
					}
				}
			}
		}

		/// <summary>
		/// An event handler called when a server raises a log event.
		/// </summary>
		/// <param name="server">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		void OnEventLogged(object sender, LogEventArgs e)
		{
			// Execute the code on the UI thread.
			this.Invoke(() =>
				{
					this.log.Add(e.Event);
				});
		}
	}
}
