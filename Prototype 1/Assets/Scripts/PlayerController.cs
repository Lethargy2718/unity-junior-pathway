using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputHandler inputHandler;
    public float forwardAcceleration = 5f;
    public float turnSpeed = 45.0f;
    public float maxSpeed = 100.0f;

    [Header("Input Keys")]
    public KeyCode keyLeft = KeyCode.A;
    public KeyCode keyRight = KeyCode.D;
    public KeyCode keyUp = KeyCode.W;
    public KeyCode keyDown = KeyCode.S;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputHandler = new(keyLeft, keyRight, keyUp, keyDown);
    }

    private void FixedUpdate()
    {
        var (sideInput, forwardInput) = inputHandler.GetInputs();
        ForceUpdate(sideInput, forwardInput);
    }

    private void ForceUpdate(float sideInput, float forwardInput)
    {
        transform.Rotate(Vector3.up, sideInput * turnSpeed * Time.fixedDeltaTime);
        rb.AddForce(forwardAcceleration * forwardInput * transform.forward, ForceMode.Acceleration);

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = maxSpeed * rb.linearVelocity.normalized;
        }
        
    }
}
