using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float offset = 10.0f;
    public bool boundTop = true;
    public bool boundBottom = true;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        bool outTop = cam.WorldToViewportPoint(transform.position - Vector3.forward * offset).y > 1f && boundTop;
        bool outBottom = cam.WorldToViewportPoint(transform.position + Vector3.forward * offset).y < 0f && boundBottom;
        if (outTop || outBottom)
        {
            Destroy(gameObject);
        }

        if (outBottom)
        {
            Debug.Log("GAME OVER");
            Time.timeScale = 0;
        }
    }
}
