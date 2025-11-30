using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Projectiles")]
public class ProjectilesPowerup : Powerup<ProjectileComponent>
{
    public float duration = 4.0f;
    public float cooldown = 0.5f;
    public override float Duration => duration;

    public Projectile projectile;

    protected override void OnApply(GameObject target)
    {
        component.projectilePrefab = projectile;
        component.cooldown = cooldown;
    }
}
