﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    class State<T>
    {
        private T state; // the state represented by a string
        private double cost; // cost to reach this state (set by a setter)
        private State<T> cameFrom; // the state we came from to this state (setter)

        public State(T state) // CTOR
        {
            this.state = state;
            this.cost = 0;
            this.cameFrom = null;
        }
        public bool Equals(object obj) // we overload Object's Equals method
        {
            return state.Equals((obj as State<T>).state);
        }

        public T GetState()
        {
            return this.state;
        }

        public void SetCost(double newCost)
        {
            this.cost = newCost;
        }

        public double GetCost()
        {
            return this.cost;
        }
    }
}
