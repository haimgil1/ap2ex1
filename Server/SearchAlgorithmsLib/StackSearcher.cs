using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
	public abstract class StackSearcher<T> : Searcher<T>
	{
		protected Stack<State<T>> openList;
		//protected int evaluatedNodes;

		public StackSearcher()
		{
			openList = new Stack<State<T>>();
			evaluatedNodes = 0;
			
		}

		//public int GetNumberOfNodesEvaluated()
		//{
		//	return evaluatedNodes;
		//}

		public void AddToOpenList(State<T> state)
		{
			openList.Push(state);
		}

		public int OpenListSize
		{
			get { return openList.Count; }
		}

		protected State<T> PopOpenList()
		{
			evaluatedNodes++;
			return openList.Pop();
		}

		//protected Solution<T> BackTrace(State<T> goal, State<T> initialState)
		//{
		//	Stack<State<T>> s = new Stack<State<T>>();

		//	State<T> current = goal;
		//	while (!(current.CameFrom.Equals(initialState)))
		//	{
		//		s.Push(current);
		//		current = current.CameFrom;
		//	}
		//	s.Push(current);
		//	s.Push(current.CameFrom);
		//	return new Solution<T>(s);
		//}

		//public abstract Solution<T> Search(ISearchable<T> searchable);

	}
}
