using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
	public class PriorityQueue<T>
	{
		private List<T> list;
		public PriorityQueue()
		{
			this.list = new List<T>();
		}

		public void Enqueue(T item)
		{
			this.list.Add(item);
		}

		public int Count()
		{
			return this.list.Count;
		}

		public bool Contains(T item)
		{
			return list.Contains(item);
		}

		public T Dequeue()
		{
			if (this.list.Count == 0)
			{
				throw new IndexOutOfRangeException();
			}
			else
			{
				list.Sort();
				T item = list[0];
				this.list.Remove(item);
				return item;
			}
		}
	}
}
