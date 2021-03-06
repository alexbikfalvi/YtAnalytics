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
using DotNetApi.Windows.Controls;
using InetAnalytics;
using InetTools.Tools.CdnFinder;

namespace InetTools.Controls.CdnFinder
{
	/// <summary>
	/// A control that the collected CDN Finder data for a web site.
	/// </summary>
	public partial class ControlCdnFinderSite : ThreadSafeControl
	{
		// Creates a new control instance.
		public ControlCdnFinderSite()
		{
			this.InitializeComponent();
		}
		

		// Public method.

		/// <summary>
		/// Clears the site information.
		/// </summary>
		public void Clear()
		{
			this.pictureBox.Image = Resources.GlobeQuestion_48;
			this.labelTitle.Text = "Site information not available";
			this.textBoxSite.Clear();
			this.textBoxUrl.Clear();
			this.textBoxAssetCdn.Clear();
			this.textBoxBaseCdn.Clear();
			this.listViewResources.Items.Clear();
		}

		/// <summary>
		/// Sets the specified site information.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="site">The site.</param>
		public void Set(string name, CdnFinderSite site)
		{
			// Clear the resources list.
			this.listViewResources.Items.Clear();
			if (null == site)
			{
				this.pictureBox.Image = Resources.GlobeError_48;
				this.labelTitle.Text = "Site not found";
				this.textBoxSite.Clear();
				this.textBoxUrl.Clear();
				this.textBoxAssetCdn.Clear();
				this.textBoxBaseCdn.Clear();
			}
			else if (site.Success)
			{
				this.pictureBox.Image = Resources.GlobeSuccess_48;
				this.labelTitle.Text = name;
				this.textBoxSite.Text = name;
				this.textBoxUrl.Text = site.Site;
				this.textBoxAssetCdn.Text = site.AssetCdn;
				this.textBoxBaseCdn.Text = site.BaseCdn;
				foreach (CdnFinderResource resource in site.Resources)
				{
					ListViewItem item = new ListViewItem(new string[] {
							resource.Hostname,
							resource.Count.ToString(),
							resource.Size.ToString(),
							resource.Cdn,
							resource.IsBase ? "Yes" : "No"
						});
					item.ImageIndex = 0;
					this.listViewResources.Items.Add(item);
				}
			}
			else
			{
				this.pictureBox.Image = Resources.GlobeWarning_48;
				this.labelTitle.Text = "No site information";
				this.textBoxSite.Clear();
				this.textBoxUrl.Clear();
				this.textBoxAssetCdn.Clear();
				this.textBoxBaseCdn.Clear();
			}
		}
	}
}
