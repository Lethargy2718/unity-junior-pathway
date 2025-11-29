using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 360.0f;
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);   
    }
}
