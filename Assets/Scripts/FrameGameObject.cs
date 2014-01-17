using UnityEngine;
using System.Collections;

public class FrameGameObject : MonoBehaviour
{
    public GameObject Subject;

    // Update is called once per frame
    void Update ()
    {
        if (Subject == null)
            return;

        var bounds = Subject.GetComponent<MeshFilter> ().mesh.bounds;

        var position = Subject.transform.position;
        position += -Vector3.forward * Vector3.Dot (bounds.size, Vector3.forward);
        position += Vector3.up * Vector3.Dot (bounds.size, Vector3.forward)  * 0.5f;

        transform.position = position;
        //transform.LookAt (Subject.transform.position + Vpo);
    }
}
