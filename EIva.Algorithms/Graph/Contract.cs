using System;
using System.Collections.Generic;

namespace Graph
{
    /// <summary>
    /// Graph interface.
    /// </summary>
    /// <typeparam name="TV">Vertex type.</typeparam>
    /// <typeparam name="TE">Edge metadata.</typeparam>
    public interface IGraph<TV, TE> where TV : IEquatable<TV>
    {
        /// <summary>
        /// Adds edge between 2 vertexes.
        /// </summary>
        /// <param name="v">First vertex.</param>
        /// <param name="t">Second vertex.</param>
        /// <param name="edge">Edge metadata.</param>
		void AddEdge(TV v, TV t, TE edge = default(TE));

        /// <summary>
        /// List of adjascent vertexes for given vertex.
        /// </summary>
        /// <param name="v">Vertex to look up.</param>
        /// <returns>Adjascent vertexes.</returns>
        IReadOnlyCollection<KeyValuePair<TV, TE>> Adjasent(TV v);

        /// <summary>
        /// List of vertexes.
        /// </summary>
        IEnumerable<TV> Vertexes { get; }
    }

    /// <summary>
    /// Path on a graph.
    /// </summary>
    /// <typeparam name="TV">Vertex type.</typeparam>
    /// <typeparam name="TE">Edge metadata type.</typeparam>
    public interface IPath<TV, TE>
    {
        /// <summary>
        /// Has the path to specifyed vertex.
        /// </summary>
        /// <param name="v">Vertex.</param>
        bool HasPath(TV v);

        /// <summary>
        /// List a path to secifyed vertex.
        /// </summary>
        /// <param name="v">Vertex.</param>
        /// <returns>Path.</returns>
        IEnumerable<KeyValuePair<TV, TE>> PathTo(TV v);
    }

	/// <summary>
	/// Allows to detect whether 2 vertices of a graph are connected.
	/// </summary>
	public interface IConnectedComponents<TV> where TV : IEquatable<TV>
	{
		/// <summary>
		/// Checks that two vertexes are connected.
		/// </summary>
		/// <returns><c>true</c> if vertexes v1 and v2 are connected; otherwise, <c>false</c>.</returns>
		/// <param name="v1">V1.</param>
		/// <param name="v2">V2.</param>
		bool IsConnected(TV v1, TV v2);

		/// <summary>
		/// Returns the id of component to which vertex is belong.
		/// </summary>
		/// <returns>The component id.</returns>
		/// <param name="v">Vertex.</param>
		int ComponentId(TV v);

		/// <summary>
		/// Returns the number of connected components.
		/// </summary>
		/// <value>The count.</value>
		int Count { get; }
	}
}
