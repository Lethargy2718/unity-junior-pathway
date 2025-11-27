using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 10.0f;

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.left);
        if (transform.position.y < -10) Destroy(gameObject);
    }

    private void OnEnable()
    {
        GameEvents.OnGameOver += StopMoving;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= StopMoving;
    }

    private void StopMoving()
    {
        enabled = false;
    }
}
