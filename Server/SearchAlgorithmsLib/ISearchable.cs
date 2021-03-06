﻿using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
	public interface ISearchable<T>
	{
		State<T> GetInitialState();
		State<T> GetIGoallState();
		List<State<T>> GetAllPossibleStates(State<T> s);
        //static string ToString(Solution<T> solution);
	}
}
