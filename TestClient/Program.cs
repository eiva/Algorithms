using System;

namespace TestClient
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var pq = new Queue.PriorityQueue<int, int> ();

			pq.Enqueue (1, 1);
			Console.WriteLine (pq.Dequeue ()); // 1
			Console.WriteLine(pq.IsEmpty);

			pq.Enqueue (1, 1);
			pq.Enqueue (2, 2);
			Console.WriteLine (pq.Dequeue ()); // 2
			Console.WriteLine (pq.Dequeue ()); // 1
			Console.WriteLine();

			pq.Enqueue (2, 2);
			pq.Enqueue (1, 1);
			Console.WriteLine (pq.Dequeue ()); // 2
			pq.Enqueue (2, 2);
			Console.WriteLine (pq.Dequeue ()); // 2
			Console.WriteLine();

			pq.Enqueue (2, 2);
			pq.Enqueue (1, 1);
			pq.Enqueue (2, 2);
			pq.Enqueue (3, 3);
			Console.WriteLine (pq.Dequeue ()); // 3
			Console.WriteLine (pq.Dequeue ()); // 2
			Console.WriteLine (pq.Dequeue ()); // 2
			Console.WriteLine (pq.Dequeue ()); // 1


			//Console.WriteLine (Text.Distance.DamerauLevenshtein("ACTC", "CTCC"));
		}
	}
}
