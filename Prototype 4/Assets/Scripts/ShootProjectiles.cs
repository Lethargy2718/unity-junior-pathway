using System.Collections;
using UnityEngine;
using UnityTimer;

public class ShootProjectiles : MonoBehaviour
{
    public float initialDelay = 3.0f;
    public float cooldown = 5.0f;
    public int maxBullets = 3;
    public float ProjectileLifeTime => cooldown * maxBullets;
    public GameObject target;
    public Projectile projectilePrefab;

    private void Start()
    {
        target = GameObject.Find("Player");
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(cooldown);
        }
    }

    private void Shoot()
    {
        Projectile proj = Instantiate(projectilePrefab, transform.position, Quaternion.LookRotation(target.transform.position - transform.position));
        if (proj.TryGetComponent<FollowTarget>(out var followComponent))
        {
            followComponent.target = target.transform;
        }

        proj.AttachTimer(ProjectileLifeTime, () =>
        {
            Destroy(proj.gameObject);
        });
    }
}
