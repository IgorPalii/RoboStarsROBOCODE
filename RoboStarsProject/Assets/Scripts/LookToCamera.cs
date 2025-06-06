using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToCamera : MonoBehaviour
{
    private Camera charCamera;

    private void Start()
    {
        charCamera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.LookAt(charCamera.transform.position);
        transform.Rotate(Vector3.up * 180);
    }
}
