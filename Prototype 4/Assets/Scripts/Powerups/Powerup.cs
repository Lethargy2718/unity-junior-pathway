using UnityEngine;

public abstract class Powerup : ScriptableObject
{
    public virtual string PowerupName => "Powerup";
    public virtual float Duration => 5f;

    public abstract void OnApply(GameObject target);

    public abstract void OnRemove(GameObject target);
}