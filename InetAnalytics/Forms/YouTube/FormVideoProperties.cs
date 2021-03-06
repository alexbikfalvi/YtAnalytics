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
using InetAnalytics.Controls.YouTube.Api2;
using InetAnalytics.Events;
using InetApi.YouTube.Api.V2.Data;
using InetCrawler.Events;
using DotNetApi.Windows;
using DotNetApi.Windows.Forms;

namespace InetAnalytics.Forms
{
	/// <summary>
	/// A form dialog displaying a YouTube video.
	/// </summary>
	public partial class FormVideo : ThreadSafeForm
	{
		/// <summary>
		/// Creates a new form instance.
		/// </summary>
		public FormVideo()
		{
			// Initialize the component.
			this.InitializeComponent();

			// Set the font.
			Window.SetFont(this);
		}

		// Public events.

		/// <summary>
		/// An event raised to view the user profile.
		/// </summary>
		public event StringEventHandler ViewProfile;

		// Public methods.

		/// <summary>
		/// Shows the form as a dialog and the specified video.
		/// </summary>
		/// <param name="owner">The owner window.</param>
		/// <param name="video">The video.</param>
		/// <returns>The dialog result.</returns>
		public DialogResult ShowDialog(IWin32Window owner, Video video)
		{
			// If the event is null, do nothing.
			if (null == video) return DialogResult.Abort;

			// Set the event.
			this.video.Video = video;
			// Set the title.
			this.Text = video.Title;
			// Open the dialog.
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
		/// An event handler called to view the user profile.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnViewProfile(object sender, StringEventArgs e)
		{
			// Close the dialog.
			this.Close();
			// Raise the event.
			if (null != this.ViewProfile) this.ViewProfile(sender, e);
		}
	}
}
