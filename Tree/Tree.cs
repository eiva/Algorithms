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
		ITree<TKey, TValue> this[TKey key] {get; set;}

		/// <summary>
		/// Gets list of existed keys in tree.
		/// </summary>
		/// <remarks>
		/// Call of <see cref="this[]"/> with value from this collection should be successfull.
		/// </remarks>
		/// <value>The keys.</value>
		IEnumerable<TKey> Keys { get; }

		/// <summary>
		/// Value stored by current tree node.
		/// </summary>
		/// <value>The value.</value>
		TValue Value { get; set; }
	}

	public static class TraverseHelpers
	{
		public static void Traverse<TK, TV>(this ITree<TK, TV> tree, Action<TK, TV> action)  where TK:IEquatable<TK>, IComparable<TK>
		{
			var q = new Queue<KeyValuePair<TK, ITree<TK, TV>>> ();
			q.Enqueue (new KeyValuePair<TK, ITree<TK, TV>>(default(TK), tree));
			while (q.Count > 0) {
				var e = q.Dequeue ();
				action (e.Key, e.Value.Value);
				foreach (var k in e.Value.Keys) {
					q.Enqueue(new KeyValuePair<TK, ITree<TK, TV>>(k, e.Value[k]));
				}
			}
		}
	}

	/*public class BinaryTree<TValue> : ITree<bool, TValue>
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
	}*/

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

