using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
	/// <summary>
	/// Searchable.
	/// </summary>
	public interface ISearchable<T>
	{
		State<T> GetInitialState();
		State<T> GetIGoallState();
		List<State<T>> GetAllPossibleStates(State<T> s);
		void updateCost(State<T> cameFrom,State<T> current);
	}
}
