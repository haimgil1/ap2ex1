using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
	public abstract class StackSearcher<T> : Searcher<T>
	{
		protected Stack<State<T>> openList;
		/// <summary>
		/// Initializes a new instance of the <see cref="T:SearchAlgorithmsLib.StackSearcher`1"/> class.
		/// </summary>
		public StackSearcher()
		{
			openList = new Stack<State<T>>();
		}

		/// <summary>
		/// Adds to open list.
		/// </summary>
		/// <param name="state">State.</param>
		public void AddToOpenList(State<T> state)
		{
			openList.Push(state);
		}

		/// <summary>
		/// Gets the size of the open list.
		/// </summary>
		/// <value>The size of the open list.</value>
		public int OpenListSize
		{
			get { return openList.Count; }
		}

		/// <summary>
		/// Pops the open list.
		/// </summary>
		/// <returns>The open list.</returns>
		protected State<T> PopOpenList()
		{
			evaluatedNodes++;
			return openList.Pop();
		}

	}
}
