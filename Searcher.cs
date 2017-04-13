using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
	public abstract class Searcher<T> : ISearcher<T>
	{

			protected int evaluatedNodes;

			public abstract Solution<T> Search(ISearchable<T> searchable);

			public int GetNumberOfNodesEvaluated()
			{
				return evaluatedNodes;
			}

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
				return new Solution<T>(s);
			}

	}
}
