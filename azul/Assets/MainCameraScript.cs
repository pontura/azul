using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smooth = 0.025f;
    Vector3 lookTo;
    public GameObject targetGO;
    float timer;
    void Update()
    {
        Vector3 lookPos = (target.transform.position + offset) ;
        targetGO.transform.position = Vector3.Lerp(targetGO.transform.position, lookPos, smooth * Time.deltaTime);
        transform.LookAt(targetGO.transform);
    }
}
