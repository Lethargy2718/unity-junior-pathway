using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Projectiles")]
public class ProjectilesPowerup : Powerup
{
    public float duration = 4.0f;
    public override float Duration => duration;

    private ProjectileComponent projectileComponent;

    public override void OnApply(GameObject target)
    {
        if (target.TryGetComponent<ProjectileComponent>(out var component))
        {
            Destroy(component);
        }

        projectileComponent = target.AddComponent<ProjectileComponent>();
    }

    public override void OnRemove(GameObject target)
    {
        if (projectileComponent != null)
        {
            Destroy(projectileComponent);
        }
    }
}
