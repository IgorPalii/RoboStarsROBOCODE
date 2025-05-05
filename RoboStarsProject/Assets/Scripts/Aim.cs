using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;
using System.IO;

public class Aim : MonoBehaviour
{
    [SerializeField]
    private Transform spawnT;
    [SerializeField]
    private List<GameObject> targets;
    [SerializeField]
    private GameObject targetMark;
    [SerializeField]
    private PhotonView pView;
    [SerializeField]
    private CharacterController cController;

    private PlayerInputActions playerInput;   
    private GameObject targetObj;

    private float range = 5;
    private bool canSearch = true;
    private int targetCount;

    private void Awake()
    {
        playerInput = new PlayerInputActions();
    }

    private void Update()
    {
        
    }

    private void SetTarget()
    {
        if (cController.velocity == Vector3.zero)
        {
            if (canSearch)
            {
                InvokeRepeating("", 0f, 0.5f);
            }
        }
        else
        {
            if (targetObj != null)
            {
                targetObj.GetComponent<Aim>().SetTargetStatus(false);
                targetObj = null;
            }
            canSearch = true;
            CancelInvoke();
        }
    }

    private void OnEnable()
    {
        playerInput.CharacterController.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterController.Disable();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void SetTargetStatus(bool isTarget)
    {
        targetMark.SetActive(isTarget);
    }
}
