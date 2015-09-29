using System;
using System.Collections.Generic;
using System.Linq;

namespace Text
{
	/// <summary>
	/// Provides functionality to calculate different distances for a specified strings.
	/// </summary>
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


		/// <summary>
		/// Damerau-Levenshtein distance is computed in asymptotic time O((max + 1) * min(first.length(), second.length()))
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static int DamerauLevenshtein<T>(IEnumerable<T> source, IEnumerable<T> target)
			where T : IEquatable<T>
		{
			if (source == null || target == null)
				throw new ArgumentNullException ();

			var first = source.ToArray ();
			var second = target.ToArray ();
			int firstLength = first.Length;
			int secondLength = second.Length;

			if (firstLength == 0)
				return secondLength;

			if (secondLength == 0) return firstLength;

			if (firstLength > secondLength)
			{
				var tmp = first;
				first = second;
				second = tmp;
				firstLength = secondLength;
				secondLength = second.Length;
			}

			int max = secondLength;
			if (secondLength - firstLength > max) return max + 1;


			var currentRow = new int[firstLength + 1];
			var previousRow = new int[firstLength + 1];
			var transpositionRow = new int[firstLength + 1];
		

			for (int i = 0; i <= firstLength; i++)
				previousRow[i] = i;

			var lastSecondCh = default(T);
			for (int i = 1; i <= secondLength; i++)
			{
				var secondCh = second[i - 1];
				currentRow[0] = i;

				// Compute only diagonal stripe of width 2 * (max + 1)
				int from = Math.Max(i - max - 1, 1);
				int to = Math.Min(i + max + 1, firstLength);

				var lastFirstCh = default(T);
				for (int j = from; j <= to; j++)
				{
					var firstCh = first[j - 1];

					// Compute minimal cost of state change to current state from previous states of deletion, insertion and swapping 
					int cost = firstCh.Equals(secondCh) ? 0 : 1;
					int value = Math.Min(Math.Min(currentRow[j - 1] + 1, previousRow[j] + 1), previousRow[j - 1] + cost);

					// If there was transposition, take in account its cost 
					if (firstCh.Equals(lastSecondCh) && secondCh.Equals(lastFirstCh))
						value = Math.Min(value, transpositionRow[j - 2] + cost);

					currentRow[j] = value;
					lastFirstCh = firstCh;
				}
				lastSecondCh = secondCh;

				int[] tempRow = transpositionRow;
				transpositionRow = previousRow;
				previousRow = currentRow;
				currentRow = tempRow;
			}

			return previousRow[firstLength];
		}
	}
}

