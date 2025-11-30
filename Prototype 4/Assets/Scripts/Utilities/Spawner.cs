using UnityEngine;

public static class Spawner
{
    public static void SpawnRandom(GameObject[] prefabs, Vector3 position, float offsetRange = 8f)
    {
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
        Spawn(prefab, position, offsetRange);        
    }

    private static void Spawn(GameObject prefab, Vector3 position, float offsetRange)
    {
        GameObject.Instantiate(prefab, GenerateSpawnPosition(position, offsetRange), prefab.transform.rotation);
    }

    public static Vector3 GenerateSpawnPosition(Vector3 position = default, float offsetRange = 0f)
    {
        if (position == null) position = Vector3.zero;

        return position + new Vector3(Random.Range(-offsetRange, offsetRange), 0, Random.Range(-offsetRange, offsetRange));
    }
}
