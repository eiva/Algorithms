using NUnit.Framework;
using System;

namespace PriorityQueueTest
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		public void TestCase ()
		{
			var pq = new Queue.PriorityQueue<int, int> ();

			pq.Enqueue (1, 1);
			Assert.IsFalse (pq.IsEmpty);
			Assert.AreEqual(pq.Dequeue (), 1);
			Assert.IsTrue(pq.IsEmpty);

			pq.Enqueue (1, 1);
			pq.Enqueue (2, 2);
			Assert.AreEqual (pq.Dequeue (), 2); // 2
			Assert.AreEqual (pq.Dequeue (), 1); // 1
			Console.WriteLine();

			pq.Enqueue (2, 2);
			pq.Enqueue (1, 1);
			Assert.AreEqual (pq.Dequeue (), 2); // 2
			pq.Enqueue (2, 2);
			Assert.AreEqual (pq.Dequeue (), 2); // 2
			Console.WriteLine();

			pq.Enqueue (2, 2);
			pq.Enqueue (1, 1);
			pq.Enqueue (2, 2);
			pq.Enqueue (3, 3);
			Assert.AreEqual (pq.Dequeue (), 3); // 3
			Assert.AreEqual (pq.Dequeue (), 2); // 2
			Assert.AreEqual (pq.Dequeue (), 2); // 2
			Assert.AreEqual (pq.Dequeue (), 1); // 1

		}
	}
}

