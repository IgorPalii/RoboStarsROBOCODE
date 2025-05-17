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
    private GameObject targetMark;
    [SerializeField]
    private PhotonView pView;
    [SerializeField]
    private CharacterController cController;

    private List<GameObject> targets;
    private PlayerInputActions playerInput;   
    private GameObject targetObj;

    private float range = 5;
    private bool canSearch = true;
    private int targetCount;

    private void Awake()
    {
        playerInput = new PlayerInputActions();
    }

    private void Start()
    {
        if (!pView.IsMine) return;

        targetMark.SetActive(false);
        playerInput.CharacterController.ChangeTarget.started += SelectNewTarget;
    }

    private void FixedUpdate()
    {
        if (!pView.IsMine) return;
        SelectTarget();
    }

    private void SelectTarget()
    {
        if (cController.velocity == Vector3.zero)
        {
            if (canSearch)
            {
                InvokeRepeating("Calculate", 0f, 0.5f);
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

    private void Calculate()
    {
        canSearch = false;
        targets.Clear();

        RaycastHit[] raycastHits = Physics.SphereCastAll(transform.position, range, transform.position, range);
        foreach (RaycastHit hit in raycastHits)
        {
            GameObject tempObj = hit.transform.gameObject;
            if (tempObj.GetComponent<CharacterController>() && !tempObj.GetComponentInParent<PhotonView>().IsMine)
            {
                targets.Add(tempObj);
            }
            else continue;
        }
        SelectNewTarget();
    }

    private void SelectNewTarget()
    {
        foreach (GameObject target in targets) 
        {
            target.GetComponent<Aim>().SetTargetStatus(false);
        }

        if (targetCount >= targets.Count)
        {
            targetCount = 0;
        }

        targetObj = targets[targetCount];
        targets[targetCount].GetComponent<Aim>().SetTargetStatus(true);
    }

    private void SelectNewTarget(InputAction.CallbackContext context)
    {
        targetCount++;
        SelectNewTarget();
    }

    private void OnEnable() =>    
        playerInput.CharacterController.Enable();
    

    private void OnDisable() {
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
