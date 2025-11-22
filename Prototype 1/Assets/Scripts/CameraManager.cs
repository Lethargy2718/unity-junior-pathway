using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera driverCamera;
    private bool isMainCamera = true;

    private void Start()
    {
        mainCamera.enabled = true;
        driverCamera.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) { 
            
            isMainCamera = !isMainCamera;

            mainCamera.enabled = isMainCamera;
            driverCamera.enabled = !isMainCamera;
        }
    }
}
