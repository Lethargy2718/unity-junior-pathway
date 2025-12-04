using System.Collections.Generic;
using UnityEngine;

public class CongratScript : MonoBehaviour
{
    public TextMesh Text;
    public ParticleSystem SparksParticles;
    
    private readonly List<string> TextToDisplay = new();

    private const float rotatingSpeed = 180f;
    private float TimeToNextText = 0f;

    private int CurrentText = 0;
    
    void Start()
    {
        TextToDisplay.Add("Congratulation");
        TextToDisplay.Add("All Errors Fixed");

        Text.text = TextToDisplay[0];
        
        SparksParticles.Play();
    }

    void Update()
    {
        TimeToNextText += Time.deltaTime;

        if (TimeToNextText > 1.5f)
        {
            TimeToNextText = 0.0f;
            

            Text.text = TextToDisplay[(++CurrentText) % TextToDisplay.Count];
        }

        transform.Rotate(Vector3.forward, rotatingSpeed * Time.deltaTime);
    }
}