using System.Collections;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float baseSpeed = 12.5f;
    [SerializeField] private float sprintSpeedMultiplier = 2f;
    [SerializeField] private float dashMultiplier = 4f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private bool preserveDashVerticalVelocity = true;
    [SerializeField] private float velocitySmoothSpeed = 10.0f;
    [SerializeField] private float jumpForce = 25f;
    [SerializeField] private float gravity = 50f;

    [Header("Head Bob & FOV")]
    [SerializeField] private float bobFrequency = 1.6f;
    [SerializeField] private float bobAmplitude = 0.36f;
    [SerializeField] private float baseFOV = 75f;
    [SerializeField] private float fovChange = 1.1f;

    private CharacterController controller;
    private Camera cam;

    private float yVelocity = 0.0f;
    private Vector3 dashVelocity;
    private Vector3 currentVelocity;
    private Vector3 camInitialLocalPos;

    private bool isDashing = false;
    private bool canDash = true;
    private bool dashedOnceInAir = false;
    private bool isSprinting = false;

    private float bobTime = 0f;

    private float Speed => isSprinting ? baseSpeed * sprintSpeedMultiplier : baseSpeed;
    public bool IsGrounded => controller.isGrounded;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        camInitialLocalPos = cam.transform.localPosition;
    }

    private void Update()
    {
        HandleGravity();

        if (isDashing)
        {
            controller.Move(dashVelocity * Time.deltaTime);
        }

        HeadBob();
        FOV();

        // If grounded, reset air dash. Can dash again.
        if (IsGrounded) dashedOnceInAir = false;
    }

    private void HandleGravity()
    {
        if (!IsGrounded && !isDashing)
        {
            yVelocity -= gravity * Time.deltaTime;
        }

        if (controller.isGrounded && yVelocity < 0)
        {
            yVelocity = -2f;
        }
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            if (isDashing) EndDash();
            yVelocity = jumpForce;
        }
    }

    public void Move(Vector2 input)
    {
        if (isDashing) return;

        if (input.magnitude > 1f) input.Normalize();

        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        Vector3 targetVelocity = transform.TransformDirection(moveDirection) * Speed;

        targetVelocity.y = yVelocity;

        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.deltaTime * velocitySmoothSpeed);

        controller.Move(currentVelocity * Time.deltaTime);
    }

    public void Dash(Vector2 input)
    {
        if (!canDash) return;
        if (dashedOnceInAir && !IsGrounded) return;
        if (!IsGrounded) dashedOnceInAir = true;
        yVelocity = 0;
        StartDash(input);
        isDashing = true;
        canDash = false;
        StartCoroutine(WaitDash());
    }

    private void StartDash(Vector2 input)
    {
        Vector3 dashDirection;

        if (input.sqrMagnitude > 0.01f)
        {
            dashDirection = cam.transform.forward * input.y + cam.transform.right * input.x;
        }
        else
        {
            dashDirection = cam.transform.forward;
        }

        dashDirection.Normalize();

        dashVelocity = dashMultiplier * baseSpeed * dashDirection;
    }


    private IEnumerator WaitDash()
    {
        yield return new WaitForSeconds(dashDuration);
        EndDash();
    }

    private void EndDash()
    {
        canDash = true;
        isDashing = false;

        // Preserve velocity to smoothly lerp into the non-dashing velocity later
        currentVelocity = controller.velocity;

        // Optionally preserve yVelocity which causes the player to float/glide for a while after dashing in air
        if (preserveDashVerticalVelocity) yVelocity = controller.velocity.y;
    }

    public void ToggleSprint(bool sprint)
    {
        isSprinting = sprint;
    }

    private void HeadBob()
    {
        Vector3 horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);

        if (horizontalVelocity.magnitude > 0.1f && IsGrounded && !isDashing)
        {
            float bobIncrement = Time.deltaTime * bobFrequency * horizontalVelocity.magnitude;
            bobIncrement = Mathf.Clamp(bobIncrement, 0f, 0.05f); // remember to make it a serialized field
            bobTime += bobIncrement;

            Vector3 bobOffset = new Vector3(
                Mathf.Cos(bobTime * 0.5f) * bobAmplitude,
                Mathf.Sin(bobTime) * bobAmplitude,
                0
            );

            cam.transform.localPosition = camInitialLocalPos + bobOffset;
        }
        else
        {
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, camInitialLocalPos, Time.deltaTime * 8f);
        }
    }

    private void FOV()
    {
        float targetFOV = baseFOV + fovChange * controller.velocity.magnitude;
        targetFOV = Mathf.Clamp(targetFOV, 0f, 130.0f); // remember to make it a serialized field
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * 8f);
    }
}
