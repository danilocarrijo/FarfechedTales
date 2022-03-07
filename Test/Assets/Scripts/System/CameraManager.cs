using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    [Header("Distance")]
    [Range(2f,20f)]public float distance = 5f;
    public float minDistance = 1f;
    public float maxDistance = 7f;
    public Vector3 offset;
    [Header("Speed")]
    public float smoothSpeed = 5f;
    public float scrollSensitivity = 5f;

    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!target)
        {
            print("NO TARGET FOR CAMERA");
            return;
        }

        float num = Input.GetAxis("Mouse ScrollWheel");
        distance -= num * scrollSensitivity;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        Vector3 pos = target.position + offset;

        pos -= transform.forward * distance;

        transform.position = Vector3.Lerp(transform.position, pos,smoothSpeed * Time.deltaTime);
;    }
}
