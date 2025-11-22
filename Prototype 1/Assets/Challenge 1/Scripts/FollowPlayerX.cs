using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    public GameObject plane;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position;
    }

    void Update()
    {
        transform.position = plane.transform.position + offset;
    }
}
