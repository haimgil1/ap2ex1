using System;
using SearchAlgorithmsLib;
using MazeLib;
using System.Collections.Generic;
using System.Text;

namespace ObjectAdapter
{
	public class MazeAdapter : ISearchable<Position>
	{
		private Maze maze;
		public MazeAdapter(Maze maze)
		{
			this.maze = maze;
		}
		public State<Position> GetInitialState()
		{
			return State<Position>.StatePool.GetState(this.maze.InitialPos);
		}
		public State<Position> GetIGoallState()
		{
			return State<Position>.StatePool.GetState(this.maze.GoalPos);
		}
		public List<State<Position>> GetAllPossibleStates(State<Position> s)
		{
			List<State<Position>> possibleStates = new List<State<Position>>();
			int i = s.GetState().Row;
			int j = s.GetState().Col;
			addNeighbor(i + 1,i + 1, j, maze.Rows, s, possibleStates);
			addNeighbor(j + 1, i,j + 1, maze.Cols, s, possibleStates);
			addNeighbor(0, i-1, j, i, s, possibleStates);
			addNeighbor(0, i, j - 1, j, s, possibleStates);

			//addNeighborDown(i,i - 1, j, s, possibleStates);
			//addNeighborDown(j,i,j - 1, s, possibleStates);
			//State<Position> state;
			//if ((i + 1 < maze.Rows) && (maze[i + 1, j] == CellType.Free))
			//{
			//	state = State<Position>.StatePool.GetState(new Position(i + 1, j));
			//	possibleStates.Add(state);
			//}

			//if ((j + 1 < maze.Cols) && (maze[i, j+1] == CellType.Free))
			//{
			//	state = State<Position>.StatePool.GetState(new Position(i , j+1));
			//	possibleStates.Add(state);
			//}

			//if ((i>0) && (maze[i - 1, j] == CellType.Free))
			//{
			//	state = State<Position>.StatePool.GetState(new Position(i - 1, j));
			//	possibleStates.Add(state);
			//}

			//if ((j>0) && (maze[i, j-1] == CellType.Free))
			//{
			//	state = State<Position>.StatePool.GetState(new Position(i, j - 1));
			//	possibleStates.Add(state);
			//}
			return possibleStates;
		}

		public void addNeighbor(int k, int i, int j, int rowCol, State<Position> s, List<State<Position>> possibleStates)
		{
			if ((k < rowCol) && (maze[i, j] == CellType.Free))
			{
				State<Position> state = State<Position>.StatePool.GetState(new Position(i, j));
				possibleStates.Add(state);
			}
			
		}

		public static string ToString(Solution<Position> solution)
		{
			StringBuilder sb = new StringBuilder();
			Stack<State<Position>> printStack = new Stack<State<Position>>(solution.Stack);
			while (printStack.Count >0)
			{
				State<Position> state = printStack.Pop();
				if (state.CameFrom != null)
				{
					appendDirection(sb,state.GetState().Row,state.CameFrom.GetState().Row,(int)MazeLib.Direction.Up );
					appendDirection(sb,state.CameFrom.GetState().Row, state.GetState().Row, (int)MazeLib.Direction.Down);
					appendDirection(sb, state.GetState().Col, state.CameFrom.GetState().Col, (int)MazeLib.Direction.Left);
					appendDirection(sb, state.CameFrom.GetState().Col,state.GetState().Col, (int)MazeLib.Direction.Right); 
				}

			}
			return sb.ToString();
		}

		public static void appendDirection(StringBuilder sb, int state, int stateCameFrom, int direction)
		{
			if (state > stateCameFrom)
			{
				sb.Append(direction);
			}
		}
	}
}