using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    public Powerup powerup;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PowerupManager.Instance.Apply(powerup);
            Destroy(gameObject);
        }
    }
}