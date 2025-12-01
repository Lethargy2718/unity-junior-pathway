using UnityEngine;

public class MouseTrail : MonoBehaviour
{
    private Camera cam;
    private TrailRenderer trail;
    private BoxCollider coll;
    private bool active = false;

    private void Awake()
    {
        cam = Camera.main;

        trail = GetComponent<TrailRenderer>();
        trail.enabled = false;

        coll = GetComponent<BoxCollider>();
        coll.enabled = false;
    }

    private void Update()
    {
        if (!GameManager.Instance.CanClick)
        {
            UpdateComponents(false);
            return;
        }

        UpdateActivity();

        if (active)
        {
            UpdateMousePos();
        }
    }

    private void UpdateActivity()
    {
        if (Input.GetMouseButtonDown(0))
        {
            active = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            active = false;
        }
        else return;

        UpdateComponents(active);
    }

    private void UpdateMousePos()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 5f;
        transform.position = cam.ScreenToWorldPoint(pos);
    }

    private void UpdateComponents(bool state)
    {
        trail.enabled = state;
        coll.enabled = state;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Target>(out var target))
        {
            if (other.gameObject.CompareTag("Bad"))
            {
                GameManager.Instance.Lives--;
            }
            target.DestroySelf();
        }
    }
}
