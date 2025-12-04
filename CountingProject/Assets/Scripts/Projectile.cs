using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public Vector3 direction = Vector3.zero;
    public float speed = 10.0f;
    public float lifeTime = 5.0f;
    public ParticleSystem psPrefab;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(DestructionCoroutine());
    }

    private void Update()
    {
        rb.linearVelocity = direction * speed;
    }

    private IEnumerator DestructionCoroutine()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided");
        Quaternion rotation = Quaternion.LookRotation(collision.contacts[0].normal);
        ParticleSystem ps = Instantiate(psPrefab, transform.position, rotation);
        ps.Play();
        Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
        Destroy(gameObject);
    }
}
