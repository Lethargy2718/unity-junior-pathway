using UnityEngine;
using UnityTimer;

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager Instance;
    public GameObject powerupIndicator;
    public SpawnManager spawnManager;
    public GameObject player;

    private int numPowerups = 0;
    private bool HasPowerup => numPowerups > 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (HasPowerup && powerupIndicator != null)
        {
            powerupIndicator.transform.position = player.transform.position + new Vector3(0, -0.5f, 0);
        }
    }

    public void Apply(IPowerup powerup)
    {
        numPowerups++;
        powerupIndicator.SetActive(true);

        powerup.Apply(player);

        this.AttachTimer(powerup.Duration, () =>
        {
            RemovePowerup(powerup);
            if (!HasPowerup)
            {
                spawnManager.WaitThenSpawnPowerup();
            }
        });
    }

    private void RemovePowerup(IPowerup powerup)
    {
        if (powerup == null) return;

        numPowerups--;
        powerup.Remove(player);
        if (!HasPowerup) powerupIndicator.SetActive(false);
    }
}