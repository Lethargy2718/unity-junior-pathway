using UnityEngine;

public class DecreaseKnockbackResistance : MonoBehaviour
{
    public float maxImpulse = 300f;
    public float minResistance = -1f;

    private float totalImpulse = 0f;
    private Knockback knockbackComponent;

    private void Awake()
    {
        knockbackComponent = GetComponent<Knockback>();
        knockbackComponent.OnKnockbackApplied += HandleKnockback;
    }

    private void HandleKnockback(float impulse, float finalImpulse)
    {
        totalImpulse += impulse;
        knockbackComponent.knockbackResistance = GetKnockbackResistance();
    }

    private float GetKnockbackResistance()
    {
        float percentage = Mathf.InverseLerp(0, maxImpulse, totalImpulse);
        float currentResistance = Mathf.Lerp(1.0f, minResistance, percentage);
        return currentResistance;
    }

    private void OnDestroy()
    {
        if (knockbackComponent != null)
            knockbackComponent.OnKnockbackApplied -= HandleKnockback;
    }
}
