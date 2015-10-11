using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UnionFind
{
	/// <summary>
	/// Connected components base interface.
	/// </summary>
	public interface IUnionFind
	{
		void Union(int i, int j);
		bool IsConnected(int i, int j);
		int Length { get; }
	}

	public class UF : IUnionFind
	{
		private readonly List<HashSet<int>> _components = new List<HashSet<int>>();

		public UF(int n)
		{
			Length = n;
			for (int i = 0; i < n; i++)
			{
				_components.Add(new HashSet<int> { i });
			}
		}

		public int Count
		{
			get { return _components.Count; }
		}

		public void Union(int i, int j)
		{
			var ci = getComponent(i);
			var cj = getComponent(j);
			if (ReferenceEquals(ci, cj))
			{
				return;
			}

			foreach (var i1 in cj)
			{
				ci.Add(i1);
			}

			_components.Remove(cj);
		}

		public bool IsConnected(int i, int j)
		{
			var ci = getComponent(i);
			return ci.Contains(j);
		}

		public int Length { get; private set; }

		private HashSet<int> getComponent(int i)
		{
			foreach (var component in _components)
			{
				if (component.Contains(i))
				{
					return component;
				}
			}
			throw new ArgumentException();
		}
	}

	public class UFQuickFind : IUnionFind
	{
		private readonly int[] _array;

		public UFQuickFind(int n)
		{
			_array = new int[n];
			for (int i = 0; i < n; i++)
			{
				_array[i] = i;
			}
		}

		public int Count
		{
			get { return _array.Distinct().Count(); }
		}

		public void Union(int i, int j)
		{
			var indexI = _array[i];
			var indexJ = _array[j];
			if (indexI == indexJ)
			{
				return;
			}
			for (int k = 0; k < _array.Length; k++)
			{
				if(_array[k] == indexJ)
				{
					_array[k] = indexI;
				}
			}
		}

		public bool IsConnected(int i, int j)
		{
			return _array[i] == _array[j];
		}

		public int Length { get { return _array.Length; } }
	}

	public class UFQuickUnion : IUnionFind
	{
		private readonly int[] _id;
		private readonly int[] _size;

		public UFQuickUnion(int n)
		{
			_id = new int[n];
			_size = new int[n];
			for (int i = 0; i < n; i++)
			{
				_id[i] = i;
				_size[i] = 1;
			}
		}

		public int Count
		{
			get { return _id.Select(root).Distinct().Count(); }
		}

		public void Union(int i, int j)
		{
			var ri = root(i);
			var rj = root(j);

			if (_size[ri] < _size[rj])
			{
				_id[ri] = rj;
				_size[rj] += _size[ri];
			}
			else
			{
				_id[rj] = ri;
				_size[ri] += _size[rj];
			}
		}

		private int root(int i)
		{
			var t = _id[i];
			while (i != t)
			{
				t = _id[t];
				_id[i] = t;
				i = t;
			}
			return i;
		}

		public bool IsConnected(int i, int j)
		{
			return root(i) == root(j);
		}

		public int Length { get { return _id.Length; } }
	}
}
