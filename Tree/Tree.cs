using System;
using System.Collections.Generic;

namespace Tree
{
	/// <summary>
	/// Interface representing abstract tree structure.
	/// </summary>
	public interface ITree<TKey, TValue> where TKey : IEquatable<TKey>, IComparable<TKey>
	{
		/// <summary>
		/// Accessing child nodes.
		/// </summary>
		/// <value>The children.</value>
		ITree<TKey, TValue> this[TKey key] {get;}

		/// <summary>
		/// Value stored by current tree node.
		/// </summary>
		/// <value>The value.</value>
		TValue Value { get; set; }
	}

	public class BinaryTree<TValue> : ITree<bool, TValue>
	{
		/// <summary>
		/// Collection of child nodes.
		/// </summary>
		/// <value>The children.</value>
		public ITree<bool, TValue> this[bool key] {
			get{ return null;}
		}

		/// <summary>
		/// Value stored by current tree node.
		/// </summary>
		/// <value>The value.</value>
		public TValue Value { get; set; }
	}

	/*class TreaTree<TValue> : ITree<char, TValue>
	{
		/// <summary>
		/// Collection of child nodes.
		/// </summary>
		/// <value>The children.</value>
		public IReadOnlyDictionary<char, ITree<char, TValue>> Children {get; set;}

		/// <summary>
		/// Value stored by current tree node.
		/// </summary>
		/// <value>The value.</value>
		public TValue Value { get; set; }
	}*/
}

