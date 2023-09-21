using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private Joystick moveStick;
    [SerializeField] private Joystick aimStick;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float turnSpeed = 30f;
    [SerializeField] CameraRig cameraRig;
    CharacterController characterController;
    Vector2 moveInput;
    Vector2 aimInput;

    Vector3 moveDir;
    Vector3 aimDir;

    Camera viewCamera;

    Animator animator;

    private void Awake()
    {
        moveStick.onInputValueChanged += MoveInputUpdated;
        aimStick.onInputValueChanged += AimInputUpdated;
        //initializing values
        characterController = GetComponent<CharacterController>();
        viewCamera = Camera.main;
        animator = GetComponent<Animator>();    
    }

    private void AimInputUpdated(Vector2 inputVal)
    {
        aimInput = inputVal;
        aimDir = ConvertInputToWorldDirection(aimInput);
    }

    private void MoveInputUpdated(Vector2 inputVal)
    {
        moveInput = inputVal;
        moveDir = ConvertInputToWorldDirection(moveInput);
    }

    // Start is called before the first frame update
    void Start()
    {
        //starting of logics
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMoveInput();
        ProcessAimInput();
        UPdateAnimation();
    }

    private void UPdateAnimation()
    {
        float leftSpeed = Vector3.Dot(moveDir, transform.right);
        float forwardSpeed = Vector3.Dot(moveDir, transform.forward);

        animator.SetFloat("leftSpeed", leftSpeed);
        animator.SetFloat("fwdSpeed", forwardSpeed);
    }

    private void ProcessAimInput()
    {
        //if aim has input, use the aim to determin the turning, if not, use the move input.
        Vector3 lookDir = aimDir.magnitude != 0 ? aimDir : moveDir; //oneliner is often bad practice.

        if (lookDir.magnitude != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDir, Vector3.up), Time.deltaTime * turnSpeed);
        }
    }

    private void LateUpdate()
    {
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        if(aimDir.magnitude == 0)
        {
            cameraRig.AddYawInput(moveInput.x);
        }
    }

    Vector3 ConvertInputToWorldDirection(Vector2 inputVal)
    {
        Vector3 rightDir = viewCamera.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);

        return (rightDir * inputVal.x + upDir * inputVal.y).normalized;
    }

    private void ProcessMoveInput()
    {
        characterController.Move(moveDir * moveSpeed * Time.deltaTime);
    }
}
