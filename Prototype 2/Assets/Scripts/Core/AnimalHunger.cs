using UnityEngine;
using UnityEngine.UI;

public class AnimalHunger : MonoBehaviour
{
    public int animalHunger = 1;
    private Slider slider;
    private int fed = 0;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        slider.maxValue = animalHunger;
    }

    public bool Feed(int i)
    {
        fed += i;
        slider.value = fed;

        if (fed >= animalHunger)
        {
            return true;
        }
        return false;
    }
}
