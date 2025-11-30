using System;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    // impulse, final impulse (after resistance reduction)
    public event Action<float, float> OnKnockbackApplied;

    [Range(0f, 1f)]
    [Tooltip("0 = no resistance, 1 = immune")]
    public float knockbackResistance = 0f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ApplyKnockback(Vector3 source, float impulse)
    {
        Vector3 sourceToSelf = (transform.position - source).normalized;
        float finalImpulse = impulse - knockbackResistance * impulse;

        rb.AddForce(finalImpulse * sourceToSelf, ForceMode.Impulse);
        OnKnockbackApplied?.Invoke(impulse, finalImpulse);
    }
}
