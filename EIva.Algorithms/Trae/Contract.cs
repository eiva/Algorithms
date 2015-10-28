using System;
using System.Collections.Generic;
using System.Linq;
using EIva.Algorithms.Utils;
using Tree;

namespace Trae
{
    /// <summary>
    ///     Lexical frequency counter tree.
    /// </summary>
    public class Trae<TChar> : ITree<TChar, uint> where TChar : IEquatable<TChar>, IComparable<TChar>
    {
        private readonly IDictionary<TChar, Trae<TChar>> _child = new Dictionary<TChar, Trae<TChar>>();

        public Trae<TChar> this[TChar charecter]
        {
            get { return _child[charecter]; }
            set { _child[charecter] = value; }
        }

        public IEnumerable<TChar> Keys => _child.Keys;

        ITree<TChar, uint> ITree<TChar, uint>.this[TChar charecter]
        {
            get { return this[charecter]; }
            set { this[charecter] = (Trae<TChar>) value; }
        }

        public uint Value { get; set; }

        public void Apply(IEnumerable<TChar> word)
        {
            ++Value;
            if (word.Any())
            {
                var node = _child.GetOrCreate(word.First());
                node.Apply(word.Skip(1));
            }
        }
    }
}