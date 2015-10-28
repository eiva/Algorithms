using System;
using System.Collections.Generic;
//using EIva.Algorithms.;
using System.Linq;

namespace TestClient
{
	
	public sealed class MultiDictionary<TK, TV>
		where TV : IEquatable<TV>
	{
		IDictionary<TK, List<KeyValuePair<TK,TV>>> _map = new Dictionary<TK, List<KeyValuePair<TK,TV>>> ();
		public void Add(TK key, TV value)
		{
			var chain = _map.GetOrCreate(key, ()=>{return new List<KeyValuePair<TK,TV>>();});
			chain.Add (new KeyValuePair<TK, TV> (key, value));
		}

		public IEnumerable<TV> Get(TK key)
		{
			return _map [key].Select (p => p.Value);
		}

		/// <summary>
		/// Removes all entrances of specifyed value at specifyed key.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		public void RemoveAll(TK key, TV value)
		{
			var chain = _map.GetOrCreate(key, ()=>{return new List<KeyValuePair<TK,TV>>();});
			chain.RemoveAll ( p => {return Equals(value, p.Value);});
			if (chain.Count == 0) {
				_map.Remove (key);
			}
		}
		/// <summary>
		/// Removes all entrances at specifyed key.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		public void RemoveAll(TK key)
		{
			_map.Remove (key);
		}

		public void Clear()
		{
			_map.Clear ();
		}

	}
	class MainClass
	{
		public static void Main (string[] args)
		{
			IReadOnlyDictionary<int, int> f;
			var d = new MultiDictionary<int, string> ();
			d.Add (1, "1");
			d.Add (1, "2");
			Console.WriteLine (String.Join (", ", d.Get (1)));
		}
	}
}
