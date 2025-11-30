using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Target : MonoBehaviour
{
    public float minSpeed = 12f;
    public float maxSpeed = 16f;
    public float maxTorque = -10f;
    public float xRange = 4f;
    public float yPos = 6f;
    public int points = 5;
    public ParticleSystem explosionParticle;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.AddForce(RandForce(), ForceMode.Impulse);

        rb.AddTorque(RandTorque(), RandTorque(), RandTorque(), ForceMode.Impulse);

        transform.position = RandSpawnPos();
    }

    Vector3 RandForce() => Random.Range(minSpeed, maxSpeed) * Vector3.up;
    float RandTorque() => Random.Range(-maxTorque, maxTorque);
    Vector3 RandSpawnPos() => new Vector3(Random.Range(-xRange, xRange), yPos);

    private void OnMouseDown()
    {
        if (!GameManager.Instance.isGameActive) return;
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        GameManager.Instance.Score += points;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gameObject.CompareTag("Bad")) {
            GameManager.Instance.GameOver();
        }
        Destroy(gameObject);
    }
}
