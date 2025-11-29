using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Projectiles")]
public class ProjectilesPowerup : Powerup
{
    public float duration = 4.0f;
    public float cooldown = 0.5f;
    public override float Duration => duration;

    public Projectile projectile;

    private ProjectileComponent projectileComponent;

    public override void OnApply(GameObject target)
    {
        if (target.TryGetComponent<ProjectileComponent>(out var component))
        {
            Destroy(component);
        }

        projectileComponent = target.AddComponent<ProjectileComponent>();
        projectileComponent.projectilePrefab = projectile;
        projectileComponent.cooldown = cooldown;
    }

    public override void OnRemove(GameObject target)
    {
        if (projectileComponent != null)
        {
            Destroy(projectileComponent);
        }

        Projectile[] projectiles = FindObjectsByType<Projectile>(FindObjectsSortMode.None);
        foreach (var projectile in projectiles) Destroy(projectile.gameObject);
    }
}
