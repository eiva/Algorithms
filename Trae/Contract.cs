using System;
using System.Collections.Generic;
using Utils;
using System.Linq;

namespace Trae
{
	/// <summary>
	/// Lexical frequency counter tree.
	/// </summary>
	public class Trae<TChar> : Tree.ITree<TChar, uint> where TChar : IEquatable<TChar>, IComparable<TChar>
	{
		private uint _freq;
		private readonly IDictionary<TChar, Trae<TChar>> _child = new Dictionary<TChar, Trae<TChar>>();

		public Trae<TChar> this[TChar charecter]
		{
			get { return _child[charecter]; }
			set { _child [charecter] = value;}
		}

		public IEnumerable<TChar> Keys => _child.Keys;

		Tree.ITree<TChar, uint> Tree.ITree<TChar, uint>.this [TChar charecter] {
			get{ return this[charecter]; }
			set{ this [charecter] = (Trae<TChar>)value;}
		}

		public uint Value{ 
			get { return _freq; } 
			set { _freq = value; }
		}

		public void Apply(IEnumerable<TChar> word) {
			++_freq;
			if (word.Any()) {
				var node = _child.GetOrCreate (word.First());
				node.Apply (word.Skip(1));
			}
		}
	}
}

