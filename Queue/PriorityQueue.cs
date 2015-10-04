using System;

namespace Queue
{
	public interface IPriorityQueue<TK, TV> 
		where TK:IComparable<TK>
	{
		void Enqueue(TK key, TV value);
		TV Dequeue();
		bool IsEmpty {get;}
	}
}

