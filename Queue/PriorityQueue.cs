﻿using System;
using System.Collections.Generic;
namespace Queue
{
	/// <summary>
	/// Generik queue interface.
	/// </summary>
	public interface IQueue<TV>
	{
		/// <summary>
		/// Enqueue element int the queue.
		/// </summary>
		/// <param name="value">Value.</param>
		void Enqueue(TV value);

		/// <summary>
		/// Value indicating whether queue is empty.
		/// </summary>
		/// <value><c>true</c> if this queue is empty; otherwise, <c>false</c>.</value>
		bool IsEmpty {get;}

		/// <summary>
		/// Dequeue first element from queue. (this element will be removed from queue).
		/// </summary>
		TV Dequeue();

		/// <summary>
		/// Pick first element from queue (this element will still be in queue).
		/// </summary>
		TV Pick();

		/// <summary>
		/// Returns number of elements within this queue.
		/// </summary>
		/// <value>The count of elements.</value>
		int Count { get; }
	}

	/// <summary>
	/// Priority queue, values will be returned accordingly to key order.
	/// </summary>
	public interface IPriorityQueue<TK, TV> : IQueue<KeyValuePair<TK, TV>>
	{
		void Enqueue(TK key, TV value);
		new TV Dequeue();
		new TV Pick();
	}


	/// <summary>
	/// Qriority queue.
	/// </summary>
	/// <test>
	/// var pq = new Queue.PriorityQueue<int, int> ();
	/// pq.Enqueue (1, 1);
	/// Console.WriteLine (pq.Dequeue ()); // 1
	/// Console.WriteLine();
	///
	/// pq.Enqueue (1, 1);
	/// pq.Enqueue (2, 2);
	/// Console.WriteLine (pq.Dequeue ()); // 2
	/// Console.WriteLine (pq.Dequeue ()); // 1
	/// Console.WriteLine();

	/// pq.Enqueue (2, 2);
	/// pq.Enqueue (1, 1);
	/// Console.WriteLine (pq.Dequeue ()); // 2
	/// pq.Enqueue (2, 2);
	/// Console.WriteLine (pq.Dequeue ()); // 2
	/// Console.WriteLine();
	///
	/// pq.Enqueue (2, 2);
	/// pq.Enqueue (1, 1);
	/// pq.Enqueue (2, 2);
	/// pq.Enqueue (3, 3);
	/// Console.WriteLine (pq.Dequeue ()); // 3
	/// Console.WriteLine (pq.Dequeue ()); // 2
	/// Console.WriteLine (pq.Dequeue ()); // 2
	/// Console.WriteLine (pq.Dequeue ()); // 1
	/// </test>
	public class PriorityQueue<TK, TV> : IPriorityQueue<TK, TV> where TK : IComparable<TK>
	{
		/// <summary>
		/// Priotity heap.
		/// </summary>
		private readonly List<KeyValuePair<TK, TV>> _pq = new List<KeyValuePair<TK, TV>>(1);

		public PriorityQueue()
		{
			_pq.Add (new KeyValuePair<TK, TV>()); // Element @ position 0 is not used.
		}

		public void Enqueue(KeyValuePair<TK, TV> value)
		{
			_pq.Add (value);
			swim (_pq.Count - 1);
		}

		public bool IsEmpty => _pq.Count <= 1;

		KeyValuePair<TK, TV> IQueue<KeyValuePair<TK, TV>>.Dequeue()
		{
			return dequeue();
		}
		KeyValuePair<TK, TV> IQueue<KeyValuePair<TK, TV>>.Pick()
		{
			return _pq [1];
		}

		public int Count => _pq.Count - 1;

		public void Enqueue(TK key, TV value)
		{
			Enqueue(new KeyValuePair<TK, TV>(key, value));
		}
		public TV Dequeue()
		{
			return dequeue().Value;
		}
		public TV Pick()
		{
			return _pq [1].Value;
		}

		private KeyValuePair<TK,TV> dequeue()
		{
			check();
			var max = _pq [1];
			swap(1,Count);
			_pq.RemoveAt (Count);
			sink (1);
			return max;
		}

		private void check()
		{
			if (IsEmpty)
				throw new InvalidOperationException("Queue is empty");
		}

		private bool less(int i, int j)
		{
			return _pq[i].Key.CompareTo (_pq [j].Key) < 0;
		}

		private void swap(int i, int j)
		{
			var t = _pq[i];
			_pq[i] = _pq[j];
			_pq[j] = t;
		}

		private void swim(int k)
		{
			while (k > 1 && less (k / 2, k)){
				swap (k / 2, k);
				k = k / 2;
			}
		}

		private void sink(int k)
		{
			while (2 * k <= Count)
			{
				int j = 2 * k;
				if (j < Count && less (j, j + 1))
					j++;
				if (!less (k, j))
					break;
				swap (k, j);
				k = j;
			}
		}
	}
}

