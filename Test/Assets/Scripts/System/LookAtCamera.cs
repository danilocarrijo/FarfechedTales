using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField]
    public Camera camera;

    // Update is called once per frame
    void Update()
    {
        //Vector3 v = camera.transform.position;
        //v.y = 180 - camera.transform.position.y;
        //v.x = v.z = 0.0f;
        transform.LookAt(transform.position + camera.transform.forward);
    }
}
