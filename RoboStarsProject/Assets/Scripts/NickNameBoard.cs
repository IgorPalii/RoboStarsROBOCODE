using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NickNameBoard : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;

    private void Update()
    {
        if (playerCamera != null)
        {
            transform.LookAt(playerCamera.transform);
            transform.Rotate(Vector3.up * 180);
        }
    }
}
