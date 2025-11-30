using System.Collections;
using UnityEngine;
using UnityTimer;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject focalPoint;

    public float acceleration = 10f;

    [Header("Dash Settings")]
    public float dashCooldown = 4.0f;
    public float dashImpulse = 10.0f;
    private bool canDash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -5.0f)
        {
            Time.timeScale = 0;
            enabled = false;
        }

        if (Input.GetKey(KeyCode.E) && canDash) {
            Dash();
        }
        else
        {
            float input = Input.GetAxis("Vertical");
            rb.AddForce(input * acceleration * focalPoint.transform.forward, ForceMode.Acceleration);
        }
    }

    private void Dash()
    {
        if (!canDash) return;

        float input = Input.GetAxisRaw("Vertical");
        if (input == 0) input = 1;

        rb.AddForce(input * dashImpulse * focalPoint.transform.forward, ForceMode.Impulse);

        canDash = false;
        this.AttachTimer(dashCooldown, () => canDash = true);
    }
}
