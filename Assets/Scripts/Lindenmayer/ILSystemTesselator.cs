using UnityEngine;
using System.Collections;

namespace Lindenmayer {

    public interface ILSystemTesselator
    {
        void Tesselate (LSystem system, Mesh mesh);
    }
}
