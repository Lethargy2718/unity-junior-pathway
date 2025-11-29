using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed = 45f;
    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, -input * rotationSpeed * Time.deltaTime);

    }
}
