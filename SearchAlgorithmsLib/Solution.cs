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
		private int evaluatedNodes;
		/// <summary>
		/// Initializes a new instance of the <see cref="T:SearchAlgorithmsLib.Solution`1"/> class.
		/// </summary>
		/// <param name="stack">Stack.</param>
		public Solution(Stack<State<T>> stack, int evaluatedNodes)
		{
			this.stack = stack;
			this.evaluatedNodes = evaluatedNodes;
		}

		/// <summary>
		/// Gets the stack.
		/// </summary>
		/// <value>The stack.</value>
		public Stack<State<T>> Stack
		{
			get { return stack; }
		}

		/// <summary>
		/// Gets the evaluated nodes.
		/// </summary>
		/// <value>The evaluated nodes.</value>
		public int EvaluatedNodes
		{
			get { return evaluatedNodes; }
		}

		/// <summary>
		/// Size this instance.
		/// </summary>
		/// <returns>The size.</returns>
		public int Size()
		{
			return stack.Count;
		}
	}
}
