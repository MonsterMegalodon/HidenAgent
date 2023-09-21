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

    [SerializeField][Range(0, 1)] float followDamping;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        cameraTrans.position = cameraArm.position - cameraTrans.forward * armLength; 

        transform.position = Vector3.Lerp(transform.position, followTransform.position, (1 - followDamping)*Time.deltaTime*20f);
    }

    public void AddYawInput(float amt)
    {
        transform.Rotate(Vector3.up, amt * Time.deltaTime * turnSpeed);
    }

}

