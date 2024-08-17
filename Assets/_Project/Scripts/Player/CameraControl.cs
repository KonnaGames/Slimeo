using System;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Camera mainCam;
    
    public float sensitivity = 0.2f;

    public Vector3 cameraForward => transform.forward;

    private float MouseX = 0;
    private float MouseY = 0;

    private float Scrool = 5;

    private void Start()
    {
        Cursor.visible = false  ;
        Cursor.lockState = CursorLockMode.Locked;

        MouseX = transform.rotation.eulerAngles.y;
        MouseY = transform.rotation.eulerAngles.x;
    }


    private void LateUpdate()
    {
        MouseX += Input.GetAxisRaw("Mouse X") * sensitivity;
        MouseY += -Input.GetAxisRaw("Mouse Y") * sensitivity;

        Scrool += -Input.mouseScrollDelta.y * sensitivity;
        
        MouseY = Mathf.Clamp(MouseY, -10, 50);
        Scrool = Mathf.Clamp(Scrool, 2, 7);
        
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.Euler(MouseY, MouseX, 0f), 
            25 * Time.deltaTime);

        // mainCam.transform.localPosition = new Vector3(0, 0, -Scrool);
        var localPos = mainCam.transform.localPosition;

        mainCam.transform.localPosition = Vector3.Slerp(localPos, new Vector3(0, 0, -Scrool), 20 * Time.deltaTime);
    }
}
