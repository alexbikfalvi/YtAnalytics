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
using System.Text;
using DotNetApi;

namespace InetApi.YouTube.Api.V2
{
	/// <summary>
	/// A class that generates YouTube URIs.
	/// </summary>
	public class YouTubeUri
	{
		private static readonly string[] standardFeedIds = {
															   "top_rated",
															   "top_favorites",
															   "most_shared", // Experimental
															   "most_popular",
															   "most_recent",
															   "most_discussed",
															   "most_responded",
															   "recently_featured",
															   "on_the_web" // Experimental
														   };

		private static readonly string[] standardFeedNames = {
																 "Top rated",
																 "Top favorites",
																 "Most shared (experimental)",
																 "Most popular",
																 "Most recent",
																 "Most discussed",
																 "Most responded",
																 "Recently featured",
																 "Trending videos (experimental)"
															 };

		private static readonly string[] timeIds = { "all_time", "today", "this_week", "this_month" };
		
		private static readonly string[] timeNames = { "All time", "Today", "This week", "This month" };

		private static readonly string[] regionIds = {
														 "DZ", "AR", "AU", "BD", "BE", "BR", "BG", "CA", "CL", "CO", "HR", "CZ", "DK", "EG", "EE", "ET", "FI", "FR", "DE", "GH",
														 "GB", "GR", "HK", "HU", "IS", "IN", "ID", "IR", "IE", "IL", "IT", "JP", "JO", "KE", "LV", "LT", "MY", "MX", "MA", "NL",
														 "NZ", "NG", "NO", "PK", "PE", "PH", "PL", "PT", "RO", "RU", "SA", "SN", "RS", "SG", "SK", "SI", "ZA", "KR", "ES", "SE",
														 "TW", "TZ", "TH", "TN", "TR", "UG", "UA", "AE", "US", "VN", "YE"
													 };

		private static readonly string[] regionNames = {
														   "Algeria",
														   "Argentina",
														   "Australia",
														   "Bangladesh",
														   "Belgium",
														   "Brazil",
														   "Bulgaria",
														   "Canada",
														   "Chile",
														   "Colombia",
														   "Croatia",
														   "Czech Republic",
														   "Denmark",
														   "Egypt",
														   "Estonia",
														   "Ethiopia",
														   "Finland",
														   "France",
														   "Germany",
														   "Ghana",
														   "Great Britain",
														   "Greece",
														   "Hong Kong",
														   "Hungary",
														   "Iceland",
														   "India",
														   "Indonesia",
														   "Iran, Islamic Republic of",
														   "Ireland",
														   "Israel",
														   "Italy",
														   "Japan",
														   "Jordan",
														   "Kenya",
														   "Latvia",
														   "Lithuania",
														   "Malaysia",
														   "Mexico",
														   "Morocco",
														   "Netherlands",
														   "New Zealand",
														   "Nigeria",
														   "Norway",
														   "Pakistan",
														   "Peru",
														   "Philippines",
														   "Poland",
														   "Portugal",
														   "Romania",
														   "Russia",
														   "Saudi Arabia",
														   "Senegal",
														   "Serbia",
														   "Singapore",
														   "Slovakia",
														   "Slovenia",
														   "South Africa",
														   "South Korea",
														   "Spain",
														   "Sweden",
														   "Taiwan",
														   "Tanzania, United Republic of",
														   "Thailand",
														   "Tunisia",
														   "Turkey",
														   "Uganda",
														   "Ukraine",
														   "United Arab Emirates",
														   "United States",
														   "Vietnam",
														   "Yemen"
													   };


		private static readonly YouTubeTimeId[][] timeFeedValidity = {
																		 new YouTubeTimeId[] { YouTubeTimeId.AllTime, YouTubeTimeId.ThisMonth, YouTubeTimeId.ThisWeek, YouTubeTimeId.Today },	// Top rated
																		 new YouTubeTimeId[] { YouTubeTimeId.AllTime, YouTubeTimeId.ThisMonth, YouTubeTimeId.ThisWeek, YouTubeTimeId.Today },	// Top favorites
																		 new YouTubeTimeId[] { },																	// Most shared
																		 new YouTubeTimeId[] { YouTubeTimeId.AllTime, YouTubeTimeId.Today },										// Most popular
																		 new YouTubeTimeId[] { },																	// Most recent
																		 new YouTubeTimeId[] { YouTubeTimeId.AllTime, YouTubeTimeId.ThisMonth, YouTubeTimeId.ThisWeek, YouTubeTimeId.Today },	// Most discussed#
																		 new YouTubeTimeId[] { YouTubeTimeId.AllTime, YouTubeTimeId.ThisMonth, YouTubeTimeId.ThisWeek, YouTubeTimeId.Today },	// Most responded
																		 new YouTubeTimeId[] { },																	// Recently featured
																		 new YouTubeTimeId[] { },																	// Trending videos
																	 };

		private static readonly Uri uriCategories = new Uri("http://gdata.youtube.com/schemas/2007/categories.cat");

		/// <summary>
		/// 0 - region ID in the format "XX/"
		/// 1 - standard feed ID
		/// 2 - category ID in the format "_CATEGORY"
		/// </summary>
		private static readonly string uriStandardVideoFeedPattern = "https://gdata.youtube.com/feeds/api/standardfeeds/{0}{1}{2}?v=2";

		/// <summary>
		/// 0 - search query
		/// </summary>
		private static readonly string uriVideosFeedPattern = "http://gdata.youtube.com/feeds/api/videos?q={0}&v=2";

		/// <summary>
		/// 0 - video ID
		/// </summary>
		private static readonly string uriRelatedVideosFeedPattern = "https://gdata.youtube.com/feeds/api/videos/{0}/related?v=2";

		/// <summary>
		/// 0 - video ID
		/// </summary>
		private static readonly string uriResponseVideosFeedPattern = "http://gdata.youtube.com/feeds/api/videos/{0}/responses?v=2";

		/// <summary>
		/// 0 - video ID
		/// </summary>
		private static readonly string uriVideoEntryPattern = "https://gdata.youtube.com/feeds/api/videos/{0}?v=2";

		/// <summary>
		/// 0 - video ID
		/// </summary>
		private static readonly string uriVideoYouTubePattern = "http://www.youtube.com/watch?v={0}";

		/// <summary>
		/// 0 - user name
		/// </summary>
		private static readonly string uriUserYouTubePattern = "http://www.youtube.com/user/{0}";

		/// <summary>
		/// 0 - playlist ID
		/// </summary>
		private static readonly string uriPlaylistYouTubePattern = "http://www.youtube.com/playlist?list={0}";

		/// <summary>
		/// 0 - user ID
		/// </summary>
		private static readonly string uriProfileEntryPattern = "http://gdata.youtube.com/feeds/api/users/{0}?v=2";

		/// <summary>
		/// 0 - video ID
		/// </summary>
		private static readonly string uriCommentsFeedPattern = "https://gdata.youtube.com/feeds/api/videos/{0}/comments?v=2";

		/// <summary>
		/// 0 - user ID
		/// </summary>
		private static readonly string uriUserUploadsFeed = "https://gdata.youtube.com/feeds/api/users/{0}/uploads?v=2";

		/// <summary>
		/// 0 - user ID
		/// </summary>
		private static readonly string uriUserPlaylistsFeed = "https://gdata.youtube.com/feeds/api/users/{0}/playlists?v=2";

		/// <summary>
		/// 0 - user ID
		/// </summary>
		private static readonly string uriUserFavoritesFeed = "https://gdata.youtube.com/feeds/api/users/{0}/favorites?v=2";

		/// <summary>
		/// 0 - playlist ID
		/// </summary>
		private static readonly string uriPlaylistFeed = "https://gdata.youtube.com/feeds/api/playlists/{0}?v=2";

		// Public properties.

		/// <summary>
		/// Returns the URI for YouTube categories.
		/// </summary>
		public static Uri UriCategories { get { return YouTubeUri.uriCategories; } }

		public static string[] StandardFeedsIds { get { return YouTubeUri.standardFeedIds; } }

		/// <summary>
		/// Returns the list of standard feed names.
		/// </summary>
		public static string[] StandardFeedNames { get { return YouTubeUri.standardFeedNames; } }

		/// <summary>
		/// Returns the list of time names.
		/// </summary>
		public static string[] TimeNames { get { return YouTubeUri.timeNames; } }

		/// <summary>
		/// Returns the list of regions IDs.
		/// </summary>
		public static string[] RegionIds { get { return YouTubeUri.regionIds; } }

		/// <summary>
		/// Returns the list of region names.
		/// </summary>
		public static string[] RegionNames { get { return YouTubeUri.regionNames; } }

		/// <summary>
		/// Creates a video entry URI.
		/// </summary>
		/// <param name="id">The video ID.</param>
		/// <returns>A URI for the selected video.</returns>
		public static Uri GetVideoEntry(
			string id
			)
		{
			return new Uri(YouTubeUri.uriVideoEntryPattern.FormatWith(id));
		}

		/// <summary>
		/// Creates a standard feed URI.
		/// </summary>
		/// <param name="feedId">The feed ID.</param>
		/// <param name="regionId">The region ID, it can be null.</param>
		/// <param name="category">The category, it can be null.</param>
		/// <param name="timeId">The time ID, it can be null.</param>
		/// <param name="startIndex">The start index, it can be null.</param>
		/// <param name="maxResults">The max results, it can be null.</param>
		/// <returns>A URI for the selected standard feed.</returns>
		public static Uri GetStandardFeed(
			YouTubeStandardFeed feedId,
			string regionId,
			string category,
			YouTubeTimeId? timeId,
			int? startIndex,
			int? maxResults
			)
		{
			// Feed ID
			string uriFeedId = YouTubeUri.standardFeedIds[(int)feedId];

			// Region ID
			string uriRegionId = null == regionId ? string.Empty : regionId + "/";

			// Category
			string uriCategory = null == category ? string.Empty : "_" + category;

			StringBuilder builder = new StringBuilder(uriStandardVideoFeedPattern.FormatWith(uriRegionId, uriFeedId, uriCategory));

			if (timeId != null)
			{
				builder.Append("&time=" + YouTubeUri.timeIds[(int)timeId]);
			}
			if (startIndex != null)
			{
				builder.Append("&start-index=" + startIndex);
			}
			if (maxResults != null)
			{
				builder.Append("&max-results=" + maxResults);
			}

			return new Uri(builder.ToString());
		}

		/// <summary>
		/// Creates a videos feed URI.
		/// </summary>
		/// <param name="video">The feed query string.</param>
		/// <param name="startIndex">The start index.</param>
		/// <param name="maxResults">The maximum results.</param>
		/// <returns>A URI for the selected videos feed.</returns>
		public static Uri GetVideosFeed(
			string query,
			int? startIndex,
			int? maxResults
			)
		{
			StringBuilder builder = new StringBuilder(uriVideosFeedPattern.FormatWith(query));

			if (startIndex != null)
			{
				builder.Append("&start-index=" + startIndex);
			}
			if (maxResults != null)
			{
				builder.Append("&max-results=" + maxResults);
			}

			return new Uri(builder.ToString());
		}

		/// <summary>
		/// Creates a related videos feed URI.
		/// </summary>
		/// <param name="video">The video ID.</param>
		/// <param name="startIndex">The start index.</param>
		/// <param name="maxResults">The maximum results.</param>
		/// <returns>A URI for the selected videos feed.</returns>
		public static Uri GetRelatedVideosFeed(
			string video,
			int? startIndex,
			int? maxResults
			)
		{
			StringBuilder builder = new StringBuilder(uriRelatedVideosFeedPattern.FormatWith(video));

			if (startIndex != null)
			{
				builder.Append("&start-index=" + startIndex);
			}
			if (maxResults != null)
			{
				builder.Append("&max-results=" + maxResults);
			}

			return new Uri(builder.ToString());
		}

		/// <summary>
		/// Creates a response videos feed URI.
		/// </summary>
		/// <param name="video">The video ID.</param>
		/// <param name="startIndex">The start index.</param>
		/// <param name="maxResults">The maximum results.</param>
		/// <returns>A URI for the selected videos feed.</returns>
		public static Uri GetResponseVideosFeed(
			string video,
			int? startIndex,
			int? maxResults
			)
		{
			StringBuilder builder = new StringBuilder(uriResponseVideosFeedPattern.FormatWith(video));

			if (startIndex != null)
			{
				builder.Append("&start-index=" + startIndex);
			}
			if (maxResults != null)
			{
				builder.Append("&max-results=" + maxResults);
			}

			return new Uri(builder.ToString());
		}

		/// <summary>
		/// Creates the URI for the video comments feed.
		/// </summary>
		/// <param name="video">The video ID.</param>
		/// <param name="startIndex">The start index.</param>
		/// <param name="maxResults">The maximum results.</param>
		/// <returns>A URI for the comments feed.</returns>
		public static Uri GetCommentsFeed(
			string video,
			int? startIndex,
			int? maxResults
			)
		{
			StringBuilder builder = new StringBuilder(YouTubeUri.uriCommentsFeedPattern.FormatWith(video));

			if (startIndex != null)
			{
				builder.Append("&start-index=" + startIndex);
			}
			if (maxResults != null)
			{
				builder.Append("&max-results=" + maxResults);
			}

			return new Uri(builder.ToString());
		}

		/// <summary>
		/// Creates the URI for the user uploads feed.
		/// </summary>
		/// <param name="user">The user ID.</param>
		/// <param name="startIndex">The start index.</param>
		/// <param name="maxResults">The maximum results.</param>
		/// <returns>A URI for the uploads feed.</returns>
		public static Uri GetUploadsFeed(
			string user,
			int? startIndex,
			int? maxResults
			)
		{
			StringBuilder builder = new StringBuilder(YouTubeUri.uriUserUploadsFeed.FormatWith(user));

			if (startIndex != null)
			{
				builder.Append("&start-index=" + startIndex);
			}
			if (maxResults != null)
			{
				builder.Append("&max-results=" + maxResults);
			}

			return new Uri(builder.ToString());
		}

		/// <summary>
		/// Creates the URI for the user playlists feed.
		/// </summary>
		/// <param name="user">The user ID.</param>
		/// <param name="startIndex">The start index.</param>
		/// <param name="maxResults">The maximum results.</param>
		/// <returns>A URI for the playlists feed.</returns>
		public static Uri GetPlaylistsFeed(
			string user,
			int? startIndex,
			int? maxResults
			)
		{
			StringBuilder builder = new StringBuilder(YouTubeUri.uriUserPlaylistsFeed.FormatWith(user));

			if (startIndex != null)
			{
				builder.Append("&start-index=" + startIndex);
			}
			if (maxResults != null)
			{
				builder.Append("&max-results=" + maxResults);
			}

			return new Uri(builder.ToString());
		}

		/// <summary>
		/// Creates the URI for the user favorite videos feed.
		/// </summary>
		/// <param name="playlist">The user ID.</param>
		/// <param name="startIndex">The start index.</param>
		/// <param name="maxResults">The maximum results.</param>
		/// <returns>A URI for the favorites feed.</returns>
		public static Uri GetFavoritesFeed(
			string user,
			int? startIndex,
			int? maxResults
			)
		{
			StringBuilder builder = new StringBuilder(YouTubeUri.uriUserFavoritesFeed.FormatWith(user));

			if (startIndex != null)
			{
				builder.Append("&start-index=" + startIndex);
			}
			if (maxResults != null)
			{
				builder.Append("&max-results=" + maxResults);
			}

			return new Uri(builder.ToString());
		}

		/// <summary>
		/// Creates the URI for the playlist videos feed.
		/// </summary>
		/// <param name="playlist">The playlist ID.</param>
		/// <param name="startIndex">The start index.</param>
		/// <param name="maxResults">The maximum results.</param>
		/// <returns>A URI for the playlist feed.</returns>
		public static Uri GetPlaylistFeed(
			string playlist,
			int? startIndex,
			int? maxResults
			)
		{
			StringBuilder builder = new StringBuilder(YouTubeUri.uriPlaylistFeed.FormatWith(playlist));

			if (startIndex != null)
			{
				builder.Append("&start-index=" + startIndex);
			}
			if (maxResults != null)
			{
				builder.Append("&max-results=" + maxResults);
			}

			return new Uri(builder.ToString());
		}

		/// <summary>
		/// Gets the set of valid time arguments for a standard feed.
		/// </summary>
		/// <param name="feedId">The standard feed ID.</param>
		/// <returns>An array of valid times. The array is always different from null, but it may be empty.</returns>
		public static YouTubeTimeId[] GetValidTime(YouTubeStandardFeed feedId)
		{
			return YouTubeUri.timeFeedValidity[(int)feedId];
		}

		/// <summary>
		/// Returns the region name for a specified region ID.
		/// </summary>
		/// <param name="id">The region ID.</param>
		/// <returns>The region name.</returns>
		public static string GetRegionName(string id)
		{
			return YouTubeUri.regionNames[Array.IndexOf(YouTubeUri.regionIds, id)];
		}

		/// <summary>
		/// Returns the region ID for a specified region name.
		/// </summary>
		/// <param name="name">The region name.</param>
		/// <returns>The region ID.</returns>
		public static string GetRegionId(string name)
		{
			return YouTubeUri.regionIds[Array.IndexOf(YouTubeUri.regionNames, name)];
		}

		/// <summary>
		/// Returns the YouTube link for a video ID.
		/// </summary>
		/// <param name="id">The video ID.</param>
		/// <returns>The YouTube link.</returns>
		public static string GetYouTubeVideoLink(string id)
		{
			return YouTubeUri.uriVideoYouTubePattern.FormatWith(id);
		}

		/// <summary>
		/// Returns the YouTube link for a user name.
		/// </summary>
		/// <param name="user">The user name (not ID).</param>
		/// <returns>The YouTube link.</returns>
		public static string GetYouTubeUserLink(string user)
		{
			return YouTubeUri.uriUserYouTubePattern.FormatWith(user);
		}

		/// <summary>
		/// Returns the YouTube link for a playlist.
		/// </summary>
		/// <param name="playlist">The playlist ID.</param>
		/// <returns>The YouTube link.</returns>
		public static string GetYouTubePlaylistLink(string playlist)
		{
			return YouTubeUri.uriPlaylistYouTubePattern.FormatWith(playlist);
		}

		/// <summary>
		/// Creates a profile entry URI.
		/// </summary>
		/// <param name="id">The profile ID.</param>
		/// <returns>A URI for the selected profile.</returns>
		public static Uri GetProfileEntry(
			string id
			)
		{
			return new Uri(YouTubeUri.uriProfileEntryPattern.FormatWith(id));
		}
	}
}
