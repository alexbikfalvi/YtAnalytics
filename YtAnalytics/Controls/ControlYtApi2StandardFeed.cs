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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YtCrawler;
using YtApi;
using YtApi.Api.V2;
using YtApi.Api.V2.Atom;
using YtApi.Api.V2.Data;
using YtCrawler.Log;

namespace YtAnalytics.Controls
{
	/// <summary>
	/// A control class for a YouTube API version 2 standard feed.
	/// </summary>
	public partial class ControlYtApi2StandardFeed : UserControl
	{
		private static string logSource = "APIv2 Standard Feed";

		// Private variables.

		private Crawler crawler;
		private ControlMessage message = new ControlMessage();
		private Uri uri = null;
		private YouTubeRequestFeed<Video> request;
		private IAsyncResult result;
		private Feed<Video> feed = null;

		private ShowMessageEventHandler delegateShowMessage;
		private HideMessageEventHandler delegateHideMessage;

		private static YouTubeStandardFeed[] feeds = new YouTubeStandardFeed[] {
			YouTubeStandardFeed.TopRated,
			YouTubeStandardFeed.TopFavories,
			YouTubeStandardFeed.MostShared,
			YouTubeStandardFeed.MostPopular,
			YouTubeStandardFeed.MostRecent,
			YouTubeStandardFeed.MostDiscussed,
			YouTubeStandardFeed.MostResponsed,
			YouTubeStandardFeed.RecentlyFeatured,
			YouTubeStandardFeed.TrendingVideos
		};
		private static YouTubeTimeId[] times = new YouTubeTimeId[] {
			YouTubeTimeId.AllTime,
			YouTubeTimeId.Today,
			YouTubeTimeId.ThisWeek,
			YouTubeTimeId.ThisMonth
		};

		// Public declarations

		/// <summary>
		/// Creates a new control instance.
		/// </summary>
		public ControlYtApi2StandardFeed()
		{
			// Add the message control.
			this.Controls.Add(this.message);

			// Initialize component.
			InitializeComponent();

			// Set the default control properties.
			this.Visible = false;
			this.Dock = DockStyle.Fill;

			// Add the feed names.
			this.comboBoxFeed.Items.AddRange(YouTubeUri.StandardFeedNames);
			this.comboBoxFeed.SelectedIndex = 0;
			
			// Add the category.
			this.comboBoxCategory.Items.Add("(any)");
			this.comboBoxCategory.SelectedIndex = 0;

			// Add the region names.
			this.comboBoxRegion.Items.Add("(any)");
			this.comboBoxRegion.Items.AddRange(YouTubeUri.RegionNames);
			this.comboBoxRegion.SelectedIndex = 0;

			// Delegates.
			this.delegateShowMessage = new ShowMessageEventHandler(this.ShowMessage);
			this.delegateHideMessage = new HideMessageEventHandler(this.HideMessage);
		}

		/// <summary>
		/// Initializes the control with a crawler object.
		/// </summary>
		/// <param name="crawler">The crawler object.</param>
		public void Initialize(Crawler crawler)
		{
			// Save the parameters.
			this.crawler = crawler;
			this.request = new YouTubeRequestFeed<Video>(this.crawler.Settings);

			// Enable the control
			this.Enabled = true;
		}

		// Public events.

		/// <summary>
		/// View the video information using the version 2 API.
		/// </summary>
		public event ViewVideoEventHandler ViewVideoInApiV2;
		/// <summary>
		/// View the related videos using the version 2 API.
		/// </summary>
		public event ViewVideoEventHandler ViewVideoRelatedInApiV2;
		/// <summary>
		/// View the response videos using the version 2 API.
		/// </summary>
		public event ViewVideoEventHandler ViewVideoResponsesInApiV2;
		/// <summary>
		/// View the user profile using the version 2 API.
		/// </summary>
		public event ViewProfileIdEventHandler ViewProfileInApiV2;
		/// <summary>
		/// View the video information using the version 3 API.
		/// </summary>
		public event ViewVideoEventHandler ViewVideoInApiV3;
		/// <summary>
		/// View the video statistics using the web.
		/// </summary>
		public event ViewVideoEventHandler ViewVideoInWeb;
		/// <summary>
		/// An event handler called when the user adds a new comment.
		/// </summary>
		public event AddVideoCommentEventHandler Comment;

		// Private methods.

		/// <summary>
		/// Shows an alerting message on top of the control.
		/// </summary>
		/// <param name="image">The message icon.</param>
		/// <param name="text">The message text.</param>
		/// <param name="progress">The visibility of the progress bar.</param>
		private void ShowMessage(Image image, string text, bool progress = true)
		{
			// Invoke the function on the UI thread.
			if (this.InvokeRequired)
				this.Invoke(this.delegateShowMessage, new object[] { image, text, progress });
			else
			{
				// Show the message.
				this.message.Show(image, text, progress);
				// Disable the control.
				this.panel.Enabled = false;
			}
		}

		/// <summary>
		/// Hides the alerting message.
		/// </summary>
		private void HideMessage()
		{
			// Invoke the function on the UI thread.
			if (this.InvokeRequired)
				this.Invoke(this.delegateHideMessage);
			else
			{
				// Hide the message.
				this.message.Hide();
				// Enable the control.
				this.panel.Enabled = true;
			}
		}

		/// <summary>
		/// An event handler for when the selection of the current options has changed.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnSelectionChanged(object sender, EventArgs e)
		{
			if (this.comboBoxFeed.SelectedIndex < 0) return;
			if (this.comboBoxTime.SelectedIndex < 0) return;
			if (this.comboBoxCategory.SelectedIndex < 0) return;
			if (this.comboBoxRegion.SelectedIndex < 0) return;

			// Compute the URI
			this.uri = YouTubeUri.GetStandardFeed(
				ControlYtApi2StandardFeed.feeds[this.comboBoxFeed.SelectedIndex],
				this.comboBoxRegion.SelectedIndex == 0 ? null : YouTubeUri.GetRegionId(this.comboBoxRegion.SelectedItem as string),
				this.comboBoxCategory.SelectedIndex == 0 ? null : this.crawler.Categories[this.comboBoxCategory.SelectedItem as string].Term,
				this.comboBoxTime.SelectedIndex == 0 ? null : ControlYtApi2StandardFeed.times[Array.IndexOf(YouTubeUri.TimeNames, this.comboBoxTime.SelectedItem as string)] as YouTubeTimeId?,
				1,
				this.videoList.VideosPerPage);
			this.linkLabel.Text = this.uri.AbsoluteUri;
		}

		/// <summary>
		/// An event handler for the opening of the video categories combo box.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnOpenCategories(object sender, EventArgs e)
		{
			// If there are no categories in the combobox, refresh the categories list.
			if (this.crawler.Categories.Count == 0)
			{
				this.OnBeginRefreshCategories(sender, e);
			}
			else
			{
				this.OnUpdateCategories(sender, e);
			}
		}

		/// <summary>
		/// An event handler for the updating of the video categories.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnUpdateCategories(object sender, EventArgs e)
		{
			// Invoke the function on the UI thread.
			if (this.InvokeRequired)
				this.Invoke(new EventHandler(this.OnUpdateCategories), new object[] { sender, e });
			else
			{
				// Update the categories list.
				this.comboBoxCategory.Items.Clear();
				this.comboBoxCategory.Items.Add("(any)");
				this.comboBoxCategory.Items.AddRange(this.crawler.Categories.CategoryLabels);
				this.comboBoxCategory.SelectedIndex = 0;
			}
		}

		/// <summary>
		/// Begins an asynchronous request for updating the video categories.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnBeginRefreshCategories(object sender, EventArgs e)
		{
			// Show a message to alert the user.
			this.ShowMessage(Resources.GlobeClock_48, "Refreshing the list of YouTube categories...");

			// Refresh the list of categories.
			this.crawler.Categories.BeginRefresh(new AsyncRequestCallback(this.OnEndRefreshCategories), null);
		}

		/// <summary>
		/// Completes an asynchronous request for updating the video categories.
		/// </summary>
		/// <param name="result">The asynchornous result.</param>
		private void OnEndRefreshCategories(AsyncRequestResult result)
		{
			try
			{
				// Complete the asynchronous request.
				this.crawler.Categories.EndRefresh(result);
				// Update the message that the operation completed successfully.
				this.ShowMessage(
					Resources.GlobeSuccess_48,
					"Refreshing the list of YouTube categories...\r\n\r\nDone.",
					false
					);
				// Update the categories.
				this.OnOpenCategories(null, null);
			}
			catch (Exception exception)
			{
				// Update the message that the operation completed successfully.
				this.ShowMessage(
					Resources.GlobeError_48,
					string.Format("Failed ({0}).", exception.Message),
					false
					);
			}
			finally
			{
				// Delay the closing of the message.
				Thread.Sleep((int)this.crawler.Config.MessageCloseDelay.TotalMilliseconds);
				// Hide the message.
				this.HideMessage();
			}
		}

		/// <summary>
		/// An event handler called when the current feed has changed.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnFeedChanged(object sender, EventArgs e)
		{
			// Update the time list: get the list of valid time IDs for the current feed.
			YouTubeTimeId[] ids = YouTubeUri.GetValidTime(ControlYtApi2StandardFeed.feeds[this.comboBoxFeed.SelectedIndex]);

			this.comboBoxTime.Items.Clear();
			this.comboBoxTime.Items.Add("(any)");
			foreach (YouTubeTimeId id in ids)
				this.comboBoxTime.Items.Add(YouTubeUri.TimeNames[(int)id]);
			this.comboBoxTime.SelectedIndex = 0;

			this.OnSelectionChanged(sender, e);
		}

		/// <summary>
		/// An event handler called when the current video category has changed.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnCategoryChanged(object sender, EventArgs e)
		{
			this.comboBoxRegion.Items.Clear();
			this.comboBoxRegion.Items.Add("(any)");

			// Get the current category
			if(this.comboBoxCategory.SelectedIndex == 0)
			{
				this.comboBoxRegion.Items.AddRange(YouTubeUri.RegionNames);
			}
			else
			{
				// Compute the category index.
				int categoryIndex = Array.IndexOf(this.crawler.Categories.CategoryLabels, this.comboBoxCategory.SelectedItem);

				// Get the category region IDs.
				string[] ids = this.crawler.Categories[categoryIndex].Browsable;

				// Add the region names.
				if (ids != null)
				{
					foreach (string id in ids)
					{
						try { this.comboBoxRegion.Items.Add(YouTubeUri.GetRegionName(id)); }
						catch (IndexOutOfRangeException)
						{
							MessageBox.Show(
								this,
								string.Format("The region ID \'{0}\' is unknown an it will be ignored.", id),
								"Unknown Region ID",
								MessageBoxButtons.OK,
								MessageBoxIcon.Warning);
						}
					}
				}
			}

			this.comboBoxRegion.SelectedIndex = 0;

			this.OnSelectionChanged(sender, e);
		}

		/// <summary>
		/// An event handler raised when the user selects the current feed link.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnOpenLink(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(this.linkLabel.Text);
		}

		/// <summary>
		/// Starts an asynchronous request for the selected video feed.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnStart(object sender, EventArgs e)
		{
			// Change the controls state.
			this.buttonStart.Enabled = false;
			this.buttonStop.Enabled = true;
			this.comboBoxFeed.Enabled = false;
			this.comboBoxTime.Enabled = false;
			this.comboBoxCategory.Enabled = false;
			this.comboBoxRegion.Enabled = false;

			// Log
			this.log.Add(this.crawler.Log.Add(
				LogEventLevel.Verbose,
				LogEventType.Information,
				ControlYtApi2StandardFeed.logSource,
				"Started request for video feed \'{0}\'.",
				new object[] { this.comboBoxFeed.SelectedItem, this.linkLabel.Text }));

			// Clear the list view.
			this.videoList.Clear();

			try
			{
				// Begin the request.
				this.result = this.request.Begin(this.uri, this.Callback);
			}
			catch (Exception exception)
			{
				this.log.Add(this.crawler.Log.Add(
					LogEventLevel.Important,
					LogEventType.Error,
					ControlYtApi2StandardFeed.logSource,
					"The request for video feed \'{0}\' failed. {1}",
					new object[] { this.comboBoxFeed.SelectedItem, exception.Message, this.linkLabel.Text },
					exception));
			}
		}

		/// <summary>
		/// Cancels an asynchronous request for the selected video feed.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnStop(object sender, EventArgs e)
		{
			// Cancel the request.
			this.request.Cancel(this.result);
			// Disable the stop button.
			this.buttonStop.Enabled = false;
		}

		/// <summary>
		/// Completes an asynchronous request for the selected video feed.
		/// </summary>
		/// <param name="result">The asynchronous result.</param>
		private void Callback(IAsyncResult result)
		{
			if (this.InvokeRequired)
				this.Invoke(new AsyncCallback(this.Callback), new object[] { result });
			else
			{
				try
				{
					// Complete the request
					this.feed = this.request.End(result);

					// Add the new items to the list view.
					foreach (Video video in feed.Entries)
					{
						this.videoList.Add(video);
					}

					// Update the page information.
					this.videoList.CountStart = feed.Entries.Count > 0 ? feed.SearchStartIndex : 0;
					this.videoList.CountPerPage = feed.Entries.Count;
					this.videoList.CountTotal = feed.SearchTotalResults;

					// Set the navigation buttons state.
					this.videoList.Previous = feed.Links.Previous != null;
					this.videoList.Next = feed.Links.Next != null;

					// Compute the event type.
					LogEventType eventType = (this.feed.FailuresAtom.Count == 0) && (this.feed.FailuresEntry.Count == 0) ?
						LogEventType.Success : LogEventType.SuccessWarning;
					string eventMessage = eventType == LogEventType.Success ?
						"The request for video feed \'{0}\' completed successfully." :
						"The request for video feed \'{0}\' completed partially successfully. However, some errors have occurred.";

					// If there are failures, create a new subevent list.
					List<LogEvent> subevents = null;
					if ((this.feed.FailuresAtom.Count != 0) || (this.feed.FailuresEntry.Count != 0))
					{
						subevents = new List<LogEvent>();
						foreach (AtomException exception in this.feed.FailuresAtom)
						{
							subevents.Add(new LogEvent(
								LogEventLevel.Important,
								LogEventType.Error,
								DateTime.MinValue,
								ControlYtApi2StandardFeed.logSource,
								"Parsing of YouTube API version 2 atom XML failed.",
								null,
								exception));
						}
						foreach (YouTubeAtomException exception in this.feed.FailuresEntry)
						{
							subevents.Add(new LogEvent(
								LogEventLevel.Important,
								LogEventType.Error,
								DateTime.MinValue,
								ControlYtApi2StandardFeed.logSource,
								"Converting atom to YouTube API version 2 video entry failed.",
								null,
								exception));
						}
					}

					// Log
					this.log.Add(this.crawler.Log.Add(
						LogEventLevel.Verbose,
						eventType,
						ControlYtApi2StandardFeed.logSource,
						eventMessage,
						new object[] { this.comboBoxFeed.SelectedItem, this.linkLabel.Text },
						null,
						subevents));
				}
				catch (WebException exception)
				{
					if (exception.Status == WebExceptionStatus.RequestCanceled)
						this.log.Add(this.crawler.Log.Add(
							LogEventLevel.Verbose,
							LogEventType.Canceled,
							ControlYtApi2StandardFeed.logSource,
							"The request for video feed \'{0}\' has been canceled.",
							new object[] { this.comboBoxFeed.SelectedItem, this.linkLabel.Text }));
					else
						this.log.Add(this.crawler.Log.Add(
							LogEventLevel.Important,
							LogEventType.Error,
							ControlYtApi2StandardFeed.logSource,
							"The request for video feed \'{0}\' failed. {1}",
							new object[] { this.comboBoxFeed.SelectedItem, exception.Message, this.linkLabel.Text },
							exception));
				}
				catch (Exception exception)
				{
					this.log.Add(this.crawler.Log.Add(
						LogEventLevel.Important,
						LogEventType.Error,
						ControlYtApi2StandardFeed.logSource,
						"The request for video feed \'{0}\' failed. {1}",
						new object[] { this.comboBoxFeed.SelectedItem, exception.Message, this.linkLabel.Text },
						exception));
				}
				finally
				{
					this.buttonStart.Enabled = true;
					this.buttonStop.Enabled = false;
					this.comboBoxFeed.Enabled = true;
					this.comboBoxTime.Enabled = true;
					this.comboBoxCategory.Enabled = true;
					this.comboBoxRegion.Enabled = true;
				}
			}
		}

		/// <summary>
		/// An event handler called when the selected video has changed.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnVideoSelectedChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			// Change the enabled state of the view video button.
			this.checkBoxView.Enabled = this.videoList.SelectedItem != null;
		}

		/// <summary>
		/// An event handler for the visualization of a video entry menu.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnViewVideo(object sender, EventArgs e)
		{
			if(this.checkBoxView.Checked)
				this.viewMenu.Show(this.checkBoxView, 0, this.checkBoxView.Height);
		}

		/// <summary>
		/// An event handler called when the video entry menu was closed.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnViewMenuClosed(object sender, ToolStripDropDownClosedEventArgs e)
		{
			this.checkBoxView.Checked = false;
		}

		/// <summary>
		/// An event handler called when the user selects the option to view the video entry
		/// in the YouTube API version 2.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnViewApiV2Entry(object sender, EventArgs e)
		{
			if (this.ViewVideoInApiV2 != null) this.ViewVideoInApiV2(this.videoList.SelectedItem.Tag as Video);
		}

		/// <summary>
		/// An event handler called when the user selects the option to view the related videos
		/// in the YouTube API version 2.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnViewApiV2Related(object sender, EventArgs e)
		{
			if (this.ViewVideoRelatedInApiV2 != null) this.ViewVideoRelatedInApiV2(this.videoList.SelectedItem.Tag as Video);
		}

		/// <summary>
		/// An event handler called when the user selects the option to view the response videos
		/// in the YouTube API version 2.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnViewApiV2Responses(object sender, EventArgs e)
		{
			if (this.ViewVideoResponsesInApiV2 != null) this.ViewVideoResponsesInApiV2(this.videoList.SelectedItem.Tag as Video);
		}

		/// <summary>
		/// An event handler called when the user selects the option to view the video entry
		/// in the YouTube API version 3.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnViewApiV3Entry(object sender, EventArgs e)
		{
			if (this.ViewVideoInApiV3 != null) this.ViewVideoInApiV3(this.videoList.SelectedItem.Tag as Video);
		}

		/// <summary>
		/// An event handler called when the user selects the option to view the video statistics
		/// in the YouTube web.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnViewWeb(object sender, EventArgs e)
		{
			if (this.ViewVideoInWeb != null) this.ViewVideoInWeb(this.videoList.SelectedItem.Tag as Video);
		}

		/// <summary>
		/// An event handler called when the user selects the option to open the video in YouTube in
		/// a browser.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnOpenYouTube(object sender, EventArgs e)
		{
			// Get the video;
			Video video = this.videoList.SelectedItem.Tag as Video;
			// Open the video link in the browser.
			System.Diagnostics.Process.Start(YouTubeUri.GetYouTubeLink(video.Id));
		}

		/// <summary>
		/// An event handler called when the user adds a comment to a video.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnComment(object sender, EventArgs e)
		{
			if (this.Comment != null) this.Comment((this.videoList.SelectedItem.Tag as Video).Id);
		}

		/// <summary>
		/// An event handler called when the user navigates to the previous page in the feed list.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnNavigatePrevious(object sender, EventArgs e)
		{
			// If the current feed is null, disable the button and return.
			if ((null == this.feed) || (null == this.feed.Links.Previous))
			{
				this.videoList.Previous = false;
				return;
			}
			// Copy the URL.
			this.uri = this.feed.Links.Previous;
			// Set the link label.
			this.linkLabel.Text = this.uri.AbsoluteUri;
			// Clear the feed.
			this.feed = null;
			// Clear the video list.
			this.videoList.Clear();
			// Start a new request.
			this.OnStart(sender, e);
		}

		/// <summary>
		/// An event handler called when the user navigates to the next page in the feed list.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnNavigateNext(object sender, EventArgs e)
		{
			// If the current feed is null, disable the button and return.
			if ((null == this.feed) || (null == this.feed.Links.Next))
			{
				this.videoList.Next = false;
				return;
			}
			// Copy the URL.
			this.uri = this.feed.Links.Next;
			// Set the link label.
			this.linkLabel.Text = this.uri.AbsoluteUri;
			// Clear the feed.
			this.feed = null;
			// Clear the video list.
			this.videoList.Clear();
			// Start a new request.
			this.OnStart(sender, e);
		}


		/// <summary>
		/// An event handler called when the user selects to view the video properties.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnViewProperties(object sender, EventArgs e)
		{
			this.videoList.ShowProperties();
		}

		/// <summary>
		/// An event handler called when the user selects the view profile.
		/// </summary>
		/// <param name="id">The profile ID.</param>
		private void OnViewProfile(string id)
		{
			if (this.ViewProfileInApiV2 != null) this.ViewProfileInApiV2(id);
		}
	}
}
