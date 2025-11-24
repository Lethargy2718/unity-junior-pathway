using UnityEngine;

public class MoveForwardX : MonoBehaviour
{
    public float speed;
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.x < -50 || transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
