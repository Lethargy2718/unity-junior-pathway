using UnityEngine;

public static class ParticleSpawner
{
    public static void Spawn(ParticleSystem psPrefab, Vector3 pos, Quaternion? rotation = null)
    {
        if (psPrefab == null) return;

        Quaternion rot = rotation ?? Quaternion.identity;
        ParticleSystem psInstance = Object.Instantiate(psPrefab, pos, rot);
        psInstance.Play();

        float lifetime = psInstance.main.duration + psInstance.main.startLifetime.constantMax;
        Object.Destroy(psInstance.gameObject, lifetime);
    }

    public static void SpawnAfterCollision(ParticleSystem psPrefab, Collision collision)
    {
        if (psPrefab == null || collision == null) return;

        var collisionInfo = collision.contacts[0];
        Vector3 impactPoint = collisionInfo.point;
        Vector3 direction = collisionInfo.normal;
        Quaternion rotation = Quaternion.LookRotation(direction);

        Spawn(psPrefab, impactPoint, rotation);
    }
}
