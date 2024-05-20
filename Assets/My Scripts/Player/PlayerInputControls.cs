using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputControls : MonoBehaviour
{
    private PlayerControlsInputAction inputActions;
    private Vector3 moveDir;
    public event Action<Vector3> OnMoveActionPerfomed;
    public event Action OnMoveActionCanceled;
    public event Action OnShootActionPerformed;
    public event Action OnShootActionCanceled;
    private void Awake()
    {
        inputActions = new PlayerControlsInputAction();
    }
    void Start()
    {
        inputActions.Enable();
        inputActions.PlayerControlsMappp.Move.performed += MoveActionPerformed;
        inputActions.PlayerControlsMappp.Move.canceled += MoveActionCanceled;
        inputActions.PlayerControlsMappp.Shoot.performed += ShootActionPerformed;
        inputActions.PlayerControlsMappp.Shoot.canceled += ShootActionCanceled;
    }
    private void ShootActionCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnShootActionCanceled?.Invoke();
    }

    private void MoveActionCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        moveDir = Vector3.zero;
        OnMoveActionCanceled?.Invoke();
    }

    private void MoveActionPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 v2Movement = context.ReadValue<Vector2>();
        moveDir = new Vector3(v2Movement.x, 0f, v2Movement.y);
    }
    private void ShootActionPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnShootActionPerformed?.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        if(moveDir != Vector3.zero)
        {
            OnMoveActionPerfomed?.Invoke(moveDir);
        }
    }
}
