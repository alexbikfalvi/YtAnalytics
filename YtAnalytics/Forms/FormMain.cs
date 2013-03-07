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
using System.Globalization;
using System.Windows.Forms;
using YtAnalytics.Controls;
using YtApi.Api.V2;
using YtApi.Api.V2.Data;
using YtCrawler;
using DotNetApi.Windows;
using DotNetApi.Windows.Controls;
using Microsoft.Win32;

namespace YtAnalytics.Forms
{
	public partial class FormMain : Form
	{
		// Crawler.
		private Crawler crawler;

		// UI formatter.
		private Formatting formatting = new Formatting();

		// Side menu items.
		private SideMenuItem sideMenuBrowse;
		private SideMenuItem sideMenuDatabase;
		private SideMenuItem sideMenuConfiguration;
		private SideMenuItem sideMenuLog;
		private SideMenuItem sideMenuComments;

		// Tree view nodes.
		private TreeNode treeNodeBrowserApi2;
		private TreeNode treeNodeBrowserApi2VideosFeedsInfo;
		private TreeNode treeNodeBrowserApi2Video;
		private TreeNode treeNodeBrowserApi2VideoComments;
		private TreeNode treeNodeBrowserApi2SearchFeed;
		private TreeNode treeNodeBrowserApi2StandardFeed;
		private TreeNode treeNodeBrowserApi2RelatedVideosFeed;
		private TreeNode treeNodeBrowserApi2ResponseVideosFeed;
		private TreeNode treeNodeBrowserApi2UserFeedsInfo;
		private TreeNode treeNodeBrowserApi2User;
		private TreeNode treeNodeBrowserApi2UploadsFeed;
		private TreeNode treeNodeBrowserApi2FavoritesFeed;
		private TreeNode treeNodeBrowserApi2Playlists;
		private TreeNode treeNodeBrowserApi2PlaylistFeed;
		private TreeNode treeNodeBrowserApi2VideoCategories;

		private TreeNode treeNodeBrowserApi3;
		private TreeNode treeNodeBrowserApi3Videos;

		private TreeNode treeNodeBrowserWeb;
		private TreeNode treeNodeBrowserWebVideos;

		private TreeNode treeNodeDatabaseServers;

		private TreeNode treeNodeSettings;

		private TreeNode treeNodeComments;
		private TreeNode treeNodeCommentsVideos;
		private TreeNode treeNodeCommentsUsers;
		private TreeNode treeNodeCommentsPlaylists;

		// Side control.
		private Control controlSideSelected = null;

		// Panel control.
		private Control controlPanelSelected = null;

		// Panel controls.
		private ControlYtApi2Info controlYtApi2 = new ControlYtApi2Info();
		private ControlYtApi2VideosFeedsInfo controlYtApi2VideosFeedsInfo = new ControlYtApi2VideosFeedsInfo();
		private ControlYtApi2Video controlYtApi2Video = new ControlYtApi2Video();
		private ControlYtApi2CommentsFeed controlYtApi2CommentsFeed = new ControlYtApi2CommentsFeed();
		private ControlYtApi2Search controlYtApi2Search = new ControlYtApi2Search();
		private ControlYtApi2StandardFeed controlYtApi2StandardFeed = new ControlYtApi2StandardFeed();
		private ControlYtApi2VideosFeed controlYtApi2RelatedFeed = new ControlYtApi2VideosFeed();
		private ControlYtApi2VideosFeed controlYtApi2ResponseFeed = new ControlYtApi2VideosFeed();
		private ControlYtApi2UserFeedsInfo controlYtApi2UserFeedInfo = new ControlYtApi2UserFeedsInfo();
		private ControlYtApi2Profile controlYtApi2Profile = new ControlYtApi2Profile();
		private ControlYtApi2VideosFeed controlYtApi2UploadsFeed = new ControlYtApi2VideosFeed();
		private ControlYtApi2PlaylistsFeed controlYtApi2PlaylistsFeed = new ControlYtApi2PlaylistsFeed();
		private ControlYtApi2VideosFeed controlYtApi2PlaylistFeed = new ControlYtApi2VideosFeed();
		private ControlYtApi2VideosFeed controlYtApi2FavoritesFeed = new ControlYtApi2VideosFeed();
		private ControlYtApi3Info controlYtApi3 = new ControlYtApi3Info();
		private ControlWeb controlWeb = new ControlWeb();
		private ControlWebStatistics controlWebStatistics = new ControlWebStatistics();
		private ControlServers controlDatabaseServers = new ControlServers();
		private ControlSettings controlSettings = new ControlSettings();
		private ControlLog controlLog = new ControlLog();
		private ControlCommentsInfo controlCommentsInfo = new ControlCommentsInfo();
		private ControlComments controlCommentsVideos = new ControlComments();
		private ControlComments controlCommentsUsers = new ControlComments();
		private ControlComments controlCommentsPlaylists = new ControlComments();

		// Forms.
		private FormAbout formAbout = new FormAbout();

		/// <summary>
		/// Constructor for main form window.
		/// </summary>
		public FormMain(Crawler crawler)
		{
			InitializeComponent();

			// Initialize the crawler
			this.crawler = crawler;

			// Create the tree view items.
			this.treeNodeBrowserApi2VideoComments = new TreeNode("Comments",
				this.imageList.Images.IndexOfKey("FolderClosedComment"),
				this.imageList.Images.IndexOfKey("FolderOpenComment"));
			this.treeNodeBrowserApi2Video = new TreeNode("Video",
				this.imageList.Images.IndexOfKey("FileVideo"),
				this.imageList.Images.IndexOfKey("FileVideo"),
				new TreeNode[] {
					this.treeNodeBrowserApi2VideoComments
				});
			this.treeNodeBrowserApi2SearchFeed = new TreeNode("Search",
				this.imageList.Images.IndexOfKey("FolderClosedVideo"),
				this.imageList.Images.IndexOfKey("FolderOpenVideo"));
			this.treeNodeBrowserApi2StandardFeed = new TreeNode("Standard feeds",
				this.imageList.Images.IndexOfKey("FolderClosedVideo"),
				this.imageList.Images.IndexOfKey("FolderOpenVideo"));
			this.treeNodeBrowserApi2RelatedVideosFeed = new TreeNode("Related videos feed",
				this.imageList.Images.IndexOfKey("FolderClosedVideo"),
				this.imageList.Images.IndexOfKey("FolderOpenVideo"));
			this.treeNodeBrowserApi2ResponseVideosFeed = new TreeNode("Response videos feed",
				this.imageList.Images.IndexOfKey("FolderClosedVideo"),
				this.imageList.Images.IndexOfKey("FolderOpenVideo"));
			this.treeNodeBrowserApi2VideosFeedsInfo = new TreeNode("Global videos",
				this.imageList.Images.IndexOfKey("FolderClosedVideo"),
				this.imageList.Images.IndexOfKey("FolderOpenVideo"),
				new TreeNode[] {
					this.treeNodeBrowserApi2Video,
					this.treeNodeBrowserApi2SearchFeed,
					this.treeNodeBrowserApi2StandardFeed,
					this.treeNodeBrowserApi2RelatedVideosFeed,
					this.treeNodeBrowserApi2ResponseVideosFeed
				});

			this.treeNodeBrowserApi2UploadsFeed = new TreeNode("Uploads feed",
				this.imageList.Images.IndexOfKey("FolderClosedVideo"),
				this.imageList.Images.IndexOfKey("FolderOpenVideo"));
			this.treeNodeBrowserApi2PlaylistFeed = new TreeNode("Playlist feed",
				this.imageList.Images.IndexOfKey("FolderClosedVideo"),
				this.imageList.Images.IndexOfKey("FolderOpenVideo"));
			this.treeNodeBrowserApi2Playlists = new TreeNode("Playlists",
				this.imageList.Images.IndexOfKey("FolderClosedPlay"),
				this.imageList.Images.IndexOfKey("FolderOpenPlay"),
				new TreeNode[] {
					this.treeNodeBrowserApi2PlaylistFeed
				});
			this.treeNodeBrowserApi2FavoritesFeed = new TreeNode("Favorites feed",
				this.imageList.Images.IndexOfKey("FolderClosedVideo"),
				this.imageList.Images.IndexOfKey("FolderOpenVideo"));
			this.treeNodeBrowserApi2User = new TreeNode("User",
				this.imageList.Images.IndexOfKey("FileUser"),
				this.imageList.Images.IndexOfKey("FileUser"),
				new TreeNode[] {
					this.treeNodeBrowserApi2UploadsFeed,
					this.treeNodeBrowserApi2FavoritesFeed,
					this.treeNodeBrowserApi2Playlists
				});
			this.treeNodeBrowserApi2UserFeedsInfo = new TreeNode("User videos",
				this.imageList.Images.IndexOfKey("FolderClosedUser"),
				this.imageList.Images.IndexOfKey("FolderOpenUser"),
				new TreeNode[] {
					this.treeNodeBrowserApi2User
				});

			this.treeNodeBrowserApi2VideoCategories = new TreeNode("Video categories",
				this.imageList.Images.IndexOfKey("Categories"),
				this.imageList.Images.IndexOfKey("Categories"));

			this.treeNodeBrowserApi2 = new TreeNode("YouTube API version 2",
				this.imageList.Images.IndexOfKey("ServerBrowse"),
				this.imageList.Images.IndexOfKey("ServerBrowse"),
				new TreeNode[] {
					this.treeNodeBrowserApi2VideosFeedsInfo,
					this.treeNodeBrowserApi2UserFeedsInfo,
					this.treeNodeBrowserApi2VideoCategories
				});

			this.treeNodeBrowserApi3Videos = new TreeNode("Videos",
				this.imageList.Images.IndexOfKey("FolderClosedVideo"),
				this.imageList.Images.IndexOfKey("FolderOpenVideo"));
			this.treeNodeBrowserApi3 = new TreeNode("YouTube API version 3",
				this.imageList.Images.IndexOfKey("ServerBrowse"),
				this.imageList.Images.IndexOfKey("ServerBrowse"),
				new TreeNode[] {
					this.treeNodeBrowserApi3Videos
				});

			this.treeNodeBrowserWebVideos = new TreeNode("Videos",
				this.imageList.Images.IndexOfKey("FileGraphLine"),
				this.imageList.Images.IndexOfKey("FileGraphLine"));
			this.treeNodeBrowserWeb = new TreeNode("YouTube Web",
				this.imageList.Images.IndexOfKey("GlobeBrowse"),
				this.imageList.Images.IndexOfKey("GlobeBrowse"),
				new TreeNode[] {
					this.treeNodeBrowserWebVideos
				});

			this.treeNodeSettings = new TreeNode("Settings",
				this.imageList.Images.IndexOfKey("Settings"),
				this.imageList.Images.IndexOfKey("Settings"));

			this.treeNodeDatabaseServers = new TreeNode("Servers",
				this.imageList.Images.IndexOfKey("ServersDatabase"),
				this.imageList.Images.IndexOfKey("ServersDatabase"));

			this.treeNodeCommentsVideos = new TreeNode("Videos",
				this.imageList.Images.IndexOfKey("CommentVideo"),
				this.imageList.Images.IndexOfKey("CommentVideo"));
			this.treeNodeCommentsUsers = new TreeNode("Users",
				this.imageList.Images.IndexOfKey("CommentUser"),
				this.imageList.Images.IndexOfKey("CommentUser"));
			this.treeNodeCommentsPlaylists = new TreeNode("Playlists",
				this.imageList.Images.IndexOfKey("CommentPlay"),
				this.imageList.Images.IndexOfKey("CommentPlay"));
			this.treeNodeComments = new TreeNode("Comments",
				this.imageList.Images.IndexOfKey("Comments"),
				this.imageList.Images.IndexOfKey("Comments"),
				new TreeNode[] {
					this.treeNodeCommentsVideos,
					this.treeNodeCommentsUsers,
					this.treeNodeCommentsPlaylists
				});

			// Add the panel controls to the split container.
			this.splitContainer.Panel2.Controls.Add(this.controlYtApi2);
			this.splitContainer.Panel2.Controls.Add(this.controlYtApi2VideosFeedsInfo);
			this.splitContainer.Panel2.Controls.Add(this.controlYtApi3);
			this.splitContainer.Panel2.Controls.Add(this.controlYtApi2Video);
			this.splitContainer.Panel2.Controls.Add(this.controlYtApi2CommentsFeed);
			this.splitContainer.Panel2.Controls.Add(this.controlYtApi2StandardFeed);
			this.splitContainer.Panel2.Controls.Add(this.controlYtApi2Search);
			this.splitContainer.Panel2.Controls.Add(this.controlYtApi2RelatedFeed);
			this.splitContainer.Panel2.Controls.Add(this.controlYtApi2ResponseFeed);
			this.splitContainer.Panel2.Controls.Add(this.controlYtApi2UserFeedInfo);
			this.splitContainer.Panel2.Controls.Add(this.controlYtApi2Profile);
			this.splitContainer.Panel2.Controls.Add(this.controlYtApi2UploadsFeed);
			this.splitContainer.Panel2.Controls.Add(this.controlYtApi2PlaylistsFeed);
			this.splitContainer.Panel2.Controls.Add(this.controlYtApi2FavoritesFeed);
			this.splitContainer.Panel2.Controls.Add(this.controlYtApi2PlaylistFeed);
			this.splitContainer.Panel2.Controls.Add(this.controlWeb);
			this.splitContainer.Panel2.Controls.Add(this.controlWebStatistics);
			this.splitContainer.Panel2.Controls.Add(this.controlDatabaseServers);
			this.splitContainer.Panel2.Controls.Add(this.controlSettings);
			this.splitContainer.Panel2.Controls.Add(this.controlLog);
			this.splitContainer.Panel2.Controls.Add(this.controlCommentsInfo);
			this.splitContainer.Panel2.Controls.Add(this.controlCommentsVideos);
			this.splitContainer.Panel2.Controls.Add(this.controlCommentsUsers);
			this.splitContainer.Panel2.Controls.Add(this.controlCommentsPlaylists);

			// Add the panel controls as tags.
			this.treeNodeBrowserApi2.Tag = this.controlYtApi2;
			this.treeNodeBrowserApi2VideosFeedsInfo.Tag = this.controlYtApi2VideosFeedsInfo;
			this.treeNodeBrowserApi2Video.Tag = this.controlYtApi2Video;
			this.treeNodeBrowserApi2VideoComments.Tag = this.controlYtApi2CommentsFeed;
			this.treeNodeBrowserApi2SearchFeed.Tag = this.controlYtApi2Search;
			this.treeNodeBrowserApi2StandardFeed.Tag = this.controlYtApi2StandardFeed;
			this.treeNodeBrowserApi2RelatedVideosFeed.Tag = this.controlYtApi2RelatedFeed;
			this.treeNodeBrowserApi2ResponseVideosFeed.Tag = this.controlYtApi2ResponseFeed;
			this.treeNodeBrowserApi2UserFeedsInfo.Tag = this.controlYtApi2UserFeedInfo;
			this.treeNodeBrowserApi2User.Tag = this.controlYtApi2Profile;
			this.treeNodeBrowserApi2UploadsFeed.Tag = this.controlYtApi2UploadsFeed;
			this.treeNodeBrowserApi2Playlists.Tag = this.controlYtApi2PlaylistsFeed;
			this.treeNodeBrowserApi2FavoritesFeed.Tag = this.controlYtApi2FavoritesFeed;
			this.treeNodeBrowserApi2PlaylistFeed.Tag = this.controlYtApi2PlaylistFeed;
			
			this.treeNodeBrowserApi2VideoCategories.Tag = null;

			this.treeNodeBrowserApi3.Tag = this.controlYtApi3;
			this.treeNodeBrowserWeb.Tag = this.controlWeb;
			this.treeNodeBrowserWebVideos.Tag = this.controlWebStatistics;

			this.treeNodeDatabaseServers.Tag = this.controlDatabaseServers;

			this.treeNodeSettings.Tag = this.controlSettings;
			this.controlPanelLog.Tag = this.controlLog;
			this.treeNodeComments.Tag = this.controlCommentsInfo;
			this.treeNodeCommentsVideos.Tag = this.controlCommentsVideos;
			this.treeNodeCommentsUsers.Tag = this.controlCommentsUsers;
			this.treeNodeCommentsPlaylists.Tag = this.controlCommentsPlaylists;

			// Add the tree nodes to the side panel tree views.
			this.controlPanelBrowser.AddRange(
				new TreeNode[] {
					this.treeNodeBrowserApi2,
					this.treeNodeBrowserApi3,
					this.treeNodeBrowserWeb
				});
			this.controlPanelDatabase.Add(this.treeNodeDatabaseServers);
			this.controlPanelConfiguration.Add(this.treeNodeSettings);
			this.controlPanelComments.Add(this.treeNodeComments);

			// Create the side menu items
			this.sideMenuBrowse = this.sideMenu.AddItem(
				"Browser",
				Resources.ServersBrowse_16,
				Resources.ServersBrowse_32,
				this.SideMenuSelect,
				this.controlPanelBrowser
				);
			this.sideMenuDatabase = this.sideMenu.AddItem(
				"Database",
				Resources.ServersDatabase_16,
				Resources.ServersDatabase_32,
				this.SideMenuSelect,
				this.controlPanelDatabase
				);
			this.sideMenuConfiguration = this.sideMenu.AddItem(
				"Configuration",
				Resources.ConfigurationSettings_16,
				Resources.ConfigurationSettings_32,
				this.SideMenuSelect,
				this.controlPanelConfiguration
				);
			this.sideMenuLog = this.sideMenu.AddItem(
				"Log",
				Resources.Log_16,
				Resources.Log_32,
				this.SideMenuSelectLog,
				this.controlPanelLog
				);
			this.sideMenuComments = this.sideMenu.AddItem(
				"Comments",
				Resources.Comments_16,
				Resources.Comments_32,
				this.SideMenuSelect,
				this.controlPanelComments
				);

			// Initialize the controls.
			this.controlYtApi2Video.Initialize(this.crawler);
			this.controlYtApi2CommentsFeed.Initialize(this.crawler);
			this.controlYtApi2StandardFeed.Initialize(this.crawler);
			this.controlYtApi2Search.Initialize(this.crawler);
			this.controlYtApi2RelatedFeed.Initialize(this.crawler, new VideosFeedEventHandler(YouTubeUri.GetRelatedVideosFeed), "&Video:", "APIv2 Related Videos Feed");
			this.controlYtApi2ResponseFeed.Initialize(this.crawler, new VideosFeedEventHandler(YouTubeUri.GetResponseVideosFeed), "&Video:", "APIv2 Response Videos Feed");
			this.controlYtApi2Profile.Initialize(this.crawler);
			this.controlYtApi2PlaylistsFeed.Initialize(this.crawler);
			this.controlYtApi2UploadsFeed.Initialize(this.crawler, new VideosFeedEventHandler(YouTubeUri.GetUploadsFeed), "&User:", "APIv2 Uploads Videos Feed");
			this.controlYtApi2FavoritesFeed.Initialize(this.crawler, new VideosFeedEventHandler(YouTubeUri.GetFavoritesFeed), "&User:", "APIv2 Favorites Videos Feed");
			this.controlYtApi2PlaylistFeed.Initialize(this.crawler, new VideosFeedEventHandler(YouTubeUri.GetPlaylistFeed), "&Playlist:", "APIv2 Playlist Videos Feed");
			this.controlDatabaseServers.Initialize(this.crawler, this.treeNodeDatabaseServers, this.splitContainer.Panel2.Controls, this.imageList, new int[] {
				this.imageList.Images.IndexOfKey("ServerDown"),
				this.imageList.Images.IndexOfKey("ServerUp"),
				this.imageList.Images.IndexOfKey("ServerWarning"),
				this.imageList.Images.IndexOfKey("ServerBusy"),
				this.imageList.Images.IndexOfKey("ServerBusy")
			});
			this.controlSettings.Initialize(this.crawler);
			this.controlWebStatistics.Initialize(this.crawler);
			this.controlLog.Initialize(this.crawler);
			this.controlCommentsVideos.Initialize(this.crawler.Comments.Videos, YtCrawler.Comments.Comment.CommentType.Video);
			this.controlCommentsUsers.Initialize(this.crawler.Comments.Users, YtCrawler.Comments.Comment.CommentType.User);
			this.controlCommentsPlaylists.Initialize(this.crawler.Comments.Playlists, YtCrawler.Comments.Comment.CommentType.Playlist);

			// Set the control events.
			this.controlYtApi2.VideosGlobalClick += this.BrowserApi2VideosGlobalClick;
			this.controlYtApi2.VideosUserClick += this.BrowserApi2VideosUserClick;
			this.controlYtApi2.CategoriesClick += this.BrowserApi2CategoriesClick;

			this.controlYtApi2VideosFeedsInfo.VideoClick += this.BrowserApi2VideoClick;
			this.controlYtApi2VideosFeedsInfo.VideoCommentsClick += this.BrowserApi2VideoCommentsClick;
			this.controlYtApi2VideosFeedsInfo.SearchFeedClick += this.BrowserApi2SearchFeedClick;
			this.controlYtApi2VideosFeedsInfo.StandardFeedClick += this.BrowserApi2StandardFeedClick;
			this.controlYtApi2VideosFeedsInfo.RelatedVideosFeedClick += this.BrowserApi2RelatedVideosFeedClick;
			this.controlYtApi2VideosFeedsInfo.ResponseVideosFeedClick += this.BrowserApi2ResponseVideosFeedClick;

			this.controlYtApi2Video.ViewVideoCommentsInApiV2 += this.ViewVideoCommentsInApiV2;
			this.controlYtApi2Video.ViewAuthorInApiV2 += this.ViewApiV2User;
			this.controlYtApi2Video.ViewVideoRelatedInApiV2 += this.ViewRelatedVideosInApiV2;
			this.controlYtApi2Video.ViewVideoResponsesInApiV2 += this.ViewResponseVideosInApiV2;
			this.controlYtApi2Video.ViewVideoInWeb += this.ViewVideoInWeb;
			this.controlYtApi2Video.Comment += this.CommentVideo;

			this.controlYtApi2StandardFeed.ViewVideoInApiV2 += this.ViewVideoInApiV2;
			this.controlYtApi2StandardFeed.ViewAuthorInApiV2 += this.ViewApiV2User;
			this.controlYtApi2StandardFeed.ViewRelatedVideosInApiV2 += this.ViewRelatedVideosInApiV2;
			this.controlYtApi2StandardFeed.ViewResponseVideosInApiV2 += this.ViewResponseVideosInApiV2;
			this.controlYtApi2StandardFeed.ViewVideoInWeb += this.ViewVideoInWeb;
			this.controlYtApi2StandardFeed.Comment += this.CommentVideo;

			this.controlYtApi2Search.ViewVideoInApiV2 += this.ViewVideoInApiV2;
			this.controlYtApi2Search.ViewAuthorInApiV2 += this.ViewApiV2User;
			this.controlYtApi2Search.ViewRelatedVideosInApiV2 += this.ViewRelatedVideosInApiV2;
			this.controlYtApi2Search.ViewResponseVideosInApiV2 += this.ViewResponseVideosInApiV2;
			this.controlYtApi2Search.ViewVideoInWeb += this.ViewVideoInWeb;
			this.controlYtApi2Search.Comment += this.CommentVideo;

			this.controlYtApi2RelatedFeed.ViewVideoInApiV2 += this.ViewVideoInApiV2;
			this.controlYtApi2RelatedFeed.ViewAuthorInApiV2 += this.ViewApiV2User;
			this.controlYtApi2RelatedFeed.ViewRelatedVideosInApiV2 += this.ViewRelatedVideosInApiV2;
			this.controlYtApi2RelatedFeed.ViewResponseVideosInApiV2 += this.ViewResponseVideosInApiV2;
			this.controlYtApi2RelatedFeed.ViewVideoInWeb += this.ViewVideoInWeb;
			this.controlYtApi2RelatedFeed.Comment += this.CommentVideo;

			this.controlYtApi2ResponseFeed.ViewVideoInApiV2 += this.ViewVideoInApiV2;
			this.controlYtApi2ResponseFeed.ViewRelatedVideosInApiV2 += this.ViewRelatedVideosInApiV2;
			this.controlYtApi2ResponseFeed.ViewResponseVideosInApiV2 += this.ViewResponseVideosInApiV2;
			this.controlYtApi2ResponseFeed.ViewAuthorInApiV2 += this.ViewApiV2User;
			this.controlYtApi2ResponseFeed.ViewVideoInWeb += this.ViewVideoInWeb;
			this.controlYtApi2ResponseFeed.Comment += this.CommentVideo;

			this.controlYtApi2UserFeedInfo.UserClick += this.BrowserApi2UserClick;
			this.controlYtApi2UserFeedInfo.UploadsFeedClick += this.BrowserApi2UserUploadsClick;
			this.controlYtApi2UserFeedInfo.FavoritesFeedClick += this.BrowserApi2UserFavoritesClick;
			this.controlYtApi2UserFeedInfo.PlaylistsFeedClick += this.BrowserApi2UserPlaylistsClick;
			this.controlYtApi2UserFeedInfo.PlaylistFeedClick += this.BrowserApi2PlaylistVideosClick;

			this.controlYtApi2Profile.ViewUserUploadsInApiV2 += this.ViewApiV2UploadedVideos;
			this.controlYtApi2Profile.ViewUserFavoritesInApiV2 += this.ViewApiV2FavoritedVideos;
			this.controlYtApi2Profile.ViewUserPlaylistsInApiV2 += this.ViewApiV2Playlists;
			this.controlYtApi2Profile.Comment += this.CommentUser;

			this.controlYtApi2UploadsFeed.ViewVideoInApiV2 += this.ViewVideoInApiV2;
			this.controlYtApi2UploadsFeed.ViewAuthorInApiV2 += this.ViewApiV2User;
			this.controlYtApi2UploadsFeed.ViewRelatedVideosInApiV2 += this.ViewRelatedVideosInApiV2;
			this.controlYtApi2UploadsFeed.ViewResponseVideosInApiV2 += this.ViewResponseVideosInApiV2;
			this.controlYtApi2UploadsFeed.ViewVideoInWeb += this.ViewVideoInWeb;
			this.controlYtApi2UploadsFeed.Comment += this.CommentVideo;

			this.controlYtApi2FavoritesFeed.ViewVideoInApiV2 += this.ViewVideoInApiV2;
			this.controlYtApi2FavoritesFeed.ViewAuthorInApiV2 += this.ViewApiV2User;
			this.controlYtApi2FavoritesFeed.ViewRelatedVideosInApiV2 += this.ViewRelatedVideosInApiV2;
			this.controlYtApi2FavoritesFeed.ViewResponseVideosInApiV2 += this.ViewResponseVideosInApiV2;
			this.controlYtApi2FavoritesFeed.ViewVideoInWeb += this.ViewVideoInWeb;
			this.controlYtApi2FavoritesFeed.Comment += this.CommentVideo;

			this.controlYtApi2PlaylistsFeed.ViewPlaylistAuthorInApiV2 += this.ViewApiV2User;
			this.controlYtApi2PlaylistsFeed.ViewPlaylistVideosInApiV2 += this.ViewApiV2Playlist;
			this.controlYtApi2PlaylistsFeed.Comment += this.CommentPlaylist;

			this.controlYtApi2PlaylistFeed.ViewVideoInApiV2 += this.ViewVideoInApiV2;
			this.controlYtApi2PlaylistFeed.ViewRelatedVideosInApiV2 += this.ViewRelatedVideosInApiV2;
			this.controlYtApi2PlaylistFeed.ViewResponseVideosInApiV2 += this.ViewResponseVideosInApiV2;
			this.controlYtApi2PlaylistFeed.ViewAuthorInApiV2 += this.ViewApiV2User;
			this.controlYtApi2PlaylistFeed.ViewVideoInWeb += this.ViewVideoInWeb;
			this.controlYtApi2PlaylistFeed.Comment += this.CommentVideo;

			this.controlWeb.ClickVideoStatistics += new EventHandler(this.BrowserWebVideosClick);

			this.controlWebStatistics.Comment += this.CommentVideo;

			this.controlCommentsInfo.ClickVideos += new EventHandler(this.BrowserCommentsVideosClick);

			// Selected control
			this.controlPanelSelected = this.labelNotAvailable;

			// Set the font.
			this.formatting.SetFont(this);
		}

		/// <summary>
		/// An event handler called when the selected side menu item has changed.
		/// </summary>
		/// <param name="item">The side menu item.</param>
		private void SideMenuSelect(SideMenuItem item)
		{
			// If the tag of the menu item is not null.
			if (null != item.Tag)
			{
				// Convert the tag to a control.
				ControlSide control = item.Tag as ControlSide;

				// If the selected control is different from the new control.
				if (control != this.controlSideSelected)
				{
					// If the current selected side control is not null.
					if (null != this.controlSideSelected)
					{
						// Hide that control.
						this.controlSideSelected.Hide();
					}
					// Show the control.
					control.Show();
					// Focus on the control.
					control.Select();
					// Set the selected side control.
					this.controlSideSelected = control;
				}
			}
		}

		private void SideMenuSelectLog(SideMenuItem item)
		{
			// Select the side menu item.
			this.SideMenuSelect(item);
			// Refresh the log.
			this.controlLog.DateChanged(this, new DateRangeEventArgs(this.controlPanelLog.Calendar.SelectionStart, this.controlPanelLog.Calendar.SelectionEnd));
		}

		/// <summary>
		/// An event handler called when the right panel control selection has changed.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnControlChanged(object sender, ControlEventArgs e)
		{
			// If the selected control has not changed, do nothing.
			if (e.Control == this.controlPanelSelected) return;

			// Hide the current selected control.
			if (null != this.controlPanelSelected)
			{
				this.controlPanelSelected.Hide();
			}

			// If the tree node tag is not null.
			if (null != e.Control)
			{
				// Show the control.
				e.Control.Show();
				// Set the selected control.
				this.controlPanelSelected = e.Control;
			}
			else
			{
				// Display the default message.
				this.labelNotAvailable.Show();
				// Set the selected control.
				this.controlPanelSelected = this.labelNotAvailable;
			}
		}

		private void BrowserApi2VideosGlobalClick(object sender, EventArgs e)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2VideosFeedsInfo;
		}

		private void BrowserApi2VideosUserClick(object sender, EventArgs e)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2UserFeedsInfo;
		}

		private void BrowserApi2CategoriesClick(object sender, EventArgs e)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2VideoCategories;
		}

		private void BrowserApi2VideoClick(object sender, EventArgs e)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2Video;
		}

		private void BrowserApi2VideoCommentsClick(object sender, EventArgs e)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2VideoComments;
		}

		private void BrowserApi2SearchFeedClick(object sender, EventArgs e)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2SearchFeed;
		}

		private void BrowserApi2StandardFeedClick(object sender, EventArgs e)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2StandardFeed;
		}

		private void BrowserApi2RelatedVideosFeedClick(object sender, EventArgs e)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2RelatedVideosFeed;
		}

		private void BrowserApi2ResponseVideosFeedClick(object sender, EventArgs e)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2ResponseVideosFeed;
		}

		private void BrowserApi2UserClick(object sender, EventArgs e)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2User;
		}

		private void BrowserApi2UserUploadsClick(object sender, EventArgs e)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2UploadsFeed;
		}

		private void BrowserApi2UserFavoritesClick(object sender, EventArgs e)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2FavoritesFeed;
		}

		private void BrowserApi2UserPlaylistsClick(object sender, EventArgs e)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2Playlists;
		}

		private void BrowserApi2PlaylistVideosClick(object sender, EventArgs e)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2PlaylistFeed;
		}

		private void BrowserWebVideosClick(object sender, EventArgs e)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserWebVideos;
		}

		private void BrowserCommentsVideosClick(object sender, EventArgs e)
		{
			this.sideMenu.SelectedItem = this.sideMenuComments;
			this.controlPanelComments.SelectedNode = this.treeNodeCommentsVideos;
		}

		private void ViewVideoInApiV2(Video video)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2Video;
			this.controlYtApi2Video.View(video);
		}

		private void ViewVideoCommentsInApiV2(string video)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2VideoComments;
			this.controlYtApi2CommentsFeed.View(video);
		}

		private void ViewRelatedVideosInApiV2(Video video)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2RelatedVideosFeed;
			this.controlYtApi2RelatedFeed.View(video.Id);
		}

		private void ViewResponseVideosInApiV2(Video video)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2ResponseVideosFeed;
			this.controlYtApi2ResponseFeed.View(video.Id);
		}

		private void ViewApiV2User(string user)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2User;
			this.controlYtApi2Profile.View(user);
		}

		private void ViewApiV2UploadedVideos(Profile profile)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2UploadsFeed;
			this.controlYtApi2UploadsFeed.View(profile.Id);
		}

		private void ViewApiV2FavoritedVideos(Profile profile)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2FavoritesFeed;
			this.controlYtApi2FavoritesFeed.View(profile.Id);
		}

		private void ViewApiV2Playlists(Profile profile)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2Playlists;
			this.controlYtApi2PlaylistsFeed.View(profile.Id);
		}

		private void ViewApiV2Playlist(string playlist)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserApi2PlaylistFeed;
			this.controlYtApi2PlaylistFeed.View(playlist);
		}

		private void ViewVideoInWeb(Video video)
		{
			this.sideMenu.SelectedItem = this.sideMenuBrowse;
			this.controlPanelBrowser.SelectedNode = this.treeNodeBrowserWebVideos;
			this.controlWebStatistics.View(video.Id);
		}

		private void CommentVideo(string video)
		{
			this.sideMenu.SelectedItem = this.sideMenuComments;
			this.controlPanelComments.SelectedNode = this.treeNodeCommentsVideos;
			this.controlCommentsVideos.AddComment(video);
		}

		private void CommentUser(string user)
		{
			this.sideMenu.SelectedItem = this.sideMenuComments;
			this.controlPanelComments.SelectedNode = this.treeNodeCommentsUsers;
			this.controlCommentsUsers.AddComment(user);
		}

		private void CommentPlaylist(string playlist)
		{
			this.sideMenu.SelectedItem = this.sideMenuComments;
			this.controlPanelComments.SelectedNode = this.treeNodeCommentsPlaylists;
			this.controlCommentsPlaylists.AddComment(playlist);
		}

		/// <summary>
		/// Closes the current window and the application.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void Close(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Opens the about form.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OpenAboutForm(object sender, EventArgs e)
		{
			this.formAbout.ShowDialog(this);
		}

		/// <summary>
		/// An event handler called when the log date has changed.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnLogDateChanged(object sender, DateRangeEventArgs e)
		{
			// Update the log.
			this.controlLog.DateChanged(sender, e);
		}

		// private void TestApi(object sender, EventArgs e)
	   // {
	   //	 ytService.Key = FormMain.apiKey;
	   //	 this.videosResource = new VideosResource(this.ytService, new NullAuthenticator());
	   //	 this.list = this.videosResource.List("MlMj3VEYBMA", "snippet");

	   //	 try
	   //	 {
	   //		 IAsyncResult result = this.list.BeginFetch(new AsyncCallback(this.CallbackApi), this);
	   //	 }
	   //	 catch (GoogleApiRequestException exception)
	   //	 {
	   //		 this.textBoxResults.AppendText(exception.Message);
	   //	 }
	   // }

	   // private void CallbackApi(IAsyncResult result)
	   // {
	   //	 if (this.InvokeRequired)
	   //	 {
	   //		 this.Invoke(new AsyncCallback(this.CallbackApi), new object[] { result });
	   //	 }
	   //	 else
	   //	 {
	   //		 try
	   //		 {
	   //			 VideoListResponse response = this.list.EndFetch(result);

	   //			 foreach (Google.Apis.Youtube.v3.Data.Video video in response.Items)
	   //			 {
	   //				 this.textBoxResults.AppendText("ID: " + video.Id + "\r\n");
	   //				 this.textBoxResults.AppendText("Etag: " + video.ETag + "\r\n");
	   //				 this.textBoxResults.AppendText("Title: " + video.Snippet.Title + "\r\n");
	   //			 }
	   //		 }
	   //		 catch (GoogleApiRequestException exception)
	   //		 {
	   //			 this.textBoxResults.AppendText(exception.Message);
	   //		 }
	   //	 }
	   //}

	   // private void TestAjax(object sender, EventArgs e)
	   // {
	   //	 try
	   //	 {
	   //		 IAsyncResult result = this.ajaxRequest.Begin("MlMj3VEYBMA", this.CallbackAjax);
	   //	 }
	   //	 catch (Exception exception)
	   //	 {
	   //		 this.textBoxResults.AppendText(exception.Message);
	   //	 }
	   // }

	   // private void CallbackAjax(IAsyncResult result)
	   // {
	   //	 if (this.InvokeRequired)
	   //	 {
	   //		 this.Invoke(new AsyncCallback(this.CallbackAjax), new object[] { result });
	   //	 }
	   //	 else
	   //	 {
	   //		 try
	   //		 {
	   //			 YtApi.Ajax.AjaxVideoStatistics statistics = this.ajaxRequest.End(result);

	   //			 // Serialize the statistics object
	   //			 BinaryFormatter formatter = new BinaryFormatter();
	   //			 MemoryStream streamWrite = new MemoryStream();
	   //			 formatter.Serialize(streamWrite, statistics);

	   //			 string data = Convert.ToBase64String(streamWrite.ToArray());

	   //			 MemoryStream streamRead = new MemoryStream();
	   //			 byte[] bytes = Convert.FromBase64String(data);
	   //			 streamRead.Write(bytes, 0, bytes.Length);

	   //			 streamRead.Seek(0, SeekOrigin.Begin);

	   //			 YtApi.Ajax.AjaxVideoStatistics stat = (YtApi.Ajax.AjaxVideoStatistics)formatter.Deserialize(streamRead);

	   //			 this.textBoxResults.AppendText(data);
	   //		 }
	   //		 catch (Exception exception)
	   //		 {
	   //			 this.textBoxResults.AppendText(exception.GetType().ToString() + " " + exception.Message);
	   //		 }
	   //	 }
	   // }

	   // private void TestCrawl(object sender, EventArgs e)
	   // {
	   //	 try
	   //	 {
	   //		 IAsyncResult result = this.ytRequest.Begin(new Uri("https://gdata.youtube.com/feeds/api/standardfeeds/most_popular"), this.CallbackCrawl);
	   //	 }
	   //	 catch (Exception exception)
	   //	 {
	   //		 this.textBoxResults.AppendText(exception.Message);
	   //	 }
	   // }

	   // private void CallbackCrawl(IAsyncResult result)
	   // {
	   //	 if (this.InvokeRequired)
	   //	 {
	   //		 this.Invoke(new AsyncCallback(this.CallbackCrawl), new object[] { result });
	   //	 }
	   //	 else
	   //	 {
	   //		 try
	   //		 {
	   //			 Feed<YtApi.Api.V2.Data.Video> feed = this.ytRequest.EndFeedVideo(result);
	   //		 }
	   //		 catch (Exception exception)
	   //		 {
	   //			 this.textBoxResults.AppendText(exception.GetType().ToString() + " " + exception.Message);
	   //		 }
	   //	 }
	   // }

	   // private void TestCategories(object sender, EventArgs e)
	   // {
	   //	 try
	   //	 {
	   //		 this.textBoxResults.AppendText("Categories refresh started...\r\n");
	   //		 IAsyncResult result = this.ytCategories.BeginRefresh(this.CallbackCategories, null);
	   //	 }
	   //	 catch (YouTubeException exception)
	   //	 {
	   //		 this.textBoxResults.AppendText(exception.GetType().ToString() + " " + exception.Message);
	   //	 }
	   // }

	   // private void CallbackCategories(IAsyncResult result)
	   // {
	   //	 if (this.InvokeRequired)
	   //	 {
	   //		 this.Invoke(new AsyncCallback(this.CallbackCategories), new object[] { result });
	   //	 }
	   //	 else
	   //	 {
	   //		 try
	   //		 {
	   //			 this.ytCategories.EndRefresh(result);
	   //			 this.textBoxResults.AppendText("Categories refresh completed.\r\n");
	   //		 }
	   //		 catch (Exception exception)
	   //		 {
	   //			 this.textBoxResults.AppendText(exception.GetType().ToString() + " " + exception.Message);
	   //		 }
	   //	 }
	   // }
	}
}
