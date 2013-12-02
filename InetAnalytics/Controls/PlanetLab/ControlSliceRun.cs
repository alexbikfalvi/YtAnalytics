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
using System.Windows.Forms;
using DotNetApi;
using PlanetLab;
using PlanetLab.Api;
using InetAnalytics.Forms.PlanetLab;
using InetCrawler;
using InetCrawler.PlanetLab;
using InetCrawler.Status;

namespace InetAnalytics.Controls.PlanetLab
{
	/// <summary>
	/// A control class for executing a program on a PlanetLab slice.
	/// </summary>
	public sealed partial class ControlSliceRun : ControlRequest
	{
		public static readonly string[] nodeImageKeys = new string[]
		{
			"NodeUnknown", "NodeBoot", "NodeSafeBoot", "NodeDisabled", "NodeReinstall"
		};

		// Private variables.

		private Crawler crawler = null;
		private CrawlerStatusHandler status = null;

		private PlSlice slice = null;
		private PlConfigSlice config = null;

		private TreeNode treeNode = null;

		private readonly RequestState requestStateGetSlice;

		private readonly FormAddCommand formAddCommand = new FormAddCommand();

		// Public declarations

		/// <summary>
		/// Creates a new control instance.
		/// </summary>
		public ControlSliceRun()
		{
			// Initialize component.
			InitializeComponent();

			// Set the default control properties.
			this.Visible = false;
			this.Dock = DockStyle.Fill;

			// Create the get slices request state.
			//this.requestStateGetSlice = new RequestState(
			//	null,
			//	this.OnRefreshSliceRequestResult,
			//	this.OnRefreshSliceRequestCanceled,
			//	this.OnRefreshSliceRequestException,
			//	null);
		}

		// Public events.

		/// <summary>
		/// An event raised when a console is selected.
		/// </summary>
		//public event PageSelectionEventHandler ConsoleSelected;

		// Public methods.

		/// <summary>
		/// Initializes the control with a crawler object.
		/// </summary>
		/// <param name="crawler">The crawler object.</param>
		/// <param name="slice">The slice.</param>
		/// <param name="treeNode">The tree node.</param>
		public void Initialize(Crawler crawler, PlSlice slice, TreeNode treeNode)
		{
			// Save the parameters.
			this.crawler = crawler;

			// Get the status handler.
			this.status = this.crawler.Status.GetHandler(this);

			// Set the slice.
			this.slice = slice;
			this.slice.Changed += this.OnSliceChanged;

			// Set the slice configuration.
			this.config = this.crawler.PlanetLab.GetSliceConfiguration(this.slice);

			// Set the tree node.
			this.treeNode = treeNode;

			// Set the title.
			this.panelRun.Title = "Run on PlanetLab Slice ({0})".FormatWith(this.slice.Name);

			// Enable the control.
			this.Enabled = true;

			// Update the information of the PlanetLab slice.
			this.OnUpdateSlice();
		}

		// Protected methods.

		/// <summary>
		/// An event handler called when the current request begins, and the notification box is displayed.
		/// </summary>
		/// <param name="state">The request state.</param>
		protected override void OnRequestStarted(RequestState state)
		{
			// Set the button enabled state.
			this.buttonRefresh.Enabled = false;
			this.buttonCancel.Enabled = true;
			// Call the base class method.
			base.OnRequestStarted(state);
		}

		/// <summary>
		/// An event handler called when the current request finishes, and the notification box is hidden.
		/// </summary>
		/// <param name="state">The request state.</param>
		protected override void OnRequestFinished(RequestState state)
		{
			// Set the button enabled state.
			this.buttonRefresh.Enabled = true;
			this.buttonCancel.Enabled = false;
			// Call the node selection changed event handler.
			this.OnNodeSelectionChanged(this, EventArgs.Empty);
			// Call the base class method.
			base.OnRequestFinished(state);
		}

		// Private methods.

		/// <summary>
		/// Updates the information of the current PlanetLab slice.
		/// </summary>
		private void OnUpdateSlice()
		{
			// Set the slice information.
			//this.textBoxName.Text = this.slice.Name;
			//this.textBoxDescription.Text = this.slice.Description;
			//this.textBoxUrl.Text = this.slice.Url;

			//this.textBoxCreated.Text = this.slice.Created.HasValue ? this.slice.Created.Value.ToString() : string.Empty;
			//this.textBoxExpires.Text = this.slice.Expires.HasValue ? this.slice.Expires.Value.ToString() : string.Empty;
			//this.textBoxMaxNodes.Text = this.slice.MaxNodes.HasValue ? this.slice.MaxNodes.Value.ToString() : string.Empty;

			// Update the list of nodes.
			this.OnUpdateNodes();

			// Update the label.
			this.status.Send(CrawlerStatus.StatusType.Normal, @"Slice '{0}' has {1} node{2}.".FormatWith(this.slice.Name, this.slice.NodeIds.Length, this.slice.NodeIds.Length.PluralSuffix()), Resources.GlobeLab_16);
		}

		/// <summary>
		/// Updates the list of slice nodes.
		/// </summary>
		private void OnUpdateNodes()
		{
			// Clear the current nodes.
			this.OnClearNodes();

			//// Synchronize access to the pending lists.
			//lock (this.pendingSync)
			//{
			//	// Clear the list of pending nodes.
			//	this.pendingNodes.Clear();
			//	this.pendingSites.Clear();

			//	// Add the list of nodes.
			//	foreach (int nodeId in this.slice.NodeIds)
			//	{
			//		// The node.
			//		PlNode node = this.crawler.PlanetLab.DbNodes.Find(nodeId);
			//		// The site identifier.
			//		int? siteId = null;
			//		// The site.
			//		PlSite site = null;

			//		// If the node is not null.
			//		if (null != node)
			//		{
			//			// Get the site identifier.
			//			siteId = node.SiteId;
			//			// Add a node event handler.
			//			node.Changed += this.OnNodeChanged;

			//			// If the node has a site identifier.
			//			if (node.SiteId.HasValue)
			//			{
			//				// Get the site from the database.
			//				site = this.crawler.PlanetLab.DbSites.Find(node.SiteId.Value);
			//				// If the site is not null.
			//				if (null != site)
			//				{
			//					// Add a site event handler.
			//					site.Changed += this.OnSiteChanged;
			//				}
			//				else
			//				{
			//					// Add the site to the pending sites.
			//					this.pendingSites.Add(node.SiteId.Value);
			//				}
			//			}
			//		}
			//		else
			//		{
			//			// Add the node ID to the pending list.
			//			this.pendingNodes.Add(nodeId);
			//		}

			//		// Create a new geo marker for this site.
			//		MapMarker marker = null;
			//		if (null != site)
			//		{
			//			// If the site has coordinates.
			//			if (site.Latitude.HasValue && site.Longitude.HasValue)
			//			{
			//				// Create a circular marker.
			//				marker = new MapBulletMarker(new MapPoint(site.Longitude.Value, site.Latitude.Value));
			//				marker.Name = "{0}{1}{2}".FormatWith(node.Hostname, Environment.NewLine, site.Name);
			//				// Add the marker to the map.
			//				this.mapControl.Markers.Add(marker);
			//			}
			//		}

			//		// Create the node information.
			//		NodeInfo info = new NodeInfo(nodeId, siteId, node, site, marker);

			//		// Create a list item.
			//		ListViewItem item = new ListViewItem(new string[] {
			//			nodeId.ToString(),
			//			node != null ? node.Hostname : string.Empty,
			//			ControlSsh.ClientState.Disconnected.ToString()
			//		});
			//		item.ImageKey = node != null ? ControlSliceRun.nodeImageKeys[(int)node.GetBootState()] : ControlSliceRun.nodeImageKeys[0];
			//		item.Tag = info;

			//		// Add the list item.
			//		this.listViewNodes.Items.Add(item);

			//		// If the marker is not null, set the marker tag.
			//		if (null != marker)
			//		{
			//			marker.Tag = item;
			//		}
			//	}

			//	// Refresh the pending nodes and sites.
			//	if (this.pendingNodes.Count > 0)
			//	{
			//		this.OnRefreshNodes();
			//	}
			//	else if (this.pendingSites.Count > 0)
			//	{
			//		this.OnRefreshSites();
			//	}
			//}
		}

		/// <summary>
		/// An event handler called when the current slice has changed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnSliceChanged(object sender, PlObjectEventArgs e)
		{
			// Update the slice information.
			this.OnUpdateSlice();
		}

		/// <summary>
		/// An event handler called when a PlanetLab node has changed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnNodeChanged(object sender, PlObjectEventArgs e)
		{
			//// Get the node.
			//PlNode node = e.Object as PlNode;
			//// Get the list item corresponding to the selected node.
			//ListViewItem item = this.listViewNodes.Items.FirstOrDefault((ListViewItem it) =>
			//	{
			//		// Get the node info.
			//		NodeInfo info = it.Tag as NodeInfo;
			//		// Check the tag node equals the current node.
			//		return info.NodeId == node.Id;
			//	});
			//// Update the item information.
			//if (null != item)
			//{
			//	// Get the node information.
			//	NodeInfo info = item.Tag as NodeInfo;

			//	// Set the item information.
			//	item.SubItems[0].Text = node.Id.Value.ToString();
			//	item.SubItems[1].Text = node.Hostname;
			//	item.ImageKey = ControlSliceRun.nodeImageKeys[(int)node.GetBootState()];
			//}
		}

		/// <summary>
		/// An event handler called when a PlanetLab site has changed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnSiteChanged(object sender, PlObjectEventArgs e)
		{
			//// Get the site.
			//PlSite site = e.Object as PlSite;
			//// Get the list item corresponding to the selected node.
			//ListViewItem item = this.listViewNodes.Items.FirstOrDefault((ListViewItem it) =>
			//{
			//	// Get the node info.
			//	NodeInfo info = it.Tag as NodeInfo;
			//	// Check the tag node equals the current node.
			//	return object.ReferenceEquals(info.Site, site);
			//});
			//// Update the item information.
			//if (null != item)
			//{
			//	// Get the node information.
			//	NodeInfo info = item.Tag as NodeInfo;

			//	// If the marker is not null.
			//	if (null != info.Marker)
			//	{
			//		// Update the marker location.
			//		info.Marker.Location = new MapPoint(site.Longitude.Value, site.Latitude.Value);
			//	}
			//	else
			//	{
			//		// If the site has coordinates.
			//		if (site.Latitude.HasValue && site.Longitude.HasValue)
			//		{
			//			// Create a circular marker.
			//			info.Marker = new MapBulletMarker(new MapPoint(site.Longitude.Value, site.Latitude.Value));
			//			info.Marker.Name = "{0}{1}{2}".FormatWith(info.Node.Hostname, Environment.NewLine, site.Name);
			//			// Add the marker to the map.
			//			this.mapControl.Markers.Add(marker);
			//		}
			//	}
			//}
		}

		/// <summary>
		/// An event handler called when clearing the list of nodes.
		/// </summary>
		private void OnClearNodes()
		{
			//// Disable the node buttons.
			//this.buttonConnect.Enabled = false;
			//this.buttonDisconnect.Enabled = false;
			//this.buttonProperties.Enabled = false;
			//this.menuItemConnect.Enabled = false;
			//this.menuItemDisconnect.Enabled = false;
			//this.menuItemNodeProperties.Enabled = false;
			//this.menuItemSiteProperties.Enabled = false;

			//// Clear the map markers.
			//this.mapControl.Markers.Clear();

			//// For all node items.
			//foreach (ListViewItem item in this.listViewNodes.Items)
			//{
			//	// Get the node info.
			//	NodeInfo info = item.Tag as NodeInfo;
			//	// Remove the event handlers.
			//	if (info.Node != null)
			//	{
			//		info.Node.Changed -= this.OnNodeChanged;
			//	}
			//	if (info.Site != null)
			//	{
			//		info.Site.Changed -= this.OnSiteChanged;
			//	}
			//	// Dispose the map marker.
			//	if (info.Marker != null)
			//	{
			//		info.Marker.Dispose();
			//	}
			//	// Close the control.
			//	if (info.ConsoleControl != null)
			//	{
			//		// Get the console control.
			//		ControlSession control = info.ConsoleControl;
			//		// Close the control.
			//		this.OnConsoleClose(item);
			//	}
			//}

			//// Clear the list.
			//this.listViewNodes.Items.Clear();
		}

		/// <summary>
		/// An event handler called when disposing the list of nodes.
		/// </summary>
		private void OnDisposeNodes()
		{
		//	// For all node items.
		//	foreach (ListViewItem item in this.listViewNodes.Items)
		//	{
		//		// Get the node info.
		//		NodeInfo info = item.Tag as NodeInfo;
		//		// Remove the event handlers.
		//		if (info.Node != null)
		//		{
		//			info.Node.Changed -= this.OnNodeChanged;
		//		}
		//		if (info.Site != null)
		//		{
		//			info.Site.Changed -= this.OnSiteChanged;
		//		}
		//		// Dispose the map marker.
		//		if (info.Marker != null)
		//		{
		//			info.Marker.Dispose();
		//		}
		//		// Close the control.
		//		if (info.ConsoleControl != null)
		//		{
		//			// Get the console control.
		//			ControlSession control = info.ConsoleControl;
		//			// Close the control.
		//			this.OnConsoleClose(item);
		//		}
		//	}
		}

		/// <summary>
		/// An event handler called when the site selection has changed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnNodeSelectionChanged(object sender, EventArgs e)
		{
			//// If there exists an emphasized marker, de-emphasize it.
			//if (this.marker != null)
			//{
			//	this.marker.Emphasized = false;
			//	this.marker = null;
			//}
			//// If no site is selected.
			//if (this.listViewNodes.SelectedItems.Count == 0)
			//{
			//	// Change the buttons enabled state.
			//	this.buttonRemoveFromNodes.Enabled = false;
			//	this.buttonConnect.Enabled = false;
			//	this.buttonDisconnect.Enabled = false;
			//	this.buttonProperties.Enabled = false;
			//	this.menuItemConnect.Enabled = false;
			//	this.menuItemDisconnect.Enabled = false;
			//	this.menuItemNodeProperties.Enabled = false;
			//	this.menuItemSiteProperties.Enabled = false;
			//}
			//else
			//{
			//	// Get the node info for this item.
			//	NodeInfo info = this.listViewNodes.SelectedItems[0].Tag as NodeInfo;
			//	// If the node has a control.
			//	if (null != info.ConsoleControl)
			//	{
			//		// Change the button enabled state.
			//		this.buttonConnect.Enabled = info.ConsoleControl.State == ControlSsh.ClientState.Disconnected;
			//		this.buttonDisconnect.Enabled = info.ConsoleControl.State == ControlSsh.ClientState.Connected;
			//		this.menuItemConnect.Enabled = info.ConsoleControl.State == ControlSsh.ClientState.Disconnected;
			//		this.menuItemDisconnect.Enabled = info.ConsoleControl.State == ControlSsh.ClientState.Connected;
			//	}
			//	else
			//	{
			//		// Change the button enabled state.
			//		this.buttonConnect.Enabled = true;
			//		this.buttonDisconnect.Enabled = false;
			//		this.menuItemConnect.Enabled = true;
			//		this.menuItemDisconnect.Enabled = false;
			//	}
			//	this.buttonRemoveFromNodes.Enabled = true;
			//	this.buttonProperties.Enabled = true;
			//	this.buttonNodeProperties.Enabled = true;
			//	this.buttonSiteProperties.Enabled = info.Node != null;
			//	this.menuItemNodeProperties.Enabled = true;
			//	this.menuItemSiteProperties.Enabled = info.Node != null;

			//	// If the marker is not null, emphasize the marker.
			//	if (null != info.Marker)
			//	{
			//		this.marker = info.Marker;
			//		this.marker.Emphasized = true;
			//	}
			//}
		}

		/// <summary>
		/// An event handler called when the user refreshes PlanetLab slice information.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnRefreshSlice(object sender, EventArgs e)
		{
		//	// If there is no validated PlanetLab person account, show a message and return.
		//	if (-1 == this.crawler.PlanetLab.PersonId)
		//	{
		//		MessageBox.Show(this, "You must set and validate a PlanetLab account in the settings page before configuring the PlanetLab slices.", "PlanetLab Account Not Configured", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//		return;
		//	}

		//	// Warn the user about the refresh.
		//	if (MessageBox.Show(
		//		this,
		//		@"You will now refresh the information for slice '{0}'. This will remove the slice configuration and disconnect all current sessions. Click Yes to continue.".FormatWith(this.slice.Name),
		//		"Refresh PlanetLab Slice",
		//		MessageBoxButtons.YesNo,
		//		MessageBoxIcon.Question,
		//		MessageBoxDefaultButton.Button2) == DialogResult.No)
		//	{
		//		return;
		//	}

		//	// Refresh the slice.
		//	this.OnRefreshSlice();
		}

		/// <summary>
		/// Refreshes the information of the current slice.
		/// </summary>
		//private void OnRefreshSlice()
		//{
		//	// Update the status.
		//	this.status.Send(CrawlerStatus.StatusType.Busy, "Refreshing the slice information...", Resources.GlobeClock_16);

		//	// Begin an asynchronous PlanetLab request.
		//	this.BeginRequest(
		//		this.requestGetSlices,
		//		this.crawler.PlanetLab.Username,
		//		this.crawler.PlanetLab.Password,
		//		PlSlice.GetFilter(PlSlice.Fields.SliceId, this.slice.SliceId),
		//		this.requestStateGetSlice);
		//}

		/// <summary>
		/// An event handler called when the user cancels the update of PlanetLab slice.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnCancel(object sender, EventArgs e)
		{
		//	// Disable the cancel button.
		//	this.buttonCancel.Enabled = false;
		//	// Cancel the request.
		//	this.CancelRequest();
		}

		/// <summary>
		/// A method called when receiving the response to a slices refresh request.
		/// </summary>
		/// <param name="response">The response.</param>
		/// <param name="state">The request state.</param>
		//private void OnRefreshSliceRequestResult(XmlRpcResponse response, RequestState state)
		//{
		//	// If the request has not failed.
		//	if ((null == response.Fault) && (null != response.Value))
		//	{
		//		// Get the slices array.
		//		XmlRpcArray slices = response.Value as XmlRpcArray;

		//		// If the response array has one element.
		//		if ((null != slices) && (slices.Length == 1))
		//		{
		//			try
		//			{
		//				// Update the current slice.
		//				this.slice.Parse(slices.Values[0].Value as XmlRpcStruct);
		//				// Return.
		//				return;
		//			}
		//			catch { }
		//		}
		//	}
		//	// Update the status.
		//	this.status.Send(CrawlerStatus.StatusType.Normal, "Refreshing the slice information failed.", Resources.GlobeError_16);
		//}

		/// <summary>
		/// A method called when the get slices request has been canceled.
		/// </summary>
		/// <param name="state">The request state.</param>
		//private void OnRefreshSliceRequestCanceled(RequestState state)
		//{
		//	// Update the status.
		//	this.status.Send(CrawlerStatus.StatusType.Normal, "Refreshing the slice information was canceled.", Resources.GlobeCanceled_16);
		//}

		/// <summary>
		/// A method called when the get slices request returned an exception.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="state">The request state.</param>
		//private void OnRefreshSliceRequestException(Exception exception, RequestState state)
		//{
		//	// Update the status.
		//	this.status.Send(CrawlerStatus.StatusType.Normal, "Refreshing the slice information failed.", Resources.GlobeError_16);
		//}

		/// <summary>
		/// An event handler called when the list of nodes is refreshed.
		/// </summary>
		//private void OnRefreshNodes()
		//{
		//	lock (this.pendingSync)
		//	{
		//		// If there are no nodes to refresh, do nothing.
		//		if (this.pendingNodes.Count == 0) return;

		//		// Update the status.
		//		this.status.Send(
		//			CrawlerStatus.StatusType.Busy,
		//			@"Slice '{0}' has {1} node{2}.".FormatWith(this.slice.Name, this.slice.NodeIds.Length, this.slice.NodeIds.Length.PluralSuffix()),
		//			"Refreshing the nodes information...",
		//			Resources.GlobeLab_16,
		//			Resources.GlobeClock_16);

		//		// Create the request state.
		//		IdsRequestState requestState = new IdsRequestState(
		//			null,
		//			this.OnRefreshNodesRequestResult,
		//			this.OnRefreshNodesRequestCanceled,
		//			this.OnRefreshNodesRequestException,
		//			this.OnRefreshNodesRequestFinished,
		//			this.pendingNodes.ToArray());

		//		// Begin an asynchronous PlanetLab request.
		//		this.BeginRequest(
		//			this.requestGetNodes,
		//			this.crawler.PlanetLab.Username,
		//			this.crawler.PlanetLab.Password,
		//			PlNode.GetFilter(PlNode.Fields.NodeId, requestState.Ids),
		//			requestState);

		//		// Clear the list of pending nodes.
		//		this.pendingNodes.Clear();
		//	}
		//}

		/// <summary>
		/// A method called when receiving the response to a nodes refresh request.
		/// </summary>
		/// <param name="response">The response.</param>
		/// <param name="state">The request state.</param>
		//private void OnRefreshNodesRequestResult(XmlRpcResponse response, RequestState state)
		//{
		//	// Convert the request state.
		//	IdsRequestState requestState = state as IdsRequestState;
		//	// If the request has not failed.
		//	if ((null == response.Fault) && (null != response.Value))
		//	{
		//		// Get the response array.
		//		XmlRpcArray array = response.Value as XmlRpcArray;

		//		// Check the array is not null.
		//		if (null == array)
		//		{
		//			// Update the status.
		//			this.status.Send(
		//				CrawlerStatus.StatusType.Normal,
		//				@"Slice '{0}' has {1} node{2}.".FormatWith(this.slice.Name, this.slice.NodeIds.Length, this.slice.NodeIds.Length.PluralSuffix()),
		//				"Refreshing the nodes information failed.",
		//				Resources.GlobeLab_16,
		//				Resources.GlobeError_16);
		//			// Return.
		//			return;
		//		}

		//		// For each value in the response array.
		//		foreach (XmlRpcValue value in array.Values)
		//		{
		//			// The PlanetLab node.
		//			PlNode node = null;

		//			// Try parse the structure to a PlanetLab node and add it to the nodes list.
		//			try { node = this.crawler.PlanetLab.Nodes.Add(value.Value as XmlRpcStruct); }
		//			catch { }

		//			// If the object is null, continue.
		//			if (null == node) continue;

		//			// Find the list item corresponding to the node.
		//			ListViewItem item = this.listViewNodes.Items.FirstOrDefault((ListViewItem it) =>
		//				{
		//					// Get the node info.
		//					NodeInfo info = it.Tag as NodeInfo;
		//					// Check the node ID.
		//					return info.NodeId == node.Id;
		//				});

		//			// If the item is not null.
		//			if (null != item)
		//			{
		//				// Get the node info.
		//				NodeInfo info = item.Tag as NodeInfo;

		//				// If the node has not been set.
		//				if (info.Node == null)
		//				{
		//					// Add a node event handler.
		//					node.Changed += this.OnNodeChanged;
		//					// Set the node information.
		//					info.Node = node;
		//					info.SiteId = node.SiteId;
		//				}

		//				// If the site has not been set.
		//				if (null == info.Site)
		//				{
		//					// If the node has a site identifier.
		//					if (node.SiteId.HasValue)
		//					{
		//						// Get the site from the database.
		//						PlSite site = this.crawler.PlanetLab.DbSites.Find(node.SiteId.Value);
		//						// If the site is not null.
		//						if (null != site)
		//						{
		//							// Add a site event handler.
		//							site.Changed += this.OnSiteChanged;
		//							// Set the site.
		//							info.Site = site;
		//							// If the item does not have a marker and if the site has coordinates.
		//							if ((null == info.Marker) && site.Latitude.HasValue && site.Longitude.HasValue)
		//							{
		//								// Create a circular marker.
		//								marker = new MapBulletMarker(new MapPoint(site.Longitude.Value, site.Latitude.Value));
		//								marker.Name = "{0}{1}{2}".FormatWith(node.Hostname, Environment.NewLine, site.Name);
		//								// Add the marker to the map.
		//								this.mapControl.Markers.Add(marker);
		//								// Set the marker.
		//								info.Marker = marker;
		//							}
		//						}
		//						else
		//						{
		//							// Add the site to the pending sites.
		//							this.pendingSites.Add(node.SiteId.Value);
		//						}
		//					}
		//				}

		//				// Set the item information.
		//				item.SubItems[0].Text = node.Id.Value.ToString();
		//				item.SubItems[1].Text = node.Hostname;
		//				item.ImageKey = ControlSliceRun.nodeImageKeys[(int)node.GetBootState()];
		//			}
		//		}

		//		// Update the status.
		//		this.status.Send(
		//			CrawlerStatus.StatusType.Normal,
		//			@"Slice '{0}' has {1} node{2}.".FormatWith(this.slice.Name, this.slice.NodeIds.Length, this.slice.NodeIds.Length.PluralSuffix()),
		//			"Refreshing the nodes information completed successfully.",
		//			Resources.GlobeLab_16,
		//			Resources.GlobeSuccess_16);
		//	}
		//	else
		//	{
		//		// Update the status.
		//		this.status.Send(
		//			CrawlerStatus.StatusType.Normal,
		//			@"Slice '{0}' has {1} node{2}.".FormatWith(this.slice.Name, this.slice.NodeIds.Length, this.slice.NodeIds.Length.PluralSuffix()),
		//			"Refreshing the nodes information failed.",
		//			Resources.GlobeLab_16,
		//			Resources.GlobeError_16);
		//	}
		//}

		/// <summary>
		/// A method called when a nodes refresh request has been canceled.
		/// </summary>
		/// <param name="state">The request state.</param>
		//private void OnRefreshNodesRequestCanceled(RequestState state)
		//{
		//	// Update the status.
		//	this.status.Send(
		//		CrawlerStatus.StatusType.Normal,
		//		@"Slice '{0}' has {1} node{2}.".FormatWith(this.slice.Name, this.slice.NodeIds.Length, this.slice.NodeIds.Length.PluralSuffix()),
		//		"Refreshing the nodes information was canceled.",
		//		Resources.GlobeLab_16,
		//		Resources.GlobeCanceled_16);
		//}

		/// <summary>
		/// A method called when a nodes refresh request returned an exception.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="state">The request state.</param>
		//private void OnRefreshNodesRequestException(Exception exception, RequestState state)
		//{
		//	// Update the status.
		//	this.status.Send(
		//		CrawlerStatus.StatusType.Normal,
		//		@"Slice '{0}' has {1} node{2}.".FormatWith(this.slice.Name, this.slice.NodeIds.Length, this.slice.NodeIds.Length.PluralSuffix()),
		//		"Refreshing the nodes information failed.",
		//		Resources.GlobeLab_16,
		//		Resources.GlobeError_16);
		//}

		/// <summary>
		/// A method called when a nodes refresh request has finished.
		/// </summary>
		/// <param name="state">The request state.</param>
		//private void OnRefreshNodesRequestFinished(RequestState state)
		//{
		//	// If there are pending sites, refresh the sites.
		//	lock (this.pendingSync)
		//	{
		//		if (this.pendingSites.Count > 0)
		//		{
		//			this.OnRefreshSites();
		//		}
		//	}
		//}

		/// <summary>
		/// An event handler called when the list of sites is refreshed.
		/// </summary>
		//private void OnRefreshSites()
		//{
		//	lock (this.pendingSync)
		//	{
		//		// If there are no sites to refresh, do nothing.
		//		if (this.pendingSites.Count == 0) return;

		//		// Update the status.
		//		this.status.Send(
		//			CrawlerStatus.StatusType.Busy,
		//			@"Slice '{0}' has {1} node{2}.".FormatWith(this.slice.Name, this.slice.NodeIds.Length, this.slice.NodeIds.Length.PluralSuffix()),
		//			"Refreshing the sites information...",
		//			Resources.GlobeLab_16,
		//			Resources.GlobeClock_16);

		//		// Create the request state.
		//		IdsRequestState requestState = new IdsRequestState(
		//			null,
		//			this.OnRefreshSitesRequestResult,
		//			this.OnRefreshSitesRequestCanceled,
		//			this.OnRefreshSitesRequestException,
		//			null,
		//			this.pendingSites.ToArray());

		//		// Begin an asynchronous PlanetLab request.
		//		this.BeginRequest(
		//			this.requestGetSites,
		//			this.crawler.PlanetLab.Username,
		//			this.crawler.PlanetLab.Password,
		//			PlSite.GetFilter(PlSite.Fields.SiteId, requestState.Ids),
		//			requestState);

		//		// Clear the list of pending sites.
		//		this.pendingSites.Clear();
		//	}
		//}

		/// <summary>
		/// A method called when receiving the response to a sites refresh request.
		/// </summary>
		/// <param name="response">The response.</param>
		/// <param name="state">The request state.</param>
		//private void OnRefreshSitesRequestResult(XmlRpcResponse response, RequestState state)
		//{
		//	// Convert the request state.
		//	IdsRequestState requestState = state as IdsRequestState;
		//	// If the request has not failed.
		//	if ((null == response.Fault) && (null != response.Value))
		//	{
		//		// Get the response array.
		//		XmlRpcArray array = response.Value as XmlRpcArray;

		//		// Check the array is not null.
		//		if (null == array)
		//		{
		//			// Update the status.
		//			this.status.Send(
		//				CrawlerStatus.StatusType.Normal,
		//				@"Slice '{0}' has {1} node{2}.".FormatWith(this.slice.Name, this.slice.NodeIds.Length, this.slice.NodeIds.Length.PluralSuffix()),
		//				"Refreshing the sites information failed.",
		//				Resources.GlobeLab_16,
		//				Resources.GlobeError_16);
		//			// Return.
		//			return;
		//		}

		//		// For each value in the response array.
		//		foreach (XmlRpcValue value in array.Values)
		//		{
		//			// The PlanetLab site.
		//			PlSite site = null;

		//			// Try parse the structure to a PlanetLab node and add it to the sites list.
		//			try { site = this.crawler.PlanetLab.Sites.Add(value.Value as XmlRpcStruct); }
		//			catch { }

		//			// If the object is null, continue.
		//			if (null == site) continue;

		//			// Find the list item corresponding to the node.
		//			ListViewItem item = this.listViewNodes.Items.FirstOrDefault((ListViewItem it) =>
		//			{
		//				// Get the node info.
		//				NodeInfo info = it.Tag as NodeInfo;
		//				// Check the site ID.
		//				return info.SiteId == site.Id;
		//			});

		//			// If the item is not null.
		//			if (null != item)
		//			{
		//				// Get the node info.
		//				NodeInfo info = item.Tag as NodeInfo;

		//				// If the site has not been set.
		//				if (info.Site == null)
		//				{
		//					// Add a node event handler.
		//					site.Changed += this.OnSiteChanged;
		//					// Set the site.
		//					info.Site = site;
		//					// If the item does not have a marker and if the site has coordinates.
		//					if ((null == info.Marker) && (null != info.Node) && site.Latitude.HasValue && site.Longitude.HasValue)
		//					{
		//						// Create a circular marker.
		//						marker = new MapBulletMarker(new MapPoint(site.Longitude.Value, site.Latitude.Value));
		//						marker.Name = "{0}{1}{2}".FormatWith(info.Node.Hostname, Environment.NewLine, site.Name);
		//						// Add the marker to the map.
		//						this.mapControl.Markers.Add(marker);
		//						// Set the marker.
		//						info.Marker = marker;
		//					}
		//				}
		//			}
		//		}

		//		// Update the status.
		//		this.status.Send(
		//			CrawlerStatus.StatusType.Normal,
		//			@"Slice '{0}' has {1} node{2}.".FormatWith(this.slice.Name, this.slice.NodeIds.Length, this.slice.NodeIds.Length.PluralSuffix()),
		//			"Refreshing the sites information completed successfully.",
		//			Resources.GlobeLab_16,
		//			Resources.GlobeSuccess_16);
		//	}
		//	else
		//	{
		//		// Update the status.
		//		this.status.Send(
		//			CrawlerStatus.StatusType.Normal,
		//			@"Slice '{0}' has {1} node{2}.".FormatWith(this.slice.Name, this.slice.NodeIds.Length, this.slice.NodeIds.Length.PluralSuffix()),
		//			"Refreshing the sites information failed.",
		//			Resources.GlobeLab_16,
		//			Resources.GlobeError_16);
		//	}
		//}

		/// <summary>
		/// A method called when a sites refresh request has been canceled.
		/// </summary>
		/// <param name="state">The request state.</param>
		//private void OnRefreshSitesRequestCanceled(RequestState state)
		//{
		//	// Update the status.
		//	this.status.Send(
		//		CrawlerStatus.StatusType.Normal,
		//		@"Slice '{0}' has {1} node{2}.".FormatWith(this.slice.Name, this.slice.NodeIds.Length, this.slice.NodeIds.Length.PluralSuffix()),
		//		"Refreshing the sites information was canceled.",
		//		Resources.GlobeLab_16,
		//		Resources.GlobeCanceled_16);
		//}

		/// <summary>
		/// A method called when a sites refresh request returned an exception.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="state">The request state.</param>
		//private void OnRefreshSitesRequestException(Exception exception, RequestState state)
		//{
		//	// Update the status.
		//	this.status.Send(
		//		CrawlerStatus.StatusType.Normal,
		//		@"Slice '{0}' has {1} node{2}.".FormatWith(this.slice.Name, this.slice.NodeIds.Length, this.slice.NodeIds.Length.PluralSuffix()),
		//		"Refreshing the sites information failed.",
		//		Resources.GlobeLab_16,
		//		Resources.GlobeError_16);
		//}

		/// <summary>
		/// An event handler called when the user clicks on the nodes list.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnMouseClick(object sender, MouseEventArgs e)
		{
			//if (e.Button == MouseButtons.Right)
			//{
			//	if (this.listViewNodes.FocusedItem != null)
			//	{
			//		if (this.listViewNodes.FocusedItem.Bounds.Contains(e.Location))
			//		{
			//			this.contextMenu.Show(this.listViewNodes, e.Location);
			//		}
			//	}
			//}
		}

		/// <summary>
		/// An event handler called when the user selects the node properties.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnNodeProperties(object sender, EventArgs e)
		{
			//// If there is no selected item, do nothing.
			//if (this.listViewNodes.SelectedItems.Count == 0) return;
			
			//// Get the node info for the selected item.
			//NodeInfo info = this.listViewNodes.SelectedItems[0].Tag as NodeInfo;

			//// If the node info does not have a node object.
			//if (null == info.Node)
			//{
			//	// Show the node properties using the identifier.
			//	this.formNodeProperties.ShowDialog(this, "Node", info.NodeId);
			//}
			//else
			//{
			//	// Show the node properties using the node object.
			//	this.formNodeProperties.ShowDialog(this, "Node", info.Node);
			//}
		}

		/// <summary>
		/// An event handler called when the user selects the site properties.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnSiteProperties(object sender, EventArgs e)
		{
			//// If there is no selected item, do nothing.
			//if (this.listViewNodes.SelectedItems.Count == 0) return;

			//// Get the node info for the selected item.
			//NodeInfo info = this.listViewNodes.SelectedItems[0].Tag as NodeInfo;

			//// If the node has a site object.
			//if (null != info.Site)
			//{
			//	// Show the site properties.
			//	this.formSiteProperties.ShowDialog(this, "Site", info.Site);
			//}
			//else if (info.SiteId.HasValue)
			//{
			//	// Show the site properties.
			//	this.formSiteProperties.ShowDialog(this, "Site", info.SiteId.Value);
			//}
		}

		private void OnStart(object sender, EventArgs e)
		{

		}

		private void OnPause(object sender, EventArgs e)
		{

		}

		private void OnStop(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// An event handler called when adding a new PlanetLab command.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnAddCommand(object sender, EventArgs e)
		{
			// Show the add command dialog.
			if (this.formAddCommand.ShowDialog(this) == DialogResult.OK)
			{

			}
		}

		private void OnRemoveCommand(object sender, EventArgs e)
		{

		}
	}
}
