using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
	/// <summary>
	/// Priority queue.
	/// </summary>
	public class PriorityQueue<T>
	{
		/// <summary>
		/// The queue.
		/// </summary>
		private List<T> queue;
		/// <summary>
		/// The comparer.
		/// </summary>
		private Comparer<T> comparer;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:SearchAlgorithmsLib.PriorityQueue`1"/> class.
		/// </summary>
		public PriorityQueue(Comparer<T> comparer)
		{
			this.queue = new List<T>();
		        this.comparer = comparer;
		}

		/// <summary>
		/// Enqueue the specified item.
		/// </summary>
		/// <returns>The enqueue.</returns>
		/// <param name="item">Item.</param>
		public void Enqueue(T item)
		{
			this.queue.Add(item);
		}

		/// <summary>
		/// Count this instance.
		/// </summary>
		/// <returns>The count.</returns>
		public int Count()
		{
			return this.queue.Count;
		} 

		/// <summary>
		/// Ises the empty.
		/// </summary>
		/// <returns><c>true</c>, if empty was ised, <c>false</c> otherwise.</returns>
		public bool IsEmpty()
		{
			return this.queue.Count == 0;
		}

		/// <summary>
		/// Contains the specified item.
		/// </summary>
		/// <returns>The contains.</returns>
		/// <param name="item">Item.</param>
		public bool Contains(T item)
		{
			return queue.Contains(item);
		}

		/// <summary>
		/// Dequeue this instance.
		/// </summary>
		/// <returns>The dequeue.</returns>
		public T Dequeue()
		{
			if (IsEmpty())
			{
				throw new IndexOutOfRangeException();
			}
				queue.Sort(this.comparer);
				T item = queue[0];
				this.queue.Remove(item);
				return item;
		}
	}
}
