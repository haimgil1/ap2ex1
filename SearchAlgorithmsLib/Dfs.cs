using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
	public class Dfs<T> : StackSearcher<T>
	{
		private HashSet<State<T>> visited;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:SearchAlgorithmsLib.Dfs`1"/> class.
		/// </summary>
		public Dfs(): base()
		{
			this.visited = new HashSet<State<T>>();
		}

		/// <summary>
		/// Search the specified searchable.
		/// </summary>
		/// <returns>The search.</returns>
		/// <param name="searchable">Searchable.</param>
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

				if (!visited.Contains(n))
				{
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
			}
			return null;
		}
	}
}
