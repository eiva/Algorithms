using NUnit.Framework;
using System;

namespace GraphTest
{
	[TestFixture]
	public class ConnectedComponentTest
	{
		[Test]
		public void TestSimple ()
		{
			var g = Graph.Graph.Bidirectional<int,int> ();
			var cc = Graph.Graph.ConnectedComponents(g);
			Assert.AreEqual (0, cc.Count);
		}
	}
}

