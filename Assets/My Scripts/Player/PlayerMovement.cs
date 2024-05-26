using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerInputControls), typeof(PlayerAnimations))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerInputControls inputControls;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveThreshold;
    void Start()
    {
        inputControls = GetComponent<PlayerInputControls>();
        inputControls.OnMoveActionPerfomed += Move;
    }

    private void Move(Vector3 inputMovement)
    {
        if (inputMovement.magnitude < moveThreshold) return;
        float targetAngle = Mathf.Atan2(inputMovement.x, inputMovement.z) * Mathf.Rad2Deg + Camera.main.transform.rotation.eulerAngles.y;
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        transform.position += new Vector3(moveDir.x, 0f, moveDir.z).normalized * moveSpeed;

        //float angle = Mathf.Atan2(inputMovement.x, inputMovement.z) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
