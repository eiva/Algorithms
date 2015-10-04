using System;
using System.Collections.Generic;
namespace Queue
{
	public interface IQueue<TV>
	{
		void Enqueue(TV value);
		bool IsEmpty {get;}
		TV Dequeue();
	}

	/// <summary>
	/// Priority queue, Value with smallest key will be returned first.
	/// </summary>
	public interface IPriorityQueue<TK, TV> : IQueue<KeyValuePair<TK, TV>>
		where TK:IComparable<TK>
	{
		void Enqueue(TK key, TV value);
		TV Dequeue();
		bool IsEmpty {get;}
	}
}

