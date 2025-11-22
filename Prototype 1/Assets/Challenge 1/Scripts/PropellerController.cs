using UnityEngine;

public class PropellerController : MonoBehaviour
{
    public float rotationsPerSecond = 1f;
    void Update()
    {
        float angle = rotationsPerSecond * 360f * Time.deltaTime;
        Debug.Log(angle);
        transform.Rotate(Vector3.forward, angle);        
    }
}
