using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    private PlayerInputControls inputActions;
    private AnimatorControllerParameter[] allParams;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        inputActions = GetComponent<PlayerInputControls>();
        allParams = anim.parameters;

        inputActions.OnMoveActionPerfomed += InputActions_OnMoveActionPerfomed;
        inputActions.OnMoveActionCanceled += InputActions_OnMoveActionCanceled;
        inputActions.OnShootActionPerformed += InputActions_OnShootActionPerformed;
        inputActions.OnShootActionCanceled += InputActions_OnShootActionCanceled;
    }

    private void InputActions_OnShootActionCanceled()
    {
        SetOneParameterToTrue("IsIdle");
    }

    private void InputActions_OnShootActionPerformed()
    {
        SetOneParameterToTrue("IsShooting");
    }

    private void InputActions_OnMoveActionCanceled()
    {
        SetOneParameterToTrue("IsIdle");
    }

    private void InputActions_OnMoveActionPerfomed(Vector3 obj)
    {
        SetOneParameterToTrue("IsRunning");
    }
    void SetOneParameterToTrue(string parameter)
    {
        foreach(var param in allParams)
        {
            if (param.name == parameter)
                anim.SetBool(param.name, true);
            else
                anim.SetBool(param.name, false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
