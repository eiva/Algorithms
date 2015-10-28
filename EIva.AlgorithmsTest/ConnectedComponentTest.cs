using NUnit.Framework;
using System;
using EIva.Algorithms.ConnectedComponents;

namespace GraphTest
{
	[TestFixture]
	public class ConnectedComponentTest
	{
		[Test]
		public void TestEmpty()
		{
			var g = Graph.Graph.Bidirectional<int,int> ();
			var cc = new ConnectedComponents<int, int>(g);
			Assert.AreEqual (0, cc.Count);
		}

		[Test]
		public void TestOne()
		{
			var g = Graph.Graph.Bidirectional<int,int> ();
			g.AddEdge (1, 2);
			g.AddEdge (1, 3);
			g.AddEdge (5, 4);
			g.AddEdge (4, 2);
			var cc = Graph.Graph.ConnectedComponents(g);
			Assert.AreEqual (1, cc.Count);
			Assert.IsTrue (cc.IsConnected (1, 5));
			Assert.IsTrue (cc.IsConnected (2, 4));
			Assert.IsTrue (cc.IsConnected (3, 5));
			Assert.IsTrue (cc.IsConnected (5, 2));
		}
		[Test]
		public void TestTwo()
		{
			var g = Graph.Graph.Bidirectional<int,int> ();
			g.AddEdge (1, 2); //1
			g.AddEdge (1, 3);
			g.AddEdge (5, 4);
			g.AddEdge (4, 6); //2
			var cc = Graph.Graph.ConnectedComponents(g);
			Assert.AreEqual (2, cc.Count);
			Assert.IsTrue (cc.IsConnected (1, 2));
			Assert.IsTrue (cc.IsConnected (2, 3));
			Assert.IsTrue (cc.IsConnected (4, 5));
			Assert.IsTrue (cc.IsConnected (5, 6));

			Assert.IsFalse (cc.IsConnected (1, 4));
			Assert.IsFalse (cc.IsConnected (2, 5));
			Assert.IsFalse (cc.IsConnected (4, 3));
			Assert.IsFalse (cc.IsConnected (1, 6));
		}
	}
}

