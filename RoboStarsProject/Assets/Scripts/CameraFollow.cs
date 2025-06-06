using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target { get; set; }

    private Vector3 cameraOffset, currentVector;

    private float returnSpeed = 1f, height = 8f, rearDistance = 10f;

    private void Start()
    {
        transform.position = new Vector3(
            target.position.x, 
            target.position.y + height, 
            target.position.z - rearDistance);
        transform.rotation = Quaternion.LookRotation(target.position - transform.position);
    }

    private void Update()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        currentVector = new Vector3(target.position.x + cameraOffset.x,
            target.position.y + height,
            (target.position.z - rearDistance) + cameraOffset.z);

        transform.position = Vector3.Lerp(transform.position, currentVector, returnSpeed * Time.deltaTime);
    }

    public void SetOffset(Vector3 offset)
    {
        cameraOffset = offset * 5;
    }
}
