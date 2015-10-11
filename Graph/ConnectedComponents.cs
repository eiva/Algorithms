using System;
using System.Collections.Generic;

namespace Graph
{
	public static partial class Graph
	{
		public static IConnectedComponents<TV> ConnectedComponents<TV,TE>(this IGraph<TV, TE> graph)
			where TV : IEquatable<TV>
		{
			return new ConnectedComponentsImpl<TV, TE>(graph);
		}

		/// <summary>
		/// Simplest implementation of building connected components using DFS and Hash.
		/// </summary>
		/// <remarks>
		/// Build takes O(V+E).
		/// IsConnected takes O(1).
		/// </remarks>
		private sealed class ConnectedComponentsImpl<TV, TE> : IConnectedComponents<TV>
			where TV : IEquatable<TV>
		{
			private readonly int _count;
			private readonly IDictionary<TV, int> _components;

			public ConnectedComponentsImpl(IGraph<TV, TE> graph)
			{
				_count = 0;
				foreach (var vertex in graph.Vertexes)
				{
					if (!_components.ContainsKey(vertex))
					{
						dfs(vertex, graph);
						++_count;
					}
				}
			}

			private void dfs(TV vertex, IGraph<TV, TE> graph)
			{
				_components [vertex] = _count;
				foreach (var adj in graph.Adjasent(vertex))
				{
					if (!_components.ContainsKey (adj.Key))
					{
						dfs (adj.Key, graph);
					}
				}
			}

			public bool IsConnected(TV v1, TV v2)
			{
				var c1 = _components [v1];
				var c2 = _components [v2];
				return c1 == c2;
			}

			public int Count => _count;
		}
	}
}