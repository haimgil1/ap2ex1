using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
	/// <summary>
	/// State.
	/// </summary>
	public class State<T> : IComparable
	{
		private T state; // the state represented by a string
		private float cost; // cost to reach this state (set by a setter)
		private State<T> cameFrom; // the state we came from to this state (setter)

		/// <summary>
		/// Initializes a new instance of the <see cref="T:SearchAlgorithmsLib.State`1"/> class.
		/// </summary>
		/// <param name="state">State.</param>
		private State(T state)
		{
			this.state = state;
			this.cost = 0;
			this.cameFrom = null;
		}

		/// <summary>
		/// Gets or sets the cost.
		/// </summary>
		/// <value>The cost.</value>
		public float Cost { get; set; }

		/// <summary>
		/// Gets or sets the came from.
		/// </summary>
		/// <value>The came from.</value>
		public State<T> CameFrom { get; set;}

		/// <summary>
		/// Determines whether the specified <see cref="SearchAlgorithmsLib.State<T>"/> is equal to the current
		///  <see cref="T:SearchAlgorithmsLib.State`1"/>.
		/// </summary>
		/// <param name="s">The <see cref="SearchAlgorithmsLib.State<T>"/> to compare with the current <see cref="T:SearchAlgorithmsLib.State`1"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="SearchAlgorithmsLib.State<T>"/> is equal to the current
		/// <see cref="T:SearchAlgorithmsLib.State`1"/>; otherwise, <c>false</c>.</returns>
		public bool Equals(State<T> s)
		{
			return state.Equals(s.state);
		}

		/// <summary>
		/// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:SearchAlgorithmsLib.State`1"/>.
		/// </summary>
		/// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:SearchAlgorithmsLib.State`1"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
		/// <see cref="T:SearchAlgorithmsLib.State`1"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj)
		{
			return Equals(obj as State<T>);
		}

		/// <summary>
		/// Gets the state.
		/// </summary>
		/// <returns>The state.</returns>
		public T GetState()
		{
			return state;
		}

		/// <summary>
		/// Compares to.
		/// </summary>
		/// <returns>The to.</returns>
		/// <param name="obj">Object.</param>
		public int CompareTo(object obj)
		{

			if (obj is State<T>)
			{
				return this.Cost.CompareTo((obj as State<T>).Cost);
			}
			throw new ArgumentException("Object is not a State");

		}

		/// <summary>
		/// Serves as a hash function for a <see cref="T:SearchAlgorithmsLib.State`1"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode()
		{
			return state.ToString().GetHashCode();
			//return String.Intern(state.ToString()).GetHashCode();
			//return state.GetHashCode();

		}

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:SearchAlgorithmsLib.State`1"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:SearchAlgorithmsLib.State`1"/>.</returns>
		public override string ToString()
		{
			return state.ToString();
		}

		/// <summary>
		/// State pool.
		/// </summary>
		public static class StatePool
		{
			/// <summary>
			/// The pool.
			/// </summary>
			private static Dictionary<T, State<T>> pool = new Dictionary<T, State<T>>();
			/// <summary>
			/// Gets the state.
			/// </summary>
			/// <returns>The state.</returns>
			/// <param name="item">Item.</param>
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