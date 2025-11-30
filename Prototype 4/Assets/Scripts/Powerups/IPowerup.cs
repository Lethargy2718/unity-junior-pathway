using UnityEngine;

public interface IPowerup
{
    float Duration { get; }
    void Apply(GameObject target);
    void Remove(GameObject target);
}
