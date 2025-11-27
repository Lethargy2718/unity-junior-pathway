using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float jumpVelocity = 10f;
    public float gravityModifier = 1f;

    private Rigidbody rb;
    private Animator playerAnim;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
    }

    private void OnEnable()
    {
        GameEvents.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= HandleGameOver;
    }

    private void Update()
    {
        HandleJump();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
            return;
        }

        isGrounded = true;
    }

    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpVelocity, ForceMode.VelocityChange);
            playerAnim.SetTrigger("Jump_trig");
            isGrounded = false;
        }
    }

    private void HandleGameOver()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.freezeRotation = false;

        playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int", Random.Range(1,3));
        enabled = false;
    }
}
