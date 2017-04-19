using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
	public class Dfs<T> : StackSearcher<T>
	{
		private HashSet<State<T>> visited;
		public Dfs()
		{
			this.visited = new HashSet<State<T>>();
		}
		public override Solution<T> Search(ISearchable<T> searchable)
		{
			AddToOpenList(searchable.GetInitialState());
			while (OpenListSize > 0)
			{
				State<T> n = PopOpenList();
				if (n.Equals(searchable.GetIGoallState()))
				{
					return BackTrace(n, searchable.GetInitialState()); 
				}
				visited.Add(n);
				List<State<T>> succerssors = searchable.GetAllPossibleStates(n);
				foreach (State<T> s in succerssors)
				{
					if (!visited.Contains(s))
					{
						s.CameFrom = n;
						AddToOpenList(s);
					}
				}
			}
			return null;
		}

	}
}
