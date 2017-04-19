using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
	public abstract class Searcher<T> : ISearcher<T>
	{
		
		protected int evaluatedNodes;

		public Searcher()
		{
			this.evaluatedNodes = 0;
		}
		/// <summary>
		/// Search the specified searchable.
		/// </summary>
		/// <returns>The search.</returns>
		/// <param name="searchable">Searchable.</param>
		public abstract Solution<T> Search(ISearchable<T> searchable);

		/// <summary>
		/// Gets the number of nodes evaluated.
		/// </summary>
		/// <returns>The number of nodes evaluated.</returns>
		public int GetNumberOfNodesEvaluated()
		{
			return evaluatedNodes;
		}

		/// <summary>
		/// Backs the trace.
		/// </summary>
		/// <returns>The trace.</returns>
		/// <param name="goal">Goal.</param>
		/// <param name="initialState">Initial state.</param>
		protected Solution<T> BackTrace(State<T> goal, State<T> initialState)
		{
			Stack<State<T>> s = new Stack<State<T>>();
			State<T> current = goal;
			while (!(current.CameFrom.Equals(initialState)))
			{
				s.Push(current);
				current = current.CameFrom;
			}
			s.Push(current);
			s.Push(current.CameFrom);
			return new Solution<T>(s,GetNumberOfNodesEvaluated());
		}
	}
}
