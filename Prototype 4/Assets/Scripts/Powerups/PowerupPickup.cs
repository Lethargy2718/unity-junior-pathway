using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    public ScriptableObject powerupSO;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (powerupSO is IPowerup powerup)
            {
                PowerupManager.Instance.Apply(powerup);
            }

            Destroy(gameObject);
        }
    }
}
