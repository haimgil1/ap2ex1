using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Priority_Queue;

namespace SearchAlgorithmsLib
{
	/// <summary>
	/// Queue searcher.
	/// </summary>
	public abstract class QueueSearcher<T> : Searcher<T>
	{
		/// <summary>
		/// The open list.
		/// </summary>
		protected PriorityQueue<State<T>> openList;
		/// <summary>
		/// The comparer.
		/// </summary>
		protected Comparer<State<T>> comparer;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:SearchAlgorithmsLib.QueueSearcher`1"/> class.
		/// </summary>
		public QueueSearcher()
		{
			this.comparer = new DeafaultComparer<T>();
			openList = new PriorityQueue<State<T>>(this.comparer);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:SearchAlgorithmsLib.QueueSearcher`1"/> class.
		/// </summary>
		public QueueSearcher(Comparer<State<T>> comparer)
		{
			this.comparer = comparer;
			openList = new PriorityQueue<State<T>>(this.comparer);
		}

		/// <summary>
		/// Pops the open list.
		/// </summary>
		/// <returns>The open list.</returns>
		protected State<T> PopOpenList()
		{
			evaluatedNodes++;
			return openList.Dequeue();
		}

		/// <summary>
		/// Adds to open list.
		/// </summary>
		/// <param name="state">State.</param>
		public void AddToOpenList(State<T> state)
		{

			openList.Enqueue(state);
		}

		/// <summary>
		/// Gets the size of the open list.
		/// </summary>
		/// <value>The size of the open list.</value>
		public int OpenListSize
		{ 
			get { return openList.Count(); }
		}

	}

	/// <summary>
	/// Deafault comparer.
	/// </summary>
	public class DeafaultComparer<T> : Comparer<State<T>>
	{
		/// <summary>
		/// Compare the specified x and y.
		/// </summary>
		/// <returns>The compare.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public override int Compare(State<T> x, State<T> y)
		{
			return x.Cost.CompareTo(y.Cost);
		}
	}
}

