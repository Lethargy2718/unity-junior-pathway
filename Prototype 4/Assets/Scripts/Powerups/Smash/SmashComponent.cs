using UnityEngine;
using UnityTimer;

public class SmashComponent : MonoBehaviour, ISafeRemovable
{
    public float ascentTime = 0.23f;
    public float knockbackImpulse = 75f;
    public float maxDistance = 30f;
    public float cooldown = 1f;
    public float verticalSpeed = 60f;

    private Rigidbody rb;
    private Timer ascentTimer;

    private enum SmashState { Idle, Ascending, Falling, Cooldown }
    private SmashState state = SmashState.Idle;

    public bool IsSafeToRemove => state == SmashState.Idle;
    public bool SafeRemoveRequested { get; set; } = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (SafeRemoveRequested && IsSafeToRemove) {
            Destroy(this);
            return;
        }

        if (Input.GetMouseButton(0) && state == SmashState.Idle)
        {
            StartSmash();
        }
    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case SmashState.Ascending:
                rb.linearVelocity = Vector3.up * verticalSpeed;
                break;
            case SmashState.Falling:
                rb.linearVelocity = 2f * verticalSpeed * Vector3.down;
                break;
            default:
                break;
        }
    }

    private void StartSmash()
    {
        state = SmashState.Ascending;

        ascentTimer = this.AttachTimer(ascentTime, () =>
        {
            state = SmashState.Falling;
        });
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && state == SmashState.Falling)
        {
            Smash();
        }
    }

    private void Smash()
    {
        if (ascentTimer != null && !ascentTimer.isCompleted)
        {
            ascentTimer.Cancel();
            ascentTimer = null;
        }

        rb.linearVelocity = Vector3.zero;

        Vector3 pos = new Vector3(transform.position.x, 0f, transform.position.z);
        rb.position = pos;

        ApplyKnockbackToEnemies();

        state = SmashState.Cooldown;
        this.AttachTimer(cooldown, () =>
        {
            state = SmashState.Idle;
        });
    }

    private void ApplyKnockbackToEnemies()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        foreach (Enemy enemy in enemies)
        {
            ApplyKnockback(enemy);
        }

        GameObject[] enemyProjeciles = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject projectile in enemyProjeciles)
        {
            if (Vector3.Distance(projectile.transform.position, transform.position) < 10.0f)
            {
                Destroy(projectile);
            }
        }
    }

    private void ApplyKnockback(Component target)
    {
        if (target.TryGetComponent<Knockback>(out var knockback))
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            float factor = Mathf.Clamp01(distance / maxDistance); // close -> smaller
            float finalImpulse = knockbackImpulse * (1f - factor);

            Vector3 pos = transform.position;
            pos.y = knockback.transform.position.y;
            knockback.ApplyKnockback(pos, finalImpulse);
        }
    }

    public void SafeRemove()
    {
        if (IsSafeToRemove)
        {
            Destroy(this);
        }
        else
        {
            SafeRemoveRequested = true;
        }
    }

}
