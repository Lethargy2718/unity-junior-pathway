using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float Speed => GameManager.Instance.Speed;
    private void Update()
    {
        transform.Translate(Speed * Time.deltaTime * Vector3.left);
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
