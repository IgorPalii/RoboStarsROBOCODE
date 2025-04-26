using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainCharacter : MonoBehaviour
{
    [SerializeField]
    private PlayerInputActions inputActions;
    [SerializeField]
    private CharacterController cController;
    [SerializeField]
    private Animator animator;

    private Vector2 movementInput;
    private Vector3 currentMovement;
    private Quaternion rotateDir;

    private bool isRun, isWalk;
    private float rotateSpeed;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.CharacterController.Movement.started += OnMovementActions;
        inputActions.CharacterController.Movement.performed += OnMovementActions;
        inputActions.CharacterController.Movement.canceled += OnMovementActions;
        inputActions.CharacterController.Run.started += OnRun;
        inputActions.CharacterController.Run.canceled += OnRun;
        rotateSpeed = 10f;
    }

    private void Update()
    {
        AnimateControl();
        PlayerRotate();
    }

    private void FixedUpdate()
    {
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

    private void OnEnable()
    {
        inputActions.CharacterController.Enable();
    }

    private void OnDisable()
    {
        inputActions.CharacterController.Disable();
    }
}
