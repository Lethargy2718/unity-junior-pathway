using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    private AnimalHunger animalHunger;

    private void Awake()
    {
        animalHunger = GetComponent<AnimalHunger>();   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            if (animalHunger.Feed(1))
            {
                Destroy(gameObject);
                //Destroy(other.gameObject); // projectile
                other.gameObject.SetActive(false);
                GameManager.Instance.Score++;
            }
        }
    }
}
