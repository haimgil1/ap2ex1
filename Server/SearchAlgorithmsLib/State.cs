using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
	/*********************************
	 * State:
	 * A generic state class
	*********************************/
	public class State<T> : IComparable
	{
		private T state; // the state represented by a string
		//private float cost; // cost to reach this state (set by a setter)
		//private State<T> cameFrom; // the state we came from to this state (setter)

		private State(T state) // CTOR
		{
			this.state = state;
			//this.cost = 0;
			//this.cameFrom = null;
		}

		public bool Equals(State<T> s)
		{
			return state.Equals(s.state);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as State<T>);
		}


		public T GetState()
		{
			return state;
		}

		public float Cost
		{
			get;
			set;
		}

		public State<T> CameFrom
		{
			get;
			set;
		}

		public int CompareTo(object obj)
		{

			if (obj is State<T>)
			{
				return this.Cost.CompareTo((obj as State<T>).Cost);
			}
			throw new ArgumentException("Object is not a State");

		}

		public override int GetHashCode()
		{
			return state.ToString().GetHashCode();
			//return String.Intern(state.ToString()).GetHashCode();
			//todo
			//return state.GetHashCode();

		}

		public override string ToString()
		{
			return state.ToString();
		}

		public static class StatePool
		{
			private static Dictionary<T, State<T>> pool = new Dictionary<T, State<T>>();
			public static State<T> GetState(T item)
			{
				if (!pool.ContainsKey(item))
				{
					pool.Add(item, new State<T>(item));
				}
				return pool[item];
			}

		}

	}
}