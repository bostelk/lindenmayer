using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurtleTesselator : ILSystemTesselator
{
    public void Tesselate (LSystem system, Mesh mesh)
    {
        var transforms = new Stack<Matrix4x4> ();

        var transform = Matrix4x4.identity;
        transforms.Push (transform);

        var vertices = new List<Vector3> ();
        var colors = new List<Color> ();
        var indices = new List<int> ();

        var polygon = new List<Vector3> ();
        var polygonStack = new Stack<List<Vector3>> ();

        var length = system.BranchLength;
        var width = system.BranchWidth;
        var theta = system.BranchDegrees;

        var color1 = new Color32 (198, 107, 74, 255);
        var color2 = new Color32 (198, 255, 74, 255);

        for (int i = 0; i < system.State.Length; i++) {
            var module = system.State [i];

            switch (module) {
            // move forward and draw a line.
            case 'F':
                var pointA = new Vector3 (width / 2, 0, 0);
                var pointB = new Vector3 (-width / 2, 0, 0);
                var pointC = new Vector3 (-width / 2, length, 0);
                var pointD = new Vector3 (width / 2, length, 0);

                vertices.Add (transform.MultiplyPoint3x4 (pointA));
                vertices.Add (transform.MultiplyPoint3x4 (pointB));
                vertices.Add (transform.MultiplyPoint3x4 (pointC));
                vertices.Add (transform.MultiplyPoint3x4 (pointD));

                colors.Add (color1);
                colors.Add (color1);
                colors.Add (color1);
                colors.Add (color1);

                int startIndex = vertices.Count - 4;

                // first tri.
                indices.Add (startIndex);
                indices.Add (startIndex + 1);
                indices.Add (startIndex + 2);

                // second tri.
                indices.Add (startIndex + 2);
                indices.Add (startIndex + 3);
                indices.Add (startIndex);

                transform *= Matrix4x4.TRS (
                    length * Vector3.up,
                    Quaternion.identity,
                    Vector3.one
                );
                break;
            // move forward without drawing a line.
            case 'G':
            case 'f':
                transform *= Matrix4x4.TRS (
                    length * Vector3.up,
                    Quaternion.identity,
                    Vector3.one
                );
                break;
            // turn left.
            case '+':
                transform *= Matrix4x4.TRS (
                    Vector3.zero,
                    Quaternion.AngleAxis (-theta, Vector3.forward),
                    Vector3.one
                );
                break;
            // trun right.
            case '-':
                transform *= Matrix4x4.TRS (
                    Vector3.zero,
                    Quaternion.AngleAxis (theta, Vector3.forward),
                    Vector3.one
                );
                break;
            // pitch up.
            case '^':
                transform *= Matrix4x4.TRS (
                    Vector3.zero,
                    Quaternion.AngleAxis (-theta, Vector3.right),
                    Vector3.one
                );
                break;
            // pitch down.
            case '&':
                transform *= Matrix4x4.TRS (
                    Vector3.zero,
                    Quaternion.AngleAxis (theta, Vector3.right),
                    Vector3.one
                );
                break;
            // roll left.
            case '\\':
                transform *= Matrix4x4.TRS (
                    Vector3.zero,
                    Quaternion.AngleAxis (-theta, Vector3.up),
                    Vector3.one
                );
                break;
            // roll right.
            case '/':
                transform *= Matrix4x4.TRS (
                    Vector3.zero,
                    Quaternion.AngleAxis (theta, Vector3.up),
                    Vector3.one
                );
                break;
            // turn around.
            case '|':
                transform *= Matrix4x4.TRS (
                    Vector3.zero,
                    Quaternion.AngleAxis (180, Vector3.forward),
                    Vector3.one
                );
                break;
            // rotate turtle to vertical.
            case '$':
                break;
            // start a branch.
            case '[':
                transforms.Push (transform);
                break;
            // complete a branch.
            case ']':
                transform = transforms.Pop ();
                break;
            // decrement segment diameter.
            case '!':
                break;
            // cut off the remainer of the branch.
            case '%':
                break;
            // begin polygon.
            case '{':
                polygonStack.Push (polygon);
                polygon = new List<Vector3> ();

                var begin = transform.MultiplyPoint3x4 (Vector3.zero);
                polygon.Add (begin);
                break;
            // end polygon.
            case '}':
                var end = transform.MultiplyPoint3x4 (Vector3.zero);
                polygon.Add (end);

                // convert polygon into triangles. think triangle-fan.
                for (int v = 1; v < polygon.Count; v++) {
                    var A = polygon [0];
                    var B = polygon [v];
                    var C = polygon [(v + 1) % polygon.Count];

                    var normal = Vector3.Cross (B - A, C - A).normalized;

                    if (normal.z > 0) {
                        vertices.Add (A);
                        vertices.Add (B);
                        vertices.Add (C);
                    } else {
                        vertices.Add (A);
                        vertices.Add (C);
                        vertices.Add (B);
                    }

                    colors.Add (color2);
                    colors.Add (color2);
                    colors.Add (color2);

                    indices.Add (vertices.Count - 1);
                    indices.Add (vertices.Count - 2);
                    indices.Add (vertices.Count - 3);
                }

                if (polygonStack.Count > 0) {
                    polygon = polygonStack.Pop ();
                }
                break;
            // add current position as a vertex in the polygon.
            case '.':
                var point = transform.MultiplyPoint3x4 (Vector3.zero);
                polygon.Add (point);
                break;
            }
        }

        mesh.vertices = vertices.ToArray ();
        mesh.colors = colors.ToArray ();
        mesh.SetIndices (indices.ToArray (), MeshTopology.Triangles, 0);
    }
}
