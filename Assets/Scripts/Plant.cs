using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plant : MonoBehaviour
{
    public int StimulateSteps = 1;

    ILSystemTesselator tesselator = new TurtleTesselator ();
    Mesh mesh;

    // Use this for initialization
    void Start ()
    {
        mesh = GetComponent<MeshFilter> ().mesh;

        var system = GetPlant7 ();
        system.Simulate (StimulateSteps);
        tesselator.Tesselate (system, mesh);

        Debug.Log (system);
    }

    // Update is called once per frame
    void Update ()
    {
    }

    LSystem GetPlant1 ()
    {
        var system = new LSystem ();
        system.Axiom = "F";
        system.Rules = new List<LSystem.ProductionRule> ()
        {
            new LSystem.ProductionRule ("F", "F[+F]F[-F]F")
        };
        system.BranchLength = 5;
        system.BranchWidth = 1;
        system.BranchDegrees = 25.7f;

        return system;
    }

    LSystem GetPlant2 ()
    {
        var system = new LSystem ();
        system.Axiom = "F";
        system.Rules = new List<LSystem.ProductionRule> ()
        {
            new LSystem.ProductionRule ("F", "F[+F]F[-F][F]")
        };
        system.BranchLength = 5;
        system.BranchWidth = 1;
        system.BranchDegrees = 20f;

        return system;
    }

    LSystem GetPlant3 ()
    {
        var system = new LSystem ();
        system.Axiom = "F";
        system.Rules = new List<LSystem.ProductionRule> ()
        {
            new LSystem.ProductionRule ("F", "FF-[-F+F+F]+[+F-F-F]")
        };
        system.BranchLength = 5;
        system.BranchWidth = 1;
        system.BranchDegrees = 22.5f;

        return system;
    }

    LSystem GetPlant4 ()
    {
        var system = new LSystem ();
        system.Axiom = "F";
        system.Rules = new List<LSystem.ProductionRule> ()
        {
            new LSystem.ProductionRule ("X", "F[+X]F[-X]+X"),
            new LSystem.ProductionRule ("F", "FF")
        };
        system.BranchLength = 5;
        system.BranchWidth = 1;
        system.BranchDegrees = 20f;

        return system;
    }

    LSystem GetPlant5 ()
    {
        var system = new LSystem ();
        system.Axiom = "X";
        system.Rules = new List<LSystem.ProductionRule> ()
        {
            new LSystem.ProductionRule ("X", "F[+X][-X]FX"),
            new LSystem.ProductionRule ("F", "FF")
        };
        system.BranchLength = 5;
        system.BranchWidth = 1;
        system.BranchDegrees = 25.7f;

        return system;
    }

    LSystem GetPlant6 ()
    {
        var system = new LSystem ();
        system.Axiom = "X";
        system.Rules = new List<LSystem.ProductionRule> ()
        {
            new LSystem.ProductionRule ("X", "F-[[X]+X]+F[+FX]-X"),
            new LSystem.ProductionRule ("F", "FF")
        };
        system.BranchLength = 5;
        system.BranchWidth = 1;
        system.BranchDegrees = 22.5f;

        return system;
    }

    LSystem GetPlant7 ()
    {
        var system = new LSystem ();
        system.Axiom = "A";
        system.Rules = new List<LSystem.ProductionRule> ()
        {
            new LSystem.ProductionRule ("A", "[&FL!A]/////’[&FL!A]///////’[&FL!A]"),
            new LSystem.ProductionRule ("F", "S ///// F"),
            new LSystem.ProductionRule ("S", "F L"),
            new LSystem.ProductionRule ("L", "[’’’^^{-f.+f.+f.-|-f.+f.+f.}]")
        };
        system.BranchLength = 5;
        system.BranchWidth = 1;
        system.BranchDegrees = 22.5f;

        return system;
    }

    LSystem GetLeaf1 ()
    {
        var system = new LSystem ();
        system.Axiom = "HSV(70, 1, 1) a";
        system.Rules = new List<LSystem.ProductionRule> ()
        {
            new LSystem.ProductionRule ("a", "[b]/[b]"),
            new LSystem.ProductionRule ("b", "H(40) [ -(19){.b.].c}"),
            new LSystem.ProductionRule ("c", "f(0.4)c"),
        };
        system.BranchLength = 5;
        system.BranchWidth = 1;
        system.BranchDegrees = 30f;

        return system;
    }

    LSystem GetLeaf2 ()
    {
        var system = new LSystem ();
        system.Axiom = "a{[++++f.][++ff.][+fff.][fffff.][-fff.][--ff.][----f.]}";
        system.Rules = new List<LSystem.ProductionRule> ();
        system.BranchLength = 5;
        system.BranchWidth = 1;
        system.BranchDegrees = 25.7f;

        return system;
    }

    LSystem GetLeaf3 ()
    {
        var system = new LSystem ();
        system.Axiom = "[{+.G.{.&G.{.&G.][-G[&G[&G.].}.].}.}]";
        system.Rules = new List<LSystem.ProductionRule> ();
        system.BranchLength = 5;
        system.BranchWidth = 1;
        system.BranchDegrees = 25.7f;

        return system;
    }

    LSystem GetLeaf4 ()
    {
        var system = new LSystem ();
        system.Axiom = "[A][B]";
        system.Rules = new List<LSystem.ProductionRule> ()
        {
            new LSystem.ProductionRule ("A", "[+A{.].C.}"),
            new LSystem.ProductionRule ("B", "[-B{.].C.}"),
            new LSystem.ProductionRule ("C", "GC"),
        };
        system.BranchLength = 5;
        system.BranchWidth = 1;
        system.BranchDegrees = 25.7f;

        return system;
    }

    LSystem GetLeaf5 ()
    {
        var system = new LSystem ();
        system.Axiom = "[X(36)A]/(72)[X(36)B]";
        system.Rules = new List<LSystem.ProductionRule> ()
        {
            new LSystem.ProductionRule ("A", "[&GA{.]."),
            new LSystem.ProductionRule ("B", "B&.G.}"),
            new LSystem.ProductionRule ("X(a)", "X(a + 4.5)"),
        };
        system.BranchLength = 5;
        system.BranchWidth = 1;
        system.BranchDegrees = 25.7f;

        return system;
    }

}
