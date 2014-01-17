using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Lindenmayer {
    public class LSystem
    {
        public struct ProductionRule
        {
            public readonly string Predecessor;
            public readonly string Successor;

            public ProductionRule (string predecessor, string successor)
            {
                Predecessor = predecessor;
                Successor = successor;
            }
        }

        interface IModule
        {
            char Type { get; set; }
        }

        struct Module : IModule
        {
            public char Type { get; set; }
        }

        struct ParamtericModule<T> : IModule
        {
            public char Type { get; set; }
            public T Value;
        }

        // the initial state.
        public string Axiom = string.Empty;
        public string Alphabet = string.Empty;
        public string State { get; private set; }
        public int Iterations { get; private set; }
        public List<ProductionRule> Rules;

        // presentation values.
        public float BranchLength;
        public float BranchWidth;
        public float BranchDegrees;

        public LSystem ()
        {
            Reset ();
        }

        public void Simulate ()
        {
            Simulate (1);
        }

        public void Simulate (int steps)
        {
            if (steps < 0)
                return;

            if (Iterations == 0)
                Reset ();

            for (int i = 0; i < steps; i++) {
                State = Derive (State);
            }

            Iterations += steps;
        }

        string Derive (string state)
        {
            var buffer = new StringBuilder ();

            for (int i = 0; i < state.Length; i++) {
                var symbol = state [i];

                var identity = true;
                foreach (var rule in Rules) {
                    if (symbol == rule.Predecessor [0]) {
                        buffer.Append (rule.Successor);
                        identity = false;
                        break;
                    }
                }

                // use identity rule: symbol -> symbol.
                if (identity)
                    buffer.Append (symbol);
            }

            return buffer.ToString ();
        }

        public void Reset ()
        {
            State = Axiom;
            Iterations = 0;
        }

        public override string ToString ()
        {
            return State;
        }

    }
}
