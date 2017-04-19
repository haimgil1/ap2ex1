using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
	public class Bfs<T> : QueueSearcher<T>
	{
		public override Solution<T> Search(ISearchable<T> searchable)
		{
			AddToOpenList(searchable.GetInitialState()); // inherited from Searcher
			HashSet<State<T>> closed = new HashSet<State<T>>();
			while (OpenListSize > 0)
			{
				State<T> n = PopOpenList(); // inherited from Searcher, removes the best state
				closed.Add(n);
				if (n.Equals(searchable.GetIGoallState()))
				{
					return BackTrace(n, searchable.GetInitialState()); // private method, back traces through the parents
																	   // calling the delegated method, returns a list of states with n as a parent	
				}

				List<State<T>> succerssors = searchable.GetAllPossibleStates(n);
				foreach (State<T> s in succerssors)
				{
					if (!closed.Contains(s) && !openList.Contains(s))
					{
						AddToOpenList(s);
						s.Cost = n.Cost + 1;
						s.CameFrom = n;
					}
					else
					{
						updatePath(n, s);
					}
				}
			}
			return null;
		}

		private void updatePath(State<T> n, State<T> s)
		{
			float distance = s.GetState().GetHashCode() - n.GetState().GetHashCode();
			float newPath = n.Cost + distance;
			float oldPath = s.Cost;
			if (newPath < oldPath)
			{
				if (!openList.Contains(s))
				{
					openList.Enqueue(s);
				}
				else
				{
					s.Cost = newPath;
					s.CameFrom = n;
				}
			}
		}
	}
}