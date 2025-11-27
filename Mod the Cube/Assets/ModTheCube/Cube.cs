using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;
    public Color initialColor = new Color(0f, 0f, 0f);

    [Header("Color Settings")]
    [Min(0f)] public float cyclesR = 1f;
    [Min(0f)] public float cyclesG = 2f;
    [Min(0f)] public float cyclesB = 0.5f;

    [Header("Rotation Settings")]
    [Min(0f)] public float cyclesX = 1f;
    [Min(0f)] public float cyclesY = 2f;
    [Min(0f)] public float cyclesZ = 0.5f;

    private void Start()
    {
        Material material = Renderer.material;
        material.color = initialColor;
    }
    
    private void Update()
    {
        Color();
        Rotate();
    }

    private void Color()
    {
        Color c = Renderer.material.color;
        c.r = Mathf.PingPong(cyclesR * 2 * Time.time, 1f);
        c.g = Mathf.PingPong(cyclesG * 2 * Time.time, 1f);
        c.b = Mathf.PingPong(cyclesB * 2 * Time.time, 1f);
        Renderer.material.color = c;
    }

    private void Rotate()
    {
        Vector3 rotation = new Vector3(cyclesX, cyclesY, cyclesZ);
        transform.Rotate(Time.deltaTime * 360f * rotation);
    }
}
