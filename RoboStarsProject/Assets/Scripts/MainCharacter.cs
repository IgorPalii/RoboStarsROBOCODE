using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class MainCharacter : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private PlayerInputActions inputActions;
    [SerializeField]
    private CharacterController cController;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private PhotonView pViev;
    [SerializeField]
    private GameObject cameraPrefab;

    private CameraFollow charCamera;

    private Vector2 movementInput;
    private Vector3 currentMovement;
    private Vector3 spawnPos;
    private Quaternion rotateDir;

    private bool isRun, isWalk;
    private float rotateSpeed = 10f;

    private void Awake()
    {
        spawnPos = new Vector3 (-5, 8.15f, 2);
        InitInputActions();
        if (pViev.IsMine)
        {
            var camera = Instantiate(cameraPrefab);
            charCamera = camera.GetComponent<CameraFollow>();
            charCamera.target = transform;
        }
    }

    private void InitInputActions()
    {
        inputActions = new PlayerInputActions();
        inputActions.CharacterController.Movement.started += OnMovementActions;
        inputActions.CharacterController.Movement.performed += OnMovementActions;
        inputActions.CharacterController.Movement.canceled += OnMovementActions;
        inputActions.CharacterController.Movement.started += OnCameraMovement;
        inputActions.CharacterController.Movement.performed += OnCameraMovement;
        inputActions.CharacterController.Movement.canceled += OnCameraMovement;
        inputActions.CharacterController.Run.started += OnRun;
        inputActions.CharacterController.Run.canceled += OnRun;
    }

    private void Update()
    {
        if (!pViev.IsMine) return;
        AnimateControl();
        PlayerRotate();
    }

    private void FixedUpdate()
    {
        if (!pViev.IsMine) return;
        cController.Move(currentMovement * Time.fixedDeltaTime);
    }

    private void AnimateControl()
    {
        animator.SetBool("isWalk", isWalk);
        animator.SetBool("isRun", isRun);
    }

    private void PlayerRotate()
    {
        if (!isWalk) return;
        rotateDir = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(currentMovement), Time.deltaTime * rotateSpeed);
        transform.rotation = rotateDir;
    }

    public void Respawn()
    {
        cController.enabled = false;
        transform.position = spawnPos;
        cController.enabled = true;
    }

    private void OnMovementActions(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        currentMovement.x = movementInput.x;
        currentMovement.z = movementInput.y;
        isWalk = movementInput.x != 0 || movementInput.y != 0;
    }

    private void OnRun(InputAction.CallbackContext context)
    {
        isRun = context.ReadValueAsButton();
    }

    private void OnCameraMovement(InputAction.CallbackContext context)
    {
        charCamera.SetOffset(currentMovement);
    }

    private void OnEnable()
    {
        inputActions.CharacterController.Enable();
    }

    private void OnDisable()
    {
        inputActions.CharacterController.Disable();
    }
}
