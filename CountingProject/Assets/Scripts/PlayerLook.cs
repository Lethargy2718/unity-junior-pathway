using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    private Camera cam;
    private float yRotation = 0f;
    public float xSens = 30f;
    public float ySens = 30f;
    public float maxYRotation = 90f;


    private void Awake()
    {
        cam = Camera.main;
    }

    public void Look(Vector2 input)
    {
        var (mouseX, mouseY) = (input.x, input.y);

        yRotation -= mouseY * ySens;
        yRotation = Mathf.Clamp(yRotation, -maxYRotation, maxYRotation);

        cam.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        transform.Rotate(Vector3.up, mouseX * xSens);
    }
}
