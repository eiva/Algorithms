using System;

namespace TestClient
{
	class GenerateLevinshtain
	{
	}
	class MainClass
	{
		public static void Main (string[] args)
		{
			Trae.Trae<char> trae = new Trae.Trae<char>();
			trae.Apply ("ATC");
			trae.Apply ("AT");
			trae.Apply ("A");
			trae.Apply ("CA");
			Tree.TraverseHelpers.BreadthFirstTraverse(trae, (k,v) => Console.Write($"{k} - {v} "), ()=>Console.WriteLine());
		}
	}
}
