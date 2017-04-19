using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace SearchAlgorithmsLib
{
	public class Solution<T>
	{

		private Stack<State<T>> stack;

		public Solution(Stack<State<T>> stack)
		{
			this.stack = stack;
		}

		public Stack<State<T>> Stack
		{
			get { return stack; }
		}

		public int SolutionSize()
		{
			return stack.Count;
		}

	}
}
