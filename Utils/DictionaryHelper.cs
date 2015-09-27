﻿using System;
using System.Collections.Generic;

namespace Utils
{
	public static class DictionaryHelper {
		
		/// <summary>
		/// Gets existing value or create new using new() from specified dictionary.
		/// </summary>
		/// <returns>The or create.</returns>
		/// <param name="dict">Dict.</param>
		/// <param name="key">Key.</param>
		/// <typeparam name="TK">The 1st type parameter.</typeparam>
		/// <typeparam name="TV">The 2nd type parameter.</typeparam>
		public static TV GetOrCreate<TK, TV>(this IDictionary<TK, TV> dict, TK key)
			where TV : class, new()
		{
			TV res;
			if (dict.TryGetValue(key, out res))
			{
				return res;
			}
			res = new TV();
			dict.Add(key, res);
			return res;
		}
	}
}

