using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform CameraHolder;
    [SerializeField] private Transform visual;
    [SerializeField] private Camera mainCam;
    
    public float sensitivity = 0.2f;

    public Vector3 cameraForward => transform.forward;

    private float MouseX = 0;
    private float MouseY = 0;

    private float Scrool = 5;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        MouseX = transform.rotation.eulerAngles.y;
        MouseY = transform.rotation.eulerAngles.x;
    }

    private float GScroll = 10;
    
    private void LateUpdate()
    {
        MouseX += Input.GetAxisRaw("Mouse X") * sensitivity;
        MouseY += -Input.GetAxisRaw("Mouse Y") * sensitivity;

        Scrool += -Input.mouseScrollDelta.y * sensitivity;
        
        MouseY = Mathf.Clamp(MouseY, -10, 50);
        Scrool = Mathf.Clamp(Scrool, 8, 30);

        GScroll = Scrool / 5f;
        
        // Vertical
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(MouseY, MouseX, 0f), 
            25 * Time.deltaTime);

        RotateVisual();

        var localPos = mainCam.transform.localPosition;

        mainCam.transform.localPosition = Vector3.Slerp(localPos, new Vector3(0, 0, -GetMaxDistance(Scrool)), 20 * Time.deltaTime);
    }

    private float GetMaxDistance(float maxDistance)
    {
        Ray ray = new Ray(CameraHolder.position, -mainCam.transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance / 5f))
        {
            if (!hit.transform.IsChildOf(target))
            {
                return hit.distance * 5;
            }
        }

        return maxDistance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(CameraHolder.position  , (mainCam.transform.position - CameraHolder.position).normalized * GScroll);
    }

    private void RotateVisual()
    {
        var temp = visual.rotation;
        temp = Quaternion.Slerp(temp, Quaternion.Euler(0, MouseX, 0f), 25 * Time.deltaTime);

        visual.rotation = temp;
    }
}
