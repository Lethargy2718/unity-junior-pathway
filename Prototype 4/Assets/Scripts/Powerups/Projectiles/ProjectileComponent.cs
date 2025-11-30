using UnityEngine;
using UnityTimer;
using System.Collections.Generic;

public class ProjectileComponent : MonoBehaviour
{
    public float cooldown = 1f;
    public Projectile projectilePrefab;

    private bool canShoot = true;
    private readonly List<Projectile> projectiles = new();

    private void Update()
    {
        if (Input.GetMouseButton(0) && canShoot)
        {
            ShootAllEnemies();
            canShoot = false;
            this.AttachTimer(cooldown, () =>
            {
                canShoot = true;
            });
        }
    }

    private void ShootAllEnemies()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        foreach (Enemy enemy in enemies)
        {
            Shoot(enemy);
        }
    }

    private void Shoot(Enemy enemy)
    {
        Projectile proj = Instantiate(projectilePrefab, transform.position, Quaternion.LookRotation(enemy.transform.position - transform.position));
        projectiles.Add(proj);

        if (proj.TryGetComponent<FollowTarget>(out var followComponent))
        {
            followComponent.target = enemy.transform;
        }
    }

    private void OnDestroy()
    {
        foreach (var proj in projectiles)
        {
            if (proj != null)
            {
                Destroy(proj.gameObject);
            }
        }
    }
}
