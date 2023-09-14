using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]

public class CameraRig : MonoBehaviour
{
    [SerializeField] Transform followTransform;
    [SerializeField] float armLength;
    [SerializeField] Transform cameraTrans;
    [SerializeField] Transform cameraArm;
    [SerializeField] float turnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddYawInput(float amt)
    {
        transform.Rotate(Vector3.up, amt * Time.deltaTime * turnSpeed);
    }

    private void LateUpdate()
    {
        cameraTrans.position = cameraArm.position - cameraTrans.forward * armLength;
        transform.position = followTransform.position;
    }
}
