using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Priority_Queue;

namespace SearchAlgorithmsLib
{
	public abstract class QueueSearcher<T> : Searcher<T>
	{
		protected PriorityQueue<State<T>> openList;
		//protected int evaluatedNodes;

		public QueueSearcher()
		{
			openList = new PriorityQueue<State<T>>();
			evaluatedNodes = 0;
		}

		protected State<T> PopOpenList()
		{
			evaluatedNodes++;
			return openList.Dequeue();
		}

		public void AddToOpenList(State<T> state)
		{

			openList.Enqueue(state);
		}

		// a property of openList
		public int OpenListSize
		{ // it is a read-only property :)
			get { return openList.Count(); }
		}

		//public int GetNumberOfNodesEvaluated()
		//{
		//	return evaluatedNodes;
		//}

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

