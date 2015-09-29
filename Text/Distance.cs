using System;
using System.Collections.Generic;
using System.Linq;

namespace Text
{
	public static class Distance
	{
		/// <summary>
		/// Levenshteins distance between two words.
		/// </summary>
		/// <returns>The distance.</returns>
		/// <param name="sourceString">Source string.</param>
		/// <param name="targetString">Target string.</param>
		/// <typeparam name="T">Char type.</typeparam>
		/// <remarks>
		/// Algorithm source.
		/// https://en.wikibooks.org/wiki/Algorithm_Implementation/Strings/Levenshtein_distance#C.23
		/// </remarks>
		public static int Levenshtein<T>(IEnumerable<T> sourceString, IEnumerable<T> targetString)
			where T:IEquatable<T>
		{
			if (sourceString == null || targetString == null) 
				throw new ArgumentNullException();
			var source = sourceString.ToArray ();
			var target = targetString.ToArray ();
			if(source.Length == 0){
				if(target.Length == 0) return 0;
				return target.Length;
			}
			if(target.Length == 0) return source.Length;

			if(source.Length > target.Length){
				var temp = target;
				target = source;
				source = temp;
			}

			var m = target.Length;
			var n = source.Length;
			var distance = new int[2, m + 1];
			// Initialize the distance 'matrix'
			for(var j = 1; j <= m; j++) distance[0, j] = j;

			var currentRow = 0;
			for(var i = 1; i <= n; ++i){
				currentRow = i & 1;
				distance[currentRow, 0] = i;
				var previousRow = currentRow ^ 1;
				for(var j = 1; j <= m; j++){
					var cost = (target[j - 1].Equals(source[i - 1]) ? 0 : 1);
					distance[currentRow, j] = Math.Min(Math.Min(
						distance[previousRow, j] + 1,
						distance[currentRow, j - 1] + 1),
						distance[previousRow, j - 1] + cost);
				}
			}
			return distance[currentRow, m];
		}
	}
}

