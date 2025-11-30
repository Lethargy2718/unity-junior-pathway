using UnityEngine;
using UnityTimer;

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager Instance;
    public GameObject powerupIndicator;
    public SpawnManager spawnManager;
    public GameObject player;
    

    private bool hasPowerup = false;
    private IPowerup currentPowerup;

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
        if (hasPowerup && powerupIndicator != null)
        {
            powerupIndicator.transform.position = player.transform.position + new Vector3(0, -0.5f, 0);
        }
    }

    public void Apply(IPowerup powerup)
    {

        if (hasPowerup)
        {
            RemoveCurrentPowerup();
        }
        hasPowerup = true;
        powerupIndicator.SetActive(true);

        powerup.Apply(player);
        currentPowerup = powerup;

        this.AttachTimer(powerup.Duration, () =>
        {
            RemoveCurrentPowerup();
            spawnManager.WaitThenSpawnPowerup();
        });
    }

    private void RemoveCurrentPowerup()
    {
        if (currentPowerup != null)
        {
            currentPowerup.Remove(player);
            currentPowerup = null;
        }

        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }
}