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

namespace InetCrawler.Tasks.Triggers
{
	/// <summary>
	/// A class representing a crawler task trigger.
	/// </summary>
	public abstract class CrawlerTriggerTask : CrawlerTrigger
	{
		private readonly CrawlerTask task;

		/// <summary>
		/// Creates a new trigger instance.
		/// </summary>
		/// <param name="tasks">The tasks handler.</param>
		/// <param name="task">The task.</param>
		public CrawlerTriggerTask(ICrawlerTasks tasks, CrawlerTask task)
			: base(tasks)
		{
			// Set the task.
			this.task = task;
		}

		// Public properties.

		/// <summary>
		/// Gets the crawler task.
		/// </summary>
		public CrawlerTask Task { get { return this.task; } }
	}
}
