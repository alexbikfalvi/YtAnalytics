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
using System.Collections;
using System.Collections.Generic;

namespace InetCommon.Tools
{
	/// <summary>
	/// A class representing a toolbox enumerable.
	/// </summary>
	public class ToolboxEnumerable : IEnumerable<Tool>
	{
		private readonly IEnumerable<ToolsetConfig> enumerable;

		/// <summary>
		/// Creates a toolbox enumerable.
		/// </summary>
		/// <param name="enumerable">The toolset configuration enumerable.</param>
		public ToolboxEnumerable(IEnumerable<ToolsetConfig> enumerable)
		{
			this.enumerable = enumerable;
		}

		// Public methods.

		/// <summary>
		/// Gets the toolbox enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator<Tool> GetEnumerator()
		{
			return new ToolboxEnumerator(this.enumerable);
		}

		/// <summary>
		/// Gets the toolbox enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
