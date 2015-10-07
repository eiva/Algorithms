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
        void AddEdge(TV v, TV t, TE edge);

        /// <summary>
        /// List of adjascent vertexes for given vertex.
        /// </summary>
        /// <param name="v">Vertex to look up.</param>
        /// <returns>Adjascent vertexes.</returns>
        IReadOnlyCollection<KeyValuePair<TV, TE>> Adjasent(TV v);

        /// <summary>
        /// List of vertexes.
        /// </summary>
        IReadOnlyCollection<TV> Vertexes { get; }
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
}
