using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private Joystick moveStick;
    [SerializeField] private Joystick aimStick;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] CameraRig cameraRig;
    CharacterController characterController;
    Vector2 aimInput;
    Vector2 moveInput;

    Camera viewCamera;

    private void Awake()
    {
        moveStick.onInputValueChanged += MoveInputUpdated;
        aimStick.onInputValueChanged += AimInputUpdated;
        //initializing values
        characterController = GetComponent<CharacterController>();
        viewCamera = Camera.main;
    }

    private void AimInputUpdated(Vector2 inputVal)
    {
        aimInput = inputVal;
    }

    private void MoveInputUpdated(Vector2 inputVal)
    {
        moveInput = inputVal;
    }

    // Start is called before the first frame update
    void Start()
    {
        //starting of logics
    }

    // Update is called once per frame
    void Update()
    {
        ProccessMoveInput();
    }

    private void LateUpdate()
    {
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        cameraRig.AddYawInput(moveInput.x);
    }

    void ProccessMoveInput()
    {
        Vector3 rightdir = viewCamera.transform.right;
        //Vector3 upDir = Vector3.Cross(rightdir, Vector3.up);      //cross is more expensive

        //cheeper way
        Vector3 upDir = viewCamera.transform.forward;
        upDir.y = 0;
        upDir = upDir.normalized;
        

        characterController.Move(new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed * Time.deltaTime);
    }
}
